using System;
using System.Threading;
using System.Windows.Forms;

namespace DataClient.Forms
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
            new WaitForm((() =>
                {
                    using var db = new TgBotContext();
                    foreach (var variable in db.MyCurRoutes)
                    {
                        if ((DateTime.Now.Date - variable.Day.Date).Days > 30) db.MyCurRoutes.Remove(variable);
                    }
                    db.SaveChanges();
                }
            ),"Видалення старих даних з БД").ShowDialog();
            isStartClicked = true;
            Close();

        }

        private void WelcomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isStartClicked) Application.Exit();

        }
    }
}