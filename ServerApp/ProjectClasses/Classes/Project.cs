using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProjectClasses.Classes
{
    [DataContractAttribute]
    public class Project
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public User[] Users { get; set; }

        #region Constructors
        public Project(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion

        #region Getters
        public string GetName() { return Name; }
        public int GetId() { return Id; }

        public User[] GetAssignedUsers()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        public bool AddToDatabase()
        {
            try
            {
                SqlCommand command;
                if (Id == 0)
                {
                    return false;
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = Configuration.ConnectionString;
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            command = connection.CreateCommand();
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.CommandText = string.Format("INSERT INTO Projects (Name) VALUES ({0});", Name);
                            command.ExecuteNonQuery();

                            transaction.Commit();
                        }


                        command = connection.CreateCommand();
                        command.CommandType = CommandType.Text;
                        command.CommandText = string.Format("SELECT Id FROM Project WHERE Name = '{0}'", Name);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Id = (int)reader["Id"];
                            }
                        }
                        connection.Close();
                    }

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
            return true;
        }
        #endregion
    }
}
