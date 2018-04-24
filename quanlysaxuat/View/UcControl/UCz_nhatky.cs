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
using DevExpress.XtraReports.UI;

namespace quanlysanxuat
{
    public partial class UCz_nhatky : UserControl
    {
        public UCz_nhatky()
        {
            InitializeComponent();
        }
        private void list_nhatky()//Danh sách z khởi tạo trọng tháng
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select nk.*,dt.Maz,dt.z_tong,kb.tenz,dt.dm_vlchinh,dt.dm_vlphu,kb.masp,kh.tenkh, "
             + " dt.dm_nctt,dt.cp_sxchung,dt.cp_bhang,dt.cp_quanly,dt.cp_khuon,kygia "
             + " from tblz_nhatky nk left outer join   "
             + " tblz_doitongtaphopchiphi dt on nk.idKygia=dt.id left outer join "
             + " tblz_khaibao kb on dt.Maz=kb.maz left outer join tblz_khachhang kh on nk.id_khach=kh.makh where convert(date,nk.ngaylap,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' "
             + " and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
        }
        private void list_dmnhatkythem()//Danh sách khởi tạo z theo mã z thêm vào
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select nk.*,kb.donvi,kb.masp,kb.tenz,kh.tenkh from tblz_nhatky nk "
            + "   left outer join tblz_khaibao kb on nk.id_z=kb.maz  "
            + "   left outer join tblz_khachhang kh on kh.makh=nk.id_khach where id_baogia like N'" + txtMabaogia.Text + "'");
            kn.dongketnoi();
        }
        private void list_dmnhatkysua()//Danh sách khởi tạo theo id z sửa
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select nk.*,kb.donvi,kb.masp,kb.tenz,kh.tenkh from tblz_nhatky nk "
            + "   left outer join tblz_khaibao kb on nk.id_z=kb.maz  "
            + "   left outer join tblz_khachhang kh on kh.makh=nk.id_khach where nk.id like N'" + txtid.Text + "'");
            kn.dongketnoi();
        }
        private void List_znhaky(object sender, EventArgs e)//Sự kiện load danh sách khởi tạo z
        {
            list_nhatky();
        }
        private void Them(object sender, EventArgs e)// Thêm 
        {
            try
            {
                if (txtMabaogia.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtSanpham.Text == "") { MessageBox.Show("Tên sản phẩm", "THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblz_nhatky(id_baogia,id_z,id_khach,nguoilap,ngaylap,diengiai,idKygia,kygia) "
                    + " values(@id_baogia,@id_z,@id_khach,@nguoilap,Getdate(),@diengiai,@idKygia,@kygia)", cn);
                    cmd.Parameters.Add(new SqlParameter("@id_baogia", SqlDbType.NVarChar)).Value = txtMabaogia.Text;
                    cmd.Parameters.Add(new SqlParameter("@id_z", SqlDbType.NVarChar)).Value = cbMaz.Text;
                    cmd.Parameters.Add(new SqlParameter("@id_khach", SqlDbType.NVarChar)).Value = cbMakhachhang.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@diengiai", SqlDbType.NVarChar)).Value = txtDiengiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@idKygia", SqlDbType.NVarChar)).Value = txtidky.Text;
                    cmd.Parameters.Add(new SqlParameter("@kygia", SqlDbType.NVarChar)).Value = txtKyCP.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_dmnhatkythem();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm không thành công", "thông báo");
            }
        }
        private void Sua(object sender, EventArgs e)// Sửa
        {
            try
            {
                if (txtMabaogia.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtSanpham.Text == "") { MessageBox.Show("Tên sản phẩm", "THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("update tblz_nhatky set id_baogia=@id_baogia,id_z=@id_z,id_khach=@id_khach, "
                    + "nguoilap=@nguoilap,ngaylap=Getdate(),diengiai=@diengiai,idKygia=@idKygia,kygia=@kygia where id like '" + txtid.Text + "'", cn);
                    cmd.Parameters.Add(new SqlParameter("@id_baogia", SqlDbType.NVarChar)).Value = txtMabaogia.Text;
                    cmd.Parameters.Add(new SqlParameter("@id_z", SqlDbType.NVarChar)).Value = cbMaz.Text;
                    cmd.Parameters.Add(new SqlParameter("@id_khach", SqlDbType.NVarChar)).Value = cbMakhachhang.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@diengiai", SqlDbType.NVarChar)).Value = txtDiengiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@idKygia", SqlDbType.BigInt)).Value = txtidky.Text;
                    cmd.Parameters.Add(new SqlParameter("@kygia", SqlDbType.NVarChar)).Value = txtKyCP.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_dmnhatkysua();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sửa không thành công", "thông báo");
            }
        }
        private void Xoa(object sender, EventArgs e)// Xóa 
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                cn.Open();
                SqlCommand cmd = new SqlCommand("delete from tblz_nhatky where id like '" + txtid.Text + "'", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                list_nhatky();
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa không thành công", "thông báo");
            }
        }
        private void Binding(object sender, EventArgs e)//Bing từ grid lên textbox
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtid.Text = gridView2.GetFocusedRowCellDisplayText(id_grid2);
            txtMabaogia.Text= gridView2.GetFocusedRowCellDisplayText(id_baogia_grid2);
            cbMaz.Text= gridView2.GetFocusedRowCellDisplayText(id_z_grid2);
            cbMakhachhang.Text= gridView2.GetFocusedRowCellDisplayText(id_khach_grid2);
            txtKhachhang.Text = gridView2.GetFocusedRowCellDisplayText(khachhang_grid2);
            txtDiengiai.Text= gridView2.GetFocusedRowCellDisplayText(diengiai_grid2);
            txtUser.Text = gridView2.GetFocusedRowCellDisplayText(nguoilap_grid2);
            txtSanpham.Text = gridView2.GetFocusedRowCellDisplayText(tensp_grid2);
            txtMasp.Text = gridView2.GetFocusedRowCellDisplayText(masp_grid2);
            txtKyCP.Text = gridView2.GetFocusedRowCellDisplayText(kychiphi_grid2);
            txtidky.Text = gridView2.GetFocusedRowCellDisplayText(idkygia_grid2);
        }
  
        private void list_khachhang()//Ma sách mã khách hàng
        {
            //ketnoi kn = new ketnoi();
            //cbMakhachhang.DataSource = kn.laybang("select makh from tblz_khachhang");
            //cbMakhachhang.DisplayMember = "makh";
            //cbMakhachhang.ValueMember = "makh";
        }
        private void GlEdiKhachHang()//Danh mục đối tượng tính z
        {
            ketnoi Connect = new ketnoi();
            cbMakhachhang.Properties.DataSource = Connect.laybang("select * from tblz_khachhang");
            cbMakhachhang.Properties.DisplayMember = "makh";
            cbMakhachhang.Properties.ValueMember = "makh";
            cbMakhachhang.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cbMakhachhang.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            cbMakhachhang.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }

        private void LoadTenkhachhang()//Load Tên theo mã Z
        {
            try
            {
                SqlConnection con = new SqlConnection(Connect.mConnect);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select tenkh from tblz_khachhang where makh like N'" + cbMakhachhang.Text + "' ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtKhachhang.Text = Convert.ToString(reader[0]);
                }
                con.Close();
            }
            catch (Exception) { };
        }
        private void LoadTenz()//Load Tên khách hàng theo mã khách hàng
        {
            try
            {
                SqlConnection con = new SqlConnection(Connect.mConnect);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select tenz,masp from tblz_khaibao where maz like N'" + cbMaz.Text + "' ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtSanpham.Text = Convert.ToString(reader[0]);
                    txtMasp.Text = Convert.ToString(reader[1]);
                }
                con.Close();
            }
            catch (Exception) { };
        }

        private void cbMaz_SelectedIndexChanged(object sender, EventArgs e)//Thay đổi combobox để hiện tên khách hàng
        {
            LoadTenz();
        }
        private void cbMakhachhang_SelectedIndexChanged(object sender, EventArgs e)//Thay đổi combobox để hiện tên khách hàng
        {
            LoadTenkhachhang();
        }
        private void btnfrmKhoiTao_Click(object sender, EventArgs e)//show from khởi tạo sản phẩm
        {
            frmZ_Doituong fZDoituong = new frmZ_Doituong();
            fZDoituong.ShowDialog();
            GlEditDoiTuongTinhGia();
        }
        private void btnKhachhang_Click(object sender, EventArgs e)//Show from thêm khách hàng
        {
            frmKHBaoGia fKH = new frmKHBaoGia();
            fKH.ShowDialog();
            list_khachhang();
        }
        private void AddCode_Z(object sender,EventArgs e)//Lấy mã phiếu nhập kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("SELECT 'BG'+CONCAT('',RIGHT(CONCAT('00000',ISNULL(right(max(id_baogia),5),0) + 1),5)) from tblz_nhatky", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMabaogia.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void GlEditDoiTuongTinhGia()//Danh muc ky chi phi
        {
            ketnoi Connect = new ketnoi();
            cbMaz.Properties.DataSource = Connect.laybang("select cp.id,kb.maz,masp,kb.tenz,cp.Kytinhgia from tblz_khaibao kb "
            + " left outer join tblz_doitongtaphopchiphi cp on cp.Maz=kb.maz");
            cbMaz.Properties.DisplayMember = "maz";
            cbMaz.Properties.ValueMember = "maz";
            cbMaz.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cbMaz.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            cbMaz.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        private void BindingLookup()//Chọn Mã chi phí từ 
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            //txtMaz.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Maz_gl);
            txtSanpham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tenz_gl);
            txtKyCP.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(kyz_gl);
            txtMasp.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(masp_gl);
            txtidky.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(id_gl);
        }
        private void cbMaz_EditValueChanged_1(object sender, EventArgs e)
        {
            BindingLookup();
        }
        public static string Username = "";//Truyền th
        private void UCz_nhatky_Load(object sender, EventArgs e)//form load
        {
            txtUser.Text = Username;
            list_khachhang();
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            GlEditDoiTuongTinhGia();
            GlEdiKhachHang();
        }

        private void BtnXuatphieuZ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from ViewRPZKEHOACH  where id_baogia like N'" + txtMabaogia.Text + "'");
            XR_ZDinhGia DUTOAN = new XR_ZDinhGia();
            DUTOAN.DataSource = dt;
            DUTOAN.DataMember = "Table";
            DUTOAN.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnbv_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }

    





        

    }
}
