namespace CodeHunt.Domain.Repositories
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
