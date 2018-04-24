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

namespace quanlysanxuat
{
    public partial class UCBanHang : DevExpress.XtraEditors.XtraForm
    {
        public UCBanHang()
        {
            InitializeComponent();
        }
        #region Load đơn đặt hàng 
        private void DonHang()
        {
            ketnoi kn = new ketnoi();
            grdLookDonHang.Properties.DataSource = kn.laybang(@"select DH.Code,nvkd,madh,CAST (Ngaydh as date)Ngaydh,Khachhang,
                MaPO,Diachi,ThanhTien from tblDONHANG DH
                inner join
                (select Code,Sum(thanhtien)ThanhTien 
                from tblDHCT group by Code) CT on DH.Code=CT.Code
                where CAST (Ngaydh as date) 
                between '" + dptu_ngay.Value.ToString("yyyy/MM/dd")+"' and '"+dpden_ngay.Value.ToString("yyyy/MM/dd") +"' order by Code DESC");
            grdLookDonHang.Properties.DisplayMember = "madh";
            grdLookDonHang.Properties.ValueMember = "madh";
            grdLookDonHang.Properties.NullText = null;
            grdLookDonHang.Properties.ImmediatePopup = true;
            kn.dongketnoi();
        }
        #endregion
        #region Thêm dữ liệu vào sổ bán hàng
        private void BanHangThem()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select BanHangID,DatHangID,
                        DonHangID,MaDH,SoLuongDonHang,Dongia,
                        ThanhTien,MaSP,TenGoiNhaMay,MaSPKhachHang,TenGoiKhachHang,
                        MaPO,NgayTao,NguoiTao,NguoiHC,NgayHC from BanHang where DatHangID like '" + txtDatHangID.Text+"'");
            kn.dongketnoi();
            gridView1.Columns["DatHangID"].GroupIndex = -1;
        }
        #endregion
        private void BanHangDanhMuc()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select BanHangID,DatHangID,
                        DonHangID,MaDH,SoLuongDonHang,Dongia,
                        ThanhTien,MaSP,TenGoiNhaMay,MaSPKhachHang,TenGoiKhachHang,
                        MaPO,NgayTao,NguoiTao,NguoiHC,NgayHC from BanHang where NgayTao between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
            gridView1.Columns["DatHangID"].GroupIndex = -1;
        }
        #region Chi tiết đơn đặt hàng
        public void ChiTietDonHang(string ID)
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select Code DatHangID,Iden DonHangID,madh,
                Soluong,dongia,thanhtien,MaSP,Tenquicach,Masp_KH,
                Tenkhachhang,MaPO,thoigianthaydoi,Thoigian_Thuc from tblDHCT where Code like '" + ID+"'");
            kn.dongketnoi();
            gridView3.Columns["DatHangID"].GroupIndex = -1;
            gridView3.Columns["DatHangID"].Visible = false;
            gridView3.Columns["madh"].Visible = false;
            gridView3.ExpandAllGroups();
            this.gridView3.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }
        #endregion
        #region Chi tiết đơn đặt hàng
        public void DocTatCaChiTietDonHang()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select Code DatHangID,Iden DonHangID,madh,
                Soluong,dongia,thanhtien,MaSP,Tenquicach,Masp_KH,
                Tenkhachhang,MaPO,thoigianthaydoi,Thoigian_Thuc from tblDHCT where CAST (thoigianthaydoi as date) 
                between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
            gridView3.Columns["DatHangID"].GroupIndex = 0;
            gridView3.Columns["DatHangID"].Visible = true;
            gridView3.Columns["madh"].Visible = true;
            gridView3.ExpandAllGroups();
            this.gridView3.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
        }
        public static string Member = "";
        #endregion
        private void UCBanHang_Load(object sender, EventArgs e) 
        {
            txtMemBer.Text = Member;
            DonHang();
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView3.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView3.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BanHang 
                        (DatHangID,DonHangID,MaDH,SoLuongDonHang,Dongia,
                        ThanhTien,MaSP,TenGoiNhaMay,MaSPKhachHang,TenGoiKhachHang,
                        MaPO,NguoiTao,NgayTao)
                        values('{0}','{1}',N'{2}','{3}','{4}','{5}',N'{6}',N'{7}',
                                N'{8}',N'{9}',N'{10}',N'{11}',GetDate())",
                                txtDatHangID.Text,
                                rowData["DonHangID"],
                                rowData["madh"],
                                rowData["Soluong"],
                                rowData["dongia"],
                                rowData["thanhtien"],
                                rowData["MaSP"],
                                rowData["Tenquicach"],
                                rowData["Masp_KH"],
                                rowData["Tenkhachhang"],
                                rowData["MaPO"],
                                txtMemBer.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                BanHangThem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView1.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BanHang 
                        set DatHangID='{0}',DonHangID='{1}',MaDH=N'{2}',
                        SoLuongDonHang='{3}',Dongia='{4}',
                        ThanhTien='{5}',MaSP=N'{6}',TenGoiNhaMay=N'{7}',
                        MaSPKhachHang=N'{8}',TenGoiKhachHang=N'{9}',
                        MaPO=N'{10}',NguoiTao=N'{11}',NgayTao=GetDate() where BanHangID='{12}'",
                                rowData["DatHangID"],
                                rowData["DonHangID"],
                                rowData["MaDH"],
                                rowData["SoLuongDonHang"],
                                rowData["Dongia"],
                                rowData["ThanhTien"],
                                rowData["MaSP"],
                                rowData["TenGoiNhaMay"],
                                rowData["MaSPKhachHang"],
                                rowData["TenGoiKhachHang"],
                                rowData["MaPO"],
                                txtMemBer.Text,
                                rowData["BanHangID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); BanHangThem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView1.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format("	Delete from BanHang where BanHangID = '{0}'", rowData["BanHangID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();BanHangDanhMuc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }

        private void grdLookDonHang_TextChanged(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtDatHangID.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(donHangID_grdLook);
            ChiTietDonHang(txtDatHangID.Text);
            BanHangThem();
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
           
        }

        private void dpden_ngay_ValueChanged(object sender, EventArgs e)
        {
            DonHang(); BanHangDanhMuc();
        }

        private void dptu_ngay_ValueChanged(object sender, EventArgs e)
        {
            DonHang(); BanHangDanhMuc();
        }

        private void btnTongChiTietDonHang_Click(object sender, EventArgs e)
        {
            DocTatCaChiTietDonHang();
        }

        private void btnDoanhSoChiTiet_Click(object sender, EventArgs e)
        {
            BanHangDanhMuc();
            gridView1.Columns["DatHangID"].GroupIndex = -1;
            this.gridView1.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void btnDoanhso_Click(object sender, EventArgs e)
        {
            BanHangDanhMuc();
            gridView1.Columns["DatHangID"].GroupIndex = 0;
            gridView1.ExpandAllGroups();
            this.gridView1.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select top 1 DatHangID='',DonHangID='',madh='',
                Soluong='',dongia='',thanhtien='',MaSP='',Tenquicach='',Masp_KH='',
                Tenkhachhang='',MaPO='',thoigianthaydoi='',Thoigian_Thuc='' from tblDHCT");
            kn.dongketnoi();
            this.gridView3.OptionsView.NewItemRowPosition
          = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }

        private void btnTongHopDoanhSo_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select BanHangID,DatHangID,
                        DonHangID,MaDH,SoLuongDonHang,Dongia,
                        ThanhTien,MaSP,TenGoiNhaMay,MaSPKhachHang,TenGoiKhachHang,
                        MaPO,NgayTao,NguoiTao,NguoiHC,NgayHC from BanHang where NgayTao between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
            gridView2.Columns["DatHangID"].GroupIndex = 0;
        }
    }
}
