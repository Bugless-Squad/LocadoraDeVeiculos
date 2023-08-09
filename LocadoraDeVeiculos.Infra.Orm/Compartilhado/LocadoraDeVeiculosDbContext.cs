using LocadoraDeVeiculos.Dominio.Compartilhado;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;

namespace LocadoraDeVeiculos.Infra.Orm.Compartilhado
{
    public class LocadoraDeVeiculosDbContext : DbContext, IContextoPersistencia
    {
        public LocadoraDeVeiculosDbContext(DbContextOptions options) : base(options)
        {

        }

        public void DesfazerAlteracoes()
        {
            var registros = ChangeTracker.Entries().Where(x=> x.State != EntityState.Unchanged);

            foreach(var registro in registros) 
            {
                switch (registro.State)
                {
                    case EntityState.Deleted:
                        {
                            registro.State = EntityState.Unchanged;
                        }
                        break;
                    case EntityState.Modified:
                        {
                            registro.State = EntityState.Unchanged;
                            registro.CurrentValues.SetValues(registro.OriginalValues);
                        }
                        break;
                    case EntityState.Added:
                        {
                            registro.State = EntityState.Detached;
                        }
                        break;
                }
            }
        }

        public void GravarDados()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create((x) =>
            {
                x.AddSerilog(Log.Logger); //instalar o pacote Serilog.Extensions.Logging
            });

            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assembly = typeof(LocadoraDeVeiculosDbContext).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}