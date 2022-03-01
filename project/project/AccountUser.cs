using project.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace project
{
    public class AccountUser
    {
        public int UserID { get; set; }
        /*[Required(ErrorMessage = "Bạn chưa nhập username")]*/
        public string FullName { get; set; }
        /*[Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]*/     
    }
    /*public static DataSet GetList(int DeptID, int Type, out DataSet ds)
    {
        Procedure a = new Procedure();
        return a.GetList("usp_Area_GetListByDeptID", "@UserID","@UserName", new { DeptID, Type }, out ds);
    }*/
}
