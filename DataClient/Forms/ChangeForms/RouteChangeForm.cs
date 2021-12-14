using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataClient.Models;

namespace DataClient.Forms.ChangeForms;

public partial class RouteChangeForm : Form
{
    public RouteChangeForm()
    {
        InitializeComponent();
    }

    void ToolStripRoutesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (toolStripRoutesComboBox.SelectedItem == "New...")
        {
            Id.ReadOnly = false;
            StopsList.ReadOnly = false;
            Id.Text = "";
            StopsList.Text = "";
            return;
        }

        using (var db = new TgBotContext())
        {
            Id.ReadOnly = true;
            var route = db.MyRoutes.Find(toolStripRoutesComboBox.SelectedItem);
            Id.Text = route.RouteId;
            StopsList.Text = string.Join(Environment.NewLine, route.Stops.Split(';'));
            StopsList.ReadOnly = true;
        }
    }

    void RouteChange_Load(object sender, EventArgs e)
    {
        using (var db = new TgBotContext())
        {
            var bindings = new BindingSource();
            foreach (var variable in db.MyRoutes) bindings.Add(variable.RouteId);

            bindings.Add("New...");
            toolStripRoutesComboBox.ComboBox.DataSource = bindings;
        }
    }


    async void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var db = new TgBotContext())
        {
            db.MyRoutes.Remove(db.MyRoutes.Find(toolStripRoutesComboBox.SelectedItem));
            await db.SaveChangesAsync();
            var bindings = new BindingSource();
            foreach (var variable in db.MyRoutes) bindings.Add(variable.RouteId);

            bindings.Add("New...");
            toolStripRoutesComboBox.ComboBox.DataSource = bindings;
        }
    }

    async void SaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var db = new TgBotContext())
        {
            if (toolStripRoutesComboBox.SelectedItem == "New...")
            {
                if (string.IsNullOrWhiteSpace(StopsList.Text) | string.IsNullOrWhiteSpace(Id.Text))
                {
                    MessageBox.Show("Ви мусите заповнити усі поля", "Помилка");
                    return;
                }

                if (db.MyRoutes.Any(x => x.RouteId == Id.Text))
                {
                    MessageBox.Show("Такий ID вже існує", "Помилка");
                    return;
                }

                db.MyRoutes.Add(new MyRoute
                {
                    RouteId = Id.Text,
                    Stops = string.Join(";",
                        StopsList.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                });
                await db.SaveChangesAsync();
            }

            var bindings = new BindingSource();
            foreach (var variable in db.MyRoutes) bindings.Add(variable.RouteId);

            bindings.Add("New...");
            toolStripRoutesComboBox.ComboBox.DataSource = bindings;
        }
    }

    void Id_TextChanged(object sender, EventArgs a)
    {
        if (a is KeyPressEventArgs e)
        {
            if (Encoding.UTF8.GetByteCount(new[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
    }

    void Id_KeyPress(object sender, KeyPressEventArgs e)
    {
        var key = (Keys)e.KeyChar;

        if (key == Keys.Back)
        {
            return;
        }

        if (!IsEnglishOrNumberCharacter(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    static bool IsEnglishOrNumberCharacter(char ch)
    {
        return (ch >= '0' & ch <= '9') || (ch >= 'a' & ch <= 'z') || (ch >= 'A' & ch <= 'Z');
    }

    static bool IsUkranianOrNumberCharacter(char ch)
    {
        return (ch == ' ') || (ch >= 'а' & ch <= 'я') || (ch >= 'А' & ch <= 'Я') || (ch >= '0' & ch <= '9'
            | ch == 'Я' | ch == 'Ґ' | ch == 'Є' | ch == 'І' | ch == 'Ї' | ch == 'я' | ch == '\'' | ch == 'є' |
            ch == 'і' | ch == 'ї' | ch == 'ґ');
    }

    void StopsList_TextChanged(object sender, EventArgs a)
    {
        if (a is KeyPressEventArgs e)
        {
            if (Encoding.UTF8.GetByteCount(new[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
    }

    void StopsList_KeyPress(object sender, KeyPressEventArgs e)

    {
        var key = (Keys)e.KeyChar;

        if (key == Keys.Back | key == Keys.Enter)
        {
            return;
        }

        if (!IsUkranianOrNumberCharacter(e.KeyChar))
        {
            e.Handled = true;
        }
    }
}