
using Domain.Records;
using MediatR;

namespace Application.UseCases.AWS.GetByIdArchive;

public record Request(
    Guid id
) : IRequest<BaseResponse>;
