using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.IO;

namespace quanlysanxuat
{
    public partial class UC_KHO_KHUON : DevExpress.XtraEditors.XtraForm
    {
        string Gol = "";
       
        public UC_KHO_KHUON()
        {InitializeComponent();}
        private void LoadKHO_KHUON()
        {
            ketnoi Connect = new ketnoi();
            gridControl1.DataSource = Connect.laybang("SELECT Ngaykh_cat,Ngaylukho,Ngaycat_Hoanthanh,Ngayrap_Hoanthanh,BPsudung,Ngaymuon,Ngaytra,Ma_khuon,DM.Mact,DM.Masp,SP.Tensp,Manhom_khuon,Ten_khuon,Dacdiem_khuon,Soluong_khuon, "
                   + " Ngaylap, Ngaybatdau, Ngayhoanthanh, Nguoilap, DM.Manv, Ghichu,GETDATE() as Today, DM.Ngaycat_Hoanthanh, DM.Ngayrap_Hoanthanh "
                   + " , DM.Mabp, DM.BPsudung, DM.Nguoiluukhuon, DM.Vitrikhuon, DM.Tinhtrang_khuon, DM.CodeMK from tblDM_KHUON DM left "
                   + " join tblSANPHAM_CT CT on DM.Mact = CT.Mact join (select Tensp, Masp from tblSANPHAM) SP on CT.Masp = SP.Masp"
                   + " where convert(Date,Ngaybatdau,103) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' ");
        }

        private void btnCapNhat_ngayHT_Click(object sender, EventArgs e)
        {
            if (txtCodeMK.Text != "")/// insert cách truyền tham số parameters
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = Connect.mConnect;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand command = new SqlCommand("update tblDM_KHUON set Ngaylukho =@Ngaylukho,Tinhtrang_khuon=@Tinhtrang_khuon, "
                    + " Nguoiluukhuon=@Nguoiluukhuon,Vitrikhuon=@Vitrikhuon,BPsudung=@BPsudung, "
                    + " Mabp=@Mabp,Ngaymuon=@Ngaymuon,Ngaytra=@Ngaytra where CodeMK like '" + txtCodeMK.Text + "'", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                try
                {
                    if (dpNgayluuKho.Text == "")
                        command.Parameters.Add(new SqlParameter("@Ngaylukho", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@Ngaylukho", SqlDbType.Date)).Value = dpNgayluuKho.Text;
                    if (dpNgaymuon.Text == "")
                        command.Parameters.Add(new SqlParameter("@Ngaymuon", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@Ngaymuon", SqlDbType.Date)).Value = dpNgaymuon.Text;
                    if (dpNgaytra.Text == "")
                        command.Parameters.Add(new SqlParameter("@Ngaytra", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@Ngaytra", SqlDbType.Date)).Value = dpNgaytra.Text;
                }
                catch (Exception)
                {
                    throw;
                }
                command.Parameters.Add(new SqlParameter("@Tinhtrang_khuon", SqlDbType.NVarChar)).Value = txtTinhtrangkhuon.Text;
                command.Parameters.Add(new SqlParameter("@Nguoiluukhuon", SqlDbType.NVarChar)).Value = txtuser.Text;
                command.Parameters.Add(new SqlParameter("@Vitrikhuon", SqlDbType.NVarChar)).Value = txtvitrikhuon.Text;
                command.Parameters.Add(new SqlParameter("@BPsudung", SqlDbType.NVarChar)).Value = txtbophansudung.Text;
                command.Parameters.Add(new SqlParameter("@Mabp", SqlDbType.NVarChar)).Value = txtMabp.Text;
                adapter.Fill(dt); cn.Close(); LoadKHO_KHUON();
            }
        }
        private void AutoComPleteBoPhan()
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select To_bophan from tblPHONGBAN where To_bophan is not null", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtbophansudung.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void UC_KHO_KHUON_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString(); txtuser.Text = Login.Username;
            AutoComPleteBoPhan();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtmasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
            txtMasp_CT.Text = gridView1.GetFocusedRowCellDisplayText(Maspct_grid1);
            txtMakhuon.Text = gridView1.GetFocusedRowCellDisplayText(Makhuon_grid1);
            txtDacdiem_khuon.Text = gridView1.GetFocusedRowCellDisplayText(Dacdiemkhuon_grid1);
            txtsoluongkhuon.Text = gridView1.GetFocusedRowCellDisplayText(soluongkhuon_grid1);
            txtNV_lapkhuon.Text = gridView1.GetFocusedRowCellDisplayText(nguoilap_grid1);
            txtmanv_lapkhuon.Text = gridView1.GetFocusedRowCellDisplayText(manv_grid1);
            dpNgaybatdau.Text = gridView1.GetFocusedRowCellDisplayText(ngaybatdau_grid1);
            dpngayketthucKH.Text = gridView1.GetFocusedRowCellDisplayText(NgayKH_grid1);
            dpNgayCatDayHT.Text = gridView1.GetFocusedRowCellDisplayText(ngayCatDayHT_grid1);
            txtGhichu.Text = gridView1.GetFocusedRowCellDisplayText(ghichu_grid1);
            dpNgayLap.Text = gridView1.GetFocusedRowCellDisplayText(ngayghi_grid1);
            txttensp.Text = gridView1.GetFocusedRowCellDisplayText(sanpham_grid1);
            txttenkhuon.Text = gridView1.GetFocusedRowCellDisplayText(Tenkhuon_grid1);
            txtCodeMK.Text = gridView1.GetFocusedRowCellDisplayText(CodeMK_grid1);
            txtbophansudung.Text= gridView1.GetFocusedRowCellDisplayText(BPmuon_grid1);
            dpNgaymuon.Text= gridView1.GetFocusedRowCellDisplayText(Ngaymuon_grid1);
            dpNgaytra.Text = gridView1.GetFocusedRowCellDisplayText(Ngaytra_grid1);
            txtvitrikhuon.Text = gridView1.GetFocusedRowCellDisplayText(Vitrikhuon_grid1);
            dpNgayCatDayHT.Text= gridView1.GetFocusedRowCellDisplayText(ngaycatHT_grid);
            dpNgayRapKhuon_HT.Text=gridView1.GetFocusedRowCellDisplayText(NgayrapHT_grid);
            dpNgayluuKho.Text = gridView1.GetFocusedRowCellDisplayText(Ngayluukhuon_grid1);
        }

        private void btnDMkhuon_Click(object sender, EventArgs e)
        {
            LoadKHO_KHUON();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtmasp.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            {
                MessageBox.Show("Mã sản không khớp đúng");
            }
        }
        private void SukienGoiMASP_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtmasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
    }
}
