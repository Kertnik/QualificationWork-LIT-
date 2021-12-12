
using System.Windows.Controls;

namespace DataClient.Forms
{
    partial class WaitForm
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
            this.DescriptionOfWork = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // DescriptionOfWork
            // 
            this.DescriptionOfWork.Font = new System.Drawing.Font("Calibri Light", 14F);
            this.DescriptionOfWork.Location = new System.Drawing.Point(12, 18);
            this.DescriptionOfWork.Name = "DescriptionOfWork";
            this.DescriptionOfWork.Size = new System.Drawing.Size(305, 48);
            this.DescriptionOfWork.TabIndex = 0;
            this.DescriptionOfWork.Text = "Please wait";
            this.DescriptionOfWork.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 78);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(305, 57);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 1;
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(329, 175);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.DescriptionOfWork);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WaitForm";
            this.Load += new System.EventHandler(this.WaitForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DescriptionOfWork;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}