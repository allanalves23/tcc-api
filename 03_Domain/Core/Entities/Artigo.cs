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

        public void Validar()
        {
            if(string.IsNullOrEmpty(Titulo))
                throw new ArgumentException("É necessário informar o titulo do artigo");

            if(Titulo.Length > 100)
                throw new ArgumentException("Titulo do artigo deve possuir até 100 caracteres");

            if(Descricao.Length > 255)
                throw new ArgumentException("Descrição do artigo deve possuir até 255 caracteres");
        }

        public void Atualizar() => DataAtualizacao = DateTime.Now;
        public void Inativar() => DataInativacao = DateTime.Now;
        public void Remover() => DataRemocao = DateTime.Now;
        public void Impulsionar() => DataImpulsionamento = DateTime.Now;

    }
}