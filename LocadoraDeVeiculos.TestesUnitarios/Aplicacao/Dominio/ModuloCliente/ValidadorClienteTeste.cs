using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado.Validators;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloCliente
{
    [TestClass]
    public class ClienteTest
    {
        private Cliente cliente;
        private ValidadorCliente validadorCliente;

        public ClienteTest()
        {
            Cliente cliente = new();

            cliente.nome = "Eduardo";
            cliente.email = "teste@gmail.com";
            cliente.telefone = "(49) 98800-3240";
            cliente.tipoPessoa = "Pessoa Física";
            cliente.cpf = "013.499.589-99";
            cliente.cnpj = "00.000.000/4567-88";
            cliente.cep = "88503-000";
            cliente.estado = "SC";
            cliente.cidade = "Lages";
            cliente.rua = "exemplo rua";
            cliente.numero = 150;
            cliente.bairro = "Centenario";

            this.cliente = cliente;

            validadorCliente = new ValidadorCliente();
        }
        [TestMethod]
        public void Nome_Do_Cliente_É_Obrigatório()
        {
            // arrange
            cliente.nome = null;

            // action
            var result = validadorCliente.TestValidate(cliente);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.nome);
        }

        [TestMethod]
        public void Nome_Do_Cliente_Maior_Que_Tres_Caracteres()
        {
            // arrange
            cliente.nome = "as";

            // action
            var result = validadorCliente.TestValidate(cliente);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.nome);
        }

        [TestMethod]
        public void Nome_Do_Cliente_Nao_Pode_Numeros()
        {
            cliente.nome = "123";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.nome);
        }

        [TestMethod]
        public void Nome_Do_Cliente_Nao_Pode_Caracteres_Especial()
        {
            cliente.nome = "$%%#@@";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.nome);
        }

        [TestMethod]
        public void Email_Do_Cliente_Precisa_Ser_Valido()
        {
            // arrange
            cliente.email = "@gmail.com";

            // action
            var result = validadorCliente.TestValidate(cliente);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.email);
        }

        [TestMethod]
        public void Email_Do_Cliente_Nulo()
        {
            // arrange
            cliente.email = "";

            // action
            var result = validadorCliente.TestValidate(cliente);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.email);
        }

        [TestMethod]
        public void Telefone_Nulo()
        {
            // arrange
            cliente.telefone = "";

            // action
            var result = validadorCliente.TestValidate(cliente);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.telefone);
        }

        [TestMethod]
        public void Telefone_Invalido()
        {
            // arrange
            cliente.telefone = "(49)999999-00999";

            // action
            var result = validadorCliente.TestValidate(cliente);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.telefone);
        }

        [TestMethod]
        public void Tipo_De_Pessoa_Nulo()
        {
            cliente.tipoPessoa = "";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.tipoPessoa);
        }

        [TestMethod]
        public void Cpf_Null()
        {
            cliente.cpf = "";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.cpf);
        }

        [TestMethod]
        public void Cpf_Caracter_Especial()
        {
            cliente.cpf = "013.#@$.@#$-%$";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.cpf);
        }

        [TestMethod]
        public void Cnpj_Caracter_Especial()
        {
            cliente.cnpj = "00.000.000/@@@@-@@";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.cnpj);
        }

        [TestMethod]
        public void Cep_Null()
        {
            cliente.cep = "";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.cep);
        }

        [TestMethod]
        public void Cep_Caractere_Especiais()
        {
            cliente.cep = "####-###";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.cep);
        }

        [TestMethod]

        public void Estado_Null()
        {
            cliente.estado = "";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.estado);
        }

        [TestMethod]
        public void Estado_Caracter_Especial()
        {
            cliente.estado = "$$$$";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.estado);
        }

        [TestMethod]
        public void Cidade_Null()
        {
            cliente.cidade = "";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.cidade);
        }

        [TestMethod]
        public void Cidade_Caracter_Especial()
        {
            cliente.estado = "$$$$";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.estado);
        }

        [TestMethod]
        public void Bairro_Null()
        {
            cliente.bairro = "";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.bairro);
        }

        [TestMethod]
        public void Bairro_Caracter_Especial()
        {
            cliente.bairro = "#$$##$#";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.bairro);
        }

        [TestMethod]
        public void Rua_Caracter_Especial()
        {
            cliente.rua = "#$$##$#";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.rua);
        }

        [TestMethod]
        public void Rua_Null()
        {
            cliente.rua = "";

            var result = validadorCliente.TestValidate(cliente);

            result.ShouldHaveValidationErrorFor(c => c.rua);
        }

    }
}
