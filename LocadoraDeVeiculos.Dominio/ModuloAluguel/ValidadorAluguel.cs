namespace LocadoraDeVeiculos.Dominio.ModuloAluguel
{
    public class ValidadorAluguel : AbstractValidator<Aluguel>, IValidadorAluguel
    {
        public ValidadorAluguel()
        {
            RuleFor(a => a.kmFinal)
                .GreaterThanOrEqualTo(a => a.kmInicial)
                .WithMessage("O valor do KM final deve ser maior ou igual ao KM inicial.");

            RuleFor(a => a.grupoAutomovel)
                .NotNull()
                .WithMessage("O grupo de automóvel é obrigatório.");

            RuleFor(a => a.valorTotalPrevisto)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor total previsto deve ser maior ou igual a zero.");

            RuleFor(a => a.nivelTanqueLitros)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O nível de tanque em litros deve ser maior ou igual a zero.");

            RuleFor(a => a.condutor)
            .NotNull()
            .WithMessage("O condutor é obrigatório.");
        }
    }
}