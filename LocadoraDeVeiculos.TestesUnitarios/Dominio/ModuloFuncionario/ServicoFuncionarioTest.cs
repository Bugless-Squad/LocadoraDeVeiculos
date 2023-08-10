using FluentAssertions;
using FluentAssertions.Extensions;
using FluentValidation.Results;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using GeradorTestes.TestesUnitarios.Compartilhado;
using Moq;
using FluentResults;
using System.Runtime.Intrinsics.X86;
using System;
using FluentAssertions.Execution;

namespace LocadoraDeVeiculos.TestesUnitarios.Aplicacao
{
    [TestClass]
    public class ServicoFuncionarioTest
    {
        Mock<IRepositorioFuncionario> repositorioFuncionarioMoq;
        Mock<IValidadorFuncionario> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoFuncionario servicoFuncionario;

        Funcionario funcionario;

        [TestInitialize]

        public void InicializarTeste()
        {
            repositorioFuncionarioMoq = new Mock<IRepositorioFuncionario>();
            validadorMoq = new Mock<IValidadorFuncionario>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoFuncionario = new ServicoFuncionario(repositorioFuncionarioMoq.Object, validadorMoq.Object, contextoMoq.Object);
            funcionario = new Funcionario();
        }

        [TestMethod]
        public void Deve_inserir_funcionario_caso_seja_valido()
        {
            repositorioFuncionarioMoq.Setup(x => x.Inserir(funcionario)).Verifiable();
            validadorMoq.Setup(x => x.Validate(funcionario)).Returns(new ValidationResult());

            Result resultado = servicoFuncionario.Inserir(funcionario);

            resultado.IsSuccess.Should().BeTrue();
            repositorioFuncionarioMoq.Verify(x => x.Inserir(funcionario), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_inserir_funcionario_caso_seja_invalido()
        {
            validadorMoq.Setup(x => x.Validate(It.IsAny<Funcionario>()))
                .Returns(() =>
                {
                    var resultado = new ValidationResult();
                    resultado.Errors.Add(new ValidationFailure("Teste", "Alguma validação falhou"));
                    return resultado;
                });

            var resultado = servicoFuncionario.Inserir(funcionario);

            resultado.IsSuccess.Should().BeFalse(); // Verifica se é uma falha
            repositorioFuncionarioMoq.Verify(x => x.Inserir(funcionario), Times.Never());
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_inserir_funcionario()
        {
            repositorioFuncionarioMoq.Setup(x => x.Inserir(It.IsAny<Funcionario>()))
                .Throws(() =>
                {
                    return new Exception();
                });

            Result resultado = servicoFuncionario.Inserir(funcionario);

            resultado.IsSuccess.Should().BeFalse();
            resultado.Errors.Should().ContainSingle();
            resultado.Errors[0].Message.Should().BeEquivalentTo("Falha ao tentar inserir um Funcionario.");
        }

        [TestMethod]
        public void Deve_tratar_erro_caso_ocorra_falha_ao_tentar_excluir_funcionario()
        {
            repositorioFuncionarioMoq.Setup(x => x.Existe(funcionario))
                .Throws(() =>
                {
                    return SqlExceptionCreator.NewSqlException();
                });

            Result resultado = servicoFuncionario.Excluir(funcionario);

            resultado.IsSuccess.Should().BeFalse();
            resultado.Errors.Should().ContainSingle();
            resultado.Errors[0].Message.Should().BeEquivalentTo("Falha ao tentar excluir um Funcionario.");
        }
        [TestMethod]
        public void Nao_deve_editar_funcionario_caso_o_nome_ja_esteja_cadastrado() 
        {
            repositorioFuncionarioMoq.Setup(x => x.SelecionarPorNome("NomeDoFuncionario"))
                 .Returns(() =>
                 {
                     return new Funcionario("NomeDoFuncionario", 1800, DateTime.Now);
                 });

            Funcionario novoFuncionario = new Funcionario("NomeDoFuncionario", 2200, DateTime.Now);

            var resultado = servicoFuncionario.Editar(novoFuncionario);

            resultado.IsSuccess.Should().BeFalse();
            resultado.Errors.Should().ContainSingle();
            resultado.Errors[0].Message.Should().MatchRegex(@"Este nome [´']?NomeDoFuncionario[´']? já está sendo utilizado");

            repositorioFuncionarioMoq.Verify(x => x.Editar(novoFuncionario), Times.Never());
        }

        [TestMethod]
        public void Nao_deve_excluir_funcionario_caso_ele_nao_esteja_cadastrado() // cenário 2
        {
            var funcionario = new Funcionario("NomeDoFuncionario", 2000, DateTime.Now);

            repositorioFuncionarioMoq.Setup(x => x.Existe(funcionario))
                .Returns(() =>
                {
                    return false;
                });

            var resultado = servicoFuncionario.Excluir(funcionario);

            resultado.IsSuccess.Should().BeFalse();
            repositorioFuncionarioMoq.Verify(x => x.Excluir(funcionario), Times.Never());
        }
    }
}

