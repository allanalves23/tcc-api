using System;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IBaseService<T> where T : class
    {
        void Adicionar(T entity);
        void Atualizar(T entity);
        T Obter(Func<T, bool> criteria);
        IEnumerable<T> Obter(Func<T, bool> criteria, int? skip = 0, int? take = 10);
        IEnumerable<T> Obter<TReturn>(Func<T, bool> criteria, Func<T, TReturn> sortCriteria, int? skip = 0, int? take = 10);
        void Excluir(Func<T, bool> criteria);
        bool Existe(Func<T, bool> criteria);
        void Salvar();
    }
}