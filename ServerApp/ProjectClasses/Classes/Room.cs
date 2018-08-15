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
    public class Room
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public User[] Users { get; set; }

        #region Constructors
        public Room(int id, string code)
        {
            Id = id;
            Code = code;
            Users = new User[0];
        }
        public Room(string id, string code)
        {
            Id = int.Parse(id);
            Code = code;
            Users = new User[0];
        }

        public Room(string Id, string Code, User[] Users)
        {
            this.Id = int.Parse(Id);
            this.Code = Code;
            this.Users = new User[0];
        }

        [JsonConstructor]
        public Room(int Id, string Code, User[] Users)
        {
            this.Id = Id;
            this.Code = Code;
            this.Users = new User[0];
        }
        public Room(Room room)
        {
            Id = room.Id;
            Code = room.Code;
            Users = new User[0];
        }
    
        #endregion

        #region Getters
        public string GetCode() { return Code; }
        public int GetId() { return Id; }
        
        public User[] GetAssignedUsers()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        public bool AddToDatabase()
        {
            if (Id == 0)
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

    public class RoomConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(System.Security.Claims.Claim));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            int Id = int.Parse(jo["Id"].ToString());
            string Code = jo["Code"].ToString();
            return new Room(Id, Code);
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
