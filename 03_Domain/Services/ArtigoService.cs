using Core.Entities;
using Core.Interfaces.Services;
using Core.Interfaces.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Core.Enums;

namespace Services
{
    public class ArtigoService : BaseService<Artigo>, IArtigoService
    {
        private UserManager<Usuario> _userManager;
        private IAutorService _autorService;
        private ITemaService _temaService;
        private ICategoriaService _categoriaService;

        public ArtigoService(
            IUnitOfWork unitOfWork,
            UserManager<Usuario> userManager,
            IAutorService autorService,
            ITemaService temaService,
            ICategoriaService categoriaService
        ) : base(unitOfWork) { 
            _userManager = userManager;
            _autorService = autorService;
            _temaService = temaService;
            _categoriaService = categoriaService;
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

        public Artigo Atualizar(int? id, string titulo, string descricao, string conteudo)
        {
            if (!id.HasValue)
                throw new ArgumentException("É necessário informar o artigo");

            Artigo artigo = Obter(id)
                ?? throw new ArgumentNullException("Artigo não encontrado");

            artigo.Atualizar(titulo, descricao, conteudo);

            Salvar();
            return artigo;
        }

        public Artigo Atualizar(int? id, int? temaId, int? categoriaId)
        {
            Artigo artigo = DefinirTema(id, temaId);
            artigo = DefinirCategoria(id, categoriaId);
            return artigo;
        }

        public Artigo Atualizar(int? id, string urlPersonalizada)
        {
            Artigo artigo = Obter(id);
            
            if (Existe(item => item.Id != id && item.UrlPersonalizada == urlPersonalizada))
                throw new ArgumentException("Esta Url Personalizada já está definida para outro artigo");

            artigo.Atualizar(urlPersonalizada);
            Salvar();

            return artigo;
        }

        private Artigo DefinirTema(int? artigoId, int? temaId)
        {
            Artigo artigo = Obter(artigoId);

            if (temaId.HasValue)
            {
                Tema tema = _temaService.Obter(temaId);
                artigo.AplicarTema(tema);
            } else {
                artigo.RemoverTema();
            }

            Salvar();
            return artigo;
        }

        private Artigo DefinirCategoria(int? artigoId, int? categoriaId)
        {
            Artigo artigo = Obter(artigoId);
            
            if (categoriaId.HasValue)
            {
                Categoria categoria = _categoriaService.Obter(categoriaId);
                artigo.AplicarCategoria(categoria);
            } else {
                artigo.RemoverCategoria();
            }

            Salvar();
            return artigo;
        }

        public Artigo Obter(int? idArtigo) =>
            Obter(item => item.Id == idArtigo && item.Estado != EstadoArtigo.Removido)
                ?? throw new ArgumentNullException("Artigo não encontrado");

        public Artigo Obter(string urlPersonalizada) =>
            Obter(item => item.UrlPersonalizada == urlPersonalizada && item.Estado != EstadoArtigo.Removido)
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
                (item.Titulo.StartsWith(termo ?? "") 
                || 
                (
                    !string.IsNullOrEmpty(item.Descricao) 
                    && item.Descricao.StartsWith(termo ?? "")
                ))
                && item.Estado != EstadoArtigo.Removido,
                skip,
                take
            );
    }
}