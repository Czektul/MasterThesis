using ProjectClasses.Classes;
using RESTLib;
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
        Receiver receiver;
        
        public Form1()
        {
            InitializeComponent();
            receiver = new Receiver("http://localhost:51412/api/values");
        }

        private void btnGetUsers_Click(object sender, EventArgs e)
        {
            User[] user;
            /*
            string url = string.Format("http://localhost:51412/api/values/user/{0}", textBox1.Text.ToString()); //JKRze - porpawic 

            using (WebClient w = new WebClient())
            {
                w.Encoding = Encoding.UTF8;
                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 

                user = Newtonsoft.Json.JsonConvert.DeserializeObject<User[]>(json_data, new UserConverter());
                /*lblFirstName.Text = user.FirstName;
                lblId.Text = user.Id.ToString();
                lblLastName.Text = user.LastName;
                lblName.Text = user.Name;
                lblRoom.Text = user.Room.Code;
                bindingSource1.Clear();
                foreach (User u in user)
                    gridList.Add(u);
                bindingSource1 = new BindingSource(gridList, null);*/

            string[] parameters = new string[0];
            bool isArray;
            if (textBox1.Text.ToString() != string.Empty)
            {
                parameters = new string[1] { textBox1.Text.ToString() };
                isArray = false;
            }
            else
                isArray = true;


            user = receiver.ReceiveData<User>("user", parameters, isArray);

            if (user[0] == null)
            {
                MessageBox.Show("brak danych");
            }
            else
            {
                tableModel1.Rows.Clear();
                for (int i = 0; i < user.Count(); i++)
                {
                    tableModel1.Rows.Add(new XPTable.Models.Row());
                    tableModel1.Rows[i].Cells.Add(new XPTable.Models.Cell(user[i].Id.ToString()));
                    tableModel1.Rows[i].Cells.Add(new XPTable.Models.Cell(user[i].Name));
                    tableModel1.Rows[i].Cells.Add(new XPTable.Models.Cell(user[i].FirstName));
                    tableModel1.Rows[i].Cells.Add(new XPTable.Models.Cell(user[i].LastName));

                }

            }


        }
    }
}
