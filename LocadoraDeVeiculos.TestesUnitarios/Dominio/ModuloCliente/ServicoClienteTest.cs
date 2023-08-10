
using Moq;
using FluentAssertions;
using FluentResults;
using FluentResults.Extensions.FluentAssertions;
using FluentValidation.Results;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using GeradorTestes.TestesUnitarios.Compartilhado;

namespace LocadoraDeVeiculos.TestesUnitarios.Aplicacao
{
    [TestClass]
    public class ServicoClienteTest
    {
        Mock<IRepositorioCliente> repositorioClienteMoq;
        Mock<IValidadorCliente> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoCliente servicoCliente;

        Cliente cliente;

        [TestInitialize]
        public void InicializarTeste()
        {
            repositorioClienteMoq = new Mock<IRepositorioCliente>();
            validadorMoq = new Mock<IValidadorCliente>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoCliente = new ServicoCliente(repositorioClienteMoq.Object, validadorMoq.Object, contextoMoq.Object);
            cliente = new Cliente();
        }

        [TestMethod]
        public void Deve_inserir_cliente_caso_seja_valido()
        {
            cliente = new Cliente("Teste");

            Result resultado = servicoCliente.Inserir(cliente);

            resultado.Should().BeSuccess();
            repositorioClienteMoq.Verify(x => x.Inserir(cliente), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_inserir_cliente_caso_seja_invalido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Cliente>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Teste", "Nome não pode ter caracteres especiais"));
                    return resultado;
                });

            var resultado = servicoCliente.Inserir(cliente);

            resultado.Should().BeFailure();
            repositorioClienteMoq.Verify(x => x.Inserir(cliente), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_inserir_cliente()
        {
            repositorioClienteMoq.Setup(x => x.Inserir(It.IsAny<Cliente>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Result resultado = servicoCliente.Inserir(cliente);

            resultado.Should().BeFailure();
            resultado.Reasons[0].Message.Should().Be("Falha ao tentar inserir um Cliente.");
        }

        [TestMethod]
        public void Deve_editar_cliente_caso_seja_valido()
        {
            Cliente cliente = new Cliente("Novo Cliente");

            Result resultado = servicoCliente.Editar(cliente);

            resultado.Should().BeSuccess();
            repositorioClienteMoq.Verify(x => x.Editar(cliente), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_editar_cliente_caso_seja_invalido() //cenário 2
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Cliente>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Nome", "Nome não pode ter caracteres especiais"));
                    return resultado;
                });

            var resultado = servicoCliente.Editar(cliente);

            resultado.Should().BeFailure();
            repositorioClienteMoq.Verify(x => x.Editar(cliente), Times.Never());
        }

        [TestMethod]
        public void Deve_editar_cliente_com_o_mesmo_nome() //cenário 3
        {
            Guid id = Guid.NewGuid();

            repositorioClienteMoq.Setup(x => x.SelecionarPorNome("NomeDoCliente"))
                 .Returns(() =>
                 {
                     return new Cliente(id, "NomeDoCliente");
                 });

            Cliente outroCliente = new Cliente(id, "NomeDoCliente");

            var resultado = servicoCliente.Editar(outroCliente);

            resultado.Should().BeSuccess();
            repositorioClienteMoq.Verify(x => x.Editar(outroCliente), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_editar_cliente_caso_o_nome_ja_esteja_cadastrado() //cenário 4
        {
            repositorioClienteMoq.Setup(x => x.SelecionarPorNome("NomeDoCliente"))
                 .Returns(() =>
                 {
                     return new Cliente("NomeDoCliente");
                 });

            Cliente novoCliente = new Cliente("NomeDoCliente");

            var resultado = servicoCliente.Editar(novoCliente);

            resultado.Should().BeFailure();
            resultado.Reasons[0].Message.Should().MatchRegex(@"Este nome [´']?NomeDoCliente[´']? já está sendo utilizado");
            repositorioClienteMoq.Verify(x => x.Editar(novoCliente), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_editar_cliente() //cenário 5
        {
            repositorioClienteMoq.Setup(x => x.Editar(It.IsAny<Cliente>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Result resultado = servicoCliente.Editar(cliente);

            resultado.Should().BeFailure();
            resultado.Errors[0].Message.Should().Be("Falha ao tentar editar um Cliente.");
        }

        [TestMethod]
        public void Deve_excluir_cliente_caso_ele_esteja_cadastrado() //cenário 1
        {
            var cliente = new Cliente("NomeDoCliente");

            repositorioClienteMoq.Setup(x => x.Existe(cliente))
               .Returns(() =>
               {
                   return true;
               });

            var resultado = servicoCliente.Excluir(cliente);

            resultado.Should().BeSuccess();
            repositorioClienteMoq.Verify(x => x.Excluir(cliente), Times.Once());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_excluir_cliente()
        {
            var cliente = new Cliente("NomeDoCliente");

            repositorioClienteMoq.Setup(x => x.Existe(cliente))
                .Throws(() =>
                {
                    return SqlExceptionCreator.NewSqlException();
                });

            Result resultado = servicoCliente.Excluir(cliente);

            resultado.Should().BeFailure();
            StringAssert.Contains("Falha ao tentar excluir um Cliente", resultado.Reasons[0].Message);
        }

        [TestMethod]
        public void Nao_deve_excluir_cliente_caso_ele_nao_esteja_cadastrado() //cenário 2
        {
            var cliente = new Cliente("NomeDoCliente");

            repositorioClienteMoq.Setup(x => x.Existe(cliente))
                .Returns(() =>
                {
                    return false;
                });

            var resultado = servicoCliente.Excluir(cliente);

            resultado.Should().BeFailure();
            repositorioClienteMoq.Verify(x => x.Excluir(cliente), Times.Never());
        }

    }
}
