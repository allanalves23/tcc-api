using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IPerfilDeAcessoService : IBaseService<PerfilDeAcesso>
    {
        PerfilDeAcesso Obter(string usuarioId);
        PerfilDeAcesso Criar(Usuario usuario, string perfil);
    }
}