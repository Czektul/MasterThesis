using ProjectClasses.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Newtonsoft.Json.JsonConvert.SerializeObject(logic.GetUser());
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
            User newUser;
            if (user != null)
            { 
                try
                {
                  //  newUser = ((User)Newtonsoft.Json.JsonConvert.DeserializeObject<User>(user));
                    if (!logic.AddUser(user))
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddRoom(string room)
        {

        }

        public void AddProject(string project)
        {

        }

        #endregion
        #region DELETE

        public void DeleteUser(string userId)
        {

        }

        public void DeleteRoom(string roomId)
        {

        }

        public void DeleteProject(string projectId)
        {

        }

        #endregion
    }
}
