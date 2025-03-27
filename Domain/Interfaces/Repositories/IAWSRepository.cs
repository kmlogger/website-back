using System;

namespace Domain.Interfaces.Repositories;

public interface IAWSRepository
{
    Task<string> UploadFileAsync(string bucketName, string key, Stream file);
    Task<bool> DeleteFileAsync(string bucket, string key);
    Task<Stream> GetFileAsync(string bucketName, string key);
    Task<string> GeneratePreSignedURLAsync(string bucketname, double duration, string objectKey);
}

