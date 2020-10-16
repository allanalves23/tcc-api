namespace Core.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}