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
    public partial class frmNHOM_KHUON : DevExpress.XtraEditors.XtraForm
    {
  
        public frmNHOM_KHUON()
        {
            InitializeComponent();
        }
        Clsketnoi knn = new Clsketnoi();
     
        public static string THONGTIN_MOI;
        string Gol = "";
        SqlCommand cmd;
        private void LoadGrid_nhomkhuon()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(" select Manhom_khuon,Tennhom_khuon,Manv,NV.HoTen,Ngaytao "
                                                +" from tblDMNHOM_KHUON NK left join tblDSNHANVIEN NV "
                                                +" on NK.Manv = NV.Sothe and Manhom_khuon "
                                                +" like N'" + txtMa_nhomKhuon.Text+"'");
        }
        private void show_DMNhomKhuon_Click(object sender, EventArgs e)//Danh muc nhom khuon
        {
            LoadGrid_nhomkhuon();
        }
        private void frmNHOM_KHUON_Load(object sender, EventArgs e)
        {
            txtuser.Text=Login.Username; MaNV();
        }
        private void MaNV()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Sothe from tblDSNHANVIEN where HoTen like N'" + txtuser.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMa_user.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtMa_nhomKhuon.Text = gridView1.GetFocusedRowCellDisplayText(Manhomkhuon_grid1);
            txtTen_nhomkhuon.Text = gridView1.GetFocusedRowCellDisplayText(Tennhomkhuon_grid1);
            txtNguoitao.Text = gridView1.GetFocusedRowCellDisplayText(Manv_grid1);
            txtMa_user.Text= gridView1.GetFocusedRowCellDisplayText(Tennv_grid1);
            dpNgayLap.Text= gridView1.GetFocusedRowCellDisplayText(ngaylap_grid1);
        }
        private bool kiemtranhomkhuon()
        {
            bool tatkt = false;
            string MaSP = txtMa_nhomKhuon.Text;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select Manhom_khuon from tblDMNHOM_KHUON", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (MaSP == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private void btnthem_Click(object sender, EventArgs e)
        {
            if (txtMa_nhomKhuon.Text == "" && txtTen_nhomkhuon.Text == "")
            {
                MessageBox.Show("Cần thêm đủ nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (kiemtranhomkhuon())
            {
                MessageBox.Show("Mã '" + txtMa_nhomKhuon.Text + "' đã tồn tại, Không thể thêm mã nhóm khuôn trùng");
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["project"].ConnectionString;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into tblDMNHOM_KHUON "
                        + " (Manhom_khuon,Tennhom_khuon,Manv,Ngaytao) "
                        + " values (@Manhom_khuon,@Tennhom_khuon,@Manv,GetDate())", con))
                    {
                        cmd.Parameters.Add("@Manhom_khuon", SqlDbType.NVarChar).Value = txtMa_nhomKhuon.Text;
                        cmd.Parameters.Add("@Tennhom_khuon", SqlDbType.NVarChar).Value = txtTen_nhomkhuon.Text;
                        cmd.Parameters.Add("@Manv", SqlDbType.NVarChar).Value = txtMa_user.Text;
                        cmd.ExecuteNonQuery();
                    }
                con.Close();LoadGrid_nhomkhuon();
                }
            }
        }
        private void btnsua_Click(object sender, EventArgs e)
        {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["project"].ConnectionString;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("update tblDMNHOM_KHUON set "
                        + " Tennhom_khuon=@Tennhom_khuon,Manv=@Manv,Ngaytao=GetDate() "
                        + " where Manhom_khuon like N'"+txtMa_nhomKhuon.Text+"'", con))
                    {
                        cmd.Parameters.Add("@Ma_nhom_sp", SqlDbType.NVarChar).Value = txtMa_nhomKhuon.Text;
                        cmd.Parameters.Add("@Tennhom_khuon", SqlDbType.NVarChar).Value = txtTen_nhomkhuon.Text;
                        cmd.Parameters.Add("@Manv", SqlDbType.NVarChar).Value = txtMa_user.Text;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close(); LoadGrid_nhomkhuon();
                }         
        }
        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (txtMa_nhomKhuon.Text != "" && MessageBox.Show("Bạn muốn xóa nhóm mã " + txtMa_nhomKhuon.Text + " có tên" + txtTen_nhomkhuon.Text + " ", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ketnoi kn = new ketnoi();
                gridControl1.DataSource = kn.xulydulieu("Delete tblDMNHOM_KHUON where Manhom_khuon like N'" + txtMa_nhomKhuon.Text + "'");
            }
            LoadGrid_nhomkhuon();
        }
        private void btnfresh_Click(object sender, EventArgs e)
        {
            LoadGrid_nhomkhuon();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMa_nhomKhuon.ReadOnly = false;txtTen_nhomkhuon.ReadOnly = false;
        }

        private void btnExportsx_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }
    }
}