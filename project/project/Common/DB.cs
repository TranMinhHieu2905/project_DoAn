using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace project
{
    public class DB
    {        
            SqlConnection sqlcon;
            SqlCommand sqlcom;
            SqlDataReader sqldr;
            SqlDataAdapter sqlda = new SqlDataAdapter();
            /*        SqlParameter SqlDbType = new SqlParameter();*/
            string strcon = "Server=DESKTOP-6HLOTS1\\MSSQLSERVER01;Database=QLF0_DEV;Trusted_Connection=True;";
            public void OpenConnection()
            {
                sqlcon = new SqlConnection(strcon);
                if (sqlcon.State == System.Data.ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
            }
            public void CloseConnection()
            {
                sqlcon.Close();
            }
        
        public class Parameter
        {
            public string parameter { get; set; }
            public string type { get; set; }
            public int length { get; set; }
            public object value { get; set; }
        }
        public class ParameterGuidId
        {
            public string parameter { get; set; }
            public string type { get; set; }
            public int length { get; set; }
            public Guid value { get; set; }
        }
        public class ParameterOutput
        {
            public string paramOutput { get; set; }
            public int paramOutput1 { get; set; }
        }
        public static SqlDbType Type(string name)
        {
            switch (name.ToLower())
            {
                case "date":
                    return SqlDbType.Date;
                case "datetime":
                    return SqlDbType.DateTime;
                case "decimal":
                    return SqlDbType.Decimal;
                case "int":
                    return SqlDbType.Int;
                case "varchar":
                    return SqlDbType.VarChar;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "bit":
                    return SqlDbType.Bit;
                case "bigint":
                    return SqlDbType.BigInt;
                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;
                default:
                    return SqlDbType.VarChar;
            }
        }
        public bool InSert(string storeName, List<Parameter> parameters,out bool isSuccess)  
        {
            try
            {
                int result = 0;
                isSuccess = false;
                OpenConnection();
                sqlcom = new SqlCommand(storeName, sqlcon);
                sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (Parameter p in parameters)
                {
                    if (p.length > 0)
                        sqlcom.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                    else
                        sqlcom.Parameters.Add(p.parameter, Type(p.type)).Value = p.value;
                }
                result = sqlcom.ExecuteNonQuery();
                CloseConnection();
                isSuccess = true;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                isSuccess = false;
                return false;
            }                   
        }
        public DataSet GetList(string storeName, List<Parameter> parameters, out DataSet ds)
        {
            try
            {
                OpenConnection();
                sqlcom = new SqlCommand(storeName, sqlcon);
                sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
                ds = new DataSet();
                foreach (Parameter p in parameters)
                {
                    if (p.length > 0)
                        sqlcom.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                    else
                        sqlcom.Parameters.Add(p.parameter, Type(p.type)).Value = p.value;
                    sqlda = new SqlDataAdapter(sqlcom);
                }
                sqlda.Fill(ds);
                CloseConnection();
                return ds;             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetOne(string storeName, Parameter parameter, out DataSet ds)
        {
            try
            {
                OpenConnection();
                sqlcom = new SqlCommand(storeName, sqlcon);
                sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
                ds = new DataSet();
                    /*if (p.length > 0)
                        sqlcom.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                    else*/
                sqlcom.Parameters.Add(parameter.parameter, Type(parameter.type)).Value = parameter.value;
                sqlda = new SqlDataAdapter(sqlcom);
                
                sqlda.Fill(ds);
                CloseConnection();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetOne(string storeName, Parameter parameter, out DataTable ds)
        {
            try
            {
                OpenConnection();
                sqlcom = new SqlCommand(storeName, sqlcon);
                sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
                ds = new DataTable();
                /*if (p.length > 0)
                    sqlcom.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                else*/
                sqlcom.Parameters.Add(parameter.parameter, Type(parameter.type)).Value = parameter.value;
                sqlda = new SqlDataAdapter(sqlcom);

                sqlda.Fill(ds);
                CloseConnection();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetList(string storeName, List<Parameter> parameters, out DataTable ds)
        {
            try
            {
                OpenConnection();
                sqlcom = new SqlCommand(storeName, sqlcon);
                sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
                ds = new DataTable();
                foreach (Parameter p in parameters)
                {
                    if (p.length > 0)
                        sqlcom.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                    else
                        sqlcom.Parameters.Add(p.parameter, Type(p.type)).Value = p.value;
                    sqlda = new SqlDataAdapter(sqlcom);
                }
                sqlda.Fill(ds);
                CloseConnection();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetList(string storeName, out DataSet ds)
        {
            /*list = new List<AccountUser>();*/
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
            sqlda = new SqlDataAdapter(sqlcom);
            ds = new DataSet();
            sqlda.Fill(ds);
            CloseConnection();
            /*foreach (DataRow row in ds.Tables[0].Rows)
            {
                AccountUser account = new AccountUser();
                account.FullName = row["FullName"].ToString();
                account.UserID = row["UserID"].GetHashCode();
                list.Add(account);
            }*/
            return ds;
        }
        public DataTable GetList(string storeName, out DataTable ds)
        {
            /*list = new List<AccountUser>();*/
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
            sqlda = new SqlDataAdapter(sqlcom);
            ds = new DataTable();
            sqlda.Fill(ds);
            CloseConnection();
            /*foreach (DataRow row in ds.Tables[0].Rows)
            {
                AccountUser account = new AccountUser();
                account.FullName = row["FullName"].ToString();
                account.UserID = row["UserID"].GetHashCode();
                list.Add(account);
            }*/
            return ds;
        }
        public string StoreResuftOutput(string storeName, List<Parameter> parameters,out string result)
        {
            result = "";
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = CommandType.StoredProcedure;
            foreach (Parameter p in parameters)
            {
                if (p.length > 0)
                    sqlcom.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                else
                    sqlcom.Parameters.Add(p.parameter, Type(p.type)).Value = p.value;
            }
            sqlcom.Parameters.Add("@result", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;

            sqlcom.ExecuteNonQuery();
             result = (string)sqlcom.Parameters["@result"].Value;
            CloseConnection();
            return result;

        }
        public string Delete(string storeName, List<Parameter> parameters)
        {
            string msg= "";
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = CommandType.StoredProcedure;
            foreach (Parameter p in parameters)
            {
                if (p.length > 0)
                    sqlcom.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                else
                    sqlcom.Parameters.Add(p.parameter, Type(p.type)).Value = p.value;
            }
         /*   sqlcom.Parameters.Add("@result", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;*/

            sqlcom.ExecuteNonQuery();
           /* result = (string)sqlcom.Parameters["@result"].Value;*/
            CloseConnection();
            return msg;

        }
        public ParameterOutput StoreResuftOutput(string storeName, List<Parameter> parameters,out ParameterOutput result1)
        {
            
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = CommandType.StoredProcedure;
            foreach (Parameter p in parameters)
            {
                if (p.length > 0)
                    sqlcom.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                else
                    sqlcom.Parameters.Add(p.parameter, Type(p.type)).Value = p.value;
            }
            List<Parameter> parameter = new List<Parameter>();
            parameter.Add(new Parameter { parameter = "@resuft", length = 0, type = "nvarchar" });
            parameter.Add(new Parameter { parameter = "@resuft1", length = 0, type = "int"});
            foreach (Parameter p in parameter)
            {
                sqlcom.Parameters.Add(p.parameter,Type(p.type),200).Direction = ParameterDirection.Output;
            }        
            sqlcom.ExecuteNonQuery();
             result1 = new ParameterOutput();
            foreach (Parameter p in parameter)
            {
               
                if(p.type=="int")
                {
                    result1.paramOutput1 = (int)sqlcom.Parameters[p.parameter].Value;
                }
                else
                {
                    result1.paramOutput = (string)sqlcom.Parameters[p.parameter].Value;
                }
            }
            CloseConnection();
            return result1;

        }
        public int StoreResuftOutput(string storeName, List<Parameter> parameters, out int result)
        {
            result = 0;
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = CommandType.StoredProcedure;
            foreach (Parameter p in parameters)
            {
                if (p.length > 0)
                    sqlcom.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                else
                    sqlcom.Parameters.Add(p.parameter, Type(p.type)).Value = p.value;
            }
            sqlcom.Parameters.Add("@result", SqlDbType.Int, 200).Direction = ParameterDirection.Output;

            sqlcom.ExecuteNonQuery();
            result = (int)sqlcom.Parameters["@result"].Value;
            CloseConnection();
            return result;

        }
        public long StoreResuftOutput(string storeName, ParameterGuidId parameters, out long result)
        {
            result = 0;
            OpenConnection();
            /*parameters = new ParameterGuidId();*/
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add(parameters.parameter, SqlDbType.UniqueIdentifier).Value = parameters.value;
            sqlcom.Parameters.Add("@result", SqlDbType.BigInt, 500).Direction = ParameterDirection.Output;

            sqlcom.ExecuteNonQuery();
            result = (Int64)sqlcom.Parameters["@result"].Value;
            CloseConnection();
            return result;
        }
        public string StoreResuftOutput(string storeName, ParameterGuidId parameters, out string result)
        {
            result = "";
            OpenConnection();
            /*parameters = new ParameterGuidId();*/
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add(parameters.parameter, SqlDbType.UniqueIdentifier).Value = parameters.value;
            sqlcom.Parameters.Add("@result", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

            sqlcom.ExecuteNonQuery();
            result = (string)sqlcom.Parameters["@result"].Value;
            CloseConnection();
            return result;
        }
    }
}
