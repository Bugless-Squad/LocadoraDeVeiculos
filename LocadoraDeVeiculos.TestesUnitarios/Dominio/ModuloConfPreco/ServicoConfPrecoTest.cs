using FluentAssertions;
using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloConfigPreco;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloConfigPreco;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloConfPreco
{
    public class ServicoConfPrecoTest
    {
        Mock<IRepositorioConfiguracaoPreco> repositorioConfPrecoMoq;
        Mock<IValidadorConfiguracaoPreco> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;
        private ServicoConfiguracaoPreco configuracaoPreco;

        [TestInitialize]
        public void InicializarTeste()
        {
            repositorioConfPrecoMoq = new Mock<IRepositorioConfiguracaoPreco>();
            validadorMoq = new Mock<IValidadorConfiguracaoPreco>();
            contextoMoq = new Mock<IContextoPersistencia>();
            configuracaoPreco = new ServicoConfiguracaoPreco(repositorioConfPrecoMoq.Object, validadorMoq.Object, contextoMoq.Object);
        }
    }

}
