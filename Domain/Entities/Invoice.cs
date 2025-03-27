using System;

namespace Domain.Entities;

public class Invoice : Entity
{
    public Guid PaymentId { get; private set; }
    public Payment Payment { get; private set; } = null!;
    public string InvoiceNumber { get; private set; } = null!;
    public DateTime IssuedDate { get; private set; }
    public Invoice(Guid paymentId, string invoiceNumber)
    {
        PaymentId = paymentId;
        InvoiceNumber = invoiceNumber;
        IssuedDate = DateTime.UtcNow;
    }
}