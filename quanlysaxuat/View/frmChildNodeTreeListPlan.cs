using quanlysanxuat.Model;
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
    public partial class frmChildNodeTreeListPlan : DevExpress.XtraEditors.XtraForm
    {
        private string sanphamid;
        private string maloai;
        private string tenloai;
        private string parentnode;
        private string mucdocon;
        private string soluongchitiet;

        private string IDDonHang;
        private string IDChiTietDonHang;
        private string IDChiTietSanPham;
        private string MaDonHang;
        private string MaPo;
        private string MaSanPham;
        private string TenSanPham;
        private string SoLuongDonHang;
        private string SoLuongChiTietDonHang;
        private string SoLuongYCSanXuat;
        private string TenCongDoan;
        private string MaChiTiet;
        private string TenChiTiet;
        private string SoChiTiet;

        public frmChildNodeTreeListPlan(string sanphamid, string maloai, string tenloai,
            string parentnode, string mucdo, string soluongchitiet,
            string IDDonHang,
            string IDChiTietDonHang,
            string IDChiTietSanPham,
            string MaDonHang,
            string MaPo,
            string MaSanPham,
            string TenSanPham,
            string SoLuongDonHang,
            string SoLuongChiTietDonHang,
            string SoLuongYCSanXuat,
            string TenCongDoan,
            string MaChiTiet,
            string TenChiTiet,
            string SoChiTiet)
        {
            this.sanphamid = sanphamid;
            this.maloai = maloai;
            this.tenloai = tenloai;
            this.parentnode = parentnode;
            this.mucdocon = mucdo;
            this.soluongchitiet = soluongchitiet;

            this.IDDonHang = IDDonHang;
            this.IDChiTietDonHang = IDChiTietDonHang;
            this.IDChiTietSanPham = IDChiTietSanPham;
            this.MaDonHang = MaDonHang;
            this.MaPo = MaPo;
            this.MaSanPham = MaSanPham;
            this.TenSanPham = TenSanPham;
            this.SoLuongDonHang = SoLuongDonHang;
            this.SoLuongChiTietDonHang = SoLuongChiTietDonHang;
            this.SoLuongYCSanXuat = SoLuongYCSanXuat;
            this.TenCongDoan = TenCongDoan;
            this.MaChiTiet = MaChiTiet;
            this.TenChiTiet = TenChiTiet;
            this.SoChiTiet = SoChiTiet;
            InitializeComponent();
        }
        private void TheHienCongDoanSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select s.Masp,
                s.Tensp from tblSANPHAM s 
                inner join 
				(select Masp from 
                tblDMuc_LaoDong group by Masp) l 
				on s.Masp=l.Masp");
            cbMaSanPham.Properties.DataSource = kn.laybang(sqlQuery);
            cbMaSanPham.Properties.DisplayMember = "Masp";
            cbMaSanPham.Properties.ValueMember = "Masp";
            kn.dongketnoi();
        }
        private void TheHienDanhSachCongDoanTheoSanPhamID()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(
                @"select Macongdoan MaLoai,Tencondoan TenLoai,Dinhmuc,
                Tothuchien ToThucHien,''ThuTu,SoChiTiet,
				''DienGiai,SoChiTiet,NguyenCong,''DonVi
                from tblDMuc_LaoDong where SanPhamID like N'{0}'", txtIDSanPham.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvChildTreeList.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #region formload
        private void frmChildNodeStages_Load(object sender, EventArgs e)
        {
           
        }
        #endregion
        private void DonViChiTiet()
        {
            repositoryItemComboBoxDonViCongDoan.Items.IndexOf("Cái");
            repositoryItemComboBoxDonViCongDoan.Items.Add("Cái");
            repositoryItemComboBoxDonViCongDoan.Items.Add("Bộ");
            repositoryItemComboBoxDonViCongDoan.Items.Add("Chiếc");
        }
        private string randonpoint;
        private void RanDonPoint()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(@"select Top 1
                        REPLACE(convert(nvarchar, GetDate(), 11), '/', '')
                      + replace(replace(left(CONVERT(time, GetDate()), 12), ':', ''), '.', '')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                this.randonpoint = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void btnSaveNodeTreeList_Click(object sender, EventArgs e)
        {
           
            //;
            //update TrienKhaiKeHoachSanXuat set ID = ParentID + IDTrienKhai
            //                    where ParentID like '{5}' and ID is null
            try
            {
                int[] listRowList = this.gvChildTreeList.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChildTreeList.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"insert into TrienKhaiKeHoachSanXuat 
                            (IDSanPham,MucDo,MaLoai,
                             TenLoai,SoLuongLoai,
                             ParentID,DinhMuc,
                             ToThucHien,SoThuTu,
                             DienGiaiCongDoan,SoLanCongDoan,
                             MaCongDoan,
                                IDDonHang,IDChiTietDonHang,IDChiTietSanPham,
					            MaDonHang,MaPo,
                                MaSanPham,TenSanPham,
                                SoLuongDonHang,SoLuongChiTietDonHang,
                                SoLuongYCSanXuat,TenCongDoan,
					            MaChiTiet,TenChiTiet,SoChiTiet,
                                NguoiLap,DonViChiTiet,DonViSanPham,ID,NgayLap)
					         values('{0}','{1}',N'{2}',
					         N'{3}','{4}',N'{5}',
                             '{6}',N'{7}',N'{8}',N'{9}',
                             N'{10}',N'{11}',N'{12}',
                             N'{13}',N'{14}',N'{15}',
                             N'{16}',N'{17}',N'{18}',
                             N'{19}',N'{20}',N'{21}',
                             N'{22}',N'{23}',N'{24}',
                             N'{25}',N'{26}',N'{27}',N'{27}','{28}',GetDate())",
                            txtIDSanPham.Text, txtMucDo.Text, rowData["MaLoai"],
                            rowData["TenLoai"], txtSoLuongChiTiet.Text,
                            parentnode, rowData["Dinhmuc"],
                            rowData["ToThucHien"], rowData["ThuTu"],
                            rowData["DienGiai"], rowData["SoChiTiet"],
                            rowData["NguyenCong"],
                            IDDonHang,
                            IDChiTietDonHang,
                            IDChiTietSanPham,
                            MaDonHang,
                            MaPo,
                            MaSanPham,
                            TenSanPham,
                            SoLuongDonHang,
                            SoLuongChiTietDonHang,
                            int.Parse(SoLuongYCSanXuat),
                            TenCongDoan,
                            MaChiTiet,
                            TenChiTiet,
                            SoChiTiet,
                            Login.Username, rowData["DonVi"],txtParentNode.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                MessageBox.Show("Success", "Mission");
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void TheHienDanhSachCongDoanMoi()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = 
                string.Format(@"select top 0 '{0}'+'/CD00' MaLoai,
                Ten_Nguonluc TenLoai,''DinhMuc,ToThucHien,ThuTu,
				'1' SoChiTiet,''DienGiai,Ma_Nguonluc NguyenCong,N'Cái' DonVi
				from tblResources order by ThuTu ASC", txtMaCha.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvChildTreeList.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void TheHienCongDoanMacDinh()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(
                @"select '{0}'+'/CD00' MaLoai,
                Ten_Nguonluc TenLoai,''DinhMuc,ToThucHien,ThuTu,
				'1' SoChiTiet,''DienGiai,Ma_Nguonluc NguyenCong,N'Cái' DonVi  from tblResources
			    where ResourceID in (45,34,42,52,23,47) order by ThuTu ASC", txtMaCha.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvChildTreeList.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        //Thể hiện danh sách tổ thực hiện
        private void ShowDeparment()
        {
            repositoryItemCBDepartment.Items.Clear();
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ToThucHien from tblResources");
            var data = kn.laybang(sqlQuery);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                repositoryItemCBDepartment.Items.Add(data.Rows[i]["ToThucHien"]);
            }
            kn.dongketnoi();
        }
        //Thể hiện danh sách nguồn lực thực hiện
        private void ShowStagesCode()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select Ma_Nguonluc NguyenCong,* from tblResources order by ResourceID ASC");
            repositoryItemGridLookUpEditCongDoan.DataSource = kn.laybang(sqlQuery);
            repositoryItemGridLookUpEditCongDoan.DisplayMember = "NguyenCong";
            repositoryItemGridLookUpEditCongDoan.ValueMember = "NguyenCong";
            kn.dongketnoi();
        }
        //form load
        private void frmChildNodeTreeListPlan_Load(object sender, EventArgs e)
        {
            RanDonPoint();
            int mucdo = int.Parse(mucdocon) + 1;
            txtIDSanPham.Text = sanphamid;
            txtMaCha.Text = maloai;
            txtTenCha.Text = tenloai;
            txtParentNode.Text = (double.Parse(parentnode) + double.Parse(randonpoint)).ToString();
            txtMucDo.Text = mucdo.ToString();
            txtSoLuongChiTiet.Text = soluongchitiet;
            TheHienCongDoanSanPham();
            TheHienDanhSachCongDoanTheoSanPhamID();
            ShowStagesCode();
            ShowDeparment();
            DonViChiTiet();
        }

        SANXUATDbContext db = new SANXUATDbContext();
        private void gvChildTreeList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //gvChildTreeList.SelectAll();
            if (e.Column.FieldName == "NguyenCong")
            {
                var value = gvChildTreeList.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblResources.FirstOrDefault(x => x.Ma_Nguonluc == (string)value);
                if (dt != null)
                {
                    gvChildTreeList.SetRowCellValue(e.RowHandle, "TenLoai", dt.Ten_Nguonluc);
                    gvChildTreeList.SetRowCellValue(e.RowHandle, "ToThucHien", dt.ToThucHien);
                    gvChildTreeList.SetRowCellValue(e.RowHandle, "MaLoai", txtMaCha.Text.ToString() + "/CD00");
                    gvChildTreeList.SetRowCellValue(e.RowHandle, "SoChiTiet", "1");
                    gvChildTreeList.SetRowCellValue(e.RowHandle, "DonVi", "Cái");
                }
            }
        }

        private void btnNewData_Click(object sender, EventArgs e)
        {
            TheHienCongDoanMacDinh();
        }

        private void btnCongMoi_Click(object sender, EventArgs e)
        {
            TheHienDanhSachCongDoanMoi();
        }
    }
}


