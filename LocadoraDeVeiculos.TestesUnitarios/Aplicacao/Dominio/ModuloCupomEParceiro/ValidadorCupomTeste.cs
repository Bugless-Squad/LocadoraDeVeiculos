using FluentValidation.Results;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LocadoraDeVeiculos.TestesUnitarios.Aplicacao.Dominio.ModuloCupomEParceiro
{
    [TestClass]
    public class CupomTest
    {
        private Cupom cupom;
        private ValidadorCupom validador;

        public CupomTest()
        {
            cupom = new Cupom();
            validador = new ValidadorCupom();
        }

        [TestMethod]
        public void Nome_Do_Cupom_Deve_Ser_Obrigatorio()
        {
            // Arrange
            cupom.nome = null;

            // Action
            ValidationResult result = validador.Validate(cupom);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "nome"));
        }

        [TestMethod]
        public void Nome_Do_Cupom_Nao_Deve_Ser_Valido_Com_Caracteres_Especiais()
        {
            // Arrange
            cupom.nome = "#CupomTeste";

            // Action
            ValidationResult result = validador.Validate(cupom);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "nome"));
        }

        [TestMethod]
        public void Valor_Do_Cupom_Deve_Ser_Maior_Que_Zero()
        {
            // Arrange
            cupom.valor = 0;

            // Action
            ValidationResult result = validador.Validate(cupom);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "valor"));
        }

        [TestMethod]
        public void Validade_Do_Cupom_Deve_Ser_Futura()
        {
            // Arrange
            cupom.validade = DateTime.Now.AddDays(-1);

            // Action
            ValidationResult result = validador.Validate(cupom);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "validade"));
        }

        [TestMethod]
        public void Cupom_Deve_Ser_Valido_Com_Valores_Corretos()
        {
            // Arrange
            var parceiro = new Parceiro("Nome Parceiro");
            cupom.nome = "CupomTeste";
            cupom.valor = 50.0m;
            cupom.validade = DateTime.Now.AddDays(7);
            cupom.parceiro = parceiro;

            // Action
            ValidationResult result = validador.Validate(cupom);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Nome_Do_Cupom_Pode_Ter_Numero()
        {
            // Arrange
            var parceiro = new Parceiro("Nome Parceiro");
            cupom.nome = "Cupom123";
            cupom.valor = 50.0m;
            cupom.validade = DateTime.Now.AddDays(7);
            cupom.parceiro = parceiro;

            // Action
            ValidationResult result = validador.Validate(cupom);

            // Assert
            Assert.IsTrue(result.IsValid);
        }
    }
}
