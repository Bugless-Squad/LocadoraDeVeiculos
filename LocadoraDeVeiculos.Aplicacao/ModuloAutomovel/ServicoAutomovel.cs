using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel
{
    public class ServicoAutomovel
    {
        private readonly IRepositorioAutomovel repositorioAutomovel;
        private readonly IValidadorAutomovel validadorAutomovel;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoAutomovel(IRepositorioAutomovel repositorioAutomovel, 
            IValidadorAutomovel validadorAutomovel, 
            IContextoPersistencia contextoPersistencia)
        {
            this.repositorioAutomovel = repositorioAutomovel;
            this.validadorAutomovel = validadorAutomovel;
            this.contextoPersistencia = contextoPersistencia;
        }

        public Result Inserir(Automovel automovel)
        {
            Log.Debug("Tentando inserir um automovel...{@d}", automovel);

            List<string> erros = ValidarAutomovel(automovel);

            if (erros.Count() > 0) //cenário 2
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros); 
            }
            try
            {
                repositorioAutomovel.Inserir(automovel);

                contextoPersistencia.GravarDados();

                Log.Debug("GrupoAutomovel {AutomovelId} inserido com sucesso", automovel.id);

                return Result.Ok(); //cenário 1
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar inserir um automovel.";

                Log.Error(exc, msgErro + "{@d}", automovel);

                return Result.Fail(msgErro); //cenário 3
            }
        }
        
        public Result Editar(Automovel automovel)
        {
            Log.Debug("Tentando editar um automovel...{@d}", automovel);

            List<string> erros = ValidarAutomovel(automovel);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }

            try
            {
                repositorioAutomovel.Editar(automovel);

                contextoPersistencia.GravarDados();

                Log.Debug("GrupoAutomovel {AutomovelId} editado com sucesso", automovel.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar editar um automovel.";

                Log.Error(exc, msgErro + "{@d}", automovel);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(Automovel automovel)
        {
            Log.Debug("Tentando excluir um automovel...{@d}", automovel);

            try
            {
                bool automovelExiste = repositorioAutomovel.Existe(automovel);

                if (automovelExiste == false)
                {
                    Log.Warning("GrupoAutomovel {AutomovelId} não encontrado para excluir", automovel.id);

                    return Result.Fail("GrupoAutomovel não encontrado");
                }

                repositorioAutomovel.Excluir(automovel);

                contextoPersistencia.GravarDados();

                Log.Debug("GrupoAutomovel {AutomovelId} excluído com sucesso", automovel.id);

                return Result.Ok();
            }
            catch (SqlException ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                List<string> erros = new List<string>();
              
                string msgErro;

                if (ex.Message.Contains("FK_TBAluguel_TBAutomovel"))
                    msgErro = "Este automovel está relacionado a um aluguel e não pode ser excluído";
                else
                    msgErro = "Falha ao tentar excluir um automovel";

                erros.Add(msgErro);

                Log.Error(ex, msgErro + " {AutomovelId}", automovel.id);

                return Result.Fail(erros);
            }
        }

        private List<string> ValidarAutomovel(Automovel automovel)
        {
            var resultadoValidacao = validadorAutomovel.Validate(automovel);

            List<string> erros = new List<string>();

            if (resultadoValidacao != null)
                erros.AddRange(resultadoValidacao.Errors.Select(x => x.ErrorMessage));

            foreach (string erro in erros)
            {
                Log.Warning(erro);
            }

            return erros;
        }

        //private bool NomeDuplicado(GrupoAutomovel automovel)
        //{
        //    GrupoAutomovel automovelEncontrado = repositorioAutomovel.SelecionarPorNome(automovel.nome);

        //    if (automovelEncontrado != null &&
        //        automovelEncontrado.id != automovel.id &&
        //        automovelEncontrado.nome == automovel.nome)
        //    return true;

        //    return false;
        //}
    }
}
