using System;
using Core.Entities;

namespace API.Models
{
    public class ArtigoModel
    {
        public int? Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string Estado { get; set; }
        public string Url { get; set; }
        public TemaModel Tema { get; set; }
        public CategoriaModel Categoria { get; set; }
        public AutorModel Autor { get; set; }
        public DateTime? DataCadastro { get; set; }
    
        public ArtigoModel() { }

        public ArtigoModel(Artigo artigo) {
            Id = artigo?.Id;
            Titulo = artigo?.Titulo;
            Descricao = artigo?.Descricao;
            Conteudo = artigo?.Conteudo;
            Estado = artigo?.Estado.ToString().ToUpper();
            DataCadastro = artigo?.DataCadastro;
            Url = artigo?.UrlPersonalizada;

            if (artigo?.Tema != null)
                Tema = new TemaModel(artigo.Tema);

            if (artigo?.Categoria != null)
                Categoria = new CategoriaModel(artigo.Categoria);

            if (artigo?.Autor != null)
                Autor = new AutorModel(artigo.Autor);
        }
    }
}
