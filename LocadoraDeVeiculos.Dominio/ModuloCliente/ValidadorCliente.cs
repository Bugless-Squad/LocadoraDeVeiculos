using LocadoraDeVeiculos.Dominio.Compartilhado.Validators;

namespace LocadoraDeVeiculos.Dominio.ModuloCliente
{
    public class ValidadorCliente : AbstractValidator<Cliente>, IValidadorCliente
    {
        public ValidadorCliente()
        {
            RuleFor(x => x.nome)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .NaoPodeDigitarNumeros()
                .NaoPodeCaracteresEspeciais();

            RuleFor(x => x.email)
                .NotEmpty()
                .NotNull()
                .VerificaFormatoEmail();

            RuleFor(x => x.telefone)
                .VerificaFormatoTelefone();

            RuleFor(x => x.tipoPessoa)
                .NotEmpty()
                .NotNull();

            When(x => x.tipoPessoa == "Pessoa Física", () =>
            {
                RuleFor(x => x.cpf)
                    .NotEmpty()
                    .NotNull()
                    .VerificaFormatoCpf();

                RuleFor(x => x.cnpj)
                    .Empty();
            });

            When(x => x.tipoPessoa == "Pessoa Jurídica", () =>
            {
                RuleFor(x => x.cnpj)
                    .NotEmpty()
                    .NotNull()
                    .VerificaFormatoCnpj();

                RuleFor(x => x.cpf)
                    .Empty();
            });

            RuleFor(x => x.cep)
                .NotEmpty()
                .NotNull()
                .VerificaFormatoCep();

            RuleFor(x => x.estado)
                .NotEmpty()
                .NaoPodeCaracteresEspeciais()
                .VerificaSePreencheu();

            RuleFor(x => x.cidade)
                .NotEmpty()
                .NotNull()
                .NaoPodeCaracteresEspeciais();

            RuleFor(x => x.bairro)
                .NotEmpty()
                .NotNull()
                .NaoPodeCaracteresEspeciais();

            RuleFor(x => x.rua)
                .NotEmpty()
                .NotNull()
                .NaoPodeCaracteresEspeciais();

            RuleFor(x => x.numero)
                .NotEmpty()
                .NotNull();
        }
    }
}
