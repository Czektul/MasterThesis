using ClientApp.Forms.UserForm;
using Extensions;
using ProjectClasses.Classes;
using RESTLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Models;

namespace ClientApp
{
    public partial class Main : Form
    {
        List<User> users;
        List<Project> projects;
        List<Room> rooms;
        private BindingSource bindingSource1 = new BindingSource();

        Receiver receiver;
        Sender sendFile;
        User selectedUser;

        public Main()
        {
            InitializeComponent();
            receiver = new Receiver(Configuration.ServerAddress);
            users = new List<User>();
            projects = new List<Project>();
            rooms = new List<Room>();
            btnEditUser.Enabled = false;
            RefreshData();
        }

        #region events

        private void btnGetUsers_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            UserDialog dialog;
                dialog = new UserDialog(this);
                dialog = new UserDialog(selectedUser, this);
            dialog.Show();
        }
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            UserDialog dialog;
            dialog = new UserDialog(selectedUser, this);
            dialog.Show();
        }

        private void table1_SelectionChanged(object sender, XPTable.Events.SelectionEventArgs e)
        {
            foreach (Row row in table1.SelectedItems)
            {
                selectedUser = users.FirstOrDefault(u => u.Id == (int.Parse(row.Cells[0].Text)));
                if (selectedUser != null)
                {
                    btnEditUser.Enabled = true;
                }
                else
                {
                    btnEditUser.Enabled = false;
                }

            }
        }
        #endregion

        #region methods

        public void RefreshData()
        {            
            User[] user;
            string[] param = new string[1] { string.Empty };
            try
            {
                users.Clear();
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
                    throw new Exception("Błąd pobierania użytkownika");
                }
                else
                {
                    tableModel1.Rows.Clear();
                    for (int i = 0; i < user.Count(); i++)
                    {
                        users.Add(user[i]);
                        tableModel1.Rows.Add(new Row());
                        tableModel1.Rows[i].Cells.Add(new Cell(user[i].Id.ToString()));
                        tableModel1.Rows[i].Cells.Add(new Cell(user[i].Name));
                        tableModel1.Rows[i].Cells.Add(new Cell(user[i].FirstName));
                        tableModel1.Rows[i].Cells.Add(new Cell(user[i].LastName));
                        tableModel1.Rows[i].Cells.Add(new Cell(user[i].Room));
                    }
                }

                projects = receiver.ReceiveData<Project>("project", param, true).ToList();
                if(projects[0] == null)
                {
                    throw new Exception("Błąd pobierania projektów");
                }
                rooms = receiver.ReceiveData<Room>("room", param, true).ToList();
                if (rooms[0] == null)
                {
                    throw new Exception("Błąd pobierania pokojów");
                }
                projects[0].ProjectUsers();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Błąd pobierania danych: \n" + ex.Message);
            }
        }
        #endregion

        private void btnFIle_Click(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog dialog = new OpenFileDialog();
            byte[] buffer;
            sendFile = new Sender(Configuration.ServerAddress);
            try
            {

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    path = dialog.InitialDirectory + dialog.FileName;
                }
                else return;

                using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    
                    if (sendFile.SendFileAsStream("add_file", fs, dialog.SafeFileName))
                    {
                        MessageBox.Show("Wysłano dane");
                    }
                    else
                    {
                        MessageBox.Show("Bład wysylania pliku");
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
