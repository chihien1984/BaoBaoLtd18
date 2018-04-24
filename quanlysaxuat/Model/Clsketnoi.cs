using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace quanlysanxuat
{
    class Clsketnoi
    {
        SqlConnection con = new SqlConnection();
        string ketnoi = Mahoa.Decrypt(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
        public Clsketnoi()
        {
            try
            {
                con.ConnectionString = ketnoi;
                if (con.State == ConnectionState.Closed)
                con.Open();
            }
            catch (Exception) { Console.WriteLine("lỗi kết nối");} 
        }
        public DataTable laydulieu(string sql)
        {
            SqlCommand command = new SqlCommand(sql,con);
            SqlDataAdapter adpter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adpter.Fill(dt);
            return dt;
        }
        public DataTable laydulieu(string sql, string []name,object []value,int Nparameter)
        {
            SqlCommand command = new SqlCommand(sql, con);
            for (int i = 0; i < Nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        public int Update(string sql)
        {
            SqlCommand command = new SqlCommand(sql, con);
            return command.ExecuteNonQuery();
        }
        public int Update (string sql,string []name,object []value, int Nparameter)
        {
            SqlCommand command = new SqlCommand(sql, con);
            for (int i = 0; i < Nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            return command.ExecuteNonQuery();
        }
    }

}