using FluentValidation.Validators;
using FluentValidation.Validators;
using LocadoraDeVeiculos.Dominio.Compartilhado.Validators;

namespace LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro
{
    public class NaoPodeDigitarNumerosValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "NaoPodeDigitarNumerosValidator";

        private string nomePropriedade;

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"'{nomePropriedade}' não pode conter apenas números.";
        }

        public override bool IsValid(ValidationContext<T> contextoValidacao, string texto)
        {
            nomePropriedade = contextoValidacao.DisplayName;

            if (string.IsNullOrEmpty(texto))
                return false;

            bool contemLetras = false;

            foreach (char letra in texto)
            {
                if (char.IsLetter(letra))
                {
                    contemLetras = true;
                    break;
                }
            }

            return contemLetras;
        }
    }
}