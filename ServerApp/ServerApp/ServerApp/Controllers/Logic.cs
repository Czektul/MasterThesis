﻿using ProjectClasses.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Controllers
{
    public class Logic
    {
        public Project[] GetProjects()
        {
            List<Project> projects = new List<Project>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = Configuration.ConnectionString;
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Format("SELECT * FROM Projects");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(
                                new Project(
                                    (int)reader["Id"], 
                                    (string)reader["Name"])
                                    );
                        }
                    }
                    connection.Close();
                }
                return projects.ToArray();
            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User[] GetUser()
        {
            List<User> users = new List<User>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = Configuration.ConnectionString;
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Format("SELECT * FROM Users");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(
                                new User(
                                    (int)reader["Id"], 
                                    (string)reader["Name"], 
                                    (string)reader["FirstName"], 
                                    (string)reader["LastName"])
                                    );
                        }
                    }
                    connection.Close();
                }
                return users.ToArray();
            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Room[] GetRooms()
        {
            List<Room> rooms = new List<Room>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = Configuration.ConnectionString;
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Format("SELECT * FROM Rooms");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add
                            (
                                new Room
                                (
                                    (int)reader["Id"], 
                                    (string)reader["Number"]
                                )
                            );
                        }
                    }
                    connection.Close();
                }
                return rooms.ToArray();
            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SetProjectUsers(Project [] projects, User[] users)
        {
            try
            {
                List<User> usersTmp = new List<User>();
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = Configuration.ConnectionString;
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    foreach (Project proj in projects)
                    {
                        command.CommandText = string.Format("SELECT * FROM ProjectsUsers WHERE  ProjectId = {0}", proj.Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersTmp.Add(users.FirstOrDefault(u => u.Id == (int)reader["UserId"]));
                            }
                        }
                        proj.Users = usersTmp.ToArray();
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        public void SetUserRoom(User[] users, Room[] rooms)
        {
            List<User> usersTmp = new List<User>();
            Room selectedRoom;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = Configuration.ConnectionString;
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    foreach (User user in users)
                    {
                        command.CommandText = string.Format("SELECT RoomId FROM Users WHERE  Id = {0}", user.Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedRoom = rooms.FirstOrDefault(r => r.Id == (int)reader["RoomId"]);
                                user.Room = selectedRoom;
                               /* usersTmp = selectedRoom.Users.ToList();
                                usersTmp.Add(user);
                                selectedRoom.Users = usersTmp.ToArray();*/
                                usersTmp.Clear();

                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        public bool AddUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = Configuration.ConnectionString;
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        command.Transaction = transaction;
                        command.CommandText = string.Format("INSERT INTO Users (Name, FirstName, LastName, RoomId) VALUES ('{0}', '{1}', '{2}', {3});", user.Name, user.FirstName, user.LastName, user.Room.Id);
                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    
                    connection.Close();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = Configuration.ConnectionString;
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        command.Transaction = transaction;
                        command.CommandText = string.Format("UPDATE Users SET [Name] = '{0}', " +
                            "[FirstName] = '{1}', [LastName] = '{2}', [RoomId] = {3}  WHERE [Id] = {4};", 
                            user.Name, user.FirstName, user.LastName, user.Room.Id, user.Id);
                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }

                    connection.Close();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
