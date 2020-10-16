using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface ITemaService 
    { 
        IEnumerable<Tema> Obter(string termo, int? skip, int? take);
        Tema Obter(int? idTema);
        Tema Criar(string nome, string descricao);
        int Remover(int? idTema);
    }
}