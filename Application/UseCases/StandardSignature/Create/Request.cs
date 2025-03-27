
using Domain.Records;
using MediatR;

namespace Application.UseCases.StandardSignature.Create;

public record  Request(
    Guid? CompanyId,
    string? PlanName,
    DateTime? StartDate, 
    DateTime? EndDate
) : IRequest<BaseResponse>;
