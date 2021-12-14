using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataClient.Forms;

public partial class WaitForm : Form
{
    readonly Action Worker;

    public WaitForm(Action worker, string text = null)
    {
        Worker = worker ?? throw new ArgumentException();
        InitializeComponent();
        if (text != null) DescriptionOfWork.Text = text;
    }

    void WaitForm_Load(object sender, EventArgs e)
    {
        Task.Factory.StartNew(Worker)
            .ContinueWith(_ => Close(), TaskScheduler.FromCurrentSynchronizationContext());
    }
}