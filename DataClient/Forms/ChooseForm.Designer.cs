using System.Windows.Forms;

namespace DataClient.Forms
{
    partial class ChooseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseForm));
            this.StatisticBox = new System.Windows.Forms.GroupBox();
            this.ViewByDriver = new System.Windows.Forms.Button();
            this.ViewByRoute = new System.Windows.Forms.Button();
            this.UpdateBox = new System.Windows.Forms.GroupBox();
            this.UpdateByDriver = new System.Windows.Forms.Button();
            this.UpdateByRoute = new System.Windows.Forms.Button();
            this.StatisticBox.SuspendLayout();
            this.UpdateBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatisticBox
            // 
            this.StatisticBox.Controls.Add(this.ViewByDriver);
            this.StatisticBox.Controls.Add(this.ViewByRoute);
            this.StatisticBox.Location = new System.Drawing.Point(12, 12);
            this.StatisticBox.Name = "StatisticBox";
            this.StatisticBox.Size = new System.Drawing.Size(710, 215);
            this.StatisticBox.TabIndex = 0;
            this.StatisticBox.TabStop = false;
            this.StatisticBox.Text = "Статистика";
            // 
            // ViewByDriver
            // 
            this.ViewByDriver.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ViewByDriver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ViewByDriver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewByDriver.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ViewByDriver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewByDriver.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ViewByDriver.Location = new System.Drawing.Point(380, 50);
            this.ViewByDriver.Name = "ViewByDriver";
            this.ViewByDriver.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ViewByDriver.Size = new System.Drawing.Size(280, 120);
            this.ViewByDriver.TabIndex = 2;
            this.ViewByDriver.Text = "За водієм";
            this.ViewByDriver.UseVisualStyleBackColor = false;
            this.ViewByDriver.Click += new System.EventHandler(this.ViewByDriver_Click);
            // 
            // ViewByRoute
            // 
            this.ViewByRoute.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ViewByRoute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ViewByRoute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewByRoute.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ViewByRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewByRoute.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ViewByRoute.Location = new System.Drawing.Point(50, 50);
            this.ViewByRoute.Name = "ViewByRoute";
            this.ViewByRoute.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ViewByRoute.Size = new System.Drawing.Size(280, 120);
            this.ViewByRoute.TabIndex = 1;
            this.ViewByRoute.Text = "За маршрутом";
            this.ViewByRoute.UseVisualStyleBackColor = false;
            this.ViewByRoute.Click += new System.EventHandler(this.ViewByRoute_Click);
            // 
            // UpdateBox
            // 
            this.UpdateBox.Controls.Add(this.UpdateByDriver);
            this.UpdateBox.Controls.Add(this.UpdateByRoute);
            this.UpdateBox.Location = new System.Drawing.Point(12, 265);
            this.UpdateBox.Name = "UpdateBox";
            this.UpdateBox.Size = new System.Drawing.Size(710, 215);
            this.UpdateBox.TabIndex = 3;
            this.UpdateBox.TabStop = false;
            this.UpdateBox.Text = "Редагувати";
            // 
            // UpdateByDriver
            // 
            this.UpdateByDriver.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.UpdateByDriver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.UpdateByDriver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateByDriver.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UpdateByDriver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateByDriver.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.UpdateByDriver.Location = new System.Drawing.Point(380, 50);
            this.UpdateByDriver.Name = "UpdateByDriver";
            this.UpdateByDriver.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UpdateByDriver.Size = new System.Drawing.Size(280, 120);
            this.UpdateByDriver.TabIndex = 2;
            this.UpdateByDriver.Text = "Користувачів";
            this.UpdateByDriver.UseVisualStyleBackColor = false;
            this.UpdateByDriver.Click += new System.EventHandler(this.UpdateByDriver_Click);
            // 
            // UpdateByRoute
            // 
            this.UpdateByRoute.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.UpdateByRoute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.UpdateByRoute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateByRoute.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UpdateByRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateByRoute.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.UpdateByRoute.Location = new System.Drawing.Point(50, 50);
            this.UpdateByRoute.Name = "UpdateByRoute";
            this.UpdateByRoute.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UpdateByRoute.Size = new System.Drawing.Size(280, 120);
            this.UpdateByRoute.TabIndex = 1;
            this.UpdateByRoute.Text = "Маршрути";
            this.UpdateByRoute.UseVisualStyleBackColor = false;
            this.UpdateByRoute.Click += new System.EventHandler(this.UpdateByRoute_Click);
            // 
            // ChooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(732, 492);
            this.Controls.Add(this.UpdateBox);
            this.Controls.Add(this.StatisticBox);
            this.Font = new System.Drawing.Font("Calibri", 14F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "ChooseForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Кваліфікаційна робота";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormChoose_FormClosed);
             this.StatisticBox.ResumeLayout(false);
            this.UpdateBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox StatisticBox;
        private Button ViewByRoute;
        private Button ViewByDriver;
        private GroupBox UpdateBox;
        private Button UpdateByDriver;
        private Button UpdateByRoute;
    }
}