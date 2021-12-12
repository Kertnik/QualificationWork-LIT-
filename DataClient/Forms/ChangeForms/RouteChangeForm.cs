using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataClient.Models;

namespace DataClient.Forms.ChangeForms
{
    public partial class RouteChangeForm : Form
    {
        public RouteChangeForm()
        {
            InitializeComponent();
        }

        private void ToolStripRoutesComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void RouteChange_Load(object sender, EventArgs e)
        {
            using (var db = new TgBotContext())
            {
                var bindings = new BindingSource();
                foreach (var variable in db.MyRoutes)
                {
                    bindings.Add(variable.RouteId);
                }

                bindings.Add("New...");
                toolStripRoutesComboBox.ComboBox.DataSource = bindings;
            }

        }



        private async void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var db = new TgBotContext())
            {
                db.MyRoutes.Remove(db.MyRoutes.Find(toolStripRoutesComboBox.SelectedItem));
                await db.SaveChangesAsync();
                var bindings = new BindingSource();
                foreach (var variable in db.MyRoutes)
                {
                    bindings.Add(variable.RouteId);
                }

                bindings.Add("New...");
                toolStripRoutesComboBox.ComboBox.DataSource = bindings;
            }
        }

        private async void SaveToolStripMenuItem_Click(object sender, EventArgs e)
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

                    if (db.MyDrivers.Any(x => x.DriverId == Id.Text))
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
                foreach (var variable in db.MyRoutes)
                {
                    bindings.Add(variable.RouteId);
                }

                bindings.Add("New...");
                toolStripRoutesComboBox.ComboBox.DataSource = bindings;
            }
        }

        private void Id_TextChanged(object sender, EventArgs a)
        {
            if (a is KeyPressEventArgs e)
                if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
                {
                    e.Handled = true;
                }

        }

        private void Id_KeyPress(object sender, KeyPressEventArgs e)
        {
            var key = (Keys)e.KeyChar;

            if (key == Keys.Back)
            {
                return;
            }

            if (!IsEnglishCharacter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        static bool IsEnglishCharacter(char ch)=>ch is >= (char)97 and <= (char)122 or >= (char)65 and <= (char)90;
        
    }
}
