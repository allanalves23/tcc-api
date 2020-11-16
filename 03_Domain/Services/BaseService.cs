using System;
using System.Collections.Generic;
using Core.Interfaces.Services;
using Core.Interfaces.Repository;

namespace Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Adicionar(T entity) => _unitOfWork.GetRepository<T>().Add(entity);

        public void Atualizar(T entity) => _unitOfWork.GetRepository<T>().Update(entity);

        public void Excluir(Func<T, bool> criteria) => _unitOfWork.GetRepository<T>().Delete(Obter(criteria));

        public T Obter(Func<T, bool> criteria) => _unitOfWork.GetRepository<T>().FirstOrDefault(criteria);

        public IEnumerable<T> Obter(Func<T, bool> criteria, int? skip = 0, int? take = 10) => _unitOfWork.GetRepository<T>().Get<T>(criteria, skip, take);

        public IEnumerable<T> Obter<TReturn>(Func<T, bool> criteria, Func<T, TReturn> sortCriteria, int? skip = 0, int? take = 10) => _unitOfWork.GetRepository<T>().Get(criteria, skip, take, sortCriteria);

        public bool Existe(Func<T, bool> criteria) => _unitOfWork.GetRepository<T>().Exists(criteria);

        public void Salvar() => _unitOfWork.Commit();

        public int Contar(Func<T, bool> criteria) => _unitOfWork.GetRepository<T>().Count(criteria);
    }
}