using System;
using System.Collections.Generic;

namespace ErpServer.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int? PermissionId { get; set; }
    }
}
