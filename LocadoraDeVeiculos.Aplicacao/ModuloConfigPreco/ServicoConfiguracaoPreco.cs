using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloConfigPreco;

namespace LocadoraDeVeiculos.Aplicacao.ModuloConfigPreco
{
    public class ServicoConfiguracaoPreco
    {
        private IRepositorioConfiguracaoPreco repositorioConfiguracaoPreco;
        private IValidadorConfiguracaoPreco validadorConfiguracaoPreco;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoConfiguracaoPreco(IRepositorioConfiguracaoPreco repositorioConfiguracaoPreco, IValidadorConfiguracaoPreco validadorConfiguracaoPreco, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioConfiguracaoPreco = repositorioConfiguracaoPreco;
            this.validadorConfiguracaoPreco = validadorConfiguracaoPreco;
            this.contextoPersistencia = contextoPersistencia;
        }

        public Result Configurar(ConfiguracaoPreco configuracaoPreco)
        {
            Log.Debug("Tentando configurar preços...{@d}", configuracaoPreco);

            List<string> erros = ValidarConfiguracaoPreco(configuracaoPreco);

            repositorioConfiguracaoPreco.ObterConfiguracaoPreco();

            if (erros.Count() > 0)
                return Result.Fail(erros); //cenário 2

            try
            {
                repositorioConfiguracaoPreco.GravarConfiguracaoPrecoEmArquivoJson(configuracaoPreco);

                Log.Debug("Preços atualizados com sucesso");

                return Result.Ok(); //cenário 1
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar atualizar as configurações.";

                Log.Error(exc, msgErro + "{@d}", configuracaoPreco);

                return Result.Fail(msgErro); //cenário 3
            }
        }
        
        private List<string> ValidarConfiguracaoPreco(ConfiguracaoPreco configuracaoPreco)
        {
            var resultadoValidacao = validadorConfiguracaoPreco.Validate(configuracaoPreco);

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
