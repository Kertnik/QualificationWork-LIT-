using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataClient.Forms
{
    public partial class WaitForm : Form
    {
        Action Worker;
        public WaitForm(Action worker,string text=null)
        {
            this.Worker = worker??throw new ArgumentException();
            InitializeComponent();
            if(text!=null)DescriptionOfWork.Text= text;
           
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {

            Task.Factory.StartNew(Worker)
                .ContinueWith(_ => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
       
        }

      
    }
}
