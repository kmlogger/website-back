using System;
using Domain.Interfaces.Repositories;
using Domain.Records;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Company.Create;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IDbCommit _dbCommit;

    public Handler(ICompanyRepository companyRepository, IDbCommit dbCommit)
    {
        _companyRepository = companyRepository;
        _dbCommit = dbCommit;
    }
    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        if(await _companyRepository.GetCompanyByName(request.name, cancellationToken) is not null
            ) return new BaseResponse(400, "Company already exists");

        var company = new Domain.Entities.Company(
            new UniqueName(request.name), 
            request.Niche,
            new InternationalRegistry(request.InternationalRegistry)
        );

        if(company.Notifications.Any()) 
            return new BaseResponse(400, "Erros ao criar Company", company.Notifications.ToList());     

        await _companyRepository.CreateAsync(company, cancellationToken);
        await _dbCommit.Commit(cancellationToken);    
        return new BaseResponse(201, "Company Created Succesfully");
    }
}

