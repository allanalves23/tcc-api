using System;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Services
{
    public class PerfilDeAcessoService : BaseService<PerfilDeAcesso>, IPerfilDeAcessoService
    {
        public PerfilDeAcessoService(IUnitOfWork unitOfWork) 
            : base(unitOfWork) { }

        public PerfilDeAcesso Obter(string usuarioId) =>
            Obter(item => item.UsuarioId == usuarioId) 
                ?? throw new ArgumentNullException("Perfil de Acesso não encontrado"); 
        

        public PerfilDeAcesso Criar(Usuario usuario, string perfil)
        {
            PerfilDeAcesso perfilDeAcesso = Obter(item => item.UsuarioId == usuario.Id);

            if (perfilDeAcesso == null)
            {
                perfilDeAcesso = new PerfilDeAcesso(usuario, perfil);
                Adicionar(perfilDeAcesso);
            } else {
                perfilDeAcesso.Atualizar(perfil);
            }

            Salvar();

            return perfilDeAcesso;
        }
    }
}