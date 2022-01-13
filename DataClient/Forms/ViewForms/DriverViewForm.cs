using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace DataClient.Forms.ViewForms;

public partial class DriverViewForm : Form
{
    public DriverViewForm()
    {
        InitializeComponent();

        routesPieChart.LegendLocation = LegendLocation.Top;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Cursor = Cursors.WaitCursor;
        using (var db = new TgBotContext())
        {
            var drivers = db.MyDrivers.ToList();
            var binding = new BindingSource();
            binding.Add("Choose");
            foreach (var d in drivers) binding.Add(d.ToString());

            DriversListBox.DataSource = binding;
            routesPieChart.Series = new SeriesCollection();
        }

        var arrDates = new DateTime[30];
        for (int i = 0; i < 30; i++)
        {
            arrDates[i] = DateTime.Now.Date.AddDays(-i);
        }

        arrDates = arrDates.Reverse().ToArray();
        TimeChart.AxisX.Add(new Axis
        {
            Title = "Дата",
            Labels = arrDates.Select(x => x.Date.ToString("M/d")).ToList()
        });

        TimeChart.AxisY.Add(new Axis
        {
            Title = "Кількість маршрутів на день",MinValue=0
        });
        DefaultMarkup();
        Cursor = Cursors.Default;
    }

    void DriverList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DriversListBox.SelectedItem == "" | DriversListBox.SelectedItem == null) return;
        if (DriversListBox.SelectedItem == "Choose")
        {
            DefaultMarkup();
            return;
        }

        Cursor = Cursors.WaitCursor;
        var now = DateTime.Now.Date;
        int[] arr = new int[30];
        using (var db = new TgBotContext())
        {
            var driver = db.MyDrivers.Find(((string)DriversListBox.SelectedItem).Split('(')[1].Replace(")", ""));
            if (driver.MyCurRoutes.Count == 0)
            {
                DefaultMarkup();
                Cursor = Cursors.Default;
                return;
            }

            foreach (var variable in driver.MyCurRoutes)
                arr[30 - (int)(DateTime.Now.Date - variable.Day.Date).TotalDays]++;

            TimeChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "",
                    Values = new ChartValues<int>(arr),
                    PointGeometry = DefaultGeometries.Square,
                    FontFamily = new System.Windows.Media.FontFamily("Calibri Light"),
                    FontSize = 20.0,
                }
            };
            routesPieChart.Series = new SeriesCollection();
            foreach (var variable in driver.MyCurRoutes.GroupBy(d => d.RouteId))
                routesPieChart.Series.Add(new PieSeries
                {
                    Title = variable.Key,
                    Values = new ChartValues<double> { variable.Count() },
                    DataLabels = true,
                    FontFamily = new System.Windows.Media.FontFamily("Calibri Light"),
                    FontSize = 20.0,
                });
        }

        Cursor = Cursors.Default;
    }

    void DefaultMarkup()
    {
        TimeChart.Series = new SeriesCollection
        {
            new LineSeries
            {
                Values = new ChartValues<int>()
            }
        };
        routesPieChart.Series = new SeriesCollection
        {
            new PieSeries
            {
                Values = new ChartValues<int>()
            }
        };
    }

    void Screenshot_Click(object sender, EventArgs e)
    {
        var bmp = new Bitmap(Width, Height);

        DrawToBitmap(bmp, Bounds);
        SaveScreenshot.Filter = "Image|*.bmp;";
        SaveScreenshot.ShowDialog();
        bmp.Save(SaveScreenshot.FileName);
    }
}