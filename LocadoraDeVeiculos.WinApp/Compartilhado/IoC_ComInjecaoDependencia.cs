using LocadoraDeVeiculos.Aplicacao.ModuloAluguel;
using LocadoraDeVeiculos.Aplicacao.ModuloAutomovel;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor;
using LocadoraDeVeiculos.Aplicacao.ModuloConfigPreco;
using LocadoraDeVeiculos.Aplicacao.ModuloCupomEParceiro;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Aplicacao.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxaEServico;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloConfigPreco;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloTaxaEServico;
using LocadoraDeVeiculos.Infra.Json.ModuloConfigPreco;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloAluguel;
using LocadoraDeVeiculos.Infra.Orm.ModuloAutomovel;
using LocadoraDeVeiculos.Infra.Orm.ModuloCliente;
using LocadoraDeVeiculos.Infra.Orm.ModuloCondutor;
using LocadoraDeVeiculos.Infra.Orm.ModuloConfigPreco;
using LocadoraDeVeiculos.Infra.Orm.ModuloCupomEParceiro;
using LocadoraDeVeiculos.Infra.Orm.ModuloFuncionario;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Infra.Orm.ModuloTaxaEServico;
using LocadoraDeVeiculos.WinApp.ModuloAluguel;
using LocadoraDeVeiculos.WinApp.ModuloAutomovel;
using LocadoraDeVeiculos.WinApp.ModuloCliente;
using LocadoraDeVeiculos.WinApp.ModuloCondutor;
using LocadoraDeVeiculos.WinApp.ModuloConfigPreco;
using LocadoraDeVeiculos.WinApp.ModuloCupomEParceiro;
using LocadoraDeVeiculos.WinApp.ModuloFuncionario;
using LocadoraDeVeiculos.WinApp.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.WinApp.ModuloPlanoCobranca;
using LocadoraDeVeiculos.WinApp.ModuloTaxaEServico;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace LocadoraDeVeiculos.WinApp.Compartilhado
{
    internal class IoC_ComInjecaoDependencia : IoC
    {
        private ServiceProvider container;

        public IoC_ComInjecaoDependencia()
        {
            var configuracao = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var servicos = new ServiceCollection();

            servicos.AddDbContext<IContextoPersistencia, LocadoraDeVeiculosDbContext>(optionsBuilder => {
                optionsBuilder.UseSqlServer(connectionString);
            });

            servicos.AddSingleton<IRepositorioConfiguracaoPreco>(j => new RepositorioConfigPrecoEmJson(true));
            servicos.AddTransient<ControladorConfigPreco>();
            servicos.AddTransient<ServicoConfiguracaoPreco>();
            servicos.AddTransient<IValidadorConfiguracaoPreco, ValidadorConfiguracaoPreco>();

            servicos.AddTransient<ControladorFuncionario>();
            servicos.AddTransient<ServicoFuncionario>();
            servicos.AddTransient<IValidadorFuncionario, ValidadorFuncionario>();
            servicos.AddTransient<IRepositorioFuncionario, RepositorioFuncionarioEmOrm>();

            servicos.AddTransient<ControladorGrupoAutomovel>();
            servicos.AddTransient<ServicoGrupoAutomovel>();
            servicos.AddTransient<IValidadorGrupoAutomovel, ValidadorGrupoAutomovel>();
            servicos.AddTransient<IRepositorioGrupoAutomovel, RepositorioGrupoAutomovelEmOrm>();

            servicos.AddTransient<ControladorPlanoCobranca>();
            servicos.AddTransient<ServicoPlanoCobranca>();
            servicos.AddTransient<IValidadorPlanoCobranca, ValidadorPlanoCobranca>();
            servicos.AddTransient<IRepositorioPlanoCobranca, RepositorioPlanoCobrancaEmOrm>();

            servicos.AddTransient<ControladorAutomovel>();
            servicos.AddTransient<ServicoAutomovel>();
            servicos.AddTransient<IValidadorAutomovel, ValidadorAutomovel>();
            servicos.AddTransient<IRepositorioAutomovel, RepositorioAutomovelEmOrm>();

            servicos.AddTransient<ControladorCliente>();
            servicos.AddTransient<ServicoCliente>();
            servicos.AddTransient<IValidadorCliente, ValidadorCliente>();
            servicos.AddTransient<IRepositorioCliente, RepositorioClienteEmOrm>();

            servicos.AddTransient<ControladorCondutor>();
            servicos.AddTransient<ServicoCondutor>();
            servicos.AddTransient<IValidadorCondutor, ValidadorCondutor>();
            servicos.AddTransient<IRepositorioCondutor, RepositorioCondutorEmOrm>();

            servicos.AddTransient<ControladorTaxaEServico>();
            servicos.AddTransient<ServicoTaxaEServico>();
            servicos.AddTransient<IValidadorTaxaEServico, ValidadorTaxaEServico>();
            servicos.AddTransient<IRepositorioTaxaEServico, RepositorioTaxaEServicoEmOrm>();

            servicos.AddTransient<ControladorParceiro>();
            servicos.AddTransient<ServicoParceiro>();
            servicos.AddTransient<IValidadorParceiro, ValidadorParceiro>();
            servicos.AddTransient<IRepositorioParceiro, RepositorioParceiroEmOrm>();

            servicos.AddTransient<ControladorCupom>();
            servicos.AddTransient<ServicoCupom>();
            servicos.AddTransient<IValidadorCupom, ValidadorCupom>();
            servicos.AddTransient<IRepositorioCupom, RepositorioCupomEmOrm>();

            servicos.AddTransient<IRepositorioAluguel, RepositorioAluguelEmOrm>();
            servicos.AddTransient<ValidadorAluguel>();
            //servicos.AddTransient < IGerado0 rArquivo, GeradorAluguelEmPdf > ();
            servicos.AddTransient<ServicoAluguel>();
            servicos.AddTransient<ControladorAluguel>();


            container = servicos.BuildServiceProvider();
        }
        public T Get<T>()
        {
            return container.GetService<T>();
        }
    }
}
