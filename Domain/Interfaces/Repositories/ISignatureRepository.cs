
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ISignatureRepository 
{
    Task<Signature?> GetActiveByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken);
    Task CreateAsync(Signature signature, CancellationToken cancellationToken);
}
