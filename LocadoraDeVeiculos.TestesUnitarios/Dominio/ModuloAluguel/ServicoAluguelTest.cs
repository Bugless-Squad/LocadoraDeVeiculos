using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloAluguel
{
    [TestClass]
    public class ServicoAluguelTest
    {
        Mock<IRepositorioAluguel> repositorioAluguelMoq;
        Mock<IValidadorAluguel> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoAluguel servicoAluguel;

        Aluguel aluguel;

        [TestInitialize]
        public void InicializarTeste()
        {
            repositorioAluguelMoq = new Mock<IRepositorioAluguel>();
            validadorMoq = new Mock<IValidadorAluguel>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoAluguel = new ServicoAluguel(repositorioAluguelMoq.Object, validadorMoq.Object, contextoMoq.Object);
            aluguel = new Aluguel();
        }

    }
}
