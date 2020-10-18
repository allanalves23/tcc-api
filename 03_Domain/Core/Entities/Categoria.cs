using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Artigo> Artigos { get; set; }
        public int TemaId { get; set; }
        public virtual Tema Tema { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataRemocao { get; set; }

        public void Validar()
        {
            if(string.IsNullOrEmpty(Nome))
                throw new ArgumentException("É necessário informar o nome da categoria");

            if(Nome.Length > 255)
                throw new ArgumentException("Nome da categoria deve ter até 255 caracteres");

            if(Descricao.Length > 255)
                throw new ArgumentException("Descrição da categoria deve ter até 255 caracteres");

            if(Tema == null && TemaId.Equals(default(int)))
                throw new ArgumentException("É necessário informar o tema da categoria");
        }

        public void Atualizar() => DataAtualizacao = DateTime.Now;

        public void Remover() => DataRemocao = DateTime.Now;
    }
}