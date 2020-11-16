using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface ICategoriaService 
    { 
        int ObterQuantidade(string termo);
        IEnumerable<Categoria> Obter(string termo, int? skip, int? take);
        IEnumerable<Categoria> Obter(string termo, int temaId, int? skip, int? take);
        Categoria Obter(int? idCategoria, bool incluirRemovido = false);
        Categoria Criar(string nome, string descricao, int? idTema);
        int Remover(int? idCategoria);
        void Atualizar(int? idCategoria, string nome, string descricao, int? idTema);
    }
}