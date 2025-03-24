using System;

namespace Domain.Entities;

public class Archive : Entity
{
    public string? Title { get; set; }
    public string Path { get; set; }

    private Archive() { }

    public Archive(string title, string path)
    {
        Title = title;
        Path = path;
    }
}
