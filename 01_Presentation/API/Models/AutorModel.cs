using Core.Entities;

namespace API.Models
{
    public class AutorModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public string UsuarioId { get; set; }

        public AutorModel() { }

        public AutorModel(Autor autor) 
        {
            Id = autor?.Id;
            Nome = autor?.Nome;
            Genero = autor?.Genero.ToString();
            Email = autor?.Email;
            UsuarioId = autor?.UsuarioId;
        }
    }
}