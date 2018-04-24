using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using quanlysanxuat.Model;
using DevExpress.Office.Utils;
using System.Collections;
using System.Linq;
using DevExpress.Charts.Native;
using DevExpress.Web.ASPxHtmlEditor.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using quanlysanxuat.Report;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Formatting = Newtonsoft.Json.Formatting;
using DevExpress.XtraRichEdit.Layout;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.UnitConversion;
using DevExpress.Xpf.Editors.Helpers;

namespace quanlysanxuat.View.UcControl
{
    public partial class UserControlBienBan : DevExpress.XtraEditors.XtraForm
    {
        public UserControlBienBan()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        #region formload
        private void UserControlBienBan_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            LayMaBienBan();
            LoaiBienBan();
            ThNguoiLapBienBan();
            ThNguoiLamChung();
            ThPhanBoThietHai();
            TkThietHai();//Thong ke thiet hai
            ThBienBan();
            gvNoiDungBienBan.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvSoBienBan.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvNguoiLapBienBan.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvNguoiLamChung.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvThongKeThietHai.Appearance.Row.Font = new Font("Segoe UI", 8f);

            gvNoiDungBienBan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gvNguoiLapBienBan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gvNguoiLamChung.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gvThongKeThietHai.OptionsSelection.CheckBoxSelectorColumnWidth = 20;

            ThBienBanPhanBoThietHaiTheoNgay();
            TheHienDSNhanVien();
        }
        #endregion

        #region The hien danh sach nhan vien
        public DataTable nhanvientable;
        private void TheHienDSNhanVien()
        {
            Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select UserFullCode,UserCardNo,
                UserFullName,UserIDD,b.Description,'Nhân viên'ChucVu
                from [datachamcong moi].[dbo].UserInfo a
                inner join 
                (select * from [datachamcong moi].[dbo].RelationDept 
                where Description <>N'danh sach CNV đã nghỉ việc') b
                on a.UserIDD=b.ID");
            var dt = Function.GetDataTable(sqlQuery);
            nhanvientable = dt;
            repositoryItemGridLookUpEditLap.DataSource = dt;
            repositoryItemGridLookUpEditLap.DisplayMember = "UserFullName";
            repositoryItemGridLookUpEditLap.ValueMember = "UserFullName";

            repositoryItemGridLookUpEditLamChung.DataSource = dt;
            repositoryItemGridLookUpEditLamChung.DisplayMember = "UserFullName";
            repositoryItemGridLookUpEditLamChung.ValueMember = "UserFullName";

            repositoryItemGridLookUpEditLienQuan.DataSource = dt;
            repositoryItemGridLookUpEditLienQuan.DisplayMember = "UserFullName";
            repositoryItemGridLookUpEditLienQuan.ValueMember = "UserFullName";
            Function.Disconnect();
        }
        #endregion

        private void LayMaBienBan()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select Top 1
                        REPLACE(convert(nvarchar, GetDate(), 11),'/','')+
						replace(replace(left(CONVERT(time, GetDate()),12),':',''),'.','')");
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaBienBan.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void LoaiBienBan()
        {
            cbLoaiBienBan.Items.Add("Biên bản vụ việc");
            cbLoaiBienBan.Items.Add("Biên bản vi phạm");
            cbLoaiBienBan.SelectedIndex = 0;
        }
        private void btnTaoMa_Click(object sender, EventArgs e)
        {
            LayMaBienBan();
        }
        private void TkThietHai()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select ID,MaBienBan,ChiTietLo,NguyenNhan,
                DienGiai,GiaTriThietHai,
                TyLeCongTyChiaSe,TyLeCaNhanChiaSe,
                THCongTy,THCaNhan
                from BienBanThongKe");
            grThongKeThietHai.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();//dong ket noi
        }
        private void ThNguoiLapBienBan()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select N'Lý Thành Phương'OngBa,
                N'Giám Sát'ChucVu,N'Nhân Sự'BoPhan");
            grNguoiLapBienBan.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();//dong ket noi
        }

        private void ThNguoiLamChung()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select N'Lý Thành Phương'OngBa,
                N'Giám Sát'ChucVu,N'Nhân Sự'BoPhan");
            grNguoiLamChung.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();//dong ket noi
        }
        //The hien nguoi lien quan
        private void ThPhanBoThietHai()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select Top 0 * from BienBanPhanBoTH");
            grNoiDungBienBan.DataSource = Function.GetDataTable(sqlQuery);
            //Function.Disconnect();//dong ket noi
        }
        //The hien thong ke thiet hai
        private void ThThongKeThietHai()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select ID,MaBienBan,ChiTietLo,NguyenNhan,
                DienGiai,GiaTriThietHai,
                TyLeCongTyChiaSe,TyLeCaNhanChiaSe,THCongTy,THCaNhan
                from BienBanThongKe where 
                MaBienBan like '{0}'", txtMaBienBan.Text);
            grThongKeThietHai.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();//dong ket noi
        }
        //Convert table to array
        string jsonnguoilap;    
        private void ConvertNguoiLapBienBanArray()
        {
            Function.ConnectSanXuat();//Mo ket noi
            try
            {
                DataRow rowData;
                int[] listRowList = this.gvNguoiLapBienBan.GetSelectedRows();
                //string[] name = new string[];
                ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNguoiLapBienBan.GetDataRow(listRowList[i]);
                    myarrayList.Add("Ông bà:" + rowData["OngBa"]);
                    myarrayList.Add("Chức vụ:" + rowData["ChucVu"]);
                    myarrayList.Add("Bộ phận:" + rowData["BoPhan"] + ";");
                }
                this.jsonnguoilap = JsonConvert.SerializeObject(myarrayList, Formatting.Indented);
                //MessageBox.Show("Error" + json, "Warning");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
            Function.Disconnect();//dong ket noi

            //this.jsonnguoilap = ConvertDataToJson(dtnguoilap());
        }
        //private DataTable dtnguoilap()
        //{
        //    Function.ConnectSanXuat();//Mo ket noi
        //    DataRow rowData;
        //    int[] listRowList = this.gvNguoiLapBienBan.GetSelectedRows();
        //    DataTable dt = new DataTable();
        //    for (int i = 0; i < listRowList.Length; i++)
        //    {
        //        rowData = this.gvNguoiLapBienBan.GetDataRow(listRowList[i]);
        //        dt.Columns.Add("Ông bà:" + rowData["OngBa"]);
        //        dt.Columns.Add("Chức vụ:" + rowData["ChucVu"]);
        //        dt.Columns.Add("Bộ phận:" + rowData["BoPhan"] + ";");
        //    }
        //    return dt;
        //} 
        //private string ConvertDataToJson(DataTable table)
        //{
        //    string jsonString = string.Empty;
        //    jsonString = JsonConvert.SerializeObject(table);
        //    return jsonString;
        //}

        string jsonnguoilamchung;
        private void nguoilamchung()
        {
            Function.ConnectSanXuat();//Mo ket noi
            try
            {
                DataRow rowData;
                int[] listRowList = this.gvNguoiLamChung.GetSelectedRows();
                ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNguoiLamChung.GetDataRow(listRowList[i]);
                    myarrayList.Add("Ông bà:" + rowData["OngBa"]);
                    myarrayList.Add("Chức vụ:" + rowData["ChucVu"]);
                    myarrayList.Add("Bộ phận:" + rowData["BoPhan"] + ";");
                }
                this.jsonnguoilamchung = JsonConvert.SerializeObject(myarrayList, Formatting.Indented);
                //MessageBox.Show("Error" + json, "Warning");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
            Function.Disconnect();//dong ket noi
        }
        string jsonnguoilienquan;
        private void nguoilienquan()
        {
            Function.ConnectSanXuat();//Mo ket noi
            try
            {
                DataRow rowData;
                int[] listRowList = this.gvNoiDungBienBan.GetSelectedRows();
                ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiDungBienBan.GetDataRow(listRowList[i]);
                    myarrayList.Add("Ông bà:" + rowData["OngBa"] + ";");
                }
                this.jsonnguoilienquan = JsonConvert.SerializeObject(myarrayList, Formatting.Indented);
                //MessageBox.Show("Error" + json, "Warning");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
            Function.Disconnect();//dong ket noi
        }
        private void GhiBienBan()
        {
            Function.ConnectSanXuat();
            try
            {
                string strQuery = string.Format(@"insert into BienBan (MaBienBan,NgayLap,NoiLap,
                        NguoiLapBienBan,NguoiChungKien,NguoiLienQuanVuViec,
                        NoiDung,SoBan,
                        ThoiGianKetThuc,
                        NguoiLap,ThietHaiVatChat,LoaiBienBan,NgayGhi) values 
                      (N'{0}','{1}',N'{2}',
                        N'{3}',N'{4}',N'{5}',
                        N'{6}','{7}',
                        '{8}',N'{9}','{10}',N'{11}',GetDate())",
                  txtMaBienBan.Text,
                  dpNgayGioLap.Value.ToString("MM-dd-yyyy HH:mm"),
                  richTextBoxNoiLap.Text, this.jsonnguoilap, this.jsonnguoilamchung,
                  this.jsonnguoilienquan, richTextBoxNoiDung.Text, txtSoBan.Text,
                  dpNgayGioKetThuc.Value.ToString("MM-dd-yyyy HH:mm"),
                  Login.Username,
                  txtTongThietHaiVatChatBanDau.Text, cbLoaiBienBan.Text);
                var dt = Function.GetDataTable(strQuery);
                ThBienBan();//The hien bien ban
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }

        
        //Ghi nguoi lap bien ban vao BienBanNguoiLap
        private void GhiNguoiLapBienBan()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvNguoiLapBienBan.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNguoiLapBienBan.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BienBanNguoiLap 
                        (MaBienBan,OngBa,ChucVu,BoPhan)
                        values
                        (N'{0}',N'{1}',N'{2}',N'{3}')",
                        txtMaBienBan.Text, rowData["OngBa"],
                        rowData["ChucVu"], rowData["BoPhan"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                ThNguoiLapBienBanTheoBB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        //Ghi nguoi lap bien ban vao BienBanNguoiChungKien
        private void GhiNguoiChungKien()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvNguoiLamChung.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNguoiLamChung.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BienBanNguoiChungKien 
                        (MaBienBan,OngBa,ChucVu,BoPhan)
                        values
                        (N'{0}',N'{1}',N'{2}',N'{3}')",
                        txtMaBienBan.Text, rowData["OngBa"],
                        rowData["ChucVu"], rowData["BoPhan"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                ThNguoiLamChung();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        //Ghi phân bổ thiệt hại vật chất
        private void GhiBienBanPhanBoTH()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvNoiDungBienBan.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiDungBienBan.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BienBanPhanBoTH 
                       (MaBienBan,OngBa,
                        NoiDung,TyLeCaNhan,
                        THCaNhan,TongTH,
                        UserFullCode,ChucVu,Description)
                        values
                      (N'{0}',N'{1}',
                       N'{2}','{3}',
                        '{4}','{5}',
                        '{6}','{7}','{8}')",
                      txtMaBienBan.Text, 
                      rowData["OngBa"],
                      rowData["NoiDung"],
                      rowData["TyLeCaNhan"],
                      rowData["THCaNhan"],
                      txtTongThietHaiVatChatBanDau.Text,
                      rowData["UserFullCode"],
                      rowData["ChucVu"], 
                      rowData["Description"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                //this.jsonnguoilienquan = JsonConvert.SerializeObject(myarrayList, Formatting.Indented);//chuyen datatable nguoi lien quan ve json de luu vao sổ BienBan
                ThBienBanPhanBoTH();//The hien phan bo thiet hai
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        private void GhiThongKeThietHai()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvThongKeThietHai.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThongKeThietHai.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BienBanThongKe 
                        (MaBienBan,ChiTietLo,NguyenNhan,
                        DienGiai,GiaTriThietHai,
                        TyLeCongTyChiaSe,TyLeCaNhanChiaSe,THCongTy,THCaNhan)
                        values
                         ('{0}',N'{1}',N'{2}',
                         N'{3}','{4}',
                          '{5}','{6}','{7}','{8}')",
                    txtMaBienBan.Text,rowData["ChiTietLo"],rowData["NguyenNhan"], 
                    rowData["DienGiai"], rowData["GiaTriThietHai"],
                    rowData["TyLeCongTyChiaSe"], rowData["TyLeCaNhanChiaSe"],
                    rowData["THCongTy"],rowData["THCaNhan"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                //this.jsonnguoilienquan = JsonConvert.SerializeObject(myarrayList, Formatting.Indented);//chuyen datatable nguoi lien quan ve json de luu vao sổ BienBan
                ThThongKeThietHai();//The hien phan bo thiet hai
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        private void SuaThongKeThietHai()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvThongKeThietHai.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThongKeThietHai.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BienBanThongKe 
                        set ChiTietLo = N'{0}',NguyenNhan=N'{1}',
                        DienGiai = N'{2}',GiaTriThietHai = N'{3}',
                        TyLeCongTyChiaSe='{4}',TyLeCaNhanChiaSe='{5}',
                        THCongTy = '{6}',THCaNhan = '{7}' where ID like '{8}'",
                      rowData["ChiTietLo"],
                      rowData["NguyenNhan"],
                      rowData["DienGiai"], 
                      rowData["GiaTriThietHai"],
                      rowData["TyLeCongTyChiaSe"],
                      rowData["TyLeCaNhanChiaSe"],
                      rowData["THCongTy"], 
                      rowData["THCaNhan"],
                      rowData["ID"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                //this.jsonnguoilienquan = JsonConvert.SerializeObject(myarrayList, Formatting.Indented);//chuyen datatable nguoi lien quan ve json de luu vao sổ BienBan
                ThThongKeThietHai();//The hien phan bo thiet hai
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        private void XoaThongKeThietHai()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvThongKeThietHai.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThongKeThietHai.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from BienBanThongKe where ID like '{0}'",rowData["ID"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                //this.jsonnguoilienquan = JsonConvert.SerializeObject(myarrayList, Formatting.Indented);//chuyen datatable nguoi lien quan ve json de luu vao sổ BienBan
                ThThongKeThietHai();//The hien phan bo thiet hai
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        private void btnGhi_Click(object sender, EventArgs e)
        {
            //ConvertNguoiLapBienBanArray();//Nguoi lap bien ban
            //nguoilamchung();//Nguoi lam chung
            //nguoilienquan();//Nguoi lien quan
            GhiBienBan();//Ghi bien ban
            GhiNguoiLapBienBan();//Ghi nguoi lap bien ban
            GhiNguoiChungKien();//Ghi nguoi chung kien
            GhiThongKeThietHai();//Ghi thong ke thiet hai
            GhiBienBanPhanBoTH();//Ghi phan bo thiet hai
        }
        //The hien bien ban phan bo thiet hai
        private void ThBienBanPhanBoTH()
        {
            Function.ConnectSanXuat();
            string strQuery = string.Format(@"select isnull(THCongTy,0)+isnull(THCaNhan,0)TongTH,* from BienBanPhanBoTH where MaBienBan like N'{0}'", txtMaBienBan.Text);
            grNoiDungBienBan.DataSource = Function.GetDataTable(strQuery);
        }
        private void ThBienBan()
        {
            Function.ConnectSanXuat();
            string strQuery = string.Format(@"select ID,a.MaBienBan,LoaiBienBan,NgayLap,
						NoiLap,NoiDung,ThietHaiVatChat,SoBan,
						ThoiGianKetThuc,NguoiLap,NgayGhi,NgaySua,
						b.NguoiLapBB,c.NguoiChungKien,d.NguoiLienQuan,
						e.THCongTy,e.THCaNhan,e.NoiDungThongKe,
						(e.THCongTy+e.THCaNhan)THTong
                        from BienBan a
                        left outer join
                        (select * from vwBienBanNguoiLap)b
                        on a.MaBienBan=b.MaBienBan
                        left outer join
                        (select * from vwBienBanNguoiChungKien)c
                        on a.MaBienBan=c.MaBienBan
						left outer join
						(select * from vwBienBanNguoiLienQuan)d
						on a.MaBienBan=d.MaBienBan
						left outer join
						(select * from vwBienBanThongKeThietHai)e
						on a.MaBienBan=e.MaBienBan
                        where cast(NgayLap as Date)
                        between '{0}' and '{1}' 
                        order by MaBienBan desc",
                dpTu.Value.ToString("MM-dd-yyyy"),
                dpDen.Value.ToString("MM-dd-yyyy"));
            grSoBienBan.DataSource = Function.GetDataTable(strQuery);
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            SuaBienBan();
            SuaNguoiLapBienBan();
            SuaNguoiChungKien();
            SuaBienBanPhanBoTH();
            SuaThongKeThietHai();
        }
        string idbienban;
        private void SuaBienBan()
        {
            Function.ConnectSanXuat();
            try
            {
                string strQuery = string.Format(@"update BienBan set MaBienBan=N'{0}',NgayLap='{1}',NoiLap=N'{2}',
                        NguoiLapBienBan=N'{3}',NguoiChungKien=N'{4}',NguoiLienQuanVuViec=N'{5}',
                        NoiDung=N'{6}',SoBan='{7}',
                        ThoiGianKetThuc='{8}',
                        NguoiLap=N'{9}',ThietHaiVatChat='{10}',LoaiBienBan=N'{11}',NgayGhi=GetDate() where ID like '{12}'",
                  txtMaBienBan.Text,
                  dpNgayGioLap.Value.ToString("MM-dd-yyyy HH:mm"),
                  richTextBoxNoiLap.Text, this.jsonnguoilap, this.jsonnguoilamchung,
                  this.jsonnguoilienquan, richTextBoxNoiDung.Text, txtSoBan.Text,
                  dpNgayGioKetThuc.Value.ToString("MM-dd-yyyy HH:mm"),
                  Login.Username,
                  txtTongThietHaiVatChatBanDau.Text, cbLoaiBienBan.Text,this.idbienban);
                var dt = Function.GetDataTable(strQuery);
                ThBienBan();//The hien bien ban
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }


        //sua nguoi lap bien ban vao BienBanNguoiLap
        private void SuaNguoiLapBienBan()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvNguoiLapBienBan.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNguoiLapBienBan.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BienBanNguoiLap 
                        set MaBienBan=N'{0}',OngBa=N'{1}',ChucVu=N'{2}',BoPhan=N'{3}' where ID like '{4}'",
                        txtMaBienBan.Text, rowData["OngBa"],
                        rowData["ChucVu"], rowData["BoPhan"],rowData["ID"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        //sua nguoi lap bien ban vao BienBanNguoiChungKien
        private void SuaNguoiChungKien()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvNguoiLamChung.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNguoiLamChung.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BienBanNguoiChungKien 
                        set MaBienBan = N'{0}',OngBa = N'{1}',ChucVu = N'{2}',BoPhan=N'{3}' where ID like '{4}'",
                        txtMaBienBan.Text, rowData["OngBa"],
                        rowData["ChucVu"], rowData["BoPhan"],rowData["ID"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        //sua phân bổ thiệt hại vật chất
        private void SuaBienBanPhanBoTH()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvNoiDungBienBan.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiDungBienBan.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BienBanPhanBoTH 
                      set MaBienBan=N'{0}',OngBa=N'{1}',NoiDung=N'{2}',
                        TyLeCaNhan='{3}',
                        THCaNhan='{4}' where ID like '{5}'",
                      txtMaBienBan.Text,
                      rowData["OngBa"],
                      rowData["NoiDung"],
                      rowData["TyLeCaNhan"],
                      rowData["THCaNhan"],
                      rowData["ID"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                //this.jsonnguoilienquan = JsonConvert.SerializeObject(myarrayList, Formatting.Indented);//chuyen datatable nguoi lien quan ve json de luu vao sổ BienBan
                ThBienBanPhanBoTH();//The hien phan bo thiet hai
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }


        //tra cuu  so bien ban theo thoi gian
        private void btnTraCuuSoBienBan_Click(object sender, EventArgs e)
        {
            ThBienBanPhanBoThietHaiTheoNgay();
        }
        //The hien so bien ban theo thoi gian
        private void ThBienBanPhanBoThietHaiTheoNgay()
        {
            Function.ConnectSanXuat();
            string strQuery = string.Format(@"select ID,a.MaBienBan,LoaiBienBan,NgayLap,
						NoiLap,NoiDung,ThietHaiVatChat,SoBan,
						ThoiGianKetThuc,NguoiLap,NgayGhi,NgaySua,
						b.NguoiLapBB,c.NguoiChungKien,d.NguoiLienQuan,
						e.THCongTy,e.THCaNhan,e.NoiDungThongKe,
						(e.THCongTy+e.THCaNhan)THTong
                        from BienBan a
                        left outer join
                        (select * from vwBienBanNguoiLap)b
                        on a.MaBienBan=b.MaBienBan
                        left outer join
                        (select * from vwBienBanNguoiChungKien)c
                        on a.MaBienBan=c.MaBienBan
						left outer join
						(select * from vwBienBanNguoiLienQuan)d
						on a.MaBienBan=d.MaBienBan
						left outer join
						(select * from vwBienBanThongKeThietHai)e
						on a.MaBienBan=e.MaBienBan
                        where cast(NgayLap as Date)
                        between '{0}' and '{1}' 
                        order by MaBienBan desc",
                dpTu.Value.ToString("MM-dd-yyyy"),
                dpDen.Value.ToString("MM-dd-yyyy"));
            grSoBienBan.DataSource = Function.GetDataTable(strQuery);
        }


        private void gvNguoiLapBienBan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var value = gvNguoiLapBienBan.GetRowCellValue(e.RowHandle, e.Column);
            if (e.Column.FieldName == "OngBa")
            {
                List<NguoiLamChungViewModels> nv = nhanvientable.AsEnumerable().Where(x => x.Field<string>("UserFullName") == (string)value).
                     Select(m => new NguoiLamChungViewModels()
                     {
                         UserFullCode = m.Field<string>("UserFullCode"),
                         UserFullName = m.Field<string>("UserFullName"),
                         Description = m.Field<string>("Description"),
                         ChucVu = m.Field<string>("ChucVu")
                     }).ToList();
                foreach (var item in nv)
                {
                    if (nv != null)
                    {
                        gvNguoiLapBienBan.SetRowCellValue(e.RowHandle, "BoPhan", item.Description);
                        gvNguoiLapBienBan.SetRowCellValue(e.RowHandle, "ChucVu", item.ChucVu);
                    }
                }
            }
        }

        private void gvNguoiLamChung_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var value = gvNguoiLamChung.GetRowCellValue(e.RowHandle, e.Column);

            if (e.Column.FieldName == "OngBa")
            {
                List<NguoiLamChungViewModels> nv = nhanvientable.AsEnumerable().Where(x => x.Field<string>("UserFullName") == (string)value).
                     Select(m => new NguoiLamChungViewModels()
                     {
                         UserFullCode = m.Field<string>("UserFullCode"),
                         UserFullName = m.Field<string>("UserFullName"),
                         Description = m.Field<string>("Description"),
                         ChucVu = m.Field<string>("ChucVu")
                     }).ToList();
                foreach (var item in nv)
                {
                    if (nv != null)
                    {
                        gvNguoiLamChung.SetRowCellValue(e.RowHandle, "BoPhan", item.Description);
                        gvNguoiLamChung.SetRowCellValue(e.RowHandle, "ChucVu", item.ChucVu);
                    }
                }
            }
        }

        private void gvNoiDungBienBan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var value = gvNoiDungBienBan.GetRowCellValue(e.RowHandle, e.Column);
            if (e.Column.FieldName == "OngBa")
            {
                List<NguoiLamChungViewModels> nv = nhanvientable.AsEnumerable().Where(x => x.Field<string>("UserFullName") == (string)value).
                     Select(m => new NguoiLamChungViewModels()
                     {
                         UserFullCode = m.Field<string>("UserFullCode"),
                         UserFullName = m.Field<string>("UserFullName"),
                         Description = m.Field<string>("Description"),
                         ChucVu = m.Field<string>("ChucVu")
                     }).ToList();
                foreach (var item in nv)
                {
                    if (nv != null)
                    {
                        gvNoiDungBienBan.SetRowCellValue(e.RowHandle, "Description", item.Description);
                        gvNoiDungBienBan.SetRowCellValue(e.RowHandle, "UserFullCode", item.UserFullCode);
                        gvNoiDungBienBan.SetRowCellValue(e.RowHandle, "ChucVu", item.ChucVu);
                    }
                }
            }
            //tinh thanh tien
            if (e.Column == tylecn_nd)
            {
                double  tylecanhan, thcanhan;
                tylecanhan = gvNoiDungBienBan.GetFocusedRowCellValue(tylecn_nd) == DBNull.Value ? 0 : Convert.ToDouble(gvNoiDungBienBan.GetFocusedRowCellValue(tylecn_nd));
                thcanhan = (txtTongThietHaiConLai.Text == null ? 0 : double.Parse(txtTongThietHaiConLai.Text))*(tylecanhan/100);
                gvNoiDungBienBan.SetFocusedRowCellValue(thaicn_nd, thcanhan);
            }
        }
        private void txtTongThietHaiVatChat_TextChanged(object sender, EventArgs e)
        {
            SoTienConLai();
        }
        private void btnXuatBienBan_Click(object sender, EventArgs e)
        {
            if (cbLoaiBienBan.Text == "Biên bản vi phạm") { RPBienBanViPham(); }
            else if (cbLoaiBienBan.Text == "Biên bản vụ việc") { RPBienBanVuViec(); }
            else
            {
                MessageBox.Show("Chọn mẫu báo cáo chưa phù hợp", "Thông báo");
            }
        }
        private void RPBienBanViPham()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select * from vwBienBan where MaBienBan like '{0}' 
                and LoaiBienBan Like N'Biên bản vi phạm'", txtMaBienBan.Text);
            ReportBBViPham vipham = new ReportBBViPham();
            vipham.DataSource = Function.GetDataTable(sqlQuery);
            vipham.DataMember = "Table";
            vipham.CreateDocument(false);
            vipham.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaBienBan.Text;
            PrintTool tool = new PrintTool(vipham.PrintingSystem);
            vipham.ShowPreviewDialog();
            Function.Disconnect();
        }
        private void RPBienBanVuViec()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select * from vwBienBan where MaBienBan like '{0}' 
                and LoaiBienBan Like N'Biên bản vụ việc'", txtMaBienBan.Text);
            ReportBBVuViec vuviec = new ReportBBVuViec();
            vuviec.DataSource = Function.GetDataTable(sqlQuery);
            vuviec.DataMember = "Table";
            vuviec.CreateDocument(false);
            vuviec.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaBienBan.Text;
            PrintTool tool = new PrintTool(vuviec.PrintingSystem);
            vuviec.ShowPreviewDialog();
            Function.Disconnect();
        }

        string strnguoilap;
        string strnguoilamchung;
        string strnguoilienquan;
        public object MessagerBox { get; private set; }

        private void grSoBienBan_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gvSoBienBan.GetFocusedDisplayText();
            txtMaBienBan.Text = gvSoBienBan.GetFocusedRowCellDisplayText(mabienbangridColumn);
            cbLoaiBienBan.Text = gvSoBienBan.GetFocusedRowCellDisplayText(loaiBBgridColumn);
            dpNgayGioLap.Text = gvSoBienBan.GetFocusedRowCellDisplayText(ngaylapgridColumn);
            strnguoilap = gvSoBienBan.GetFocusedRowCellDisplayText(nguoilapgridColumn);
            strnguoilamchung = gvSoBienBan.GetFocusedRowCellDisplayText(nguoilamchunggridColumn);
            strnguoilienquan = gvSoBienBan.GetFocusedRowCellDisplayText(nguoilienquangridColumn);
            richTextBoxNoiLap.Text= gvSoBienBan.GetFocusedRowCellDisplayText(noilapgridColumn);
            dpNgayGioLap.Text = gvSoBienBan.GetFocusedRowCellDisplayText(ngaylapgridColumn);
            richTextBoxYKienKhac.Text =gvSoBienBan.GetFocusedRowCellDisplayText(ykienkhacgridColumn);
            dpNgayGioKetThuc.Text = gvSoBienBan.GetFocusedRowCellDisplayText(kethucgridColumn);
            richTextBoxNoiDung.Text = gvSoBienBan.GetFocusedRowCellDisplayText(noidunggridColumn);
            this.idbienban = gvSoBienBan.GetFocusedRowCellDisplayText(idgridColumn);
            ThNguoiLapBienBanTheoBB();
            ThNguoiLamChungTheoBB();
            ThNguoiLienQuanTheoBB();
            ThThongKeThietHai();
        }

        private void ThNguoiLapBienBanTheoBB()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string strQuery = string.Format(@"select ID,MaBienBan,OngBa,ChucVu,BoPhan
                    from BienBanNguoiLap where MaBienBan like '{0}'", txtMaBienBan.Text);
            grNguoiLapBienBan.DataSource = Function.GetDataTable(strQuery);
            Function.Disconnect();//dong ket noi
        }

        private void ThNguoiLamChungTheoBB()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string strQuery = string.Format(@"select ID,MaBienBan,OngBa,ChucVu,BoPhan
                    from BienBanNguoiChungKien where MaBienBan like '{0}'", txtMaBienBan.Text);
            grNguoiLamChung.DataSource = Function.GetDataTable(strQuery);
            Function.Disconnect();//dong ket noi
        }
        //The hien nguoi lien quan
        private void ThNguoiLienQuanTheoBB()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string strQuery = string.Format(@"select isnull(THCongTy,0)+isnull(THCaNhan,0)TongTH,
                * from BienBanPhanBoTH where MaBienBan like '{0}'", txtMaBienBan.Text);
            string sqlQueryTK = string.Format(@"select ID,MaBienBan,ChiTietLo,NguyenNhan,
                DienGiai,GiaTriThietHai,
                TyLeCongTyChiaSe,TyLeCaNhanChiaSe,THCongTy,THCaNhan
                from BienBanThongKe where 
                MaBienBan like '{0}'", txtMaBienBan.Text);
            var dt = Function.GetDataTable(sqlQueryTK);
            var sum = dt.AsEnumerable().Sum(x => x.Field<double>("THCaNhan"));
            txtTongThietHaiVatChatBanDau.Text = sum.ToString();
            grNoiDungBienBan.DataSource = Function.GetDataTable(strQuery);
            Function.Disconnect();//dong ket noi
        }

        private void txtTongThietHaiVatChat_KeyUp(object sender, KeyEventArgs e)
        {
            string str = txtTongThietHaiVatChatBanDau.Text;
            int start = txtTongThietHaiVatChatBanDau.Text.Length - txtTongThietHaiVatChatBanDau.SelectionStart;
            str = str.Replace(",", "");
            txtTongThietHaiVatChatBanDau.Text = FormatMoney(str);
            txtTongThietHaiVatChatBanDau.SelectionStart = -start + txtTongThietHaiVatChatBanDau.Text.Length;
        }
        string FormatMoney(object money)
        {
            string str = money.ToString();
            string pattern = @"(?<a>\d*)(?<b>\d{3})*";
            Match m = Regex.Match(str, pattern, RegexOptions.RightToLeft);
            StringBuilder sb = new StringBuilder();
            foreach (Capture i in m.Groups["b"].Captures)
            {
                sb.Insert(0, "," + i.Value);
            }
            sb.Insert(0, m.Groups["a"].Value);
            return sb.ToString().Trim(',');
        }

        private void txtConLai_TextChanged(object sender, EventArgs e)
        {  
            double tylecanhan, thcanhan;
            tylecanhan = gvNoiDungBienBan.GetFocusedRowCellValue(tylecn_nd) == DBNull.Value ? 0 : Convert.ToDouble(gvNoiDungBienBan.GetFocusedRowCellValue(tylecn_nd));
            thcanhan = (txtTongThietHaiConLai.Text == null ? 0 : double.Parse(txtTongThietHaiConLai.Text)) * (tylecanhan / 100);
            gvNoiDungBienBan.SetFocusedRowCellValue(thaicn_nd, thcanhan);
        }
        private void SoTienConLai()
        {
            double TongTHBanDau = txtTongThietHaiVatChatBanDau.Text!=null? double.Parse(txtTongThietHaiVatChatBanDau.Text):0;
            double TongTHPhanBo = txtTongThietHaiPhanBo.Text!=null? double.Parse(txtTongThietHaiPhanBo.Text):0;
            double TongTHConLai = TongTHBanDau - TongTHPhanBo;
            txtTongThietHaiConLai.Text = TongTHConLai <0?"0":TongTHConLai.ToString();
        }

        private void txtTongThietHaiPhanBo_TextChanged(object sender, EventArgs e)
        {
            SoTienConLai();
        }

        private void btnXoaNguoiLapBB_Click(object sender, EventArgs e)
        {
            XoaNguoiLapBienBan();
        }

        private void btnXoaNguoiLamChung_Click(object sender, EventArgs e)
        {
            XoaNguoiChungKien();
        }

        private void btnXoaNguoiLienQuan_Click(object sender, EventArgs e)
        {
            XoaBienBanPhanBoTH();
        }
        #region xoa
        //xoa nguoi lap bien ban vao BienBanNguoiLap
        private void XoaNguoiLapBienBan()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvNguoiLapBienBan.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNguoiLapBienBan.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from BienBanNguoiLap 
                        where ID like '{0}'",rowData["ID"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                ThNguoiLapBienBan();
                //ThNguoiLamChung();
                //ThPhanBoThietHai();
                ThBienBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        private void btnThongKeThietHaiVatChat_Click(object sender, EventArgs e)
        {
            GhiThongKeThietHai();
            ThThongKeThietHai();
        }
        //xoa nguoi lap bien ban vao BienBanNguoiChungKien
        private void XoaNguoiChungKien()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvNguoiLamChung.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNguoiLamChung.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from BienBanNguoiChungKien 
                        where ID like '{0}'",rowData["ID"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                //ThNguoiLapBienBan();
                ThNguoiLamChung();
                //ThPhanBoThietHai();
                ThBienBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        //xoa phân bổ thiệt hại vật chất
        private void XoaBienBanPhanBoTH()
        {
            try
            {
                Function.ConnectSanXuat();
                DataRow rowData;
                int[] listRowList = this.gvNoiDungBienBan.GetSelectedRows();
                //ArrayList myarrayList = new ArrayList();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiDungBienBan.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from BienBanPhanBoTH 
                      where ID like '{0}'",rowData["ID"]);
                    //myarrayList.Add("Đối tượng:" + rowData["DoiTuong"]);
                    var dt = Function.GetDataTable(strQuery);
                }
                //this.jsonnguoilienquan = JsonConvert.SerializeObject(myarrayList, Formatting.Indented);//chuyen datatable nguoi lien quan ve json de luu vao sổ BienBan
                ThBienBanPhanBoTH();//The hien phan bo thiet hai
                //ThNguoiLapBienBan();
                //ThNguoiLamChung();
                //ThPhanBoThietHai();
                ThBienBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }

        #endregion

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //XoaBienBan();
            //XoaBienNguoiLapBB();
            //XoaBienBanNguoiChungKienBB();
            //XoaBienBienBanNguoiLienQuan();
            //XoaThongKeThietHai();
            //The hien
            XoaNoiDungBienBan();
            ThNguoiLapBienBan();
            ThNguoiLamChung();
            ThPhanBoThietHai();
            ThBienBan();
        }

        private void XoaNoiDungBienBan()
        {
            Function.ConnectSanXuat();
            try
            {
                string strQuery = 
                    string.Format(@"delete from BienBan where MaBienBan like '{0}';
                    delete from BienBanNguoiChungKien where MaBienBan like '{0}';
                    delete from BienBanNguoiLap where MaBienBan like '{0}';
                    delete from BienBanPhanBoTH where MaBienBan like '{0}';
                    delete from BienBanThongKe where MaBienBan like '{0}'",
                    txtMaBienBan.Text);
                var dt = Function.GetDataTable(strQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }

        private void XoaBienNguoiLapBB()
        {
            Function.ConnectSanXuat();
            try
            {
                string strQuery = string.Format(@"delete from BienBan where MaBienBan like '{0}'", txtMaBienBan.Text);
                var dt = Function.GetDataTable(strQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        private void XoaBienBanNguoiChungKienBB()
        {
            Function.ConnectSanXuat();
            try
            {
                string strQuery = string.Format(@"delete from BienBan where MaBienBan like '{0}'", txtMaBienBan.Text);
                var dt = Function.GetDataTable(strQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        private void XoaBienBienBanNguoiLienQuan()
        {
            Function.ConnectSanXuat();
            try
            {
                string strQuery = string.Format(@"delete from BienBan where MaBienBan like '{0}'", txtMaBienBan.Text);
                var dt = Function.GetDataTable(strQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }
        private void XoaBienBan()
        {
            Function.ConnectSanXuat();
            try
            {
                string strQuery = string.Format(@"delete from BienBan where MaBienBan like '{0}'", txtMaBienBan.Text);
                var dt = Function.GetDataTable(strQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }

        private void btnThemNguoiLap_Click(object sender, EventArgs e)
        {
            GhiNguoiLapBienBan();//Ghi nguoi lap bien ban
            ThNguoiLapBienBanTheoBB();
        }

        private void btnThemNguoiLamChung_Click(object sender, EventArgs e)
        {
            GhiNguoiChungKien();//Ghi nguoi chung kien
            ThNguoiLamChungTheoBB();
        }

        private void btnThemNguoiLienQuan_Click(object sender, EventArgs e)
        {
            GhiBienBanPhanBoTH();//Ghi phan bo thiet hai
            ThNguoiLienQuanTheoBB();
        }

        private void btnExportSoBienBan_Click(object sender, EventArgs e)
        {
            grSoBienBan.ShowPrintPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            XoaThongKeThietHai();
        }

        private void gvThongKeThietHai_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column==GiaTriThietHai_tke||e.Column == TyLeCongTy_tke || e.Column == TyLeCaNhan_tke)
            {
                double tyleCongTy, tyleCanNhan, thCongTy, thCanNhan,giatriThietHai;
                giatriThietHai = gvThongKeThietHai.GetFocusedRowCellValue(GiaTriThietHai_tke) == DBNull.Value ? 0 : Convert.ToDouble(gvThongKeThietHai.GetFocusedRowCellValue(GiaTriThietHai_tke));
                tyleCongTy = gvThongKeThietHai.GetFocusedRowCellValue(TyLeCongTy_tke) == DBNull.Value ? 0 : Convert.ToDouble(gvThongKeThietHai.GetFocusedRowCellValue(TyLeCongTy_tke));
                tyleCanNhan = gvThongKeThietHai.GetFocusedRowCellValue(TyLeCaNhan_tke) == DBNull.Value ? 0 : Convert.ToDouble(gvThongKeThietHai.GetFocusedRowCellValue(TyLeCaNhan_tke));
                thCongTy = tyleCongTy / 100 * giatriThietHai;
                thCanNhan = tyleCanNhan / 100 * giatriThietHai;
                //Binding value to gridcontrol
                gvThongKeThietHai.SetFocusedRowCellValue(THCongTy_tke, thCongTy);
                gvThongKeThietHai.SetFocusedRowCellValue(THCaNhan_tke, thCanNhan);
                //Tinh tong thiet hai tru dan vao so tien ban dau
                double tongTH = 0;
                for (int i = 0; i < gvThongKeThietHai.DataRowCount; i++)
                {
                    //DataRow rowData = this.gvNoiDungBienBan.GetDataRow(listRowList[i]);
                    DataRow rowData = this.gvThongKeThietHai.GetDataRow(i);
                    tongTH += rowData["THCaNhan"] != null ? Convert.ToDouble(rowData["THCaNhan"].ToString()) : 0;
                }
                txtTongThietHaiVatChatBanDau.Text = tongTH < 0 ? "0" : tongTH.ToString();
                //MessageBox.Show(""+tongTH, "");
            }
        }
    }
}
