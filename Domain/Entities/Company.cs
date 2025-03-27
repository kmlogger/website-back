using Domain.ValueObjects;

namespace Domain.Entities;

public class Company : Entity
{
    public UniqueName Name { get; private set; }
    public string? Niche { get; private set; }
    public InternationalRegistry InternationalRegistry  { get; private set;}
    public Signature Signature { get; private set; } = null!;
    protected Company() {}

    public Company(UniqueName name, string? niche, InternationalRegistry internationalRegistry)
    {
        Name = name;
        Niche = niche;
        InternationalRegistry = internationalRegistry;
        AddNotificationsFromValueObjects(Name, InternationalRegistry);
    }

    public void SetSignature(Signature signature)
    {
        Signature = signature;
    }
}
