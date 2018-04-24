using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlysanxuat.Model;
using System.Data.SqlClient;

namespace quanlysanxuat.View
{
    public partial class frmCapNhatDinhMucVatLieu : DevExpress.XtraEditors.XtraForm
    {
        SANXUATDbContext db = new SANXUATDbContext();

        //private string tenChiTiet;
        //private string soLuongChiTiet;
        //private decimal qty = 0;
        //private decimal amount = 0;
        //private decimal vat = 0;
        //private decimal price = 0;
        
        public frmCapNhatDinhMucVatLieu()
        {
            InitializeComponent();
        }

        private void LoadDSSPChiTiet()
        {

        }
        private void DSDinhMucVatLieu()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select *
                from DinhMucVatTu");
            kn.dongketnoi();
            gridView3.ExpandAllGroups();
        }
        private void DSDinhMucVatLieuThemMoi()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select *
                from DinhMucVatTu where SanPhamID like '"+txtSanPhanID.Text+"'");
            kn.dongketnoi();
            gridView3.ExpandAllGroups();
        }
   
        private void LoadItem()
        {
            var dt = db.tblDM_VATTU.ToList();
            repositoryItem.DataSource = dt;
            repositoryItem.ValueMember = "Ma_vl";
            repositoryItem.DisplayMember = "Ma_vl";
            repositoryItem.NullText = @"Chọn vật tư";
            Ma_vl.ColumnEdit = repositoryItem;
        }
        private void LoadDMVatLieu()
        {
            var dt = db.Delivery_VatTu.ToList();
            gridControl1.DataSource = new BindingList<Delivery_VatTu>(dt);
            dt.Clear();
        }
        public static string maSanPham;
        public static string tenSanPham;
        public static string sanPhamID;
        public static string userName;
        private void frmDinhMucVatLieu_Load(object sender, EventArgs e)
        {
            LoadItem();
            LoadDMVatLieu();
            txtMaSanPham.Text = maSanPham;
            txtTenSanPham.Text = tenSanPham;
            txtSanPhanID.Text = sanPhamID;
            txtMember.Text = userName;
            DSDinhMucVatLieu();
            DanhMucSanPham();
            DocDSSanPham();
            DocDSDinhMucTheoSanPham();
            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView3.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            btnGhi_DM_VatLieu.Enabled = false;
            btnSua_DM_VatLieu.Enabled = false;
            btnXoa_DM_VatLieu.Enabled = false;
        }
        void Show_Button_Save_Edit()
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                btnGhi_DM_VatLieu.Enabled = true;
            }
            else
            {
                btnGhi_DM_VatLieu.Enabled = false;
            };
            if (this.gridView3.GetSelectedRows().Count() > 0)
            {
                btnSua_DM_VatLieu.Enabled = true;
                btnXoa_DM_VatLieu.Enabled = true;
            }
            else
            {
                btnSua_DM_VatLieu.Enabled = false;
                btnXoa_DM_VatLieu.Enabled = false;
            };
        }
        #region đọc danh mục sản phẩm vào gridlookupEdit
        private void DanhMucSanPham()
        {
           ketnoi Connect = new ketnoi();
           gridLookUpEditSanPham.Properties.DataSource = Connect.laybang(@"select 
            Code,Masp,Tensp from tblSANPHAM");
           gridLookUpEditSanPham.Properties.DisplayMember = "Masp";
           gridLookUpEditSanPham.Properties.ValueMember = "Masp";
           gridLookUpEditSanPham.Properties.NullText = null;
           Connect.dongketnoi();
        }
        private void BindingDMSanPham()
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtSanPhanID.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(SanPhamIDgridLook);
            txtMaSanPham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(MaSanPhamgridLook);
            txtTenSanPham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(TenSanPhamgridLook);
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView3.GetFocusedDisplayText();
            txtSanPhanID.Text = gridView3.GetFocusedRowCellDisplayText(sanPhamIDgrid2);
            txtMaSanPham.Text = gridView3.GetFocusedRowCellDisplayText(maSanPhamgrid2);
            txtTenSanPham.Text = gridView3.GetFocusedRowCellDisplayText(tenSanPhamgrid2);
            ShowButtonSave_Edit();
        }

        private void gridLookUpEditSanPham_EditValueChanged(object sender, EventArgs e)
        {
            BindingDMSanPham();
        }
        #endregion

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Ma_vl")
            {
                var value = gridView1.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblDM_VATTU.FirstOrDefault(x => x.Ma_vl == (string)value);
                var dinhMuc = db.Delivery_VatTu.ToString();
                if (dt != null)
                {
                    gridView1.SetRowCellValue(e.RowHandle, "id", dt.id);
                    gridView1.SetRowCellValue(e.RowHandle, "Ten_vat_lieu", dt.Ten_vat_lieu);
                 
                    //gridView1.SetRowCellValue(e.RowHandle, "Unit", dt.Unit);
                    //gridView1.SetRowCellValue(e.RowHandle, "Price", dt.Price);
                //    if (gridView1.GetFocusedRowCellValue(colQty) == "")
                //    {
                //        qty = 0;
                //    }
                //    else
                //    {
                //        qty = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(colQty));
                //        price = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(colPrice));
                //        amount = qty * price;
                //        gridView1.SetFocusedRowCellValue(colAmount, amount);
                //        vat = amount / 10;
                //        gridView1.SetFocusedRowCellValue(colVat, vat);
                //    }
                //}
            }
            //if (e.Column == colQty)
            //{
            //    qty = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(colQty));
            //    price = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(colPrice));
            //    amount = qty * price;
            //    gridView1.SetFocusedRowCellValue(colAmount, amount);
            //    vat = amount / 10;
            //    gridView1.SetFocusedRowCellValue(colVat, vat);
           }
        }
        //private void LoadDeliverVatTu()
        //{
        //    ketnoi kn = new ketnoi();
        //    gridControl1.DataSource = kn.laybang("select Ma_vl,Ten_vat_lieu,DinhMuc,DonVi,DienGiai,id from Delivery_VatTu");
        //    kn.dongketnoi();
        //}
        void ShowButtonSave_Edit()
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                btnGhi_DM_VatLieu.Enabled = true;
            }
            else
            {
                btnGhi_DM_VatLieu.Enabled = false;
            }
            if (this.gridView3.GetSelectedRows().Count() > 0)
            {
                btnSua_DM_VatLieu.Enabled = true;
                btnXoa_DM_VatLieu.Enabled = true;
            }
            else
            {
                btnSua_DM_VatLieu.Enabled = false;
                btnXoa_DM_VatLieu.Enabled = false;
            }
        }
        #region Ghi định mức vật tư
        private void btnGhi_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/28640995/how-to-save-data-from-gridcontrol-devexpress-into-oracle-database
            int[] listRowList = this.gridView1.GetSelectedRows();
            if (listRowList.Count()>0)
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;            
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                      string strQuery = string.Format(@"insert into DinhMucVatTu (SanPhamID,VatTuID,MaVatTu,
                      TenVatTu,DienGiai,DinhMuc,DonVi,NguoiTao,MaSP,TenSP,KhoanMuc,NgayTao)
					  VALUES ('{0}','{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',GetDate())",
                    txtSanPhanID.Text,
                    gridView1.GetRowCellValue(i, "id"),
                    gridView1.GetRowCellValue(i, "Ma_vl"),
                    gridView1.GetRowCellValue(i, "Ten_vat_lieu"),
                    gridView1.GetRowCellValue(i, "DienGiai"),
                    gridView1.GetRowCellValue(i, "DinhMuc"),
                    gridView1.GetRowCellValue(i, "DonVi"),
                    txtMember.Text,
                    txtMaSanPham.Text,
                    txtTenSanPham.Text,
                    gridView1.GetRowCellValue(i, "KhoanMuc"));
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do: "+ex, "Thông báo lỗi");
            }
            DSDinhMucVatLieuThemMoi();
        }
        #endregion
        #region sửa
        private void btnSua_Click(object sender, EventArgs e)
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
                    string strQuery = string.Format(@"update DinhMucVatTu set 
                    DienGiai=N'{0}',DinhMuc='{1}',DonVi=N'{2}',NguoiTao=N'{3}',KhoanMuc=N'{4}',NgayTao=GetDate()
                    where DinhMucVatTuID = '{5}'",
                    rowData["DienGiai"],
                    rowData["DinhMuc"],
                    rowData["DonVi"],
                    txtMember.Text, rowData["KhoanMuc"],
                    rowData["DinhMucVatTuID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DSDinhMucVatLieuThemMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }
        #endregion
        #region xóa
        private void btnXoa_Click(object sender, EventArgs e)
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
                    string strQuery = string.Format(@"delete from DinhMucVatTu where DinhMucVatTuID={0}",
                    rowData["DinhMucVatTuID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DSDinhMucVatLieuThemMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do:" + ex.Message,"Thông báo lỗi");
            }
        }
        #endregion

        private void BtnXuatPhieu_Click(object sender, EventArgs e)
        {

        }

        private void btnDMDinhMucVL_Click(object sender, EventArgs e)
        {
            DSDinhMucVatLieu();
            gridView3.ExpandAllGroups();
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, path.pathbanve);
            f2.Show();
        }

        private void DocDSSanPham()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(
                @"select C.MaSP maDinhMuc,Code,P.Masp,Tensp from tblSANPHAM P
				left outer join 
				(select distinct (MaSP) from DinhMucVatTu where Masp <>'')C
				on P.MaSP=C.MaSP
                order by Code DESC");
            kn.dongketnoi();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtSanPhanID.Text = gridView2.GetFocusedRowCellDisplayText(SanPhamID_grid);
            txtMaSanPham.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid);
            txtTenSanPham.Text = gridView2.GetFocusedRowCellDisplayText(Tensp_grid);
            DocDSDinhMucTheoSanPham();
        }
        private void DocDSDinhMucTheoSanPham()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select *
                from DinhMucVatTu where MaSP='"+txtMaSanPham.Text+"'");
            kn.dongketnoi();
            gridView3.ExpandAllGroups();
        }
        private void btnCapNhatVatTu_Click(object sender, EventArgs e)
        {
            frmThemDMvattu themDMvattu = new frmThemDMvattu();
            themDMvattu.ShowDialog();
            LoadItem();
        }

        private void btnDocDSSanPham_Click(object sender, EventArgs e)
        {
            DocDSSanPham();
        }

        private void btnTaoMoiVatLieu_Click(object sender, EventArgs e)
        {
            LoadDMVatLieu();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            ShowButtonSave_Edit();
        }

        private void gridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            ShowButtonSave_Edit();
        }

        private void gridControl3_MouseMove(object sender, MouseEventArgs e)
        {
            ShowButtonSave_Edit();
        }

        private void txtPath_MaSP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
