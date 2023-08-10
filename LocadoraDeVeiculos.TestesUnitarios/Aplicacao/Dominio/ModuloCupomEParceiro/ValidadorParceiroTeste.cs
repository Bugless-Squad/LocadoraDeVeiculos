using FluentValidation.TestHelper;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LocadoraDeVeiculos.TestesUnitarios.Aplicacao.Dominio.ModuloParceiro
{
    [TestClass]
    public class ParceiroTest
    {
        private Parceiro parceiro;
        private ValidadorParceiro validador;

        public ParceiroTest()
        {
            parceiro = new Parceiro();
            validador = new ValidadorParceiro();
        }

        [TestMethod]
        public void Nome_Do_Parceiro_É_Obrigatório()
        {
            // arrange
            parceiro.nome = null;

            // action
            var result = validador.TestValidate(parceiro);

            // assert
            result.ShouldHaveValidationErrorFor(p => p.nome);
        }

        [TestMethod]
        public void Nome_Do_Parceiro_Nao_Deve_Ser_Valido()
        {
            // arrange
            parceiro.nome = "$1234567";

            // action
            var result = validador.TestValidate(parceiro);

            // assert
            result.ShouldHaveValidationErrorFor(p => p.nome);
        }

        [TestMethod]
        public void Nome_Do_Parceiro_Nao_Deve_Possuir_Numeros()
        {
            // arrange
            parceiro.nome = "1234567";

            // action
            var result = validador.TestValidate(parceiro);

            // assert
            result.ShouldHaveValidationErrorFor(p => p.nome);
        }

        [TestMethod]
        public void Nome_Do_Parceiro_Deve_Ser_Maior_Que_Dois_Carateres()
        {
            //arrenge
            parceiro.nome = "Je";

            //action
            var outroResultado = validador.TestValidate(parceiro);

            //assert
            outroResultado.ShouldHaveValidationErrorFor(parceiro => parceiro.nome);
        }

        [TestMethod]
        public void Nome_Do_Parceiro_Nao_Deve_Possuir_Caracteres_Especiais()
        {
            // arrange
            parceiro.nome = "#NomeParceiro";

            // action
            var resultado = validador.TestValidate(parceiro);

            // assert
            resultado.ShouldHaveValidationErrorFor(p => p.nome);
        }

        [TestMethod]
        public void Nome_Do_Parceiro_Nao_Deve_Ser_Vazio()
        {
            // arrange
            parceiro.nome = string.Empty;

            // action
            var resultado = validador.TestValidate(parceiro);

            // assert
            resultado.ShouldHaveValidationErrorFor(p => p.nome);
        }

        [TestMethod]
        public void Nome_Do_Parceiro_Pode_Ter_Numero()
        {
            // arrange
            parceiro.nome = "NomeParceiro123";

            // action
            var resultado = validador.TestValidate(parceiro);

            // assert
            resultado.ShouldNotHaveValidationErrorFor(p => p.nome);
        }

    }
}
