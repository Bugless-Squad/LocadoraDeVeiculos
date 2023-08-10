using FluentValidation.TestHelper;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio
{
    [TestClass]
    public class PlanosCobrancaTest
    {
        private PlanoCobranca planoCobranca;
        private ValidadorPlanoCobranca validador;

        public PlanosCobrancaTest()
        {
            planoCobranca = new PlanoCobranca()
            {
                grupoAutomovel = new GrupoAutomovel(),
                tipoPlano = new TipoPlanoEnum(),
                precoDiaria = 50.00m,
                precoPorKm = 100.00m,
                precoPorKmExtrapolado = 200.00m,
                kmDisponiveis = 5.00m
            };

            validador = new ValidadorPlanoCobranca();
        }

        [TestMethod]
        public void Grupo_É_Obrigatório()
        {
            // Arrange
            planoCobranca.grupoAutomovel = null;

            // Act
            var resultado = validador.TestValidate(planoCobranca);

            // Assert
            resultado.ShouldHaveValidationErrorFor(plano => plano.grupoAutomovel);
        }

        [TestMethod]
        public void PrecoPorKm_Pode_Ser_Nulo()
        {
            planoCobranca.precoPorKm = null;

            var resultado = validador.TestValidate(planoCobranca);

            resultado.ShouldNotHaveValidationErrorFor(plano => plano.precoPorKm);
        }

        [TestMethod]
        public void PrecoDiaria_Deve_Ser_Maior_Que_Zero_Com_Valor_Minimo()
        {
            planoCobranca.precoDiaria = 0.01m; 

            var resultado = validador.TestValidate(planoCobranca);

            resultado.ShouldNotHaveValidationErrorFor(plano => plano.precoDiaria);
        }

        [TestMethod]
        public void PrecoDiaria_Deve_Ser_Valido()
        {
            planoCobranca.precoDiaria = 100; // Definir um valor válido

            var resultado = validador.TestValidate(planoCobranca);

            resultado.ShouldNotHaveValidationErrorFor(plano => plano.precoDiaria);
        }

        [TestMethod]
        public void PrecoPorKmExtrapolado_Deve_Ser_Valido()
        {
            planoCobranca.precoPorKmExtrapolado = 150; // Definir um valor válido

            var resultado = validador.TestValidate(planoCobranca);

            resultado.ShouldNotHaveValidationErrorFor(plano => plano.precoPorKmExtrapolado);
        }

        [TestMethod]
        public void PrecoPorKmExtrapolado_Deve_Ser_Maior_Que_Zero_Com_Valor_Minimo()
        {
            planoCobranca.precoPorKmExtrapolado = 0.01m; // Definir o valor mínimo permitido

            var resultado = validador.TestValidate(planoCobranca);

            resultado.ShouldNotHaveValidationErrorFor(plano => plano.precoPorKmExtrapolado);
        }

        [TestMethod]
        public void PrecoPorKmExtrapolado_Deve_Ser_Obrigatorio_Com_TipoPlano_Cobranca_Controlada()
        {
            planoCobranca.tipoPlano = TipoPlanoEnum.Cobrança_Controlada;
            planoCobranca.precoPorKmExtrapolado = null;

            var resultado = validador.TestValidate(planoCobranca);

            resultado.ShouldHaveValidationErrorFor(plano => plano.precoPorKmExtrapolado);
        }

        [TestMethod]
        public void KmDisponiveis_Deve_Ser_Valido()
        {
            planoCobranca.kmDisponiveis = 100; // Definir um valor válido

            var resultado = validador.TestValidate(planoCobranca);

            resultado.ShouldNotHaveValidationErrorFor(plano => plano.kmDisponiveis);
        }

        [TestMethod]
        public void KmDisponiveis_Deve_Ser_Maior_Que_Zero_Com_Valor_Minimo()
        {
            planoCobranca.kmDisponiveis = 0.01m; // Definir o valor mínimo permitido

            var resultado = validador.TestValidate(planoCobranca);

            resultado.ShouldNotHaveValidationErrorFor(plano => plano.kmDisponiveis);
        }

        [TestMethod]
        public void KmDisponiveis_Deve_Ser_Obrigatorio_Com_TipoPlano_Cobranca_Controlada()
        {
            planoCobranca.tipoPlano = TipoPlanoEnum.Cobrança_Controlada;
            planoCobranca.kmDisponiveis = null;

            var resultado = validador.TestValidate(planoCobranca);

            resultado.ShouldHaveValidationErrorFor(plano => plano.kmDisponiveis);
        }
    }

}
