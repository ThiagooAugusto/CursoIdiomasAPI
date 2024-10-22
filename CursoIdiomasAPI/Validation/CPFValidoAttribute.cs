using System.ComponentModel.DataAnnotations;

namespace CursoIdiomasAPI.Validation
{
    public class CPFValidoAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("O CPF é obrigatório.");
            }

            var cpf = value.ToString().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                return new ValidationResult("CPF inválido.");
            }

            if (!ValidarCpf(cpf))
            {
                return new ValidationResult("CPF inválido.");
            }

            return ValidationResult.Success;
        }

        private bool ValidarCpf(string cpf)
        {
            // Verifica se todos os dígitos são iguais (caso típico de CPFs inválidos como 111.111.111-11)
            if (cpf.All(c => c == cpf[0])) return false;

            // Calcula o primeiro dígito verificador
            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += (cpf[i] - '0') * (10 - i);

            var resultado = soma % 11;
            var digito1 = resultado < 2 ? 0 : 11 - resultado;

            // Calcula o segundo dígito verificador
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * (11 - i);

            resultado = soma % 11;
            var digito2 = resultado < 2 ? 0 : 11 - resultado;

            // Verifica se os dígitos calculados são iguais aos do CPF informado
            return cpf[9] - '0' == digito1 && cpf[10] - '0' == digito2;
        }
    }
}
