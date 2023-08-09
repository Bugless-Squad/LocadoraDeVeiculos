using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCupomEParceiro
{
    public class ServicoCupom
    {
        private IRepositorioCupom repositorioCupom;
        private IValidadorCupom validadorCupom;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoCupom(IRepositorioCupom repositorioCupom, IValidadorCupom validadorCupom, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioCupom = repositorioCupom;
            this.validadorCupom = validadorCupom;
            this.contextoPersistencia = contextoPersistencia;
        }

        public Result Inserir(Cupom registro)
        {
            Log.Debug("Tentando inserir um cupom...{@d} ", registro);

            List<string> erros = ValidarCupom(registro);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);

            }
            try
            {
                repositorioCupom.Inserir(registro);

                contextoPersistencia.GravarDados();

                Log.Debug("Cupom {CupomId} inserido com sucesso.", registro.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar inserir um Cupom.";

                Log.Error(msgErro, exc);

                return Result.Fail(msgErro);
            }

        }

        public Result Editar(Cupom registro)
        {
            Log.Debug("Tentando editar um cupom...{@d}", registro);

            List<string> erros = ValidarCupom(registro);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }
            try
            {
                repositorioCupom.Editar(registro);

                contextoPersistencia.GravarDados();

                Log.Debug("Cupom {cupomId} editado com sucesso", registro.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar editar um cupom.";

                Log.Error(exc, msgErro + "{@d}", registro);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(Cupom cupomSelecionado)
        {
            Log.Debug("Tentando excluir um parceiro...{@d}", cupomSelecionado);
            try
            {
                bool CupomExiste = repositorioCupom.Existe(cupomSelecionado);

                if (CupomExiste == false)
                {
                    Log.Warning("Cupom {CupomId} não encontrado para excluir", cupomSelecionado.id);

                    return Result.Fail("Parceiro não encontrado");
                }

                repositorioCupom.Excluir(cupomSelecionado);

                contextoPersistencia.GravarDados();

                Log.Debug("Cupom {CupomId} excluido com sucesso.", cupomSelecionado.id);

                return Result.Ok();

            }
            catch (SqlException ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                List<string> erros = new List<string>();
                string MsgErro;

                if (cupomSelecionado.validade >= DateTime.Now)
                    MsgErro = "Este cupom não pode ser excluido pois ainda está valido";
                else
                    MsgErro = "Falha ao tentar excluir um Cupom";

                erros.Add(MsgErro);

                Log.Error(ex, MsgErro + "{CupomId}", cupomSelecionado.id);
                return Result.Fail(MsgErro);
            }
        }

        private List<string> ValidarCupom(Cupom cupom)
        {
            var resultadoValidacao = validadorCupom.Validate(cupom);

            List<string> erros = new List<string>();

            if (resultadoValidacao != null)
                erros.AddRange(resultadoValidacao.Errors.Select(x => x.ErrorMessage));

            if (NomeDuplicado(cupom))
                erros.Add($"Este nome ´{cupom.nome}´ já está sendo utilizado");

            foreach (string erro in erros)
            {
                Log.Warning(erro);
            }

            return erros;

        }

        private bool NomeDuplicado(Cupom cupom)
        {
            Cupom cupomEncontrado = repositorioCupom.SelecionarPorNome(cupom.nome);

            if (cupomEncontrado != null &&
               cupomEncontrado.id != cupom.id &&
               cupomEncontrado.nome == cupom.nome)
                return true;

            return false;
        }
    }
}
