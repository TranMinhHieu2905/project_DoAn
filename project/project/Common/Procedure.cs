using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace project.Common
{
    public class Procedure
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
       public string LoadTen(out List<AccountUser> list)
        {
            list = new List<AccountUser>();
            OpenConnection();
            sqlcom = new SqlCommand("usp_Area_GetListByDeptID", sqlcon);
            sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
            sqldr = sqlcom.ExecuteReader();
            while(sqldr.Read())
            {
                list.Add(new AccountUser
                {
                    FullName=sqldr["FullName"].ToString()
                });
            }
            return "";
        }
        public DataSet GetList(string storeName,out DataSet ds)
        {
            /*list = new List<AccountUser>();*/
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
            sqlda = new SqlDataAdapter(sqlcom);
             ds = new DataSet();
            sqlda.Fill(ds);
            /*foreach (DataRow row in ds.Tables[0].Rows)
            {
                AccountUser account = new AccountUser();
                account.FullName = row["FullName"].ToString();
                account.UserID = row["UserID"].GetHashCode();
                list.Add(account);
            }*/
            return ds;
        }
        public DataSet GetList<T,TL>(string storeName, TL param , T parameter ,out DataSet ds)
        {
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcom.Parameters.AddWithValue(param.ToString(), parameter);
            sqlda = new SqlDataAdapter(sqlcom);
            ds = new DataSet();
            sqlda.Fill(ds);
            CloseConnection();
            return ds;
        }
        public DataSet GetList<T, TL>(string storeName, List<TL> param1, List<T> parameter1 , out DataSet ds)
        {
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
            ds = new DataSet();
            for(int i =0;i< param1.Count();i++)
            {
                TL item1 = param1[i];
                T value1 = parameter1[i];
                sqlcom.Parameters.AddWithValue(item1.ToString(), value1);
                sqlda = new SqlDataAdapter(sqlcom);       
            }
            sqlda.Fill(ds);
            CloseConnection();
            return ds;
        }
        public bool Insert<T, TL>(string storeName, List<TL> param1, List<T> parameter1, out bool isSuccess)
        {
            isSuccess = false;
            try
            {
                OpenConnection();
                int result = 0;
                sqlcom = new SqlCommand(storeName, sqlcon);
                sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
                for (int i = 0; i < param1.Count(); i++)
                {
                    TL item1 = param1[i];
                    T value1 = parameter1[i];
                    sqlcom.Parameters.AddWithValue(item1.ToString(), value1);
                    sqlda = new SqlDataAdapter(sqlcom);
                    result = sqlcom.ExecuteNonQuery();
                    CloseConnection();
                    isSuccess = true;
                   
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                isSuccess = false;
                return false;
            }  
        }
        public DataSet GetOne<T, TL>(string storeName, TL param, T parameter, out DataSet ds)
        {
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcom.Parameters.AddWithValue(param.ToString(), parameter);
            sqlda = new SqlDataAdapter(sqlcom);
            ds = new DataSet();
            sqlda.Fill(ds);
            CloseConnection();
            return ds;
        }
        public bool Insert<T,TL,L,K>(string storeName, T param1, TL param2,L value1,K value2, out bool isSuccess)
        {
             isSuccess = false;
            try
            {
                OpenConnection();
                int result = 0;
                sqlcom = new SqlCommand(storeName, sqlcon);
                sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue(param1.ToString(), value1);
                sqlcom.Parameters.AddWithValue(param2.ToString(), value2);
                result = sqlcom.ExecuteNonQuery();
                CloseConnection();
                isSuccess = true;
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
                isSuccess = false;
                return false;
            }          
        }
        public bool Inserts
        <A1,A2,A3,A4,A5,A6,A7,A8,A9,A10,A11,A12,A13,A14,A15,A16,A17,A18,A19,A20,
        A21,A22,A23,A24/*,A25,A26,A27,A28,A29,A30,A31,A32*/,
        B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24/*,B25,B26,B27,B28,B29,B30,B31,B32*/>
       (string storeName, A1 param1, A2 param2,A3 param3, A4 param4,A5 param5,
        A6 param6, A7 param7, A8 param8, A9 param9, A10 param10,
        A11 param11, A12 param12, A13 param13, A14 param14, A15 param15, A16 param16,
        A17 param17, A18 param18, A19 param19, A20 param20, A21 param21, A22 param22, 
        A23 param23, A24 param24/*, A25 param25, A26 param26, A27 param27, A28 param28, A29 param29, A30 param30, 
        A31 param31, A32 param32*/,
        B1 value1, B2 value2, B3 value3, B4 value4, B5 value5, B6 value6, B7 value7, B8 value8, B9 value9, B10 value10, B11 value11, 
        B12 value12, B13 value13, B14 value14, B15 value15, B16 value16, B17 value17, B18 value18, B19 value19, B20 value20, B21 value21, B22 value22, B23 value23, 
        B24 value24,  out bool isSuccess)
        {
             isSuccess = false;
            try
            {
                OpenConnection();
                int result = 0;
                sqlcom = new SqlCommand(storeName, sqlcon);
                sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue(param1.ToString(), value1);
                sqlcom.Parameters.AddWithValue(param2.ToString(), value2);
                sqlcom.Parameters.AddWithValue(param3.ToString(), value3);
                sqlcom.Parameters.AddWithValue(param4.ToString(), value4);
                sqlcom.Parameters.AddWithValue(param5.ToString(), value5);
                sqlcom.Parameters.AddWithValue(param6.ToString(), value6);
                sqlcom.Parameters.AddWithValue(param7.ToString(), value7);
                sqlcom.Parameters.AddWithValue(param8.ToString(), value8);
                sqlcom.Parameters.AddWithValue(param9.ToString(), value9);
                sqlcom.Parameters.AddWithValue(param10.ToString(), value10);
                sqlcom.Parameters.AddWithValue(param11.ToString(), value11);
                sqlcom.Parameters.AddWithValue(param12.ToString(), value12);
                sqlcom.Parameters.AddWithValue(param13.ToString(), value13);
                sqlcom.Parameters.AddWithValue(param14.ToString(), value14);
                sqlcom.Parameters.AddWithValue(param15.ToString(), value15);
                sqlcom.Parameters.AddWithValue(param16.ToString(), value16);
                sqlcom.Parameters.AddWithValue(param17.ToString(), value17);
                sqlcom.Parameters.AddWithValue(param18.ToString(), value18);
                sqlcom.Parameters.AddWithValue(param19.ToString(), value19);
                sqlcom.Parameters.AddWithValue(param20.ToString(), value20);
                sqlcom.Parameters.AddWithValue(param21.ToString(), value21);
                sqlcom.Parameters.AddWithValue(param22.ToString(), value22);
                sqlcom.Parameters.AddWithValue(param23.ToString(), value23);
                sqlcom.Parameters.AddWithValue(param24.ToString(), value24);
                result = sqlcom.ExecuteNonQuery();
                CloseConnection();
                isSuccess = true;
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
                isSuccess = false;
                return false;
            }
            
        }
        /*public static string StoreResuftOutput(string storeName, List<Parameter> parameters)
        {
            clsconnect cls = new clsconnect();
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandText = "{call " + storeName + "}";
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (Parameter p in parameters)
            {
                if (p.length > 0)
                    cmd.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                else
                    cmd.Parameters.Add(p.parameter, Type(p.type)).Value = p.value;
            }
            cmd.Parameters.Add("@resuft", OdbcType.VarChar, 200).Direction = ParameterDirection.Output;

            cls.connect_Data();
            cmd.Connection = cls.con;
            cmd.ExecuteNonQuery();
            string resuft = (string)cmd.Parameters["@resuft"].Value;
            cls.close_Data();
            return resuft;

        }*/
       /* public static DataTable Store(string storeName, List<Parameter> parameters)
        {
            clsconnect cls = new clsconnect();
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandText = "{call " + storeName + "}";
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (Parameter p in parameters)
            {
                if (p.length > 0)
                    cmd.Parameters.Add(p.parameter, Type(p.type), p.length).Value = p.value;
                else
                    cmd.Parameters.Add(p.parameter, Type(p.type)).Value = p.value;
            }
            DataTable dataTable = new DataTable();
            cls.connect_Data();
            cmd.Connection = cls.con;
            dataTable.Load(cmd.ExecuteReader());
            cls.close_Data();
            return dataTable;
        }*/
        /*public string GetList<TL>(string storeName, out List<TL> list)
        {
            list = new List<TL>();
            OpenConnection();
            sqlcom = new SqlCommand(storeName, sqlcon);
            sqlcom.CommandType = System.Data.CommandType.StoredProcedure;
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                foreach(var item in sqldr.ToString())
                {
                    string data = sqldr[0].ToString();
                    string data1 = sqldr[1].ToString();
                }    
            }
            return "";
        }*/

    }
}
