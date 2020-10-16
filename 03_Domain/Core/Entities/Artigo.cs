using System;
using Core.Enums;

namespace Core.Entities
{
    public class Artigo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public EstadoArtigo Estado { get; set; }
        public int? TemaId { get; set; }
        public virtual Tema Tema { get; set; }
        public int? CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public int AutorId { get; set; }
        public virtual Autor Autor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataRemocao { get; set; }
        public DateTime? DataInativacao { get; set; }
        public DateTime? DataImpulsionamento { get; set; }
        public DateTime? DataPublicacao { get; set; }

    }
}