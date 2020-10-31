using Core.Entities;
using Core.Interfaces.Services;
using Core.Interfaces.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ArtigoService : BaseService<Artigo>, IArtigoService
    {
        private UserManager<Usuario> _userManager;
        private IAutorService _autorService;

        public ArtigoService(
            IUnitOfWork unitOfWork,
            UserManager<Usuario> userManager,
            IAutorService autorService
        ) : base(unitOfWork) { 
            _userManager = userManager;
            _autorService = autorService;
        }

        public Artigo Criar(string titulo, string idUsuario)
        {
            Autor autor = _autorService.Obter(idUsuario);

            if (autor == null)
                autor = _autorService.Criar(idUsuario);

            var artigo = new Artigo(titulo, autor);

            Adicionar(artigo);
            Salvar();

            return artigo;
        }

        public Artigo Obter(int? idArtigo) =>
            Obter(item => item.Id == idArtigo)
                ?? throw new ArgumentNullException("Artigo não encontrado");

        public Artigo Publicar(int? idArtigo)
        {
            Artigo artigo = Obter(idArtigo); 
            artigo.Publicar();
            Salvar();
            return artigo;
        }

        public Artigo Inativar(int? idArtigo)
        {
            Artigo artigo = Obter(idArtigo);
            artigo.Inativar();
            Salvar();
            return artigo;
        }

        public Artigo Remover(int? idArtigo)
        {
            Artigo artigo = Obter(idArtigo);
            artigo.Remover();
            Salvar();
            return artigo;
        }

        public Artigo Impulsionar(int? idArtigo)
        {
            Artigo artigo = Obter(idArtigo);
            artigo.Impulsionar();
            Salvar();
            return artigo;
        }

        public IEnumerable<Artigo> Obter(string termo, int? skip = 0, int? take = 10) =>
            Obter(
                item => 
                (item.Titulo.StartsWith(termo ?? "")) 
                || (!string.IsNullOrEmpty(item.Descricao) 
                    && item.Descricao.StartsWith(termo ?? "")
                    ),
                skip,
                take
            );
    }
}