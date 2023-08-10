
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
    public class ServicoParceiroTest
    {
        Mock<IRepositorioParceiro> repositorioParceiroMoq;
        Mock<IValidadorParceiro> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoParceiro servicoParceiro;

        Parceiro parceiro;

        [TestInitialize]
        public void InicializarTeste()
        {
            repositorioParceiroMoq = new Mock<IRepositorioParceiro>();
            validadorMoq = new Mock<IValidadorParceiro>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoParceiro = new ServicoParceiro(repositorioParceiroMoq.Object, validadorMoq.Object, contextoMoq.Object);
            parceiro = new Parceiro("Teste");
        }

        [TestMethod]
        public void Deve_inserir_parceiro_caso_ela_for_valida()
        {
            parceiro = new Parceiro("Teste");

            Result resultado = servicoParceiro.Inserir(parceiro);

            resultado.Should().BeSuccess();
            repositorioParceiroMoq.Verify(x => x.Inserir(parceiro), Times.Once());
        }
        [TestMethod]
        public void Nao_deve_inserir_parceiro_caso_ela_seja_invalida()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Parceiro>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Teste", "Nome não pode ter caracteres especiais"));
                    return resultado;
                });

            var resultado = servicoParceiro.Inserir(parceiro);

            resultado.Should().BeFailure();
            repositorioParceiroMoq.Verify(x => x.Inserir(parceiro), Times.Never());
        }

        [TestMethod]
        public void Nao_deve_inserir_parceiro_caso_o_nome_ja_esteja_cadastrado() 
        {
            string nomeParceiro = "Teste";

            repositorioParceiroMoq.Setup(x => x.SelecionarPorNome(nomeParceiro))
                .Returns(() =>
                {
                    return new Parceiro(nomeParceiro); 
                });

            var resultado = servicoParceiro.Inserir(parceiro);

            resultado.Should().BeFailure();
            resultado.Reasons[0].Message.Should().Contain("Este nome").And.Contain("Teste").And.Contain("já está sendo utilizado");

            repositorioParceiroMoq.Verify(x => x.Inserir(It.IsAny<Parceiro>()), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_inserir_parceiro()
        {
            repositorioParceiroMoq.Setup(x => x.Inserir(It.IsAny<Parceiro>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Result resultado = servicoParceiro.Inserir(parceiro);

            resultado.Should().BeFailure();
            resultado.Reasons[0].Message.Should().Be("Falha ao tentar inserir um Parceiro.");
        }

        [TestMethod]
        public void Deve_editar_parceiro_caso_ela_for_valido() 
        {
            Parceiro parceiro = new Parceiro("Novo Parceiro");

            Result resultado = servicoParceiro.Editar(parceiro);

            resultado.Should().BeSuccess();
            repositorioParceiroMoq.Verify(x => x.Editar(parceiro), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_editar_parceiro_caso_ela_seja_invalido() //cenário 2
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Parceiro>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Nome", "Nome não pode ter caracteres especiais"));
                    return resultado;
                });

            var resultado = servicoParceiro.Editar(parceiro);

            resultado.Should().BeFailure();
            repositorioParceiroMoq.Verify(x => x.Editar(parceiro), Times.Never());
        }

        [TestMethod]
        public void Deve_editar_parceiro_com_o_mesmo_nome() //cenário 3
        {
            Guid id = Guid.NewGuid();

            repositorioParceiroMoq.Setup(x => x.SelecionarPorNome("NomeDoParceiro"))
                 .Returns(() =>
                 {
                     return new Parceiro(id, "NomeDoParceiro");
                 });

            Parceiro outroParceiro = new Parceiro(id, "NomeDoParceiro");

            var resultado = servicoParceiro.Editar(outroParceiro);

            resultado.Should().BeSuccess();
            repositorioParceiroMoq.Verify(x => x.Editar(outroParceiro), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_editar_parceiro_caso_o_nome_ja_esteja_cadastrado() //cenário 4
        {
            repositorioParceiroMoq.Setup(x => x.SelecionarPorNome("NomeDoParceiro"))
                 .Returns(() =>
                 {
                     return new Parceiro("NomeDoParceiro");
                 });

            Parceiro novoParceiro = new Parceiro("NomeDoParceiro");

            var resultado = servicoParceiro.Editar(novoParceiro);

            resultado.Should().BeFailure();
            resultado.Reasons[0].Message.Should().MatchRegex(@"Este nome [´']?NomeDoParceiro[´']? já está sendo utilizado");
            repositorioParceiroMoq.Verify(x => x.Editar(novoParceiro), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_editar_parceiro() //cenário 5
        {
            repositorioParceiroMoq.Setup(x => x.Editar(It.IsAny<Parceiro>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Result resultado = servicoParceiro.Editar(parceiro);

            resultado.Should().BeFailure();
            resultado.Errors[0].Message.Should().Be("Falha ao tentar editar um Parceiro.");

        }

        [TestMethod]
        public void Deve_excluir_parceiro_caso_ele_esteja_cadastrado() //cenário 1
        {
            var parceiro = new Parceiro("NomeDoParceiro");

            repositorioParceiroMoq.Setup(x => x.Existe(parceiro))
               .Returns(() =>
               {
                   return true;
               });

            var resultado = servicoParceiro.Excluir(parceiro);

            resultado.Should().BeSuccess();
            repositorioParceiroMoq.Verify(x => x.Excluir(parceiro), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_excluir_parceiro_caso_ele_nao_esteja_cadastrado() //cenário 2
        {
            var parceiro = new Parceiro("NomeDoParceiro");

            repositorioParceiroMoq.Setup(x => x.Existe(parceiro))
               .Returns(() =>
               {
                   return false;
               });

            var resultado = servicoParceiro.Excluir(parceiro);
 
            resultado.Should().BeFailure();
            repositorioParceiroMoq.Verify(x => x.Excluir(parceiro), Times.Never());
        }

        [TestMethod]
        public void Nao_deve_excluir_parceiro_caso_ele_esteja_relacionado_com_cupom() //cenário 3
        {
            var parceiro = new Parceiro("NomeDoParceiro");

            repositorioParceiroMoq.Setup(x => x.Existe(parceiro))
               .Returns(() =>
               {
                   return true;
               });

            repositorioParceiroMoq.Setup(x => x.Excluir(It.IsAny<Parceiro>()))
                .Throws(() =>
                {
                    return SqlExceptionCreator.NewSqlException(errorMessage: "FK_TBCupom_TBParceiro");
                });

            Result resultado = servicoParceiro.Excluir(parceiro);

            resultado.Should().BeFailure();
            resultado.Reasons[0].Message.Should().Be("Este parceiro está relacionado a um cupom e não pode ser excluido");

        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_excluir_parceiro()
        {
            var parceiro = new Parceiro("NomeDoParceiro");

            repositorioParceiroMoq.Setup(x => x.Existe(parceiro))
                .Throws(() =>
                {
                    return SqlExceptionCreator.NewSqlException();
                });


            Result resultado = servicoParceiro.Excluir(parceiro);

            resultado.Should().BeFailure();
            StringAssert.Contains("Falha ao tentar excluir um Parceiro", resultado.Reasons[0].Message);
        }

    }
}
