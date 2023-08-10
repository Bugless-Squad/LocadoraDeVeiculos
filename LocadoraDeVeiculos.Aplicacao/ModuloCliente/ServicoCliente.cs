using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente
{
    public class ServicoCliente
    {
        private readonly IRepositorioCliente repositorioCliente;
        private readonly IValidadorCliente validadorCliente;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoCliente(IRepositorioCliente repositorioCliente, IValidadorCliente validadorCliente, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioCliente = repositorioCliente;
            this.validadorCliente = validadorCliente;
            this.contextoPersistencia = contextoPersistencia;
        }

        public Result Inserir(Cliente cliente)
        {
            Log.Debug("Tentando inserir um cliente...{@d}", cliente);

            List<string> erros = ValidarCliente(cliente);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }
            try
            {
                repositorioCliente.Inserir(cliente);
                
                contextoPersistencia.GravarDados();

                Log.Debug("Cliente {ClienteId} inserido com sucesso.", cliente.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar inserir um Cliente.";

                Log.Error(msgErro, exc);

                return Result.Fail(msgErro);
            }

        }

        public Result Editar(Cliente cliente)
        {
            Log.Debug("Tentando editar um cliente..{@d}", cliente);

            List<string> erros = ValidarCliente(cliente);

            if (erros.Count() > 0)
            {
                contextoPersistencia.DesfazerAlteracoes();

                return Result.Fail(erros);
            }

            try
            {
                repositorioCliente.Editar(cliente);

                contextoPersistencia.GravarDados();

                Log.Debug("Cliente {ClienteId} editado com sucesso.", cliente.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string MsgErro = "Falha ao tentar editar um Cliente.";
                Log.Error(MsgErro, exc);
                return Result.Fail(MsgErro);
            }
        }

        public Result Excluir(Cliente cliente)
        {
            Log.Debug("Tentando excluir um cliente...{@d}", cliente);
            try
            {
                bool ClienteExiste = repositorioCliente.Existe(cliente);

                if (ClienteExiste == false)
                {
                    Log.Warning("Cliente {ClienteId} editado com sucesso.", cliente.id);

                    return Result.Fail("Cliente não encontrado");
                }

                repositorioCliente.Excluir(cliente);

                contextoPersistencia.GravarDados();

                Log.Debug("Cliente {ClienteId} excluido com sucesso.", cliente.id);

                return Result.Ok();


            }
            catch (SqlException ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                List<string> erros = new List<string>();
                string MsgErro;

                if (ex.Message.Contains("FK_TBCondutor_TBCliente"))
                    MsgErro = "Este cliente está relacionado a um condutor e não pode ser excluido";
                else
                    MsgErro = "Falha ao tentar excluir um Cliente";

                erros.Add(MsgErro);

                Log.Error(ex, MsgErro + "{ClienteId}", cliente.id);
                return Result.Fail(MsgErro);
            }
        }

        private List<string> ValidarCliente(Cliente cliente)
        {
            var resultadoValidacao = validadorCliente.Validate(cliente);

            List<string> erros = new List<string>();

            if (resultadoValidacao != null)
                erros.AddRange(resultadoValidacao.Errors.Select(x => x.ErrorMessage));

            if (NomeDuplicado(cliente))
                erros.Add($"Este nome '{cliente.nome}' já está sendo utilizado");

            foreach (string erro in erros)
            {
                Log.Warning(erro);
            }

            return erros;
        }

        private bool NomeDuplicado(Cliente cliente)
        {
            Cliente clienteEncontrado = repositorioCliente.SelecionarPorNome(cliente.nome);

            if (clienteEncontrado != null &&
               clienteEncontrado.id != cliente.id &&
               clienteEncontrado.nome == cliente.nome)
            return true;

            return false;
        }
    }
}
