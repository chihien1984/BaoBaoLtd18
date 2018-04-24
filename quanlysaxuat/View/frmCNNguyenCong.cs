using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class frmCNNguyenCong : Form
    {
        public frmCNNguyenCong()
        {
            InitializeComponent();
        }
        private void DanhSachSanPham()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select Code,SP.Masp,Tensp,Hotennv,
            SP.Ngaylap,NC.MaSP NguyenCong from tblSANPHAM SP
			left outer join (select MaSP from NguyenCong group by MaSP) NC
			on SP.Masp=NC.MaSP
			ORDER BY Code DESC");
            kn.dongketnoi();
        }
        private void DanhSachNguyenCong()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"SELECT SanPhamID,NguyenCongID,MaNC,NguyenCong,ChiTietNC,MaSP,
                    SanPham,TenNC,SoChiTiet,BoPhan,DonGia,DinhMuc FROM NguyenCong");
            kn.dongketnoi();
            gridView2.Columns["MaSP"].Visible = true;
            gridView2.Columns["SanPham"].Visible = true;
            gridView2.Columns["SanPham"].GroupIndex = 0;
            gridView2.ExpandAllGroups();
            this.gridView2.OptionsView.NewItemRowPosition 
            = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        private void DanhSachNguyenCongThem()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"SELECT SanPhamID,NguyenCongID,MaNC,NguyenCong,ChiTietNC,MaSP,
                    SanPham,TenNC,SoChiTiet,BoPhan,DonGia,DinhMuc FROM NguyenCong where MaSP like N'"+txtMaSanPham.Text+"'");
            kn.dongketnoi();
            gridView2.ExpandAllGroups();
        }
       
        private void frmCNNguyenCong_Load(object sender, EventArgs e)
        {
            txtMemBer.Text = Login.Username;
            DanhSachSanPham();
            TaoMoiNguyenCong();
            RepositoryNguonLuc();
        }
        private void RepositoryNguonLuc()//GridlookupEdit Repository chọn mã nguồn lực trong gridcontrol Áp mã nguồn lực
        {
            ketnoi Connect = new ketnoi();
            repository.DataSource = Connect.laybang(@"SELECT Ma_Nguonluc,Ten_Nguonluc,Nguoi,Ngay FROM tblResources");
            repository.DisplayMember = "Ma_Nguonluc";
            repository.ValueMember = "Ma_Nguonluc";
            repository.NullText = null;
            Connect.dongketnoi();
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Point = "";
            Point = gridView1.GetFocusedDisplayText();
            txtSanPhamID.Text = gridView1.GetFocusedRowCellDisplayText(sanPhamID_grid);
            txtMaSanPham.Text = gridView1.GetFocusedRowCellDisplayText(maSP_grid);
            txtSanPham.Text = gridView1.GetFocusedRowCellDisplayText(tenSanPham_grid);
            DanhSachNguyenCongThem();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            TaoMoiNguyenCong();
        }
        private void TaoMoiNguyenCong()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"SELECT TOP 1 MaNC='',NguyenCong='',ChiTietNC='',
                    TenNC='',SoChiTiet='',BoPhan='',DonGia='',DinhMuc='' FROM NguyenCong");
            kn.dongketnoi();
            gridView2.Columns["MaSP"].Visible = false;
            gridView2.Columns["SanPham"].Visible = false;
            gridView2.Columns["SanPham"].GroupIndex = -1;
            gridView2.ExpandAllGroups();
            this.gridView2.OptionsView.NewItemRowPosition
             = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }

        private void BtnDanhSachNguyenCong_Click(object sender, EventArgs e)
        {
            DanhSachNguyenCong();
        }
        
        #region Ghi nguyên công
        private void btnGhi_Click(object sender, EventArgs e)
        {
            int[] listRowList1 = this.gridView2.GetSelectedRows();
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into NguyenCong (MaNC,NguyenCong,ChiTietNC,MaSP,
                        SanPham,TenNC,SoChiTiet,BoPhan,DonGia,DinhMuc,NguoiLap,SanPhamID,NgayLap) 
                        VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',GetDate()) ",
                    rowData["MaNC"],
                    rowData["NguyenCong"],
                    rowData["ChiTietNC"],
                    txtMaSanPham.Text,
                    txtSanPham.Text,
                    rowData["TenNC"],
                    rowData["SoChiTiet"],
                    rowData["BoPhan"],
                    rowData["DonGia"],
                    rowData["DinhMuc"],
                    txtMemBer.Text,
                    txtSanPhamID.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DanhSachNguyenCongThem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do :" + ex.Message);
            }
        }
        #endregion

        #region Sửa nguyên công
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update NguyenCong  set MaNC=N'{0}',NguyenCong=N'{1}',ChiTietNC=N'{2}',MaSP=N'{3}',
                        SanPham=N'{4}',TenNC=N'{5}',SoChiTiet=N'{6}',BoPhan=N'{7}',DonGia='{8}',DinhMuc='{9}',NguoiLap=N'{10}',NgayLap= GetDate() where NguyenCongID='{11}'",
                    rowData["MaNC"],
                    rowData["NguyenCong"],
                    rowData["ChiTietNC"],
                    txtMaSanPham.Text,
                    txtSanPham.Text,
                    rowData["TenNC"],
                    rowData["SoChiTiet"],
                    rowData["BoPhan"],
                    rowData["DonGia"],
                    rowData["DinhMuc"],
                    txtMemBer.Text,
                    rowData["NguyenCongID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DanhSachNguyenCongThem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do :" + ex.Message);
            }
        }
        #endregion

        private void btnDSSanPham_Click(object sender, EventArgs e)
        {
            DanhSachSanPham();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from NguyenCong where NguyenCongID={0}",
                    rowData["NguyenCongID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DanhSachNguyenCongThem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do :" + ex.Message);
            }
        }

        private void btnThemNguyenCong_Click(object sender, EventArgs e)
        {
            frmResources Resources = new frmResources();
            Resources.ShowDialog();
            RepositoryNguonLuc();
        }

        private void btnXemBanVe_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }
    }
}
