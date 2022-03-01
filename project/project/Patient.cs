using Microsoft.AspNetCore.Mvc;
using project.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static project.DB;

namespace project
{
    public class Patient
    {
        [JsonIgnore]
        public long PatientID { get; set; }
        public Guid ObjectGuid { get; set; }
        [JsonIgnore]
        public string MaBenhNhan { get; set; }
        /*[Required(ErrorMessage = "Hãy nhập tên bệnh nhân")]*/
        public string HoTen { get; set; }
      /*  [Required(ErrorMessage = "Hãy nhập năm sinh")]*/
        public int NamSinh { get; set; }
       /* [Required(ErrorMessage = "Hãy nhập giới tính")]*/
        public int GioiTinh { get; set; }
       /* [Required(ErrorMessage = "Hãy nhập số điện thoại")]*/
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        /*[JsonIgnore]*/
        public int LoaiDoiTuong { get; set; }
        /*[Required(ErrorMessage = "Hãy nhập ngày có kết quả xét nghiệm")]*/
       /* public DateTime NgayCoKetQuaXN { get; set; } = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        [JsonIgnore]
        public long NgayCoKetQuaXN_Tick { get; set; }*/
        [JsonIgnore]
        public int TinhTrang { get; set; }
        [JsonIgnore]
        public int NVYTID { get; set; }
        [JsonIgnore]
        public int PhacDoDieuTriID { get; set; }
       /* [Required(ErrorMessage = "Hãy nhập tình thành")]
        [AssertThat("checkProvinceCode", ErrorMessage = "Tỉnh Thành không hợp lệ")]*/
        public string ProvinceCode { get; set; }
       /* [Required(ErrorMessage = "Hãy nhập quận huyện")]
        [AssertThat("checkDistrictCode", ErrorMessage = "Quận Huyện không hợp lệ")]*/
        public string DistrictCode { get; set; }
      /*  [Required(ErrorMessage = "Hãy nhập xã phường")]
        [AssertThat("checkWardCode", ErrorMessage = "Xã Phường không hợp lệ")]*/
        public string WardCode { get; set; }
      /*  [Required(ErrorMessage = "Hãy nhập địa chỉ chi tiết")]*/
        public string DiaChiCT { get; set; }
      /*  [Required(ErrorMessage = "Hãy nhập đơn vị xét nghiệm")]*/
        public string NgheNghiep { get; set; }
        public string QuocTich { get; set; }
        public int DoiTuongDacBiet { get; set; }
        public string DoiTuongDacBietKhac { get; set; }
        public int TiemCovid { get; set; }
     /*   [Required(ErrorMessage = "Hãy nhập chạy thận chu kỳ")]*/
        public bool ChayThanChuKy { get; set; }
       /* [Required(ErrorMessage = "Hãy nhập đối tượng thai sản")]*/
        public bool DoiTuongThaiSan { get; set; }
        public int TuoiThai { get; set; }
        public string BHYT { get; set; }
        public string MaDKBD { get; set; }
        [JsonIgnore]
        public int UserIDCreate { get; set; }
        [JsonIgnore]
        public int MucDoNguyCo { get; set; }
        public int FloorType { get; set; }
        public int PointTotal { get; set; }
       /* public int LoaiXN { get; set; }
        public int KetQuaXN { get; set; }*/
        public DateTime NgayXN { get; set; } = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        [JsonIgnore]
        public long NgayXN_Tick { get; set; }
        [JsonIgnore]
        public int DeptIDProvince { get; set; }
        [JsonIgnore]
        public int DeptIDDistrict { get; set; }
        [JsonIgnore]
        public int DeptIDWard { get; set; }
        public static int Insert(List<Parameter> parameters, out int result)
        {            
            DB insert = new DB();
            return insert.StoreResuftOutput("usp_Patient_Insert", parameters, out result);
        }
        public static long GetPatientIDbyOjectGuiID(ParameterGuidId guid,out long result)
        {
            result = 0;
            DB insert = new DB();
            insert.StoreResuftOutput("usp_Patient_GetIDByObjectGuid", guid, out result);
            return result;
        }
        public static long Delete(long PatientID)
        {
            long msg =0;
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { parameter = "@PatientID", length = 0, type = "bigint", value = PatientID });
            DB insert = new DB();
            insert.Delete("usp_Patient_DeleteByPatientID", parameters);
            return msg;
        }
        public static DataTable GetList(out DataTable ds)
        {
            DB insert = new DB();
            return insert.GetList("usp_Patient_GetList", out ds);
        }
    }
    public class PatientInput
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        public string WardCode { get; set; }
        public string TextSearch { get; set; }
        public int FloorType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TinhTrang { get; set; }
        public PatientInput()
        {
            PageSize = 20;
            CurrentPage = 1;
            ProvinceCode = "";
            DistrictCode = "";
            WardCode = "";
            TextSearch = "";
            FromDate = DateTime.Parse("1900-01-01");
            ToDate = DateTime.Parse("1900-01-01");
            TinhTrang = 0;
        }
    }
}
