using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IAutorService { 
        Autor Obter(int? idAutor);
        Autor Obter(string idUsuario);
        Autor Criar(string idUsuario);
    }
}