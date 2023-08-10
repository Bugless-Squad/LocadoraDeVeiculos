using FizzWare.NBuilder;
using FluentAssertions;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculos.TestesIntegracao.ModuloCupomEParceiro
{
    public class RepositorioParceiroEmOrmTest : LocacaoIntegracaoBase
    {
        [TestMethod]
        public void Deve_inserir_Parceiro()
        {
            //arrange
            var parceiro = Builder<Parceiro>.CreateNew().Build();
            //action
            repositorioParceiro.Inserir(parceiro);

            //assert
            repositorioParceiro.SelecionarPorId(parceiro.id).Should().Be(parceiro);
        }
        public void Deve_editar_Parceiro()
        {
            //arrange
            var parceiroId = Builder<Parceiro>.CreateNew().Persist().id;

            var parceiro = repositorioParceiro.SelecionarPorId(parceiroId);
            parceiro.nome = "Influencers";

            //action
            repositorioParceiro.Editar(parceiro);

            //assert
            repositorioParceiro.SelecionarPorId(parceiro.id)
                .Should().Be(parceiro);
        }

    }
}
