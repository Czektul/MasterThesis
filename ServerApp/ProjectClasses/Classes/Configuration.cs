using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClasses.Classes
{
    /// <summary>
    /// Configuration of server addresses. Singleton type class. 
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Server addres. Client application send data to this. 
        /// </summary>
        public static string ServerAddress = @"http://localhost:52435/RestService.svc/api";

        /// <summary>
        /// Database connection string.
        /// </summary>
        public static string ConnectionString = @"Server=.;Database=REST_System;User ID=sa;Password=TestPass!;";

    }
}

