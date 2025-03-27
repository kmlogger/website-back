
using Domain.Records;
using MediatR;

namespace Application.UseCases.StandardSignature.Create;

public record Request(Guid CompanyId, int MaxUsers, decimal PriceValue, string Currency)
    : IRequest<BaseResponse>;