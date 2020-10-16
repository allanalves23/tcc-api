using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Tema
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Artigo> Artigos { get; set; }
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

        public void Validar()
        {
            if(string.IsNullOrEmpty(Nome))
                throw new ArgumentException("É necessário informar o nome da categoria");

            if(Nome.Length > 255)
                throw new ArgumentException("Nome da categoria deve ter até 255 caracteres");

            if(Descricao.Length > 255)
                throw new ArgumentException("Descrição da categoria deve ter até 255 caracteres");
        }

        public void Atualizar() => DataAtualizacao = DateTime.Now;

        public void Remover() => DataRemocao = DateTime.Now;

        public bool EstaRemovido() => !DataRemocao.Equals(null);
    }
}