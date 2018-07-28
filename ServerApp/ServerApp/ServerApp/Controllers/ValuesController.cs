using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectClasses.Classes;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public Logic logic = new Logic();
        public User[] users;
        public Room[] rooms;
        public Project[] projects;

        public ValuesController()
        {
            users = logic.GetUser();
            rooms = logic.GetRooms();
            projects = logic.GetProjects();
            logic.SetUserRoom(users, rooms);
            logic.SetProjectUsers(projects, users);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("user/{id}")]
        public string GetUser(int id)
        {            
            return Newtonsoft.Json.JsonConvert.SerializeObject(users.FirstOrDefault(u => u.GetId() == id));
        }

        [HttpGet("project/{id}")]
        public Project GetProject(int id)
        {
            return projects.FirstOrDefault(u => u.GetId() == id);
        }

        [HttpGet("room/{id}")]
        public Room GetRooms(int id)
        {
            return rooms.FirstOrDefault(u => u.GetId() == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
