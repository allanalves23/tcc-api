using Core.Entities;

namespace API.Models
{
    public class AutorModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public string UsuarioId { get; set; }

        public AutorModel() { }

        public AutorModel(Autor autor) 
        { 
            Nome = autor?.Nome;
            Genero = autor?.Genero.ToString().ToUpper();
            Email = autor?.Email;
            UsuarioId = autor?.UsuarioId;
        }
    }
}