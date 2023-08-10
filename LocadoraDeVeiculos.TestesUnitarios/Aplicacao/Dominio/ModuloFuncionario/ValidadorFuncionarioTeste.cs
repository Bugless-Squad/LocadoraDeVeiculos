using FluentValidation.TestHelper;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio
{
    [TestClass]
    public class FuncionarioTest
    {
        private Funcionario funcionario;
        private ValidadorFuncionario validador;
        
        public FuncionarioTest()
        {
            funcionario = new Funcionario();
            validador = new ValidadorFuncionario();

        }

        [TestMethod]
        public void Nome_Do_Funcionario_É_Obrigatório()
        {
            //arrenge
            funcionario.nome = null;

            //action
            var outroResultado = validador.TestValidate(funcionario);

            //assert
            outroResultado.ShouldHaveValidationErrorFor(funcionario => funcionario.nome);
        }

        [TestMethod]
        public void Nome_Do_Funcionario_Deve_Ser_Valido()
        {

            funcionario.nome = "##123";


            var outroResultado = validador.TestValidate(funcionario);


            outroResultado.ShouldHaveValidationErrorFor(funcionario => funcionario.nome);
        }

        [TestMethod]
        public void Nome_Do_Funcionario_Deve_Ser_Maior_Que_Dois_Carateres()
        {

            funcionario.nome = "Je";


            var outroResultado = validador.TestValidate(funcionario);


            outroResultado.ShouldHaveValidationErrorFor(funcionario => funcionario.nome);
        }

        [TestMethod]
        public void Salario_Do_Funcionario_Deve_Ser_Maior_Que_Zero()
        {

            funcionario.salario = 0.00m;


            var outroResultado = validador.TestValidate(funcionario);


            outroResultado.ShouldHaveValidationErrorFor(funcionario => funcionario.salario);
        }

        [TestMethod]
        public void Data_Admissao_Deve_Ser_Valida()
        {

            funcionario.dataAdmissao = new DateTime(2023, 2, 6);


            var result = validador.TestValidate(funcionario);


            result.ShouldNotHaveValidationErrorFor(f => f.dataAdmissao);
        }

        [TestMethod]
        public void Data_Admissao_Nao_Pode_Ser_Futura()
        {

            funcionario.dataAdmissao = DateTime.Now.AddDays(1);


            var result = validador.TestValidate(funcionario);


            result.ShouldHaveValidationErrorFor(f => f.dataAdmissao);
        }

        [TestMethod]
        public void Data_Admissao_Deve_Ser_Menor_Que_Data_Atual()
        {
            funcionario.dataAdmissao = DateTime.Today.AddDays(1);

            var result = validador.TestValidate(funcionario);

            result.ShouldHaveValidationErrorFor(f => f.dataAdmissao);
        }

    }
}