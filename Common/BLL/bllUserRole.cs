using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Common.DAL;
using Common.Entity;

namespace Common.BLL
{
    public class bllUserRole
    {
        dalUserRole dal = new dalUserRole();
        #region User
        public DataTable GetUserByUserID(int UserID)
        {
            return dal.GetUserByUserID(UserID);
        }
        public DataTable GetUserByUserName(string UserName)
        {
            return dal.GetUserName(UserName);
        }
        public DataTable GetAllUser()
        {
            return dal.GetUserName("");
        }
       
        public string ChangePassword(string UserID, string PassOld,string PassNew)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp = dal.ChangePassword(UserID, PassOld, PassNew);
                return dtTemp.Rows[0][0].ToString();
            }
            catch(Exception ex)
            {
                return "Error :"+ ex;
            }
        }
        public bool Login(string Username, string Password,ref string ID)
        {
            if (dal.Login(Username, Password).Rows.Count > 0)
            {
                ID = dal.Login(Username, Password).Rows[0][0].ToString();
                return true;
            }
            else
                return false;
        }
        public bool InsertUser(UserEntity user)
        {
            return dal.InsertUser(user);
        }
        public bool UpdateUser(UserEntity user)
        {
            return dal.UpdateUser(user);
        }
        public bool DeleteUser(string id)
        {
            string sql = "";
            sql += " delete Users where UserID = " + id + "; ";

            return dal.DeleteUser(sql);
        }

        public DataTable GetHandler()
        {
            return dal.GetHandler();
        }
        #endregion
        #region Role
        public DataTable GetMenuByRole(string RoleID)
        {
            return dal.GetMenuByRole(RoleID);
        }
        public DataTable GetAllGroupRole()
        {
            return dal.GetAllGroupRole();
        }
        public DataTable GetMenuChild(string FatherID)
        {
            return dal.GetMenuChild(FatherID);
        }
        public DataTable GetMenuFather()
        {
            return dal.GetMenuFather();
        }
        public bool InsertRole(GroupRoleEntity RE)
        {
            return dal.InsertRole(RE);
        }
        public bool UpdateRole(GroupRoleEntity RE)
        {
            return dal.UpdateRole(RE);
        }
        public bool DeleteRole(string GroupRoleID)
        {
            return dal.DeleteRole(GroupRoleID);
        }
        public bool DeleteRoleMenu(string RoleID)
        {
            return dal.DeleteRoleMenu(RoleID);
        }
        public bool InsertRoleMenu(string SQL)
        {
            return dal.InsertRoleMenu(SQL);
        }
        public DataTable GetRolebyRoleName(string GroupRoleName)
        {
            return dal.GetRolebyRoleName(GroupRoleName);
        }
        public bool CheckRoleName(string GroupRoleName,string GroupRoleID)
        {
            DataTable dttemp = new DataTable();
            if (string.IsNullOrEmpty(GroupRoleID))
            {
                dttemp = dal.GetRolebyRoleName(GroupRoleName);
            }
            else
            {
                dttemp = dal.CheckRoleName(GroupRoleID, GroupRoleName);
            }

            if(dttemp.Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable RoleMenuByUserID(string UserID)
        {
            return dal.RoleMenuByUserID( UserID);
        }
        #endregion
        
        
    }
}
