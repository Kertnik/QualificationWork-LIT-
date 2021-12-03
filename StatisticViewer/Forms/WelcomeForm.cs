namespace StatisticViewer.Forms
{
    public partial class WelcomeForm : Form
    {
 
        bool isStartClicked = false;
        public WelcomeForm()
        {
            InitializeComponent();
        }

        void Start_Click(object sender, EventArgs e)
        {
            isStartClicked = true;
            Close();

        }

        private void WelcomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(!isStartClicked) Application.ExitThread();
            
        }
    }
}