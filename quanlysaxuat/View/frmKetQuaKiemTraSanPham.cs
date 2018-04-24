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
using static quanlysanxuat.View.frmKiemTraSanPham;

namespace quanlysanxuat.View
{
    public partial class frmKetQuaKiemTraSanPham : DevExpress.XtraEditors.XtraForm
    {
        public frmKetQuaKiemTraSanPham()
        {
            InitializeComponent();
        }
        public class ChiTietKiemTra
        {
            public string ChiTiet1 { set; get; }
            public string ChiTiet2 { set; get; }
            public string ChiTiet3 { set; get; }
            public string ChiTiet4 { set; get; }
            public string ChiTiet5 { set; get; }
            public string ChiTiet6 { set; get; }
            public string ChiTiet7 { set; get; }
            public string ChiTiet8 { set; get; }
            public string ChiTiet9 { set; get; }
            public string ChiTiet10 { set; get; }
            public ChiTietKiemTra(string chitiet1, string chitiet2, string chitiet3, string chitiet4,
                string chitiet5, string chitiet6, string chitiet7, string chitiet8,
                string chitiet9, string chitiet10)
            {
                this.ChiTiet1 = chitiet1; this.ChiTiet2 = chitiet2; this.ChiTiet3 = chitiet3; this.ChiTiet4 = chitiet4;
                this.ChiTiet5 = chitiet5; this.ChiTiet6 = chitiet6; this.ChiTiet7 = chitiet7; this.ChiTiet8 = chitiet8;
                this.ChiTiet9 = chitiet9; this.ChiTiet10 = chitiet10;
            }
            public override string ToString()
            {
                return base.ToString();
            }
        }
        private void ThDanhMucSanPhamXuatKho()
        {
            Function.ConnectSanXuat();
            var sqlQuery = string.Format(@"select ngaynhan,Num,madh,MaPo,MaSP_Khach,
                mabv,sanpham,TenSP_KH,SoluongSP,
                khachhang,BTPT11,SL_DH soluongyc,
                khachhang,TongCongBaoBi from tbl11
				order by ngaynhan desc");
            gridLookUpEditListViewXuatKho.Properties.DataSource = Function.GetDataTable(sqlQuery);
            gridLookUpEditListViewXuatKho.Properties.DisplayMember = "Num";
            gridLookUpEditListViewXuatKho.Properties.ValueMember = "Num";
            gridLookUpEditListViewXuatKho.Properties.PopupFormSize = new Size(1200, 600);
        }
        private void ThDanhMucKetQuaKiemTra()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"SELECT IDKQKiemTra,ID,FinalCheck,
                MaSanPham,TenSanPham,NgayBHBV,PO,PhieuSanXuat,
                CodeBB,NgayGiao,SoLuongGiao,SoThungBich,
                TomTatKetQua,KetQuaKiem,HDKhacPhuc,PPXuLy,
                KQPPXuLy,NguoiLap,NgayLap,NguoiHC,
                NgayHC,ToXacNhan  FROM KetQuaKTSP");
            gridLookUpEditListCheck.Properties.DataSource = Function.GetDataTable(sqlQuery);
            gridLookUpEditListCheck.Properties.DisplayMember = "IDKQKiemTra";
            gridLookUpEditListCheck.Properties.ValueMember = "IDKQKiemTra";
            //gridLookUpEditListCheck.EditValue = "";
            gridLookUpEditListCheck.Properties.PopupFormSize = new Size(1200, 600);
            Function.Disconnect();
        }
        private void ThNoiDungKiemTra()
        {
            /*select IDNoiDung,NoiDungKT,HangMuc,
                IDKQKiemTra,KetQuaChiTiet from KetQuaKTSPNoiDung */
        }
        private void ThKichThuocNgoaiQuan()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MucKiemTra", typeof(string));
            dataTable.Columns.Add("AQLLevel", typeof(string));
            dataTable.Columns.Add("SoLuongKiem", typeof(string));
            dataTable.Columns.Add("LoiChapNhan", typeof(string));
            dataTable.Columns.Add("LoiPhatHien", typeof(string));
            dataTable.Columns.Add("KetQuaYes", typeof(bool));
            dataTable.Columns.Add("KetQuaNo", typeof(bool));
            dataTable.Rows.Add("Mgoại quan", "", "", "", "", 0, 0);
            dataTable.Rows.Add("Kích thước", "", "", "", "", 0, 0);
            gridControlNgoaiQuan.DataSource = dataTable;
        }
        private void ThChiTietKiem()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("HangMuc", typeof(string));
            dataTable.Columns.Add("st1", typeof(string));
            dataTable.Columns.Add("nd2", typeof(string));
            dataTable.Columns.Add("rd3", typeof(string));
            dataTable.Columns.Add("th4", typeof(string));
            dataTable.Columns.Add("th5", typeof(string));
            dataTable.Columns.Add("th6", typeof(string));
            dataTable.Columns.Add("th7", typeof(string));
            dataTable.Columns.Add("th8", typeof(string));
            dataTable.Columns.Add("DoDayLopPhu", typeof(string));
            dataTable.Rows.Add("Kích thước (mm)", "", "", "", "", "", "", "", "", "Độ dày lớp phủ(µm)");
            dataTable.Rows.Add("Dung Sai", "", "", "", "", "", "", "", "", "");
            dataTable.Rows.Add("1", "", "", "", "", "", "", "", "", "");
            dataTable.Rows.Add("2", "", "", "", "", "", "", "", "", "");
            dataTable.Rows.Add("3", "", "", "", "", "", "", "", "", "");
            dataTable.Rows.Add("4", "", "", "", "", "", "", "", "", "");
            dataTable.Rows.Add("5", "", "", "", "", "", "", "", "", "");
            gridControlKichThuocChiTiet.DataSource = dataTable;
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
        private void ckTraVeNoiSanXuat_CheckedChanged(object sender, EventArgs e)
        {
            ckLuaChonKhac.Checked = false;
        }

        private void ckLuaChonKhac_CheckedChanged(object sender, EventArgs e)
        {
            ckTraVeNoiSanXuat.Checked = false;
        }
        //Tao ID phiếu kiểm tra để tạo unique
        private string IDKiemTra()
        {
            Function.ConnectSanXuat();
            string strQuery = string.Format(@"");
            return strQuery;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridLookUpEditListViewXuatKho.Text == null)
            {
                MessageBox.Show("Đối tượng xuất kho không được rỗng", "Mesage"); return;
            }
            string idKiemTra = DateTime.Now.ToString("yyyyMMddHHmmssFF");
            //save KetQuaKiemTraSanPham
            SaveKetQuaKiemTraSanPham(idKiemTra);
            //Save KetQuaKiemTraSanPhamNgoaiQuan
            SaveKetQuaKiemTraSanPhamNgoaiQuan(idKiemTra);
            ////Save KetQuaKiemTraSanPhamKichThuoc
            SaveKetQuaKiemTraSanPhamNoiDung(idKiemTra);
            ////Save KetQuaKiemTraSanPhamNoiDung
            SaveKetQuaKiemTraSanPhamKichThuocChiTiet(idKiemTra);
            //Thể hiện kết quả kiểm tra vào danh sách List gridlookup
            ThDanhMucKetQuaKiemTra();
            gridLookUpEditListCheck.Text = idKiemTra;
        }

        //Save phan ket qua kiem tra san pham -> dbo.KetQuaKTSP
        private void SaveKetQuaKiemTraSanPham(string idkiemtra)
        {
            //Check final
            string finalCheck = null;
            if (ckFinalFirst.Checked == false && ckFinalfty2nd.Checked == false&& ckInLine.Checked==false&& ckPilot.Checked==false) { finalCheck = ""; }
            else if (ckFinalFirst.Checked == true) { finalCheck = ckFinalFirst.Text; }
            else if (ckFinalfty2nd.Checked == true) { finalCheck = ckFinalfty2nd.Text; }
            else if (ckInLine.Checked == true) { finalCheck = ckInLine.Text; }
            else if (ckPilot.Checked == true) { finalCheck = ckPilot.Text; };
            //Check kết quả
            string ketquaCheck = null;
            if (ckKetQuaNo.Checked == false && ckKetQuaYes.Checked == false) { ketquaCheck = ""; }
            else if (ckKetQuaYes.Checked == true) { ketquaCheck = "1"; }
            else if (ckKetQuaNo.Checked == true) { ketquaCheck = "0"; }
            //Check khắc phục
            string khacphucCheck = null;
            if (ckKhacPhucNo.Checked == false && ckKhacPhucYes.Checked == false) { khacphucCheck = ""; }
            else if (ckKhacPhucYes.Checked == true) { khacphucCheck = "1"; }
            else if (ckKhacPhucNo.Checked == true) { khacphucCheck = "0"; }
            //check phương án khắc phục
            string ppXuLy = null;
            if (ckTraVeNoiSanXuat.Checked == true) { ppXuLy = "Trả về nơi sản xuất"; }//nếu là 0 thì trả về nơi sản xuất 
            else if (ckLuaChonKhac.Checked == true) { ppXuLy = "Lựa chọn khác"; } //Nếu là 1 thì cho lựa chọn khác
            try
            {
                string sqlQuery = string.Format(@"insert into KetQuaKTSP 
               (IDKQKiemTra,FinalCheck,
                MaSanPham,TenSanPham,NgayBHBV,PO,PhieuSanXuat,
                CodeBB,NgayGiao,SoLuongGiao,SoThungBich,
                TomTatKetQua,KetQuaKiem,HDKhacPhuc,PPXuLy,
                KQPPXuLy,NguoiLap,ToXacNhan,IDXuatKho,NgayLap)
                values ('{0}','{1}',
                   N'{2}',N'{3}',N'{4}',N'{5}',
                   N'{6}',N'{7}',N'{8}',
                   N'{9}',N'{10}',N'{11}',
                    '{12}','{13}',N'{14}',
                   N'{15}',N'{16}',N'{17}',N'{18}',GetDate())",
                    idkiemtra, finalCheck,
                    txtMaSanPhamKhach.Text, txtTenSanPham.Text,
                    dpNgayBanHanhBanVe.Value.ToString("MM-dd-yyyy"),
                    txtPO.Text, txtPSX.Text,
                    txtCodeBB.Text, dpNgayLap.Value.ToString("MM-dd-yyyy"),
                    Convert.ToDouble(txtSoLuongGiao.Text),
                    txtSoThungBich.Text, txtTomTatKetQuaLoi.Text,
                    ketquaCheck, khacphucCheck, ppXuLy,
                    txtGhiChu.Text, MainDev.username,
                    txtToXacNhan.Text, gridLookUpEditListViewXuatKho.Text);
                GetDataTable(sqlQuery);
                //MessageBox.Show("Success", "Message");
            }
            catch (Exception ex) { MessageBox.Show("Reason" + ex, "Error"); }
        }
        //save phan kiem tra ngoai quan va kich thuoc -> dbo.KetQuaKTSPNgoaiQuan
        private void SaveKetQuaKiemTraSanPhamNgoaiQuan(string idkiemtra)
        {
            try
            {
                int listRowList = gridViewNgoaiQuan.RowCount; DataRow rowData;
                for (int i = 0; i < listRowList; i++)
                {
                    rowData = gridViewNgoaiQuan.GetDataRow(i);
                    string sqlQuery = string.Format(@"insert into KetQuaKTSPNgoaiQuan 
                    (IDKQKiemTra,MucKiemTra,AQLLevel,
                    SoLuongKiem,LoiChapNhan,
                    LoiPhatHien,KetQuaYes,
                    KetQuaNo)
                    values('{0}',N'{1}',N'{2}',
                           N'{3}',N'{4}',N'{5}',
                           N'{6}',N'{7}')",
                        idkiemtra, rowData["MucKiemTra"],
                        rowData["AQLLevel"], rowData["SoLuongKiem"],
                        rowData["LoiChapNhan"], rowData["LoiPhatHien"],
                        rowData["KetQuaYes"], rowData["KetQuaNo"]);
                    GetDataTable(sqlQuery);
                }
                //MessageBox.Show("Success", "Message");
            }
            catch (Exception ex) { MessageBox.Show("Reason" + ex, "Error"); }
        }
        //Save phan noi dung kiem -> dbo.KetQuaKTSPNoiDung
        private void SaveKetQuaKiemTraSanPhamNoiDung(string idkiemtra)
        {
            string ck11 = null;
            if (ck11No.Checked == false &&
            ck11Yes.Checked == false) { ck11 = ""; }
            else if (ck11No.Checked == true) { ck11 = "0"; }
            else if (ck11Yes.Checked == true) { ck11 = "1"; }

            string ck12 = null;
            if (ck12No.Checked == false &&
                ck12Yes.Checked == false) { ck12 = ""; }
            else if (ck12No.Checked == true) { ck12 = "0"; }
            else if (ck12Yes.Checked == true) { ck12 = "1"; }

            string ck13 = null;
            if (ck13No.Checked == false &&
                ck13Yes.Checked == false) { ck13 = ""; }
            else if (ck13No.Checked == true) { ck13 = "0"; }
            else if (ck13Yes.Checked == true) { ck13 = "1"; }

            string ck14 = null;
            if (ck14No.Checked == false &&
                ck14Yes.Checked == false) { ck14 = ""; }
            else if (ck14No.Checked == true) { ck14 = "0"; }
            else if (ck14Yes.Checked == true) { ck14 = "1"; }

            string ck21 = null;
            if (ck21No.Checked == false &&
                ck21Yes.Checked == false) { ck21 = ""; }
            else if (ck21No.Checked == true) { ck21 = "0"; }
            else if (ck21Yes.Checked == true) { ck21 = "1"; }

            string ck31 = null;
            if (ck31No.Checked == false &&
                ck31Yes.Checked == false) { ck31 = ""; }
            else if (ck31No.Checked == true) { ck31 = "0"; }
            else if (ck31Yes.Checked == true) { ck31 = "1"; }

            string ck32 = null;
            if (ck32No.Checked == false &&
                ck32Yes.Checked == false) { ck32 = ""; }
            else if (ck32No.Checked == true) { ck32 = "0"; }
            else if (ck32Yes.Checked == true) { ck32 = "1"; }

            string ck41 = null;
            if (ck41No.Checked == false &&
                ck41Yes.Checked == false) { ck41 = ""; }
            else if (ck41No.Checked == true) { ck41 = "0"; }
            else if (ck41Yes.Checked == true) { ck41 = "1"; }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("IDNoiDung", typeof(string));
            dataTable.Columns.Add("NoiDungKT", typeof(string));
            dataTable.Columns.Add("HangMuc", typeof(string));
            dataTable.Columns.Add("IDKQKiemTra", typeof(string));
            dataTable.Columns.Add("KetQuaChiTiet", typeof(string));
            dataTable.Rows.Add(lb11.Text.Trim(), lbNoiDung11.Text, lbNgoaiQuan.Text, idkiemtra, ck11);
            dataTable.Rows.Add(lb12.Text.Trim(), lbNoiDung12.Text, lbNgoaiQuan.Text, idkiemtra, ck12);
            dataTable.Rows.Add(lb13.Text.Trim(), lbNoiDung13.Text, lbNgoaiQuan.Text, idkiemtra, ck13);
            dataTable.Rows.Add(lb14.Text.Trim(), lbNoiDung14.Text, lbNgoaiQuan.Text, idkiemtra, ck14);
            dataTable.Rows.Add(lb21.Text.Trim(), lbNoiDung21.Text, lbKichThuoc.Text, idkiemtra, ck21);
            dataTable.Rows.Add(lb31.Text.Trim(), lbNoiDung31.Text, lbVatLieu.Text, idkiemtra, ck31);
            dataTable.Rows.Add(lb32.Text.Trim(), lbNoiDung32.Text, lbVatLieu.Text, idkiemtra, ck32);
            dataTable.Rows.Add(lb41.Text.Trim(), txtNoiDung41.Text, lbLapRap.Text, idkiemtra, ck41);
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                foreach (DataRow item in dataTable.Rows)
                {
                    string sqlQuery = string.Format(@"insert into KetQuaKTSPNoiDung 
                   (IDNoiDung,NoiDungKT,HangMuc,
                    IDKQKiemTra,KetQuaChiTiet)
                    values('{0}',N'{1}',
                          N'{2}',N'{3}',
                          N'{4}')",
                    item["IDNoiDung"], item["NoiDungKT"], item["HangMuc"],
                    item["IDKQKiemTra"], item["KetQuaChiTiet"]);
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    cmd.ExecuteNonQuery();
                }
                //MessageBox.Show("Success", "Message");
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show("Reason" + ex, "Error"); }
        }
        //save phan kiem tra kich thuoc chi tiet ->[dbo].[KetQuaKTSPKTChiTiet]
        private void SaveKetQuaKiemTraSanPhamKichThuocChiTiet(string idkiemtra)
        {
            try
            {
                for (int i = 0; i < gridViewKichThuocChiTiet.RowCount; i++)
                {
                    DataRow row = gridViewKichThuocChiTiet.GetDataRow(i);
                    string sqlQuery = string.Format(@"insert into KetQuaKTSPKTChiTiet 
               (IDKQKiemTra,HangMuc,st1,nd2,rd3,th4,
                th5,th6,th7,th8,DoDayLopPhu)
                values ('{0}',N'{1}','{2}',
	                   N'{3}',N'{4}','{5}',
                        '{6}','{7}','{8}',
	                    '{9}',N'{10}')",
                            idkiemtra, row["HangMuc"], row["st1"],
                            row["nd2"], row["rd3"],
                            row["th4"], row["th5"],
                            row["th6"], row["th7"],
                            row["th8"], row["DoDayLopPhu"]);
                    GetDataTable(sqlQuery);
                }
                MessageBox.Show("Success", "Message");
            }
            catch (Exception ex) { MessageBox.Show("Reason" + ex, "Error"); }
        }

        private void SaveCheckNoiDungCheck(string idkiemtra)
        {

        }
        //Cập nhật các nội dung
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateKetQuaKiemTraSanPham();
            UpdateKetQuaKiemTraSanPhamNgoaiQuan();
            UpdateKetQuaKiemTraSanPhamNoiDung();
            UpdateKetQuaKiemTraSanPhamKichThuocChiTiet();
            //Thể hiện kết quả kiểm tra vào danh sách List gridlookup
            ThDanhMucKetQuaKiemTra();
        }

        private void UpdateKetQuaKiemTraSanPham()
        {
            //Check final
            string finalCheck = null;
            if (ckFinalFirst.Checked == false && ckFinalfty2nd.Checked == false && ckInLine.Checked == false && ckPilot.Checked == false) { finalCheck = ""; }
            else if (ckFinalFirst.Checked == true) { finalCheck = ckFinalFirst.Text; }
            else if (ckFinalfty2nd.Checked == true) { finalCheck = ckFinalfty2nd.Text; }
            else if (ckInLine.Checked == true) { finalCheck = ckInLine.Text; }
            else if (ckPilot.Checked == true) { finalCheck = ckPilot.Text; };
            //Check kết quả
            string ketquaCheck = null;
            if (ckKetQuaNo.Checked == false && ckKetQuaYes.Checked == false) { ketquaCheck = ""; }
            else if (ckKetQuaNo.Checked == true) { ketquaCheck = "0"; }
            else if (ckKetQuaYes.Checked == true) { ketquaCheck = "1"; };
            //Check khắc phục
            string khacphucCheck = null;
            if (ckKhacPhucNo.Checked == false && ckKhacPhucYes.Checked == false) { khacphucCheck = ""; }
            else if (ckKhacPhucNo.Checked == true) { khacphucCheck = "0"; }
            else if (ckKhacPhucYes.Checked == true) { khacphucCheck = "1"; }
            //check phương án khắc phục
            string ppXuLy = null;
            if (ckTraVeNoiSanXuat.Checked == true) { ppXuLy = "Trả về nơi sản xuất"; }//nếu là 0 thì trả về nơi sản xuất 
            else if (ckLuaChonKhac.Checked == true) { ppXuLy = "Lựa chọn khác"; } //Nếu là 1 thì cho lựa chọn khác
            try
            {
                string sqlQuery = string.Format(@"update KetQuaKTSP set
                           FinalCheck='{0}',MaSanPham=N'{1}',
                           TenSanPham=N'{2}',NgayBHBV=N'{3}',PO=N'{4}',PhieuSanXuat=N'{5}',
                           CodeBB=N'{6}',NgayGiao=N'{7}',SoLuongGiao=N'{8}',SoThungBich=N'{9}',
                           TomTatKetQua=N'{10}',KetQuaKiem='{11}',HDKhacPhuc='{12}',PPXuLy=N'{13}',
                           KQPPXuLy=N'{14}',NguoiHC=N'{15}',ToXacNhan = N'{16}', NgayHC=GetDate()
	                       where IDKQKiemTra like '{17}'",
                    finalCheck, txtMaSanPhamKhach.Text,
                    txtTenSanPham.Text, dpNgayBanHanhBanVe.Value.ToString("MM-dd-yyyy"),
                    txtPO.Text, txtPSX.Text,
                    txtCodeBB.Text, dpNgayGiao.Value.ToString("MM-dd-yyyy"),
                    Convert.ToDouble(txtSoLuongGiao.Text),
                    txtSoThungBich.Text, txtTomTatKetQuaLoi.Text,
                    ketquaCheck, khacphucCheck, ppXuLy,
                    txtGhiChu.Text, MainDev.username,
                    txtToXacNhan.Text, gridLookUpEditListCheck.Text);
                GetDataTable(sqlQuery);
                //MessageBox.Show("Success", "Message");
            }
            catch (Exception ex) { MessageBox.Show("Reason" + ex, "Error"); }
        }
        //save phan kiem tra ngoai quan va kich thuoc -> dbo.KetQuaKTSPNgoaiQuan
        private void UpdateKetQuaKiemTraSanPhamNgoaiQuan()
        {
            try
            {
                int listRowList = gridViewNgoaiQuan.RowCount;
                DataRow rowData;
                for (int i = 0; i < listRowList; i++)
                {
                    rowData = gridViewNgoaiQuan.GetDataRow(i);
                    string sqlQuery = string.Format(@" update KetQuaKTSPNgoaiQuan 
                    set MucKiemTra=N'{0}',AQLLevel=N'{1}',
                    SoLuongKiem=N'{2}',LoiChapNhan=N'{3}',
                    LoiPhatHien=N'{4}',KetQuaYes=N'{5}',
                    KetQuaNo=N'{6}' where ID like '{7}'",
                        rowData["MucKiemTra"],
                        rowData["AQLLevel"], rowData["SoLuongKiem"],
                        rowData["LoiChapNhan"], rowData["LoiPhatHien"],
                        rowData["KetQuaYes"], rowData["KetQuaNo"], rowData["ID"]);
                    GetDataTable(sqlQuery);
                }
                //MessageBox.Show("Success", "Message");
            }
            catch (Exception ex) { MessageBox.Show("Reason" + ex, "Error"); }
        }
        //Save phan noi dung kiem -> dbo.KetQuaKTSPNoiDung
        private void UpdateKetQuaKiemTraSanPhamNoiDung()
        {
            string ck11 = null;
            if (ck11No.Checked == false &&
            ck11Yes.Checked == false) { ck11 = ""; }
            else if (ck11No.Checked == true) { ck11 = "0"; }
            else if (ck11Yes.Checked == true) { ck11 = "1"; }

            string ck12 = null;
            if (ck12No.Checked == false &&
                ck12Yes.Checked == false) { ck12 = ""; }
            else if (ck12No.Checked == true) { ck12 = "0"; }
            else if (ck12Yes.Checked == true) { ck12 = "1"; }

            string ck13 = null;
            if (ck13No.Checked == false &&
                ck13Yes.Checked == false) { ck13 = ""; }
            else if (ck13No.Checked == true) { ck13 = "0"; }
            else if (ck13Yes.Checked == true) { ck13 = "1"; }

            string ck14 = null;
            if (ck14No.Checked == false &&
                ck14Yes.Checked == false) { ck14 = ""; }
            else if (ck14No.Checked == true) { ck14 = "0"; }
            else if (ck14Yes.Checked == true) { ck14 = "1"; }

            string ck21 = null;
            if (ck21No.Checked == false &&
                ck21Yes.Checked == false) { ck21 = ""; }
            else if (ck21No.Checked == true) { ck21 = "0"; }
            else if (ck21Yes.Checked == true) { ck21 = "1"; }

            string ck31 = null;
            if (ck31No.Checked == false &&
                ck31Yes.Checked == false) { ck31 = ""; }
            else if (ck31No.Checked == true) { ck31 = "0"; }
            else if (ck31Yes.Checked == true) { ck31 = "1"; }

            string ck32 = null;
            if (ck32No.Checked == false &&
                ck32Yes.Checked == false) { ck32 = ""; }
            else if (ck32No.Checked == true) { ck32 = "0"; }
            else if (ck32Yes.Checked == true) { ck32 = "1"; }

            string ck41 = null;
            if (ck41No.Checked == false &&
                ck41Yes.Checked == false) { ck41 = ""; }
            else if (ck41No.Checked == true) { ck41 = "0"; }
            else if (ck41Yes.Checked == true) { ck41 = "1"; }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("IDNoiDung", typeof(string));
            dataTable.Columns.Add("NoiDungKT", typeof(string));
            dataTable.Columns.Add("HangMuc", typeof(string));
            dataTable.Columns.Add("KetQuaChiTiet", typeof(string));
            dataTable.Rows.Add(lb11.Text, lbNoiDung11.Text, lbNgoaiQuan.Text, ck11);
            dataTable.Rows.Add(lb12.Text, lbNoiDung12.Text, lbNgoaiQuan.Text, ck12);
            dataTable.Rows.Add(lb13.Text, lbNoiDung13.Text, lbNgoaiQuan.Text, ck13);
            dataTable.Rows.Add(lb14.Text, lbNoiDung14.Text, lbNgoaiQuan.Text, ck14);
            dataTable.Rows.Add(lb21.Text, lbNoiDung21.Text, lbKichThuoc.Text, ck21);
            dataTable.Rows.Add(lb31.Text, lbNoiDung31.Text, lbVatLieu.Text, ck31);
            dataTable.Rows.Add(lb32.Text, lbNoiDung32.Text, lbVatLieu.Text, ck32);
            dataTable.Rows.Add(lb41.Text, txtNoiDung41.Text, lbLapRap.Text, ck41);
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                foreach (DataRow item in dataTable.Rows)
                {
                    string sqlQuery = string.Format(@"update KetQuaKTSPNoiDung 
                        set NoiDungKT= N'{0}',KetQuaChiTiet = '{1}' 
                        where IDKQKiemTra like '{2}' 
                        and IDNoiDung like '{3}'",
                        item["NoiDungKT"], item["KetQuaChiTiet"],
                        gridLookUpEditListCheck.Text, item["IDNoiDung"]);
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    cmd.ExecuteNonQuery();
                }
                //MessageBox.Show("Success", "Message");
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show("Reason" + ex, "Error"); }
        }
        //save phan kiem tra kich thuoc chi tiet ->[dbo].[KetQuaKTSPKTChiTiet]
        private void UpdateKetQuaKiemTraSanPhamKichThuocChiTiet()
        {
            try
            {
                for (int i = 0; i < gridViewKichThuocChiTiet.RowCount; i++)
                {
                    DataRow row = gridViewKichThuocChiTiet.GetDataRow(i);
                    string sqlQuery = string.Format(@"update KetQuaKTSPKTChiTiet 
                            set HangMuc=N'{0}',st1=N'{1}',
				                    nd2=N'{2}',rd3=N'{3}',
				                    th4=N'{4}',th5=N'{5}',
				                    th6=N'{6}',th7=N'{7}',
				                    th8=N'{8}',DoDayLopPhu=N'{9}' where ID like '{10}'",
                            row["HangMuc"], row["st1"],
                            row["nd2"], row["rd3"],
                            row["th4"], row["th5"],
                            row["th6"], row["th7"],
                            row["th8"], row["DoDayLopPhu"], row["ID"]);
                    GetDataTable(sqlQuery);
                }
                MessageBox.Show("Success", "Message");
            }
            catch (Exception ex) { MessageBox.Show("Reason" + ex, "Error"); }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            idKiemTra = gridLookUpEditListCheck.Text;
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"SELECT ID,IDKQKiemTra,HangMuc,st1,nd2,rd3,th4,
                th5,th6,th7,th8,th9,th10,DoDayLopPhu
                FROM KetQuaKTSPKTChiTiet where IDKQKiemTra like '{0}'", gridLookUpEditListCheck.Text);
            RpKetQuaKiemTraSanPham ketQuaKiemTraSanPham = new RpKetQuaKiemTraSanPham();
            ketQuaKiemTraSanPham.DataSource = Function.GetDataTable(sqlQuery);
            ketQuaKiemTraSanPham.DataMember = "Table";
            ketQuaKiemTraSanPham.CreateDocument(false);
            ketQuaKiemTraSanPham.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = gridLookUpEditListCheck.Text;
            PrintTool tool = new PrintTool(ketQuaKiemTraSanPham.PrintingSystem);
            ketQuaKiemTraSanPham.ShowPreviewDialog();
            //Function.Disconnect();
        }
        public static string idKiemTra;
        private void btn_Click(object sender, EventArgs e)
        {

        }
        //GetDataTable dùng chung
        private DataTable GetDataTable(string stringQuery)
        {
            Function.ConnectSanXuat();
            var dataTable = Function.GetDataTable(stringQuery);
            Function.Disconnect();
            return dataTable;
        }


        //formload
        private void frmKetQuaKiemTraSanPham_Load(object sender, EventArgs e)
        {
            ThDanhMucSanPhamXuatKho();//Thể hiện danh sách xuất kho vào gridlookupedit
            ThDanhMucKetQuaKiemTra();//Show List Kết quả kiểm tra vào gridlookupedit
            //
            ThChiTietKiem();//Thể hiện kích thước chi tiết
            ThKichThuocNgoaiQuan();//Thể hiện kiểm tra ngoại quan kích thước
            //refresh all checkbox
            ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = false;
            txtTenSanPham.Text = ""; txtMaSanPhamKhach.Text = ""; txtPO.Text = ""; txtPSX.Text = ""; txtCodeBB.Text = ""; txtTomTatKetQuaLoi.Text = ""; txtGhiChu.Text = "";
            ck11Yes.Checked = false; ck11No.Checked = false; ck11Yes.Checked = false; ck11No.Checked = false;
            ck12Yes.Checked = false; ck12No.Checked = false; ck12Yes.Checked = false; ck12No.Checked = false;

            ck13Yes.Checked = false; ck13No.Checked = false; ck13Yes.Checked = false; ck13No.Checked = false;
            ck14Yes.Checked = false; ck14No.Checked = false; ck14Yes.Checked = false; ck14No.Checked = false;

            ck21Yes.Checked = false; ck21No.Checked = false; ck21Yes.Checked = false; ck21No.Checked = false;
            ck31Yes.Checked = false; ck31No.Checked = false; ck31Yes.Checked = false; ck31No.Checked = false;

            ck32Yes.Checked = false; ck32No.Checked = false; ck32Yes.Checked = false; ck32No.Checked = false;
            ck41Yes.Checked = false; ck41No.Checked = false; ck41Yes.Checked = false; ck41No.Checked = false;

            ckKetQuaNo.Checked = false; ckKetQuaYes.Checked = false;
            ckKhacPhucNo.Checked = false; ckKhacPhucYes.Checked = false;
            ckTraVeNoiSanXuat.Checked = false; ckLuaChonKhac.Checked = false;
        }

        private void gridLookUpEditListView_EditValueChanged(object sender, EventArgs e)
        {
            if (gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                 gridLookUpEditViewListView.Columns["Num"]) != null)
            {
                txtPSX.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                gridLookUpEditViewListView.Columns["madh"]).ToString();

                txtPO.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                gridLookUpEditViewListView.Columns["MaPo"]).ToString();

                txtMaSanPhamKhach.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                gridLookUpEditViewListView.Columns["MaSP_Khach"]).ToString();

                txtCodeBB.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                gridLookUpEditViewListView.Columns["mabv"]).ToString();

                txtTenSanPham.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                gridLookUpEditViewListView.Columns["sanpham"]).ToString();

                string khachhang = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                gridLookUpEditViewListView.Columns["khachhang"]).ToString();
                dpNgayGiao.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
               gridLookUpEditViewListView.Columns["ngaynhan"]).ToString();

                txtSoLuongGiao.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
               gridLookUpEditViewListView.Columns["BTPT11"]).ToString().Trim();

                string soThung = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
                    gridLookUpEditViewListView.Columns["TongCongBaoBi"]).ToString();
                txtSoThungBich.Text = soThung.Trim();
            }
        }

        private void gridLookUpEditListCheck_EditValueChanged(object sender, EventArgs e)
        {
            RefreshCheckBox();//Làm mới các checkbox
            TraCuuKetQuaKTSP();//Tra cứu kết quả kiểm tra sản phẩm
            ThKetQuaKiemTraSanPhamTheoID();
            ThKetQuaKiemTraSanPhamChiTietTheoID();
            ThKetQuaKiemTraSanPhamNgoaiQuanTheoID();
            ThKetQuaKiemTraSanPhamNoiDungTheoID();
        }
        private void ThKetQuaKiemTraSanPhamTheoID()
        {
            //string strQuery = string.Format(@"SELECT ID,IDKQKiemTra,FinalCheck,
            //    MaSanPham,TenSanPham,NgayBHBV,PO,PhieuSanXuat,
            //    CodeBB,NgayGiao,SoLuongGiao,SoThungBich,
            //    TomTatKetQua,KetQuaKiem,HDKhacPhuc,PPXuLy,
            //    KQPPXuLy,NguoiLap,NgayLap,NguoiHC,
            //    NgayHC,ToXacNhan  FROM KetQuaKTSP where IDKQKiemTra like '{0}'", 
            //    gridLookUpEditListCheck.Text);
            //GetDataTable(strQuery);
        }
        private void ThKetQuaKiemTraSanPhamChiTietTheoID()
        {
            string strQuery = string.Format(@"SELECT ID,IDKQKiemTra,HangMuc,
                    st1,nd2,rd3,th4,
                    th5,th6,th7,th8,th9,th10,DoDayLopPhu
                    FROM KetQuaKTSPKTChiTiet
                    where IDKQKiemTra like '{0}'",
                    gridLookUpEditListCheck.Text);
            gridControlKichThuocChiTiet.DataSource = GetDataTable(strQuery);
        }
        private void ThKetQuaKiemTraSanPhamNgoaiQuanTheoID()
        {
            string strQuery = string.Format(@"SELECT ID,IDKQKiemTra,MucKiemTra,AQLLevel,
                SoLuongKiem,LoiChapNhan,LoiPhatHien,
				cast(KetQuaYes as bit)KetQuaYes,cast(KetQuaNo as bit)KetQuaNo
                FROM dbo.KetQuaKTSPNgoaiQuan where IDKQKiemTra like '{0}'",
                gridLookUpEditListCheck.Text);
            gridControlNgoaiQuan.DataSource = GetDataTable(strQuery);
        }
        private void ThKetQuaKiemTraSanPhamNoiDungTheoID()
        {
            string strQuery = string.Format(@"SELECT ID,IDNoiDung,NoiDungKT,HangMuc,
                IDKQKiemTra,KetQuaChiTiet FROM KetQuaKTSPNoiDung where IDKQKiemTra like '{0}'",
                gridLookUpEditListCheck.Text);
            DataTable dataTable = GetDataTable(strQuery);
            var xrl11 = dataTable.Select("IDNoiDung like '%" + lb11.Text + "%'");
            DataRow[] xrl12 = dataTable.Select("IDNoiDung like '%" + lb12.Text + "%'");
            DataRow[] xrl13 = dataTable.Select("IDNoiDung like '%" + lb13.Text + "%'");
            DataRow[] xrl14 = dataTable.Select("IDNoiDung like '%" + lb14.Text + "%'");
            DataRow[] xrl21 = dataTable.Select("IDNoiDung like '%" + lb21.Text + "%'");
            DataRow[] xrl31 = dataTable.Select("IDNoiDung like '%" + lb31.Text + "%'");
            DataRow[] xrl32 = dataTable.Select("IDNoiDung like '%" + lb32.Text + "%'");
            DataRow[] xrl41 = dataTable.Select("IDNoiDung like '%" + lb41.Text + "%'");
            //string a = xrl11[0]["KetQuaChiTiet"].ToString();
            if (xrl11[0]["KetQuaChiTiet"].ToString() == "") { ck11No.Checked = false; ck11Yes.Checked = false; }
            if (xrl11[0]["KetQuaChiTiet"].ToString() == "1") { ck11Yes.Checked = true; }
            if (xrl11[0]["KetQuaChiTiet"].ToString() == "0") { ck11No.Checked = true; }

            if (xrl12[0]["KetQuaChiTiet"].ToString() == "") { ck12No.Checked = false; ck12Yes.Checked = false; }
            if (xrl12[0]["KetQuaChiTiet"].ToString() == "1") { ck12Yes.Checked = true; }
            if (xrl12[0]["KetQuaChiTiet"].ToString() == "0") { ck12No.Checked = true; }

            if (xrl13[0]["KetQuaChiTiet"].ToString() == "") { ck13No.Checked = false; ck13Yes.Checked = false; }
            if (xrl13[0]["KetQuaChiTiet"].ToString() == "1") { ck13Yes.Checked = true; }
            if (xrl13[0]["KetQuaChiTiet"].ToString() == "0") { ck13No.Checked = true; }

            if (xrl14[0]["KetQuaChiTiet"].ToString() == "") { ck14No.Checked = false; ck14Yes.Checked = false; }
            if (xrl14[0]["KetQuaChiTiet"].ToString() == "1") { ck14Yes.Checked = true; }
            if (xrl14[0]["KetQuaChiTiet"].ToString() == "0") { ck14No.Checked = true; }

            if (xrl21[0]["KetQuaChiTiet"].ToString() == "") { ck21No.Checked = false; ck21Yes.Checked = false; }
            if (xrl21[0]["KetQuaChiTiet"].ToString() == "1") { ck21Yes.Checked = true; }
            if (xrl21[0]["KetQuaChiTiet"].ToString() == "0") { ck21No.Checked = true; }

            if (xrl31[0]["KetQuaChiTiet"].ToString() == "") { ck31No.Checked = false; ck31Yes.Checked = false; }
            if (xrl31[0]["KetQuaChiTiet"].ToString() == "1") { ck31Yes.Checked = true; }
            if (xrl31[0]["KetQuaChiTiet"].ToString() == "0") { ck31No.Checked = true; }

            if (xrl32[0]["KetQuaChiTiet"].ToString() == "") { ck32No.Checked = false; ck32Yes.Checked = false; }
            if (xrl32[0]["KetQuaChiTiet"].ToString() == "1") { ck32Yes.Checked = true; }
            if (xrl32[0]["KetQuaChiTiet"].ToString() == "0") { ck32No.Checked = true; }

            if (xrl41[0]["KetQuaChiTiet"].ToString() == "") { ck41No.Checked = false; ck41Yes.Checked = false; }
            if (xrl41[0]["KetQuaChiTiet"].ToString() == "1") { ck41Yes.Checked = true; }
            if (xrl41[0]["KetQuaChiTiet"].ToString() == "0") { ck41No.Checked = true; }


        }

        private void TraCuuKetQuaKTSP()
        {
            if (gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["IDKQKiemTra"]) != null)
            {
                string FinalCheck = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                gridLookUpEdit1ViewKetQuaKiemTra.Columns["FinalCheck"]).ToString();
                if (FinalCheck == "First Fty Final") { ckFinalFirst.Checked = true; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = false; }
                if (FinalCheck == "2nd Fty Final") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = true; ckInLine.Checked = false; ckPilot.Checked = false; }
                if (FinalCheck == "In-Line") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = true; ckPilot.Checked = false; }
                if (FinalCheck == "Pilot Run") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = true; }

                string MaSanPham = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["MaSanPham"]).ToString(); txtMaSanPhamKhach.Text = MaSanPham;

                string TenSanPham = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["TenSanPham"]).ToString(); txtTenSanPham.Text = TenSanPham;

                string NgayBHBV = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["NgayBHBV"]).ToString(); dpNgayBanHanhBanVe.Text = NgayBHBV;
                string PO = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["PO"]).ToString(); txtPO.Text = PO;

                string PhieuSanXuat = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["PhieuSanXuat"]).ToString(); txtPSX.Text = PhieuSanXuat;

                string CodeBB = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["CodeBB"]).ToString(); txtCodeBB.Text = CodeBB;

                string NgayGiao = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["NgayGiao"]).ToString(); dpNgayGiao.Text = NgayGiao;

                string SoLuongGiao = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["SoLuongGiao"]).ToString(); txtSoLuongGiao.Text = SoLuongGiao;

                string SoThungBich = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["SoThungBich"]).ToString(); txtSoThungBich.Text = SoThungBich;

                string TomTatKetQua = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["TomTatKetQua"]).ToString(); txtTomTatKetQuaLoi.Text = TomTatKetQua;
                //Ket qua xu ly va hanh dong khac phuc
                string KetQuaKiem = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["KetQuaKiem"]).ToString();
                if (KetQuaKiem == "") { ckKetQuaYes.Checked = false; ckKetQuaNo.Checked = false; }
                else if (KetQuaKiem == "1") { ckKetQuaYes.Checked = true; }
                else if (KetQuaKiem == "0") { ckKetQuaNo.Checked = true; };

                string HDKhacPhuc = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["HDKhacPhuc"]).ToString();
                if (HDKhacPhuc == "") { ckKhacPhucYes.Checked = false; ckKhacPhucNo.Checked = false; }
                else if (HDKhacPhuc == "1") { ckKhacPhucYes.Checked = true; }
                else if (HDKhacPhuc == "0") { ckKhacPhucNo.Checked = true; };

                string PPXuLy = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["PPXuLy"]).ToString();
                if (PPXuLy == "") { ckTraVeNoiSanXuat.Checked = false; ckLuaChonKhac.Checked = false; }
                else if (PPXuLy == "Trả về nơi sản xuất") { ckTraVeNoiSanXuat.Checked = true; }
                else if (PPXuLy == "Lựa chọn khác") { ckLuaChonKhac.Checked = true; };

                string KQPPXuLy = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["KQPPXuLy"]).ToString(); txtGhiChu.Text = KQPPXuLy;

                string ToXacNhan = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                gridLookUpEdit1ViewKetQuaKiemTra.Columns["ToXacNhan"]).ToString(); txtToXacNhan.Text = ToXacNhan;

                string NguoiLap = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["NguoiLap"]).ToString(); txtNguoiLap.Text = NguoiLap;

                string NgayLap = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["NgayLap"]).ToString(); dpNgayLap.Text = NgayLap;

                string NguoiHC = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["NguoiHC"]).ToString();

                string NgayHC = gridLookUpEdit1ViewKetQuaKiemTra.GetRowCellValue(gridLookUpEdit1ViewKetQuaKiemTra.FocusedRowHandle,
                 gridLookUpEdit1ViewKetQuaKiemTra.Columns["NgayHC"]).ToString();
            }
        }
        //Tạo mới
        private void btnCreate_Click(object sender, EventArgs e)
        {
            Refreshfrom();
        }
        private void Refreshfrom()
        {
            ThDanhMucSanPhamXuatKho();//Thể hiện danh sách xuất kho vào gridlookupedit
            ThDanhMucKetQuaKiemTra();//Show List Kết quả kiểm tra vào gridlookupedit
            //
            ThChiTietKiem();//Thể hiện kích thước chi tiết
            ThKichThuocNgoaiQuan();//Thể hiện kiểm tra ngoại quan kích thước
            //refresh all checkbox
            ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = false;
            txtTenSanPham.Text = ""; txtMaSanPhamKhach.Text = ""; txtPO.Text = ""; txtPSX.Text = ""; txtCodeBB.Text = ""; txtTomTatKetQuaLoi.Text = ""; txtGhiChu.Text = "";
            ck11Yes.Checked = false; ck11No.Checked = false; ck11Yes.Checked = false; ck11No.Checked = false;
            ck12Yes.Checked = false; ck12No.Checked = false; ck12Yes.Checked = false; ck12No.Checked = false;

            ck13Yes.Checked = false; ck13No.Checked = false; ck13Yes.Checked = false; ck13No.Checked = false;
            ck14Yes.Checked = false; ck14No.Checked = false; ck14Yes.Checked = false; ck14No.Checked = false;

            ck21Yes.Checked = false; ck21No.Checked = false; ck21Yes.Checked = false; ck21No.Checked = false;
            ck31Yes.Checked = false; ck31No.Checked = false; ck31Yes.Checked = false; ck31No.Checked = false;

            ck32Yes.Checked = false; ck32No.Checked = false; ck32Yes.Checked = false; ck32No.Checked = false;
            ck41Yes.Checked = false; ck41No.Checked = false; ck41Yes.Checked = false; ck41No.Checked = false;

            ckKetQuaNo.Checked = false; ckKetQuaYes.Checked = false;
            ckKhacPhucNo.Checked = false; ckKhacPhucYes.Checked = false;
            ckTraVeNoiSanXuat.Checked = false; ckLuaChonKhac.Checked = false;
        }
        private void RefreshCheckBox()
        {
            ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = false;
            txtTenSanPham.Text = ""; txtMaSanPhamKhach.Text = ""; txtPO.Text = ""; txtPSX.Text = ""; txtCodeBB.Text = ""; txtTomTatKetQuaLoi.Text = ""; txtGhiChu.Text = "";
            ck11Yes.Checked = false; ck11No.Checked = false; ck11Yes.Checked = false; ck11No.Checked = false;
            ck12Yes.Checked = false; ck12No.Checked = false; ck12Yes.Checked = false; ck12No.Checked = false;

            ck13Yes.Checked = false; ck13No.Checked = false; ck13Yes.Checked = false; ck13No.Checked = false;
            ck14Yes.Checked = false; ck14No.Checked = false; ck14Yes.Checked = false; ck14No.Checked = false;

            ck21Yes.Checked = false; ck21No.Checked = false; ck21Yes.Checked = false; ck21No.Checked = false;
            ck31Yes.Checked = false; ck31No.Checked = false; ck31Yes.Checked = false; ck31No.Checked = false;

            ck32Yes.Checked = false; ck32No.Checked = false; ck32Yes.Checked = false; ck32No.Checked = false;
            ck41Yes.Checked = false; ck41No.Checked = false; ck41Yes.Checked = false; ck41No.Checked = false;

            ckKetQuaNo.Checked = false; ckKetQuaYes.Checked = false;
            ckKhacPhucNo.Checked = false; ckKhacPhucYes.Checked = false;
            ckTraVeNoiSanXuat.Checked = false; ckLuaChonKhac.Checked = false;
        }

        private void gridViewNgoaiQuan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //int listRowList = gridViewNgoaiQuan.RowCount; DataRow rowData;
            //for (int i = 0; i < listRowList; i++)
            //{
            //    rowData = gridViewNgoaiQuan.GetDataRow(i);
            //    string sqlQuery = string.Format(@"",
            //        idkiemtra, rowData["MucKiemTra"],
            //        rowData["AQLLevel"], rowData["SoLuongKiem"],
            //        rowData["LoiChapNhan"], rowData["LoiPhatHien"],
            //        rowData["KetQuaYes"], rowData["KetQuaNo"]);
            //    GetDataTable(sqlQuery);
            //}
            //MessageBox.Show("Success", "Message");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteKetQuaKTSP();
            DeleteKetQuaKTSPNgoaiQuan();
            DeleteKetQuaKTSPNoiDung();
            DeleteKetQuaKTSPKichThuocChiTiet();
            ThDanhMucKetQuaKiemTra();
            Refreshfrom();
        }
        private void DeleteKetQuaKTSP()
        {
            string strQuery = string.Format(@"delete from KetQuaKTSP where IDKQKiemTra like '{0}'",
                gridLookUpEditListCheck.Text);
            GetDataTable(strQuery);
        }
        private void DeleteKetQuaKTSPNgoaiQuan()
        {
            string strQuery = string.Format(@"delete from KetQuaKTSPNgoaiQuan where IDKQKiemTra like '{0}'", 
                gridLookUpEditListCheck.Text); 
            GetDataTable(strQuery);
        }
        private void DeleteKetQuaKTSPNoiDung()
        {
            string strQuery = string.Format(@"delete from KetQuaKTSPNoiDung where IDKQKiemTra like '{0}'",
                gridLookUpEditListCheck.Text); 
            GetDataTable(strQuery);
        }
      
        private void DeleteKetQuaKTSPKichThuocChiTiet()
        {
            try { 
                string strQuery = string.Format(@"delete from KetQuaKTSPKTChiTiet where IDKQKiemTra like '{0}'", 
                gridLookUpEditListCheck.Text); 
                GetDataTable(strQuery);
                MessageBox.Show("Success","Message");
            }
            catch(Exception ex)
                 { MessageBox.Show("Reason"+ex,"Error"); }
        }
    }
}
