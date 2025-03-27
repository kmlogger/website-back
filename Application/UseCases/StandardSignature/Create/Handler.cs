using System;
using Domain.Interfaces.Repositories;
using Domain.Records;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.StandardSignature.Create;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ISignatureRepository _signatureRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IDbCommit _dbCommit;

    public Handler(ISignatureRepository signatureRepository, ICompanyRepository companyRepository, IDbCommit dbCommit)
    {
        _signatureRepository = signatureRepository;
        _companyRepository = companyRepository;
        _dbCommit = dbCommit;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);
        if (company is null)
            return new BaseResponse(404, "Empresa não encontrada");

        var signature = new Domain.Entities.StandardSignature(
            request.CompanyId,
            new UniqueName("Standard"),
            DateTime.UtcNow,
            request.MaxUsers,
            new Price(request.PriceValue, request.Currency)
        );

        if (signature.Notifications.Any())
            return new BaseResponse(400, "Erro ao criar assinatura", signature.Notifications.ToList());

        company.SetSignature(signature);

        await _signatureRepository.CreateAsync(signature, cancellationToken);
        await _companyRepository.UpdateAsync(company, cancellationToken);
        await _dbCommit.Commit(cancellationToken);

        return new BaseResponse(201, "Assinatura criada com sucesso", null, signature.LicenseKey);
    }
}
