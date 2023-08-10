using FluentAssertions;
using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraDeVeiculos.Aplicacao.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using Moq;
using FluentAssertions.Execution;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;
using GeradorTestes.TestesUnitarios.Compartilhado;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloPlanoCobranca
{
    [TestClass]
    public class ServicoPlanoCobrancaTest
    {
        Mock<IRepositorioPlanoCobranca> repositorioPlanoCobrancaMoq;
        Mock<IValidadorPlanoCobranca> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoPlanoCobranca servicoPlanoCobranca;

        PlanoCobranca planoCobranca;

        [TestInitialize]
        public void InicializarTeste()
        {
            repositorioPlanoCobrancaMoq = new Mock<IRepositorioPlanoCobranca>();
            validadorMoq = new Mock<IValidadorPlanoCobranca>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoPlanoCobranca = new ServicoPlanoCobranca(repositorioPlanoCobrancaMoq.Object, validadorMoq.Object, contextoMoq.Object);

            planoCobranca = new PlanoCobranca();
        }

        [TestMethod]
        public void Deve_inserir_planoCobranca_caso_ele_seja_valido()
        {
            planoCobranca = new PlanoCobranca();

            Result resultado = servicoPlanoCobranca.Inserir(planoCobranca);

            resultado.IsSuccess.Should().BeTrue();
            repositorioPlanoCobrancaMoq.Verify(x => x.Inserir(planoCobranca), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_inserir_planoCobranca_caso_ele_seja_invalido()
        {

            validadorMoq.Setup(x => x.Validate(It.IsAny<PlanoCobranca>()))
                .Returns(() =>
                {
                    var validationResult = new ValidationResult(new List<ValidationFailure>
                    {
                        new ValidationFailure("Nome", "Nome não pode ter caracteres especiais")
                    });
                    return validationResult;
                });

            var resultado = servicoPlanoCobranca.Inserir(planoCobranca);

            resultado.IsSuccess.Should().BeFalse();
            repositorioPlanoCobrancaMoq.Verify(x => x.Inserir(planoCobranca), Times.Never());
        }

        [TestMethod]
        public void Deve_editar_planoCobranca_caso_ele_seja_valido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<PlanoCobranca>()))
                .Returns(new ValidationResult());

            Result resultado = servicoPlanoCobranca.Editar(planoCobranca);

            resultado.IsSuccess.Should().BeTrue();
            repositorioPlanoCobrancaMoq.Verify(x => x.Editar(planoCobranca), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_editar_planoCobranca_caso_ele_seja_invalido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<PlanoCobranca>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Campo", "Mensagem de erro"));
                    return resultado;
                });

            var resultado = servicoPlanoCobranca.Editar(planoCobranca);

            resultado.IsSuccess.Should().BeFalse();
            repositorioPlanoCobrancaMoq.Verify(x => x.Editar(planoCobranca), Times.Never());
        }

        [TestMethod]
        public void Nao_deve_excluir_disciplina_caso_ela_nao_esteja_cadastrada()
        {
            repositorioPlanoCobrancaMoq.Setup(x => x.Existe(planoCobranca))
               .Returns(() =>
               {
                   return false;
               });

            var resultado = servicoPlanoCobranca.Excluir(planoCobranca);

            resultado.IsSuccess.Should().BeFalse();
            repositorioPlanoCobrancaMoq.Verify(x => x.Excluir(planoCobranca), Times.Never());
        }
    }
}
