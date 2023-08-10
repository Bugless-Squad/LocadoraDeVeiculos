using FluentValidation.TestHelper;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloCondutor
{
    [TestClass]
    public class CondutorTest
    {
        private Condutor condutor;
        private ValidadorCondutor validador;

        public CondutorTest()
        {
            condutor = new Condutor();

            condutor.nome = "Eduardo";
            condutor.telefone = "(49) 98800-3240";
            condutor.cpf = "013.499.589-99";
            condutor.email = "teste@gmail.com";
            condutor.cnh = "1234567891";
            condutor.validadeCnh = DateTime.Now.AddDays(20);

            validador = new ValidadorCondutor();
        }

        [TestMethod]
        public void Cliente_Do_Condutor_É_Obrigatório()
        {
            condutor.cliente = null;

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.cliente);
        }

        [TestMethod]
        public void Nome_Do_Condutor_Deve_Ter_Pelo_Menos_Tres_Caracteres()
        {
            condutor.nome = "ab";

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.nome);
        }

        [TestMethod]
        public void Email_Do_Condutor_Precisa_Ser_Valido()
        {
            condutor.email = "@gmail.com";

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.email);
        }

        [TestMethod]
        public void Telefone_Do_Condutor_Deve_Ser_Valido()
        {
            condutor.telefone = "(49)999999-00999";

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.telefone);
        }

        [TestMethod]
        public void Cpf_Do_Condutor_Deve_Ser_Valido()
        {
            condutor.cpf = "013.#@$.@#$-%$";

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.cpf);
        }

        [TestMethod]
        public void Cnh_Do_Condutor_Deve_Ser_Valida()
        {
            condutor.cnh = "123#$%45678901";

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.cnh);
        }

        [TestMethod]
        public void ValidadeCnh_Do_Condutor_Deve_Ser_Maior_Que_Hoje()
        {
            condutor.validadeCnh = DateTime.Today.AddDays(5);

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.validadeCnh);
        }

        [TestMethod]
        public void ValidadeCnh_Do_Condutor_Deve_Ser_Pelo_Menos_15_Dias_Futura()
        {
            condutor.validadeCnh = DateTime.Today.AddDays(10);

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.validadeCnh);
        }

        [TestMethod]
        public void ValidadeCnh_Do_Condutor_Nao_Pode_Ser_Passada()
        {
            condutor.validadeCnh = DateTime.Today.AddDays(-1);

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.validadeCnh);
        }

        [TestMethod]
        public void Cnh_Do_Condutor_Deve_Ter_11_Caracteres()
        {
            condutor.cnh = "1234567890123";

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.cnh);
        }

        [TestMethod]
        public void Cnh_Do_Condutor_Não_Pode_Ter_Caracteres_Especiais()
        {
            condutor.cnh = "1234567890!";

            var resultado = validador.TestValidate(condutor);

            resultado.ShouldHaveValidationErrorFor(c => c.cnh);
        }
    }
}

