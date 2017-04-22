using System;
using System.Collections.Generic;
using System.Text;


namespace Common.Entity
{
    public class RoleEntity
    {
        public int GroupRoleID { get; set; }
        public string GroupRoleName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        
    }
}
