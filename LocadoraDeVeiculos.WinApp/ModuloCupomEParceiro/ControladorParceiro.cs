using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloCupomEParceiro;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.WinApp.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.WinApp.ModuloCupomEParceiro
{
    public class ControladorParceiro : ControladorBase
    {
        private TabelaParceiroControl tabelaParceiro;

        private IRepositorioParceiro repositorioParceiro;

        private ServicoParceiro servicoParceiro;

        public ControladorParceiro(IRepositorioParceiro repositorioParceiro, ServicoParceiro servicoParceiro)
        {
            this.repositorioParceiro = repositorioParceiro;
            this.servicoParceiro = servicoParceiro;
        }

        public override void Inserir()
        {
            TelaParceiroForm tela = new TelaParceiroForm();

            tela.onGravarRegistro += servicoParceiro.Inserir;

            tela.ConfigurarParceiro(new Parceiro());

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarParceiros();
            }
        }


        public override void Editar()
        {
            int id = tabelaParceiro.ObterNumeroTesteSelecionado();

            Parceiro parceiroSelecionado = repositorioParceiro.SelecionarPorId(id);

            if (parceiroSelecionado == null)
            {
                MessageBox.Show("Selecione um parceiro primeiro",
                "Edição de Parceiro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
                return;
            }

            TelaParceiroForm tela = new TelaParceiroForm();

            tela.onGravarRegistro += servicoParceiro.Editar;

            tela.ConfigurarParceiro(parceiroSelecionado);

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarParceiros();
            }
        }

        public override void Excluir()
        {
            int id = tabelaParceiro.ObterNumeroTesteSelecionado();

            Parceiro parceiroSelecionado = repositorioParceiro.SelecionarPorId(id);

            if (parceiroSelecionado == null)
            {
                MessageBox.Show("Selecione um parceiro primeiro",
                "Exclusão de Parceiro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult opcaoEscolhida =
               MessageBox.Show($"Deseja realmente excluir um parceiro \"{parceiroSelecionado}\"?",
               "Exclusão de Parceiro",
               MessageBoxButtons.OKCancel,
               MessageBoxIcon.Question);

            if (opcaoEscolhida == DialogResult.OK)
            {
                Result resultado = servicoParceiro.Excluir(parceiroSelecionado);

                if (resultado.IsFailed)
                {
                    MessageBox.Show(resultado.Errors[0].Message,
                        "Exclusão de Parceiro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                CarregarParceiros();
            }
        }


        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxCupomEParceiro();
        }

        public override UserControl ObterListagem()
        {
            if(tabelaParceiro == null)
                tabelaParceiro = new TabelaParceiroControl();

            CarregarParceiros();

            return tabelaParceiro;
        }

        private void CarregarParceiros()
        {
            List<Parceiro> parceiros = repositorioParceiro.SelecionarTodos();

            tabelaParceiro.AtualizarRegistros(parceiros);

            mensagemRodape = string.Format("Visualizando {0} parceiros{1}", parceiros.Count, parceiros.Count == 1 ? "" : "s");

            TelaPrincipalForm.Tela.AtualizarRodape(mensagemRodape);
        }
    }
}
