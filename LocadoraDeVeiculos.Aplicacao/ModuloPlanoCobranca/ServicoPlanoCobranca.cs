using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlanoCobranca
{
    public class ServicoPlanoCobranca
    {
        private IRepositorioPlanoCobranca repositorioPlanoCobranca;
        private IValidadorPlanoCobranca validadorPlanoCobranca;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoPlanoCobranca(IRepositorioPlanoCobranca repositorioPlanoCobranca, IValidadorPlanoCobranca validadorPlanoCobranca, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioPlanoCobranca = repositorioPlanoCobranca;
            this.validadorPlanoCobranca = validadorPlanoCobranca;
            this.contextoPersistencia = contextoPersistencia;
        }

        public Result Inserir(PlanoCobranca registro)
        {
            Log.Debug("Tentando inserir um Plano...{@d} ", registro);

            List<string> erros = validarPlanoCobranca(registro);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }
            try
            {
                //registro.grupoAutomovel.tiposPlano.Add(registro.tipoPlano);

                repositorioPlanoCobranca.Inserir(registro);

                contextoPersistencia.GravarDados();

                Log.Debug("Plano {PlanoCobrancaId} inserido com sucesso.", registro.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar inserir um Plano.";

                Log.Error(msgErro, exc);

                return Result.Fail(msgErro);
            }
        }

        public Result Editar(PlanoCobranca registro)
        {
            Log.Debug("Tentando editar um cupom...{@d}", registro);

            List<string> erros = validarPlanoCobranca(registro);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }
            try
            {
                repositorioPlanoCobranca.Editar(registro);

                contextoPersistencia.GravarDados();

                Log.Debug("Plano {planoCobrancaId} editado com sucesso", registro.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar editar um plano.";

                Log.Error(exc, msgErro + "{@d}", registro);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(PlanoCobranca planoCobrancaSelecionado)
        {
            Log.Debug("Tentando excluir um plano...{@d}", planoCobrancaSelecionado);
            try
            {
                bool PlanoCobrancaExiste = repositorioPlanoCobranca.Existe(planoCobrancaSelecionado);

                if (PlanoCobrancaExiste == false)
                {
                    Log.Warning("Planos {PlanoCobrancaId} não encontrado para excluir", planoCobrancaSelecionado.id);

                    return Result.Fail("Parceiro não encontrado");
                }

                repositorioPlanoCobranca.Excluir(planoCobrancaSelecionado);

                contextoPersistencia.GravarDados();

                Log.Debug("Plano {PlanoCobrancaId} excluido com sucesso.", planoCobrancaSelecionado.id);

                return Result.Ok();

            }
            catch (Exception exc)
            {
                contextoPersistencia.DesfazerAlteracoes();

                List<string> erros = new();

                string msgErro;

                if (exc.Message.Contains("FK_TBGrupoAutomovel_TBPlanoCobranca"))
                    msgErro = "Este plano está relacionado a um grupo de automovel e não pode ser excluido";
                else
                    msgErro = "Falha ao tentar excluir um funcionario.";
                erros.Add(msgErro);

                Log.Error(exc, msgErro + "{PlanoCobrancaId}", planoCobrancaSelecionado.id);

                return Result.Fail(msgErro);
            }
        }

        private List<string> validarPlanoCobranca(PlanoCobranca registro)
        {
            var resultadoValidacao = validadorPlanoCobranca.Validate(registro);

            List<string> erros = new List<string>();

            if (resultadoValidacao != null)
                erros.AddRange(resultadoValidacao.Errors.Select(x => x.ErrorMessage));

            foreach (string erro in erros)
            {
                Log.Warning(erro);
            }

            return erros;

        }

    }
}
