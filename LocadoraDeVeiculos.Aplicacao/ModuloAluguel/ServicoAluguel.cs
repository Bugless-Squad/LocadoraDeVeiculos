﻿using LocadoraDeVeiculos.Dominio.ModuloAluguel;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel
{
    public class ServicoAluguel
    {
        private IRepositorioAluguel repositorioAluguel;
        private IValidadorAluguel validadorAluguel;

        public ServicoAluguel(IRepositorioAluguel repositorioAluguel, IValidadorAluguel validadorAluguel)
        {
            this.repositorioAluguel = repositorioAluguel;
            this.validadorAluguel = validadorAluguel;
        }

        //public Result Inserir(Aluguel aluguel)
        //{
        //    Log.Debug("Tentando inserir aluguel...{@d}", aluguel);

        //    //.....
        //}
        
        //public Result Editar(Aluguel aluguel)
        //{
        //    Log.Debug("Tentando editar aluguel...{@d}", aluguel);
            
        //    //.........
        //}

        //public Result Excluir(Aluguel aluguel)
        //{
        //    Log.Debug("Tentando excluir aluguel...{@d}", aluguel);

        //    try
        //    {
        //        //............

        //        return Result.Ok();
        //    }
        //    catch (SqlException ex)
        //    {
        //        //..........

        //        //return Result.Fail(erros);
        //    }
        //}

        //private List<string> ValidarAluguel(Aluguel aluguel)
        //{
        //  //..............
        //}
    }
}