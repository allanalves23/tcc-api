using System;
using System.Collections.Generic;
using Core.Enums;

namespace Core.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public virtual IEnumerable<Artigo> Artigos { get; set; }
        public TipoUsuario Tipo { get; set; }
        public Genero Genero { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataRemocao { get; set; }
    }
}