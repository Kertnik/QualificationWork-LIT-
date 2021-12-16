using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace DataClient.Forms.ViewForms;

public partial class RouteViewForm : Form
{



    public RouteViewForm()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Cursor = Cursors.WaitCursor;
        Number.Enabled = false;
        using (var db = new TgBotContext())
        {
            routesPieChart.Font = new Font(FontFamily.Families.First(c => c.Name == "Calibri Light"), 20);
            var routes = db.MyRoutes.ToList();
            var binding = new BindingSource();
            binding.Add("Choose");
            int[] arr = new int[routes.Count];
            routesPieChart.Series = new SeriesCollection();
            foreach (var t in routes)
            {
                binding.Add(t.RouteId);

                routesPieChart.Series.Add(new PieSeries
                {
                    Title = t.RouteId,
                    DataLabels = true,
                    FontFamily = new System.Windows.Media.FontFamily("Calibri Light"),
                    FontSize = 20.0,
                    Values = new ChartValues<int> { t.MyCurRoutes.Count }
                });
            }

            Total.Text = "Усього маршрутів:" + routes.Count;
            routesPieChart.LegendLocation = LegendLocation.Right;
            TypeOfRoutesListBox.DataSource = binding;
        }
        TimeChart.AxisY.Clear();
        TimeChart.AxisY.Add(new Axis
        {
            Title = "Кількість"
        });
        Cursor = Cursors.Default;
    }

    void TypeOfRoutesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TypeOfRoutesListBox.SelectedItem == "" | TypeOfRoutesListBox.SelectedItem == null) return;
        if (TypeOfRoutesListBox.SelectedItem == "Choose")
        {
            RoutesListBox.Enabled = false;
            OnLoad(new EventArgs());
            routesPieChart.Visible = true;
            Number.Enabled = false;
            return;
        }

        routesPieChart.Visible = false;
        Number.Enabled = true;
        Number.Checked = false;
        using (var db = new TgBotContext())
        {
            var route = db.MyRoutes.Find(TypeOfRoutesListBox.SelectedItem.ToString());
            var binding = new BindingSource();

            binding.Add(new ComboboxItem("Choose", null));
            for (int i = 0; i < route.MyCurRoutes.Count; i++)
            {
                var d = route.MyCurRoutes[i];
                binding.Add(new ComboboxItem(d.Day.ToString("dd/MM HH:mm:ss"), d.RecordID));
            }

            RoutesListBox.DataSource = binding;
            RoutesListBox.Enabled = true;
        }


        Cursor = Cursors.Default;
    }


    void RoutesListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (var db = new TgBotContext())
        {
            if (((ComboboxItem)RoutesListBox.SelectedItem).Value == null)
            {
                var route = db.MyRoutes.Find(TypeOfRoutesListBox.SelectedItem.ToString());
                double[] arr = new double[route.Stops.Split(';').Length - 1];
                var listOfFinshed = route.MyCurRoutes.Where(p => p.NumberOfIncoming.Split(';').Length == arr.Length + 1);
                foreach (var curRoute in listOfFinshed)
                {

                    var times = (curRoute.Day + ";" + curRoute.TimeOfStops).Split(';').Select(x => Convert.ToDateTime(x)).ToList();

                    for (int i = 1; i < arr.Length + 1; i++)
                    {

                        arr[i - 1] += (times[i] - times[i - 1]).TotalMinutes;
                    }
                }

                TimeChart.Series = new SeriesCollection();
                string[] stops = route.Stops.Split(';');
                for (int i = 0; i < arr.Length; i++)
                {
                    stops[i] += "->" + stops[i + 1];
                    arr[i] = Math.Round(arr[i] / listOfFinshed.Count(), 2);
                }

                Total.Text = "Усього маршрутів:" + route.MyCurRoutes.Count + "\n Середня тривалість маршруту (хв):" +
                             arr.Sum();
                TimeChart.AxisY.Clear();
                TimeChart.AxisX.Clear();

                TimeChart.Series.Add(new LineSeries
                {
                    Title = "Середній час",
                    Values = new ChartValues<double>(arr)
                });
                TimeChart.AxisY.Add(new Axis
                {
                    Title = "Час у хвилинах",
                    MinValue = 0
                });
                TimeChart.AxisX.Add(new Axis
                {
                    Title = "Частини шляху",
                    Labels = stops,
                    ShowLabels = false
                });
                Number.Enabled = true;
            }
            else
            {
                Number.Enabled = false;
                Number.Checked=false;
                var route = db.MyCurRoutes.Find(((ComboboxItem)RoutesListBox.SelectedItem).Value);
                var arrIncoming = route.NumberOfIncoming.Split(';').Select(x => Convert.ToInt32(x)).ToArray();
                var arrLeaving = route.NumberOfLeaving.Split(';').Select(x => Convert.ToInt32(x)).ToArray();
                int[] arrTotal = new int[arrIncoming.Count()];
                for (int i = 0; i < arrTotal.Length; i++)
                {
                    arrTotal[i] = (i == 0 ? 0 : arrTotal[i - 1]) + arrIncoming[i] - arrLeaving[i];

                }
                Total.Text = "Усього пасажирів:" + arrIncoming.Sum() + "\n" + "Водій:" + route.MyDriver.Name +
                             $" ({route.MyDriver.DriverId})";


                TimeChart.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Values = new ChartValues<int>(arrLeaving),
                        Title = "Кількість вийшовших пасажирів"
                    },
                    new LineSeries
                    {
                        Values =
                            new ChartValues<int>(arrIncoming),
                        Title = "Кількість нових пасажирів"
                    }
                    ,
                    new LineSeries
                    {
                        Values =
                            new ChartValues<int>(arrTotal),
                        Title = "Загальна кількість пасажирів"
                    }
                };
            }
        }
    }

    void Screenshot_Click(object sender, EventArgs e)
    {
        Bitmap bmp;
        if (RoutesListBox.Enabled == false)
        {
            bmp = new Bitmap(routesPieChart.Width, routesPieChart.Height);
            routesPieChart.DrawToBitmap(bmp, new Rectangle(0, 0, routesPieChart.Width, routesPieChart.Height));
        }
        else
        {
            bmp = new Bitmap(TimeChart.Width, TimeChart.Height);
            TimeChart.DrawToBitmap(bmp, new Rectangle(0, 0, TimeChart.Width, TimeChart.Height));
        }

        SaveScreenshot.Filter = "Image|*.bmp;";
        SaveScreenshot.ShowDialog();
        bmp.Save(SaveScreenshot.FileName);
    }


    public class ComboboxItem
    {
        public ComboboxItem(string text, object value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    private void Number_CheckedChanged(object sender, EventArgs e)
    {
        using (var db = new TgBotContext())
        {
            if (((ComboboxItem)RoutesListBox.SelectedItem).Value == null)
            {
                TimeChart.Series.Clear();
                if (Number.Checked == true)
                {
                    var route = db.MyRoutes.Find(TypeOfRoutesListBox.SelectedItem.ToString());
                    double[] totalPassangersArr = new double[route.Stops.Split(';').Length];
                    var listOfFinished = route.MyCurRoutes.Where(p => p.NumberOfIncoming.Split(';').Length == totalPassangersArr.Length);
                    foreach (var curRoute in listOfFinished)
                    {
                        var incomers = curRoute.NumberOfIncoming.Split(';');
                        var leavers = curRoute.NumberOfLeaving.Split(';');
                        var locPassangers=new double[leavers.Length];
                        for (int i = 1; i < totalPassangersArr.Length + 1; i++)
                        {
                            locPassangers[i - 1]= (i == 1 ? 0 : locPassangers[i - 2]) + Convert.ToInt32(incomers[i - 1]) - Convert.ToInt32(leavers[i - 1]);

                            totalPassangersArr[i - 1] += (i == 1 ? 0 : locPassangers[i - 2]) + Convert.ToInt32(incomers[i - 1]) - Convert.ToInt32(leavers[i - 1]);

                        }
                    }
                    for (int i = 0; i < totalPassangersArr.Length; i++)
                    {
                        totalPassangersArr[i] = Math.Round(totalPassangersArr[i] / listOfFinished.Count(), 2);
                    }
                    TimeChart.AxisY.Clear(); TimeChart.Series.Add(new LineSeries
                    {
                        Title = "Загальна кількість пасажирів",
                        Values = new ChartValues<double>(totalPassangersArr)
                    });
                    TimeChart.AxisY.Add(new Axis
                    {
                        Title = "Середня кількість",
                        MinValue = 0
                    });

                }
                else
                {
                    RoutesListBox_SelectedIndexChanged(null, null);
                }
            }

        }



    }
}