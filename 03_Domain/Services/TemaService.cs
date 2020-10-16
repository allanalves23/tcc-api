using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Services
{
    public class TemaService : BaseService<Tema>, ITemaService
    {
        public TemaService(IUnitOfWork unitOfWork, IRepository<Tema> repository) 
            : base(unitOfWork, repository) { }
    }
}