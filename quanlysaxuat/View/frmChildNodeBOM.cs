using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmChildNodeBOM : DevExpress.XtraEditors.XtraForm
    {
        private string sanphamid;
        private string maloai;
        private string tenloai;
        private string parentnode;
        private string mucdocon;
        private string soluongchitiet;
        private string masanpham;
        private string tensanpham;
        private string machitiet;
        private string tenchitiet;
        private string sochitiet;
        public frmChildNodeBOM(string sanphamid, string maloai,
            string tenloai, string parentnode,
            string mucdo, string soluongchitiet, string masanpham,
            string tensanpham, string machitiet,
            string tenchitiet, string sochitiet)
        {
            this.sanphamid = sanphamid;
            this.maloai = maloai;
            this.tenloai = tenloai;
            this.parentnode = parentnode;
            this.mucdocon = mucdo;
            this.soluongchitiet = soluongchitiet;
            this.masanpham = masanpham;
            this.tensanpham = tensanpham;
            this.machitiet = machitiet;
            this.tenchitiet = tenchitiet;
            this.sochitiet = sochitiet;
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
                @"select Macongdoan MaLoai,Tencondoan Ten_Nguonluc,Dinhmuc,
                Tothuchien ToThucHien,''ThuTu,SoChiTiet,
				''DienGiai,SoChiTiet,NguyenCong Ma_Nguonluc,''DonVi
                from tblDMuc_LaoDong where SanPhamID like N'{0}'", txtIDSanPham.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvChildTreeList.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #region formload
        private void frmChildNodeStages_Load(object sender, EventArgs e)
        {
            txtIDSanPham.Text = sanphamid;
            txtMaCha.Text = maloai;
            txtTenCha.Text = tenloai;
            txtParentNode.Text = parentnode;
            txtMucDo.Text = mucdocon;
            txtSoLuongChiTiet.Text = soluongchitiet;
            TheHienCongDoanSanPham();
            TheHienDanhSachCongDoanTheoSanPhamID();
            ShowStagesCode();
            //Thể hiện mã công đoạn sản xuất
            //TheHienItemMaCongDoan();
            //Hiện danh sách rỗng
            //DeliveryCongDoan();
            TheHienToThucHien();//thể hiện tổ thực hiện
            gvChildTreeList.Appearance.Row.Font = new Font("Segoe UI", 8f);
            //Enable sort cột số thứ tự mỗi lần thêm công đoạn sản xuất
            //
            gvChildTreeList.SortInfo.ClearAndAddRange(new[]
            {
                new GridColumnSortInfo(stt_, DevExpress.Data.ColumnSortOrder.Ascending),
            });
            DonViChiTiet();
        }
        //Đơn vị công đoan
        private void DonViChiTiet()
        {
            repositoryItemComboBoxDonViCongDoan.Items.Add("Cái");
            repositoryItemComboBoxDonViCongDoan.Items.Add("Bộ");
            repositoryItemComboBoxDonViCongDoan.Items.Add("Chiếc");
        }
        //Goi item mã công đoạn
        SANXUATDbContext db = new SANXUATDbContext();
        void TheHienItemMaCongDoan()
        {
            ketnoi kn = new ketnoi();
            repositoryItemGridLookUpEditMaCongDoan.DataSource = kn.laybang(@"select * from tblResources order by ResourceID ASC");
            repositoryItemGridLookUpEditMaCongDoan.DisplayMember = "Ma_Nguonluc";
            repositoryItemGridLookUpEditMaCongDoan.ValueMember = "Ma_Nguonluc";
            repositoryItemGridLookUpEditMaCongDoan.NullText = @"Chọn mã công đoạn";
            gridColumn2.ColumnEdit = repositoryItemGridLookUpEditMaCongDoan;
        }

        void DeliveryCongDoan()
        {
            var dt = db.tblResourcesDeliveries.ToList();
            grChildTreeList.DataSource = new BindingList<tblResourcesDelivery>(dt);
            dt.Clear();
            this.gvChildTreeList.OptionsView.NewItemRowPosition
                = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }
        private void TheHienToThucHien()
        {
            repositoryItemCBToThucHien.Items.Clear();
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ToThucHien from tblResources");
            var data = kn.laybang(sqlQuery);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                repositoryItemCBToThucHien.Items.Add(data.Rows[i]["ToThucHien"]);
            }
            kn.dongketnoi();
        }
        #endregion
        private void btnSaveNodeTreeList_Click(object sender, EventArgs e)
        {
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
                        @"insert into tblSanPhamTreeList 
                           (IDSanPham,MucDo,MaLoai,
                            TenLoai,ParentID,
                            DinhMucCongDoan,
                            ToThucHien,ThuTu,
                            DienGiaiCongDoan,
                            SoLanCongDoan,MaCong,SoLuong,
                            NguoiLap,TenCong,MaChiTiet,TenChiTiet,SoChiTiet,MaSanPham,TenSanPham,DonVi,NgayLap)
					        values('{0}','{1}',N'{2}',
					        N'{3}','{4}',N'{5}',
                            N'{6}','{7}',
                            N'{8}',N'{9}',N'{10}',
                            N'{11}',N'{12}',N'{13}',
                            N'{14}',N'{15}','{16}',N'{17}',N'{18}',N'{19}',GetDate())",
                            txtIDSanPham.Text, txtMucDo.Text, rowData["MaLoai"],
                            rowData["Ten_Nguonluc"], txtParentNode.Text,
                            rowData["Dinhmuc"], rowData["Tothuchien"],
                            rowData["ThuTu"], rowData["DienGiai"],
                            rowData["SoChiTiet"], rowData["Ma_Nguonluc"], txtSoLuongChiTiet.Text,
                            Login.Username, rowData["Ten_Nguonluc"],
                            txtMaCha.Text, txtTenCha.Text, txtSoLuongChiTiet.Text,
                            masanpham, tensanpham,rowData["DonVi"]);
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

        private void btnNewData_Click(object sender, EventArgs e)
        {
            TheHienCongDoanMacDinh();
        }
        private void TheHienCongDoanMacDinh()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(
                @"select  '{0}'+'/CD00' MaLoai,Ten_Nguonluc,''DinhMuc,ToThucHien,ThuTu,
				'1' SoChiTiet,''DienGiai,Ma_Nguonluc,''DonVi from tblResources
			    where ResourceID in (45,34,42,52,23,47) order by ThuTu ASC", txtMaCha.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvChildTreeList.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void ShowStagesCode()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from tblResources order by ResourceID ASC");
            repositoryItemGridLookUpEditMaCongDoan.DataSource = kn.laybang(sqlQuery);
            repositoryItemGridLookUpEditMaCongDoan.DisplayMember = "Ma_Nguonluc";
            repositoryItemGridLookUpEditMaCongDoan.ValueMember = "Ma_Nguonluc";
            kn.dongketnoi();
        }
        private void gvChildTreeList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //gvChildTreeList.SelectAll();//Khi thêm một công đoạn mới sẽ check tất cả các công đoạn cũ
            if (e.Column.FieldName == "Ma_Nguonluc")
            {
                var value = gvChildTreeList.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblResources.FirstOrDefault(x => x.Ma_Nguonluc == (string)value);
                if (dt != null)
                {
                    gvChildTreeList.SetRowCellValue(e.RowHandle, "Ten_Nguonluc", dt.Ten_Nguonluc);
                    gvChildTreeList.SetRowCellValue(e.RowHandle, "ToThucHien", dt.ToThucHien);
                    gvChildTreeList.SetRowCellValue(e.RowHandle, "MaLoai", txtMaCha.Text.ToString() + "/CD00");
                    gvChildTreeList.SetRowCellValue(e.RowHandle, "SoChiTiet", "1");
                    //Vòng lặp gán số 0 vào công đoạn vừa thêm trước đó 1 lần mục đích sắp xếp công đoạn mới thêm
                    //Lên phía trên đầu để việc ghi dữ liệu các công đoạn vào sổ kế hoạch lần lượt theo thứ tự 
                    //int[] listRowList = this.gvChildTreeList.GetSelectedRows();
                    //for (int i = 0; i < listRowList.Length; i++)
                    //{
                    //    DataRow rowData = this.gvChildTreeList.GetDataRow(listRowList[i]);
                    //    if (rowData["ThuTu"].ToString() == "")
                    //    {
                    //        rowData["ThuTu"] = "0";
                    //    }
                    //}
                }
            }
            //focused vào row mới thêm vào
            GridView view = sender as GridView;
            if (view.IsNewItemRow(view.FocusedRowHandle))
                return;
            view.UpdateCurrentRow();
        }

        private void gvChildTreeList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.IsNewItemRow(e.PrevFocusedRowHandle))
            {
                view.FocusedRowHandle = view.DataRowCount - 1;
            }
        }

        private void btnHienThiMoi_Click(object sender, EventArgs e)
        {
            TheHienCongDoanMoi();
        }
        private void TheHienCongDoanMoi()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select top 0 '{0}'+'/CD00' MaLoai,Ten_Nguonluc,''DinhMuc,ToThucHien,ThuTu,
				'1' SoChiTiet,''DienGiai,Ma_Nguonluc,''DonVi from tblResources order by ThuTu ASC", txtMaCha.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvChildTreeList.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        private void cbMaSanPham_EditValueChanged(object sender, EventArgs e)
        {
            TheHienCongDoanTuongTu();
        }
 
        private void TheHienCongDoanTuongTu()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select MaLoai,TenCong Ten_Nguonluc,DinhMucCongDoan Dinhmuc,ToThucHien,
				''ThuTu,SoChiTiet,DienGiai,SoChiTiet,MaCong Ma_Nguonluc,''DonVi
				from tblSanPhamTreeList where MaSanPham like '{0}' and MaCong <>''", cbMaSanPham.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvChildTreeList.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
    }
}
