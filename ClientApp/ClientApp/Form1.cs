using ProjectClasses.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        BindingList<User> gridList = new BindingList<User>();
        private BindingSource bindingSource1 = new BindingSource();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetUsers_Click(object sender, EventArgs e)
        {
            User user;
            string url = string.Format("http://localhost:51412/api/values/user/{0}", textBox1.Text.ToString()); //JKRze - porpawic 

            using (var w = new WebClient())
            {
                w.Encoding = Encoding.UTF8;
                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 

                user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json_data, new UserConverter());
                lblFirstName.Text = user.FirstName;
                lblId.Text = user.Id.ToString();
                lblLastName.Text = user.LastName;
                lblName.Text = user.Name;
                lblRoom.Text = user.Room.Code;
            }


        }
    }
}
