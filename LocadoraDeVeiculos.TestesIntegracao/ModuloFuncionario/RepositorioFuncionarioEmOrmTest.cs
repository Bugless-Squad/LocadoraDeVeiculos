using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Infra.Orm.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocadoraDeVeiculos.TestesIntegracao.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmOrmTest 
    {
        Funcionario funcionario;
        IRepositorioFuncionario repositorio;

        public RepositorioFuncionarioEmOrmTest()
        {
            //repositorio = new RepositorioFuncionarioEmOrm();
        }

        [TestMethod]
        public void Inserir_Funcionario()
        {
            ////arrange
            //funcionario = new Funcionario("Eduardo Moreira", 2.500, new(2023/2/6));
            //repositorio.Inserir(funcionario);

            ////assert
            //var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.id);

            //Assert.IsNotNull(funcionarioEncontrado);
            //Assert.AreEqual(funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Editar_Informacoes_Funcionario()
        {
            ////arrange
            //funcionario = new Funcionario("Eduardo Moreira", 2.500, new(2023,2,6));
            //repositorio.Inserir(funcionario);

            ////action
            //funcionario.nome = "Tiago Oliveira";
            //funcionario.salario = 2.000;
            //funcionario.dataAdmissao = new(2023,2,4);

            //repositorio.Editar(funcionario);

            ////assert
            //var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.id);
            //Assert.IsNull(funcionarioEncontrado);
            //Assert.AreEqual(funcionario, funcionarioEncontrado);
        }
    }
}
