using Domain.Records;
using MediatR;

namespace Application.UseCases.AWS.GetArchives;

public record Request : IRequest<BaseResponse>;