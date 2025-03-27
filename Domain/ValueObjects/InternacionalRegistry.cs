using DocumentValidator;
using Flunt.Validations;

namespace Domain.ValueObjects;

public class InternationalRegistry : BaseValueObject
{
    public string Number { get; private set; }
    public InternationalRegistry(string number)
    {
        Number = number?.Trim() ?? string.Empty;
        AddNotifications(new Contract<InternationalRegistry>()
            .Requires()
            .IsNotNullOrWhiteSpace(Number, nameof(Number), "O número de registro é obrigatório")
            .IsLowerOrEqualsThan(Number.Length, 25, nameof(Number), "O número de registro deve ter no máximo 25 caracteres"));

        if (IsPossibleCnpjorCpf(Number) && !IsValidCnpjORCpf(Number))
        {
            AddNotification(nameof(Number), "CNPJ ou CPF inválido");
        }
    }
    public bool IsCnpj => Number.Length == 14 && Number.All(char.IsDigit);
    private bool IsPossibleCnpjorCpf(string value) =>
        (value.Length == 14 || value.Length == 12) && value.All(char.IsDigit);

    private bool IsValidCnpjORCpf(string cpfOrcnpj)
        => CnpjValidation.Validate(cpfOrcnpj) || CpfValidation.Validate(cpfOrcnpj);

    public override string ToString() => Number;
}
