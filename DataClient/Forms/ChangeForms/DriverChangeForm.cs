using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataClient.Models;

namespace DataClient.Forms.ChangeForms;

public partial class DriverChangeForm : Form
{
    public DriverChangeForm()
    {
        InitializeComponent();
    }


    void DriverChange_Load(object sender, EventArgs e)
    {
        using (var db = new TgBotContext())
        {
            var bindings = new BindingSource();
            foreach (var variable in db.MyDrivers) bindings.Add(variable.DriverId);

            bindings.Add("New...");
            ToolStripDriversComboBox.ComboBox.DataSource = bindings;
        }
    }

    async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (ToolStripDriversComboBox.SelectedItem == "New") return;
        using (var db = new TgBotContext())
        {
            db.MyDrivers.Remove(db.MyDrivers.Find(ToolStripDriversComboBox.SelectedItem));
            await db.SaveChangesAsync();
            var bindings = new BindingSource();
            foreach (var variable in db.MyDrivers) bindings.Add(variable.DriverId);
            bindings.Add("New...");
            ToolStripDriversComboBox.ComboBox.DataSource = bindings;
        }
    }


    void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ToolStripDriversComboBox.SelectedItem == "New...")
        {
            Id.Enabled = true;
            DriverName.Text = "";
            Id.Text = "";
            return;
        }

        using (var db = new TgBotContext())
        {
            Id.Enabled = false;
            var user = db.MyDrivers.Find(ToolStripDriversComboBox.SelectedItem);
            Id.Text = user.DriverId;
            DriverName.Text = user.Name;
        }
    }

    async void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var db = new TgBotContext())
        {
            if (ToolStripDriversComboBox.SelectedItem == "New...")
            {
                if (string.IsNullOrWhiteSpace(DriverName.Text) | string.IsNullOrWhiteSpace(Id.Text))
                {
                    MessageBox.Show("Ви мусите заповнити усі поля", "Помилка");
                    return;
                }

                if (db.MyDrivers.Any(x => x.DriverId == Id.Text))
                {
                    MessageBox.Show("Такий ID вже існує", "Помилка");
                    return;
                }

                db.MyDrivers.Add(new MyDriver { Name = DriverName.Text, DriverId = Id.Text });
                await db.SaveChangesAsync();
            }
            else
            {
                var driver = await db.MyDrivers.FindAsync(ToolStripDriversComboBox.SelectedItem);
                driver.Name = DriverName.Text;
                driver.DriverId = Id.Text;
                await db.SaveChangesAsync();
            }

            var bindings = new BindingSource();
            foreach (var variable in db.MyDrivers) bindings.Add(variable.DriverId);
            bindings.Add("New...");
            ToolStripDriversComboBox.ComboBox.DataSource = bindings;
        }
    }


    void Id_TextChanged_1(object sender, EventArgs a)
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

        if (!(IsEnglishOrNumberCharacter(e.KeyChar) || IsUkranianOrNumberCharacter(e.KeyChar)))
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
}