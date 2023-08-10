using FluentValidation.TestHelper;
using LocadoraDeVeiculos.Dominio.ModuloTaxaEServico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloTaxaEServico
{
    [TestClass]
    public class TaxaEServicoTest
    {
        private TaxaEServico taxaEServico;
        private ValidadorTaxaEServico validador;

        [TestInitialize]
        public void Setup()
        {
            taxaEServico = new TaxaEServico();
            validador = new ValidadorTaxaEServico();
        }

        [TestMethod]
        public void Nome_Da_TaxaEServico_É_Obrigatório()
        {
            taxaEServico.nome = null;

            var resultado = validador.TestValidate(taxaEServico);

            resultado.ShouldHaveValidationErrorFor(t => t.nome);
        }

        [TestMethod]
        public void Nome_Da_TaxaEServico_Deve_Ter_Pelo_Menos_Tres_Caracteres()
        {
            taxaEServico.nome = "ab";

            var resultado = validador.TestValidate(taxaEServico);

            resultado.ShouldHaveValidationErrorFor(t => t.nome);
        }

        [TestMethod]
        public void Preco_Da_TaxaEServico_É_Obrigatório()
        {
            taxaEServico.preco = 0;

            var resultado = validador.TestValidate(taxaEServico);

            resultado.ShouldHaveValidationErrorFor(t => t.preco);
        }

        [TestMethod]
        public void Preco_Da_TaxaEServico_Não_Pode_Ser_Negativo()
        {
            taxaEServico.preco = -50.00m;

            var resultado = validador.TestValidate(taxaEServico);

            resultado.ShouldHaveValidationErrorFor(t => t.preco);
        }

    }
}
