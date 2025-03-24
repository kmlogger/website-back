using System;
using Domain.Enums;

namespace Domain.Extensions;

public  static class StringExtension
{
    public static ContentType? GetContentTypeFromPath(this string filePath)
    {
        if (filePath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || filePath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
        {
            return ContentType.ImageJpeg;
        }
        else if (filePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
        {
            return ContentType.ImagePng;
        }
        else if (filePath.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
        {
            return ContentType.ImageGif;
        }
        else if (filePath.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
        {
            return ContentType.ImageBmp;
        }
        else if (filePath.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
        {
            return ContentType.VideoMp4;
        }
        else
        {
            return ContentType.VideoMp4;
        }
    }
}
