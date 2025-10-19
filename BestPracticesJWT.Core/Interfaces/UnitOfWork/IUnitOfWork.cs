namespace BestPracticesJWT.Core.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync();
    void Commit();
}
