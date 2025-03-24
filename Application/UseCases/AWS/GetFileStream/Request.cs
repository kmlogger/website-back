
using Domain.Records;
using MediatR;

namespace Application.UseCases.AWS.GetFileStream;

public record  Request(Guid id) : IRequest<BaseResponse>;
