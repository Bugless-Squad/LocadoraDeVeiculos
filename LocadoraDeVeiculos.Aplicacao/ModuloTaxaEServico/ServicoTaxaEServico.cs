using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloTaxaEServico;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxaEServico
{
    public class ServicoTaxaEServico
    {
        private IRepositorioTaxaEServico repositorioTaxaEServico;
        private IValidadorTaxaEServico validadorTaxaEServico;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoTaxaEServico(IRepositorioTaxaEServico repositorioTaxaEServico, IValidadorTaxaEServico validadorTaxaEServico, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioTaxaEServico = repositorioTaxaEServico;
            this.validadorTaxaEServico = validadorTaxaEServico;
            this.contextoPersistencia = contextoPersistencia;
        }

        public Result Inserir(TaxaEServico taxaEServico)
        {
            Log.Debug("Tentando inserir uma taxa ou serviço...{@d}", taxaEServico);

            List<string> erros = ValidarTaxaEServico(taxaEServico);

            if (erros.Count() > 0)

                contextoPersistencia.DesfazerAlteracoes();

            return Result.Fail(erros); //cenário 2

            try
            {
                repositorioTaxaEServico.Inserir(taxaEServico);

                contextoPersistencia.GravarDados();

                Log.Debug("Taxa/Servico {TaxaEServicoId} inserido com sucesso", taxaEServico.id);

                return Result.Ok(); //cenário 1
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar inserir uma taxa ou serviço.";

                Log.Error(exc, msgErro + "{@d}", taxaEServico);

                return Result.Fail(msgErro); //cenário 3
            }
        }
        
        public Result Editar(TaxaEServico taxaEServico)
        {
            Log.Debug("Tentando editar uma taxa ou serviço...{@d}", taxaEServico);

            List<string> erros = ValidarTaxaEServico(taxaEServico);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }
            try
            {
                repositorioTaxaEServico.Editar(taxaEServico);

                contextoPersistencia.GravarDados();

                Log.Debug("Taxa/Servico {TaxaEServicoId} editado com sucesso", taxaEServico.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar editar uma taxa ou serviço.";

                Log.Error(exc, msgErro + "{@d}", taxaEServico);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(TaxaEServico taxaEServico)
        {
            Log.Debug("Tentando excluir uma taxa ou serviço...{@d}", taxaEServico);

            try
            {
                bool taxaEServicoExiste = repositorioTaxaEServico.Existe(taxaEServico);

                if (taxaEServicoExiste == false)
                {
                    Log.Warning("Taxa/Servico {TaxaEServicoId} não encontrado para excluir", taxaEServico.id);

                    return Result.Fail("Taxa/Servico não encontrado");
                }

                repositorioTaxaEServico.Excluir(taxaEServico);

                contextoPersistencia.GravarDados();

                Log.Debug("Taxa/Servico {TaxaEServicoId} excluído com sucesso", taxaEServico.id);

                return Result.Ok();
            }
            catch (SqlException ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                List<string> erros = new List<string>();
              
                string msgErro;

                if (ex.Message.Contains("FK_TBAluguel_TBTaxaEServico"))
                    msgErro = "Esta taxa/servico está relacionado a um aluguel e não pode ser excluída.";
                else
                    msgErro = "Falha ao tentar excluir uma taxa ou serviço.";

                erros.Add(msgErro);

                Log.Error(ex, msgErro + " {TaxaEServicoId}", taxaEServico.id);

                return Result.Fail(erros);
            }
        }

        private List<string> ValidarTaxaEServico(TaxaEServico taxaEServico)
        {
            var resultadoValidacao = validadorTaxaEServico.Validate(taxaEServico);

            List<string> erros = new List<string>();

            if (resultadoValidacao != null)
                erros.AddRange(resultadoValidacao.Errors.Select(x => x.ErrorMessage));

            if (NomeDuplicado(taxaEServico))
                erros.Add($"Este nome '{taxaEServico.nome}' já está sendo utilizado");

            foreach (string erro in erros)
            {
                Log.Warning(erro);
            }

            return erros;
        }

        private bool NomeDuplicado(TaxaEServico taxaEServico)
        {
            TaxaEServico taxaEServicoEncontrado = repositorioTaxaEServico.SelecionarPorNome(taxaEServico.nome);

            if (taxaEServicoEncontrado != null &&
                taxaEServicoEncontrado.id != taxaEServico.id &&
                taxaEServicoEncontrado.nome == taxaEServico.nome)
            return true;

            return false;
        }
    }
}
