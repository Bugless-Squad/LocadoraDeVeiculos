namespace LocadoraDeVeiculos.WinApp
{
    partial class TelaPrincipalForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaPrincipalForm));
            lableRodape = new ToolStripStatusLabel();
            panelRegistros = new Panel();
            pictureBox1 = new PictureBox();
            toolStrip1 = new ToolStrip();
            btnHome = new ToolStripButton();
            toolStripSeparator0 = new ToolStripSeparator();
            btnConfigurar = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnInserir = new ToolStripButton();
            btnEditar = new ToolStripButton();
            btnExcluir = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnAdicionarItens = new ToolStripButton();
            btnRemoverItens = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnFinalizarPgto = new ToolStripButton();
            btnVisualizar = new ToolStripButton();
            btnVisualizarGabarito = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            btnGerarPdf = new ToolStripButton();
            btnFiltrar = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            labelTipoDoCadastro = new ToolStripLabel();
            rodape = new StatusStrip();
            menuStrip1 = new MenuStrip();
            cadastrosToolStrip = new ToolStripMenuItem();
            funcionáriosToolStripMenuItem = new ToolStripMenuItem();
            grupoDeAutomóveisToolStripMenuItem = new ToolStripMenuItem();
            planosDeCobrançaToolStripMenuItem = new ToolStripMenuItem();
            automoveisToolStripMenuItem = new ToolStripMenuItem();
            clientesToolStripMenuItem = new ToolStripMenuItem();
            condutoresToolStripMenuItem = new ToolStripMenuItem();
            taxasEServiçosToolStripMenuItem = new ToolStripMenuItem();
            parceirosToolStripMenuItem = new ToolStripMenuItem();
            cuponsEParceirosToolStripMenuItem = new ToolStripMenuItem();
            configuraçãoDePreçosToolStripMenuItem = new ToolStripMenuItem();
            serviçosToolStripMenuItem = new ToolStripMenuItem();
            aluguéisToolStripMenuItem = new ToolStripMenuItem();
            panelRegistros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            toolStrip1.SuspendLayout();
            rodape.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lableRodape
            // 
            lableRodape.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lableRodape.Name = "lableRodape";
            lableRodape.Size = new Size(50, 21);
            lableRodape.Text = "          ";
            // 
            // panelRegistros
            // 
            panelRegistros.BorderStyle = BorderStyle.FixedSingle;
            panelRegistros.Controls.Add(pictureBox1);
            panelRegistros.Dock = DockStyle.Fill;
            panelRegistros.Location = new Point(0, 50);
            panelRegistros.Margin = new Padding(4);
            panelRegistros.Name = "panelRegistros";
            panelRegistros.Size = new Size(1188, 589);
            panelRegistros.TabIndex = 11;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(-1, -1);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1184, 586);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnHome, toolStripSeparator0, btnConfigurar, toolStripSeparator1, btnInserir, btnEditar, btnExcluir, toolStripSeparator2, btnAdicionarItens, btnRemoverItens, toolStripSeparator3, btnFinalizarPgto, btnVisualizar, btnVisualizarGabarito, toolStripSeparator4, btnGerarPdf, btnFiltrar, toolStripSeparator5, labelTipoDoCadastro });
            toolStrip1.Location = new Point(0, 25);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1188, 25);
            toolStrip1.TabIndex = 10;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnHome
            // 
            btnHome.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnHome.Enabled = false;
            btnHome.Image = (Image)resources.GetObject("btnHome.Image");
            btnHome.ImageTransparentColor = Color.Magenta;
            btnHome.Name = "btnHome";
            btnHome.Padding = new Padding(6);
            btnHome.Size = new Size(32, 32);
            btnHome.Text = "Home";
            btnHome.ToolTipText = "Botão Home indisponível.";
            btnHome.Visible = false;
            btnHome.Click += btnHome_Click;
            // 
            // toolStripSeparator0
            // 
            toolStripSeparator0.Name = "toolStripSeparator0";
            toolStripSeparator0.Size = new Size(6, 35);
            toolStripSeparator0.Visible = false;
            // 
            // btnConfigurar
            // 
            btnConfigurar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnConfigurar.Enabled = false;
            btnConfigurar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnConfigurar.Image = (Image)resources.GetObject("btnConfigurar.Image");
            btnConfigurar.ImageTransparentColor = Color.Magenta;
            btnConfigurar.Name = "btnConfigurar";
            btnConfigurar.Padding = new Padding(6);
            btnConfigurar.Size = new Size(32, 32);
            btnConfigurar.Visible = false;
            btnConfigurar.Click += btnConfigurar_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 35);
            toolStripSeparator1.Visible = false;
            // 
            // btnInserir
            // 
            btnInserir.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnInserir.Enabled = false;
            btnInserir.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnInserir.Image = (Image)resources.GetObject("btnInserir.Image");
            btnInserir.ImageTransparentColor = Color.Magenta;
            btnInserir.Name = "btnInserir";
            btnInserir.Padding = new Padding(6);
            btnInserir.Size = new Size(32, 32);
            btnInserir.Text = "Configurar";
            btnInserir.Visible = false;
            btnInserir.Click += btnInserir_Click;
            // 
            // btnEditar
            // 
            btnEditar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditar.Enabled = false;
            btnEditar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnEditar.Image = (Image)resources.GetObject("btnEditar.Image");
            btnEditar.ImageTransparentColor = Color.Magenta;
            btnEditar.Name = "btnEditar";
            btnEditar.Padding = new Padding(6);
            btnEditar.Size = new Size(32, 32);
            btnEditar.Text = "Editar";
            btnEditar.Visible = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnExcluir.Enabled = false;
            btnExcluir.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnExcluir.Image = (Image)resources.GetObject("btnExcluir.Image");
            btnExcluir.ImageTransparentColor = Color.Magenta;
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Padding = new Padding(6);
            btnExcluir.Size = new Size(32, 32);
            btnExcluir.Text = "Excluir";
            btnExcluir.Visible = false;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 35);
            toolStripSeparator2.Visible = false;
            // 
            // btnAdicionarItens
            // 
            btnAdicionarItens.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAdicionarItens.Enabled = false;
            btnAdicionarItens.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnAdicionarItens.Image = (Image)resources.GetObject("btnAdicionarItens.Image");
            btnAdicionarItens.ImageTransparentColor = Color.Magenta;
            btnAdicionarItens.Name = "btnAdicionarItens";
            btnAdicionarItens.Padding = new Padding(6);
            btnAdicionarItens.Size = new Size(32, 32);
            btnAdicionarItens.Visible = false;
            // 
            // btnRemoverItens
            // 
            btnRemoverItens.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRemoverItens.Enabled = false;
            btnRemoverItens.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnRemoverItens.Image = (Image)resources.GetObject("btnRemoverItens.Image");
            btnRemoverItens.ImageTransparentColor = Color.Magenta;
            btnRemoverItens.Name = "btnRemoverItens";
            btnRemoverItens.Padding = new Padding(6);
            btnRemoverItens.Size = new Size(32, 32);
            btnRemoverItens.Visible = false;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 35);
            toolStripSeparator3.Visible = false;
            // 
            // btnFinalizarPgto
            // 
            btnFinalizarPgto.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnFinalizarPgto.Enabled = false;
            btnFinalizarPgto.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnFinalizarPgto.Image = (Image)resources.GetObject("btnFinalizarPgto.Image");
            btnFinalizarPgto.ImageTransparentColor = Color.Magenta;
            btnFinalizarPgto.Name = "btnFinalizarPgto";
            btnFinalizarPgto.Padding = new Padding(6);
            btnFinalizarPgto.Size = new Size(32, 32);
            btnFinalizarPgto.Visible = false;
            // 
            // btnVisualizar
            // 
            btnVisualizar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnVisualizar.Enabled = false;
            btnVisualizar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnVisualizar.Image = (Image)resources.GetObject("btnVisualizar.Image");
            btnVisualizar.ImageTransparentColor = Color.Magenta;
            btnVisualizar.Name = "btnVisualizar";
            btnVisualizar.Padding = new Padding(6);
            btnVisualizar.Size = new Size(32, 32);
            btnVisualizar.Text = "VisualizarTeste";
            btnVisualizar.Visible = false;
            // 
            // btnVisualizarGabarito
            // 
            btnVisualizarGabarito.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnVisualizarGabarito.Enabled = false;
            btnVisualizarGabarito.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnVisualizarGabarito.Image = (Image)resources.GetObject("btnVisualizarGabarito.Image");
            btnVisualizarGabarito.ImageTransparentColor = Color.Magenta;
            btnVisualizarGabarito.Name = "btnVisualizarGabarito";
            btnVisualizarGabarito.Padding = new Padding(6);
            btnVisualizarGabarito.Size = new Size(32, 32);
            btnVisualizarGabarito.Text = "VisualizarTeste Gabarito";
            btnVisualizarGabarito.Visible = false;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 35);
            toolStripSeparator4.Visible = false;
            // 
            // btnGerarPdf
            // 
            btnGerarPdf.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnGerarPdf.Enabled = false;
            btnGerarPdf.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnGerarPdf.Image = (Image)resources.GetObject("btnGerarPdf.Image");
            btnGerarPdf.ImageTransparentColor = Color.Magenta;
            btnGerarPdf.Name = "btnGerarPdf";
            btnGerarPdf.Padding = new Padding(6);
            btnGerarPdf.Size = new Size(32, 32);
            btnGerarPdf.Text = "Filtrar";
            btnGerarPdf.Visible = false;
            // 
            // btnFiltrar
            // 
            btnFiltrar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnFiltrar.Enabled = false;
            btnFiltrar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnFiltrar.Image = (Image)resources.GetObject("btnFiltrar.Image");
            btnFiltrar.ImageTransparentColor = Color.Magenta;
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Padding = new Padding(6);
            btnFiltrar.Size = new Size(32, 32);
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.Visible = false;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 25);
            toolStripSeparator5.Visible = false;
            // 
            // labelTipoDoCadastro
            // 
            labelTipoDoCadastro.Name = "labelTipoDoCadastro";
            labelTipoDoCadastro.Size = new Size(76, 22);
            labelTipoDoCadastro.Text = "                       ";
            // 
            // rodape
            // 
            rodape.Items.AddRange(new ToolStripItem[] { lableRodape });
            rodape.Location = new Point(0, 639);
            rodape.Name = "rodape";
            rodape.Padding = new Padding(1, 0, 18, 0);
            rodape.Size = new Size(1188, 26);
            rodape.TabIndex = 9;
            rodape.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { cadastrosToolStrip, serviçosToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 3, 0, 3);
            menuStrip1.Size = new Size(1188, 25);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // cadastrosToolStrip
            // 
            cadastrosToolStrip.DropDownItems.AddRange(new ToolStripItem[] { funcionáriosToolStripMenuItem, grupoDeAutomóveisToolStripMenuItem, planosDeCobrançaToolStripMenuItem, automoveisToolStripMenuItem, clientesToolStripMenuItem, condutoresToolStripMenuItem, taxasEServiçosToolStripMenuItem, parceirosToolStripMenuItem, cuponsEParceirosToolStripMenuItem, configuraçãoDePreçosToolStripMenuItem });
            cadastrosToolStrip.Name = "cadastrosToolStrip";
            cadastrosToolStrip.Size = new Size(71, 19);
            cadastrosToolStrip.Text = "Cadastros";
            // 
            // funcionáriosToolStripMenuItem
            // 
            funcionáriosToolStripMenuItem.Image = (Image)resources.GetObject("funcionáriosToolStripMenuItem.Image");
            funcionáriosToolStripMenuItem.Name = "funcionáriosToolStripMenuItem";
            funcionáriosToolStripMenuItem.Size = new Size(203, 22);
            funcionáriosToolStripMenuItem.Text = "Funcionários";
            funcionáriosToolStripMenuItem.Click += funcionariosMenuItem_Click;
            // 
            // grupoDeAutomóveisToolStripMenuItem
            // 
            grupoDeAutomóveisToolStripMenuItem.Image = (Image)resources.GetObject("grupoDeAutomóveisToolStripMenuItem.Image");
            grupoDeAutomóveisToolStripMenuItem.Name = "grupoDeAutomóveisToolStripMenuItem";
            grupoDeAutomóveisToolStripMenuItem.Size = new Size(203, 22);
            grupoDeAutomóveisToolStripMenuItem.Text = "Grupo de automóveis ";
            grupoDeAutomóveisToolStripMenuItem.Click += grupoDeAutomoveisMenuItem_Click;
            // 
            // planosDeCobrançaToolStripMenuItem
            // 
            planosDeCobrançaToolStripMenuItem.Image = (Image)resources.GetObject("planosDeCobrançaToolStripMenuItem.Image");
            planosDeCobrançaToolStripMenuItem.Name = "planosDeCobrançaToolStripMenuItem";
            planosDeCobrançaToolStripMenuItem.Size = new Size(203, 22);
            planosDeCobrançaToolStripMenuItem.Text = "Planos de cobrança";
            planosDeCobrançaToolStripMenuItem.Click += planosDeCobrancaMenuItem_Click;
            // 
            // automoveisToolStripMenuItem
            // 
            automoveisToolStripMenuItem.Image = (Image)resources.GetObject("automoveisToolStripMenuItem.Image");
            automoveisToolStripMenuItem.Name = "automoveisToolStripMenuItem";
            automoveisToolStripMenuItem.Size = new Size(203, 22);
            automoveisToolStripMenuItem.Text = "Automoveis";
            automoveisToolStripMenuItem.Click += automoveisMenuItem_Click;
            // 
            // clientesToolStripMenuItem
            // 
            clientesToolStripMenuItem.Image = (Image)resources.GetObject("clientesToolStripMenuItem.Image");
            clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            clientesToolStripMenuItem.Size = new Size(203, 22);
            clientesToolStripMenuItem.Text = "Clientes";
            clientesToolStripMenuItem.Click += clientesMenuItem_Click;
            // 
            // condutoresToolStripMenuItem
            // 
            condutoresToolStripMenuItem.Name = "condutoresToolStripMenuItem";
            condutoresToolStripMenuItem.Size = new Size(203, 22);
            condutoresToolStripMenuItem.Text = "Condutores";
            condutoresToolStripMenuItem.Click += condutoresMenuItem_Click;
            // 
            // taxasEServiçosToolStripMenuItem
            // 
            taxasEServiçosToolStripMenuItem.Image = (Image)resources.GetObject("taxasEServiçosToolStripMenuItem.Image");
            taxasEServiçosToolStripMenuItem.Name = "taxasEServiçosToolStripMenuItem";
            taxasEServiçosToolStripMenuItem.Size = new Size(203, 22);
            taxasEServiçosToolStripMenuItem.Text = "Taxas e serviços";
            taxasEServiçosToolStripMenuItem.Click += taxasEServiçosMenuItem_Click;
            // 
            // parceirosToolStripMenuItem
            // 
            parceirosToolStripMenuItem.Name = "parceirosToolStripMenuItem";
            parceirosToolStripMenuItem.Size = new Size(203, 22);
            parceirosToolStripMenuItem.Text = "Parceiros";
            parceirosToolStripMenuItem.Click += parceirosToolStripMenuItem_Click;
            // 
            // cuponsEParceirosToolStripMenuItem
            // 
            cuponsEParceirosToolStripMenuItem.Image = (Image)resources.GetObject("cuponsEParceirosToolStripMenuItem.Image");
            cuponsEParceirosToolStripMenuItem.Name = "cuponsEParceirosToolStripMenuItem";
            cuponsEParceirosToolStripMenuItem.Size = new Size(203, 22);
            cuponsEParceirosToolStripMenuItem.Text = "Cupons";
            cuponsEParceirosToolStripMenuItem.Click += cuponsMenuItem_Click;
            // 
            // configuraçãoDePreçosToolStripMenuItem
            // 
            configuraçãoDePreçosToolStripMenuItem.Image = (Image)resources.GetObject("configuraçãoDePreçosToolStripMenuItem.Image");
            configuraçãoDePreçosToolStripMenuItem.Name = "configuraçãoDePreçosToolStripMenuItem";
            configuraçãoDePreçosToolStripMenuItem.Size = new Size(203, 22);
            configuraçãoDePreçosToolStripMenuItem.Text = "Configuração de preços ";
            configuraçãoDePreçosToolStripMenuItem.Click += configuracaoDePrecosMenuItem_Click;
            // 
            // serviçosToolStripMenuItem
            // 
            serviçosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aluguéisToolStripMenuItem });
            serviçosToolStripMenuItem.Name = "serviçosToolStripMenuItem";
            serviçosToolStripMenuItem.Size = new Size(62, 19);
            serviçosToolStripMenuItem.Text = "Serviços";
            // 
            // aluguéisToolStripMenuItem
            // 
            aluguéisToolStripMenuItem.Name = "aluguéisToolStripMenuItem";
            aluguéisToolStripMenuItem.Size = new Size(123, 22);
            aluguéisToolStripMenuItem.Text = "Aluguéis ";
            aluguéisToolStripMenuItem.Click += alugueisMenuItem_Click;
            // 
            // TelaPrincipalForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1188, 665);
            Controls.Add(panelRegistros);
            Controls.Add(toolStrip1);
            Controls.Add(rodape);
            Controls.Add(menuStrip1);
            Name = "TelaPrincipalForm";
            ShowIcon = false;
            Text = "Locadora De Veículos";
            panelRegistros.ResumeLayout(false);
            panelRegistros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            rodape.ResumeLayout(false);
            rodape.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStripStatusLabel lableRodape;
        private Panel panelRegistros;
        private ToolStrip toolStrip1;
        private ToolStripButton btnHome;
        private ToolStripSeparator toolStripSeparator0;
        private ToolStripButton btnConfigurar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnInserir;
        private ToolStripButton btnEditar;
        private ToolStripButton btnExcluir;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnAdicionarItens;
        private ToolStripButton btnRemoverItens;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnFinalizarPgto;
        private ToolStripButton btnVisualizar;
        private ToolStripButton btnVisualizarGabarito;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnGerarPdf;
        private ToolStripButton btnFiltrar;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripLabel labelTipoDoCadastro;
        private StatusStrip rodape;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem cadastrosToolStrip;
        private ToolStripMenuItem funcionáriosToolStripMenuItem;
        private ToolStripMenuItem grupoDeAutomóveisToolStripMenuItem;
        private ToolStripMenuItem planosDeCobrançaToolStripMenuItem;
        private ToolStripMenuItem automoveisToolStripMenuItem;
        private ToolStripMenuItem clientesToolStripMenuItem;
        private ToolStripMenuItem condutoresToolStripMenuItem;
        private ToolStripMenuItem taxasEServiçosToolStripMenuItem;
        private ToolStripMenuItem cuponsEParceirosToolStripMenuItem;
        private ToolStripMenuItem configuraçãoDePreçosToolStripMenuItem;
        private ToolStripMenuItem serviçosToolStripMenuItem;
        private ToolStripMenuItem aluguéisToolStripMenuItem;
        private ToolStripMenuItem parceirosToolStripMenuItem;
        private PictureBox pictureBox1;
    }
}