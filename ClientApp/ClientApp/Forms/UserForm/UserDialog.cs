using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectClasses.Classes;
using RESTLib;

namespace ClientApp.Forms.UserForm
{
    public partial class UserDialog : Form
    {
        public User user;
        public Room[] rooms;
        Receiver receiver;
        Main main;

        public UserDialog(Main main)
        {
            InitializeComponent();
            this.main = main;

            tbFirstName.Text = string.Empty;
            tbLastName.Text = string.Empty;
            tbName.Text = string.Empty;

            receiver = new Receiver(Configuration.ServerAddress);
            rooms = receiver.ReceiveData<Room>("room", new string[0], true);
            cbxRoom.DataSource = rooms;
            cbxRoom.DisplayMember = "Code";
            btnDelete.Visible = false;

            this.button1.Click += new System.EventHandler(this.AddNew);
        }
        public UserDialog(User user, Main main)
        {
            InitializeComponent();
            this.main = main;
            this.user = user;

            tbFirstName.Text = user.FirstName;
            tbLastName.Text = user.LastName;
            tbName.Text = user.Name;
            receiver = new Receiver(Configuration.ServerAddress);
            rooms = receiver.ReceiveData<Room>("room", new string[0], true);
            cbxRoom.DataSource = rooms;
            cbxRoom.DisplayMember = "Code";
            cbxRoom.SelectedItem = rooms.FirstOrDefault(r => r.Id == user.Room.Id);
            this.button1.Click += new System.EventHandler(this.SaveChanges);
            btnDelete.Visible = true;
        }

        private void SaveChanges(object sender, EventArgs e)
        {
            string data = string.Empty;
            Updater updater = new Updater(Configuration.ServerAddress);

            user.Name = tbName.Text;
            user.FirstName = tbFirstName.Text;
            user.LastName = tbLastName.Text;
            user.Room = cbxRoom.SelectedValue as Room;
            if (updater.UpdateData("put_user", user))
            {
                MessageBox.Show("Poprawnie edytowano dane");
                this.Close();
                main.RefreshData();
            }
            else
                MessageBox.Show("Błąd edycji danych");

        }
        private void AddNew(object sender, EventArgs e)
        {
            string data = string.Empty;
            Sender poster = new Sender(Configuration.ServerAddress);
            User user = new User
            {
                Name = tbName.Text,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Room = cbxRoom.SelectedValue as Room
            };

            if (poster.SendData("add_user", user))
            {
                MessageBox.Show("Poprawnie dodano dane");
                this.Close();
                main.RefreshData();
            }
            else
                MessageBox.Show("Błąd dodawania danych");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Deleter deleter = new Deleter(Configuration.ServerAddress);
            try
            {
                string[] ids = new string[1];
                ids[0] = user.Id.ToString();

                if (deleter.DeleteData("delete_user", ids))
                {
                    this.Close();
                    main.RefreshData();
                }
                else
                    MessageBox.Show("Błąd usuwania danych");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd usuwania danych");
            }
        }
    }
}
