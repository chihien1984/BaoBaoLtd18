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
using System.Data.SqlClient;
using DevExpress.Xpf.Grid;

namespace quanlysanxuat
{
    public partial class frmCongDoan : DevExpress.XtraEditors.XtraForm
    {
        FtpClient FtpClient;
        public frmCongDoan()
        {
            InitializeComponent();
            FtpClient = new FtpClient("ftp://192.168.1.22", "ftpPublic", "ftp#1234");
        }

        private void btnUpdate_Click(object sender, EventArgs e)//Duyet luu danh sach cong doan moi
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
                    string strQuery = string.Format(@"insert into tblCalender_Product(IdDonHang,Madh,Masp,Ma_CongDoan,CongDoan,Thoigian_Dinhmuc, "
                    + "Soluong_DonHang,TG_ChuanBi,TG_DuTru,BatDau,KetThuc,WorkDay,TG_DUTINH,TONGTG_DUTINH,Ma_BoPhan,ID_DMLD,TONGTG_DUTINHTYLE) "
                    + "VALUES ('{0}','{1}','{2}',N'{3}',N'{4}','{5}','{6}',N'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')",
                    txtIdDonHang.Text, txtMaDH.Text, txtMaSP.Text, rowData["Macongdoan"], rowData["Tencondoan"], rowData["Dinhmuc"],
                    txtSoluong.Text, rowData["ThoigianCB"], rowData["TG_DuTru"],
                    rowData["BatDau"]==DBNull.Value ? "" : Convert.ToDateTime(rowData["BatDau"]).ToString("MM/dd/yyyy"),
                    rowData["KetThuc"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["KetThuc"]).ToString("MM/dd/yyyy"), rowData["WorkDay"],
                    rowData["TG_DUTINH"], rowData["TONGTG_DUTINH"], rowData["Tothuchien"], rowData["id"], rowData["TONGTG_DUTINH"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
               con.Close(); DS();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void DMCongDoan()//Goi danh muc dinh muc cong doan
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select id,Masp,Tensp,Macongdoan,Tencondoan,Dinhmuc, "
                                                + "Tothuchien,Nguoilap,convert(Numeric(4,2)," + txtSoluong.Text + "/Dinhmuc) AS TG_DUTINH,'' AS ThoigianCB,'' as TG_DuTru,convert(Date,NULL,101)as BatDau, "
                                                + "convert(Date,NULL,101) as KetThuc,NULL as WorkDay,convert(numeric(4,2)," + txtSoluong.Text + "/Dinhmuc) as TONGTG_DUTINH,PB.To_bophan "
                                                + "from tblDMuc_LaoDong LD left outer join tblPHONGBAN PB on PB.Ma_bophan=LD.Tothuchien where Masp like N'" + txtMaSP.Text + "'");
            kn.dongketnoi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }
        private void List_DMSP()
        {
            DataTable Table = new DataTable();
            ketnoi Connect = new ketnoi();
            glMasp.Properties.DataSource = Connect.laybang(@"select Iden,thoigianthaydoi,nguoithaydoi,madh,CT.Masp,Tenquicach,LD.Masp as CongDoan,Soluong 
                                                  from tblDHCT CT left outer join 
                                                  (select Distinct Masp from tblDMuc_LaoDong) LD 
                                                  on  CT.MaSP=LD.Masp where CT.Masp <>'' order by thoigianthaydoi DESC");
            glMasp.Properties.DisplayMember = "Masp";
            glMasp.Properties.ValueMember = "Masp";
            glMasp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            glMasp.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            glMasp.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        private void _CongDoan()
        {

        }
        private void BinDingCheckMa()//Check ma san pham, ten san pham
        {
            try
            {
                string Gol;
                Gol = gridLookUpEdit1View.GetFocusedDisplayText();
                txtMaDH.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Madh_look);
                txtMaSP.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Masp_look);
                txtSoluong.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Soluong_look);
                txtSanpham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tenqc_look);
                txtIdDonHang.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(id_look);
            }
            catch (Exception)
            {}
            
        }
        private void glMasp_EditValueChanged(object sender, EventArgs e)
        {
            BinDingCheckMa();
            DMCongDoan();
            //DSDH_TINHLICHSX_CUNGMA();
        }

        private void frmCongDoan_Load(object sender, EventArgs e)//From Load
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); 
            dpden_ngay.Text = DateTime.Now.ToString();//Ngay bat dau va ket thu thang
            txtUser.Text = Login.Username;
            List_DMSP();
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 40;
            gridView2.OptionsView.AllowHtmlDrawHeaders = true; gridView2.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView2.ColumnPanelRowHeight = 40;
            //glMasp.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            //dòng này để gridcontrol trong GridlookupEdit tự động resize các column để không thừa không thiếu nội dung
            glMasp.Properties.ImmediatePopup = true;
            // dòng này tự động mở popup khi search có kết quả
            glMasp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            //Setup dòng này để có thể nhập vào gridlookup
            ComBoBox_Resource();
            if (txtUser.Text == "Đàm Thị Ánh Mai"){
                btnUpdate.Enabled = true;
                BtnGhiThoiGianSXUocLuong.Enabled = true;
                BtnHuy.Enabled = true;
                BtnXoa.Enabled = true;
            }
        }
        
        private void LookupGrid()
        {
            ketnoi kn = new ketnoi();
            DataTable dt = new DataTable();
            dt = kn.laybang("select Ma_Nguonluc,Ten_Nguonluc from tblResources");
            List<tblDelivery_Dtl> details = new List<tblDelivery_Dtl>();
            details = Utils.ConvertDataTable<tblDelivery_Dtl>(dt);
            gridControl2.DataSource = new BindingList<tblDelivery_Dtl>(details);
            gridControl2.DataSource = dt;
            kn.dongketnoi();
        }

        private void ComBoBox_Resource()//Combobox chon ma nguon luc trong grid control
        {       
            ketnoi Connect = new ketnoi();
            repositoryItemGridLookUpEdit1.DataSource = Connect.laybang("select Ma_Nguonluc,Ten_Nguonluc from tblResources");
            repositoryItemGridLookUpEdit1.DisplayMember = "Ma_Nguonluc";
            repositoryItemGridLookUpEdit1.ValueMember = "Ma_Nguonluc";
            repositoryItemGridLookUpEdit1.NullText = null;
            repositoryItemGridLookUpEdit1.PopupFilterMode = PopupFilterMode.Contains;
            repositoryItemGridLookUpEdit1.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            repositoryItemGridLookUpEdit1.View.OptionsView.ShowAutoFilterRow = true;
            repositoryItemGridLookUpEdit1.EditValueChanged += Reposi_CongDoan_EditValueChanged;
            repositoryItemGridLookUpEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemGridLookUpEdit1.ImmediatePopup = true;
            Connect.dongketnoi();
        }

        private void Value_CellChange(object sender,EventArgs e) { }

        private void gridControl1_TextChanged(object sender, EventArgs e)
        {}

        private double getDataValue(string tmp) {
            try
            {
                return Convert.ToDouble(tmp);
            }
            catch (Exception) {
                return 0;
            }
        }
        //Hàm tinh tong thoi gian thuc hien cong doan (Tong thoi gian = thoi gian dinh muc + thoi gian chuan bi + thoi gian du tru)
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null) return;
                string columnName = e.Column.Name.ToString();
                if (columnName == "TG_DUTINH_grid" || columnName == "ThoigianCB_grid" || columnName == "TGDuTru_grid")
                {
                    double tongTgDuTinh = this.getDataValue(view.GetRowCellValue(e.RowHandle, "TG_DuTru").ToString());
                    tongTgDuTinh += this.getDataValue(view.GetRowCellValue(e.RowHandle, "ThoigianCB").ToString());
                    tongTgDuTinh += this.getDataValue(view.GetRowCellValue(e.RowHandle, "TG_DUTINH").ToString());
                    view.SetRowCellValue(e.RowHandle, "TONGTG_DUTINH", Convert.ToDouble(tongTgDuTinh).ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }
   
        private void BtnList_CongDoan_Click(object sender, EventArgs e)//Danh sach đơn hàng chuyển qua tính lịch sản xuất
        {
            ketnoi kn = new ketnoi();
            DataTable dt = new DataTable();
            dt = kn.laybang("select Nguoi,Ngay,TyleNangsuat,Ma_NguonLuc,id,Madh,PR.Masp,SP.Tensp,Ma_CongDoan,CongDoan,Thoigian_Dinhmuc,Soluong_DonHang, "
                                                +"TG_ChuanBi,TG_DuTru,CASE BatDau WHEN '1900-01-01' THEN Null ELSE BatDau END AS BatDau, "
                                                + "CASE KetThuc WHEN '1900-01-01' THEN Null ELSE KetThuc END AS KetThuc,WorkDay,TG_DUTINH, "
                                                + "TONGTG_DUTINH,TONGTG_DUTINHTYLE,idDonHang,PR.Ma_BoPhan,PB.To_bophan,ID_DMLD "
                                                +"from tblCalender_Product PR left outer join tblPHONGBAN PB on PR.Ma_BoPhan=PB.Ma_bophan "
                                                +"left outer join tblSANPHAM SP on SP.Masp = PR.Masp");
            gridControl2.DataSource = dt;
            kn.dongketnoi(); gridView2.ExpandAllGroups();
        }

        private void DS(){
            ketnoi kn = new ketnoi();
            DataTable dt = new DataTable();
            dt = kn.laybang("select Nguoi,Ngay,TyleNangsuat,Ma_NguonLuc,id,Madh,PR.Masp,SP.Tensp,Ma_CongDoan,CongDoan,Thoigian_Dinhmuc,Soluong_DonHang, "
                                                + "TG_ChuanBi,TG_DuTru,CASE BatDau WHEN '1900-01-01' THEN Null ELSE BatDau END AS BatDau, "
                                                + "CASE KetThuc WHEN '1900-01-01' THEN Null ELSE KetThuc END AS KetThuc,WorkDay,TG_DUTINH, "
                                                + "TONGTG_DUTINH,TONGTG_DUTINHTYLE,idDonHang,PR.Ma_BoPhan,PB.To_bophan,ID_DMLD "
                                                + "from tblCalender_Product PR left outer join tblPHONGBAN PB on PR.Ma_BoPhan=PB.Ma_bophan "
                                                + "left outer join tblSANPHAM SP on SP.Masp = PR.Masp where Madh like N'" + txtMaDH.Text + "'");
            gridControl2.DataSource = dt;
            kn.dongketnoi(); gridView2.ExpandAllGroups();
        }
        //Binding gridView2 Calender_Product
        private void Binding_Grid2(){
                string Gol;
                Gol = gridView2.GetFocusedDisplayText();
                txtMaDH.Text = gridView2.GetFocusedRowCellDisplayText(Madh_grid2);
                txtMaSP.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
                txtSanpham.Text = gridView2.GetFocusedRowCellDisplayText(Col2Sanpham);
                txtIdDonHang.Text = gridView2.GetFocusedRowCellDisplayText(idDonHang_grid2);
        }

        private void DSDH_TINHLICHSX_CUNGMA()//Danh sach đơn hàng chuyển tính lịch sản xuất sau khi ghi du lieu
        {
            ketnoi kn = new ketnoi();
            DataTable dt = new DataTable();
            dt = kn.laybang("select Nguoi,Ngay,TyleNangsuat,CASE WHEN Ma_NguonLuc IS NULL THEN '' ELSE  Ma_NguonLuc END Ma_NguonLuc, "
                                                +" id,Madh,PR.Masp,SP.Tensp,Ma_CongDoan,CongDoan,Thoigian_Dinhmuc,Soluong_DonHang, "
                                                +" TG_ChuanBi,TG_DuTru,CASE BatDau WHEN '1900-01-01' THEN '' ELSE BatDau END AS BatDau, "
                                                +" CASE KetThuc WHEN '1900-01-01' THEN '' ELSE KetThuc END AS KetThuc,WorkDay, "
                                                +" TG_DUTINH,TONGTG_DUTINH,TONGTG_DUTINHTYLE,idDonHang,PR.Ma_BoPhan,PB.To_bophan,ID_DMLD "
                                                +" from tblCalender_Product PR left outer join tblPHONGBAN PB on PR.Ma_BoPhan=PB.Ma_bophan "
                                                +" left outer join tblSANPHAM SP on SP.Masp = PR.Masp where idDonHang like '" + txtIdDonHang.Text + "'");
            List<tblDelivery_Dtl> lst = new List<tblDelivery_Dtl>();
            lst = Utils.ConvertDataTable<tblDelivery_Dtl>(dt);
            gridControl2.DataSource = new BindingList<tblDelivery_Dtl>(lst);
            kn.dongketnoi(); gridView2.ExpandAllGroups();
        }


        private void BtnNguonLuc_Click(object sender, EventArgs e)
        {
            frmResources Resources = new frmResources();
            Resources.ShowDialog();
        }
        
        private void Reposi_CongDoan_EditValueChanged(object sender, EventArgs e)//Su kien thay doi du lieu gridcon trol
        {
           
        }
        //Hàm tính ngày bắt đầu ngày kết thúc ()
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null) return;
                string columnName = e.Column.Name.ToString();
                if (columnName == "TyleCongSuat_grid2" || columnName == "TGChuanBi_grid2" || columnName == "TimeDuTru_grid2")
                {
                    //Tính tổng thời gian định mức sản xuất cho 1 công đoạn sản xuất (Thời gian chuẩn bị, thời gian dự trữ, thời gian thực tế sản xuất)
                    //double TONGTG=(this.getDataValue(view.GetRowCellValue(e.RowHandle, "Soluong_DonHang").ToString())) /
                    //              (this.getDataValue(view.GetRowCellValue(e.RowHandle, "Thoigian_Dinhmuc").ToString())) *
                    //              (this.getDataValue(view.GetRowCellValue(e.RowHandle, "TyleNangsuat").ToString())) +
                    //              (this.getDataValue(view.GetRowCellValue(e.RowHandle, "TG_ChuanBi").ToString())) +
                    //              (this.getDataValue(view.GetRowCellValue(e.RowHandle, "TG_DuTru").ToString()));//Hàm tính thời gian định mức  
                    //view.SetRowCellValue(e.RowHandle, "TONGTG_DUTINH", Convert.ToDouble(TONGTG));//Ngay lam viec  
                    double SLDH = (double)(gridView2.GetFocusedRowCellValue("Soluong_DonHang"));
                    double TGDM = (double)(gridView2.GetFocusedRowCellValue("Soluong_DonHang"));
                    double  TONG = (double)(gridView2.GetFocusedRowCellValue("Soluong_DonHang"));

                    //trường hợp công đoạn cụ thể có sử dụng thêm 2 nhân công trở lên thì Thời gian định mức x số lượng nhân công
                    double TG_TYLE=(this.getDataValue(view.GetRowCellValue(e.RowHandle, "Soluong_DonHang").ToString()))/
                                  (this.getDataValue(view.GetRowCellValue(e.RowHandle, "Thoigian_Dinhmuc").ToString()))/
                                  (this.getDataValue(view.GetRowCellValue(e.RowHandle, "TyleNangsuat").ToString())) +
                                  (this.getDataValue(view.GetRowCellValue(e.RowHandle, "TG_ChuanBi").ToString())) +
                                  (this.getDataValue(view.GetRowCellValue(e.RowHandle, "TG_DuTru").ToString()));//Hàm tính tỷ lệ thời gian định mức  
                    view.SetRowCellValue(e.RowHandle, "TONGTG_DUTINHTYLE", Convert.ToDouble(TG_TYLE));//Ngay lam viec 


                    double TimeHC = double.Parse(txtTimeHC.Text);//Thoi gian hanh chinh do nguoi dung chọn
                    //Console.WriteLine(view.GetRowCellValue(e.RowHandle, "BatDau").ToString());
                    DateTime BatDau = DateTime.Parse(view.GetRowCellValue(e.RowHandle, "BatDau").ToString());
                    //Console.WriteLine(BatDau.ToString());
                    DateTime TempDate = BatDau.AddDays(this.getDataValue(view.GetRowCellValue(e.RowHandle, "TONGTG_DUTINHTYLE").ToString()));//Ngày kết thúc tạm tính (NgayBatDau + Tong thoi gian du tinh)
                    //DateTime TempDate;
                    
                    //Lấy số ngày chủ nhật trong khoản starDate(BatDau)  endDate(TempDate) add Date
                    var DayChuNhat = 0;
                    for (var dateCount = BatDau; dateCount <= TempDate; dateCount = dateCount.AddDays(1))//Vòng lặp lấy số ngày chủ nhật
                    {
                        if (dateCount.DayOfWeek == DayOfWeek.Sunday)
                            DayChuNhat++;
                    }
                    DateTime KetThuc = TempDate.AddDays(DayChuNhat);
                    TimeSpan workday = (TempDate - BatDau);
                    int TongNgay = workday.Days + 1;
                    view.SetRowCellValue(e.RowHandle, "KetThuc", Convert.ToDateTime(KetThuc).ToString());//Ngày kết thúc đã trừ ngày chủ nhật
                    view.SetRowCellValue(e.RowHandle, "WorkDay", Convert.ToDouble(TongNgay));//Ngay lam viec 
                }
                if (columnName == "BatDau_grid2")//Ham tính thời gian kết thúc
                {
                   double TimeHC = double.Parse(txtTimeHC.Text);//Thoi gian hanh chinh do nguoi dung chọn
                    //Console.WriteLine(view.GetRowCellValue(e.RowHandle, "BatDau").ToString());
                   DateTime BatDau = DateTime.Parse(view.GetRowCellValue(e.RowHandle, "BatDau").ToString());
                    //Console.WriteLine(BatDau.ToString());
                   DateTime TempDate = BatDau.AddDays(this.getDataValue(view.GetRowCellValue(e.RowHandle, "TONGTG_DUTINHTYLE").ToString()) / TimeHC);//Ngày kết thúc tạm tính (NgayBatDau + Tong thoi gian du tinh)
                   
                    
                    //Lấy số ngày chủ nhật trong khoản starDate(BatDau)  endDate(TempDate) add Date
                   var DayChuNhat = 0;
                   for (var dateCount = BatDau; dateCount <= TempDate; dateCount = dateCount.AddDays(1))//Vòng lặp lấy số ngày chủ nhật
                   {
                       if (dateCount.DayOfWeek == DayOfWeek.Sunday)
                                    DayChuNhat++;
                   }
                   DateTime KetThuc = TempDate.AddDays(DayChuNhat);
                   TimeSpan workday = (TempDate - BatDau);
                   int TongNgay = workday.Days+1;         
                       view.SetRowCellValue(e.RowHandle, "KetThuc", Convert.ToDateTime(KetThuc).ToString());//Ngày kết thúc đã trừ ngày chủ nhật
                       view.SetRowCellValue(e.RowHandle, "WorkDay",Convert.ToDouble(TongNgay));//Ngay lam viec    
                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }  
        }
        

        public int GetWorkingDays(DateTime from, DateTime to)
        {
            var totalDays = 0;
            for (var date = from; date < to; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday
                    && date.DayOfWeek != DayOfWeek.Sunday)
                    totalDays++;
            }
            return totalDays;
        }

        private void BtnGhiThoiGianSXUocLuong_Click(object sender, EventArgs e)//Ghi ma nguon luc thoi gian bat dau ket thuc
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
                    string strQuery = string.Format(@"update tblCalender_Product set  Ma_NguonLuc= '{0}',BatDau='{1}',KetThuc='{2}',"
                    +"WorkDay='{3}',TyleNangsuat='{4}',TONGTG_DUTINHTYLE='{5}',Ngay=GetDate(),Nguoi='"+txtUser.Text+"' where id = {6}",
                    rowData["Ma_NguonLuc"],
                    rowData["BatDau"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["BatDau"]).ToString("yyyy-MM-dd"),
                    rowData["KetThuc"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["KetThuc"]).ToString("yyyy-MM-dd"),
                    rowData["WorkDay"],
                    rowData["TyleNangsuat"],
                    rowData["TONGTG_DUTINHTYLE"],
                    rowData["id"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DS();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }  
        }

        private void BtnXoa_Click_1(object sender, EventArgs e)//Xoa ma nguon luc
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
                    Console.WriteLine(rowData["id"]);
                    string strQuery = string.Format(@"delete from tblCalender_Product where id ='{0}'", rowData["id"]);
                    Console.WriteLine(strQuery);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DS();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        //Lap Danh Sach Theo ma dh
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            DS();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            Binding_Grid2();
        }
        //Uoc luong Thoi gian hoan thanh cong viec cac bo phan
        private void IdDonHong_ValueChange(object sender,EventArgs e){
            Cat(); Dap(); Tien(); Bulon(); Han(); STD(); DongGoi(); DuTinhTongHop();
        }
        private void DuTinhTongHop(){
            ketnoi kn = new ketnoi();
            gridControl10.DataSource = kn.laybang("select IdDonHang,Masp,Madh,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>''  group by IdDonHang,Masp,Madh having IdDonHang ='" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }
        private void Cat(){
            ketnoi kn = new ketnoi();
            gridControl12.DataSource = kn.laybang("select IdDonHang,Masp,Madh,Ma_NguonLuc,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>'' group by IdDonHang,Masp,Madh,Ma_NguonLuc HAVING Ma_NguonLuc='C' and IdDonHang like '" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }
        private void Dap(){
            ketnoi kn = new ketnoi();
            gridControl11.DataSource = kn.laybang("select IdDonHang,Masp,Madh,Ma_NguonLuc,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>'' and idDonHang='" + txtIdDonHang.Text + "' group by IdDonHang,Masp,Madh,Ma_NguonLuc HAVING Ma_NguonLuc='D' and IdDonHang like '" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }
        private void Tien(){
            ketnoi kn = new ketnoi();
            gridControl13.DataSource = kn.laybang("select IdDonHang,Masp,Madh,Ma_NguonLuc,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>'' and idDonHang='" + txtIdDonHang.Text + "' group by IdDonHang,Masp,Madh,Ma_NguonLuc HAVING Ma_NguonLuc='T' and IdDonHang like '" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }
        
        private void Bulon()
        {
            ketnoi kn = new ketnoi();
            gridControl14.DataSource = kn.laybang("select IdDonHang,Masp,Madh,Ma_NguonLuc,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>'' and idDonHang='" + txtIdDonHang.Text + "' group by IdDonHang,Masp,Madh,Ma_NguonLuc HAVING Ma_NguonLuc='B' and IdDonHang like '" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }
        private void Han()
        {
            ketnoi kn = new ketnoi();
            gridControl15.DataSource = kn.laybang("select IdDonHang,Masp,Madh,Ma_NguonLuc,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>'' and idDonHang='" + txtIdDonHang.Text + "' group by IdDonHang,Masp,Madh,Ma_NguonLuc HAVING Ma_NguonLuc='H' and IdDonHang like '" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }
        private void STD()
        {
            ketnoi kn = new ketnoi();
            gridControl16.DataSource = kn.laybang("select IdDonHang,Masp,Madh,Ma_NguonLuc,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>'' and idDonHang='" + txtIdDonHang.Text + "' group by IdDonHang,Masp,Madh,Ma_NguonLuc HAVING Ma_NguonLuc='STD' and IdDonHang like '" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }
        private void DongGoi()
        {
            ketnoi kn = new ketnoi();
            gridControl17.DataSource = kn.laybang("select IdDonHang,Masp,Madh,Ma_NguonLuc,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>'' and idDonHang='" + txtIdDonHang.Text + "' group by IdDonHang,Masp,Madh,Ma_NguonLuc HAVING Ma_NguonLuc='DG'and IdDonHang like '" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }
        private void Han_Mai()
        {
            ketnoi kn = new ketnoi();
            gridControl10.DataSource = kn.laybang("select IdDonHang,Masp,Madh,Ma_NguonLuc,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>'' and idDonHang='" + txtIdDonHang.Text + "' group by IdDonHang,Masp,Madh,Ma_NguonLuc HAVING Ma_NguonLuc='M' and IdDonHang like '" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }
        private void Han_Sua()
        {
            ketnoi kn = new ketnoi();
            gridControl10.DataSource = kn.laybang("select IdDonHang,Masp,Madh,Ma_NguonLuc,SUM(TONGTG_DUTINH) TONGTG_DUTINH,MIN(BatDau) BatDau,MAX(KetThuc) KetThuc "
            + " from tblCalender_Product where BatDau<>'' or KetThuc<>'' and idDonHang='" + txtIdDonHang.Text + "' group by IdDonHang,Masp,Madh,Ma_NguonLuc HAVING Ma_NguonLuc='S' and IdDonHang like '" + txtIdDonHang.Text + "'");
            kn.dongketnoi();
        }

        #region gọi nguồn lực sản xuất
        private void BtnSupport_Resource_Click(object sender, EventArgs e)
        {
            NguonLuc_Cat(); NguonLuc_Dap(); NguonLuc_Tien();
            NguonLuc_Bulon(); NguonLuc_Han(); NguonLuc_Mai();
            NguonLuc_Sua(); NguonLuc_STD(); NguonLuc_DongGoi();
            //Console.Write(txtGioCong_Han.Text);
        }
       
        private void NguonLuc_Cat()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("select * from GetResourceHan ('"+txtGioCong_Cat.Text+"', "
            +"'" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "', "
            +"'" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "','"+txtMa_NLCat.Text+"') "
            +" ORDER BY Date ASC");
            kn.dongketnoi();
        }
        private void NguonLuc_Dap()
        {
            ketnoi kn = new ketnoi();
            gridControl6.DataSource = kn.laybang("select * from GetResourceHan ('"+txtGioCong_Dap.Text+"', "
            +"'" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "', "
            +"'" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "','"+txtMa_NLDap.Text+"') "
            +" ORDER BY Date ASC");
            kn.dongketnoi();
        }
        private void NguonLuc_Tien()
        {
            ketnoi kn = new ketnoi();
            gridControl7.DataSource = kn.laybang("select * from GetResourceHan ('"+txtGioCong_Tien.Text+"', "
            +"'" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "', "
            +"'" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "','"+txtMa_NLTien.Text+"') "
            +" ORDER BY Date ASC");
            kn.dongketnoi();
        }
        private void NguonLuc_Bulon()
        {
            ketnoi kn = new ketnoi();
            gridControl8.DataSource = kn.laybang("select * from GetResourceHan ('"+txtGioCong_Bulon.Text+"', "
            +"'" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "', "
            +"'" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "','"+txtMa_NLBulon.Text+"') "
            +" ORDER BY Date ASC");
            kn.dongketnoi();
        }
        private void NguonLuc_Han()
        {
            ketnoi kn = new ketnoi();
            gridControl9.DataSource = kn.laybang("select * from GetResourceHan ('"+txtGioCong_Han.Text+"', "
            +"'" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "', "
            +"'" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "','"+txtMa_NLHan.Text+"') "
            +" ORDER BY Date ASC");
            kn.dongketnoi();
        }
        private void NguonLuc_Mai()
        {
            ketnoi kn = new ketnoi();
            gridControl18.DataSource = kn.laybang("select * from GetResourceHan ('"+txtGioCong_Mai.Text+"', "
            +"'" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "', "
            +"'" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "','"+txtMa_NLMai.Text+"') "
            +" ORDER BY Date ASC");
            kn.dongketnoi();
        }
        private void NguonLuc_Sua()
        {
            ketnoi kn = new ketnoi();
            gridControl19.DataSource = kn.laybang("select * from GetResourceHan ('"+txtGioCong_Sua.Text+"', "
            +"'" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "', "
            +"'" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "','"+txtMa_NLSua.Text+"') "
            +" ORDER BY Date ASC");
            kn.dongketnoi();
        }
        private void NguonLuc_STD()
        {
            ketnoi kn = new ketnoi();
            gridControl20.DataSource = kn.laybang("select * from GetResourceHan ('"+txtGioCong_STD.Text+"', "
            +"'" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "', "
            +"'" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "','"+txtMa_NLSon.Text+"') "
            +" ORDER BY Date ASC");
            kn.dongketnoi();
        }
       
        private void NguonLuc_DongGoi(){
            ketnoi kn = new ketnoi();
            gridControl21.DataSource = kn.laybang("select * from GetResourceHan ('"+txtGioCong_DongGoi.Text+"', "
            +"'" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "', "
            + "'" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "','DG')"
            +" ORDER BY Date ASC");
            kn.dongketnoi();
        }
        #endregion
        private void BtnMasanpham_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMaSP.Text, txtPath_MaSP.Text);
            f2.Show();
        }
    }
}