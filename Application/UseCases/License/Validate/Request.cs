using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.License.Validate;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ISignatureRepository _signatureRepository;

    public Handler(ISignatureRepository signatureRepository)
    {
        _signatureRepository = signatureRepository;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var signature = await _signatureRepository.GetActiveByCompanyIdAsync(request.CompanyId, cancellationToken);

        if (signature is null || !signature.IsValid() || signature.LicenseKey != request.LicenseKey)
        {
            return new BaseResponse(401, "Licença inválida ou expirada");
        }

        return new BaseResponse(200, "Licença válida",null, new
        {
            signature.ExpireAt,
            Plan = signature.PlanName.ToString(),
            signature.MaxUsers
        });
    }
}