
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public abstract class Signature : Entity
{
    public Guid CompanyId { get; protected set; }
    public Company Company { get; protected set; } = null!;
    public UniqueName PlanName { get; protected set; }
    public DateTime StartDate { get; protected set; }
    public DateTime? EndDate { get; protected set; }
    public bool Active { get; protected set; }
    public int MaxUsers { get; protected set; }
    public Price Price { get; protected set; } = null!;
    public string LicenseKey { get; protected set; } = null!;
    public DateTime? ExpireAt { get; protected set; }

    protected Signature() {}

    public Signature(Guid companyId, UniqueName planName, DateTime startDate, int maxUsers, Price price)
    {
        CompanyId = companyId;
        PlanName = planName;
        StartDate = startDate;
        MaxUsers = maxUsers;
        Price = price;
        Active = true;
        LicenseKey = GenerateLicenseKey(); 
    }

    public virtual void Cancel(DateTime cancelDate)
    {
        EndDate = cancelDate;
        Active = false;
    }

    public bool IsExpired() => ExpireAt.HasValue && ExpireAt.Value < DateTime.UtcNow;
    public bool IsValid() => Active && !IsExpired() && !string.IsNullOrEmpty(LicenseKey);

    protected string GenerateLicenseKey()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                     .Replace("=", "")
                     .Replace("+", "")
                     .Replace("/", "")
                     .Substring(0, 25); 
    }

    public void ActivateFromPayment(Payment payment)
    {
        if (payment.Status != PaymentStatus.Completed)
            throw new InvalidOperationException("Pagamento não concluído.");

        StartDate = payment.PaymentDate;
        ExpireAt = payment.PaymentDate.AddMonths(1); 
        Active = true;
    }
}
