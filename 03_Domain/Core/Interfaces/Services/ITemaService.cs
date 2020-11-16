using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface ITemaService : IBaseService<Tema>
    { 
        IEnumerable<Tema> Obter(string termo, int? skip, int? take);
        int ObterQuantidade(string termo);
        Tema Obter(int? idTema, bool incluirRemovido = false);
        Tema Criar(string nome, string descricao);
        int Remover(int? idTema);
        void Atualizar(int? idTema, string nome, string descricao);
    }
}