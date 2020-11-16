using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Services
{
    public class CategoriaService : BaseService<Categoria>, ICategoriaService
    {
        private readonly ITemaService _temaService;

        public CategoriaService(IUnitOfWork unitOfWork, ITemaService temaService) 
            : base(unitOfWork) 
        { 
            _temaService = temaService;
        }

        public IEnumerable<Categoria> Obter(string termo, int? skip, int? take) => 
            Obter(
                ObterFiltroDeBusca(termo),
                skip, 
                take
            );
        
        public int ObterQuantidade(string termo) =>
            Contar(ObterFiltroDeBusca(termo));

        private Func<Categoria, bool> ObterFiltroDeBusca(string termo) =>
            item => (string.IsNullOrEmpty(termo) || item.Nome.ToUpper().StartsWith(termo.ToUpper())) && !item.DataRemocao.HasValue;

        public Categoria Obter(int? idCategoria, bool incluirRemovido = false) =>
            Obter(item => item.Id == idCategoria.Value && (incluirRemovido ? incluirRemovido : !item.DataRemocao.HasValue)) 
                ?? throw new ArgumentNullException("Categoria não encontrada");

        public IEnumerable<Categoria> Obter(string termo, int temaId, int? skip, int? take)
        {
            if (!_temaService.Existe(item => item.Id == temaId && !item.EstaRemovido()))
                throw new ArgumentException("Tema não encontrado");

            return Obter(
                item => (string.IsNullOrEmpty(termo) || item.Nome.ToUpper().StartsWith(termo.ToUpper())) && !item.DataRemocao.HasValue && item.TemaId == temaId,
                skip, 
                take
            );
        }

        public Categoria Criar(string nome, string descricao, int? idTema)
        {
            var categoria = new Categoria(nome, descricao, idTema);
            categoria.Validar();

            ValidarTemaDaCategoria(idTema.Value);
            
            Categoria categoriaExistente = Obter(item => item.Nome == categoria.Nome);
            if(categoriaExistente == null)
                Adicionar(categoria);
            else 
            {
                if(categoriaExistente.EstaRemovido())
                {
                    ReativarCategoria(categoriaExistente);
                    categoria = categoriaExistente;
                }
                else
                    throw new ArgumentException("Já existe um categoria cadastrada com este nome");
            }

            Salvar();
            return categoria;
        }

        private void ReativarCategoria(Categoria categoria)
        {
            if(categoria == null)
                throw new InvalidOperationException("Categoria não informada");

            if(!categoria.DataRemocao.HasValue)
                throw new ArgumentException("Este categoria já está reativada");

            categoria.Reativar();
        }

        public int Remover(int? idCategoria)
        {
            if(!idCategoria.HasValue)
                throw new ArgumentException("É necessário informar o identificador da categoria");
            
            Categoria categoria = Obter(item => item.Id == idCategoria.Value) 
                ?? throw new ArgumentNullException("Categoria não encontrada");

            if(categoria.EstaRemovido())
                throw new Exception("Este categoria já esta removida");
            
            categoria.Remover();
            Salvar();

            return categoria.Id;
        }

        public void Atualizar(int? idCategoria, string nome, string descricao, int? idTema)
        {
            if(!idCategoria.HasValue)
                throw new ArgumentException("É necessário informar a categoria");

            Categoria categoria = Obter(item => item.Id == idCategoria.Value && !item.DataRemocao.HasValue)
                ?? throw new ArgumentNullException("Categoria não encontrada");

            categoria.Atualizar(nome, descricao, idTema);
            categoria.Validar();

            ValidarTemaDaCategoria(idTema.Value);

            Salvar();
        }

        private void ValidarTemaDaCategoria(int idTema)
        {
            if(!_temaService.Existe(item => item.Id == idTema))
                throw new ArgumentNullException("Tema não encontrado");
        }
    }
}