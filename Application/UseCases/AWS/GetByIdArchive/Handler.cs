using System;
using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.AWS.GetByIdArchive;

public class Handler : IRequestHandler<Request, BaseResponse>
{   
    private readonly IArchiveRepository _archiveRepository;

    public Handler(IArchiveRepository archiveRepository)
    {
        _archiveRepository = archiveRepository;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var archive = await _archiveRepository.GetProjectedAsync(x => x.Id.Equals(request.id), 
        x => new { x.Id, x.Title, x.Path }
        ,cancellationToken);

        if(archive is null) return new BaseResponse(404, "Archive not found");
        return new BaseResponse(200, response: archive);
    }
}
