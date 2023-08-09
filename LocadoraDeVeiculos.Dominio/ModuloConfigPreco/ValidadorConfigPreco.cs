using LocadoraDeVeiculos.Dominio.ModuloConfigPreco;

namespace LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel
{
    public class ValidadorConfiguracaoPreco : AbstractValidator<ConfiguracaoPreco>, IValidadorConfiguracaoPreco
    {
        public ValidadorConfiguracaoPreco()
        {
            RuleFor(x => x.precoGasolina)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.precoAlcool)
               .NotEmpty()
               .NotNull();

            RuleFor(x => x.precoDisel)
               .NotEmpty()
               .NotNull();

            RuleFor(x => x.precoGas)
               .NotEmpty()
               .NotNull();
        }
    }
}