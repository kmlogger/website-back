
using Domain.Records;
using MediatR;

namespace Application.UseCases.Company.Create;

public record Request(
    string? name, 
    string? Niche, 
    string? InternationalRegistry
) : IRequest<BaseResponse>;