﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace LocadoraDeVeiculos.Infra.Orm.Compartilhado
{
    internal class LocadoraDeVeiculosDesignFactory : IDesignTimeDbContextFactory<LocadoraDeVeiculosDbContext>
    {
        public LocadoraDeVeiculosDbContext CreateDbContext(string[] args)
        {
            var configuracao = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var optionsBuilder = new DbContextOptionsBuilder<LocadoraDeVeiculosDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new LocadoraDeVeiculosDbContext(optionsBuilder.Options);
        }
    }
}