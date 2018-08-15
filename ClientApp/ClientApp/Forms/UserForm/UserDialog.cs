using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
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

        public UserDialog()
        {
            InitializeComponent();

            tbFirstName.Text = string.Empty;
            tbLastName.Text = string.Empty;
            tbName.Text = string.Empty;

            receiver = new Receiver("http://localhost:52435/RestService.svc/api");
            rooms = receiver.ReceiveData<Room>("room", new string[0], true);
            cbxRoom.DataSource = rooms;
            cbxRoom.DisplayMember = "Room.Id";

            this.button1.Click += new System.EventHandler(this.AddNew);
        }
        public UserDialog(User user)
        {
            this.user = user;
            InitializeComponent();

            tbFirstName.Text = user.FirstName;
            tbLastName.Text = user.LastName;
            tbName.Text = user.Name;
            receiver = new Receiver("http://localhost:52435/RestService.svc/api");
            rooms = receiver.ReceiveData<Room>("room", new string[0], true);
            cbxRoom.Items.AddRange(rooms);
            cbxRoom.SelectedItem = user.Room;
            this.button1.Click += new System.EventHandler(this.SaveChanges);
        }

        private void SaveChanges(object sender, EventArgs e)
        {
            string url = "http://localhost:52435/RestService.svc/api";
            string data = string.Empty;
            Sender poster = new Sender(url);

            user.Name = tbName.Text;
            user.FirstName = tbFirstName.Text;
            user.LastName = tbLastName.Text;
            user.Room = cbxRoom.SelectedValue as Room;

        }
        private void AddNew(object sender, EventArgs e)
        {
            string url = "http://localhost:52435/RestService.svc/api";
            string data = string.Empty;
            Sender poster = new Sender(url);
            User user = new User
            {
                Name = tbName.Text,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Room = cbxRoom.SelectedValue as Room
            };

            if(poster.SendData("add_user", user, false))
            {
                MessageBox.Show("Poprawnie wysłano dane");
            
            }
            else
            {
                MessageBox.Show("Błąd wysyłania danych");
            }
        }

    }
}
