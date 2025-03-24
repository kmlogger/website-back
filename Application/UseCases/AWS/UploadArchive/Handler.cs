using System;
using MediatR;

namespace Application.UseCases.AWS.UploadArchive;

public class UploadArchiveHandler : IRequestHandler<UploadArchiveRequest, UploadArchiveResponse>
{
    private readonly IArchiveRepository _archiveRepository;
    private readonly IAwsRepository _awsRepository;
    private readonly IMapper _mapper;
    private readonly IDbCommit _dbCommit;

    public UploadArchiveHandler(IArchiveRepository archiveRepository, IAwsRepository awsRepository, IMapper mapper, IDbCommit dbCommit)
    {
        _archiveRepository = archiveRepository;
        _awsRepository = awsRepository;
        _mapper = mapper;
        _dbCommit = dbCommit;
    }

    public async Task<UploadArchiveResponse> Handle(UploadArchiveRequest request, CancellationToken cancellationToken)
    {
        // Gerar uma chave única para o arquivo
        request.Key = $"/images/{Guid.NewGuid()}_{request.Key}";

        // Upload do arquivo para o S3
        var fileUrl = await _awsRepository.UploadFileAsync(Configuration.BucketImages, request.Key, request.File);
        if (string.IsNullOrEmpty(fileUrl))
            return new UploadArchiveResponse { Success = false, Message = "Upload falhou." };

        // Mapeia o UploadArchiveRequest para a entidade Archive
        var archive = _mapper.Map<Archive>(request);
        archive.Path = fileUrl;  // Define a URL do arquivo como o caminho do arquivo

        // Adiciona o arquivo no banco de dados
        await _archiveRepository.AddArchiveAsync(archive);
        await _dbCommit.Commit(cancellationToken);

        // Retorna o resultado
        return new UploadArchiveResponse
        {
            Success = true,
            FileUrl = fileUrl,
            Message = "Upload bem-sucedido."
        };
    }
}