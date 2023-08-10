
using Moq;
using FluentAssertions;
using FluentResults;
using FluentResults.Extensions.FluentAssertions;
using FluentValidation.Results;
using LocadoraDeVeiculos.Aplicacao.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using GeradorTestes.TestesUnitarios.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using LocadoraDeVeiculos.Aplicacao.ModuloCupomEParceiro;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloAutomovel
{
    [TestClass]
    public class ServicoAutomovelTest
    {
        Mock<IRepositorioAutomovel> repositorioAutomovelMoq;
        Mock<IValidadorAutomovel> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoAutomovel servicoAutomovel;

        Automovel automovel;

        [TestInitialize]

        public void InicializarTeste()
        {
            repositorioAutomovelMoq = new Mock<IRepositorioAutomovel>();
            validadorMoq = new Mock<IValidadorAutomovel>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoAutomovel = new ServicoAutomovel(repositorioAutomovelMoq.Object, validadorMoq.Object, contextoMoq.Object);
            automovel = new Automovel();
        }
        [TestMethod]
        public void Deve_inserir_automovel_caso_seja_valido()
        {
            repositorioAutomovelMoq.Setup(x => x.Inserir(automovel)).Verifiable();
            validadorMoq.Setup(x => x.Validate(automovel)).Returns(new ValidationResult());

            Result resultado = servicoAutomovel.Inserir(automovel);

            resultado.Should().BeSuccess();
            repositorioAutomovelMoq.Verify(x => x.Inserir(automovel), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_inserir_automovel_caso_seja_invalido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Automovel>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Teste", "Alguma validação falhou"));
                    return resultado;
                });

            var resultado = servicoAutomovel.Inserir(automovel);

            resultado.Should().BeFailure();
            repositorioAutomovelMoq.Verify(x => x.Inserir(automovel), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_inserir_automovel()
        {
            repositorioAutomovelMoq.Setup(x => x.Inserir(It.IsAny<Automovel>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Result resultado = servicoAutomovel.Inserir(automovel);

            resultado.Should().BeFailure();
            resultado.Reasons[0].Message.Should().Be("Falha ao tentar inserir um automovel.");

        }

        [TestMethod]
        public void Nao_deve_editar_automovel_caso_seja_invalido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Automovel>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Modelo", "Modelo inválido"));
                    return resultado;
                });

            var resultado = servicoAutomovel.Editar(automovel);

            resultado.Should().BeFailure();
            repositorioAutomovelMoq.Verify(x => x.Editar(automovel), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_excluir_automovel()
        {
            var automovel = new Automovel();

            repositorioAutomovelMoq.Setup(x => x.Existe(automovel))
                .Throws(() =>
                {
                    return SqlExceptionCreator.NewSqlException();
                });

            Result resultado = servicoAutomovel.Excluir(automovel);

            resultado.Should().BeFailure();
            StringAssert.Contains("Falha ao tentar excluir um automovel.", resultado.Reasons[0].Message, StringComparison.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void Nao_deve_excluir_automovel_caso_ele_nao_esteja_cadastrado()
        {
            var automovel = new Automovel();

            repositorioAutomovelMoq.Setup(x => x.Existe(automovel))
               .Returns(() =>
               {
                   return false;
               });

            var resultado = servicoAutomovel.Excluir(automovel);

            resultado.Should().BeFailure();
            repositorioAutomovelMoq.Verify(x => x.Excluir(automovel), Times.Never());
        }


    }
}
