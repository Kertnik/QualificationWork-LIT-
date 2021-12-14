using System;
using System.Windows.Forms;
using DataClient.Forms.ChangeForms;
using DataClient.Forms.ViewForms;

namespace DataClient.Forms;

public partial class ChooseForm : Form
{
    public ChooseForm()
    {
        new WelcomeForm().ShowDialog(this);

        InitializeComponent();
    }


    void FormChoose_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.ExitThread();
    }

    void ViewByRoute_Click(object sender, EventArgs e)
    {
        Cursor.Current = Cursors.WaitCursor;
        Hide();
        new WaitForm(() => new RouteViewForm().ShowDialog(), "Підготовка макету").ShowDialog();
        Show();
    }


    void ViewByDriver_Click(object sender, EventArgs e)
    {
        Cursor.Current = Cursors.WaitCursor;
        Hide();
        new WaitForm(() => new DriverViewForm(this).ShowDialog(), "Підготовка макету").ShowDialog();
        Show();
    }

    void UpdateByRoute_Click(object sender, EventArgs e)
    {
        Hide();
        new RouteChangeForm().ShowDialog();
        Show();
    }

    void UpdateByDriver_Click(object sender, EventArgs e)
    {
        Hide();
        new DriverChangeForm().ShowDialog();
        Show();
    }
}