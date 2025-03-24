using System;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Domain;
using Domain.Extensions;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repositories;

public sealed class AWSRepository : IAWSRepository
{
    private readonly IAmazonS3 _awsS3Client;
    public AWSRepository()
    {
        _awsS3Client = new AmazonS3Client(new BasicAWSCredentials(Configuration.AwsKeyId, Configuration.AwsKeySecret),
            new AmazonS3Config { RegionEndpoint = RegionEndpoint.GetBySystemName(Configuration.AwsRegion) });
    }


    public async Task<string> UploadFileAsync(string bucketName, string key, IFormFile file)
    {
        using var newMemoryStream = new MemoryStream();
        await file.CopyToAsync(newMemoryStream);

        var fileTransferUtility = new TransferUtility(_awsS3Client);
        await fileTransferUtility.UploadAsync(new TransferUtilityUploadRequest
        {
            InputStream = newMemoryStream,
            Key = key,
            BucketName = bucketName,
            ContentType = file.ContentType
        });
        return key;
    }

    public async Task<bool> DeleteFileAsync(string bucket, string key)
    {
        var response = await _awsS3Client.DeleteObjectAsync(bucket, key);
        return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
    }

    public async Task<Stream> GetFileAsync(string bucketName, string key)
    {
        var request = new GetObjectRequest
        {
            BucketName = bucketName,
            Key = key
        };

        using var response = await _awsS3Client.GetObjectAsync(request);
        var memoryStream = new MemoryStream();
        await response.ResponseStream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }

    public async Task<string> GeneratePreSignedURLAsync(string bucketname, double duration, string objectKey)
    {
        var contentTypeEnum = objectKey.GetContentTypeFromPath();
        var contentType = contentTypeEnum.ToMimeType();
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucketname,
            Key = objectKey,
            Expires = DateTime.UtcNow.AddHours(duration),
            Verb = HttpVerb.GET,
            ResponseHeaderOverrides = new ResponseHeaderOverrides
            {
                ContentType = contentType
            }
        };
        return await _awsS3Client.GetPreSignedURLAsync(request);
    }
}
