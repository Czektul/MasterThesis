using ProjectClasses.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ProjectExtensions
    {
        public static string ProjectUsers(this Project project)
        {
            string users = string.Empty;
            try
            {
                foreach (User user in project.Users)
                { 
                    users = users + user.Name + ", ";
                }
                users = users.Substring(0, users.Length - 2);

                return users;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
