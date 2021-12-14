namespace DataClient.Forms.ChangeForms
{
    partial class DriverChangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriverChangeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.CreateStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripDriversComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.Id = new System.Windows.Forms.TextBox();
            this.DriverName = new System.Windows.Forms.TextBox();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Id";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateStripMenuItem1,
            this.ToolStripDriversComboBox});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(341, 27);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "MainMenu";
            // 
            // CreateStripMenuItem1
            // 
            this.CreateStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.CreateStripMenuItem1.Name = "CreateStripMenuItem1";
            this.CreateStripMenuItem1.Size = new System.Drawing.Size(33, 23);
            this.CreateStripMenuItem1.Text = "Дії";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.SaveToolStripMenuItem.Text = "Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.DeleteToolStripMenuItem.Text = "Delete";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // ToolStripDriversComboBox
            // 
            this.ToolStripDriversComboBox.Name = "ToolStripDriversComboBox";
            this.ToolStripDriversComboBox.Size = new System.Drawing.Size(121, 23);
            this.ToolStripDriversComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // Id
            // 
            this.Id.Enabled = false;
            this.Id.Location = new System.Drawing.Point(73, 53);
            this.Id.Name = "Id";
            this.Id.Size = new System.Drawing.Size(254, 30);
            this.Id.TabIndex = 4;
            this.Id.TextChanged += new System.EventHandler(this.Id_TextChanged_1);
            this.Id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Id_KeyPress);
            // 
            // DriverName
            // 
            this.DriverName.Location = new System.Drawing.Point(73, 105);
            this.DriverName.Name = "DriverName";
            this.DriverName.Size = new System.Drawing.Size(254, 30);
            this.DriverName.TabIndex = 5;
            // 
            // DriverChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(341, 157);
            this.Controls.Add(this.DriverName);
            this.Controls.Add(this.Id);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("Calibri", 14F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DriverChangeForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Кваліфікаційна робота";
            this.Load += new System.EventHandler(this.DriverChange_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem CreateStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox ToolStripDriversComboBox;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.TextBox Id;
        private System.Windows.Forms.TextBox DriverName;
    }
}