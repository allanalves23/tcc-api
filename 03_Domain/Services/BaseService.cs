using System;
using System.Collections.Generic;
using Core.Interfaces.Services;
using Core.Interfaces.Repository;

namespace Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {

        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void Adicionar(T entity) => _repository.Add(entity);

        public void Atualizar(T entity) => _repository.Update(entity);

        public void Excluir(Func<T, bool> criteria) => _repository.Delete(Obter(criteria));

        public T Obter(Func<T, bool> criteria) => _repository.FirstOrDefault(criteria);

        public IEnumerable<T> Obter(Func<T, bool> criteria, int? skip = 0, int? take = 10) => _repository.Get<T>(criteria, skip, take);

        public IEnumerable<T> Obter<TReturn>(Func<T, bool> criteria, Func<T, TReturn> sortCriteria, int? skip = 0, int? take = 10) => _repository.Get(criteria, skip, take, sortCriteria);

        public void Salvar() => _unitOfWork.Commit();

    }
}