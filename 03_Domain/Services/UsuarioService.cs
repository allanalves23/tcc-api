using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class UsuarioService : IUsuarioService
    {
        private UserManager<Usuario> _userManager;

        public UsuarioService(UserManager<Usuario> userManager) 
        {
            _userManager = userManager;
        }

        public async Task<Usuario> Obter(string id) => await _userManager.FindByIdAsync(id);

        public IEnumerable<Usuario> Obter(string termo, int skip, int take) =>
            _userManager
                .Users
                .Where(item => 
                    string.IsNullOrEmpty(termo) 
                    || (item.NormalizedEmail == termo.ToUpper())
                )
                .Skip(skip)
                .Take(take);
    }
}