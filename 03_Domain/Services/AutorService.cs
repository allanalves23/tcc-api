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

        public Autor Obter(string idUsuario) => Obter(item => item.UsuarioId == idUsuario);

        public Autor Obter(int? idAutor) => Obter(item => item.Id == idAutor);

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
    }
}