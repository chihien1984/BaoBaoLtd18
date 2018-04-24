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

namespace quanlysanxuat.View.UcControl
{
    public partial class CongSuatMayUserControl : DevExpress.XtraEditors.XtraForm
    {
        public CongSuatMayUserControl()
        {
            InitializeComponent();
        }
        //formload
        private void ConSuatMayUserControl_Load(object sender, EventArgs e)
        {
            TheHienDanhMucCongSuatMay();
            this.gvCongSuatMay.Appearance.Row.Font = new Font("Segoe UI", 9f);
            gvCongSuatMay.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gvDanhMucMay.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            THDanhMucMay();
        }
        private async void THDungChung(string strSql, DevExpress.XtraEditors.GridLookUpEdit cbo)
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(strSql);
            });
            Invoke((Action)(() =>
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DataSource = Model.Function.GetDataTable(strSql);
            }));
        }

        private async void TheHienDanhMucCongSuatMay()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select id,Masp Nhom,Tensp,Macongdoan,
                    Tencondoan,Masp,Macongdoan,
			        Dinhmuc,Tothuchien,MayThucHien,MaHieuMay,NoiSuDung
			        from tblDMuc_LaoDong where Masp<>''
			        and Dinhmuc <>''");
                Invoke((Action)(() =>
                {
                    grCongSuatMay.DataSource = null;
                    grCongSuatMay.DataSource = Function.GetDataTable(sqlQuery);
                    gvCongSuatMay.Columns["Nhom"].GroupIndex = 0;
                    gvCongSuatMay.ExpandAllGroups();
                }));
            }
           );
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            TheHienDanhMucCongSuatMay();
        }
        private async void THDanhMucMay()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select MaHieuMay,NoiSuDung,QuanLy,
			    TinhTrang,TenMayMocThietBi from DanhMucMay");
                Invoke((Action)(() =>
                {
                    repositoryItemGridLookUpEditDanhMucMay.DataSource = Function.GetDataTable(sqlQuery);
                    repositoryItemGridLookUpEditDanhMucMay.DisplayMember = "MaHieuMay";
                    repositoryItemGridLookUpEditDanhMucMay.ValueMember = "MaHieuMay";
                }));
            });
        }
        SANXUATDbContext db = new SANXUATDbContext();
        private void gvCongSuatMay_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "MaHieuMay")
            {
                var value = gvCongSuatMay.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.DanhMucMays.FirstOrDefault(x => x.MaHieuMay == (string)value);
                if (dt != null)
                {
                    gvCongSuatMay.SetRowCellValue(e.RowHandle, "MayThucHien", dt.TenMayMocThietBi);
                    gvCongSuatMay.SetRowCellValue(e.RowHandle, "NoiSuDung", dt.NoiSuDung);
                }
            }
        }

        private void btnCapNhatCongSuatMay_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gvCongSuatMay.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvCongSuatMay.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblDMuc_LaoDong set 
					MaHieuMay=N'{0}',MayThucHien = N'{1}',NoiSuDung=N'{2}'
					where id like '{3}'",
                    rowData["MaHieuMay"],
                    rowData["MayThucHien"],
                    rowData["NoiSuDung"],
                    rowData["id"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                TheHienDanhMucCongSuatMay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void btnXoaCongSuatMay_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gvCongSuatMay.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvCongSuatMay.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblDMuc_LaoDong set 
					MaHieuMay=N'{0}',
                    MayThucHien = N'{1}',
                    NoiSuDung=N'{2}'
					where id like '{3}'",
                    "", "", "", rowData["id"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                TheHienDanhMucCongSuatMay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gvCongSuatMay.Columns["Nhom"].GroupIndex = -1;
            gvCongSuatMay.ShowPrintPreview();
            gvCongSuatMay.Columns["Nhom"].GroupIndex = 0;
            gvCongSuatMay.ExpandAllGroups();
        }

        private void btnTraCuuCongSuatMay_Click(object sender, EventArgs e)
        {
            THCongSuatMay();
        }
        private async void THCongSuatMay()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
            string sqlQuery = string.Format(@"select MayThucHien,
            sum(Thang01)Thang01,
            sum(Thang02)Thang02,
            sum(Thang03)Thang03,
            sum(Thang04)Thang04,
            sum(Thang05)Thang05,
            sum(Thang06)Thang06,
            sum(Thang07)Thang07,
            sum(Thang08)Thang08,
            sum(Thang08)Thang09,
            sum(Thang10)Thang10,
            sum(Thang11)Thang11,
            sum(Thang12)Thang12
            from
            (select IDTrienKhai,MaHieuMay,MayThucHien,MaDonHang,MaSanPham,SoLuong,BD,KT,
            thang01*SoMay Thang01,
            thang02*SoMay Thang02,
            thang03*SoMay Thang03,
            thang04*SoMay Thang04,
            thang05*SoMay Thang05,
            thang06*SoMay Thang06,
            thang07*SoMay Thang07,
            thang08*SoMay Thang08,
            thang09*SoMay Thang09,
            thang10*SoMay Thang10,
            thang11*SoMay Thang11,
            thang12*SoMay Thang12
            from 
            (select idtrienkhai,madonhang,masanpham,soluong,bd,kt,datediff(month,bd,kt)+1 thang,
            case when month(bd) = '01'						then soluong/(datediff(month,bd,kt)+1) end thang01,
            case when month(bd)<= '02' and month(kt)>= '02' then soluong/(datediff(month,bd,kt)+1) end thang02,
            case when month(bd)<= '03' and month(kt)>= '03' then soluong/(datediff(month,bd,kt)+1) end thang03,
            case when month(bd)<= '04' and month(kt)>= '04' then soluong/(datediff(month,bd,kt)+1) end thang04,
            case when month(bd)<= '05' and month(kt)>= '05' then soluong/(datediff(month,bd,kt)+1) end thang05,
            case when month(bd)<= '06' and month(kt)>= '06' then soluong/(datediff(month,bd,kt)+1) end thang06,
            case when month(bd)<= '07' and month(kt)>= '07' then soluong/(datediff(month,bd,kt)+1) end thang07,
            case when month(bd)<= '08' and month(kt)>= '08' then soluong/(datediff(month,bd,kt)+1) end thang08,
            case when month(bd)<= '09' and month(kt)>= '09' then soluong/(datediff(month,bd,kt)+1) end thang09,
            case when month(bd)<= '10' and month(kt)>= '10' then soluong/(datediff(month,bd,kt)+1) end thang10,
            case when month(bd)<= '11' and month(kt)>= '11' then soluong/(datediff(month,bd,kt)+1) end thang11,
            case when month(bd)<= '12' and month(kt)>= '12' then soluong/(datediff(month,bd,kt)+1) end thang12
            from (select max(IDTrienKhai)IDTrienKhai,
            MaDonHang,MaSanPham,
            min(batdau)BD,max(ketthuc)KT,SoLuongYCSanXuat soluong
            from trienkhaikehoachsanxuat
		    where macongdoan <>'' 
		    and MaSanPham <> 'TEM-NUL-000'
            and batdau <>''
		    and KetThuc <>''
		    and year(batdau)=year(getdate())
            and year(ketthuc)=year(getdate())
		    and MaCongDoan like 'GHA'
            group by MaDonHang,MaSanPham,SoLuongYCSanXuat)n)a
            left outer join
            (select Masp,MaHieuMay,MayThucHien,sum(SoMay)SoMay
            from viewDinhMucCongSuatMay
            group by Masp,MaHieuMay,MayThucHien)b
            on a.MaSanPham=b.Masp)p
		    left outer join
		    (select MaHieuMay,NoiSuDung from DanhMucMay)q
		    on p.MaHieuMay=q.MaHieuMay
		    where p.MaHieuMay <>''
		    group by p.MayThucHien");
                Invoke((Action)(() =>
                {
                    grCongSuatMayCan.DataSource = null;
                    grCongSuatMayCan.DataSource = Function.GetDataTable(sqlQuery);
                }));
            });
        }

        private void btnExportCongSuatMay_Click(object sender, EventArgs e)
        {
            grCongSuatMay.ShowPrintPreview();
        }

        private void btnTaoMoiDanhMucMay_Click(object sender, EventArgs e)
        {
            gvDanhMucMay.OptionsView.NewItemRowPosition =
                DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            TaoMoiDanhMucMay();
        }
        private async void TaoMoiDanhMucMay()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select top 0 ID,MaHieuMay,TenMayMocThietBi,
                    NoiSuDung,QuanLy,TinhTrang from DanhMucMay");
                Invoke((Action)(() =>
                {
                    grDanhMucMay.DataSource = null;
                    grDanhMucMay.DataSource = Function.GetDataToTable(sqlQuery);
                }));
            });
        }
        private async void THDanhMucMayThemMoi()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,MaHieuMay,TenMayMocThietBi,
                    NoiSuDung,QuanLy,TinhTrang from DanhMucMay");
                Invoke((Action)(() =>
                {
                    grDanhMucMay.DataSource = null;
                    grDanhMucMay.DataSource = Function.GetDataToTable(sqlQuery);
                }));
            });
            THDanhMucMay();
        }
        private void btnThemDanhMucMay_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gvDanhMucMay.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhMucMay.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into DanhMucMay 
                    (MaHieuMay,TenMayMocThietBi,
                    NoiSuDung,QuanLy,TinhTrang)
                    values (N'{0}',N'{1}',
                    N'{2}',N'{3}',N'{4}')",
                    rowData["MaHieuMay"],
                    rowData["TenMayMocThietBi"],
                    rowData["NoiSuDung"],
                    rowData["QuanLy"],
                    rowData["TinhTrang"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                THDanhMucMayThemMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void btnCapNhatDanhMucMay_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gvDanhMucMay.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhMucMay.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update DanhMucMay 
                    set MaHieuMay=N'{0}',TenMayMocThietBi=N'{1}',
                    NoiSuDung=N'{2}',QuanLy=N'{3}',TinhTrang=N'{4}' where ID like '{5}'",
                    rowData["MaHieuMay"],
                    rowData["TenMayMocThietBi"],
                    rowData["NoiSuDung"],
                    rowData["QuanLy"],
                    rowData["TinhTrang"],
                    rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                THDanhMucMayThemMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void btnXoaDanhMucMay_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gvDanhMucMay.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhMucMay.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete DanhMucMay where ID like '{0}'",
                    rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                THDanhMucMayThemMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void btnExportDanhMucMay_Click(object sender, EventArgs e)
        {
            this.gvDanhMucMay.OptionsSelection.MultiSelectMode
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            gvDanhMucMay.ShowPrintPreview();
            this.gvDanhMucMay.OptionsSelection.MultiSelectMode 
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            grDanhMucMay.ShowPrintPreview();
        }

        private void btnTraCuuDanhMucMay_Click(object sender, EventArgs e)
        {
            THDanhMucMayThemMoi();
            gvDanhMucMay.OptionsView.NewItemRowPosition =
             DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
    }
}
