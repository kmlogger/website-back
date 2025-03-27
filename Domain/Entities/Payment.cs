using System;
using Domain.Enums;

namespace Domain.Entities;

public class Payment : Entity
{
    public Guid SignatureId { get; private set; }
    public Signature Signature { get; private set; } = null!;

    public DateTime PaymentDate { get; private set; }
    public decimal Amount { get; private set; }
    public string TransactionId { get; private set; } = null!;
    public PaymentStatus Status { get; private set; }
    public PaymentMethod Method { get; private set; }

    protected Payment() {}

    public Payment(Guid signatureId, decimal amount, string transactionId, PaymentStatus status, PaymentMethod method)
    {
        SignatureId = signatureId;
        Amount = amount;
        TransactionId = transactionId;
        Status = status;
        Method = method;
        PaymentDate = DateTime.UtcNow;
    }
}
