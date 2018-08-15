using ProjectClasses.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "api/user/")]
        string GetUsers();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "api/user/{id}")]
        string GetUser(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "api/project/{id}")]
        string GetProject(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "api/project/")]
        string GetProjects();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "api/room/{id}")]
        string GetRoom(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
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
          BodyStyle = WebMessageBodyStyle.Wrapped,
          UriTemplate = "api/room/")]
        void AddRoom(string room);

        [OperationContract]
        [WebInvoke(Method = "POST",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped,
          UriTemplate = "api/project/")]
        void AddProject(string project);

        #endregion

        #region DELETE

        [OperationContract]
        [WebInvoke(Method = "DELETE",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped,
          UriTemplate = "api/user/")]
        void DeleteUser(string userId);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped,
          UriTemplate = "api/room/")]
        void DeleteRoom(string roomId);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
          ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped,
          UriTemplate = "api/project/")]
        void DeleteProject(string projectId);

        #endregion

    }
}
