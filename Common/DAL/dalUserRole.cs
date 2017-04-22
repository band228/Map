using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Common.DataConfig;
using Common.Entity;

namespace Common.DAL
{
    public class dalUserRole : Provider
    {
        #region User
        public DataTable GetUserName(string UserName)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@UserName", UserName));
            return ExecuteProc("User_GetUser", sqlpa);
        }
        public DataTable GetHandler()
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            return ExecuteProc("User_GetHandler", sqlpa);
        }
        public DataTable GetUserByUserID(int UserID)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@UserID", UserID));
            return ExecuteProc("User_GetUserByUserID", sqlpa);
        }
        public DataTable Login(string UserName, string PassWord)
        {
            string PassEncode = new TextHelper().EncodePass(PassWord);
            string a = PassEncode.Length.ToString();
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@UserName", UserName));
            sqlpa.Add(new SqlParameter("@PassWord", PassEncode));
            return ExecuteProc("Users_CheckLogin", sqlpa);
        }
        public bool InsertUser(UserEntity user)
        {
            string PassEncode = new TextHelper().EncodePass(user.PassNew);
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@UserName", user.UserName));
            sqlpa.Add(new SqlParameter("@Pass", PassEncode));
            sqlpa.Add(new SqlParameter("@GroupRoleID", user.GroupRoleID));
            sqlpa.Add(new SqlParameter("@Name", user.Name));
            sqlpa.Add(new SqlParameter("@Birthday", user.Birthday));
            sqlpa.Add(new SqlParameter("@Address", user.Address));
            sqlpa.Add(new SqlParameter("@PhoneNumber", user.PhoneNumber));
            sqlpa.Add(new SqlParameter("@Email", user.Email));
            sqlpa.Add(new SqlParameter("@Note", user.Note));
            sqlpa.Add(new SqlParameter("@Active", user.Active));
            sqlpa.Add(new SqlParameter("@CreatedUser", user.CreatedUser));
            sqlpa.Add(new SqlParameter("@CreatedDate", user.CreatedDate));
            return RunProc("Users_Insert", sqlpa);
        }
        public bool UpdateUser(UserEntity user)
        {
            string PassEncode = "";
            if(!string.IsNullOrEmpty(user.PassNew))
            {
                PassEncode = new TextHelper().EncodePass(user.PassNew);
            }
            else
            {
                PassEncode = user.PassWord;
            }
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@UserID", user.UserID));
            sqlpa.Add(new SqlParameter("@UserName", user.UserName));
            sqlpa.Add(new SqlParameter("@Pass", PassEncode));
            sqlpa.Add(new SqlParameter("@GroupRoleID", user.GroupRoleID));
            sqlpa.Add(new SqlParameter("@Name", user.Name));
            sqlpa.Add(new SqlParameter("@Birthday", user.Birthday));
            sqlpa.Add(new SqlParameter("@Address", user.Address));
            sqlpa.Add(new SqlParameter("@PhoneNumber", user.PhoneNumber));
            sqlpa.Add(new SqlParameter("@Email", user.Email));
            sqlpa.Add(new SqlParameter("@Note", user.Note));
            sqlpa.Add(new SqlParameter("@Active", user.Active));
            sqlpa.Add(new SqlParameter("@ModifiedUser", user.ModifiedUser));
            sqlpa.Add(new SqlParameter("@ModifiedDate", user.ModifiedDate));
            return RunProc("Users_Update", sqlpa);
        }
      
        public DataTable ChangePassword(string UserID, string PassOld, string PassNew)
        {
            string PassOldEncode = new TextHelper().EncodePass(PassOld);
            string PassNewEncode = new TextHelper().EncodePass(PassNew);
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@UserID", UserID));
            sqlpa.Add(new SqlParameter("@PassOld", PassOldEncode));
            sqlpa.Add(new SqlParameter("@PassNew", PassNewEncode));
            return ExecuteProc("Users_ChangePassword", sqlpa);
        }
        public bool DeleteUser(string strSQL)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@stringSQL", strSQL));
            return RunProc("Users_Deletelist", sqlpa);
        }
        #endregion
        #region Role
        public DataTable GetMenuByRole(string RoleID)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@GroupRoleID", RoleID));
            return ExecuteProc("RoleMenu_GetByRoleID", sqlpa);
        }
        public DataTable GetAllGroupRole()
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            return ExecuteProc("GroupRole_GetAll", sqlpa);
        }
        public DataTable GetMenuChild(string FatherID)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@FatherID", FatherID));
            return ExecuteProc("Menu_GetChild", sqlpa);
        }
        public DataTable GetMenuFather()
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            return ExecuteProc("Menu_GetFather", sqlpa);
        }
        public DataTable GetRolebyRoleName(string GroupRoleName)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@GroupRoleName", GroupRoleName));
            return ExecuteProc("Role_GetByName", sqlpa);
        }
        public DataTable CheckRoleName(string GroupRoleID, string GroupRoleName)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@GroupRoleID", GroupRoleID));
            sqlpa.Add(new SqlParameter("@GroupRoleName", GroupRoleName));
            return ExecuteProc("Role_CheckByNameID", sqlpa);
        }
        public bool InsertRole(GroupRoleEntity RE)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@GroupRoleName", RE.GroupRoleName));
            sqlpa.Add(new SqlParameter("@CreatedDate", RE.CreatedDate));
            sqlpa.Add(new SqlParameter("@CreatedUser", RE.CreatedUser));
            return RunProc("Role_Insert", sqlpa);
        }
        public bool UpdateRole(GroupRoleEntity RE)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@GroupRoleID", RE.GroupRoleID));
            sqlpa.Add(new SqlParameter("@GroupRoleName", RE.GroupRoleName));
            sqlpa.Add(new SqlParameter("@ModifiedDate", RE.ModifiedDate));
            sqlpa.Add(new SqlParameter("@ModifiedUser", RE.ModifiedUser));
            return RunProc("Role_Update", sqlpa);
        }
        public bool DeleteRole(string GroupRoleID)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@GroupRoleID", GroupRoleID));
            return RunProc("Role_Delete", sqlpa);
        }
        public bool DeleteRoleMenu(string RoleID)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@GroupRoleID", RoleID));
            return RunProc("RoleMenu_Delete", sqlpa);
        }
        public bool InsertRoleMenu(string SQL)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@strSQL", SQL));
            return RunProc("RoleMenu_Insert", sqlpa);
        }
        public DataTable RoleMenuByUserID(string UserID)
        {
            List<SqlParameter> sqlpa = new List<SqlParameter>();
            sqlpa.Add(new SqlParameter("@UserID", UserID));
            return ExecuteProc("RoleMenu_GetByUserID", sqlpa);
        }
        #endregion
        
    }
}
