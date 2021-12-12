namespace DataClient.Forms.ChangeForms
{
    partial class RouteChangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RouteChangeForm));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.ActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripRoutesComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.Id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StopsList = new System.Windows.Forms.TextBox();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ActionsToolStripMenuItem,
            this.toolStripRoutesComboBox});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(256, 27);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "MainMenu";
            // 
            // ActionsToolStripMenuItem
            // 
            this.ActionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.ActionsToolStripMenuItem.Name = "ActionsToolStripMenuItem";
            this.ActionsToolStripMenuItem.Size = new System.Drawing.Size(33, 23);
            this.ActionsToolStripMenuItem.Text = "Дії";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.SaveToolStripMenuItem.Text = "Зберегти";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.DeleteToolStripMenuItem.Text = "Видалити";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // toolStripRoutesComboBox
            // 
            this.toolStripRoutesComboBox.Name = "toolStripRoutesComboBox";
            this.toolStripRoutesComboBox.Size = new System.Drawing.Size(121, 23);
            this.toolStripRoutesComboBox.SelectedIndexChanged += new System.EventHandler(this.ToolStripRoutesComboBox_SelectedIndexChanged);
            // 
            // Id
            // 
            this.Id.Location = new System.Drawing.Point(43, 41);
            this.Id.Name = "Id";
            this.Id.Size = new System.Drawing.Size(201, 30);
            this.Id.TabIndex = 2;
            this.Id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Id_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Id";
            // 
            // StopsList
            // 
            this.StopsList.Location = new System.Drawing.Point(16, 77);
            this.StopsList.MaxLength = 1000000;
            this.StopsList.Multiline = true;
            this.StopsList.Name = "StopsList";
            this.StopsList.ReadOnly = true;
            this.StopsList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StopsList.Size = new System.Drawing.Size(228, 344);
            this.StopsList.TabIndex = 4;
            // 
            // RouteChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(256, 433);
            this.Controls.Add(this.StopsList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Id);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("Calibri Light", 14F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RouteChangeForm";
            this.Text = "RouteChange";
            this.Load += new System.EventHandler(this.RouteChange_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem ActionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripRoutesComboBox;
        private System.Windows.Forms.TextBox Id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox StopsList;
    }
}