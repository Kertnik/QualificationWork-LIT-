using System;
using System.Windows.Forms;

namespace DataClient.Forms;

public partial class WelcomeForm : Form
{
    bool isStartClicked;

    public WelcomeForm()
    {
        InitializeComponent();
    }

    void Start_Click(object sender, EventArgs e)
    {
        new WaitForm(() =>
        {
            using var db = new TgBotContext();
            foreach (var variable in db.MyCurRoutes)
                if ((DateTime.Now.Date - variable.Day.Date).Days > 30)
                    db.MyCurRoutes.Remove(variable);
            db.SaveChanges();
        }, "Актуалізація даних").ShowDialog();
        isStartClicked = true;
        Close();
    }

    void WelcomeForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!isStartClicked) Application.Exit();
    }
}