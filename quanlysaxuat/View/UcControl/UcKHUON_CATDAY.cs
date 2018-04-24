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
using System.IO;

namespace quanlysanxuat
{
    public partial class UcKHUON_CATDAY : UserControl
    {
        public UcKHUON_CATDAY()
        {
            InitializeComponent();
        }
        Clsketnoi knn = new Clsketnoi();
      
        public static string THONGTIN_MOI;
        string Gol = "";SqlCommand cmd;
        Clsketnoi connect = new Clsketnoi();
        private void Load_ctsanpham()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select (case  when DMK.Masp is not null then 'Có khuôn' end) TinhTrang,CT.Masp,CT.Mact,Ten_ct, "
                   +" Soluong_CT, Ngaycapnhat, CT.Manv, CT.hotennv, Sp.Tensp, Dacdiem, SP.Ngaylap, Sp.Makh from tblSANPHAM_CT CT left "
                   +" join tblSANPHAM SP on CT.Masp = SP.Masp left join (select Distinct Masp from tblDM_KHUON) DMK on SP.Masp = DMK.Masp");
        }
        private void Load_ctkhuon()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select Ngaykh_cat,CodeMK,Manhom_khuon,Ma_khuon,DM.Masp,SP.Tensp,DM.Mact,Ten_khuon,Dacdiem_khuon, "
                         + "  Soluong_khuon, Ngaylap, Ngaybatdau, Ngayhoanthanh, Nguoilap, Ghichu "
                         + "  from tblDM_KHUON DM left "
                         + "  join tblSANPHAM_CT CT on DM.Mact = CT.Mact "
                         + "  join (select Tensp, Masp from tblSANPHAM) SP on CT.Masp = SP.Masp "
                         + "  where Ma_khuon like N'"+txtMakhuon.Text+ "' order by Mact ASC");
        }
        private void Load_Changechitiet()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select Ngaykh_cat,CodeMK,Manhom_khuon,Ma_khuon,DM.Masp,SP.Tensp,DM.Mact,Ten_khuon,Dacdiem_khuon, "
                         + "  Soluong_khuon, Ngaylap, Ngaybatdau, Ngayhoanthanh, Nguoilap, Ghichu "
                         + "  from tblDM_KHUON DM left "
                         + "  join tblSANPHAM_CT CT on DM.Mact = CT.Mact "
                         + "  join (select Tensp, Masp from tblSANPHAM) SP on CT.Masp = SP.Masp where DM.Mact like N'" + txtMasp_CT.Text + "' order by Mact ASC");
        }
        private void Load_changeSanPham()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select Ngaykh_cat,CodeMK,Manhom_khuon,Ma_khuon,DM.Masp,SP.Tensp,DM.Mact,Ten_khuon,Dacdiem_khuon, "
                         + "  Soluong_khuon, Ngaylap, Ngaybatdau, Ngayhoanthanh, Nguoilap, Ghichu "
                         + "  from tblDM_KHUON DM left "
                         + "  join tblSANPHAM_CT CT on DM.Mact = CT.Mact "
                         + "  join (select Tensp, Masp from tblSANPHAM) SP on CT.Masp = SP.Masp where DM.Masp like N'" + txtmasp.Text + "' order by Ma_khuon ASC");
        }
        private void UcKHUON_CATDAY_Load(object sender, EventArgs e)
        { ketnoi kn = new ketnoi();
            txtuser.Text = Login.Username;
            cbmanhom_khuon.DataSource = kn.laybang("select Manhom_khuon from tblDMNHOM_KHUON");
            cbmanhom_khuon.DisplayMember = "Manhom_khuon";
            cbmanhom_khuon.ValueMember = "Manhom_khuon";
            kn.dongketnoi();
            MaNV();LayTenKhuon(); Autocomplete_Dacdiemkhuon();
        }
        private void MaNV()//Load Mã nhân viên
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
            con.Close();
        }
        private void LoadMax()//Lấy mã sản phẩm lớn nhất
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select 'KHAC-'+ right('0000'+CONVERT(nvarchar,Max(right(Ma_khuon,4))+1),4) as MAKHAC from tblDM_KHUON where left(Ma_khuon,4) like N'" + txtMakhuon.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMakhuon.Text = Convert.ToString(reader[0]);
            txtMasp_CT.Text= Convert.ToString(reader[0]);
            reader.Close();
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txtmasp.Text = gridView2.GetFocusedRowCellDisplayText(masanpham_grid2);
            txttensp.Text = gridView2.GetFocusedRowCellDisplayText(tensanpham_grid2);
            txtMasp_CT.Text = gridView2.GetFocusedRowCellDisplayText(Machitiet_grid2);
            txtNguoiTaoSP.Text= gridView2.GetFocusedRowCellDisplayText(nguoitao_grid2);
            txtTen_CT.Text= gridView2.GetFocusedRowCellDisplayText(tenchitiet_grid2);
            dpNgayLap.Text= gridView2.GetFocusedRowCellDisplayText(ngaytao_grid2);
            Load_Changechitiet();
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtmasp.Text= gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
            txtMasp_CT.Text= gridView1.GetFocusedRowCellDisplayText(Maspct_grid1);
            txtMakhuon.Text= gridView1.GetFocusedRowCellDisplayText(Makhuon_grid1);
            txtDacdiem_khuon.Text= gridView1.GetFocusedRowCellDisplayText(Dacdiemkhuon_grid1);
            txtsoluongkhuon.Text= gridView1.GetFocusedRowCellDisplayText(soluongkhuon_grid1);
            txtNV_lapkhuon.Text= gridView1.GetFocusedRowCellDisplayText(nguoilap_grid1);
            txtmanv_lapkhuon.Text= gridView1.GetFocusedRowCellDisplayText(manv_grid1);
            dpNgaybatdau.Text= gridView1.GetFocusedRowCellDisplayText(ngaybatdau_grid1);
            dpngayketthuc.Text= gridView1.GetFocusedRowCellDisplayText(ngayhoanthanh_grid1);
            txtGhichu.Text= gridView1.GetFocusedRowCellDisplayText(ghichu_grid1);
            dpNgayLap.Text=gridView1.GetFocusedRowCellDisplayText(ngayghi_grid1);
            txttensp.Text= gridView1.GetFocusedRowCellDisplayText(sanpham_grid1);
            txttenkhuon.Text= gridView1.GetFocusedRowCellDisplayText(Tenkhuon_grid1);
            txtCodeMK.Text = gridView1.GetFocusedRowCellDisplayText(CodeMK_grid1);
        }
        private void show_CTsanpham_Click(object sender, EventArgs e)
        {
            Load_ctsanpham();
        }

        private void btnShow_ctkhuon_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select Ngaykh_cat,CodeMK,Manhom_khuon,Ma_khuon,DM.Masp,SP.Tensp,DM.Mact,Ten_khuon,Dacdiem_khuon, "
                         + "  Soluong_khuon, Ngaylap, Ngaybatdau, Ngayhoanthanh, Nguoilap, Ghichu "
                         +"  from tblDM_KHUON DM left "
                         +"  join tblSANPHAM_CT CT on DM.Mact = CT.Mact "
                         + " join (select Tensp, Masp from tblSANPHAM) SP on CT.Masp = SP.Masp order by Masp,  Mact ASC");
        }

        private void btnfresh_Click(object sender, EventArgs e)
        {
            UcKHUON_CATDAY_Load(sender, e);
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMakhuon.Enabled = true;txttenkhuon.Enabled = true;txtDacdiem_khuon.Enabled = true;
            txtGhichu.Enabled = true;dpNgaybatdau.Enabled = true;dpngayketthuc.Enabled = true;txtsoluongkhuon.Enabled = true;
        }
        private bool kiemtramakhuon()
        {
            bool tatkt = false;
            string MaSP = txtMakhuon.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            SqlCommand cmd = new SqlCommand("select Mact from tblSANPHAM_CT", con);
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
            //if (kiemtramakhuon()) 
            //{
            //    MessageBox.Show("Mã khuôn'" + txtMakhuon.Text + "' đã tồn tại, Không thể thêm mã CT trùng");
            //}
            if (txtMakhuon.Text == "" && txttenkhuon.Text == "") //Có thể thêm được khuôn trùng nhau cho 2 loại chi tiết khác nhau của 2 sản phẩm khác nhau
            {
                MessageBox.Show("Tên khuôn, mã khuôn không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if(txtMakhuon.Text != "" && txttenkhuon.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("insert into tblDM_KHUON "
                            + " (Manhom_khuon,Ma_khuon,Masp,Mact,Ten_khuon,Dacdiem_khuon,Soluong_khuon,Nguoilap,Ngaylap,Ngaybatdau,Ngayhoanthanh,Ghichu,Manv,Ngaykh_cat) "
                            + " values(@Manhom_khuon,@Ma_khuon,@Masp,@Mact,@Ten_khuon,@Dacdiem_khuon,@Soluong_khuon,@Nguoilap,GetDate(),@Ngaybatdau,@Ngayhoanthanh,@Ghichu,@Manv,@Ngaykh_cat)", con))
                        {
                            cmd.Parameters.Add("@Ngaykh_cat", SqlDbType.Date).Value = dpNgayCatHT.Text;
                            cmd.Parameters.Add("@Manhom_khuon", SqlDbType.NVarChar).Value = cbmanhom_khuon.Text;
                            cmd.Parameters.Add("@Ma_khuon", SqlDbType.NVarChar).Value = txtMakhuon.Text;
                            cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
                            cmd.Parameters.Add("@Mact", SqlDbType.NVarChar).Value = txtMasp_CT.Text;
                            cmd.Parameters.Add("@Ten_khuon", SqlDbType.NVarChar).Value = txttenkhuon.Text;
                            cmd.Parameters.Add("@Dacdiem_khuon", SqlDbType.NVarChar).Value = txtDacdiem_khuon.Text;
                            cmd.Parameters.Add("@Soluong_khuon", SqlDbType.Int).Value = txtsoluongkhuon.Text;
                            cmd.Parameters.Add("@Nguoilap", SqlDbType.NVarChar).Value = txtuser.Text;
                            cmd.Parameters.Add("@Ngaybatdau", SqlDbType.Date).Value = dpNgaybatdau.Text;
                            cmd.Parameters.Add("@Ngayhoanthanh", SqlDbType.Date).Value = dpngayketthuc.Text;
                            cmd.Parameters.Add("@Ghichu", SqlDbType.NVarChar).Value = txtGhichu.Text;
                            cmd.Parameters.Add("@Manv", SqlDbType.NVarChar).Value = txtMa_user.Text;
                            cmd.ExecuteNonQuery();
                        }
                        Load_changeSanPham(); con.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Kiểm tra mã","Thông báo");
                }
                
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("update tblDM_KHUON "
                    + " set Ngaykh_cat=@Ngaykh_cat,Manhom_khuon=@Manhom_khuon,Ma_khuon=@Ma_khuon,Masp=@Masp,Mact=@Mact, "
                    + " Ten_khuon=@Ten_khuon,Dacdiem_khuon=@Dacdiem_khuon,Soluong_khuon=@Soluong_khuon, "
                    +" Nguoilap=@Nguoilap,Ngaylap=GetDate(),Ngaybatdau=@Ngaybatdau, "
                    + " Ngayhoanthanh=@Ngayhoanthanh,Ghichu=@Ghichu,Manv=@Manv where CodeMK like '"+txtCodeMK.Text+"'", con))
                {
                    cmd.Parameters.Add("@Ngaykh_cat", SqlDbType.Date).Value = dpNgayCatHT.Text;
                    cmd.Parameters.Add("@Manhom_khuon", SqlDbType.NVarChar).Value = cbmanhom_khuon.Text;
                    cmd.Parameters.Add("@Ma_khuon", SqlDbType.NVarChar).Value = txtMakhuon.Text;
                    cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
                    cmd.Parameters.Add("@Mact", SqlDbType.NVarChar).Value = txtMasp_CT.Text;
                    cmd.Parameters.Add("@Ten_khuon", SqlDbType.NVarChar).Value = txttenkhuon.Text;
                    cmd.Parameters.Add("@Dacdiem_khuon", SqlDbType.NVarChar).Value = txtDacdiem_khuon.Text;
                    cmd.Parameters.Add("@Soluong_khuon", SqlDbType.Int).Value = txtsoluongkhuon.Text;
                    cmd.Parameters.Add("@Nguoilap", SqlDbType.NVarChar).Value = txtuser.Text;
                    cmd.Parameters.Add("@Ngaybatdau", SqlDbType.Date).Value = dpNgaybatdau.Text;
                    cmd.Parameters.Add("@Ngayhoanthanh", SqlDbType.Date).Value = dpngayketthuc.Text;
                    cmd.Parameters.Add("@Ghichu", SqlDbType.NVarChar).Value = txtGhichu.Text;
                    cmd.Parameters.Add("@Manv", SqlDbType.NVarChar).Value = txtMa_user.Text;
                    cmd.ExecuteNonQuery();
                }
                Load_changeSanPham(); con.Close();
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (txtMasp_CT.Text != "" && MessageBox.Show("Bạn muốn xóa nhóm mã " + txtMakhuon.Text + " có tên" + txtMasp_CT.Text + " ", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ketnoi kn = new ketnoi();
                gridControl1.DataSource = kn.xulydulieu("Delete tblDM_KHUON "
                    + " where CodeMK like N'" + txtCodeMK.Text + "'");
            }
            Load_changeSanPham();
        }

        private void btnExportsx_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }



        private void btnThem_nhomkhuon_Click(object sender, EventArgs e)
        {
            frmNHOM_KHUON Them_nhomkhuon = new frmNHOM_KHUON();
            Them_nhomkhuon.ShowDialog();
        }

        private void cbmanhom_khuon_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayTenKhuon(); txtMakhuon.Text = cbmanhom_khuon.Text;
          
        }
        private void LayTenKhuon()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Tennhom_khuon from tblDMNHOM_KHUON WHERE Manhom_khuon like N'"+cbmanhom_khuon.Text+"'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtten_nhom.Text = Convert.ToString(reader[0]);
            reader.Close();con.Close();
        }

        private void txtmasp_TextChanged(object sender, EventArgs e)
        {
            txtMaCheck.Text=txtmasp.Text;
        }

        private void gridControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Load_changeSanPham();
        }

        private void btnCatKhac_Click(object sender, EventArgs e)
        {
            txtmasp.Clear(); txtMakhuon.Enabled = true;txttensp.Clear();txtMasp_CT.Clear();txtTen_CT.Clear();
            txttenkhuon.Enabled = true; txtDacdiem_khuon.Enabled = true;
            txtGhichu.Enabled = true; dpNgaybatdau.Enabled = true; dpngayketthuc.Enabled = true; txtsoluongkhuon.Enabled = true;
            LoadMax();          
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            Gol = gridView3.GetFocusedDisplayText();
            txtmasp.Text = gridView3.GetFocusedRowCellDisplayText(Masp_grid1);
            txtMasp_CT.Text = gridView3.GetFocusedRowCellDisplayText(Maspct_grid1);
            txtMakhuon.Text = gridView3.GetFocusedRowCellDisplayText(Makhuon_grid1);
            txtDacdiem_khuon.Text = gridView3.GetFocusedRowCellDisplayText(Dacdiemkhuon_grid1);
            txtsoluongkhuon.Text = gridView3.GetFocusedRowCellDisplayText(soluongkhuon_grid1);
            txtNV_lapkhuon.Text = gridView3.GetFocusedRowCellDisplayText(nguoilap_grid1);
            txtmanv_lapkhuon.Text = gridView3.GetFocusedRowCellDisplayText(manv_grid1);
            dpNgaybatdau.Text = gridView3.GetFocusedRowCellDisplayText(ngaybatdau_grid1);
            dpngayketthuc.Text = gridView3.GetFocusedRowCellDisplayText(ngayhoanthanh_grid1);
            txtGhichu.Text = gridView3.GetFocusedRowCellDisplayText(ghichu_grid1);
            dpNgayLap.Text = gridView3.GetFocusedRowCellDisplayText(ngayghi_grid1);
            txttensp.Text = gridView3.GetFocusedRowCellDisplayText(sanpham_grid1);
            txttenkhuon.Text = gridView3.GetFocusedRowCellDisplayText(Tenkhuon_grid1);
        }

        private void Load_DMTongHop()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("select Manhom_khuon,Ma_khuon,DM.Masp,SP.Tensp,DM.Mact,Ten_khuon,Dacdiem_khuon, "
                         + "  Soluong_khuon, Ngaylap, Ngaybatdau, Ngayhoanthanh, Nguoilap, Ghichu "
                         + "  from tblDM_KHUON DM "
                         + "  left outer join tblSANPHAM_CT CT on DM.Mact = CT.Mact "
                         + "  left outer join(select Tensp, Masp from tblSANPHAM) SP on CT.Masp = SP.Masp order by Masp, Mact ASC");
        }
        private void show_DMTongHop_Click(object sender, EventArgs e)
        {
            Load_DMTongHop();
        }

        private void btnXoakhac_Click(object sender, EventArgs e)
        {
            if (txtMasp_CT.Text != "" && MessageBox.Show("Bạn muốn xóa nhóm mã " + txtMakhuon.Text + " có tên" + txtMasp_CT.Text + " ", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ketnoi kn = new ketnoi();
                gridControl3.DataSource = kn.xulydulieu("Delete tblDM_KHUON "
                    + " where Ma_khuon like N'" + txtMakhuon.Text + "' and Mact like N'" + txtMasp_CT.Text + "'");
                Load_DMTongHop();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }
        private void LOOUP_SANPHAM()//LooupEdit mã sản phẩm từ đặc điểm khuôn
        {
            DataTable Table = new DataTable();
            ketnoi Connect = new ketnoi();
            LookupCheck_Masp.Properties.DataSource = Connect.laybang("select Masp,Ma_khuon,Ten_khuon,Dacdiem_khuon from tblDM_KHUON where Dacdiem_khuon like N'" + txtDacdiem_khuon.Text+"'");
            LookupCheck_Masp.Properties.DisplayMember = "Masp";
            LookupCheck_Masp.Properties.ValueMember = "Masp";
            LookupCheck_Masp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            LookupCheck_Masp.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            LookupCheck_Masp.Properties.ImmediatePopup = true;
        }

        private void Autocomplete_Dacdiemkhuon()//Automplete dac diem khuon
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select Dacdiem_khuon from tblDM_KHUON where Dacdiem_khuon <>''", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection Collection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    Collection.Add(reader.GetString(0));
                }
                txtDacdiem_khuon.AutoCompleteCustomSource = Collection;
                con.Close();con.Close();
            }


        }

        private void txtDacdiem_khuon_TextChanged(object sender, EventArgs e)
        {
            LOOUP_SANPHAM();
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMaCheck.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            {
                MessageBox.Show("Mã sản không khớp đúng");
            }
        }
        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtmasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void LookupCheck_Masp_EditValueChanged(object sender, EventArgs e)
        {
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMaCheck.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Masp_look);
        }
    }
}
