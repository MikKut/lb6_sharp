using lb6_server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lb6_server.Services;

public abstract class BaseDataService<T>
    where T : DbContext
{
    private readonly IDbContextWrapper<T> _dbContextWrapper;

    protected BaseDataService(IDbContextWrapper<T> dbContextWrapper)
    {
        _dbContextWrapper = dbContextWrapper;
    }

    protected Task ExecuteSafeAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        return ExecuteSafeAsync(token => action(), cancellationToken);
    }

    protected Task<TResult> ExecuteSafeAsync<TResult>(Func<Task<TResult>> action, CancellationToken cancellationToken = default)
    {
        return ExecuteSafeAsync(token => action(), cancellationToken);
    }

    private async Task ExecuteSafeAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default)
    {
        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

        try
        {
            await action(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }

    private async Task<TResult> ExecuteSafeAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken cancellationToken = default)
    {
        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

        try
        {
            TResult? result = await action(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
        }

        return default!;
    }
}