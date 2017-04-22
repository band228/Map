using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity
{
    public class EntitiesLogs
    {
        private string _LogsID;

        public string LogsID
        {
            get { return _LogsID; }
            set { _LogsID = value; }
        }
        private string _FormName;

        public string FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }
        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _Note;

        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        private DateTime _DateActivity;

        public DateTime DateActivity
        {
            get { return _DateActivity; }
            set { _DateActivity = value; }
        }

    }
}
