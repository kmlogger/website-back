using Flunt.Validations;

namespace Domain.ValueObjects;

public class Price : BaseValueObject
{
    public decimal Value { get; private set; }
    public string Currency { get; private set; }
    protected Price() { }
    public Price(decimal value, string currency = "BRL")
    {
        Value = value;
        Currency = currency;

        AddNotifications(new Contract<Price>()
            .Requires()
            .IsGreaterThan(Value, 0, nameof(Value), "O preço deve ser maior que zero")
            .IsNotNullOrWhiteSpace(Currency, nameof(Currency), "A moeda é obrigatória")
            .IsLowerOrEqualsThan(Currency.Length, 5, nameof(Currency), "Código de moeda deve ter até 5 caracteres"));
    }

    public override string ToString() => $"{Value:N2} {Currency}";
}
