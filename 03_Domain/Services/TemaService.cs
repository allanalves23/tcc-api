using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Services
{
    public class TemaService : BaseService<Tema>, ITemaService
    {
        public TemaService(IUnitOfWork unitOfWork) 
            : base(unitOfWork) { }

        private Func<Tema, bool> ObterFiltroDeBusca(string termo) =>
            item => (string.IsNullOrEmpty(termo) || item.Nome.ToUpper().StartsWith(termo.ToUpper())) && !item.DataRemocao.HasValue; 

        public IEnumerable<Tema> Obter(string termo, int? skip, int? take) => 
            Obter(
                ObterFiltroDeBusca(termo),
                skip, 
                take
            );

        public int ObterQuantidade(string termo) => Contar(ObterFiltroDeBusca(termo));

        public Tema Obter(int? idTema, bool incluirRemovido = false) =>
            Obter(item => item.Id == idTema && (incluirRemovido ? incluirRemovido : !item.DataRemocao.HasValue)) 
                ?? throw new ArgumentNullException("Tema não encontrado");

        public Tema Criar(string nome, string descricao)
        {
            var tema = new Tema(nome, descricao);
            tema.Validar();
            
            Tema temaExistente = Obter(item => item.Nome == tema.Nome);
            if(temaExistente == null)
                Adicionar(tema);
            else 
            {
                if(temaExistente.EstaRemovido())
                {
                    ReativarTema(temaExistente);
                    tema = temaExistente;
                }
                else
                    throw new ArgumentException("Já existe um tema cadastrado com este nome");
            }

            Salvar();
            return tema;
        }

        private void ReativarTema(Tema tema)
        {
            if(tema == null)
                throw new InvalidOperationException("Tema não informado");

            if(!tema.DataRemocao.HasValue)
                throw new ArgumentException("Este tema já está reativado");

            tema.Reativar();
        }

        public int Remover(int? idTema)
        {
            if(!idTema.HasValue)
                throw new ArgumentException("É necessário informar o identificador do tema");
            
            Tema tema = Obter(item => item.Id == idTema.Value) 
                ?? throw new ArgumentNullException("Tema não encontrado");

            if(tema.EstaRemovido())
                throw new Exception("Este tema já esta removido");
            
            tema.Remover();
            Salvar();

            return tema.Id;
        }

        public void Atualizar(int? idTema, string nome, string descricao)
        {
            if(!idTema.HasValue)
                throw new ArgumentException("É necessário informar o tema");

            Tema tema = Obter(item => item.Id == idTema.Value && !item.DataRemocao.HasValue)
                ?? throw new ArgumentNullException("Tema não encontrado");

            tema.Atualizar(nome, descricao);
            tema.Validar();

            Salvar();
        }
    }
}