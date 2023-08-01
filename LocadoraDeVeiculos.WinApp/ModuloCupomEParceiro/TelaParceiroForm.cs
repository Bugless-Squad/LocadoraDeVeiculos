using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCupomEParceiro;

namespace LocadoraDeVeiculos.WinApp.ModuloCupomEParceiro
{
    public partial class TelaParceiroForm : Form
    {
        private Parceiro parceiro;

        public event GravarRegistroDelegate<Parceiro> onGravarRegistro;

        public TelaParceiroForm()
        {
            InitializeComponent();

            this.ConfigurarDialog();
        }

        public Parceiro ObterParceiro()
        {
            parceiro.id = Convert.ToInt32(txtIdParceiro.Text);
            parceiro.nome = txtNomeParceiro.Text;

            return parceiro;
        }

        public void ConfigurarParceiro(Parceiro parceiro)
        {
            this.parceiro = parceiro;

            txtIdParceiro.Text = parceiro.id.ToString();
            txtNomeParceiro.Text = parceiro.nome;

        }
        private void btnGravarParceiro_Click(object sender, EventArgs e)
        {
            this.parceiro = ObterParceiro();

            if (onGravarRegistro != null)
            {
                Result resultado = onGravarRegistro(parceiro);


                if (resultado.IsFailed)
                {
                    string erro = resultado.Errors[0].Message;

                    TelaPrincipalForm.Tela.AtualizarRodape(erro);

                    DialogResult = DialogResult.None;
                }
            }
        }
    }
}
