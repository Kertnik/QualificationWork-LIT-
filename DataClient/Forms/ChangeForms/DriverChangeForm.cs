using System;
using System.Linq;
using System.Windows.Forms;
using DataClient.Models;

namespace DataClient.Forms.ChangeForms
{
    public partial class DriverChangeForm : Form
    {

        public DriverChangeForm()
        {

            InitializeComponent();
        }



        private void DriverChange_Load(object sender, EventArgs e)
        {
            using (var db = new TgBotContext())
            {
                var bindings = new BindingSource();
                foreach (var variable in db.MyDrivers)
                {
                    bindings.Add(variable.DriverId);
                }

                bindings.Add("New...");
                ToolStripDriversComboBox.ComboBox.DataSource = bindings;
            }
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ToolStripDriversComboBox.SelectedItem == "New") return;
            using (var db = new TgBotContext())
            {
                db.MyDrivers.Remove(db.MyDrivers.Find(ToolStripDriversComboBox.SelectedItem));
                await db.SaveChangesAsync();
                var bindings = new BindingSource();
                foreach (var variable in db.MyDrivers)
                {
                    bindings.Add(variable.DriverId);
                }
                bindings.Add("New...");
                ToolStripDriversComboBox.ComboBox.DataSource = bindings;
            }
        }



        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
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
                    if(db.MyDrivers.Any(x=>x.DriverId==Id.Text)){MessageBox.Show("Такий ID вже існує", "Помилка");
                        return;}
                    db.MyDrivers.Add(new MyDriver { Name = DriverName.Text, DriverId = Id.Text });
                    await db.SaveChangesAsync();
                }
                else
                {
                    var driver = db.MyDrivers.Find(ToolStripDriversComboBox.SelectedItem);
                    driver.Name = DriverName.Text;
                    driver.DriverId = Id.Text;
                    await db.SaveChangesAsync();
                }
                var bindings = new BindingSource();
                foreach (var variable in db.MyDrivers)
                {
                    bindings.Add(variable.DriverId);
                }
                bindings.Add("New...");
                ToolStripDriversComboBox.ComboBox.DataSource = bindings;

            }
        }

        private void Id_TextChanged(object sender, EventArgs e)
        {

        }
    }
}