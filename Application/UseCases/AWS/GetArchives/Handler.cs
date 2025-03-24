using System;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.AWS.GetArchives;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly IArchiveRepository _archiveRepository;

    public Handler(IArchiveRepository archiveRepository)
    {
        _archiveRepository = archiveRepository;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var archives = await _archiveRepository.GetAllProjectedAsync(
            selector: x => new { x.Title, x.Path },
            cancellationToken: cancellationToken);

        if(archives is null) return new BaseResponse(404, "Archives not found");
        return new BaseResponse(200, "Archives found", null, archives);
    }
}

