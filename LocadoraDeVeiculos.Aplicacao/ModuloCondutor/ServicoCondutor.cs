﻿ using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor
{
    public class ServicoCondutor
    {
        private IRepositorioCondutor repositorioCondutor;
        private IValidadorCondutor validadorCondutor;

        public ServicoCondutor(IRepositorioCondutor repositorioCondutor, IValidadorCondutor validadorCondutor)
        {
            this.repositorioCondutor = repositorioCondutor;
            this.validadorCondutor = validadorCondutor;
        }

        public Result Inserir(Condutor condutor)
        {
            Log.Debug("Tentando inserir um condutor...{@d}", condutor);

            List<string> erros = ValidarCondutor(condutor);

            if (erros.Count() > 0)
                return Result.Fail(erros);

            try
            {
                repositorioCondutor.Inserir(condutor);

                Log.Debug("Condutor {CondutorId} inserido com sucesso", condutor.id);

                return Result.Ok(); 
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar inserir um condutor.";

                Log.Error(exc, msgErro + "{@d}", condutor);

                return Result.Fail(msgErro); 
            }
        }

        public Result Editar(Condutor condutor)
        {
            Log.Debug("Tentando editar um condutor...{@d}", condutor);

            List<string> erros = ValidarCondutor(condutor);

            if (erros.Count() > 0)
                return Result.Fail(erros);
            try
            {
                repositorioCondutor.Editar(condutor);

                Log.Debug("Condutor {condutorId} editado com sucesso", condutor.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                string msgErro = "Falha ao tentar editar um condutor.";

                Log.Error(exc, msgErro + "{@d}", condutor);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(Condutor condutor)
        {
            Log.Debug("Tentando excluir um condutor...{@d}", condutor);

            try
            {
                bool condutorExiste = repositorioCondutor.Existe(condutor);

                if (condutorExiste == false)
                {
                    Log.Debug("Condutor {condutorId} não encontrado para excluir", condutor.id);

                    return Result.Fail("Condutor não encontrado.");
                }

                repositorioCondutor.Excluir(condutor);

                Log.Debug("Condutor {condutorId} excluido com sucesso", condutor.id);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                List<string> erros = new();

                string msgErro;

                if (exc.Message.Contains("FK_TBAluguel_TBCondutor"))
                    msgErro = "Este condutor está relacionado a um aluguel e não pode ser excluido";
                else
                    msgErro = "Falha ao tentar excluir um condutor.";

                erros.Add(msgErro);

                Log.Error(exc, msgErro + "{CondutorId}", condutor.id);

                return Result.Fail(msgErro);
            }
        }

        private List<string> ValidarCondutor(Condutor condutor)
        {
            var resultadoValidacao = validadorCondutor.Validate(condutor);

            List<string> erros = new List<string>();

            if (resultadoValidacao != null)
                erros.AddRange(resultadoValidacao.Errors.Select(x => x.ErrorMessage));

            if (NomeDuplicado(condutor))
                erros.Add($"Este cliente já possuí um condutor cadastrado com esse nome '{condutor.nome}'");

            foreach (string erro in erros)
            {
                Log.Warning(erro);
            }

            return erros;
        }

        private bool NomeDuplicado(Condutor condutor)
        {
            Condutor condutorEncontrado = repositorioCondutor.SelecionarPorNome(condutor.nome);

            if (condutorEncontrado != null &&
                condutorEncontrado.id != condutor.id &&
                condutorEncontrado.nome == condutor.cliente.nome)
                return true;

            return false;
        }
    }
}
