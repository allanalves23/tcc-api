using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> Obter(string id);
        IEnumerable<Usuario> Obter(string termo, int skip, int take);
    }
}