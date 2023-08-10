using System;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloTaxaEServico;
using FluentValidation.TestHelper;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloAluguel
{
    [TestClass]
    public class AluguelTest
    {
        private Aluguel aluguel;
        private ValidadorAluguel validadorAluguel;

        public AluguelTest()
        {
            aluguel = new Aluguel
            {

            };

            validadorAluguel = new ValidadorAluguel();
        }

        [TestMethod]
        public void Deve_validar_aluguel_com_propriedades_corretas()
        {
            ValidationResult result = validadorAluguel.Validate(aluguel);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Deve_invalidar_aluguel_com_valor_total_previsto_negativo()
        {
            aluguel.valorTotalPrevisto = -200;

            var result = validadorAluguel.TestValidate(aluguel);

            result.ShouldHaveValidationErrorFor(a => a.valorTotalPrevisto);
        }

        [TestMethod]
        public void Deve_invalidar_aluguel_com_nivel_tanque_litros_negativo()
        {
            aluguel.nivelTanqueLitros = -20;

            var result = validadorAluguel.TestValidate(aluguel);

            result.ShouldHaveValidationErrorFor(a => a.nivelTanqueLitros);
        }

        public void Deve_invalidar_aluguel_com_condutor_nulo()
        {
            aluguel.condutor = null;

            var result = validadorAluguel.TestValidate(aluguel);

            result.ShouldHaveValidationErrorFor(a => a.condutor);
        }


    }
}
