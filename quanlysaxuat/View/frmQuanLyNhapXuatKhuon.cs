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

namespace quanlysanxuat.View
{
    public partial class frmQuanLyNhapXuatKhuon : DevExpress.XtraEditors.XtraForm
    {
        public frmQuanLyNhapXuatKhuon(string masanpham,string tensanpham,string makhuon,string tenkhuon,string soluongkhuon,string idsanphamkhuon)
        {
            InitializeComponent();
            this.masanpham = masanpham;
            this.tensanpham = tensanpham;
            this.makhuon = makhuon;
            this.tenkhuon = tenkhuon;
            this.soluongkhuon = soluongkhuon;
            this.idsanphamkhuon = idsanphamkhuon;
        }
        private string masanpham;
        private string tensanpham;
        private string makhuon;
        private string tenkhuon;
        private string soluongkhuon;
        private string idsanphamkhuon;
        private void DanhMucBoPhan()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select TenBoPhanNhan from DSBoPhanNhanVatTu");
            cbBoPhanLayKhuon.DataSource = kn.laybang(sqlStr);
            cbBoPhanLayKhuon.ValueMember = "TenBoPhanNhan";
            cbBoPhanLayKhuon.DisplayMember = "TenBoPhanNhan";
            kn.dongketnoi();
        }
        private void frmQuanLyNhapXuatKhuon_Load(object sender, EventArgs e)
        {
            txtidsanphamkhuon.Text = idsanphamkhuon;
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtMaSanPham.Text = masanpham;
            txtSanPham.Text = tensanpham;
            txtMaKhuon.Text = makhuon;
            txtTenKhuon.Text = tenkhuon;
            txtSoLuongKhuon.Text = soluongkhuon;
            DanhMucBoPhan();
            DocDSKhuonNhanXuatTheoSanPham();
            HidenSaveUpdateDelete();
            HidenSave();
            this.grKhuonNhapXuat.Appearance.Row.Font = new Font("Times New Roman", 7f);
        }
     
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            DocDSKhuonNhanXuatTheoThoiGian();
        }
        private void DocDSKhuonNhanXuatTheoThoiGian()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from tblKhuon_Xuat_Nhap where NgayLap between '{0}' and '{1}'",
                dptu_ngay.Value.ToString("MM-dd-yyyy"),
                dpden_ngay.Value.ToString("MM-dd-yyyy"));
            grcKhuonNhapXuat.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void DocDSKhuonNhanXuatTheoSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from tblKhuon_Xuat_Nhap where MaSanPham like '{0}'",
                txtMaSanPham.Text);
            grcKhuonNhapXuat.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (idsanphamkhuon == "")
                { MessageBox.Show("Chưa có ID nào được chọn"); return; }
            if ( masanpham == "" )
                { MessageBox.Show("Chưa có mã sản phẩm nào được chọn"); return; }
            if ( makhuon == "")
                { MessageBox.Show("Chưa có mã khuôn"); return; }
            else
            { 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
             string sqlStr = string.Format(@"insert into tblKhuon_Xuat_Nhap 
			            (SanPhamKhuonID,NgayLap,MaKhuon,
			            TenKhuon,MaSanPham,
			            TenSanPham,ViTri,
			            TinhTrang,NoiMuon,
                        NgayMuon,NguoiTao,SoLuongKhuon,NguoiMuon,NgayTao)
			            values('{0}',N'{1}',
                                N'{2}',N'{3}',
                                N'{4}',N'{5}',
                                N'{6}',N'{7}',N'{8}',
                                N'{9}',N'{10}','{11}',N'{12}',
                                GetDate())", 
                                idsanphamkhuon, 
                                dpNgayLap.Value.ToString("MM-dd-yyyy"),
                                txtMaKhuon.Text,
                                txtTenKhuon.Text,
                                txtMaSanPham.Text,
                                txtSanPham.Text,
                                txtViTri.Text,
                                cbTinhTrang.Text,
                                cbBoPhanLayKhuon.Text,
                                dpNgayMuonTra.Value.ToString("MM-dd-yyyy"),
                                Login.Username,
                                txtSoLuongKhuon.Text,
                                txtNguoiLayKhuon.Text);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(sqlStr,con);
            cmd.ExecuteNonQuery();
            con.Close();
                DocDSKhuonNhanXuatTheoThoiGian();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            string sqlStr = string.Format(@"update tblKhuon_Xuat_Nhap 
			            set NgayLap='{0}',MaKhuon=N'{1}',
			            TenKhuon=N'{2}',MaSanPham=N'{3}',
			            TenSanPham=N'{4}',ViTri=N'{5}',
			            TinhTrang=N'{6}',NoiMuon=N'{7}',NgayMuon='{8}',
			            NguoiTao=N'{9}',NguoiMuon=N'{10}',NgayTao=GetDate() where ID={11}", 
                               dpNgayLap.Value.ToString("MM-dd-yyyy"),txtMaKhuon.Text,
                               txtTenKhuon.Text, txtMaSanPham.Text,
                               txtSanPham.Text, txtViTri.Text,
                               cbTinhTrang.Text, cbBoPhanLayKhuon.Text,
                               dpNgayMuonTra.Value.ToString("MM-dd-yyyy"),
                               Login.Username, txtNguoiLayKhuon.Text,
                               grKhuonNhapXuat.GetFocusedRowCellDisplayText(id_khuonmuon));
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.ExecuteNonQuery();
            con.Close();
            DocDSKhuonNhanXuatTheoThoiGian();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            string sqlStr = string.Format(@"delete from tblKhuon_Xuat_Nhap where ID='{0}'", 
                               grKhuonNhapXuat.GetFocusedRowCellDisplayText(id_khuonmuon));
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            DataTable dt = new DataTable();
            cmd.ExecuteNonQuery();
            con.Close();
            DocDSKhuonNhanXuatTheoThoiGian();
        }

        private void grcKhuonNhapXuat_Click(object sender, EventArgs e)
        {
            txtidsanphamkhuon.Text = "";
            string point = "";
            point = grKhuonNhapXuat.GetFocusedDisplayText();
            txtIDSoNhapXuatKho.Text= grKhuonNhapXuat.GetFocusedRowCellDisplayText(id_khuonmuon);
            txtMaSanPham.Text = grKhuonNhapXuat.GetFocusedRowCellDisplayText(masanpham_khuonmuon);
            txtSanPham.Text= grKhuonNhapXuat.GetFocusedRowCellDisplayText(tensanpham_khuonmuon);
            txtMaKhuon.Text= grKhuonNhapXuat.GetFocusedRowCellDisplayText(makhuon_khuonmuon);
            txtTenKhuon.Text= grKhuonNhapXuat.GetFocusedRowCellDisplayText(tenkhuon_khuonmuon);
            txtSoLuongKhuon.Text= grKhuonNhapXuat.GetFocusedRowCellDisplayText(soluong_khuonmuon);
            cbBoPhanLayKhuon.Text= grKhuonNhapXuat.GetFocusedRowCellDisplayText(noimuon_khuonmuon);
            txtNguoiLayKhuon.Text= grKhuonNhapXuat.GetFocusedRowCellDisplayText(nguoimuon_khuonmuon);
            HidenSaveUpdateDelete();
            HidenSave();
        }
        private void HidenSaveUpdateDelete()
        {
            if (txtIDSoNhapXuatKho.Text !=""||txtidsanphamkhuon.Text!="")
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else 
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            if (txtidsanphamkhuon.Text != "")
            {
                btnLuu.Enabled = true;
            }
        }
        private void HidenSave()
        {
            if (txtidsanphamkhuon.Text == "")
            {
                btnLuu.Enabled = false;
            }
        }
        private void cbBoPhanLayKhuon_TextChanged(object sender, EventArgs e)
        {
            txtViTri.Text = null;
        }

        private void txtViTri_TextChanged(object sender, EventArgs e)
        {
            cbBoPhanLayKhuon.SelectedIndex=-1;txtNguoiLayKhuon.Text = "";
        }
    }
}
