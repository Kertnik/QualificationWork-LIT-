
using System.Windows.Forms;

namespace DataClient.Forms.ViewForms
{
    partial class DriverViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriverViewForm));
            this.DriversListBox = new System.Windows.Forms.ComboBox();
            this.TimeChart = new LiveCharts.WinForms.CartesianChart();
            this.routesPieChart = new LiveCharts.WinForms.PieChart();
            this.SuspendLayout();
            // 
            // DriversListBox
            // 
            this.DriversListBox.FormattingEnabled = true;
            this.DriversListBox.Location = new System.Drawing.Point(66, 76);
            this.DriversListBox.Name = "DriversListBox";
            this.DriversListBox.Size = new System.Drawing.Size(239, 31);
            this.DriversListBox.TabIndex = 0;
            this.DriversListBox.SelectedIndexChanged += new System.EventHandler(this.DriverList_SelectedIndexChanged);
            // 
            // TimeChart
            // 
            this.TimeChart.Location = new System.Drawing.Point(12, 183);
            this.TimeChart.Name = "TimeChart";
            this.TimeChart.Size = new System.Drawing.Size(662, 325);
            this.TimeChart.TabIndex = 1;
            this.TimeChart.Text = "TimeChart";
            // 
            // routesPieChart
            // 
            this.routesPieChart.Location = new System.Drawing.Point(378, 12);
            this.routesPieChart.Name = "routesPieChart";
            this.routesPieChart.Size = new System.Drawing.Size(238, 176);
            this.routesPieChart.TabIndex = 2;
            this.routesPieChart.Text = "routesPieChart";
            // 
            // DriverViewForm
            // 
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(698, 520);
            this.Controls.Add(this.routesPieChart);
            this.Controls.Add(this.TimeChart);
            this.Controls.Add(this.DriversListBox);
            this.Font = new System.Drawing.Font("Calibri Light", 14F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DriverViewForm";
            this.Text = "Статистика за водієм";
            this.ResumeLayout(false);

        }


        #endregion
        private ComboBox DriversListBox;
        private LiveCharts.WinForms.CartesianChart TimeChart;
        private LiveCharts.WinForms.PieChart routesPieChart;
    }
}