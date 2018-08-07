﻿using ClientApp.Forms.UserForm;
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
using XPTable.Models;

namespace ClientApp
{
    public partial class Main : Form
    {
        List<User> users;
        private BindingSource bindingSource1 = new BindingSource();
        Receiver receiver;

        User selectedUser;
        public Main()
        {
            InitializeComponent();
            receiver = new Receiver("http://localhost:50042/api/values");
            users = new List<User>();
        }

        private void btnGetUsers_Click(object sender, EventArgs e)
        {
            User[] user;

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
                    users.Add(user[i]);
                    tableModel1.Rows.Add(new XPTable.Models.Row());
                    tableModel1.Rows[i].Cells.Add(new XPTable.Models.Cell(user[i].Id.ToString()));
                    tableModel1.Rows[i].Cells.Add(new XPTable.Models.Cell(user[i].Name));
                    tableModel1.Rows[i].Cells.Add(new XPTable.Models.Cell(user[i].FirstName));
                    tableModel1.Rows[i].Cells.Add(new XPTable.Models.Cell(user[i].LastName));

                }

            }


        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {

            UserDialog dialog;
            if (selectedUser == null)
            {
                dialog = new UserDialog();
            }
            else
            {
                dialog = new UserDialog(selectedUser);
            }

            dialog.Show();
        }

        private void table1_SelectionChanged(object sender, XPTable.Events.SelectionEventArgs e)
        {
            foreach (Row row in table1.SelectedItems)
            {
                selectedUser = users.FirstOrDefault(u => u.Id == (int.Parse(row.Cells[0].Text)));

            }
        }
    }
}
