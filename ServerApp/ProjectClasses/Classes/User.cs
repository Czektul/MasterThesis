using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectClasses.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProjectClasses.Classes
{
    [DataContractAttribute]
    public class User
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public Room Room { get; set; }

        #region Constructors
        public User() { }

        public User(int id, string name, string firstname, string lastname)
        {
            Id = id;
            Name = name;
            FirstName = firstname;
            LastName = lastname;
        }

        [JsonConstructor]
        public User(int id, string name, string firstname, string lastname, Room room)
        {
            Id = id;
            Name = name;
            FirstName = firstname;
            LastName = lastname;
            Room = room;
        }
        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public  User(string id, string name, string firstname, string lastname, Room room)
        {
            Id = int.Parse(id);
            Name = name;
            FirstName = firstname;
            LastName = lastname;
            Room = new Room(room);
        }
        #endregion

        #region Getters
        public string GetName() { return Name; }
        public string GetFirstName() { return FirstName; }
        public string GetLastName() { return LastName; }
        public int GetId() { return Id; }
        public Room GetRoom()
        {
            throw new NotImplementedException();
        }

        #endregion
        
        #region Methods

        public bool AssingUserToRoom(string roomName)
        {
            throw new NotImplementedException();
        }

        public bool AddToDatabase()
        {
            if(Id == 0)
            {
                return false;
            }
            else
            {
                throw new NotImplementedException();
            }

        }
        
        #endregion
    }

    public class UserConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(System.Security.Claims.Claim));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            int Id = int.Parse(jo["Id"].ToString());
            string Name = (string)jo["Name"];
            string FirstName = (string)jo["FirstName"];
            string LastName = (string)jo["LastName"];
            return new User(Id, Name, FirstName, LastName);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
