using Moq;
using FluentAssertions;
using FluentResults;
using FluentResults.Extensions.FluentAssertions;
using FluentValidation.Results;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using GeradorTestes.TestesUnitarios.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloCondutor
{
    [TestClass]
    public class ServicoCondutorTest
    {
        Mock<IRepositorioCondutor> repositorioCondutorMoq;
        Mock<IValidadorCondutor> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoCondutor servicoCondutor;

        Condutor condutor;

        [TestInitialize]

        public void InicializarTeste()
        {
            Cliente cliente = new Cliente("teste");
            repositorioCondutorMoq = new Mock<IRepositorioCondutor>();
            validadorMoq = new Mock<IValidadorCondutor>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoCondutor = new ServicoCondutor(repositorioCondutorMoq.Object, validadorMoq.Object, contextoMoq.Object);
            condutor = new Condutor(cliente, DateTime.Now, "telefone", "nome", "email", "cpf", "cnh");
        }

        [TestMethod]
        public void Deve_inserir_condutor_caso_seja_valido()
        {
            repositorioCondutorMoq.Setup(x => x.Inserir(condutor)).Verifiable();
            validadorMoq.Setup(x => x.Validate(condutor)).Returns(new ValidationResult());

            Result resultado = servicoCondutor.Inserir(condutor);

            resultado.Should().BeSuccess();
            repositorioCondutorMoq.Verify(x => x.Inserir(condutor), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_inserir_condutor_caso_seja_invalido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Condutor>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Teste", "Alguma validação falhou"));
                    return resultado;
                });

            var resultado = servicoCondutor.Inserir(condutor);

            resultado.Should().BeFailure();
            repositorioCondutorMoq.Verify(x => x.Inserir(condutor), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_inserir_condutor()
        {
            repositorioCondutorMoq.Setup(x => x.Inserir(It.IsAny<Condutor>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Result resultado = servicoCondutor.Inserir(condutor);

            resultado.IsSuccess.Should().BeFalse();
            resultado.Errors.Should().ContainSingle();
            resultado.Errors[0].Message.Should().BeEquivalentTo("Falha ao tentar inserir um Condutor.");
        }



        [TestMethod]
        public void Deve_editar_condutor_caso_seja_valido()
        {
            Cliente novoCliente = new Cliente("NovoCliente");

            Condutor condutorAntigo = new Condutor(new Cliente("ClienteAntigo"), DateTime.Now, "Telefone", "Nome", "Email", "CPF", "CNH");
            Condutor novoCondutor = new Condutor(novoCliente, DateTime.Now, "NovoTelefone", "NovoNome", "NovoEmail", "NovoCPF", "NovoCNH");

            Result resultado = servicoCondutor.Editar(novoCondutor);

            resultado.Should().BeSuccess();
            repositorioCondutorMoq.Verify(x => x.Editar(novoCondutor), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_editar_condutor_caso_seja_invalido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Condutor>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Nome", "Nome não pode ter caracteres especiais"));
                    return resultado;
                });

            var resultado = servicoCondutor.Editar(condutor);

            resultado.Should().BeFailure();
            repositorioCondutorMoq.Verify(x => x.Editar(condutor), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_editar_condutor()
        {
            repositorioCondutorMoq.Setup(x => x.Editar(It.IsAny<Condutor>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Condutor condutor = new Condutor();

            Result resultado = servicoCondutor.Editar(condutor);

            resultado.Should().BeFailure();
            resultado.Errors[0].Message.Should().MatchRegex("(?i)Falha ao tentar editar um Condutor\\.");
        }

        [TestMethod]
        public void Nao_deve_excluir_condutor_caso_ele_nao_esteja_cadastrado()
        {
            var condutor = new Condutor();

            repositorioCondutorMoq.Setup(x => x.Existe(condutor))
               .Returns(() =>
               {
                   return false;
               });

            var resultado = servicoCondutor.Excluir(condutor);

            resultado.Should().BeFailure();
            repositorioCondutorMoq.Verify(x => x.Excluir(condutor), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_excluir_condutor()
        {
            var condutor = new Condutor();

            repositorioCondutorMoq.Setup(x => x.Existe(condutor))
                .Throws(() =>
                {
                    return SqlExceptionCreator.NewSqlException();
                });

            Result resultado = servicoCondutor.Excluir(condutor);

            resultado.Should().BeFailure();
            StringAssert.Contains("Falha ao tentar excluir um condutor.", resultado.Reasons[0].Message, StringComparison.OrdinalIgnoreCase);
        }


    }
}
