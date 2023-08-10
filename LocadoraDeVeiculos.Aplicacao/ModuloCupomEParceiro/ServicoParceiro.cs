using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;


namespace LocadoraDeVeiculos.Aplicacao.ModuloCupomEParceiro
{
    public class ServicoParceiro
    {
        private readonly IRepositorioParceiro repositorioParceiro;
        private readonly IValidadorParceiro validadorParceiro;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoParceiro(IRepositorioParceiro repositorioParceiro,
                               IValidadorParceiro validadorParceiro,
                               IContextoPersistencia contextoPersistencia)
        {
            this.repositorioParceiro = repositorioParceiro;
            this.validadorParceiro = validadorParceiro;
            this.contextoPersistencia = contextoPersistencia;
        }

        public Result Inserir(Parceiro parceiro)
        {
            Log.Debug("Tentando inserir um parceiro...{@d}", parceiro);

            List<string> erros = ValidarParceiro(parceiro);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }
            try
            {
                repositorioParceiro.Inserir(parceiro);

                contextoPersistencia.GravarDados();

                Log.Debug("Parceiro {ParceiroId} inserido com sucesso.", parceiro.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar inserir um Parceiro.";

                Log.Error(msgErro, exc);

                return Result.Fail(msgErro);
            }

        }

        public Result Editar(Parceiro parceiro)
        {
            Log.Debug("Tentando editar um parceiro..{@d}", parceiro);

            List<string> erros = ValidarParceiro(parceiro);

            if(erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }

            try
            {
                repositorioParceiro.Editar(parceiro);

                contextoPersistencia.GravarDados();

                Log.Debug("Parceiro {ParceiroId} editado com sucesso.", parceiro.id);

                return Result.Ok();
            }catch (Exception exc)
            {
                string MsgErro = "Falha ao tentar editar um Parceiro.";
                Log.Error(MsgErro, exc);
                return Result.Fail(MsgErro);
            }
        }

        public Result Excluir(Parceiro parceiro)
        {
            Log.Debug("Tentando excluir um parceiro...{@d}", parceiro);

            try
            {
                bool ParceiroExiste = repositorioParceiro.Existe(parceiro);

                if(ParceiroExiste == false) 
                {
                    Log.Warning("Parceiro {ParceiroId} não encontrado para excluir", parceiro.id);

                    return Result.Fail("Parceiro não encontrado");
                }

                repositorioParceiro.Excluir(parceiro);

                contextoPersistencia.GravarDados();

                Log.Debug("Parceiro {ParceiroId} excluido com sucesso.", parceiro.id);

                return Result.Ok();


            }catch(SqlException ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                List<string> erros = new List<string>();
                string MsgErro;

                if (ex.Message.Contains("FK_TBCupom_TBParceiro"))
                    MsgErro = "Este parceiro está relacionado a um cupom e não pode ser excluido";
                else
                    MsgErro = "Falha ao tentar excluir um Parceiro";

                erros.Add(MsgErro);

                Log.Error(ex, MsgErro + "{ParceiroId}", parceiro.id);
                return Result.Fail(MsgErro);
            }
        }


        private List<string> ValidarParceiro(Parceiro parceiro)
        {
            var resultadoValidacao = validadorParceiro.Validate(parceiro);

            List<string> erros = new List<string>();

            if(resultadoValidacao != null)
                erros.AddRange(resultadoValidacao.Errors.Select(x => x.ErrorMessage));

            if (NomeDuplicado(parceiro))
                erros.Add($"Este nome ´{parceiro.nome}´ já está sendo utilizado");

            foreach(string erro in erros)
            {
                Log.Warning(erro);
            }

            return erros;
        }

        private bool NomeDuplicado(Parceiro parceiro)
        {
            Parceiro parceiroEncontrado = repositorioParceiro.SelecionarPorNome(parceiro.nome);

            if(parceiroEncontrado != null &&
               parceiroEncontrado.id != parceiro.id &&
               parceiroEncontrado.nome == parceiro.nome)
            return true; 

            return false;
        }
    }

}
