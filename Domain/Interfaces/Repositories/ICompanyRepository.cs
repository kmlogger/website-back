using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task CreateAsync(Company company, CancellationToken cancellationToken);

    Task<bool> GetCompanyByName(string name, CancellationToken cancellationToken);
}
