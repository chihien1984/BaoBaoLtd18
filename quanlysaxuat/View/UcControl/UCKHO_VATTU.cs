using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace quanlysanxuat
{
    public partial class UCKHO_VATTU : DevExpress.XtraEditors.XtraForm
    {
        public UCKHO_VATTU()
        {
            InitializeComponent();
        }
        private void LOADAD_MAGH()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select Top 1 convert(nvarchar,(DATEPART(HH,GetDate())))+':'+convert(nvarchar,DATEPART(MI,GetDate()))+':'+REPLACE(convert(nvarchar,GetDate(),3),'/','') from tbl01", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaPNK.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void GHI_NHAPKHO()
        {
            if (txtMaPNK.Text!="")
            {
                SqlConnection cn = new SqlConnection();
                decimal Soluongsanxuat = Convert.ToDecimal(txtTLNhapKho.Text);
                
                cn.ConnectionString = Connect.mConnect;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO tbl01(NgayLap,Ngaynhap,SochungTu,MaPDN,MaVL,TenVL,Diengiai,SL_DeNghi,TL_DeNghi, "
                 + " SL_ThucNhap, TL_ThucNhap, SL_Tinhgia, Donvi, Kemtheo, MaPSX, DinhMuc, NCC, NguoiGD)"
                 + "values (NgayLap,Ngaynhap,SochungTu,MaPDN,MaVL,TenVL,Diengiai,SL_DeNghi,TL_DeNghi, "
                 + " SL_ThucNhap, TL_ThucNhap, SL_Tinhgia, Donvi, Kemtheo, MaPSX, DinhMuc, NCC, NguoiGD)", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                try
                {
                    if (dpNgaylap.Text == "")

                        command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = dpNgaylap.Text;
                    if (dpketthuc.Text == "")

                        command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = dpketthuc.Text;
                }
                catch (Exception)
                {
                    throw;
                }
                command.Parameters.Add(new SqlParameter("@IDSP", SqlDbType.NVarChar)).Value = txtMaPDN.Text;               
                adapter.Fill(dt);              
                cn.Close();
            }
            else
            {
                MessageBox.Show("Không Thành Công", "THÔNG BÁO");
            }
        }
        private void LAYMAPHIEU_NHAPKHO(object sender, EventArgs e)
        {
            LOADAD_MAGH();
        }
        private void GHI_NHAPKHO(object sender,EventArgs e)
        {
            GHI_NHAPKHO();
        }
    }
}
