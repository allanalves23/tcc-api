using System;
using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private IRepository<Artigo> _artigoRepository;
        private IRepository<Autor> _autorRepository;
        private IRepository<Tema> _temaRepository;
        private IRepository<Categoria> _categoriaRepository;
        public IRepository<Artigo> ArtigoRepository
        {
            get
            {
                if (_artigoRepository == null)
                    _artigoRepository = new ArtigoRepository(_context.Artigos, ConnectionString);
                return _artigoRepository;
            }
            set => _artigoRepository = value;
        }

        public IRepository<Autor> AutorRepository
        {
            get
            {
                if (_autorRepository == null)
                    _autorRepository = new AutorRepository(_context.Autores, ConnectionString);
                return _autorRepository;
            }
            set => _autorRepository = value;
        }

        public IRepository<Tema> TemaRepository
        {
            get
            {
                if (_temaRepository == null)
                    _temaRepository = new TemaRepository(_context.Temas, ConnectionString);
                return _temaRepository;
            }
            set => _temaRepository = value;
        }

        public IRepository<Categoria> CategoriaRepository
        {
            get
            {
                if (_categoriaRepository == null)
                    _categoriaRepository = new CategoriaRepository(_context.Categorias, ConnectionString);
                return _categoriaRepository;
            }
            set => _categoriaRepository = value;
        }
        
        private readonly BaseContext _context;
        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionString = _context.Database.GetDbConnection().ConnectionString;
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        public UnitOfWork(BaseContext context) => _context = context;

        public IRepository<T> GetRepository<T>() where T : class =>
            typeof(T) switch {
                Type tipo when tipo == typeof(Artigo) => (IRepository<T>)ArtigoRepository,
                Type tipo when tipo == typeof(Autor) => (IRepository<T>)AutorRepository,
                Type tipo when tipo == typeof(Tema) => (IRepository<T>)TemaRepository,
                Type tipo when tipo == typeof(Categoria) => (IRepository<T>)CategoriaRepository,
                _ => null
            };

        public void Commit() => _context.SaveChanges();

    }
}