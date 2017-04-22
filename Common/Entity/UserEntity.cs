﻿using System;
using System.Collections.Generic;
using System.Text;


namespace Common.Entity
{
    public class UserEntity
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string PassNew { get; set; }
        public int GroupRoleID { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public bool Active { get; set; }
        
    }
}
