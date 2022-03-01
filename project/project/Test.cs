using project.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static project.DB;

namespace project
{
    public class Test
    {
        public int a { get; set; }
        public string b { get; set; }
        
        public static ParameterOutput GetParameterOutput(List<Parameter> b, out ParameterOutput result)
        {         
            DB insert = new DB();
            return insert.StoreResuftOutput("usp_Inser_GetByID", b, out result);
        }
        public static string Insert(List<Parameter> b, out string result)
        {
            DB insert = new DB();
            return insert.StoreResuftOutput("usp_InsertTest", b, out result);
        }
        public static DataSet GetTestbyID(List<Parameter> b, out DataSet result)
        {
            DB insert = new DB();
            return insert.GetList("usp_Inser_GetByID", b, out result);
        }
    }
}
