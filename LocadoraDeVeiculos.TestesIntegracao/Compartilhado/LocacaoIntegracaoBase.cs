using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloTaxaEServico;
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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculos.TestesIntegracao.Compartilhado
{
    public class LocacaoIntegracaoBase
    {
        public IRepositorioFuncionario repositorioFuncionario;
        public IRepositorioCliente repositorioCliente;
        public IRepositorioAutomovel repositorioAutomovel;
        public IRepositorioCondutor repositorioCondutor;
        public IRepositorioCupom repositorioCupom;
        public IRepositorioParceiro repositorioParceiro;
        public IRepositorioGrupoAutomovel repositorioGrupoAutomovel;
        public IRepositorioPlanoCobranca repositorioPlanoCobranca;
        public IRepositorioTaxaEServico repositorioTaxaEServico;
        //public IRepositorioAluguel repositorioAluguel;

        public LocacaoIntegracaoBase()
        {
            LimparTabelas();

            string connectionString = ObterConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<LocadoraDeVeiculosDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            var dbContext = new LocadoraDeVeiculosDbContext(optionsBuilder.Options);

            repositorioFuncionario = new RepositorioFuncionarioEmOrm(dbContext);
            repositorioCliente = new RepositorioClienteEmOrm(dbContext);
            repositorioAutomovel = new RepositorioAutomovelEmOrm(dbContext);
            repositorioCondutor = new RepositorioCondutorEmOrm(dbContext);
            repositorioCupom = new RepositorioCupomEmOrm(dbContext);
         //   repositorioParceiro = RepositorioParceiroEmOrm(dbContext);
            repositorioGrupoAutomovel = new RepositorioGrupoAutomovelEmOrm(dbContext);
            repositorioPlanoCobranca = new RepositorioPlanoCobrancaEmOrm(dbContext);
            repositorioTaxaEServico = new RepositorioTaxaEServicoEmOrm(dbContext);
            // repositorioAluguel = new RepositorioAluguelEmOrm 


            BuilderSetup.SetCreatePersistenceMethod<Funcionario>(repositorioFuncionario.Inserir);
            BuilderSetup.SetCreatePersistenceMethod<Cliente>(repositorioCliente.Inserir);
            BuilderSetup.SetCreatePersistenceMethod<Automovel>(repositorioAutomovel.Inserir);
            BuilderSetup.SetCreatePersistenceMethod<Condutor>(repositorioCondutor.Inserir);
            BuilderSetup.SetCreatePersistenceMethod<Cupom>(repositorioCupom.Inserir);
         //   BuilderSetup.SetCreatePersistenceMethod<Parceiro>(repositorioParceiro.Inserir);
            BuilderSetup.SetCreatePersistenceMethod<GrupoAutomovel>(repositorioGrupoAutomovel.Inserir);
            BuilderSetup.SetCreatePersistenceMethod<PlanoCobranca>(repositorioPlanoCobranca.Inserir);
            BuilderSetup.SetCreatePersistenceMethod<TaxaEServico>(repositorioTaxaEServico.Inserir);
           // BuilderSetup.SetCreatePersistenceMethod<Aluguel>(repositorioAluguel.Inserir);
        }

        protected static void LimparTabelas()
        {
            string? connectionString = ObterConnectionString();

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string sqlLimpezaTabela =
                @"
                DELETE FROM [DBO].[TBCLIENTE];
                DELETE FROM [DBO].[TBFUNCIONARIO];                
                DELETE FROM [DBO].[TBAUTOMOVEL];                
                DELETE FROM [DBO].[TBCONDUTOR];                
                DELETE FROM [DBO].[TBCUPOM];                
                DELETE FROM [DBO].[TBPARCEIRO];                
                DELETE FROM [DBO].[TBGRUPOAUTOMOVEL];                
                DELETE FROM [DBO].[TBPLANOCOBRANCA];                
                DELETE FROM [DBO].[TBTAXAESERVICO];                
                DELETE FROM [DBO].[TBALUGUEL];";

            SqlCommand comando = new SqlCommand(sqlLimpezaTabela, sqlConnection);

            sqlConnection.Open();

            comando.ExecuteNonQuery();

            sqlConnection.Close();
        }

        protected static string ObterConnectionString()
        {
            var configuracao = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");
            return connectionString;
        }
    }
}
