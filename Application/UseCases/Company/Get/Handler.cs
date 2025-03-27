using System;
using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.Company.Get;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ICompanyRepository _companyRepository;

    public Handler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);
        if (company is null)
            return new BaseResponse(404, "Empresa não encontrada");

        return new BaseResponse(200, "Empresa encontrada",null, company);
    }
}