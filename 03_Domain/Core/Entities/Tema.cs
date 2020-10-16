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
    }
}