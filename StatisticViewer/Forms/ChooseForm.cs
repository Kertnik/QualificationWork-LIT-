namespace StatisticViewer.Forms
{
    public partial class ChooseForm : Form
    {

        public ChooseForm()
        {

            new WelcomeForm().ShowDialog(this);
            Hide();
            InitializeComponent();
        }

        private void FormChoose_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }

        private void ViewByRoute_Click(object sender, EventArgs e)
        {

        }


        private void ViewByDriver_Click(object sender, EventArgs e)
        {

        }

        private void UpdateByRoute_Click(object sender, EventArgs e)
        {

        }

        private void UpdateByDriver_Click(object sender, EventArgs e)
        {

        }
    }
}