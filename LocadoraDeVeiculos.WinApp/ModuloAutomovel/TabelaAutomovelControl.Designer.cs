﻿namespace LocadoraDeVeiculos.WinApp.ModuloAutomovel
{
    partial class TabelaAutomovelControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // grid
            // 
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Location = new Point(0, 0);
            grid.MaximumSize = new Size(1184, 586);
            grid.MinimumSize = new Size(1184, 586);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 25;
            grid.Size = new Size(1184, 586);
            grid.TabIndex = 2;
            // 
            // TabelaAutomovelControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(grid);
            Name = "TabelaAutomovelControl";
            Size = new Size(1184, 586);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView grid;
    }
}
