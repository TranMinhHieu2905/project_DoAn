using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Common
{
    public class Result
    {       
        public const int Ok = 0;
        public const int Error = 1;
        public static Result ResultOk { get; }
        public int Status { get; set; }
        public object Object { get; set; }
        public bool isOk { get; set; } 
        public bool isError { get; set; }
        public static string Message { get; set; }
        public static string MessageDelete { get; set; } = "Đã xoá thành công";
        public static string MessageUpdate { get; set; } = "Đã sửa thành công";
        public static string MessageSuccess { get; set; } = "Đã thêm thành công";
        public static string MessageError { get; set; } = "Có lỗi, hãy xem lại";
        public Result(bool isActive)
        {
            if(isActive == true)
            {
                Status = 200;
                isOk = true;
                isError = false;
                Message = " Thêm mới bệnh nhân thành công ";
            }  
            else
            {
                Status = 401;
                isOk = false;
                isError = true;
                Message = " Thêm mới bệnh nhân thất bại ";
            }               
        }
    }
}
