using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IAutorService 
    { 
        Autor Obter(int? idAutor);
        Autor Obter(string idUsuario, bool lancaExcecao = false);
        Autor Criar(string idUsuario);
        Autor Atualizar(int? id, string nome, string email, string genero);
        bool AutorEhAdmin(string usuarioId);
    }
}