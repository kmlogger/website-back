using Domain.ValueObjects;

namespace Domain.Entities;
    
public class StandardSignature : Signature
{
    public StandardSignature(Guid companyId, UniqueName planName, DateTime startDate, 
        int maxUsers, Price price)
    {
        CompanyId = companyId;
        PlanName = planName;
        StartDate = startDate;
        EndDate = startDate.AddMonths(1);
        MaxUsers = maxUsers;
        Price = price;
        Active = true;
        AddNotifications(price.Notifications); 
    }
    public void Renew(TimeSpan duration)
    {
        if (!Active || IsExpired())
        {
            AddNotification("Active", "Signature is not active or has expired");
        }
        StartDate = DateTime.UtcNow;
        EndDate = DateTime.UtcNow.Add(duration);
    }
}
