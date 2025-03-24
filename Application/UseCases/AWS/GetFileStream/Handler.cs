using System;
using Domain;
using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.AWS.GetFileStream;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly IArchiveRepository _archiveRepository;
    private readonly IAWSRepository _awsRepository;
    private readonly string _bucketName = Configuration.BucketImages;

    public Handler(IArchiveRepository archiveRepository, IAWSRepository awsRepository)
    {
        _archiveRepository = archiveRepository;
        _awsRepository = awsRepository;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var archive = await _archiveRepository.GetWithParametersAsync(x => x.Id.Equals(request.id), cancellationToken);
        if (archive is null)
            return new BaseResponse(404, "Arquivo não encontrado no banco de dados");

        var stream = await _awsRepository.GetFileAsync(_bucketName, archive.Path);
        return new BaseResponse(200, response: stream);
    }
}