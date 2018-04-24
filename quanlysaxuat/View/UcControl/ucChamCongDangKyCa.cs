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
using DevExpress.DataAccess.Sql;
using DevExpress.XtraRichEdit.Model;
using quanlysanxuat.Model;
using DevExpress.Web;

namespace quanlysanxuat.View.UcControl
{
    public partial class ucChamCongDangKyCa : DevExpress.XtraEditors.XtraForm
    {
        public ucChamCongDangKyCa()
        {
            InitializeComponent();
        }
        private string deptID;
        private string userID;
        private string hoten;
        //formload
        private void ucChamCongDangKyCa_Load(object sender, EventArgs e)
        {
            ChinhSuaChamCong();
            Model.Function.ConnectChamCong();//Mo ket noi
            dpTu.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            this.treeListPhongBan.Appearance.Row.Font = new Font("Segoe UI", 10f);
            this.gvDanhSachNhanVien.Appearance.Row.Font = new Font("Segoe UI", 7f);
            this.gvChiTietDangKyCa.Appearance.Row.Font = new Font("Segoe UI", 7f);
            
            gvDanhSachNhanVien.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gvChiTietDangKyCa.OptionsSelection.CheckBoxSelectorColumnWidth = 20;

            TheHienDanhMucPhongBan();
          
            TheHienMaCa();
            ThayDoiMaCa();
            ThayDoiMaCaDK();
            
            TheHienMaTangCa();
            ThayDoiMaTangCa();
            ThayDoiMaTangCaDK();
            TheHienDanhSachNhanVienTheoPhongBan();
            Model.Function.Disconnect();//Dong ket noi
        }
        private void ChinhSuaChamCong()
        {
            if (Login.role == "1" || Login.role == "39")
            {
                btnDuyetKhoa.Visible = true;
                btnXoaDaDuyet.Visible = true;
                btnCapNhat.Visible = false;
                btnXoa.Visible = false;
            }
            else
            {
                btnCapNhat.Visible = true;
                btnXoa.Visible = true;
                btnDuyetKhoa.Visible = false;
                btnXoaDaDuyet.Visible = false;
            }
        }
        private void TheHienDanhMucPhongBan()
        {
            string sqlQuery = string.Format(@"select RelationID ParentID,LevelID,
                ID,Description from RelationDept");
            treeListPhongBan.DataSource = Model.Function.GetDataTable(sqlQuery);
            treeListPhongBan.ForceInitialize();
            treeListPhongBan.ExpandAll();
            treeListPhongBan.BestFitColumns();
            treeListPhongBan.OptionsSelection.MultiSelect = true;
            treeListPhongBan.Appearance.Row.Font = new Font("Segoe UI", 7f); 
        }

        private void TheHienTatCaNhanVien()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select UserFullCode,UserCardNo,
                UserFullName,UserIDD,b.Description,'{0}' MaCa,'' MaTangCa,b.ID,'' MoTa
                from UserInfo a
                inner join RelationDept b
                on a.UserIDD=b.ID","HC");
            grDanhSachNhanVien.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
        }

        private void TheHienDanhSachNhanVienTheoPhongBan()
        {
            //Model.Function.ConnectChamCong();//Mo ket noi
            //string sqlQuery = string.Format(@"select UserFullCode,UserCardNo,
            //    UserFullName,UserIDD,b.Description,'{0}' MaCa,'' MaTangCa,b.ID,'' MoTa
            //    from UserInfo a
            //    inner join RelationDept b
            //    on a.UserIDD=b.ID where a.UserIDD like '{1}'","HC", deptID);
            //grDanhSachNhanVien.DataSource = Model.Function.GetDataTable(sqlQuery);

            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select UserFullCode,UserCardNo,
                UserFullName,UserIDD,b.Description,'{0}'MaCa,'{1}'MaTangCa,b.ID,'' MoTa
                from UserInfo a
                inner join RelationDept b
                on a.UserIDD=b.ID where a.UserIDD like '{2}'",
                "HC","", deptID);
            grDanhSachNhanVien.DataSource = Model.Function.GetDataTable(sqlQuery);
            gvDanhSachNhanVien.SelectAll();
        }
     
        private void TheHienMaCa()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select MaCa,MoTa,HeSo from [datachamcong moi].[dbo].[LoaiCa] order by ID asc");
            grlChonLoaiCaLamViec.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
            grlChonLoaiCaLamViec.Properties.DisplayMember = "MaCa";
            grlChonLoaiCaLamViec.Properties.ValueMember = "MaCa";
            grlChonLoaiCaLamViec.EditValue = grlChonLoaiCaLamViec.Properties.GetKeyValue(0);
        }
        private void ThayDoiMaCa()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select MaCa,MoTa,HeSo from [datachamcong moi].[dbo].[LoaiCa] order by ID asc");
            repositoryItemGridLookUpEditMaCa.DataSource = Model.Function.GetDataTable(sqlQuery);
            repositoryItemGridLookUpEditMaCa.DisplayMember = "MaCa";
            repositoryItemGridLookUpEditMaCa.ValueMember = "MaCa";
        }
        //Thay doi ma ca da dang ky
        private void ThayDoiMaCaDK()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select ID,MaCa,MoTa,HeSo from [datachamcong moi].[dbo].[LoaiCa] order by ID asc");
            repositoryItemGridLookUpEditMaCaDK.DataSource = Model.Function.GetDataTable(sqlQuery);
            repositoryItemGridLookUpEditMaCaDK.DisplayMember = "MaCa";
            repositoryItemGridLookUpEditMaCaDK.ValueMember = "MaCa";
        }

        private void TheHienMaTangCa()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select ID,MaTangCa,MoTa,HeSo from [datachamcong moi].[dbo].[LoaiTangCa] order by ID asc");
            grlChonLoaiTangCa.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
            grlChonLoaiTangCa.Properties.DisplayMember = "MaTangCa";
            grlChonLoaiTangCa.Properties.ValueMember = "MaTangCa";
            grlChonLoaiTangCa.EditValue = grlChonLoaiCaLamViec.Properties.GetKeyValue(0);
        } 
        private void ThayDoiMaTangCa()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select ID,MaTangCa,MoTa,HeSo from [datachamcong moi].[dbo].[LoaiTangCa] order by ID asc");
            repositoryItemGridLookUpEditMaTangCa.DataSource = Model.Function.GetDataTable(sqlQuery);
            repositoryItemGridLookUpEditMaTangCa.DisplayMember = "MaTangCa";
            repositoryItemGridLookUpEditMaTangCa.ValueMember = "MaTangCa";
        }
        //Thay doi ma tang ca da dang ky
        private void ThayDoiMaTangCaDK()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select ID,MaTangCa,MoTa,HeSo from [datachamcong moi].[dbo].[LoaiTangCa] order by ID asc");
            repositoryItemGridLookUpEditMaTangCaDK.DataSource = Model.Function.GetDataTable(sqlQuery);
            repositoryItemGridLookUpEditMaTangCaDK.DisplayMember = "MaTangCa";
            repositoryItemGridLookUpEditMaTangCaDK.ValueMember = "MaTangCa";
        }

        private void treeListPhongBan_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string point;
            point = treeListPhongBan.GetFocusedDisplayText();
            deptID = treeListPhongBan.GetFocusedRowCellDisplayText(treeListColumnDeptID);
            
          
            //if (treeListPhongBan.GetFocusedRowCellDisplayText(treeListColumnDeptID) == "0")
            //{
            //    TheHienTatCaNhanVien();
            //}
            //else
            //{
                TheHienDanhSachNhanVienTheoPhongBan();
            //}  
            TheHienDanhSachDangKyCaTheoBoPhan();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
        }

        private void grlChonLoaiCaLamViec_EditValueChanged(object sender, EventArgs e)
        {
            ThemMaCa();
        }

        private void ThemMaCa()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select UserFullCode,UserCardNo,
                UserFullName,UserIDD,b.Description,'{0}'MaCa,'{1}'MaTangCa,b.ID,'' MoTa
                from UserInfo a
                inner join RelationDept b
                on a.UserIDD=b.ID where a.UserIDD like '{2}'", 
                grlChonLoaiCaLamViec.Text, 
                grlChonLoaiTangCa.Text, deptID);
            grDanhSachNhanVien.DataSource = Model.Function.GetDataTable(sqlQuery);
            gvDanhSachNhanVien.SelectAll();
        }

        private void grlChonLoaiTangCa_EditValueChanged(object sender, EventArgs e)
        {
            ThemMaCa();
        }
        private void TheHienDanhSachDangKyCaTheoBoPhan()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"	select ID,MaThe,MaCa,Tu,Den,
				MaTangCa,HoTen,Dep,
				DepID,DienGiai,TinhTrang,NguoiLap,
				NgayLap,NguoiDuyet,NgayDuyet
				from LoaiDangKyCa where NgayLap between '{0}' and '{1}' 
                and DepID like '{2}' and Xoa like 'Y' order by den desc", 
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"),
                deptID);
            grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
        private void TheHienDanhSachDangKyCaTheoNhanVien()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"	select ID,MaThe,MaCa,Tu,Den,
				MaTangCa,HoTen,Dep,
				DepID,DienGiai,TinhTrang,NguoiLap,
				NgayLap,NguoiDuyet,NgayDuyet
				from LoaiDangKyCa where NgayLap between '{0}' and '{1}' 
                and DepID like '{2}' and Xoa like 'Y' order by den desc",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"), 
                nhanviendepID);
            grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
        private void TheHienTatCaDanhSachDangKyCa()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"	select ID,MaThe,MaCa,Tu,Den,
				MaTangCa,HoTen,Dep,
				DepID,DienGiai,TinhTrang,NguoiLap,
				NgayLap,NguoiDuyet,NgayDuyet
				from LoaiDangKyCa where Xoa like 'Y' order by den desc");
            grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
        private void btnApDungMaCa_Click(object sender, EventArgs e)
        {
            DateTime tu = dpNgayApDungTu.Value;
            DateTime den = dpNgayAppDungDen.Value;
            if (tu <= den) { MessageBox.Show("Ngày bắt đầu không được lớn hơn kết thúc", "Error...");return; }
            else { 
            try
            {
                Model.Function.ConnectChamCong();//Mo ket noi
                DataRow rowData;
                int[] listRowList = this.gvDanhSachNhanVien.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhSachNhanVien.GetDataRow(listRowList[i]);
                    //MessageBox.Show("",""+rowData["MaCa"].ToString());
                    string strQuery = string.Format(@"insert into LoaiDangKyCa 
                        (MaThe,MaCa,
				        Tu,Den,MaTangCa,
				        HoTen,Dep,
				        DepID,DienGiai,
				        TinhTrang,NguoiLap,NgayLap)
				        values ('{0}',N'{1}',
                        '{2}','{3}',N'{4}',
                        N'{5}',N'{6}',
                        '{7}',N'{8}',
                        N'{9}','{10}',GetDate());
                        update LoaiDangKyCa set HeSoCa=a.HeSo
                        from LoaiCa a
                        where LoaiDangKyCa.MaCa=a.MaCa 
                        and HeSoCa is null;
                        update LoaiDangKyCa 
                        set HeSoTangCa=b.HeSo
                        from LoaiTangCa b 
                        where LoaiDangKyCa.MaTangCa=b.MaTangCa
                        and HeSoTangCa is null",
                      rowData["UserFullCode"], rowData["MaCa"].ToString()==""? "HC" : rowData["MaCa"],
                      dpNgayApDungTu.Value.ToString("yyyy-MM-dd"),
                      dpNgayAppDungDen.Value.ToString("yyyy-MM-dd"), rowData["MaTangCa"],
                      rowData["UserFullName"], rowData["Description"],
                      rowData["UserIDD"], rowData["MoTa"], 
                      "Đã báo ca", Login.Username);
                      grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(strQuery);
                }
                TheHienDanhSachDangKyCaTheoBoPhan();
                MessageBox.Show("Success", "!");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
            }
        }
        private void CapNhatHeSoCa()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"	select ID,MaThe,MaCa,Tu,Den,
				MaTangCa,HoTen,Dep,
				DepID,DienGiai,TinhTrang,NguoiLap,
				NgayLap,NguoiDuyet,NgayDuyet
				from LoaiDangKyCa where Xoa like 'Y' order by den desc");
            grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
        private void CapNhatHeSoTangCa()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"	select ID,MaThe,MaCa,Tu,Den,
				MaTangCa,HoTen,Dep,
				DepID,DienGiai,TinhTrang,NguoiLap,
				NgayLap,NguoiDuyet,NgayDuyet
				from LoaiDangKyCa where Xoa like 'Y' order by den desc");
            grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
        string nhanviendepID;
        private void grDanhSachNhanVien_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gvDanhSachNhanVien.GetFocusedDisplayText();
            nhanviendepID = gvDanhSachNhanVien.GetFocusedRowCellDisplayText(DepID);
            TheHienDanhSachDangKyCaTheoNhanVien();
        }

        private void btnTraCuuBaoDangKyCa_Click(object sender, EventArgs e)
        {
            TheHienTatCaDanhSachDangKyCa();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                Model.Function.ConnectChamCong();
                DataRow rowData;
                int[] listRowList = this.gvChiTietDangKyCa.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChiTietDangKyCa.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update LoaiDangKyCa 
                        set MaCa =N'{0}',MaTangCa=N'{1}',
                        Tu='{2}',Den='{3}',
				        DienGiai=N'{4}',NguoiLap =N'{5}',NgayLap=GetDate() 
                        where ID like '{6}' and TinhTrang like N'Đã báo ca';
                        update LoaiDangKyCa set HeSoCa=a.HeSo
                         from LoaiCa a
                         where LoaiDangKyCa.MaCa=a.MaCa 
                         and LoaiDangKyCa.ID like '{6}';
                         update LoaiDangKyCa 
                         set HeSoTangCa=b.HeSo
                         from LoaiTangCa b 
                         where LoaiDangKyCa.MaTangCa=b.MaTangCa
                         and LoaiDangKyCa.ID like '6'",
                      rowData["MaCa"], rowData["MaTangCa"],
                      Convert.ToDateTime(rowData["Tu"]).ToString("yyyy-MM-dd"), 
                      Convert.ToDateTime(rowData["Den"]).ToString("yyyy-MM-dd"),
                      rowData["DienGiai"],Login.Username,rowData["ID"]);
                    grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(strQuery);
                }
                TheHienDanhSachDangKyCaTheoBoPhan();
                MessageBox.Show("Success", "!");
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        //Duyet khoa dang ky ca
        private void btnDuyetKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                Model.Function.ConnectChamCong();
                DataRow rowData;
                int[] listRowList = this.gvChiTietDangKyCa.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChiTietDangKyCa.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update LoaiDangKyCa 
                        set MaCa = N'{0}',MaTangCa = N'{1}',
                        Tu = '{2}',Den= '{3}',
				        DienGiai = N'{4}',TinhTrang = N'{5}',NguoiDuyet = N'{6}', NgayDuyet = GetDate() 
                        where ID like '{7}'; 
                         update LoaiDangKyCa set HeSoCa=a.HeSo
                         from LoaiCa a
                         where LoaiDangKyCa.MaCa=a.MaCa 
                         and LoaiDangKyCa.ID like '{7}';
                         update LoaiDangKyCa 
                         set HeSoTangCa=b.HeSo
                         from LoaiTangCa b 
                         where LoaiDangKyCa.MaTangCa=b.MaTangCa
                         and LoaiDangKyCa.ID like '{7}'",
                      rowData["MaCa"], rowData["MaTangCa"],
                      Convert.ToDateTime(rowData["Tu"]).ToString("yyyy-MM-dd"),
                      Convert.ToDateTime(rowData["Den"]).ToString("yyyy-MM-dd"),
                      rowData["DienGiai"],"Đã duyệt",
                      Login.Username, rowData["ID"]);
                    grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(strQuery);
                }
               
                MessageBox.Show("Success", "!");
                TheHienDanhSachDangKyCaTheoBoPhan();
           
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                Model.Function.ConnectChamCong();
                DataRow rowData;
                int[] listRowList = this.gvChiTietDangKyCa.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChiTietDangKyCa.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update LoaiDangKyCa 
                        set Xoa='D', NguoiLap = N'{0}',NgayLap=GetDate() 
                        where ID like '{1}' and TinhTrang like N'Đã báo ca'", Login.Username,rowData["ID"]);
                    grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(strQuery);
                }
                TheHienDanhSachDangKyCaTheoBoPhan();
                MessageBox.Show("Success", "!");
     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }

        private void btnXoaDaDuyet_Click(object sender, EventArgs e)
        {
            try
            {
                Model.Function.ConnectChamCong();
                DataRow rowData;
                int[] listRowList = this.gvChiTietDangKyCa.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChiTietDangKyCa.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update LoaiDangKyCa 
                        set Xoa = 'D', NguoiDuyet = N'{0}',NgayDuyet=GetDate() 
                        where ID like '{1}'", Login.Username, rowData["ID"]);
                    grChiTietDangKyCa.DataSource = Model.Function.GetDataTable(strQuery);
                }
                TheHienDanhSachDangKyCaTheoBoPhan();
                MessageBox.Show("Success", "!");
     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }

        private void btnTrinhKy_Click(object sender, EventArgs e)
        {

        }
    }
}
