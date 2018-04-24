using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using quanlysanxuat.View;
using quanlysanxuat.Model;
using System.Data.SqlClient;

namespace quanlysanxuat
{
    public partial class KhuonMau : DevExpress.XtraEditors.XtraForm
    {
        public KhuonMau()
        {
            InitializeComponent();
        }
        public static string maSP;
        public static string tenSanPham;
        public static string maKhuon;
        public static string tenKhuon;
        public static string khuonID;
        public static string soLuongKhuon;
        public static string idsanphamkhuon;
        
        SANXUATDbContext db = new SANXUATDbContext();
        #region formload
        private void KhuonMau_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            LoadItemTenKhuon();
            DsKhuonTaoMoi();
            DocDSSanPham();
            DocDSKhuonSanPham();
            grKhuonSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            this.grSanPham.Appearance.Row.Font = new Font("Segoe UI", 7f);
            this.grKhuon.Appearance.Row.Font = new Font("Segoe UI", 7f);
            this.grKhuonSanPham.Appearance.Row.Font = new Font("Segoe UI", 7f);
        }
        #endregion
        private void EnableSaveEditDelete()
        {
            if (Login.role == "1039"||Login.role=="1"||Login.role=="39")
            {
                btnLuuTenKhuonVaoDanhMucSanPham.Visible = true;
                btnSuaKhuonSanPham.Visible = true;
                btnXoaKhuonSanPham.Visible = true;
                btnDanhMucKhuon.Visible = true;
                btnTaoMoi.Visible = true;
                btnSanPhamMoi.Visible = true;
            }
            else
            {
                btnLuuTenKhuonVaoDanhMucSanPham.Visible = false;
                btnSuaKhuonSanPham.Visible = false;
                btnXoaKhuonSanPham.Visible = false;
                btnDanhMucKhuon.Visible = false;
                btnTaoMoi.Visible = false;
            }
        }


        private void LoadItemTenKhuon()
        {
            ketnoi kn = new ketnoi();
            repositoryItemGridLookUpEditTenKhuon.DataSource = 
                kn.laybang(@"select KhuonID,MaKhuon,TenKhuon,
                DacDiemKhuon,SoLuong,DateCreate,
                UserCreate from tblDM_KHUON");
            repositoryItemGridLookUpEditTenKhuon.DisplayMember = "TenKhuon";
            repositoryItemGridLookUpEditTenKhuon.ValueMember = "TenKhuon";
            tenkhuon_.ColumnEdit = repositoryItemGridLookUpEditTenKhuon;
        }
        private void DsKhuonTaoMoi()
        {
            ketnoi kn = new ketnoi();
            grcKhuon.DataSource = kn.laybang(@"select Top 0 * from tblDM_KHUON");
            kn.dongketnoi();
            grKhuon.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            frmLoading f2 = new 
                frmLoading(txtMaSanPham.Text, txtPath_MaSP.Text);
            f2.ShowDialog();
        }
        private void DocDSKhuonSanPham()
        {   
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select k.*,v.ViTri,v.NoiMuon,
                v.NguoiMuon,NgayMuon from SanPhamKhuon k left outer join
                (select max(ID)ID,max(NgayMuon)NgayMuon,MaKhuon,
				max(NoiMuon)NoiMuon,max(NguoiMuon)NguoiMuon,
                max(ViTri)ViTri
                from tblKhuon_Xuat_Nhap
                group by MaKhuon)v
                on k.MaKhuon=v.MaKhuon
                order by DateCreate Desc");
            grcKhuonSanPham.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            grKhuonSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        private void DocDSSanPham()
        {
            ketnoi kn = new ketnoi();
            grcSanPham.DataSource = kn.laybang(@"select case when m.MaSanPham is not null
                then 'x' end DaApMa,Code,Masp,Tensp,
                Hotennv,Ngaylap
                from tblSANPHAM s
				left outer join
				(select MaSanPham from SanPhamKhuon group by MaSanPham)m
				on s.Masp=m.MaSanPham order by Ngaylap Desc");
            kn.dongketnoi();
        }
    

        private void btnDanhMucKhuon_Click(object sender, EventArgs e)
        {
            frmDMKhuon dMKhuon = new frmDMKhuon();
            dMKhuon.ShowDialog();
            LoadItemTenKhuon();
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            Layout_Masp();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            string point = "";
            point = grSanPham.GetFocusedDisplayText();
            txtMaSanPham.Text = grSanPham.GetFocusedRowCellDisplayText(maSanPham_col2);
            txtSanPham.Text = grSanPham.GetFocusedRowCellDisplayText(sanPham_col2);
            txtSanPhamID.Text = grSanPham.GetFocusedRowCellDisplayText(sanphamid_col2);
        }

        private void grKhuon_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName== "TenKhuon")
            {
                var value = grKhuon.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblDM_KHUON.FirstOrDefault(x => x.TenKhuon == (string)value);
                if(dt!=null)
                {
                    grKhuon.SetRowCellValue(e.RowHandle, "MaKhuon",dt.MaKhuon);
                    grKhuon.SetRowCellValue(e.RowHandle, "DacDiemKhuon", dt.DacDiemKhuon);
                    grKhuon.SetRowCellValue(e.RowHandle, "SoLuong", dt.SoLuong);
                    grKhuon.SetRowCellValue(e.RowHandle, "KhuonID", dt.KhuonID);
                }
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            DsKhuonTaoMoi();
            this.grKhuon.OptionsView.NewItemRowPosition
                = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }

        private void btnTraCuDanhMucSanPham_Click(object sender, EventArgs e)
        {
            DocDSSanPham();
        }

        private void btnLuuTenKhuonVaoDanhMucSanPham_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            int[] listRowList = this.grKhuon.GetSelectedRows();
            for(int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.grKhuon.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"insert into SanPhamKhuon 
				(KhuonID,SanPhamID,
				MaSanPham,TenSanPham,
				MaKhuon,TenKhuon,SoLuongKhuon,
				UserCreate,DateCreate) values ('{0}','{1}',
                    N'{2}',N'{3}',
                    N'{4}',N'{5}',N'{6}',
                    N'{7}',GetDate())",
                rowData["KhuonID"],txtSanPhamID.Text,
                txtMaSanPham.Text, txtSanPham.Text,
                rowData["MaKhuon"], rowData["TenKhuon"], rowData["SoLuong"],
                Login.Username);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DocDSKhuonSanPham();
            DocDSSanPham();
        }

        private void btnTraCuuKhuonSanPham_Click(object sender, EventArgs e)
        {
            DocDSKhuonSanPham();
        }

        private void btnSuaKhuonSanPham_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            int[] listRowList = this.grKhuonSanPham.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.grKhuonSanPham.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update SanPhamKhuon 
				set KhuonID='{0}',SanPhamID ='{1}',
				MaSanPham=N'{2}',TenSanPham=N'{3}',
				MaKhuon=N'{4}',TenKhuon=N'{5}',SoLuongKhuon='{6}',
				UserCreate=N'{7}',DateCreate=GetDate() where ID={8}",
                rowData["KhuonID"], rowData["SanPhamID"],
                rowData["MaSanPham"], rowData["TenSanPham"],
                rowData["MaKhuon"], rowData["TenKhuon"], rowData["SoLuongKhuon"],
                Login.Username,rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DocDSKhuonSanPham();
        }

        private void btnXoaKhuonSanPham_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            int[] listRowList = this.grKhuonSanPham.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.grKhuonSanPham.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"delete from SanPhamKhuon where ID ='{0}'",
                rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DocDSKhuonSanPham();
        }

        private void grcKhuonSanPham_Click(object sender, EventArgs e)
        {
            string point = "";
            point = grKhuonSanPham.GetFocusedDisplayText();
            maSP = grKhuonSanPham.GetFocusedRowCellDisplayText(maSanPham_colSanPhamKhuon);
            tenSanPham= grKhuonSanPham.GetFocusedRowCellDisplayText(sanPham_colSanPhamKhuon);
            maKhuon=grKhuonSanPham.GetFocusedRowCellDisplayText(makhuon_colSanPhamKhuon);
            tenKhuon=grKhuonSanPham.GetFocusedRowCellDisplayText(tenkhuon_colSanPhamKhuon);
            soLuongKhuon=grKhuonSanPham.GetFocusedRowCellDisplayText(soluongkhuon_colSanPhamKhuon);
            idsanphamkhuon = grKhuonSanPham.GetFocusedRowCellDisplayText(id_colSanPhamKhuon);
            txtMaSanPham.Text = grKhuonSanPham.GetFocusedRowCellDisplayText(maSanPham_colSanPhamKhuon);
            txtSanPham.Text = grKhuonSanPham.GetFocusedRowCellDisplayText(sanPham_colSanPhamKhuon);
            txtSanPhamID.Text = grKhuonSanPham.GetFocusedRowCellDisplayText(idsanpham_colSanPhamKhuon);
        }
        private void QuanLyNhapXuatKhuon()
        {
            frmQuanLyNhapXuatKhuon quanLyNhapXuatKhuon = new frmQuanLyNhapXuatKhuon(maSP,tenSanPham,maKhuon,tenKhuon,soLuongKhuon,idsanphamkhuon);
            quanLyNhapXuatKhuon.ShowDialog();
        }
        private void grcKhuonSanPham_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void grcKhuonSanPham_DoubleClick(object sender, EventArgs e)
        {
            QuanLyNhapXuatKhuon();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSanPhamMoi_Click(object sender, EventArgs e)
        {
            txtMaSanPham.ReadOnly = false;txtSanPham.ReadOnly = false;
        }
    }
}