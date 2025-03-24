using System;

namespace Domain.Entities;

public sealed record AWSSecret
{
    public string?  AwsKeyId { get; set; }
    public string?  AwsKeySecret { get; set; }

    public AWSSecret(string keyid , string keysecret)
    {
        AwsKeyId = keyid;
        AwsKeySecret = keysecret;
    }
}
