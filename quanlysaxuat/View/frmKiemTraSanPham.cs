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
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using quanlysanxuat.Model;
using quanlysanxuat.Report;

namespace quanlysanxuat.View
{
    //Danh sách nội dung checklist

    public partial class frmKiemTraSanPham : DevExpress.XtraEditors.XtraForm
    {
        public frmKiemTraSanPham()
        {
            InitializeComponent();
        }

        public class CheckNoiDung
        {
            public string HangMuc { set; get; }
            public string IDNoiDung { set; get; }
            public string NoiDungKiemTra { set; get; }
            public string CheckND { set; get; }
            public string IDKiemTra { set; get; }
            public CheckNoiDung(string hangmuc, string idnoidung, string noidungkiemtra, string checknd, string idkiemtra)
            {
                this.HangMuc = hangmuc;
                this.NoiDungKiemTra = noidungkiemtra;
                this.CheckND = checknd;
                this.IDKiemTra = idkiemtra;
                this.IDNoiDung = idnoidung;
            }
            public override string ToString()
            {
                return base.ToString();
            }
        }
        //formload
        private void frmKiemTraSanPham_Load(object sender, EventArgs e)
        {
            dpMin.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpMax.Text = DateTime.Today.ToString("dd-MM-yyyy");
            TheHienThongTinXuatHang();//List thông tin xuất hàng
            TheHienNoiDungGiao();//List thông tin xuất hàng vào gridlookup trong gridview
            ThDanhMucPhieuKiemTra();//List sổ kiểm tra đã lưu trử
            gvThongTinLoHang.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        //Thể hiện List sổ các phiếu đã kiểm tra sản phẩm
        private void ThDanhMucPhieuKiemTra()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select IDKiemTra,NguoiLap,NgayLap,MaQuanLy,
	            CheckKPPN,CheckKQ,FinalCheck
                from tblKiemTraHangHoa 
                group by IDKiemTra,NguoiLap,NgayLap,
				MaQuanLy,CheckKPPN,CheckKQ,FinalCheck");
            gridLookUpEditListView.Properties.DataSource = Function.GetDataTable(sqlQuery);
            gridLookUpEditListView.Properties.DisplayMember = "IDKiemTra";
            gridLookUpEditListView.Properties.ValueMember = "IDKiemTra";
            gridLookUpEditListView.Properties.PopupFormSize = new Size(600, 500);
            Function.Disconnect();
        }

        //Thể hiện các thuộc tính của danh sách xuất kho
        private void TheHienThongTinXuatHang()
        {
            Function.ConnectSanXuat();
            var sqlQuery = string.Format(@"select top 0 Num,madh,MaPo,MaSP_Khach,mabv,sanpham,
                TenSP_KH,SoluongSP,ngaynhan,khachhang from tbl11");
            grThongTinLoHang.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();
        }
        //gridlookupEdit in gridview Thể hiện danh sách các sản phẩm đã xuất kho theo thời gian - Gọi lên bên trong lưới nhằm chọn mã sản phẩm, tên sản phẩm
        private void TheHienNoiDungGiao()
        {
            Function.ConnectSanXuat();
            var sqlQuery = string.Format(@"select Num,madh,MaPo,MaSP_Khach,
                        mabv,sanpham,TenSP_KH,SoluongSP,ngaynhan,khachhang from tbl11
                        where convert(Date,ngaynhan,103)
                        between '{0}' and '{1}'
			            order by left(MaGH,6) desc,
			            SoChungTu_XK asc,Num desc",
                        dpMin.Value.ToString("MM-dd-yyyy"),
                        dpMax.Value.ToString("MM-dd-yyyy"));
            repositoryItemGridLookUpEditMaDH.DataSource = Function.GetDataTable(sqlQuery);
            repositoryItemGridLookUpEditMaDH.DisplayMember = "Num";
            repositoryItemGridLookUpEditMaDH.ValueMember = "Num";
            repositoryItemGridLookUpEditMaDH.PopupFormSize = new Size(900, 600);
        }
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {}

        private void gvThongTinLoHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }
        //sự kiện thay đỗi dữ liệu trên gridlookupedit thì sẽ gán dữ liệu vào các cột khác trong gridview
        private void gvThongTinLoHang_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Function.ConnectSanXuat();
            var sqlQuery = string.Format(@"select khachhang,Num,madh,MaPo,MaSP_Khach,
                        mabv,sanpham,TenSP_KH,SoluongSP,ngaynhan from tbl11
                        where convert(Date,ngaynhan,103)
                        between '{0}' and '{1}'",
                        dpMin.Value.ToString("MM-dd-yyyy"),
                        dpMax.Value.ToString("MM-dd-yyyy"));
            var dataTable = Function.GetDataTable(sqlQuery);
            if (e.Column.FieldName == "Num")
            {
                var value = gvThongTinLoHang.GetRowCellValue(e.RowHandle, e.Column);
                var dataRow = dataTable.AsEnumerable().Where(x => x.Field<int>("Num") == (int)value).FirstOrDefault();
                if (dataRow != null)
                {
                    gvThongTinLoHang.SetRowCellValue(e.RowHandle, "MaPo", dataRow["MaPo"]);
                    gvThongTinLoHang.SetRowCellValue(e.RowHandle, "MaSP_Khach", dataRow["MaSP_Khach"]);
                    gvThongTinLoHang.SetRowCellValue(e.RowHandle, "TenSP_KH", dataRow["TenSP_KH"]);
                    gvThongTinLoHang.SetRowCellValue(e.RowHandle, "SoluongSP", dataRow["SoluongSP"]);
                    gvThongTinLoHang.SetRowCellValue(e.RowHandle, "madh", dataRow["madh"]);
                    gvThongTinLoHang.SetRowCellValue(e.RowHandle, "sanpham", dataRow["sanpham"]);
                    gvThongTinLoHang.SetRowCellValue(e.RowHandle, "mabv", dataRow["mabv"]);
                    txtCustomer.Text= dataRow["khachhang"].ToString();
                }
            }
            gvThongTinLoHang.SelectAll();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string idKiemTra = DateTime.Now.ToString("yyyyMMddHHmmssFF");
            string finalCheck = null;
            if (ckFinalFirst.Checked == true) { finalCheck = ckFinalFirst.Text; }
            else if (ckFinalfty2nd.Checked == true) { finalCheck = ckFinalfty2nd.Text; }
            else if (ckInLine.Checked == true) { finalCheck = ckFinalfty2nd.Text; }
            else if (ckPilot.Checked == true) { finalCheck = ckFinalfty2nd.Text; };
            string kqCheck = null;
            if (ckKetQuaNo.Checked == true) { kqCheck = "0"; }
            else if (ckKetQuaYes.Checked == true) { kqCheck = "1"; }
            string kpCheck = null;
            if (ckKhacPhucNo.Checked == true) { kpCheck = "0"; }
            else if (ckKhacPhucYes.Checked == true) { kpCheck = "1"; }
            //MessageBox.Show("asdfa" + a.ToString(), "");
            try
            {
                int[] listRowList = this.gvThongTinLoHang.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThongTinLoHang.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"insert into tblKiemTraHangHoa 
                        (IDSoXuatHang,NgayGiao,
                        FinalCheck,PoNo,CustomerCode,CodeBB,
                        ProductName,PSXBB,QtyDelivery,
                        CustomerDate,CheckKQ,CheckKPPN,
                        MaQuanLy,IDKiemTra,NguoiLap,Customer,NgayLap)
                        values ('{0}','{1}',
                        '{2}',N'{3}',N'{4}',N'{5}',
                        N'{6}',N'{7}','{8}',
                        '{9}','{10}','{11}','{12}','{13}','{14}',N'{15}',getdate())",
                        rowData["Num"], dpNgayGiao.Value.ToString("MM-dd-yyyy"),
                        finalCheck, rowData["MaPo"], rowData["MaSP_Khach"], rowData["mabv"],
                        rowData["sanpham"], rowData["madh"], rowData["SoluongSP"],
                        rowData["ngaynhan"], kqCheck, kpCheck,
                        idKiemTra + "CS", idKiemTra,
                        MainDev.username,txtCustomer.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                SaveNoiDungCheck(idKiemTra);//Ghi nội dung kiểm tra check
                ThDanhMucPhieuKiemTra();//Load lại list các phiếu đã kiểm tra vào gridlookupEdit
                MessageBox.Show("Success","Message");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "Error");
            }
        }


        private void SaveNoiDungCheck(string idkiemtra)
        {
            string ck11 = null;
            if (ck11No.Checked == true) { ck11 = "0"; }
            else if (ck11Yes.Checked == true) { ck11 = "1"; }
            string ck12 = null;
            if (ck12No.Checked == true) { ck12 = "0"; }
            else if (ck12Yes.Checked == true) { ck12 = "1"; }
            string ck13 = null;
            if (ck13No.Checked == true) { ck13 = "0"; }
            else if (ck13Yes.Checked == true) { ck13 = "1"; }
            string ck14 = null;
            if (ck14No.Checked == true) { ck14 = "0"; }
            else if (ck14Yes.Checked == true) { ck14 = "1"; }
            string ck21 = null;
            if (ck21No.Checked == true) { ck21 = "0"; }
            else if (ck21Yes.Checked == true) { ck21 = "1"; }
            string ck31 = null;
            if (ck31No.Checked == true) { ck31 = "0"; }
            else if (ck31Yes.Checked == true) { ck31 = "1"; }
            string ck32 = null;
            if (ck32No.Checked == true) { ck32 = "0"; }
            else if (ck32Yes.Checked == true) { ck32 = "1"; }
            string ck41 = null;
            if (ck41No.Checked == true) { ck41 = "0"; }
            else if (ck41Yes.Checked == true) { ck41 = "1"; }

            var checkNoidungs = new List<CheckNoiDung>() {
                new CheckNoiDung(lbNgoaiQuan.Text ,lb11.Text ,lbNoiDung11.Text,ck11,idkiemtra),
                new CheckNoiDung(lbNgoaiQuan.Text ,lb12.Text ,lbNoiDung12.Text,ck12,idkiemtra),
                new CheckNoiDung(lbNgoaiQuan.Text ,lb13.Text ,lbNoiDung13.Text,ck13,idkiemtra),
                new CheckNoiDung(lbNgoaiQuan.Text ,lb14.Text ,lbNoiDung14.Text,ck14,idkiemtra),
                new CheckNoiDung(lbKichThuoc.Text ,lb21.Text ,lbNoiDung21.Text,ck21,idkiemtra),
                new CheckNoiDung(lbVatLieu.Text   ,lb31.Text ,lbNoiDung31.Text,ck31,idkiemtra),
                new CheckNoiDung(lbVatLieu.Text   ,lb32.Text ,lbNoiDung32.Text,ck32,idkiemtra),
                new CheckNoiDung(lbLapRap.Text ,lb41.Text ,lbNoiDung41.Text,ck41,idkiemtra),
            };
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (var item in checkNoidungs)
            {
                string sqlQuery = string.Format(@"insert into tblKiemTraHangHoaNoiDung 
                (HangMuc,IDNoiDung,NoiDung,CheckNoiDung,IDKiemTra)
                values (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}')",
                item.HangMuc, item.IDNoiDung,
                item.NoiDungKiemTra, item.CheckND, 
                item.IDKiemTra);
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        //Sự kiện update
        private void UpdateNoiDungCheck()
        {
            string ck11 = null;
            if (ck11No.Checked == true) { ck11 = "0"; }
            else if (ck11Yes.Checked == true) { ck11 = "1"; }
            string ck12 = null;
            if (ck12No.Checked == true) { ck12 = "0"; }
            else if (ck12Yes.Checked == true) { ck12 = "1"; }
            string ck13 = null;
            if (ck13No.Checked == true) { ck13 = "0"; }
            else if (ck13Yes.Checked == true) { ck13 = "1"; }
            string ck14 = null;
            if (ck14No.Checked == true) { ck14 = "0"; }
            else if (ck14Yes.Checked == true) { ck14 = "1"; }
            string ck21 = null;
            if (ck21No.Checked == true) { ck21 = "0"; }
            else if (ck21Yes.Checked == true) { ck21 = "1"; }
            string ck31 = null;
            if (ck31No.Checked == true) { ck31 = "0"; }
            else if (ck31Yes.Checked == true) { ck31 = "1"; }
            string ck32 = null;
            if (ck32No.Checked == true) { ck32 = "0"; }
            else if (ck32Yes.Checked == true) { ck32 = "1"; }
            string ck41 = null;
            if (ck41No.Checked == true) { ck41 = "0"; }
            else if (ck41Yes.Checked == true) { ck41 = "1"; }
            Function.ConnectSanXuat();
            var checkNoidungs = new List<CheckNoiDung>() {
                new CheckNoiDung(lbNgoaiQuan.Text ,lb11.Text ,lbNoiDung11.Text,ck11,gridLookUpEditListView.Text),
                new CheckNoiDung(lbNgoaiQuan.Text ,lb12.Text ,lbNoiDung12.Text,ck12,gridLookUpEditListView.Text),
                new CheckNoiDung(lbNgoaiQuan.Text ,lb13.Text ,lbNoiDung13.Text,ck13,gridLookUpEditListView.Text),
                new CheckNoiDung(lbNgoaiQuan.Text ,lb14.Text ,lbNoiDung14.Text,ck14,gridLookUpEditListView.Text),
                new CheckNoiDung(lbKichThuoc.Text ,lb21.Text ,lbNoiDung21.Text,ck21,gridLookUpEditListView.Text),
                new CheckNoiDung(lbVatLieu.Text   ,lb31.Text ,lbNoiDung31.Text,ck31,gridLookUpEditListView.Text),
                new CheckNoiDung(lbVatLieu.Text   ,lb32.Text ,lbNoiDung32.Text,ck32,gridLookUpEditListView.Text),
                new CheckNoiDung(lbLapRap.Text    ,lb41.Text ,lbNoiDung41.Text,ck41,gridLookUpEditListView.Text),
            };
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                foreach (var item in checkNoidungs)
                {
                    var sqlQuery = string.Format(@"update tblKiemTraHangHoaNoiDung set IDNoiDung='{0}',
                    HangMuc=N'{1}',NoiDung=N'{2}',CheckNoiDung='{3}'
                    where IDKiemTra like '{4}' and IDNoiDung like '{5}'",
                    item.IDNoiDung, item.HangMuc, item.NoiDungKiemTra,
                    item.CheckND, gridLookUpEditListView.Text, item.IDNoiDung);
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    cmd.ExecuteNonQuery();
                }
                Function.Disconnect();
                ThDanhMucPhieuKiemTra();//Load lại list các phiếu đã kiểm tra vào gridlookupEdit
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        //Cập nhật sổ kiểm tra
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int[] listRowList = this.gvThongTinLoHang.GetSelectedRows();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThongTinLoHang.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                             @"update tblKiemTraHangHoa 
                               set PoNo=N'{0}',CustomerCode=N'{1}',CodeBB=N'{2}',
                               ProductName=N'{3}',PSXBB=N'{4}',QtyDelivery='{5}',
                               CustomerDate='{6}',NguoiHC=N'{7}',Customer=N'{8}',NgayHC=getdate()
                               where ID like '{9}'",
                        rowData["MaPo"], rowData["MaSP_Khach"], rowData["mabv"],
                        rowData["sanpham"], rowData["madh"], rowData["SoluongSP"],
                        rowData["ngaynhan"] == null ? "" : Convert.ToDateTime(rowData["ngaynhan"]).ToString("MM-dd-yyyy"),
                        MainDev.username, txtCustomer.Text,rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                UpdateNoiDungCheck();
                UpdateKetQuaKiemTra();
                ThDanhMucPhieuKiemTra();//Load lại list các phiếu đã kiểm tra vào gridlookupEdit
                MessageBox.Show("Success","Message");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reason" + ex, "error");
            }
        }
        //Update ket qua kiem tra & khắc phục phòng ngừa & FinalCheck
        private void UpdateKetQuaKiemTra()
        {
            string finalCheck = null;
            if (ckFinalFirst.Checked == true)       { finalCheck = ckFinalFirst.Text;  ckFinalfty2nd.Checked = false;ckInLine.Checked = false;ckPilot.Checked = false; }
            else if (ckFinalfty2nd.Checked == true) { finalCheck = ckFinalfty2nd.Text; ckFinalFirst.Checked=false; ckInLine.Checked = false; ckPilot.Checked = false; }
            else if (ckInLine.Checked == true)      { finalCheck = ckInLine.Text; ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckPilot.Checked = false; }
            else if (ckPilot.Checked == true)       { finalCheck = ckPilot.Text; ckFinalFirst.Checked=false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; };
            string kqCheck = null;
            if (ckKetQuaNo.Checked == true) { kqCheck = "0"; }
            else if (ckKetQuaYes.Checked == true) { kqCheck = "1"; }
            string kpCheck = null;
            if (ckKhacPhucNo.Checked == true) { kpCheck = "0"; }
            else if (ckKhacPhucYes.Checked == true) { kpCheck = "1"; }
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"update tblKiemTraHangHoa 
                set CheckKQ='{0}',CheckKPPN='{1}',FinalCheck='{2}'
                where IDKiemTra = '{3}'", kqCheck, kpCheck, finalCheck, gridLookUpEditListView.Text);
            var kq = Function.GetDataTable(sqlQuery);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //Xóa thông tin Nội dung chính
                Function.ConnectSanXuat();
                string sqlQueryListKiemTra = string.Format(@"delete from tblKiemTraHangHoa 
                where IDKiemTra like '{0}'", gridLookUpEditListView.Text);
                var kqNDung = Function.GetDataTable(sqlQueryListKiemTra);
                //Xóa nội dung kiểm tra
                string sqlQueryListChek = string.Format(@"delete from tblKiemTraHangHoaNoiDung 
                where IDKiemTra like '{0}'", gridLookUpEditListView.Text);
                var kqNoiDungKiem = Function.GetDataTable(sqlQueryListChek);
                ThDanhMucPhieuKiemTra();//Load lại list các phiếu đã kiểm tra vào gridlookupEdit
                TheHienThongTinXuatHang();//Load lai list moi null
                MessageBox.Show("Success","Message");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reason"+ex, "Error");
            }
        }

        private void ckFinalFirst_Click(object sender, EventArgs e)
        {
            ckFinalfty2nd.Checked = false;
            ckInLine.Checked = false;
            ckPilot.Checked = false;
        }

        private void ckFinalfty2nd_Click(object sender, EventArgs e)
        {
            ckFinalFirst.Checked = false;
            ckInLine.Checked = false;
            ckPilot.Checked = false;
        }

        private void ckInLine_Click(object sender, EventArgs e)
        {
            ckFinalFirst.Checked = false;
            ckFinalfty2nd.Checked = false;
            ckPilot.Checked = false;
        }

        private void ckPilot_Click(object sender, EventArgs e)
        {
            ckFinalFirst.Checked = false;
            ckFinalfty2nd.Checked = false;
            ckInLine.Checked = false;
        }

        private void ck11Yes_Click(object sender, EventArgs e)
        {
            ck11No.Checked = false;
        }

        private void ck11No_Click(object sender, EventArgs e)
        {
            ck11Yes.Checked = false;
        }

        private void ck12Yes_Click(object sender, EventArgs e)
        {
            ck12No.Checked = false;
        }

        private void ck12No_Click(object sender, EventArgs e)
        {
            ck12Yes.Checked = false;
        }

        private void ck13Yes_Click(object sender, EventArgs e)
        {
            ck13No.Checked = false;
        }

        private void ck13No_Click(object sender, EventArgs e)
        {
            ck13Yes.Checked = false;
        }

        private void ck14Yes_Click(object sender, EventArgs e)
        {
            ck14No.Checked = false;
        }
        private void ck14No_Click(object sender, EventArgs e)
        {
            ck14Yes.Checked = false;
        }

        private void ck21Yes_Click(object sender, EventArgs e)
        {
            ck21No.Checked = false;
        }

        private void ck21No_Click(object sender, EventArgs e)
        {
            ck21Yes.Checked = false;
        }

        private void ck31Yes_Click(object sender, EventArgs e)
        {
            ck31No.Checked = false;
        }

        private void ck31No_Click(object sender, EventArgs e)
        {
            ck31Yes.Checked = false;
        }

        private void ck32Yes_Click(object sender, EventArgs e)
        {
            ck32No.Checked = false;
        }

        private void ck32No_Click(object sender, EventArgs e)
        {
            ck32Yes.Checked = false;
        }

        private void ck41Yes_Click(object sender, EventArgs e)
        {
            ck41No.Checked = false;
        }

        private void ck41No_Click(object sender, EventArgs e)
        {
            ck41Yes.Checked = false;
        }

        private void ckKetQuaYes_Click(object sender, EventArgs e)
        {
            ckKetQuaNo.Checked = false;
        }

        private void ckKetQuaNo_Click(object sender, EventArgs e)
        {
            ckKetQuaYes.Checked = false;
        }
        private void ckKhacPhucYes_Click(object sender, EventArgs e)
        {
            ckKhacPhucNo.Checked = false;
        }
        private void ckKhacPhucNo_Click(object sender, EventArgs e)
        {
            ckKhacPhucYes.Checked = false;
        }


        private void ThThongTinLoHangDaKiem()
        {
            Function.ConnectSanXuat();
            var sqlQuery = string.Format(
                @"select ID,IDKiemTra,IDSoXuatHang Num,NgayGiao,
				FinalCheck,PoNo MaPo,CustomerCode MaSP_Khach,
				CodeBB mabv,ProductName sanpham,PSXBB madh,QtyDelivery SoluongSP,
				CustomerDate ngaynhan
				from tblKiemTraHangHoa where IDKiemTra like '{0}'",
                gridLookUpEditListView.Text);
            var dataTable = Function.GetDataTable(sqlQuery);
            grThongTinLoHang.DataSource = dataTable;
            Function.Disconnect();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            TheHienThongTinXuatHang();

        }
        private void gridLookUpEditListView_Click(object sender, EventArgs e)
        {
            if (gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
               gridLookUpEditViewListView.Columns["IDKiemTra"]) != null)
            {
                txtAuditor.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                gridLookUpEditViewListView.Columns["NguoiLap"]).ToString();
                string ngayLap = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["NgayLap"]).ToString();
                string maQuanLy = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["MaQuanLy"]).ToString();
                string checkKQ = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["CheckKQ"]).ToString();
                string checkKP = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["CheckKPPN"]).ToString();
                txtCustomer.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["Customer"]).ToString();

                //Gọi lại data của sự kiện kết quả kiểm và sự kiện check khắc phục phòng ngừa 
                //txtMaQuanLy.Text = maQuanLy;
                //ckKetQuaNo.Checked = checkKQ == "1" ? true : false;
                //ckKhacPhucNo.Checked = checkKP == "1" ? true : false;
                txtMaQuanLy.Text = maQuanLy;
                if (checkKQ == "1") { ckKetQuaYes.Checked = true; ckKetQuaNo.Checked = false; };
                if (checkKP == "1") { ckKhacPhucYes.Checked = true; ckKhacPhucNo.Checked = false; };
            }
        }
        private void gridLookUpEditListView_EditValueChanged(object sender, EventArgs e)
        {
            if (gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                 gridLookUpEditViewListView.Columns["IDKiemTra"]) != null)
            {
                txtAuditor.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["NguoiLap"]).ToString();
                string ngayLap = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["NgayLap"]).ToString();
                string maQuanLy = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["MaQuanLy"]).ToString();
                string checkKQ = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["CheckKQ"]).ToString();
                string checkKP = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["CheckKPPN"]).ToString();
                string finalcheck = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["FinalCheck"]).ToString();

                //check final
                if (finalcheck == "")                { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = false; };
                if (finalcheck == "First Fty Final") { ckFinalFirst.Checked = true; ckFinalfty2nd.Checked  = false;ckInLine.Checked  = false;ckPilot.Checked  = false; };
                if (finalcheck == "2nd Fty Final")   { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = true;ckInLine.Checked   = false;ckPilot.Checked  = false; };
                if (finalcheck == "In-Line")         { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = true; ckPilot.Checked  = false; };
                if (finalcheck == "Pilot Run")       { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = true;  };

                //Check kết quả kiểm và Khắc phục phòng ngừa
                txtMaQuanLy.Text = maQuanLy;
                ckKetQuaYes.Checked = false;ckKetQuaNo.Checked = false;
                ckKhacPhucYes.Checked = false;ckKhacPhucNo.Checked = false;
                if (checkKQ == "True") { ckKetQuaYes.Checked = true;} 
                    else if (checkKQ == "False") { ckKetQuaNo.Checked = false;} 
                if (checkKP == "True") { ckKhacPhucYes.Checked = true; }
                    else if (checkKP=="False") { ckKhacPhucYes.Checked = false;};
            }
            TheHienNoiDungGiao();//
            ThThongTinLoHangDaKiem();//Gọi List thông tin chi tiết các lô đã kiểm tra vào gridviewThongTinLoHang
            ThListNoiDungKiem();
        }
        //Gọi lại danh sách mục phục vụ sự kiện update
        private void ThListNoiDungKiem()
        {
            Function.ConnectSanXuat();
            var sqlQuery = string.Format(@"select IDNoiDung,HangMuc,NoiDung,
                    CheckNoiDung,IDKiemTra
                    from tblKiemTraHangHoaNoiDung 
                    where IDKiemTra like '{0}'", gridLookUpEditListView.Text);
            var dataTable = Function.GetDataTable(sqlQuery);
            if (dataTable != null)
            {
                var kq11 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == lb11.Text).FirstOrDefault();
                if (kq11["CheckNoiDung"].ToString() == "1") { ck11Yes.Checked = true; ck11No.Checked = false; }
                else if (kq11["CheckNoiDung"].ToString() == "0") { ck11Yes.Checked = false; ck11No.Checked = true; };
                var kq12 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == lb12.Text).FirstOrDefault();
                if (kq12["CheckNoiDung"].ToString() == "1") { ck12Yes.Checked = true; ck12No.Checked = false; }
                else if (kq12["CheckNoiDung"].ToString() == "0") { ck12Yes.Checked = false; ck13No.Checked = true; };
                var kq13 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == lb13.Text).FirstOrDefault();
                if (kq13["CheckNoiDung"].ToString() == "1") { ck13Yes.Checked = true; ck13No.Checked = false; }
                else if (kq13["CheckNoiDung"].ToString() == "0") { ck13Yes.Checked = false; ck13No.Checked = true; };
                var kq14 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == lb14.Text).FirstOrDefault();
                if (kq14["CheckNoiDung"].ToString() == "1") { ck14Yes.Checked = true; ck14No.Checked = false; }
                else if (kq14["CheckNoiDung"].ToString() == "0") { ck14Yes.Checked = false; ck14No.Checked = true; };
                var kq21 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == lb21.Text).FirstOrDefault();
                if (kq21["CheckNoiDung"].ToString() == "1") { ck21Yes.Checked = true; ck21No.Checked = false; }
                else if (kq21["CheckNoiDung"].ToString() == "0") { ck21Yes.Checked = false; ck21No.Checked = true; };
                var kq31 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == lb31.Text).FirstOrDefault();
                if (kq31["CheckNoiDung"].ToString() == "1") { ck31Yes.Checked = true; ck31No.Checked = false; }
                else if (kq31["CheckNoiDung"].ToString() == "0") { ck31Yes.Checked = false; ck31No.Checked = true; };
                var kq32 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == lb32.Text).FirstOrDefault();
                if (kq32["CheckNoiDung"].ToString() == "1") { ck32Yes.Checked = true; ck32No.Checked = false; }
                else if (kq32["CheckNoiDung"].ToString() == "0") { ck32Yes.Checked = false; ck32No.Checked = true; };
                var kq41 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == lb41.Text).FirstOrDefault();
                if (kq41["CheckNoiDung"].ToString() == "1") { ck41Yes.Checked = true; ck41No.Checked = false; }
                else if (kq41["CheckNoiDung"].ToString() == "0") { ck41Yes.Checked = false; ck41No.Checked = true; };
            };
        }
        public static string maPhieuKiem;
        private void btnReport_Click(object sender, EventArgs e)
        {
            maPhieuKiem = gridLookUpEditListView.Text;
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select * from tblKiemTraHangHoa 
                where IDKiemTra like '{0}'", gridLookUpEditListView.Text);
            RpKiemTraSanPham kiemTraSanPham = new RpKiemTraSanPham();
            kiemTraSanPham.DataSource = Function.GetDataTable(sqlQuery);
            kiemTraSanPham.DataMember = "Table";
            kiemTraSanPham.CreateDocument(false);
            kiemTraSanPham.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = gridLookUpEditListView.Text;
            PrintTool tool = new PrintTool(kiemTraSanPham.PrintingSystem);
            kiemTraSanPham.ShowPreviewDialog();
            Function.Disconnect();
        }
        //Tra cứu thông tin sản phẩm xuất kho theo thời gian
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            TheHienNoiDungGiao();
        }
    }
}
