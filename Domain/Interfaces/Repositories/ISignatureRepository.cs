
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ISignatureRepository 
{
    Task CreateAsync(Signature signature, CancellationToken cancellationToken);
}
