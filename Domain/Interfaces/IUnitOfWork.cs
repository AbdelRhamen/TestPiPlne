namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        Task SaveChangesAsync();
    }
}
