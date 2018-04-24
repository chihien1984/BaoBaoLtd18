using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace quanlysanxuat
{
       class ketnoi
    {
        public SqlConnection kn=new SqlConnection();
        public int UpdateData(string sql, string[] Name, object[] value, int Nparameter)
        {
            sql = Mahoa.Decrypt(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            SqlConnection con = new SqlConnection();
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand Command = new SqlCommand(sql, con);
            for (int i = 0; i < Nparameter; i++)
            {
                Command.Parameters.AddWithValue(Name[i], value[i]);
            }
            Command.CommandType = CommandType.StoredProcedure;
            int kq = Command.ExecuteNonQuery();
            return kq;
        }
        public void kn_csdl()
        {
            try
            {
                kn.ConnectionString = Mahoa.Decrypt(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
                kn.Open();
            }
            catch (Exception)
            {
                Console.WriteLine("Không thể kết nối máy chủ");
            }
        }
        public string lay1giatri(string sql)
        {
            string kq = "";
            try
            {
                kn_csdl();
                SqlCommand sqlComm = new SqlCommand(sql, kn);
                SqlDataReader r = sqlComm.ExecuteReader();
                if (r.Read())
                {
                    kq = r["tong"].ToString();
                }
              }
            catch
            { }
            return kq;
        }


        public void dongketnoi()
        {
            if (kn.State == ConnectionState.Open)
            { kn.Close(); }
        }
        public DataTable bangdulieu = new DataTable();
        public DataTable laybang(string caulenh)
        {
            try
            {    kn_csdl();
                SqlDataAdapter Adapter = new SqlDataAdapter(caulenh, kn);
                DataSet ds = new DataSet();

                Adapter.Fill(bangdulieu);
            }
            catch (System.Exception)
            {
                bangdulieu = null;
            }
            finally
            {
                dongketnoi();
            }

            return bangdulieu;
        }
        public int xulydulieu(string caulenhsql)
        {
            int kq = 0;
            try
            {
                kn_csdl();
                SqlCommand lenh = new SqlCommand(caulenhsql, kn);
                kq = lenh.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //Thông báo lỗi ra!
                
                kq = 0;
            }
            finally
            {
                dongketnoi();
            }
                return kq;
        }
        public SqlConnection cmd { get; set; }
    }
}

