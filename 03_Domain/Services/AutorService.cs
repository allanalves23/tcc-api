using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Services
{
    public class AutorService : BaseService<Autor>, IAutorService
    {
        public AutorService(IUnitOfWork unitOfWork) 
            : base(unitOfWork) { }
    }
}