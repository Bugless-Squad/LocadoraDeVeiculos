using LocadoraDeVeiculos.Dominio.Compartilhado.Validators;

namespace LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro
{
    public class ValidadorCupom : AbstractValidator <Cupom>, IValidadorCupom
    {
        public ValidadorCupom() 
        {
            RuleFor(x => x.nome)
                .NotEmpty()
                .NotNull()
                .NaoPodeCaracteresEspeciais()
                .MinimumLength(3);

            RuleFor(x => x.validade)
               .NotEmpty()
               .NotNull()
               .GreaterThan(DateTime.Today);

            RuleFor(x => x.valor)
              .NotEmpty()
              .NotNull();
        }
    }
}