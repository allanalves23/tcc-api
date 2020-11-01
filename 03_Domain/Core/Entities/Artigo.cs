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
        public string UrlPersonalizada { get; set; }
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

        public Artigo() { }

        public Artigo(string titulo, Autor autor) { 
            Titulo = titulo;
            Autor = autor;
            DataCadastro = DateTime.Now;
            Estado = EstadoArtigo.Rascunho;
            UrlPersonalizada = Guid.NewGuid().ToString();

            Validar();
        }

        public void Validar()
        {
            if(string.IsNullOrEmpty(Titulo))
                throw new ArgumentException("É necessário informar o titulo do artigo");

            if(Titulo.Length > 100)
                throw new ArgumentException("Titulo do artigo deve possuir até 100 caracteres");
        }

        public void ValidarParaEdicao()
        {
            if (!string.IsNullOrEmpty(Descricao) && Descricao.Length > 250)
                throw new ArgumentException("Descrição do artigo deve possuir até 250 caracteres");
        }

        public void Atualizar(string titulo, string descricao, string conteudo)
        {
            ValidarParaEdicao();

            if (!string.IsNullOrEmpty(titulo))
            {
                Validar();
                Titulo = titulo;
            }

            if (!string.IsNullOrEmpty(descricao))
                Descricao = descricao;

            Conteudo = conteudo;
            DataAtualizacao = DateTime.Now;
        }

        public void Atualizar(string urlPersonalizada)
        {
            if (string.IsNullOrEmpty(urlPersonalizada))
                throw new ArgumentException("É necessário informar a Url Personalizada");

            UrlPersonalizada = urlPersonalizada;
        }

        public void AplicarTema(Tema tema)
        {
            if (tema == null)
                throw new ArgumentException("Tema não definido");

            Tema = tema;
            DataAtualizacao = DateTime.Now;
        }

        public void AplicarCategoria(Categoria categoria)
        {
            if (categoria == null)
                throw new ArgumentException("Categoria não definida");

            if (!Tema.PertenceACategoria(categoria))
                throw new ArgumentException("Esta Categoria não pertence a este Tema");

            Categoria = categoria;
            DataAtualizacao = DateTime.Now;
        }

        public void Publicar()
        {
            if (EstaPublicado())
                throw new ArgumentException("Este Artigo já esta publicado");

            DataPublicacao = DateTime.Now;
            Estado = EstadoArtigo.Publicado;
        }

        public void Inativar()
        {
            if (EstaInativado())
                throw new ArgumentException("Este Artigo já esta inativado");

            if (EstaRemovido())
                throw new ArgumentException("Não é possível inativar um artigo que foi removido");

            if (!JaFoiPublicado())
                throw new ArgumentException("Somente artigos que ja foram publicados podem ser inativados");

            DataInativacao = DateTime.Now;
            Estado = EstadoArtigo.Inativo;
        }

        public void Remover()
        {
            if (EstaRemovido())
                throw new ArgumentException("Este Artigo já foi removido");

            if (JaFoiPublicado())
                throw new ArgumentException("Não é possível remover artigos que já foram publicados");

            DataRemocao = DateTime.Now;
            Estado = EstadoArtigo.Removido;
        }

        public void Impulsionar()
        {
            if (EstaImpulsionado())
                throw new ArgumentException("Este Artigo já esta impulsionado");

            if (!EstaPublicado())
                throw new ArgumentException("Não é possível impulsionar artigos que não estão publicados");

            DataImpulsionamento = DateTime.Now;
            Estado = EstadoArtigo.Impulsionado;
        }

        public bool EhRascunho() => Estado == EstadoArtigo.Rascunho;
        public bool EstaPublicado() => DataPublicacao.HasValue && Estado == EstadoArtigo.Publicado;
        public bool EstaImpulsionado() => DataImpulsionamento.HasValue && Estado == EstadoArtigo.Impulsionado;
        public bool EstaInativado() => DataInativacao.HasValue && Estado == EstadoArtigo.Inativo;
        public bool EstaRemovido() => DataRemocao.HasValue && Estado == EstadoArtigo.Removido;
        public bool JaFoiPublicado() => DataPublicacao.HasValue || DataImpulsionamento.HasValue;
    }
}