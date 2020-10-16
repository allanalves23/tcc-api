using Core.Entities;

namespace API.Models
{
    public class TemaModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public TemaModel() { }
        public TemaModel(Tema tema) 
        {
            Id = tema?.Id;
            Nome = tema?.Nome;
            Descricao = tema?.Descricao;
        }
    }
}