using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlysanxuat
{
    class ClassUser
    {
        public static string User { get; set; }
    }
}

/*
 * 
Key Visual Studio 2019 Professional: NYWVH-HT4XC-R2WYW-9Y3CM-X4V3Y
Key Visual Studio 2019 Enterprise: BF8Y8-GN2QH-T84XB-QVY3B-RC4DF
 *   /////////////////
            //Nhận địa chỉ MAC
            ////////////////  
            string DanhSachMAC = "";
            NetworkInterface[] DanhSachCardMang = NetworkInterface.GetAllNetworkInterfaces();
            for (int i = 0; i < DanhSachCardMang.Length; i++)
            {
                PhysicalAddress DiaChiMAC = DanhSachCardMang[i].GetPhysicalAddress();
                DanhSachMAC += DanhSachCardMang[i].Name + " : ";
                byte[] ByteDiaChi = DiaChiMAC.GetAddressBytes();
                for (int j = 0; j < ByteDiaChi.Length; j++)
                {
                    DanhSachMAC += ByteDiaChi[j].ToString("X2");
                    if (j != ByteDiaChi.Length - 1)
                    {
                        DanhSachMAC += "-";
                    }
                }
                DanhSachMAC += "\r\n";
                txtMAC.Text += DanhSachMAC;
            }
 */
/* format font cho gridview và width của checkedbox trong gridview
griview.Appearance.Row.Font = new Font("Segoe UI", 8f);
griview.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
*/
/*
            try
            {
                int[] listRowList = this.gvChildTreeList.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChildTreeList.GetDataRow(listRowList[i]);
                    string strQuery = string.Format();
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
 */
/*
 public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
 */
//convert Date to string của ngày tháng năm tại thời điểm hiện tại
/*
  SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select Top 1 
				REPLACE(convert(nvarchar,GetDate(),11),'/','') 
				+replace(replace(left(CONVERT(time, GetDate()),12),':',''),'.','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtCodeReport.Text = Convert.ToString(reader[0]);
            reader.Close();
 */

//
/* Focus data Row
 gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["ID"]).ToString();
*/
/* Ways to Bind DataGridView in C# Windows Forms
 https://www.c-sharpcorner.com/UploadFile/deveshomar/ways-to-bind-datagridview-in-window-forms-C-Sharp/
 */