using System;
using Domain.Records;
using MediatR;

namespace Application.UseCases.License.Validate;

public record Request(Guid CompanyId, string LicenseKey) : IRequest<BaseResponse>;
