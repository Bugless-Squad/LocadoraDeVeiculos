﻿using LocadoraDeVeiculos.Dominio.ModuloAluguel;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloAluguel
{
    public class MapeadorAluguelOrm : IEntityTypeConfiguration<Aluguel>
    {
        public void Configure(EntityTypeBuilder<Aluguel> builder)
        {
            builder.ToTable("TBAluguel");

            builder.Property(d => d.id).IsRequired().ValueGeneratedNever();

            builder.Property(d => d.dataPrevistaDevolucao).IsRequired();

            builder.Property(d => d.valorTotalPrevisto).HasConversion<decimal>().HasPrecision(25,2).IsRequired();

            builder.Property(d => d.valorTotalFinal).HasConversion<decimal>().HasPrecision(25,2).IsRequired(false);

            builder.Property(d => d.dataDevolucao).IsRequired(false);

            builder.Property(d => d.nivelTanque).HasConversion<int>().IsRequired(); 

            builder.Property(d => d.dataLocacao).IsRequired();

            builder.Property(d => d.kmPercorrido).HasConversion<decimal>().HasPrecision(25,2).IsRequired(false);

            builder.Property(d => d.kmInicial).HasConversion<decimal>().HasPrecision(25,2).IsRequired();

            builder.Property(d => d.kmFinal).HasConversion<decimal>().HasPrecision(25,2).IsRequired(false);

            builder.HasOne(m => m.grupoAutomovel)
                   .WithMany()
                   .IsRequired()
                   .HasConstraintName("FK_TBAluguel_TBGrupoAutomovel")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.funcionario)
                   .WithMany()
                   .IsRequired()
                   .HasConstraintName("FK_TBAluguel_TBFuncionario")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.cliente)
                   .WithMany()
                   .IsRequired()
                   .HasConstraintName("FK_TBAluguel_TBCliente")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.condutor)
                   .WithMany()
                   .IsRequired()
                   .HasConstraintName("FK_TBAluguel_TBCondutor")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.automovel)
                   .WithMany()
                   .IsRequired()
                   .HasConstraintName("FK_TBAluguel_TBAutomovel")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.cupom)
                   .WithMany()
                   .IsRequired(false)
                   .HasConstraintName("FK_TBAluguel_TBCupom")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.planoCobranca)
                   .WithMany()
                   .IsRequired()
                   .HasConstraintName("FK_TBAluguel_TBPlanoCobranca")
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.taxas)
                   .WithMany()
                   .UsingEntity(x => x.ToTable("TBAluguel_TBTaxaEServico"));
        }
    }
}
