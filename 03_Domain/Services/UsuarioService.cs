using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class UsuarioService : IUsuarioService
    {
        private UserManager<Usuario> _userManager;
        private IPerfilDeAcessoService _perfilDeAcessoService;

        public UsuarioService(UserManager<Usuario> userManager, IPerfilDeAcessoService perfilDeAcessoService) 
        {
            _userManager = userManager;
            _perfilDeAcessoService = perfilDeAcessoService;
        }

        public async Task<(Usuario, PerfilDeAcesso)> ObterAsync(string id) =>
            (
                await _userManager.FindByIdAsync(id) ?? throw new ArgumentNullException("Usuario não encontrado"), 
                _perfilDeAcessoService.Obter(id) ?? throw new ArgumentNullException("Perfil de Acesso não encontrado")
            );

        public IEnumerable<(Usuario, PerfilDeAcesso)> Obter(string termo, int skip, int take) =>
            _userManager
                .Users
                .Where(item => 
                    string.IsNullOrEmpty(termo) 
                    || (item.NormalizedEmail == termo.ToUpper())
                )
                .Skip(skip)
                .Take(take)
                .Select(item => 
                    new Tuple<Usuario, PerfilDeAcesso>(
                        item, _perfilDeAcessoService.Obter(item.Id)
                    ).ToValueTuple()
                );

        public void DefinirPerfilDeAcesso(Usuario usuario, string perfil)
        {
            if (usuario == null)
                throw new ArgumentException("Usuário não informado");

            PerfilDeAcesso perfilDeAcesso = _perfilDeAcessoService.Obter(usuario.Id);

            if (perfilDeAcesso == null)
                _perfilDeAcessoService.Criar(usuario, perfil);
            else
            {
                perfilDeAcesso.Atualizar(perfil);
                _perfilDeAcessoService.Salvar();
            }
        }
        
        public async Task AlterarSenhaAsync(string id, string senhaAtual, string novaSenha, string confirmacaoNovaSenha)
        {
            if (string.IsNullOrEmpty(novaSenha))
                throw new ArgumentException("É necessário informar a Senha");

            if (string.IsNullOrEmpty(confirmacaoNovaSenha))
                throw new ArgumentException("É necessário informar a confirmação de Senha");

            if (!novaSenha.Equals(confirmacaoNovaSenha))
                throw new ArgumentException("As Senhas não conferem, ambas precisam ser iguais");

            Usuario usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
                throw new ArgumentNullException("Usuário não encontrado");

            IdentityResult result = await _userManager.ChangePasswordAsync(usuario, senhaAtual, novaSenha);

            if (!result.Succeeded)
                throw new Exception("Ocorreu um erro ao alterar a Senha, se persistir reporte");
        }

        public async Task<(Usuario, PerfilDeAcesso)> AtualizarAsync(string id, string email, string perfil)
        {
            Usuario usuario = await _userManager.FindByIdAsync(id);

            ValidarEmail(email);
            ValidarPerfilDeAcesso(perfil);

            await _userManager.ChangeEmailAsync(
                usuario,
                email,
                await _userManager.GenerateChangeEmailTokenAsync(usuario, email)
            );

            await _userManager.SetUserNameAsync(usuario, email);

            PerfilDeAcesso perfilDeAcesso = AlterarPerfilDeAcesso(usuario, perfil);
            
            return (usuario, perfilDeAcesso);
        }

        public (Usuario, PerfilDeAcesso) Criar(string email, string senha, string perfil)
        {
            ValidarParaCadastro(email, senha, perfil);

            Usuario usuario = ConstruirUsuarioAsync(email).Result;
            CriarSenhaAsync(usuario, senha).Wait();
            _userManager.AddToRoleAsync(usuario, "tcc_api").Wait();

            PerfilDeAcesso perfilDeAcesso = AdicionarPerfilDeAcesso(usuario, perfil);
            
            return (usuario, perfilDeAcesso);
        }

        private async Task<Usuario> ConstruirUsuarioAsync(string email)
        {
            Usuario usuario = await _userManager.FindByEmailAsync(email);

            if (usuario != null)
            {
                if (await _userManager.HasPasswordAsync(usuario))
                    throw new ArgumentException("Já existe um usuário cadastrado com este E-mail");
                else
                    return usuario;
            } else {
                usuario = new Usuario 
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };

                IdentityResult result = await _userManager.CreateAsync(usuario);

                if (!result.Succeeded)
                    throw new Exception("Ocorreu um erro ao cadastrar o Usuário, se persistir reporte");

                return usuario;
            }
        }

        private async Task CriarSenhaAsync(Usuario usuario, string senha)
        {
            if (await _userManager.HasPasswordAsync(usuario)) return;

            IdentityResult result = await _userManager.AddPasswordAsync(usuario, senha);

            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(usuario);
                throw new Exception(result.Errors.FirstOrDefault().Description ?? "Ocorreu um erro ao adicionar a senha do Usuário, se persistir reporte");
            }
        }

        private void ValidarParaCadastro(string email, string senha, string perfil)
        {
            ValidarEmail(email);
            ValidarSenha(senha);
            ValidarPerfilDeAcesso(perfil);
        }

        private void ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("É necessário fornecer um E-mail");

            if (!(email.Contains('@') && email.Contains('.')))
                throw new ArgumentException("E-mail inválido");
        }

        private void ValidarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
                throw new ArgumentException("É necessário fornecer uma Senha");
        }

        private  void ValidarPerfilDeAcesso(string perfil)
        {
            if (string.IsNullOrEmpty(perfil))
                throw new ArgumentException("É necessário fornecer um Perfil de Acesso");

            if (!Enum.IsDefined(typeof(TipoUsuario), perfil))
                throw new ArgumentException("Perfil de Acesso inválido");
        }

        public void Remover(string id)
        {
            PerfilDeAcesso perfilDeAcesso = _perfilDeAcessoService.Obter(id);
            perfilDeAcesso.Desativar();
            _perfilDeAcessoService.Salvar();
        }

        public void Reativar(string id)
        {
            PerfilDeAcesso perfilDeAcesso = _perfilDeAcessoService.Obter(id);
            perfilDeAcesso.Reativar();
            _perfilDeAcessoService.Salvar();
        }
        
        private PerfilDeAcesso AdicionarPerfilDeAcesso(Usuario usuario, string perfil) =>
            _perfilDeAcessoService.Criar(usuario, perfil);

        private PerfilDeAcesso AlterarPerfilDeAcesso(Usuario usuario, string perfil)
        {
            PerfilDeAcesso perfilDeAcesso = _perfilDeAcessoService.Obter(usuario.Id);
            
            if (perfilDeAcesso == null)
                throw new ArgumentNullException("Perfil de Acesso não encontrado");

            perfilDeAcesso.Atualizar(perfil);
            _perfilDeAcessoService.Salvar();

            return perfilDeAcesso;
        }

        public bool EhAdmin(string usuarioId)
        {
            PerfilDeAcesso perfilDeAcesso = _perfilDeAcessoService.Obter(usuarioId)
                ?? throw new ArgumentNullException("Perfil de acesso não encontrado");

            return perfilDeAcesso?.Perfil == TipoUsuario.Admin;
        }
    }
}