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

namespace quanlysanxuat.View
{
    public partial class frmBaoCaoSanXuatMoiNgay : Form
    {
        public frmBaoCaoSanXuatMoiNgay()
        {
            InitializeComponent();
        }

        private void frmBaoCaoSanXuatMoiNgay_Load(object sender, EventArgs e)
        {
            lbUser.Text = Login.Username;
            dpTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDen.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DocDSBaoCaoNgayTheoNgay();
        }
        private void DocDSBaoCaoNgayTheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(
                @"select * from BaoCaoSanXuatNgay
                where NgaySanXuat
			    between '{0}' and '{1}'",
             dpTu.Value.ToString("MM-dd-yyyy"),
             dpDen.Value.ToString("MM-dd-yyyy"));
            grcBaoCaoNgay.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void TaoMoiDanhSach()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select top 0 from BaoCaoSanXuatNgay");
            grcBaoCaoNgay.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void btnGhi_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.grBaoCaoNgay.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for(int i = 0; i < listRowList.Length; i++)
            {
                rowData = grBaoCaoNgay.GetDataRow(listRowList[i]);
                string sqlStr = string.Format(@"insert into BaoCaoSanXuatNgay 
                    (NgaySanXuat,MaDonHang,
                    MaSanPham,TenSanPham,
                    KeHoach,KetQua,
                    CongSuatTrenGio,ThoiGianHoanThanh,
                    RotKeHoach,DaGiao,
                    TonKho,NguoiGiao,
                    NguoiNhan,SoLuongCongNhan,
                    ToTruong,GhiChu,
                    NguoiTao,NgayTao) 
                    values('{0}',N'{1}',
                    N'{2}',N'{3}',
                    N'{4}',N'{5}',
                    N'{6}',N'{7}',
                    N'{8}',N'{9}',
                    N'{10}',N'{11}',
                    N'{12}',N'{13}',
                    N'{14}',N'{15}',
                    N'{16}',GetDate())",
                    rowData["NgaySanXuat"]==DBNull.Value?null:Convert.ToDateTime(rowData["NgaySanXuat"]).ToString("MM-dd-yyyy"), rowData["MaDonHang"],
                    rowData["MaSanPham"], rowData["TenSanPham"],
                    rowData["KeHoach"], rowData["KetQua"],
                    rowData["CongSuatTrenGio"], rowData["ThoiGianHoanThanh"]==DBNull.Value?null:Convert.ToDateTime(rowData["ThoiGianHoanThanh"]).ToString("MM-dd-yyyy"),
                    rowData["RotKeHoach"], rowData["DaGiao"],
                    rowData["TonKho"], rowData["NguoiGiao"],
                    rowData["NguoiNhan"], rowData["SoLuongCongNhan"],
                    rowData["ToTruong"], rowData["GhiChu"],
                    lbUser.Text, rowData["NgayTao"]);
                SqlCommand cmd = new SqlCommand(sqlStr,con);
                cmd.ExecuteNonQuery();
            }
            DocDSBaoCaoNgayTheoNgay();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.grBaoCaoNgay.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = grBaoCaoNgay.GetDataRow(listRowList[i]);
                string sqlStr = string.Format(@"update BaoCaoSanXuatNgay 
                    set NgaySanXuat='{0}',MaDonHang=N'{1}',
                    MaSanPham=N'{2}',TenSanPham=N'{3}',
                    KeHoach=N'{4}',KetQua=N'{5}',
                    CongSuatTrenGio=N'{6}',ThoiGianHoanThanh=N'{7}',
                    RotKeHoach=N'{8}',DaGiao=N'{9}',
                    TonKho=N'{10}',NguoiGiao=N'{11}',
                    NguoiNhan=N'{12}',SoLuongCongNhan=N'{13}',
                    ToTruong=N'{14}',GhiChu=N'{15}',
                    NguoiSua=N'{16}',NgaySua=GetDate() where ID like '{17}'",
                    rowData["NgaySanXuat"]==DBNull.Value?"":Convert.ToDateTime(rowData["NgaySanXuat"]).ToString("MM-dd-yyyy"), rowData["MaDonHang"],
                    rowData["MaSanPham"], rowData["TenSanPham"],
                    rowData["KeHoach"], rowData["KetQua"],
                    rowData["CongSuatTrenGio"], rowData["ThoiGianHoanThanh"] == DBNull.Value ? null : Convert.ToDateTime(rowData["ThoiGianHoanThanh"]).ToString("MM-dd-yyyy"),
                    rowData["RotKeHoach"], rowData["DaGiao"],
                    rowData["TonKho"], rowData["NguoiGiao"],
                    rowData["NguoiNhan"], rowData["SoLuongCongNhan"],
                    rowData["ToTruong"], rowData["GhiChu"],
                    lbUser.Text,rowData["ID"]);
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.ExecuteNonQuery();
            }
            DocDSBaoCaoNgayTheoNgay();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.grBaoCaoNgay.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = grBaoCaoNgay.GetDataRow(listRowList[i]);
                string sqlStr = string.Format(@"delete from BaoCaoSanXuatNgay 
                    where ID like '{0}'",
                    rowData["ID"]);
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.ExecuteNonQuery();
            }
            DocDSBaoCaoNgayTheoNgay();
        }

        private void btnTraCuuSanLuongHoanThanh_Click(object sender, EventArgs e)
        {
            DocDSBaoCaoNgayTheoNgay();
            DongTaoMoi();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            TaoMoi();
        }
        private void TaoMoi()
        {
            grBaoCaoNgay.OptionsSelection.MultiSelectMode 
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            grBaoCaoNgay.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            grBaoCaoNgay.OptionsView.NewItemRowPosition
                = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }
        private void DongTaoMoi()
        {
            grBaoCaoNgay.OptionsSelection.MultiSelectMode
                           = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
         
            grBaoCaoNgay.OptionsView.NewItemRowPosition
                = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            btnGhi.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void grcBaoCaoNgay_MouseMove(object sender, MouseEventArgs e)
        {
            if (grBaoCaoNgay.SelectedRowsCount>=1)
            {
            btnGhi.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            }
            else
            {
            btnGhi.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            grBaoCaoNgay.ShowPrintPreview();
        }
    }
}
