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

        public IEnumerable<Tema> Obter(string termo, int? skip, int? take) => 
            Obter(
                item => (string.IsNullOrEmpty(termo) || item.Nome.StartsWith(termo)) && !item.DataRemocao.HasValue,
                skip, 
                take
            );

        public Tema Obter(int? idTema) =>
            Obter(item => item.Id == idTema.Value && !item.DataRemocao.HasValue) ?? throw new ArgumentNullException("Tema não encontrado");

        public Tema Criar(string nome, string descricao)
        {
            var tema = new Tema(nome, descricao);
            tema.Validar();

            Adicionar(tema);
            Salvar();

            return tema;
        }

        public int Remover(int? idTema)
        {
            if(!idTema.HasValue)
                throw new ArgumentException("É necessário informar o identificador do tema");
            
            Tema tema = Obter(item => item.Id == idTema.Value);

            if(tema.EstaRemovido())
                throw new ArgumentException("Este tema já esta removido");
            
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
            Salvar();
        }
    }
}