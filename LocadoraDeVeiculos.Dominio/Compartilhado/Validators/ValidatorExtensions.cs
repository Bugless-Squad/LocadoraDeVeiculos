using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;

namespace LocadoraDeVeiculos.Dominio.Compartilhado.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> NaoPodeCaracteresEspeciais<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NaoPodeCaracteresEspeciaisValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> NaoPodeDigitarNumeros<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NaoPodeDigitarNumerosValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> PodeApenasLetras<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new PodeApenasLetrasValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoEmailValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoCpfValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoCnh<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoCnhValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoCep<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoCepValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaSePreencheu<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new ClickBtnBuscaValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoCnpj<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoCnpjValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoTelefone<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoTelefoneValidator<T>());
        }

        public static IRuleBuilderOptions<T, decimal> SemValoresNegativosDecimal<T>(this IRuleBuilder<T, decimal> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NaoPodeValoresNegativosValidator1<T>());
        }

        public static IRuleBuilderOptions<T, double> SemValoresNegativosDouble<T>(this IRuleBuilder<T, double> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NaoPodeValoresNegativosValidator2<T>());
        }

        public static IRuleBuilderOptions<T, byte[]> VerificaTamanhoMax2MB<T>(this IRuleBuilder<T, byte[]> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NaoPodeMaiorQue2MB<T>());
        }
    }
}
