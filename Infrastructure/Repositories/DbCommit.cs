using System;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Cold;
using Infrastructure.Data;
using Infrastructure.Data.Cold;


namespace Infrastructure.Repositories.Cold
{
    public class DbCommit(KMLoggerDbContex context) : IDbCommit
    {
        public async Task Commit(CancellationToken cancellationToken)
            => await context.SaveChangesAsync(cancellationToken);
    }
}
