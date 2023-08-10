
using Moq;
using FluentAssertions;
using FluentResults;
using FluentResults.Extensions.FluentAssertions;
using FluentValidation.Results;
using LocadoraDeVeiculos.Aplicacao.ModuloCupomEParceiro;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using GeradorTestes.TestesUnitarios.Compartilhado;

namespace LocadoraDeVeiculos.TestesUnitarios.Aplicacao
{
    [TestClass]
    public class ServicoCupomTest
    {
        Mock<IRepositorioCupom> repositorioCupomMoq;
        Mock<IValidadorCupom> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoCupom servicoCupom;

        Cupom cupom;

        [TestInitialize]
        public void InicializarTeste()
        {
            repositorioCupomMoq = new Mock<IRepositorioCupom>();
            validadorMoq = new Mock<IValidadorCupom>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoCupom = new ServicoCupom(repositorioCupomMoq.Object, validadorMoq.Object, contextoMoq.Object);
            cupom = new Cupom(new Parceiro("ParceiroTeste"), DateTime.Now.AddDays(7), 10, "CUPOM DO DEKO");
        }

        [TestMethod]
        public void Deve_inserir_cupom_caso_ela_for_valido()
        {
            repositorioCupomMoq.Setup(x => x.Inserir(cupom)).Verifiable();
            validadorMoq.Setup(x => x.Validate(cupom)).Returns(new ValidationResult());

            Result resultado = servicoCupom.Inserir(cupom);

            resultado.Should().BeSuccess();
            repositorioCupomMoq.Verify(x => x.Inserir(cupom), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_inserir_cupom_caso_ele_seja_invalido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Cupom>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Teste", "Alguma validação falhou"));
                    return resultado;
                });

            var resultado = servicoCupom.Inserir(cupom);

            resultado.Should().BeFailure();
            repositorioCupomMoq.Verify(x => x.Inserir(cupom), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_inserir_cupom()
        {
            repositorioCupomMoq.Setup(x => x.Inserir(It.IsAny<Cupom>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Result resultado = servicoCupom.Inserir(cupom);

            resultado.Should().BeFailure();
            resultado.Reasons[0].Message.Should().Be("Falha ao tentar inserir um Cupom.");
        }

        [TestMethod]
        public void Deve_editar_cupom_caso_seja_valido()
        {
            Cupom cupom = new Cupom(new Parceiro("Parceiro"), DateTime.Now, 10.0m, "CupomAntigo");
            Cupom novoCupom = new Cupom(new Parceiro("Parceiro"), DateTime.Now, 15.0m, "NovoCupom");

            Result resultado = servicoCupom.Editar(novoCupom);

            resultado.Should().BeSuccess();
            repositorioCupomMoq.Verify(x => x.Editar(novoCupom), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_editar_cupom_caso_seja_invalido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Cupom>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Nome", "Nome não pode ter caracteres especiais"));
                    return resultado;
                });

            var resultado = servicoCupom.Editar(cupom);

            resultado.Should().BeFailure();
            repositorioCupomMoq.Verify(x => x.Editar(cupom), Times.Never());
        }
        [TestMethod]
        public void Nao_deve_editar_cupom_caso_o_nome_ja_esteja_cadastrado()
        {
            repositorioCupomMoq.Setup(x => x.SelecionarPorNome("NomeDoCupom"))
                 .Returns(() =>
                 {
                     return new Cupom(new Parceiro("Parceiro"), DateTime.Now, 10.0m, "NomeDoCupom");
                 });

            Cupom novoCupom = new Cupom(new Parceiro("Parceiro"), DateTime.Now, 15.0m, "NomeDoCupom");

            var resultado = servicoCupom.Editar(novoCupom);

            resultado.Should().BeFailure();
            resultado.Reasons[0].Message.Should().MatchRegex(@"Este nome [´']?NomeDoCupom[´']? já está sendo utilizado");
            repositorioCupomMoq.Verify(x => x.Editar(novoCupom), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_editar_cupom()
        {
            repositorioCupomMoq.Setup(x => x.Editar(It.IsAny<Cupom>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Cupom cupom = new Cupom(new Parceiro("Parceiro"), DateTime.Now, 10.0m, "NomeDoCupom");

            Result resultado = servicoCupom.Editar(cupom);

            resultado.Should().BeFailure();
            resultado.Errors[0].Message.Should().MatchRegex("(?i)Falha ao tentar editar um Cupom\\.");
        }

        [TestMethod]
        public void Deve_excluir_cupom_caso_ele_esteja_cadastrado()
        {
            var cupom = new Cupom(new Parceiro("Parceiro"), DateTime.Now, 10.0m, "NomeDoCupom");

            repositorioCupomMoq.Setup(x => x.Existe(cupom))
               .Returns(() =>
               {
                   return true;
               });

            var resultado = servicoCupom.Excluir(cupom);

            resultado.Should().BeSuccess();
            repositorioCupomMoq.Verify(x => x.Excluir(cupom), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_excluir_cupom_caso_ele_nao_esteja_cadastrado()
        {
            var cupom = new Cupom(new Parceiro("Parceiro"), DateTime.Now, 10.0m, "NomeDoCupom");

            repositorioCupomMoq.Setup(x => x.Existe(cupom))
               .Returns(() =>
               {
                   return false;
               });

            var resultado = servicoCupom.Excluir(cupom);

            resultado.Should().BeFailure();
            repositorioCupomMoq.Verify(x => x.Excluir(cupom), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_excluir_cupom()
        {
            var cupom = new Cupom(new Parceiro("Parceiro"), DateTime.Now, 10.0m, "NomeDoCupom");

            repositorioCupomMoq.Setup(x => x.Existe(cupom))
                .Throws(() =>
                {
                    return SqlExceptionCreator.NewSqlException();
                });

            Result resultado = servicoCupom.Excluir(cupom);

            resultado.Should().BeFailure();
            StringAssert.Contains("Falha ao tentar excluir um Cupom", resultado.Reasons[0].Message);
        }

    }
}
