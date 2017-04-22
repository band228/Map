using System;
using System.Collections.Generic;
using System.Text;


namespace Common.Entity
{
    public class ATMEntity
    {
        public int ATMID { get; set; }
        public string ATMName { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }
        public string ImageServerID { get; set; }
        public string CameraID { get; set; }
        public string IPLan { get; set; }
        public string Port { get; set; }
        public string License { get; set; }
        public string Serial { get; set; }
        public string IPWan { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public bool Active { get; set; }
        public int SecurityID { get; set; }
        public int Village { get; set; }
        public int City { get; set; }
        public int District { get; set; }
    }
}
