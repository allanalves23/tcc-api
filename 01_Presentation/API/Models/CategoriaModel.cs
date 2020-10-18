using Core.Entities;

namespace API.Models
{
    public class CategoriaModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TemaModel Tema { get; set; }

        public CategoriaModel() { }

        public CategoriaModel(Categoria categoria) 
        { 
            Id = categoria?.Id;
            Nome = categoria?.Nome;
            Descricao = categoria?.Descricao;
            Tema = categoria?.Tema == null ? new TemaModel(categoria.Tema) : null;
        }
    }
}