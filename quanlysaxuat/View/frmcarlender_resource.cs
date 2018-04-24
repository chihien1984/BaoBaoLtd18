using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using quanlysanxuat.Report;
using DevExpress.XtraPrinting;

namespace quanlysanxuat.View
{
    public partial class frmcarlender_resource : DevExpress.XtraEditors.XtraForm
    {
        public frmcarlender_resource()
        {
            InitializeComponent();
        }

        private void frmcarlender_resource_Load(object sender, EventArgs e)
        {
            txtuser.Text = Login.Username;
            if (Login.role == "1"|| Login.role == "1039")
            {
                pgcapnhatnguonluc.PageVisible = true;
            }
            else
            {
                pgcapnhatnguonluc.PageVisible = false;
            }
            ListMonth();
            cbthang.Text = DateTime.Now.Month.ToString();
            this.gridView1.OptionsSelection.MultiSelectMode
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            dpyearend.Text = DateTime.Now.ToString();
            dpyearstar.Text = DateTime.Now.ToString("01/01/yyyy");
            dpTrangThai_Tu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpTrangThai_Den.Text = DateTime.Now.ToString();
            Category_Resource_By_Year();
            DocTrangThaiNguonLucTheoNgay();
            this.gridView2.Appearance.Row.Font = new Font("Times New Roman", 10f);
            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            dptu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DocKeHoachChiTiet();
            DocKeHoachNgayCongDoanCuoi();
        }

        private void New_Category_Resource()//Tạo mới DanhSach Nguồn lực
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select ResourceID,Ma_Nguonluc,Ten_Nguonluc,
                date01='',date02='',date03='',date04='',date05='',date06='',date07='',date08=''
                ,date09='',date10='',date11='',date12='',date13='',date14='',date15='',date16='',
                date17='',date18='',date19='',date20='',date21=''
                ,date22='',date23='',date24='',date25='',date26='',
                date27='',date28='',date29='',date30='',date31='' from tblResources");
            kn.dongketnoi();
        }
      
        private void Category_Resource_By_Date()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"SELECT RCID,ResourceID,
                BoPhan Ten_Nguonluc,MaBP Ma_Nguonluc,Thang,Nam,
                date01,date02,date03,date04,date05,date06,date07,date08,
                date09,date10,date11,date12,date13,date14,date15,date16,
                date17,date18,date19,date20,date21,date22,date23,date24,
                date25,date26,date27,date28,date29,date30,date31,
                NgayLap,NguoiLap
                FROM dbo.Resource_Calender where Thang ='" + cbthang.Text + "' and Nam='" + dpNam.Text + "' order by Nam desc, Thang desc");
            kn.dongketnoi();
            gridView1.Columns["Thang"].GroupIndex = -1;
        }
        private void Category_Resource_By_Year()
        {
            string thangBD = dpyearstar.Value.ToString("MM");
            string thangKT = dpyearend.Value.ToString("MM");
            string namBD = dpyearend.Value.ToString("yyyy");
            string namKT = dpyearend.Value.ToString("yyyy");
            ketnoi kn = new ketnoi();
            string strQuery = string.Format(@"SELECT RCID,ResourceID,
                BoPhan Ten_Nguonluc,MaBP Ma_Nguonluc,Thang,Nam,
                date01,date02,date03,date04,date05,date06,date07,date08,
                date09,date10,date11,date12,date13,date14,date15,date16,
                date17,date18,date19,date20,date21,date22,date23,date24,
                date25,date26,date27,date28,date29,date30,date31,
                NgayLap,NguoiLap
                FROM dbo.Resource_Calender where
                Nam between '{0}' and '{1}' and
                Thang between '{2}' and '{3}' order by Nam desc, Thang desc", namBD, namKT, thangBD, thangKT);
            gridControl1.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
        }
        private void ListMonth()
        {
            for (int i = 1; i <= 12; i++)
            {
                cbthang.Items.Add(i.ToString());
            }
        }
     
        private void cbthang_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void btnnguon_luc_Click(object sender, EventArgs e)
        {
            frmResources resources = new frmResources();
            resources.ShowDialog();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            New_Category_Resource();
            gridView1.Columns["Thang"].GroupIndex = -1;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Kiểm tra xem trong tháng có xuất hiện 2 nguyên công giống nhau không
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"SELECT MaBP, COUNT(1)Trung 
                FROM Resource_Calender where Thang='{0}' GROUP BY MaBP HAVING COUNT(1) > 1",cbthang.Text);
            var kq = kn.laybang(sqlStr);
            if (kq.Rows.Count > 0)
            {
                MessageBox.Show("Đã có thêm trong tháng", "Notification");
                return;
            }

            else 
            if 
                (this.gridView1.GetSelectedRows().Count() < 0)
            {
                MessageBox.Show("Cần tích chọn", "Thông báo");
                return;
            }
            kn.dongketnoi();

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
                    string strQuery = string.Format(@"insert into Resource_Calender
				    (MaBP,BoPhan,Thang,
                    date01,date02,date03,date04,date05,
				    date06,date07,date08,date09,date10,
				    date11,date12,date13,date14,date15,
				    date16,date17,date18,date19,date20,
				    date21,date22,date23,date24,date25,
				    date26,date27,date28,date29,date30,
                    date31,NguoiLap,Nam,ResourceID,NgayLap)
				    values(N'{0}',N'{1}',N'{2}',
					N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',
					N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',
					N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',
					N'{18}',N'{19}',N'{20}',N'{21}',N'{22}',
					N'{23}',N'{24}',N'{25}',N'{26}',N'{27}',
					N'{28}',N'{29}',N'{30}',N'{31}',N'{32}',
                    N'{33}',N'{34}',N'{35}',N'{36}',GetDate())",
                    rowData["Ma_Nguonluc"], rowData["Ten_Nguonluc"], cbthang.Text,
                    rowData["date01"], rowData["date02"], rowData["date03"], rowData["date04"], rowData["date05"],
                    rowData["date06"], rowData["date07"], rowData["date08"], rowData["date09"], rowData["date10"],
                    rowData["date11"], rowData["date12"], rowData["date13"], rowData["date14"], rowData["date15"],
                    rowData["date16"], rowData["date17"], rowData["date18"], rowData["date19"], rowData["date20"],
                    rowData["date21"], rowData["date22"], rowData["date23"], rowData["date24"], rowData["date25"],
                    rowData["date26"], rowData["date27"], rowData["date28"], rowData["date29"], rowData["date30"], 
                    rowData["date31"], txtuser.Text, dpNam.Text, rowData["ResourceID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                Category_Resource_By_Date();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() < 0)
            { MessageBox.Show("Cần tích chọn", "Thông báo"); return; }
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
                    string strQuery = string.Format(@"update Resource_Calender
				    set 
                    date01='{0}',date02='{1}',date03='{2}',date04='{3}',date05='{4}',
				    date06='{5}',date07='{6}',date08='{7}',date09='{8}',date10='{9}',
				    date11='{10}',date12='{11}',date13='{12}',date14='{13}',date15='{14}',
				    date16='{15}',date17='{16}',date18='{17}',date19='{18}',date20='{19}',
				    date21='{20}',date22='{21}',date23='{22}',date24='{23}',date25='{24}',
				    date26='{25}',date27='{26}',date28='{27}',date29='{28}',date30='{29}',
                    date31='{30}' where RCID='{31}'",
                    rowData["date01"], rowData["date02"], rowData["date03"], rowData["date04"], rowData["date05"],
                    rowData["date06"], rowData["date07"], rowData["date08"], rowData["date09"], rowData["date10"],
                    rowData["date11"], rowData["date12"], rowData["date13"], rowData["date14"], rowData["date15"],
                    rowData["date16"], rowData["date17"], rowData["date18"], rowData["date19"], rowData["date20"],
                    rowData["date21"], rowData["date22"], rowData["date23"], rowData["date24"], rowData["date25"],
                    rowData["date26"], rowData["date27"], rowData["date28"], rowData["date29"], rowData["date30"],
                    rowData["date31"],rowData["RCID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                Category_Resource_By_Date();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                    string strQuery = string.Format(@"delete from Resource_Calender where RCID='{0}'",
                    rowData["RCID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                Category_Resource_By_Date();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            Category_Resource_By_Year();
            //gridView1.Columns["Thang"].GroupIndex= 0;
            //gridView1.ExpandAllGroups();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            //string repoint = "";
            //repoint=gridView1.GetFocusedDisplayText();
            //cbthang.Text = gridView1.GetFocusedRowCellDisplayText(thang_col1);
        }
        
        private void DocTrangThaiNguonLucTheoNgay()
        {
         

            string thangBD = dpTrangThai_Tu.Value.ToString("MM");
            string thangKT = dpTrangThai_Den.Value.ToString("MM");
            string namBD = dpTrangThai_Tu.Value.ToString("yyyy");
            string namKT = dpTrangThai_Den.Value.ToString("yyyy");
            ketnoi kn = new ketnoi();
            string strQuery = string.Format(@"select 'Tháng '+cast(R.Thang as varchar)+N' Năm '+cast(R.Nam as varchar) ThoiGian,MaBP,BoPhan,R.Thang,R.Nam,
			 (R.date01-isnull(P.date01,0))date01,(R.date02-isnull(P.date02,0))date02,(R.date03-isnull(P.date03,0))date03,(R.date04-isnull(P.date04,0))date04,(R.date05-isnull(P.date05,0))date05,
             (R.date06-isnull(P.date06,0))date06,(R.date07-isnull(P.date07,0))date07,(R.date08-isnull(P.date08,0))date08,(R.date09-isnull(P.date09,0))date19,(R.date10-isnull(P.date10,0))date10,
             (R.date11-isnull(P.date11,0))date11,(R.date12-isnull(P.date12,0))date12,(R.date13-isnull(P.date13,0))date13,(R.date14-isnull(P.date14,0))date14,(R.date15-isnull(P.date15,0))date15,
             (R.date16-isnull(P.date16,0))date16,(R.date17-isnull(P.date17,0))date17,(R.date18-isnull(P.date18,0))date17,(R.date19-isnull(P.date19,0))date19,(R.date20-isnull(P.date20,0))date20,
             (R.date21-isnull(P.date21,0))date21,(R.date22-isnull(P.date22,0))date22,(R.date23-isnull(P.date23,0))date23,(R.date24-isnull(P.date24,0))date24,(R.date25-isnull(P.date25,0))date25,
             (R.date26-isnull(P.date26,0))date26,(R.date27-isnull(P.date27,0))date27,(R.date28-isnull(P.date28,0))date28,(R.date29-isnull(P.date29,0))date29,(R.date30-isnull(P.date30,0))date30,
             (R.date30-isnull(P.date30,0))date31
			  from Resource_Calender R
			  left outer join
			 (select NguyenCong,thang,nam,
                cast(sum(date01/Dinhmuc) as float) date01,
                sum(date02/Dinhmuc) date02,sum(date03/Dinhmuc) date03,sum(date04/Dinhmuc) date04,sum(date05/Dinhmuc) date05,
                sum(date06/Dinhmuc) date06,sum(date07/Dinhmuc) date07,sum(date08/Dinhmuc) date08,sum(date09/Dinhmuc) date09,
                sum(date10/Dinhmuc) date10,sum(date11/Dinhmuc) date11,sum(date12/Dinhmuc )date12,sum(date13/Dinhmuc) date13,
                sum(date14/Dinhmuc) date14,sum(date15/Dinhmuc) date15,sum(date16/Dinhmuc)date16,sum(date17/Dinhmuc ) date17,
                sum(date18/Dinhmuc) date18,sum(date19/Dinhmuc) date19,sum(date20/Dinhmuc)date20,sum(date21/Dinhmuc ) date21,
                sum(date22/Dinhmuc) date22,sum(date23/Dinhmuc) date23,sum(date24/Dinhmuc)date24,sum(date25/Dinhmuc ) date25,
                sum(date26/Dinhmuc) date26,sum(date27/Dinhmuc) date27,sum(date28/Dinhmuc)date28,sum(date29/Dinhmuc ) date29,
                sum(date30/Dinhmuc) date30 from tblCalender_Product where NguyenCong <>''
                Group by NguyenCong,thang,nam)P on R.MaBP=P.NguyenCong and R.Thang=P.thang and R.Nam=P.nam
                where
                R.Nam between '{0}' and '{1}' and
                R.Thang between '{2}' and '{3}' order by R.Nam desc, R.Thang desc", namBD, namKT, thangBD, thangKT);
            gridControl2.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
            //gridView2.Columns["ThoiGian"].GroupIndex = 0;
            gridView2.ExpandAllGroups();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DocTrangThaiNguonLucTheoNgay();
        }
        private void btnTraCuuTrangThaiNguonLuc_Click(object sender, EventArgs e)
        {
            DocTrangThaiNguonLucTheoNgay();
        }

        private void btnTraCuuDSNguonLuc_Click(object sender, EventArgs e)
        {
            DocKeHoachChiTiet();
        }
        private void DocKeHoachChiTiet()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select madh +'-'+C.MaSP+'-'+C.Tenquicach
                ThonTin,C.madh,C.MaSP,
		        P.* from tblCalender_Product P
		        left outer join tblDHCT C
		        on P.DonHangID=C.Iden where NgayLap 
                between '{0}' and '{1}'",
                dptu.Value.ToString("yyyy-MM-dd"),
                dpden.Value.ToString("yyyy-MM-dd"));
            gridControl3.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            gridView3.ExpandAllGroups();
        }
        private void ntndocKeHoachHangNgay_Click(object sender, EventArgs e)
        {
            DocKeHoachNgayCongDoanCuoi();
        }
        private void DocKeHoachNgayCongDoanCuoi()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select p.DonHangID,p.NgayLap,p.NguoiLap,
            p.ProDucID,p.NguyenCong,N'Đơn hàng: '+ c.madh+char(13)+'; '+c.Tenquicach+
            char(13)+N'; '+cast(format(p.SoLuongDonHang,'#,#') as nvarchar)
            +N';Chi tiết'+cast(SoChiTiet as nvarchar)+
            N'; Cần làm: '+cast(format(p.SoLuongChiTietDonHang,'#,#') as nvarchar)
            ThongTinSanXuat,
            N'Công: '+ p.Tencondoan +
            N'; Định mức: '+ cast(Dinhmuc as nvarchar)+
            N'; BQ/Ngày: '+cast(format(SanLuongBQ,'#,#') as nvarchar)NangLucSanXuat,
            n.BatDau,n.KetThuc,
            date01,date02,date03,date04,date05,date06,date07,date08,date09,
            date10,date11,date12,date13,date14,date15,date16,date17,date18,date19,
            date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,
            date30,date31 from
            (select t.ProDucID,t.DonHangID,t.NguyenCong,t.Tencondoan,t.BatDau,e.KetThuc from
            (select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
            Min(BatDau)BatDau from tblCalender_Product
            group by DonHangID,NguyenCong)t
            left outer join
            (select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
            max(KetThuc)KetThuc from tblCalender_Product
            group by DonHangID,NguyenCong)e
            on t.ProDucID=e.ProDucID)n
            left outer join  
            (select ProDucID,DinhMucID,DonHangID,Masp,Tencondoan,Dinhmuc,NgayCanLam,
            EpCongSuat,NguyenCong,Sunday,SanLuongBQ,thang,nam,TrienKhai,
            ConLai,date01,date02,date03,date04,date05,date06,date07,date08,date09,
            date10,date11,date12,date13,date14,date15,date16,date17,date18,date19,
            date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,
            date30,date31,NguoiLap,NgayLap,SoChiTiet,SoLuongChiTietDonHang,TonKhoChiTiet,
            SoLuongDonHang from tblCalender_Product) p
            on p.ProDucID=n.ProDucID
            left outer join 
            (select Iden,madh,Tenquicach,Soluong from tblDHCT) c
            on p.DonHangID=c.Iden where NgayLap 
                                between '{0}' and '{1}'",
                dptu.Value.ToString("yyyy-MM-dd"),
                dpden.Value.ToString("yyyy-MM-dd"));
            gridControl6.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            gridView6.ExpandAllGroups();
        }
        private void DocKeHoachNgayCongDoanCuoiTheoIDDonHang()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select p.DonHangID,p.NgayLap,p.NguoiLap,
            p.ProDucID,p.NguyenCong,N'Đơn hàng: '+ c.madh+char(13)+'; '+c.Tenquicach+
            char(13)+N'; '+cast(format(p.SoLuongDonHang,'#,#') as nvarchar)
            +N';Chi tiết'+cast(SoChiTiet as nvarchar)+
            N'; Cần làm: '+cast(format(p.SoLuongChiTietDonHang,'#,#') as nvarchar)
            ThongTinSanXuat,
            N'Công: '+ p.Tencondoan +
            N'; Định mức: '+ cast(Dinhmuc as nvarchar)+
            N'; BQ/Ngày: '+cast(format(SanLuongBQ,'#,#') as nvarchar)NangLucSanXuat,
            n.BatDau,n.KetThuc,
            date01,date02,date03,date04,date05,date06,date07,date08,date09,
            date10,date11,date12,date13,date14,date15,date16,date17,date18,date19,
            date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,
            date30,date31 from
            (select t.ProDucID,t.DonHangID,t.NguyenCong,t.Tencondoan,t.BatDau,e.KetThuc from
            (select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
            Min(BatDau)BatDau from tblCalender_Product
            group by DonHangID,NguyenCong)t
            left outer join
            (select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
            max(KetThuc)KetThuc from tblCalender_Product
            group by DonHangID,NguyenCong)e
            on t.ProDucID=e.ProDucID)n
            left outer join  
            (select ProDucID,DinhMucID,DonHangID,Masp,Tencondoan,Dinhmuc,NgayCanLam,
            EpCongSuat,NguyenCong,Sunday,SanLuongBQ,thang,nam,TrienKhai,
            ConLai,date01,date02,date03,date04,date05,date06,date07,date08,date09,
            date10,date11,date12,date13,date14,date15,date16,date17,date18,date19,
            date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,
            date30,date31,NguoiLap,NgayLap,SoChiTiet,SoLuongChiTietDonHang,TonKhoChiTiet,
            SoLuongDonHang from tblCalender_Product) p
            on p.ProDucID=n.ProDucID
            left outer join 
            (select Iden,madh,Tenquicach,Soluong from tblDHCT) c
            on p.DonHangID=c.Iden where p.DonHangID = '{0}'",txtIDDonHang.Text);
            gridControl6.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            gridView6.ExpandAllGroups();
        }
        private void btnExportLichSanXuatChiTiet_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }

        private void btnXatKeHoachHangNgay_Click(object sender, EventArgs e)
        {
            gridControl6.ShowPrintPreview();
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gridView3.GetFocusedDisplayText();
            txtMaDonHang.Text = gridView3.GetFocusedRowCellDisplayText(madonhang_col);
            txtIDDonHang.Text= gridView3.GetFocusedRowCellDisplayText(idDonHang_col);
            DocKeHoachNgayCongDoanCuoiTheoIDDonHang();
        }

        private void btnKeHoachSanXuat_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@"select * from vw_KeHoachNgayCongDoanCuoi where madh= '" + txtMaDonHang.Text + "' order by KetThuc ASC ");
            XRLichSanXuaTheoNgay rpKiemHang = new XRLichSanXuaTheoNgay();
            rpKiemHang.DataSource = dt;
            rpKiemHang.DataMember = "Table";
            rpKiemHang.CreateDocument(false);
            rpKiemHang.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaDonHang.Text;
            PrintTool tool = new PrintTool(rpKiemHang.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }
    }
}
