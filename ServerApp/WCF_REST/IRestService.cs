using ProjectClasses.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_REST
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IRestService” w kodzie i pliku konfiguracji.
    [ServiceContract]
    public interface IRestService
    {
        #region GET

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "api/user/")]
        string GetUsers();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "api/user/{id}")]
        string GetUser(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "api/project/{id}")]
        string GetProject(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "api/project/")]
        string GetProjects();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "api/room/{id}")]
        string GetRoom(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "api/room/")]
        string GetRooms();

        #endregion

        #region POST
        [OperationContract]
        [WebInvoke(Method = "POST",
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/add_user/")]
        bool AddUser(User user);

        [OperationContract]
        [WebInvoke(Method = "POST",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/add_room/")]
        bool AddRoom(Room room);

        [OperationContract]
        [WebInvoke(Method = "POST",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/add_project/")]
        bool AddProject(Project project);

        [OperationContract]
        [WebInvoke(Method = "POST",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/add_file/{filename}")]
        bool AddFile(Stream fileStream, string filename);



        #endregion

        #region PUT
        [OperationContract]
        [WebInvoke(Method = "PUT",
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/put_user/")]
        bool UpdateUser(User user);

        [OperationContract]
        [WebInvoke(Method = "PUT",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/put_room/")]
        bool UpdateRoom(Room room);

        [OperationContract]
        [WebInvoke(Method = "PUT",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/put_project/")]
        bool UpdateProject(Project project);

        #endregion

        #region DELETE

        [OperationContract]
        [WebInvoke(Method = "DELETE",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/delete_user/{userId}")]
        bool DeleteUser(string userId);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/delete_room/{roomId}")]
        bool DeleteRoom(string roomId);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "api/delete_project/{projectId}")]
        bool DeleteProject(string projectId);

        #endregion

    }
}
