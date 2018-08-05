using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectClasses.Classes;

namespace ClientApp.Forms.UserForm
{
    public partial class UserDialog : Form
    {
        public User user;

        public UserDialog()
        {
            InitializeComponent();

            tbFirstName.Text = string.Empty;
            tbLastName.Text = string.Empty;
            tbName.Text = string.Empty;

            this.button1.Click += new System.EventHandler(this.AddNew);
        }
        public UserDialog(User user)
        {
            this.user = user;
            InitializeComponent();

            tbFirstName.Text = user.FirstName;
            tbLastName.Text = user.LastName;
            tbName.Text = user.Name;
            cbxRoom.SelectedItem = user.Room;
            this.button1.Click += new System.EventHandler(this.SaveChanges);
        }

        private void SaveChanges(object sender, EventArgs e)
        {/*
            User[] user;

            var values = new NameValueCollection();

            string[] parameters = new string[0];
            bool isArray;
            if (textBox1.Text.ToString() != string.Empty)
            {
                parameters = new string[1] { textBox1.Text.ToString() };
                isArray = false;
            }
            else
                isArray = true;


            user = receiver.ReceiveData<User>("user", parameters, isArray);*/
        }
        private void AddNew(object sender, EventArgs e)
        {
            User user = new User
            {
                Name = tbName.Text,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Room = cbxRoom.SelectedValue as Room
            };

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["user"] = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                client. = "application/json";

                var response = client.UploadValues("http://localhost:51412/api/values/user/", values);

                var responseString = Encoding.Default.GetString(response);
            }
        }

    }
}
