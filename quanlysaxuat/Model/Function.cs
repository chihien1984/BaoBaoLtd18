using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.Model
{
    class Function
    {
        public static SqlConnection Con;//Khai báo đối tượng kết nối        
        public static void ConnectChamCong()
        {
            Con = new SqlConnection();//Khởi tạo đối tượng
            Con.ConnectionString = @"Data Source=192.168.1.22;Initial Catalog = datachamcong moi; Persist Security Info=True;User ID = Data; Password=6ZF4T%69^#u8";
            Con.Open();//Mở kết nối
            //Kiểm tra kết nối
            if (Con.State == ConnectionState.Open);
            //MessageBox.Show("Kết nối thành công");
            else MessageBox.Show("Không thể kết nối với dữ liệu");
        }
        public static void ConnectSanXuat()
        {
            try
            {
                Con = new SqlConnection();//Khởi tạo đối tượng
                Con.ConnectionString = @"Data Source=192.168.1.22;Initial Catalog = SANXUAT; Persist Security Info=True; User ID = Data; Password=6ZF4T%69^#u8";
                if (Con.State == ConnectionState.Closed)
                { 
                    Con.Open();
                }
                //Con.Open();//Mở kết nối
                //Kiểm tra kết nối
                else
                {
                    MessageBox.Show("Error Connection", "Message");return;
                }
            }
            catch{ MessageBox.Show("Error Connection","Message");return; }  
        }
        public static void Disconnect()
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();   	//Đóng kết nối
                Con.Dispose(); 	//Giải phóng tài nguyên
                Con = null;
            }
        }
        //Lấy dữ liệu vào bảng
        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            //Tạo đối tượng thuộc lớp SqlCommand
            dap.SelectCommand = new SqlCommand();
            dap.SelectCommand.Connection = Model.Function.Con; //Kết nối cơ sở dữ liệu
            dap.SelectCommand.CommandText = sql; //Lệnh SQL
            //Khai báo đối tượng table thuộc lớp DataTable
            DataTable table = new DataTable();
            dap.Fill(table);
            return table;
        }
        //Lấy dữ liệu vào bảng
        public static DataTable GetDataTable(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            //Khai báo đối tượng table thuộc lớp DataTable
            DataTable table = new DataTable();
            dap.Fill(table); //Đổ kết quả từ câu lệnh sql vào table
            return table;
        }
    }
}
