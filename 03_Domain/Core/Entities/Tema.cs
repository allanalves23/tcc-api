using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class Tema
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Artigo> Artigos { get; set; }
        public virtual IEnumerable<Categoria> Categorias { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataRemocao { get; set; }

        public Tema() { }

        public Tema(string nome, string descricao) 
        {
            Nome = nome;
            Descricao = descricao;
            DataCadastro = DateTime.Now;
        }

        public bool PertenceACategoria(Categoria categoria) => Categorias.Any(item => item.Id == categoria.Id);

        public void Validar()
        {
            if(string.IsNullOrEmpty(Nome))
                throw new ArgumentException("É necessário informar o nome do Tema");

            if(Nome.Length > 255)
                throw new ArgumentException("Nome do Tema deve ter até 255 caracteres");

            if(!string.IsNullOrEmpty(Descricao) && Descricao.Length > 255)
                throw new ArgumentException("Descrição do Tema deve ter até 255 caracteres");
        }

        public void Atualizar(string nome, string descricao)
        {
            Nome = nome ?? Nome;
            Descricao = descricao ?? Descricao;
            DataAtualizacao = DateTime.Now;
        }

        public void Remover() => DataRemocao = DateTime.Now;

        public void Reativar() => DataRemocao = null;

        public bool EstaRemovido() => DataRemocao != null;
    }
}