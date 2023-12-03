using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CostumerRegistration.Services.Validation
{
    public class CnpjValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid([DisallowNull] object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var cnpj = value.ToString();

                // Remover caracteres não numéricos do CNPJ
                cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

                // Verificar se o CNPJ tem 14 dígitos
                if (cnpj.Length != 14)
                {
                    return new ValidationResult("CNPJ deve conter 14 dígitos.");
                }

                // Calcular os dígitos verificadores
                int[] multiplicadoresPrimeiroDigito = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicadoresSegundoDigito = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma = 0;

                for (int i = 0; i < 12; i++)
                {
                    soma += int.Parse(cnpj[i].ToString()) * multiplicadoresPrimeiroDigito[i];
                }

                int resto = soma % 11;
                int primeiroDigito = resto < 2 ? 0 : 11 - resto;

                soma = 0;
                for (int i = 0; i < 13; i++)
                {
                    soma += int.Parse(cnpj[i].ToString()) * multiplicadoresSegundoDigito[i];
                }

                resto = soma % 11;
                int segundoDigito = resto < 2 ? 0 : 11 - resto;

                // Verificar se os dígitos verificadores estão corretos
                if (int.Parse(cnpj[12].ToString()) != primeiroDigito || int.Parse(cnpj[13].ToString()) != segundoDigito)
                {
                    return new ValidationResult("CNPJ inválido.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
