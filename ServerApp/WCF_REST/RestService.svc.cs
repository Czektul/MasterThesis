using Newtonsoft.Json.Linq;
using ProjectClasses.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using WCF_REST.Controllers;

namespace WCF_REST
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „RestService” w kodzie, usłudze i pliku konfiguracji.
    // UWAGA: aby uruchomić klienta testowego WCF w celu przetestowania tej usługi, wybierz plik RestService.svc lub RestService.svc.cs w eksploratorze rozwiązań i rozpocznij debugowanie.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class RestService : IRestService
    {
        private Logic logic;
        public User[] users;
        public Room[] rooms;
        public Project[] projects;
        public static byte[] fileBytes;
        public static bool ifFileReciving;
        public RestService()
        {
            logic = new Logic();
            users = logic.GetUser();
            rooms = logic.GetRooms();
            projects = logic.GetProjects();
            logic.SetUserRoom(users, rooms);
            logic.SetProjectUsers(projects, users);
        }

        #region GET

        public string GetUser(string id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(users.FirstOrDefault(u => u.GetId() == int.Parse(id)));
        }
        public string GetUsers()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(users);
        }

        public string GetProject(string id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(projects.FirstOrDefault(p => p.GetId() == int.Parse(id)));
        }

        public string GetProjects()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(projects);
        }

        public string GetRoom(string id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(rooms.FirstOrDefault(r => r.GetId() == int.Parse(id)));
        }

        public string GetRooms()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(rooms);
        }

        #endregion

        #region POST

        public bool AddUser(User user)
        {
            try
            {
                if (user != null)
                    if (!logic.AddUser(user))
                        return false;
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool AddRoom(Room room)
        {
            try
            {
                if (room != null)
                    if (!logic.AddRoom(room))
                        return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool AddProject(Project project)
        {
            try
            {
                if (project != null)
                    if (!logic.AddProject(project))
                        return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool AddFile(string data, string filename, string messageEnd)
        {
            //string filename = "test";
            byte[] bytearray;
            List<byte> bytesTmp = new List<byte>();
            /*FileStream fileToupload = new FileStream("D:\\FileUpload\\"+ filename, FileMode.Create);
            using (MemoryStream ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                bytearray = ms.ToArray();
            }*/

            
//            bytearray = Encoding.UTF8.GetBytes(data));
            bytearray = Convert.FromBase64String(data);
            if (ifFileReciving)
            {
                bytesTmp = fileBytes.ToList();
                bytesTmp.AddRange(bytearray);
                fileBytes = bytesTmp.ToArray();
                ifFileReciving = true;
            }
            else
            {
                fileBytes = bytearray;
                ifFileReciving = true;
            }
            if(bool.Parse(messageEnd))
            {
                FileStream fileToupload = new FileStream("D:\\FileUpload\\" + filename, FileMode.Create);
                using (MemoryStream ms = new MemoryStream())
                {
                    ifFileReciving = false;
                   // ms.Write(fileBytes, 0, fileBytes.Length);
                   // ms.CopyTo(fileToupload);
                    fileToupload.Write(fileBytes, 0, fileBytes.Length);
                    fileToupload.Close();
                    fileToupload.Dispose();
                }
                
            }

            return true;
        }

        #endregion

        #region PUT

        public bool UpdateUser(User user)
        {
            try
            {
                if (user != null)
                {
                    if (!logic.UpdateUser(user))
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool UpdateRoom(Room room)
        {
            try
            {
                if (room != null)
                {
                    if (!logic.UpdateRoom(room))
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool UpdateProject(Project project)
        {
            try
            {
                if (project != null)
                {
                    if (!logic.UpdateProject(project))
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        #endregion


        #region DELETE

        public bool DeleteUser(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    if (!logic.DeleteUser(userId))
                        return false;
                }
                else
                    return false;
                
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteRoom(string roomId)
        {
            try
            {
                if (!string.IsNullOrEmpty(roomId))
                {
                    if (!logic.DeleteRoom(roomId))
                        return false;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteProject(string projectId)
        {
            try
            {
                if (!string.IsNullOrEmpty(projectId))
                {
                    if (!logic.DeleteProject(projectId))
                        return false;
                }
                else
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
