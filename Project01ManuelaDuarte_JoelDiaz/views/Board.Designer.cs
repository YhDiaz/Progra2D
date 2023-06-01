
using System.Windows.Forms;

namespace Project01
{
    partial class Board
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.history = new System.Windows.Forms.Label();
            this.pivotButton = new System.Windows.Forms.Button();
            this.player = new System.Windows.Forms.PictureBox();
            this.inventoryButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // history
            // 
            this.history.AutoSize = true;
            this.history.Location = new System.Drawing.Point(50, 10);
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(49, 16);
            this.history.TabIndex = 3;
            this.history.Text = "History";
            // 
            // pivotButton
            // 
            this.pivotButton.Location = new System.Drawing.Point(50, 50);
            this.pivotButton.Name = "pivotButton";
            this.pivotButton.Size = new System.Drawing.Size(50, 50);
            this.pivotButton.TabIndex = 4;
            this.pivotButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.pivotButton.UseVisualStyleBackColor = true;
            // 
            // player
            // 
            this.player.Image = global::Project01.Properties.Resources.Player;
            this.player.InitialImage = global::Project01.Properties.Resources.Player;
            this.player.Location = new System.Drawing.Point(101, 50);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(50, 50);
            this.player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.player.TabIndex = 2;
            this.player.TabStop = false;
            // 
            // inventoryButton
            // 
            this.inventoryButton.Location = new System.Drawing.Point(800, 50);
            this.inventoryButton.Name = "inventoryButton";
            this.inventoryButton.Size = new System.Drawing.Size(131, 38);
            this.inventoryButton.TabIndex = 6;
            this.inventoryButton.Text = "Inventario";
            this.inventoryButton.UseVisualStyleBackColor = true;
            // 
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.inventoryButton);
            this.Controls.Add(this.pivotButton);
            this.Controls.Add(this.history);
            this.Controls.Add(this.player);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Board";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.Label history;
        private System.Windows.Forms.Button pivotButton;
        private Button inventoryButton;
    }
}

