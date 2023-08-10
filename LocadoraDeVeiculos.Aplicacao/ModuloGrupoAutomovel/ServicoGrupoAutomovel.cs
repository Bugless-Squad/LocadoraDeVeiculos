using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel
{
    public class ServicoGrupoAutomovel
    {
        private IRepositorioGrupoAutomovel repositorioGrupoAutomovel;
        private IValidadorGrupoAutomovel validadorGrupoAutomovel;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoGrupoAutomovel(IRepositorioGrupoAutomovel repositorioGrupoAutomovel, IValidadorGrupoAutomovel validadorGrupoAutomovel, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioGrupoAutomovel = repositorioGrupoAutomovel;
            this.validadorGrupoAutomovel = validadorGrupoAutomovel;
            this.contextoPersistencia = contextoPersistencia;
        }

        public Result Inserir(GrupoAutomovel grupoAutomovel)
        {
            Log.Debug("Tentando inserir um grupo de automovel...{@d}", grupoAutomovel);

            List<string> erros = ValidarGrupoAutomovel(grupoAutomovel);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }
            try
            {
                repositorioGrupoAutomovel.Inserir(grupoAutomovel);

                contextoPersistencia.GravarDados();

                Log.Debug("Grupo de automovel {GrupoAutomovelId} inserido com sucesso", grupoAutomovel.id);

                return Result.Ok(); //cenário 1
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar inserir um grupo de automovel.";

                Log.Error(exc, msgErro + "{@d}", grupoAutomovel);

                return Result.Fail(msgErro); //cenário 3
            }
        }
        
        public Result Editar(GrupoAutomovel grupoAutomovel)
        {
            Log.Debug("Tentando editar um grupo de automovel...{@d}", grupoAutomovel);

            List<string> erros = ValidarGrupoAutomovel(grupoAutomovel);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }
            try
            {
                repositorioGrupoAutomovel.Editar(grupoAutomovel);

                contextoPersistencia.GravarDados();

                Log.Debug("Grupo de automovel {GrupoAutomovelId} editado com sucesso", grupoAutomovel.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar editar um grupo de automovel.";

                Log.Error(exc, msgErro + "{@d}", grupoAutomovel);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(GrupoAutomovel grupoAutomovel)
        {
            Log.Debug("Tentando excluir um grupo de automovel...{@d}", grupoAutomovel);

            try
            {
                bool grupoAutomovelExiste = repositorioGrupoAutomovel.Existe(grupoAutomovel);

                if (grupoAutomovelExiste == false)
                {
                    Log.Warning("Grupo de automovel {GrupoAutomovelId} não encontrado para excluir", grupoAutomovel.id);

                    return Result.Fail("Grupo de automovel não encontrado");
                }

                repositorioGrupoAutomovel.Excluir(grupoAutomovel);

                contextoPersistencia.GravarDados();

                Log.Debug("Grupo de automovel {GrupoAutomovelId} excluído com sucesso", grupoAutomovel.id);

                return Result.Ok();
            }
            catch (SqlException ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                List<string> erros = new List<string>();
              
                string msgErro;

                if (ex.Message.Contains("FK_TBAutomovel_TBGrupoAutomovel"))
                    msgErro = "Este grupo de automovel está relacionado a um automovel e não pode ser excluído";
                else
                    msgErro = "Falha ao tentar excluir um grupo de automovel";

                erros.Add(msgErro);

                Log.Error(ex, msgErro + " {GrupoAutomovelId}", grupoAutomovel.id);

                return Result.Fail(erros);
            }
        }

        private List<string> ValidarGrupoAutomovel(GrupoAutomovel grupoAutomovel)
        {
            var resultadoValidacao = validadorGrupoAutomovel.Validate(grupoAutomovel);

            List<string> erros = new List<string>();

            if (resultadoValidacao != null)
                erros.AddRange(resultadoValidacao.Errors.Select(x => x.ErrorMessage));

            if (NomeDuplicado(grupoAutomovel))
                erros.Add($"Este nome '{grupoAutomovel.nome}' já está sendo utilizado");

            foreach (string erro in erros)
            {
                Log.Warning(erro);
            }

            return erros;
        }

        private bool NomeDuplicado(GrupoAutomovel grupoAutomovel)
        {
            GrupoAutomovel grupoAutomovelEncontrado = repositorioGrupoAutomovel.SelecionarPorNome(grupoAutomovel.nome);

            if (grupoAutomovelEncontrado != null &&
                grupoAutomovelEncontrado.id != grupoAutomovel.id &&
                grupoAutomovelEncontrado.nome == grupoAutomovel.nome)
            return true;

            return false;
        }
    }
}
