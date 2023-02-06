namespace Game
{
    partial class Desk
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.boardPanel = new Game.BoardPanel(this.components);
            this.SuspendLayout();
            // 
            // boardPanel
            // 
            this.boardPanel.Location = new System.Drawing.Point(12, 12);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(696, 696);
            this.boardPanel.TabIndex = 0;
            this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPanel_Paint);
            // 
            // Desk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.boardPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "Desk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Desk_Load);
            this.ResizeEnd += new System.EventHandler(this.Desk_ResizeEnd);
            this.ResumeLayout(false);
        }

        #endregion

        private Game.BoardPanel boardPanel;
    }
}

/*this.boardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
this.boardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.boardPanel.Location = new System.Drawing.Point(20, 20);
this.boardPanel.Margin = new System.Windows.Forms.Padding(20);
this.boardPanel.Name = "boardPanel";
this.boardPanel.Size = new System.Drawing.Size(680, 680);
this.boardPanel.TabIndex = 0;
this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPanel_Paint);*/
