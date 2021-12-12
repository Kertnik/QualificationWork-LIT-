using System;
using System.Windows.Forms;
using DataClient.Forms.ChangeForms;
using DataClient.Forms.ViewForms;

namespace DataClient.Forms
{
    public partial class ChooseForm : Form
    {

        public ChooseForm()
        {

            new WelcomeForm().ShowDialog(this);

            InitializeComponent();
        }

        

        private void FormChoose_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }

        private void ViewByRoute_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            new RouteViewForm(this).ShowDialog();
        }


        private void ViewByDriver_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            new DriverViewForm(this).ShowDialog();
        }

        private void UpdateByRoute_Click(object sender, EventArgs e)
        {       new RouteChangeForm().ShowDialog();

        }

        private void UpdateByDriver_Click(object sender, EventArgs e)
        {
            new DriverChangeForm().ShowDialog();
        }

      
    }
}