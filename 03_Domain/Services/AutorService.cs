using System;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class AutorService : BaseService<Autor>, IAutorService
    {
        private UserManager<Usuario> _userManager;

        public AutorService(IUnitOfWork unitOfWork, UserManager<Usuario> userManager) 
            : base(unitOfWork) {
                _userManager = userManager;
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
            Usuario usuario = _userManager.FindByIdAsync(idUsuario).Result
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
    }
}