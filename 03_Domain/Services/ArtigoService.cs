using Core.Entities;
using Core.Interfaces.Services;
using Core.Interfaces.Repository;

namespace Services
{
    public class ArtigoService : BaseService<Artigo>, IArtigoService
    {
        public ArtigoService(IUnitOfWork unitOfWork) 
            : base(unitOfWork) { }
    }
}