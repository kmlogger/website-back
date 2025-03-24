using System;
using Domain.Enums;

namespace Domain.Extensions;

public static class ContentTypeExtension
{
    public static string? ToMimeType(this ContentType? contentType)
        => contentType switch
        {
            ContentType.ImageJpeg => "image/jpeg",
            ContentType.ImagePng => "image/png",
            ContentType.ImageGif => "image/gif",
            ContentType.ImageBmp => "image/bmp",
            ContentType.VideoMp4 => "video/mp4",
            _ => "video/mp4"
        };
}
