namespace Core.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        void Commit();
        IRepository<T> GetRepository<T>() where T : class;
    }
}