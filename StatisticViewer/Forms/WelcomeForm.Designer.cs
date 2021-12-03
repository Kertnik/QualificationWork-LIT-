namespace StatisticViewer.Forms
{
    partial class WelcomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.NameOfWork = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameOfWork
            // 
            this.NameOfWork.BackColor = System.Drawing.Color.Azure;
            this.NameOfWork.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NameOfWork.Font = new System.Drawing.Font("Calibri Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameOfWork.Location = new System.Drawing.Point(120, 40);
            this.NameOfWork.Multiline = true;
            this.NameOfWork.Name = "NameOfWork";
            this.NameOfWork.ReadOnly = true;
            this.NameOfWork.Size = new System.Drawing.Size(650, 350);
            this.NameOfWork.TabIndex = 1;
            this.NameOfWork.Text = resources.GetString("NameOfWork.Text");
            this.NameOfWork.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.SystemColors.Control;
            this.Start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Start.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Start.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Start.Location = new System.Drawing.Point(300, 411);
            this.Start.Name = "Start";
            this.Start.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Start.Size = new System.Drawing.Size(280, 120);
            this.Start.TabIndex = 0;
            this.Start.Text = "Почати";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.NameOfWork);
            this.Controls.Add(this.Start);
            this.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "WelcomeForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Кваліфікаційна робота";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WelcomeForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox NameOfWork;
        private Button Start;
    }
}