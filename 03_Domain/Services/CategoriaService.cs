using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Services
{
    public class CategoriaService : BaseService<Categoria>, ICategoriaService
    {
        public CategoriaService(IUnitOfWork unitOfWork, IRepository<Categoria> repository) 
            : base(unitOfWork, repository) { }
    }
}