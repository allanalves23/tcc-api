using System;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Services
{
    public class AutorService : BaseService<Autor>, IAutorService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilDeAcessoService _perfilDeAcessoService;

        public AutorService(IUnitOfWork unitOfWork, IUsuarioService usuarioService, IPerfilDeAcessoService perfilDeAcessoService) 
            : base(unitOfWork) {
                _usuarioService = usuarioService;
                _perfilDeAcessoService = perfilDeAcessoService;
            }

        public Autor Obter(string idUsuario, bool lancaExcecao = false)
        {
            Autor autor = Obter(item => item.UsuarioId == idUsuario);
            
            if (autor == null && lancaExcecao)
                throw new ArgumentNullException("Autor não encontrado");

            return autor;
        }

        public Autor Obter(int? idAutor) => Obter(item => item.Id == idAutor)
            ?? throw new ArgumentNullException("Autor não encontrado");

        public Autor Criar(string idUsuario)
        {
            Usuario usuario = _usuarioService.ObterAsync(idUsuario).Result.usuario
                ?? throw new ArgumentException("Usuário não encontrado");

            var autor = new Autor(usuario);
            autor.Validar();

            Adicionar(autor);
            Salvar();

            return autor;
        }

        public Autor Atualizar(int? id, string nome, string email, string genero)
        {
            if (!id.HasValue)
                throw new ArgumentException("É necessário informar o autor");

            Autor autor = Obter(id);
            autor.Atualizar(nome, email, genero);

            Salvar();

            return autor;
        }

        public bool AutorEhAdmin(string usuarioId) => _usuarioService.EhAdmin(usuarioId);
    }
}