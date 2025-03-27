using Domain.Records;
using MediatR;

namespace Application.UseCases.Company.Get;

public record Request(Guid CompanyId) : IRequest<BaseResponse>;
