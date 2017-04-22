using System;
using System.Collections.Generic;

using System.Text;


namespace Common.Entity
{
    public class EntitiesSensorLogs
    {
        public string SensorLogsID { get; set; }
        public string ATMID { get; set; }
        public string ATMName { get; set; }
        public string StatusSensorID{get;set;}
        public string StatusSensorName { get; set; }
        public string Status { get; set; }
        public string StatusID { get; set; }
        public DateTime SensorLogsDate { get; set; }
        public string Description { get; set; }
        public string Handler { get; set; }
        public DateTime HandleDate { get; set; }
    }
}
