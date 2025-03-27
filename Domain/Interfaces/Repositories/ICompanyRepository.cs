using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Company?> GetCompanyByName(string name, CancellationToken cancellationToken);
    Task CreateAsync(Company company, CancellationToken cancellationToken);
    Task UpdateAsync(Company company, CancellationToken cancellationToken);
}
