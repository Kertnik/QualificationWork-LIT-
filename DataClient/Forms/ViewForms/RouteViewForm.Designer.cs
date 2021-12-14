
using System.Windows.Forms;
using LiveCharts.WinForms;
namespace DataClient.Forms.ViewForms
{
    partial class RouteViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RouteViewForm));
            this.TypeOfRoutesListBox = new System.Windows.Forms.ComboBox();
            this.TimeChart = new LiveCharts.WinForms.CartesianChart();
            this.routesPieChart = new LiveCharts.WinForms.PieChart();
            this.RoutesListBox = new System.Windows.Forms.ComboBox();
            this.Total = new System.Windows.Forms.Label();
            this.Screenshot = new System.Windows.Forms.Button();
            this.SaveScreenshot = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // TypeOfRoutesListBox
            // 
            this.TypeOfRoutesListBox.FormattingEnabled = true;
            this.TypeOfRoutesListBox.Location = new System.Drawing.Point(12, 12);
            this.TypeOfRoutesListBox.Name = "TypeOfRoutesListBox";
            this.TypeOfRoutesListBox.Size = new System.Drawing.Size(239, 31);
            this.TypeOfRoutesListBox.TabIndex = 0;
            this.TypeOfRoutesListBox.SelectedIndexChanged += new System.EventHandler(this.TypeOfRoutesList_SelectedIndexChanged);
            // 
            // TimeChart
            // 
            this.TimeChart.Location = new System.Drawing.Point(12, 112);
            this.TimeChart.Name = "TimeChart";
            this.TimeChart.Size = new System.Drawing.Size(855, 582);
            this.TimeChart.TabIndex = 1;
            this.TimeChart.Text = "TimeChart";
            // 
            // routesPieChart
            // 
            this.routesPieChart.Location = new System.Drawing.Point(12, 112);
            this.routesPieChart.Name = "routesPieChart";
            this.routesPieChart.Size = new System.Drawing.Size(855, 582);
            this.routesPieChart.TabIndex = 2;
            this.routesPieChart.Text = "routesPieChart";
            // 
            // RoutesListBox
            // 
            this.RoutesListBox.Enabled = false;
            this.RoutesListBox.FormattingEnabled = true;
            this.RoutesListBox.Location = new System.Drawing.Point(277, 12);
            this.RoutesListBox.Name = "RoutesListBox";
            this.RoutesListBox.Size = new System.Drawing.Size(362, 31);
            this.RoutesListBox.TabIndex = 3;
            this.RoutesListBox.SelectedIndexChanged += new System.EventHandler(this.RoutesListBox_SelectedIndexChanged);
            // 
            // Total
            // 
            this.Total.Location = new System.Drawing.Point(12, 55);
            this.Total.Name = "Total";
            this.Total.Size = new System.Drawing.Size(627, 54);
            this.Total.TabIndex = 4;
            this.Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Screenshot
            // 
            this.Screenshot.Font = new System.Drawing.Font("Calibri Light", 50F);
            this.Screenshot.Location = new System.Drawing.Point(645, 12);
            this.Screenshot.Name = "Screenshot";
            this.Screenshot.Size = new System.Drawing.Size(222, 97);
            this.Screenshot.TabIndex = 5;
            this.Screenshot.Text = "📷";
            this.Screenshot.UseVisualStyleBackColor = true;
            this.Screenshot.Click += new System.EventHandler(this.Screenshot_Click);
            // 
            // RouteViewForm
            // 
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(879, 706);
            this.Controls.Add(this.Screenshot);
            this.Controls.Add(this.Total);
            this.Controls.Add(this.RoutesListBox);
            this.Controls.Add(this.routesPieChart);
            this.Controls.Add(this.TimeChart);
            this.Controls.Add(this.TypeOfRoutesListBox);
            this.Font = new System.Drawing.Font("Calibri Light", 14F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RouteViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Статистика за маршрутом";
            this.ResumeLayout(false);

        }


        #endregion
        private ComboBox TypeOfRoutesListBox;
        private LiveCharts.WinForms.CartesianChart TimeChart;
        private LiveCharts.WinForms.PieChart routesPieChart;
        private ComboBox RoutesListBox;
        private Label Total;
        private Button Screenshot;
        private SaveFileDialog SaveScreenshot;
    }
}