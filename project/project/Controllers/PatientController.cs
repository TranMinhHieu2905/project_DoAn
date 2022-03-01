using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Common;
using project.ObjectGuid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static project.DB;

namespace project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : Controller
    {       
        [HttpPost("Insert")]
        public Result Insert([FromBody] Patient patient)
        {
            int msg = 0;
            Result resulttrue = new Result(true);
            Result resultfalse = new Result(false);          
            msg= Insert_Validate(patient,out int result);
            if (result > 0) return resultfalse;
            else return resulttrue;
        }
        private int Insert_Validate(Patient patient,out int result)
        {
            result = 0;
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { parameter = "@PatientID", length = 0, type = "bigint", value = patient.PatientID });
            parameters.Add(new Parameter { parameter = "@HoTen", length = 0, type = "nvarchar", value = patient.HoTen });
            parameters.Add(new Parameter { parameter = "@NamSinh", length = 0, type = "int", value = patient.NamSinh });
            parameters.Add(new Parameter { parameter = "@GioiTinh", length = 0, type = "int", value = patient.GioiTinh });
            parameters.Add(new Parameter { parameter = "@CCCD", length = 0, type = "varchar", value = patient.CCCD });
            parameters.Add(new Parameter { parameter = "@SoDienThoai", length = 0, type = "varchar", value = patient.SoDienThoai });
            parameters.Add(new Parameter { parameter = "@LoaiDoiTuong", length = 0, type = "int", value = patient.LoaiDoiTuong });
            parameters.Add(new Parameter { parameter = "@MucDoNguyCo", length = 0, type = "int", value = patient.MucDoNguyCo });
            parameters.Add(new Parameter { parameter = "@ProvinceCode", length = 0, type = "varchar", value = patient.ProvinceCode });
            parameters.Add(new Parameter { parameter = "@DistrictCode", length = 0, type = "varchar", value = patient.DistrictCode });
            parameters.Add(new Parameter { parameter = "@WardCode", length = 0, type = "varchar", value = patient.WardCode });
            parameters.Add(new Parameter { parameter = "@DiaChiCT", length = 0, type = "nvarchar", value = patient.DiaChiCT });
            parameters.Add(new Parameter { parameter = "@NgheNghiep", length = 0, type = "nvarchar", value = patient.NgheNghiep });
            parameters.Add(new Parameter { parameter = "@QuocTich", length = 0, type = "nvarchar", value = patient.QuocTich });
            parameters.Add(new Parameter { parameter = "@DoiTuongDacBiet", length = 0, type = "int", value = patient.DoiTuongDacBiet });
            parameters.Add(new Parameter { parameter = "@DoiTuongDacBietKhac", length = 0, type = "nvarchar", value = patient.DoiTuongDacBietKhac });
            parameters.Add(new Parameter { parameter = "@TiemCovid", length = 0, type = "int", value = patient.TiemCovid });
            parameters.Add(new Parameter { parameter = "@DoiTuongThaiSan", length = 0, type = "bit", value = patient.DoiTuongThaiSan });
            parameters.Add(new Parameter { parameter = "@TuoiThai", length = 0, type = "int", value = patient.TuoiThai });
            parameters.Add(new Parameter { parameter = "@BHYT", length = 0, type = "varchar", value = patient.BHYT });
            parameters.Add(new Parameter { parameter = "@FloorType", length = 0, type = "int", value = patient.FloorType });
            parameters.Add(new Parameter { parameter = "@DeptIDProvince", length = 0, type = "int", value = patient.DeptIDProvince });
            parameters.Add(new Parameter { parameter = "@DeptIDDistrict", length = 0, type = "int", value = patient.DeptIDDistrict });
            parameters.Add(new Parameter { parameter = "@DeptIDWard", length = 0, type = "int", value = patient.DeptIDWard });          
            return Patient.Insert( parameters, out result);
        }
        [HttpPost("Delete")]
        public string Delete([FromBody] ObjGuid guid)
        {
            if (guid.ObjectGuid==Guid.Empty)
            {
                return Result.MessageError;
            }    
            long msg = 0;
            Result resulttrue = new Result(true);
            Result resultfalse = new Result(false);
            ParameterGuidId parameters = new ParameterGuidId() { parameter = "@ObjectGuid", length = 0, type = "uniqueidentifier", value = guid.ObjectGuid };
            msg = Patient.GetPatientIDbyOjectGuiID(parameters, out long result);
            if (result < 0) return Result.MessageError;
            Patient.Delete(result);
            return Result.MessageSuccess;
        }
        [HttpPost("GetList")]
        public List<Patient> GetList([FromBody] PatientInput input)
        {
            List <Patient> list = new List<Patient>();
            input.PageSize = input.PageSize == 0 ? 20 : input.PageSize;
            input.CurrentPage = input.CurrentPage == 0 ? 1 : input.CurrentPage;
            Patient.GetList(out DataTable ds);
            foreach (DataRow row in ds.Rows)
            {
                Patient patient = new Patient();
               /* patient.ObjectGuid = row["ObjectGuid"].GetType().ToString();*/
                patient.PatientID = row["PatientID"].GetHashCode();
                patient.HoTen = row["HoTen"].ToString();
                patient.GioiTinh = row["GioiTinh"].GetHashCode();
                patient.MaBenhNhan = row["MaBenhNhan"].ToString();
                list.Add(patient);
            }
            return list;
        }
    }
}
