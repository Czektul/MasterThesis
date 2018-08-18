using Newtonsoft.Json;
using ProjectClasses.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WCF_REST.Controllers
{
    public class Logic
    {
        ///<summary>
        ///Receiving projects from database
        ///</summary>
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

        ///<summary>
        ///Receiving users from database
        ///</summary>
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

        ///<summary>
        ///Receiving rooms from database
        ///</summary>
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

        ///<summary>
        ///Setting user to project by [ProjectsUsers] table (many to many corelation)
        ///</summary>
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

        ///<summary>
        ///Setting room to user by Room.Id
        ///</summary>
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

        ///<summary>
        ///Add new user to database
        ///</summary>
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
                        command.CommandText = string.Format("INSERT INTO Users (Name, FirstName, LastName, RoomId) " +
                            "VALUES ('{0}', '{1}', '{2}', {3});", user.Name, user.FirstName, user.LastName, user.Room.Id);
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

        ///<summary>
        ///Add new user to database
        ///</summary>
        public bool AddRoom(Room room)
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
                        command.CommandText = string.Format("INSERT INTO Rooms (Number) VALUES ('{0}');", 
                            room.Code);
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

        ///<summary>
        ///Add new user to database
        ///</summary>
        public bool AddProject(Project project)
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
                        command.CommandText = string.Format("INSERT INTO Projects (Name) VALUES ('{0}');",
                            project.Name);
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

        ///<summary>
        ///Update selected user 
        ///</summary>
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

        ///<summary>
        ///Update selected room 
        ///</summary>
        public bool UpdateRoom(Room room)
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
                        command.CommandText = string.Format("UPDATE Rooms SET [Number] = '{0}' " +
                            "WHERE [Id] = {1};", room.Code, room.Id);
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

        ///<summary>
        ///Update selected project 
        ///</summary>
        public bool UpdateProject(Project project)
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
                        command.CommandText = string.Format("UPDATE Projects SET [Name] = '{0}' WHERE [Id] = {1};",
                            project.Name, project.Id);
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

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(string userId)
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
                        command.CommandText = string.Format("DELETE FROM Users WHERE [Id] = {0};", userId);
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

        /// <summary>
        /// Delete room
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public bool DeleteRoom(string roomId)
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
                        command.CommandText = string.Format("DELETE FROM Rooms WHERE [Id] = {0};", roomId);
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

        /// <summary>
        /// Delete project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public bool DeleteProject(string projectId)
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
                        command.CommandText = string.Format("DELETE FROM Projects WHERE [Id] = {0};", projectId);
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

        /// <summary>
        /// Assing users to project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="usersIds"></param>
        /// <returns></returns>
        public bool AssingUserToProject(int projectId, int[] usersIds)
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
                        foreach(int userId in usersIds)
                        {
                            command.Transaction = transaction;
                            command.CommandText = string.Format("INSERT INTO ProjectsUsers ([ProjectId], [UserId]) " +
                                "VALUES ({0}, {1});", projectId, userId);
                            command.ExecuteNonQuery();

                            transaction.Commit();
                        }
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

        /// <summary>
        /// Remove user from project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool RemoveUserFromProject(int projectId, int userId)
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
                        command.CommandText = string.Format("DELETE FROM ProjectsUsers WHERE " +
                            "[ProjectId] = {0} AND [UserId] = {1};", projectId, userId);
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
