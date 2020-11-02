using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<(Usuario, PerfilDeAcesso)> ObterAsync(string id);
        IEnumerable<(Usuario, PerfilDeAcesso)> Obter(string termo, int skip, int take);
        void DefinirPerfilDeAcesso(Usuario usuario, string perfil);
        (Usuario, PerfilDeAcesso) Criar(string email, string senha, string perfil);
        Task<(Usuario, PerfilDeAcesso)> AtualizarAsync(string id, string email, string perfil);
        Task AlterarSenhaAsync(string id, string senhaAtual, string novaSenha, string confirmacaoNovaSenha);
        void Remover(string id);
        void Reativar(string id);
    }
}