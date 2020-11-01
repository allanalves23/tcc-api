using Core.Entities;

namespace API.Models
{
    public class UsuarioModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public UsuarioModel() { }

        public UsuarioModel(Usuario usuario) 
        { 
            Id = usuario?.Id;
            UserName = usuario?.UserName;
            Email = usuario?.Email;
        }
    }
}