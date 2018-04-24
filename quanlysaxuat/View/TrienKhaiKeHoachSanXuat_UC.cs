using Dapper;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList.Nodes;
using quanlysanxuat.Model;
using quanlysanxuat.Report;
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
    public partial class TrienKhaiKeHoachSanXuat_UC : DevExpress.XtraEditors.XtraForm
    {
        public TrienKhaiKeHoachSanXuat_UC()
        {
            InitializeComponent();
        }

        private string user;
        #region formload
        private void frmPhanBoLichSanXuat_Load(object sender, EventArgs e)
        {
            this.user = Login.Username;
            dpTu.Text = DateTime.Today.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dplichkehoachTu.Text = DateTime.Today.ToString("01-MM-yyyy");
            dpkehoachden.Text = DateTime.Today.ToString("dd-MM-yyyy");

            dpNgayKetThucDuAn.Text = DateTime.Now.ToString("dd-MM-yyyy");
            gvOrderDetailfollowDate.Appearance.Row.Font = new Font("Segoe UI", 6.8f);
            ShowOrderCodefollowDate();
            user = Login.Username;
            ShowOrderDetailfollowDate();
            DocToThucHienSanXuat();//Đọc tổ thực hiện sản xuất
            DocMaCongDoan();//Đọc nguyên công sản xuất
            TheHienToThucHien();
            ShowStagesCode();
        }
        #endregion
        #region Đọc comBoBox Edit Tổ thực hiện sản xuất, Doc mã công đoạn
        private void DocToThucHienSanXuat()
        {
            //repositoryItemCBToThucHien.Items.Clear();
            //ketnoi kn = new ketnoi();
            //var dt = kn.laybang(@"select distinct Tenpb  from Admin where GiaoNhan = 1");
            //for (int i = 0; i <dt.Rows.Count; i++)
            //{
            //    repositoryItemCBToThucHien.Items.Add(dt.Rows[i]["Tenpb"]);
            //}
            //kn.dongketnoi();
        }
        private void DocMaCongDoan()
        {
            //repositoryItemCBMaCongDoan.Items.Clear();
            //repositoryItemCBMaCongDoan.NullText = @"Nguyên công";
            //ketnoi kn = new ketnoi();
            //var dt = kn.laybang(@"select Ten_Nguonluc,Ma_Nguonluc from tblResources");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    repositoryItemCBMaCongDoan.Items.Add(dt.Rows[i]["Ma_Nguonluc"]);
            //}
            //kn.dongketnoi();
        }
        #endregion
        private void ShowOrderCodefollowDate()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select madh from 
                tblDHCT where cast(thoigianthaydoi as Date) 
                between '{0}' and '{1}'
                group by madh,Code
                order by cast(Code as nvarchar) DESC",
                dpTu.Value.ToString("MM-dd-yyyy"),
                dpDen.Value.ToString("MM-dd-yyyy"));
            cbMaDonHang.DataSource = kn.laybang(sqlQuery);
            cbMaDonHang.DisplayMember = "madh";
            cbMaDonHang.ValueMember = "madh";
            kn.dongketnoi();
        }
        private void cbMaDonHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            DocChiTietDonHangTheoMaDonHang();
        }
        private void cbMaDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DocChiTietDonHangTheoMaDonHang();
        }

        private void btnDocChiTietDonHang_Click(object sender, EventArgs e)
        {
            ShowOrderDetailfollowDate();
        }
        private void ShowOrderDetailfollowDate()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select t.IDSanPham,case when t.Masp <>'' then 'x' end CoNguyenCong,
				case when k.IDChiTietDonHang <>'' then 'x' end DaLapPhanRa,c.*
				from tblDHCT c left outer join
				(select s.Masp,p.IDSanPham from
				(select IDSanPham from tblSanPhamTreeList group by IDSanPham)p
				inner join tblSANPHAM s on p.IDSanPham=s.Code)t
				on c.MaSP=t.Masp 
				left outer join
				(select IDChiTietDonHang from TrienKhaiKeHoachSanXuat group by IDChiTietDonHang)k
				on c.Iden=k.IDChiTietDonHang
				where thoigianthaydoi
				between '{0}' and '{1}' order by Iden DESC",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            grOrderDetailfollowDate.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void TheHienDanhSachTrienKhai()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select t.IDSanPham,case when t.Masp <>'' then 'x' end CoNguyenCong,
				case when k.IDChiTietDonHang <>'' then 'x' end DaLapPhanRa,c.*
				from tblDHCT c left outer join
				(select s.Masp,p.IDSanPham from
				(select IDSanPham from tblSanPhamTreeList group by IDSanPham)p
				inner join tblSANPHAM s on p.IDSanPham=s.Code)t
				on c.MaSP=t.Masp 
				left outer join
				(select IDChiTietDonHang from TrienKhaiKeHoachSanXuat group by IDChiTietDonHang)k
				on c.Iden=k.IDChiTietDonHang
				where madh like N'{0}'", madonhang);
            grOrderDetailfollowDate.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void DocChiTietDonHangTheoMaDonHang()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select t.IDSanPham,
                case when t.Masp <>'' then 'x' end CoNguyenCong,
				case when k.IDChiTietDonHang <>'' then 'x' end DaLapPhanRa,c.*
				from tblDHCT c left outer join
				(select s.Masp,p.IDSanPham from
				(select IDSanPham from tblSanPhamTreeList group by IDSanPham)p
				inner join tblSANPHAM s on p.IDSanPham=s.Code)t
				on c.MaSP=t.Masp 
				left outer join
				(select IDChiTietDonHang from TrienKhaiKeHoachSanXuat group by IDChiTietDonHang)k
				on c.Iden=k.IDChiTietDonHang
				where thoigianthaydoi
				where madh like N'{0}' order by Iden DESC",
                cbMaDonHang.Text);
            grOrderDetailfollowDate.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }

        private void btnCapNhatNgayTuDong_Click(object sender, EventArgs e)
        {
            GanNgayTuDongBatDauKetThuc();
        }
        private void GanNgayTuDongBatDauKetThuc()
        {
            //   ketnoi kn = new ketnoi();
            //   string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuat 
            //            set BatDau=l.BatDau,KetThuc=l.KetThuc 
            //from TrienKhaiKeHoachSanXuat t, 
            //(select ID,BatDau,KetThuc from LuyKeKeHoach_fun({0},'{1}'))l
            //where  t.ID=l.ID",
            //            txtIDChiTietDonHang.Text,
            //            dpNgayKetThucDuAn.Value.ToString("MM-dd-yyyy"));
            //   var kq = kn.xulydulieu(sqlQuery);
            //   kn.dongketnoi();
            //   TraCuuTrienKhaiKeHoachTheoMaSanPham();
        }

        #region tra cu tất cả trien khai ke hoach
        private void btnTraCuuTrienKhaiKeHoach_Click(object sender, EventArgs e)
        {
            TraCuuTatCaTrienKhaiKeHoach();
        }

        private void TraCuuTatCaTrienKhaiKeHoach()
        {
            //ketnoi kn = new ketnoi();
            //string sqlQuery = string.Format(@"SELECT *
            //    FROM  TrienKhaiKeHoachSanXuat");
            //grcTrienKhaiKeHoach.DataSource = kn.laybang(sqlQuery);
            //kn.dongketnoi();
            //grTrienKhaiKeHoach.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #endregion

        #region tra cưu trien khai ke hoach theo mã sản phẩm
        private void TraCuuTrienKhaiKeHoachTheoMaSanPham()
        {
            //ketnoi kn = new ketnoi();
            //string sqlQuery = string.Format(@"SELECT *
            //    FROM  TrienKhaiKeHoachSanXuat where IDChiTietDonHang like N'{0}'", txtIDChiTietDonHang.Text);
            //grcTrienKhaiKeHoach.DataSource = kn.laybang(sqlQuery);
            //kn.dongketnoi();
            //grTrienKhaiKeHoach.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        //SoThuTu,ID,MaDonHang,MaSanPham,
        //        TenSanPham,SoDonHang,DonViSanPham,MaChiTiet,
        //        TenChiTiet,DonViChiTiet,SoChiTietSanPham,SoLuongChiTietDonHang,TonKho, 
        //        SoLuongYCSanXuat,NgayLap,NguoiLap,
        //        NgayHieuChinh,IDDonHang,IDChiTietDonHang,IDChiTietSanPham,ToThucHien,
        //        BatDau,KetThuc,CongSuatChuyenGio,CongSuatChuyenNgay
        #endregion
        private string randonpoint;
        private void RanDonPoint()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(@"select Top 1
                        REPLACE(convert(nvarchar, GetDate(), 11), '/', '')
                      + replace(replace(left(CONVERT(time, GetDate()), 12), ':', ''), '.', '')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                this.randonpoint = Convert.ToString(reader[0]);
            reader.Close();
        }
        //Tạo 1 dãy số tự nhiên 15 chữ số không trung nhau
        //mỗi lần ghi + vào parentid,id mục đích tạo cây thư mục không trùng nhau ở mỗi lần ghi
        //cùng sản phẩm của đơn hàng sau này, hoặc 1 sản phẩm chia làm 2 lần sản xuất 
        private void SaveTreeListNodeProductionStagesPlan()
        {
            RanDonPoint();
            List<TreeListNode> list = treeListProductionStages.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                double id = double.Parse(node.GetDisplayText(treeListStagesID)) + double.Parse(this.randonpoint);
                double parentid = double.Parse(node.GetDisplayText(treeListStagesParentID)) + double.Parse(this.randonpoint);
                string MaSanPham = node.GetDisplayText(treeListStagesMaLoai);
                string TenSanPham = node.GetDisplayText(treeListStagesTenLoai);
                string SoChiTietSanPham = node.GetDisplayText(treeListStagesSoChiTiet);
                string DonViSanPham = cbInventoryUnit.Text;
            
                string ToThucHien = node.GetDisplayText(treeListStagesToThucHien);
                string MaCongDoan = node.GetDisplayText(treeListStagesMaCongDoan);

                string DinhMuc = node.GetDisplayText(treeListStagesDinhMuc);
                string SoThuTu = node.GetDisplayText(treeListStagesThuTu);

                string strQuery = string.Format(
                    @"insert into TrienKhaiKeHoachSanXuat
                        (ID,ParentID,
                         IDDonHang,IDChiTietDonHang,
                         IDChiTietSanPham,MaDonHang,
                         MaSanPham,TenSanPham,SoLuongDonHang,
                         DonViSanPham,MaLoai,TenLoai,SoLuongLoai,
                         DonViChiTiet,SoLuongChiTietDonHang,
                         TonKho,SoLuongYCSanXuat,ToThucHien,
                         MaCongDoan,TenCongDoan,
                         DinhMuc,SoThuTu,NgayLap,NguoiLap,IDSanPham,
                         MaChiTiet,TenChiTiet,SoChiTiet,MucDo,
                         MaPo,NgoaiQuan,GhiChu,KhoGiaoHang,RandonPoint) values 
                       ('{0}','{1}','{2}','{3}',
                        '{4}',N'{5}',N'{6}',N'{7}','{8}',
                       N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',
                        '{14}','{15}','{16}',N'{17}',N'{18}',
                       N'{19}','{20}',N'{21}','{22}',N'{23}',
                        '{24}',N'{25}',N'{26}',N'{27}','{28}',
                       N'{29}',N'{30}',N'{31}',N'{32}','{33}')",
                        id,
                        parentid,
                        idorder, iddetailorder,
                        node.GetDisplayText(treeListStagesID),
                        cbMaDonHang.Text,
                        txtMaSanPham.Text,
                        tenquycach, soluongdathang,
                        node.GetDisplayText(treeListStagesDonVi), MaSanPham, TenSanPham,
                        Convert.ToDouble(SoChiTietSanPham), node.GetDisplayText(treeListStagesDonVi),
                        (Convert.ToDouble(soluongdathang) * Convert.ToDouble(SoChiTietSanPham)),
                        Convert.ToDouble(txtTonKhoThanhPham.Text) * Convert.ToDouble(SoChiTietSanPham),
                        (Convert.ToDouble(soluongdathang) - Convert.ToDouble(txtTonKhoThanhPham.Text)) * Convert.ToDouble(SoChiTietSanPham),
                        ToThucHien,
                        MaCongDoan, TenSanPham,
                        DinhMuc, SoThuTu,
                        dpNgayGhi.Value.ToString("yyyy-MM-dd"),
                        Login.Username, idproduction,
                        node.GetDisplayText(treeListStagesMaChiTiet),
                        node.GetDisplayText(treeListStagesTenChiTiet),
                        node.GetDisplayText(treeListStagesSoChiTiet),
                        node.GetDisplayText(treeListStagesMucDo),
                        txtMaPo.Text, txtNgoaiQuan.Text, 
                        txtGhiChu.Text, txtKhoGiao.Text,
                        this.randonpoint);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            ShowTreeListStagesPlanfollowProductionID();
            TheHienDanhSachTrienKhai();
        }
        #region tinh cong suat chuyen ngay
        private void TinhCongSuatChuyenTrenNgay()
        {
            //        ketnoi kn = new ketnoi();
            //        string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuat 
            //set CongSuatChuyenNgay = SoLuongYCSanXuat/(CongSuatChuyenGio*8)
            //where IDChiTietDonHang like '{0}'", txtIDChiTietDonHang.Text);
            //        var kq = kn.xulydulieu(sqlQuery);
            //        kn.dongketnoi();
        }
        #endregion
        private void grTrienKhaiKeHoach_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        { }
        //    private void CapNhatTonKhoChiTiet()
        //    {

        //        SqlConnection con = new SqlConnection();
        //        con.ConnectionString = Connect.mConnect;
        //        int[] listRowList = grTrienKhaiKeHoach.GetSelectedRows();
        //        if (con.State == ConnectionState.Closed) con.Open();
        //        for (int i = 0; i < grTrienKhaiKeHoach.DataRowCount; i++)
        //        {
        //            var batdau = Convert.ToDateTime(rowData["BatDau"]).ToString("yyyy-MM-dd");
        //            var ketthuc = Convert.ToDateTime(rowData["KetThuc"]).ToString("yyyy-MM-dd");
        //            string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuat set
        //TonKho = '{0}',SoLuongYCSanXuat = SoLuongChiTietDonHang - '{0}',DonViChiTiet=N'{2}',SoThuTu='{3}',BatDau='{4}',KetThuc='{5}'
        //where ID like '{6}'",
        //            rowData["TonKho"],
        //            rowData["SoLuongYCSanXuat"],
        //            rowData["DonViChiTiet"],
        //            rowData["SoThuTu"],
        //            batdau,
        //            ketthuc,
        //            rowData["ID"]);
        //            SqlCommand cmd = new SqlCommand(strQuery, con);
        //            cmd.ExecuteNonQuery();
        //        }
        //        TinhCongSuatChuyenTrenNgay();
        //        TraCuuTrienKhaiKeHoachTheoMaSanPham();
        //    }
        #region Điều chỉnh lại ngày bắt đầu hoặc kết thúc DateAdd(DD,CongSuatChuyenNgay,'{2}')
        private void EditTreeListNodeProductionStagesPlan()
        {
            List<TreeListNode> list = treeListProductionStagesPlan.GetAllCheckedNodes();
            Function.ConnectSanXuat();//Mo ket noi
            foreach (TreeListNode node in list)
            {
                string MaCongDoan = node.GetDisplayText(treeListPlanMaCongDoan);
                string ToThucHien = node.GetDisplayText(treeListPlanToThucHien);
                string SoThuTu = node.GetDisplayText(treeListPlanSoThuTu) == "" ? "0" : node.GetDisplayText(treeListPlanSoThuTu);
                string BatDau = node.GetDisplayText(treeListPlanDateStar) == "" ? "0" : node.GetDisplayText(treeListPlanDateStar);
                string KetThuc = node.GetDisplayText(treeListPlanDateEnd) == "" ? "0" : node.GetDisplayText(treeListPlanDateEnd);
                //string SoLuongDonHang = node.GetDisplayText(treeListPlanSoLuongDonHang);
                string SoLuongLoai = node.GetDisplayText(treeListPlanSoLuongLoai);
                string TonKho = node.GetDisplayText(treeListPlanTonKho);
                //string SoLuongYCSanXuat = node.GetDisplayText(treeListPlanSoLuongCanSanXuat);
                string DonViChiTiet = node.GetDisplayText(treeListPlanDonViChiTiet);
                string DonViSanPham = node.GetDisplayText(treeListPlanDonViSanPham);
                string NguoiLap = Login.Username;
                DateTime NgayHieuChinh = Convert.ToDateTime(node.GetDisplayText(treeListPlanNgayLap));
                string IDTrienKhai = node.GetDisplayText(treeListPlanIDTrienKhai);
                string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuat set
                        MaCongDoan=N'{0}',ToThucHien=N'{1}',SoThuTu='{2}',BatDau='{3}',
                        KetThuc='{4}',SoLuongLoai='{5}',SoChiTiet='{5}',
                        TonKho='{6}',DonViChiTiet=N'{7}',DonViSanPham=N'{8}',
                        NguoiLap=N'{9}',NgoaiQuan=N'{10}',GhiChu=N'{11}',
                        NgayHieuChinh=GetDate(),TenLoai=N'{12}',
                        NgayLap='{13}',SoLuongDonHang='{14}',TenChiTiet=N'{15}'
                        where IDTrienKhai like '{16}'",
                      node.GetDisplayText(treeListPlanMaCongDoan), node.GetDisplayText(treeListPlanToThucHien),
                      double.Parse(node.GetDisplayText(treeListPlanSoThuTu)),
                      node.GetDisplayText(treeListPlanDateStar) == "" ? "" :
                      Convert.ToDateTime(node.GetDisplayText(treeListPlanDateStar)).ToString("MM-dd-yyyy"),
                      node.GetDisplayText(treeListPlanDateEnd) == "" ? "" :
                      Convert.ToDateTime(node.GetDisplayText(treeListPlanDateEnd)).ToString("MM-dd-yyyy"),
                      double.Parse(node.GetDisplayText(treeListPlanSoChiTiet)),
                      double.Parse(node.GetDisplayText(treeListPlanTonKho)),
                      node.GetDisplayText(treeListPlanDonViChiTiet),
                      node.GetDisplayText(treeListPlanDonViSanPham),
                      Login.Username, node.GetDisplayText(treeListPlanNgoaiQuan),
                      node.GetDisplayText(treeListPlanGhiChu),
                      node.GetDisplayText(treeListPlanTenLoai),
                      dpNgayCapNhat.Value.ToString("yyyy-MM-dd"),
                      double.Parse(node.GetDisplayText(treeListPlanSoLuongDonHang)),
                      node.GetDisplayText(treeListPlanTenChiTiet),
                      IDTrienKhai);
                var dt = Function.GetDataTable(strQuery);
            }
            Function.Disconnect();//dong ket noi
            TruTonKho();//Tru ton kho
            CapNhatNgayMinMax();//Cập nhật ngày min, max cho sản phẩm vừa ghi các ngày
            ShowTreeListStagesPlanfollowProductionID();
        }
        private void TruTonKho()
        {
            List<TreeListNode> list = treeListProductionStagesPlan.GetAllCheckedNodes();
            Function.ConnectSanXuat();//Mo ket noi
            foreach (TreeListNode node in list)
            {
                string IDTrienKhai = node.GetDisplayText(treeListPlanIDTrienKhai);
                string strQuery = string.Format(
                      @"update TrienKhaiKeHoachSanXuat 
                        set SoLuongChiTietDonHang=SoLuongDonHang*SoChiTiet,
                        SoLuongYCSanXuat=(SoLuongDonHang*SoChiTiet)-TonKho
                        where IDTrienKhai like '{0}'", IDTrienKhai);
                var dt = Function.GetDataTable(strQuery);
            }
            Function.Disconnect();//dong ket noi
        }
        #endregion
        //Sau khi cap nhat thoi gian cho cac chi tiet thi cap nhat ngay Min cho ngay bat dau va max cho ngay ket thuc
        private void CapNhatNgayMinMax()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuat 
				set BatDau=g.BatDau,KetThuc=g.KetThuc from 
				TrienKhaiKeHoachSanXuat t inner join
				(select IDChiTietDonHang,ParentID,GhiChu,
				Min(IDTrienKhai)IDTrienKhai,
				Min(BatDau)BatDau,Max(KetThuc)KetThuc
				from TrienKhaiKeHoachSanXuat where 
				BatDau is not null and BatDau <> '1900-01-01' 
				and KetThuc is not null and KetThuc <> '1900-01-01'
				group by IDChiTietDonHang,ParentID,GhiChu)g
				on t.ID=g.ParentID 
				where t.IDChiTietDonHang like '{0}'", IDChiTietDonHang);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void CapNhatThoiGianSanXuat()
        {

        }
        private void DeleteTreeListNodeProductionStagesPlan()
        {
            List<TreeListNode> list = treeListProductionStagesPlan.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                      @"if NOT EXISTS (select 1 from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet where IDTrienKhai = {0})
					    begin
					    delete from 
                            TrienKhaiKeHoachSanXuat 
                            where IDTrienKhai like '{0}'
					    end",
                        node.GetDisplayText(treeListPlanIDTrienKhai));
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            ShowTreeListStagesPlanfollowProductionID();
        }
        private string idproduction;//idproduction
        private string idorder;//IDDonHang
        private string iddetailorder;//IDChiTietDonHang
        private string madonhang;//mã đơn hàng
        private string masanpham;//mã sản phẩm
        private string tenquycach;//Ten quy cách sản phẩm
        private string soluongdathang;//số lượng đặt hàng
        private string donvi;//Đơn vị sản phẩm
        private string dayend;

        private void grcChiTietDonHang_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gvOrderDetailfollowDate.GetFocusedDisplayText();
            idproduction = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(idproduction_);
            idorder = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(idOrder_);
            iddetailorder = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(idChiTietDonHang_);
            madonhang = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(madonhang_);
            cbMaDonHang.Text = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(madonhang_);
            masanpham = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(maSanPham_);
            tenquycach = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(tenSanPham_);
            soluongdathang = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(soluong_);
            donvi = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(donvi_);
            dayend = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(ngaygiao_);

            txtNgoaiQuan.Text = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(ngoaiquan_);
            txtGhiChu.Text = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(ghichu_);
            txtMaSanPham.Text = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(maSanPham_);
            txtMaPo.Text = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(poOrder_);
            cbInventoryUnit.Text = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(donvi_);
            txtKhoGiao.Text = gvOrderDetailfollowDate.GetFocusedRowCellDisplayText(khogiao_);
            ShowTreeListStagesfollowProductionCode();//thể hiện chi tiết sản phẩm đã phân cụm theo mã sản phẩm
            //ShowTreeListStagesPlanfollowProductionID();//thể hiện danh sách sản phẩm đã lập kế hoạch theo id chi tiết đơn hàng
            //TheHienKeHoachTheoMaDonHang();//thể hiện danh sách sản phẩm
        }
        private void treeListProductionStages_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }
        private void ShowTreeListStagesfollowProductionCode()
        {
            if (ckGiuKhongThayDoiChiTiet.Checked == true)
            {
                return;
            }
            else
            {
                treeListProductionStages.Appearance.Row.Font = new Font("Segoe UI", 6.8f);
                ketnoi kn = new ketnoi();
                string sqlQuery = string.Format(@"select * from tblSanPhamTreeList where IDSanPham like '{0}'", idproduction);
                treeListProductionStages.DataSource =
                    kn.laybang(sqlQuery);
                treeListProductionStages.ForceInitialize();
                treeListProductionStages.ExpandAll();
                //treeListProductionStages.BestFitColumns();
                treeListProductionStages.OptionsSelection.MultiSelect = true;
                kn.dongketnoi();
            }
        }

        private async void ShowTreeListStagesPlan()
        {
            //ketnoi kn = new ketnoi();
            //string sqlQuery = string.Format(@"",
            //    dpTu.Value.ToString("yyyy-MM-dd"),
            //    dpDen.Value.ToString("yyyy-MM-dd"));
            //treeListProductionStagesPlan.DataSource = kn.laybang(sqlQuery);
            //treeListProductionStagesPlan.ForceInitialize();
            //treeListProductionStagesPlan.ExpandAll();
            //treeListProductionStagesPlan.OptionsSelection.MultiSelect = true;
            //treeListProductionStagesPlan.Appearance.Row.Font = new Font("Segoe UI", 7f);
            //kn.dongketnoi();
            //treeListProductionStagesPlan.BestFitColumns();
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select b.SoLuongGiao,b.NgayGiao,
                 case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
                 when GetDate()<BatDau and isnull(b.SoLuongGiao,0) < 1 then N'Chưa khởi động'
                 when cast(GetDate() as date)>= KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
                 when GetDate()<KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
				 end TinhTrang,a.* from
				(SELECT RandonPoint,ID,ParentID,IDTrienKhai,IDDonHang,IDChiTietDonHang,
                IDChiTietSanPham,IDSanPham,
                MaDonHang,MaPo,MaSanPham,
                TenSanPham,MaLoai,
                TenLoai,SoLuongLoai,SoLuongDonHang,
                DonViSanPham,MaLapGhep,
                DonViChiTiet,SoLuongChiTietDonHang,
                TonKho,SoLuongYCSanXuat,ToThucHien,
                BatDau,KetThuc,MaCongDoan,
                TenCongDoan,DinhMuc,CongSuatChuyenNgay,
                SoThuTu,NgayLap,NguoiLap,NgayHieuChinh,
                CongSuatChuyenGio,NgayGiao,
                SoLuongGiao,ToNhan,NgayNhan,
                SoNhan,HuHongThatLac,NguyenNhanSoBienBan,
                MucDo,DienGiaiCongDoan,SoLanCongDoan,
                MaChiTiet,TenChiTiet,SoChiTiet,
                NgoaiQuan,GhiChu,KhoGiaoHang,
                TinhTrang,UuTien,TinhTrangNgung,
                DonGiaKhoanTo,SoLuongTinhGiaKhoan,ThanhTienKhoanTo
                FROM dbo.TrienKhaiKeHoachSanXuat
				where NgayLap between '{0}' and '{1}')a 
				left outer join
				(select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
			    group by IDTrienKhai)b
			    on a.IDTrienKhai=b.IDTrienKhai
				order by NgayLap desc",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    treeListProductionStagesPlan.DataSource = Model.Function.GetDataTable(sqlQuery);
                    treeListProductionStagesPlan.ForceInitialize();
                    treeListProductionStagesPlan.ExpandAll();
                    treeListProductionStagesPlan.OptionsSelection.MultiSelect = true;
                    treeListProductionStagesPlan.Appearance.Row.Font = new Font("Segoe UI", 7f);
                }));
            });
        }
        private void ShowTreeListStagesPlanfollowProductionID()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select b.SoLuongGiao,b.NgayGiao,
                 case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
                 when GetDate()<BatDau and isnull(b.SoLuongGiao,0) < 1 then N'Chưa khởi động'
                 when cast(GetDate() as date)>= KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
                 when GetDate()<KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
				 end TinhTrang,a.* from
				(SELECT RandonPoint,ID,ParentID,IDTrienKhai,IDDonHang,IDChiTietDonHang,
                IDChiTietSanPham,IDSanPham,
                MaDonHang,MaPo,MaSanPham,
                TenSanPham,MaLoai,
                TenLoai,SoLuongLoai,SoLuongDonHang,
                DonViSanPham,MaLapGhep,
                DonViChiTiet,SoLuongChiTietDonHang,
                TonKho,SoLuongYCSanXuat,ToThucHien,
                BatDau,KetThuc,MaCongDoan,
                TenCongDoan,DinhMuc,CongSuatChuyenNgay,
                SoThuTu,NgayLap,NguoiLap,NgayHieuChinh,
                CongSuatChuyenGio,NgayGiao,
                SoLuongGiao,ToNhan,NgayNhan,
                SoNhan,HuHongThatLac,NguyenNhanSoBienBan,
                MucDo,DienGiaiCongDoan,SoLanCongDoan,
                MaChiTiet,TenChiTiet,SoChiTiet,
                NgoaiQuan,GhiChu,KhoGiaoHang,
                TinhTrang,UuTien,TinhTrangNgung,
                DonGiaKhoanTo,SoLuongTinhGiaKhoan,ThanhTienKhoanTo
                FROM dbo.TrienKhaiKeHoachSanXuat)a 
				left outer join
				(select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
			    group by IDTrienKhai)b
			    on a.IDTrienKhai=b.IDTrienKhai
				where IDChiTietDonHang like '{0}'",
                iddetailorder);
            treeListProductionStagesPlan.DataSource =
                kn.laybang(sqlQuery);
            treeListProductionStagesPlan.ForceInitialize();
            treeListProductionStagesPlan.ExpandAll();
            //treeListProductionStagesPlan.BestFitColumns();
            treeListProductionStagesPlan.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
            treeListProductionStagesPlan.Appearance.Row.Font = new Font("Segoe UI", 7f);
        }
        private void TheHienDonHangDaTrienKhai()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select b.SoLuongGiao,b.NgayGiao,
                 case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
                 when GetDate()<BatDau and isnull(b.SoLuongGiao,0) < 1 then N'Chưa khởi động'
                 when cast(GetDate() as date)>= KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
                 when GetDate()<KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
				 end TinhTrang,a.* from
				(SELECT RandonPoint,ID,ParentID,IDTrienKhai,IDDonHang,IDChiTietDonHang,
                IDChiTietSanPham,IDSanPham,
                MaDonHang,MaPo,MaSanPham,
                TenSanPham,MaLoai,
                TenLoai,SoLuongLoai,SoLuongDonHang,
                DonViSanPham,MaLapGhep,
                DonViChiTiet,SoLuongChiTietDonHang,
                TonKho,SoLuongYCSanXuat,ToThucHien,
                BatDau,KetThuc,MaCongDoan,
                TenCongDoan,DinhMuc,CongSuatChuyenNgay,
                SoThuTu,NgayLap,NguoiLap,NgayHieuChinh,
                CongSuatChuyenGio,NgayGiao,
                SoLuongGiao,ToNhan,NgayNhan,
                SoNhan,HuHongThatLac,NguyenNhanSoBienBan,
                MucDo,DienGiaiCongDoan,SoLanCongDoan,
                MaChiTiet,TenChiTiet,SoChiTiet,
                NgoaiQuan,GhiChu,KhoGiaoHang,
                TinhTrang,UuTien,TinhTrangNgung,
                DonGiaKhoanTo,SoLuongTinhGiaKhoan,ThanhTienKhoanTo
                FROM dbo.TrienKhaiKeHoachSanXuat)a 
				left outer join
				(select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
			    group by IDTrienKhai)b
			    on a.IDTrienKhai=b.IDTrienKhai
				where MaDonHang like N'{0}'", madonhang);
            treeListProductionStagesPlan.DataSource =
                kn.laybang(sqlQuery);
            treeListProductionStagesPlan.ForceInitialize();
            treeListProductionStagesPlan.ExpandAll();
            //treeListProductionStagesPlan.BestFitColumns();
            treeListProductionStagesPlan.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
            treeListProductionStagesPlan.Appearance.Row.Font = new Font("Segoe UI", 7f);
        }
        private void ShowckChiTietCongDoan()
        {
            if (txtMaSanPham.Text != "")
            {
                ckChiTietCongDoan.Visible = true;
            }
            else
            {
                ckChiTietCongDoan.Visible = false;
            }
        }
        #region thể hiện nguyên công chi tiết sản phẩm sản xuất
        private void DocNguyenCongChiTietSanPham()
        {
            // ketnoi kn = new ketnoi();
            // string sqlQuery = string.Format(@"select MaLapGhep,CongSuatChuyen,DonViChiTiet,
            //     id,MaChiTiet,ChiTietSanPham,SoChiTietSanPham,
            //Tencondoan,NguyenCong,ThuTuCongDoan,Tothuchien,Dinhmuc,
            //'{0}'*SoChiTietSanPham SoLuongChiTietDonHang,'{1}'TonKho,
            //     case when {0}*SoChiTietSanPham < {1} then 0 else {0}*SoChiTietSanPham - {1} end  SoLuongYCSanXuat,
            //Nguoilap,Ngaylap
            //from ChiTietSanPhamDinhMuc where Masp like N'{2}' and SoChiTietSanPham >=1",
            // txtSoLuong.Text, txtTonKhoThanhPham.Text, txtMaSanPham.Text);
            // grcChiTietSanPham.DataSource = kn.laybang(sqlQuery);
            // kn.dongketnoi();
            // grChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #endregion

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            ShowOrderCodefollowDate();
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            EditTreeListNodeProductionStagesPlan();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {


        }

        private void grcChiTietSanPham_MouseMove(object sender, MouseEventArgs e)
        {
            //if (grChiTietSanPham.SelectedRowsCount >= 1)
            //{
            //    btnGhi.Enabled = true;
            //}
            //else { btnGhi.Enabled = false; }
        }

        private void grcTrienKhaiKeHoach_MouseMove(object sender, MouseEventArgs e)
        {
            //if (grTrienKhaiKeHoach.SelectedRowsCount >= 1)
            //{
            //    btnSua.Enabled = true;
            //    btnXoa.Enabled = true;
            //}
            //else
            //{
            //    btnSua.Enabled = false;
            //    btnXoa.Enabled = false;
            //}
        }

        private void txtTonKhoThanhPham_TextChanged(object sender, EventArgs e)
        {
            checkChiTietNguyenCongSanPham();
        }

        private void grcTrienKhaiKeHoach_Click(object sender, EventArgs e)
        {
            //string point = "";
            //point = grTrienKhaiKeHoach.GetFocusedDisplayText();
            //txtMaSanPham.Text = grTrienKhaiKeHoach.GetFocusedRowCellDisplayText(masanpham);
            //txtSanPham.Text = grTrienKhaiKeHoach.GetFocusedRowCellDisplayText(tensanpham);
            //txtDonViSanPham.Text = grTrienKhaiKeHoach.GetFocusedRowCellDisplayText(donvisanpham);
            //txtSoLuong.Text= grTrienKhaiKeHoach.GetFocusedRowCellDisplayText(soluongdonhang);
            //txtIDChiTietDonHang.Text = grTrienKhaiKeHoach.GetFocusedRowCellDisplayText(idChiTietDonHang);
            //txtIDDonHang.Text= grTrienKhaiKeHoach.GetFocusedRowCellDisplayText(IDDonHang);
        }

        private void btnUngroup_Click(object sender, EventArgs e)
        {
            frmTinh_nguon_luc_san_xuat tinh_Nguon_Luc_San_Xuat = new frmTinh_nguon_luc_san_xuat();
            tinh_Nguon_Luc_San_Xuat.Show();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from ViewLichSanXuatChiTiet where MucDo <>0 and MaDonHang like N'" + cbMaDonHang.Text + "'");
            XRLapLichSanXuat rplich = new XRLapLichSanXuat();
            rplich.DataSource = dt;
            rplich.DataMember = "Table";
            rplich.CreateDocument(false);
            rplich.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = madonhang;
            PrintTool tool = new PrintTool(rplich.PrintingSystem);
            rplich.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(masanpham, path.pathbanve);
            f2.Show();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            DocDinhMucSanPham();
        }
        private void DocDinhMucSanPham()
        {
            //ketnoi kn = new ketnoi();
            //string sqlQuery = string.Format(@"select MaLapGhep,'' CongSuatChuyen ,'' DonViChiTiet,
            //    id,'' MaChiTiet,ChiTietSanPham,SoChiTietSanPham,
            //Tencondoan,NguyenCong,ThuTuCongDoan,Tothuchien,Dinhmuc,
            //'{0}'*SoChiTietSanPham SoLuongChiTietDonHang,'{1}'TonKho,
            //    case when {0}*SoChiTietSanPham < {1} then 0 else {0}*SoChiTietSanPham - {1} end  SoLuongYCSanXuat,
            //Nguoilap,Ngaylap
            //from ChiTietSanPhamDinhMuc where Masp like N'{2}'",
            //txtSoLuong.Text, txtTonKhoThanhPham.Text, txtMaSanPham.Text);
            //grcChiTietSanPham.DataSource = kn.laybang(sqlQuery);
            //kn.dongketnoi();
            //grChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        private void ckChiTietCongDoan_CheckedChanged(object sender, EventArgs e)
        {
            checkChiTietNguyenCongSanPham();
        }
        private void checkChiTietNguyenCongSanPham()
        {
            if (ckChiTietCongDoan.Checked == true)
            {
                DocChiTietNguyenCongChiTietSanPham();
            }
            else if (ckChiTietCongDoan.Checked == false)
            {
                DocNguyenCongChiTietSanPham();
            }
        }
        #region đọc chi tiết công đoạn sản xuất

        private void DocChiTietNguyenCongChiTietSanPham()
        {
            //        ketnoi kn = new ketnoi();
            //        string sqlQuery = string.Format(@"select a.* from
            //(select max(id)id,Masp,
            //max(ThuTuCongDoan)ThuTuCongDoan,max(ChiTietSanPham)ChiTietSanPham,
            //max(CongSuatChuyen)CongSuatChuyen,NguyenCong  
            //from ChiTietSanPhamDinhMuc where 
            //SoChiTietSanPham >0 
            //and ThuTuCongDoan >0 and NguyenCong<>''
            //and Masp like N'{2}'
            //group by NguyenCong,Masp)b
            //left outer join (select MaLapGhep,CongSuatChuyen,DonViChiTiet,
            //            id,MaChiTiet,ChiTietSanPham,SoChiTietSanPham,
            //Tencondoan,NguyenCong,ThuTuCongDoan,Tothuchien,Dinhmuc,
            //'{0}'*SoChiTietSanPham SoLuongChiTietDonHang,'{1}'TonKho,
            //            case when {0}*SoChiTietSanPham < {1} then 0 else {0}*SoChiTietSanPham - {1} end  SoLuongYCSanXuat,
            //Nguoilap,Ngaylap
            //from ChiTietSanPhamDinhMuc where SoChiTietSanPham >=1) a
            //on b.id=a.id",
            //        txtSoLuong.Text, txtTonKhoThanhPham.Text, txtMaSanPham.Text);
            //        grcChiTietSanPham.DataSource = kn.laybang(sqlQuery);
            //        kn.dongketnoi();
            //        grChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #endregion

        private void btnShowOrderDetailfollowDay_Click(object sender, EventArgs e)
        {
            ShowOrderDetailfollowDate();
        }

        #region sự kiện checkedchildNode đồng thời checkedParentNode
        private bool OneOfChildsIsChecked(TreeListNode node)
        {
            bool result = false;
            foreach (TreeListNode item in node.Nodes)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    result = true;
                }
            }
            return result;
        }
        private void treeListProductionStages_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node.Checked)
            {
                node.UncheckAll();
            }
            else
            {
                node.CheckAll();
            }
            while (node.ParentNode != null)
            {
                node = node.ParentNode;
                bool oneOfChildIsChecked = OneOfChildsIsChecked(node);
                if (oneOfChildIsChecked)
                {
                    node.CheckState = CheckState.Checked;
                }
                else
                {
                    node.CheckState = CheckState.Unchecked;
                }
            }
        }
        private void treeListProductionStagesPlan_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (ckChonNhom.Checked == true)
            {
                TreeListNode node = e.Node;
                if (node.Checked)
                {
                    node.UncheckAll();
                }
                else
                {
                    node.CheckAll();
                }
                while (node.ParentNode != null)
                {
                    node = node.ParentNode;
                    bool oneOfChildIsChecked = OneOfChildsIsChecked(node);
                    if (oneOfChildIsChecked)
                    {
                        node.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        node.CheckState = CheckState.Unchecked;
                    }
                }
            }
        }
        #endregion

        private string maloai;
        private string tenloai;
        private string mucDoCon;
        private string soluongchitiet;
        private string parentnode;

        private void btnEditTreeListNodeProductionStagesPlan_Click(object sender, EventArgs e)
        {
            EditTreeListNodeProductionStagesPlan();
        }

        private void btnSaveTreeListNodeProductionStagesPlan_Click(object sender, EventArgs e)
        {
            SaveTreeListNodeProductionStagesPlan();
        }



        private void btnDeleteDeleteTreeListNodeProductionStagesPlan_Click(object sender, EventArgs e)
        {
            DeleteTreeListNodeProductionStagesPlan();
        }



        private void treeListProductionStages_MouseMove(object sender, MouseEventArgs e)
        {
            List<TreeListNode> node = treeListProductionStages.GetAllCheckedNodes();
            if (node.Count > 0)
            {
                btnSaveTreeListNodeProductionStagesPlan.Enabled = true;
            }
            else
            {
                btnSaveTreeListNodeProductionStagesPlan.Enabled = false;
            }
        }

        private void btnShowTreeListPlan_Click(object sender, EventArgs e)
        {
            ShowTreeListStagesPlan();
        }
        
        private string id;//node hiện hanh
        private string IDDonHang;
        private string IDChiTietDonHang;
        private string IDChiTietSanPham;
        private string MaDonHang;
        private string MaPo;
        private string MaSanPham;
        private string TenSanPham;
        private string SoLuongDonHang;
        private string SoLuongChiTietDonHang;
        private string SoLuongYCSanXuat;
        private string TenCongDoan;
        private string MaChiTiet;
        private string TenChiTiet;
        private string SoChiTiet;

        private void treeListProductionStagesPlan_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string point = "";
            point = treeListProductionStagesPlan.GetFocusedDisplayText();
            id = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanID);//Khi tao them node con thì node hiện hành là node cha
            maloai = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMaLoai);
            tenloai = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanTenLoai);
            idproduction = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanIDSanPham);
            mucDoCon = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMucDo);
            soluongchitiet = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanSoLuongLoai);
            IDDonHang = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanIDDonHang);
            IDChiTietDonHang = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanIDChiTietDonHang);
            IDChiTietSanPham = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanIDChiTietSanPham);
            MaDonHang = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMaDonHang);
            MaPo = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMaPo);
            MaSanPham = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMaSanPham);
            TenSanPham = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanTenSanPham);
            SoLuongDonHang = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanSoLuongDonHang);
            SoLuongChiTietDonHang = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanSoLuongChiTietDonHang);
            SoLuongYCSanXuat = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanSoLuongYCSanXuat);
            TenCongDoan = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanTenCongDoan);
            MaChiTiet = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMaChiTiet);
            TenChiTiet = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanTenChiTiet);
            SoChiTiet = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanSoChiTiet);
            dpNgayCapNhat.Text = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanNgayLap);
        }
        private void treeListProductionStagesPlan_DoubleClick(object sender, EventArgs e)
        {
            //Double click show giao diên add công đoạn
             frmChildNodeTreeListPlan childNodeTreeListPlan =
             new frmChildNodeTreeListPlan(idproduction, maloai, tenloai, id, mucDoCon.ToString(), soluongchitiet.ToString(),
             IDDonHang,
             IDChiTietDonHang,
             IDChiTietSanPham,
             MaDonHang,
             MaPo,
             MaSanPham,
             TenSanPham,
             SoLuongDonHang,
             SoLuongChiTietDonHang,
             SoLuongYCSanXuat,
             TenCongDoan,
             MaChiTiet,
             TenChiTiet,
             SoChiTiet);
            childNodeTreeListPlan.ShowDialog();
            ShowTreeListStagesPlanfollowProductionID();//Thể hiện danh sách sản phẩm theo id chi tiết đơn hàng
        }

        private void treeListProductionStagesPlan_MouseMove(object sender, MouseEventArgs e)
        {
            List<TreeListNode> node = treeListProductionStagesPlan.GetAllCheckedNodes();
            if (node.Count >= 1)
            {
                btnEditTreeListNodeProductionStagesPlan.Enabled = true;
                btnDeleteDeleteTreeListNodeProductionStagesPlan.Enabled = true;
                btnCapNhatNgayBatDauKetThuc.Enabled = true;
            }
            else
            {
                btnEditTreeListNodeProductionStagesPlan.Enabled = false;
                btnDeleteDeleteTreeListNodeProductionStagesPlan.Enabled = false;
                btnCapNhatNgayBatDauKetThuc.Enabled = false;
            }
        }
        //Thể hiện công đoạn
        private void ShowStagesCode()
        {
            ketnoi kn = new ketnoi();
            repositoryItemGridLookUpEditMaCongDoan.DataSource = kn.laybang(@"select Ma_Nguonluc MaCongDoan,* from tblResources order by ResourceID ASC");
            repositoryItemGridLookUpEditMaCongDoan.DisplayMember = "Ma_Nguonluc";
            repositoryItemGridLookUpEditMaCongDoan.ValueMember = "Ma_Nguonluc";
            treeListStagesMaCongDoan.ColumnEdit = repositoryItemGridLookUpEditMaCongDoan;
            kn.dongketnoi();
        }
        //Thể hiện tổ thực hiện
        private void TheHienToThucHien()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ToThucHien from tblResources");
            var dt = kn.laybang(sqlQuery);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemComboBoxToThucHien.Items.Add(dt.Rows[i]["ToThucHien"]);
            }
            kn.dongketnoi();
        }

        private void treeListProductionStagesPlan_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //SANXUATDbContext db = new SANXUATDbContext();
            //if (e.Column.FieldName == "Ma_Nguonluc")
            //    {
            //        dynamic value = treeListProductionStagesPlan.GetNodeByRowHandle(e.Rowhand, e.Column);
            //        var dt = db.tblResources.FirstOrDefault(x => x.Ma_Nguonluc == (string));
            //        if (dt != null)
            //        {
            //        treeListProductionStagesPlan.SetRowCellValue(e.RowHandle, "Ten_Nguonluc", dt.Ten_Nguonluc);
            //        treeListProductionStagesPlan.SetRowCellValue(e.RowHandle, "ToThucHien", dt.ToThucHien);

            //        }
            //    }
        }

        private void btnTraCuuRePort_Click(object sender, EventArgs e)
        {
            ShowPlanMadeDetailProductionStages();
        }
        private void ShowPlanMadeDetailProductionStages()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from KeHoachChiTiet_func('{0}','{1}') 
                  order by BatDau ASC",
                  dplichkehoachTu.Value.ToString("yyyy-MM-dd"),
                  dpkehoachden.Value.ToString("yyyy-MM-dd"),
                  madonhang);
            treeListKeHoachChiTietGiaoHang.DataSource =
              kn.laybang(sqlQuery);
            treeListKeHoachChiTietGiaoHang.ForceInitialize();
            treeListKeHoachChiTietGiaoHang.ExpandAll();
            treeListKeHoachChiTietGiaoHang.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
            treeListKeHoachChiTietGiaoHang.Appearance.Row.Font = new Font("Segoe UI", 7f);
        }
        private void TheHienKeHoachTheoMaDonHang()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from KeHoachChiTiet_func('{0}','{1}')",
                  dpTu.Value.ToString("yyyy-MM-dd"),
                  dpDen.Value.ToString("yyyy-MM-dd"));
            treeListKeHoachChiTietGiaoHang.DataSource =
              kn.laybang(sqlQuery);
            treeListKeHoachChiTietGiaoHang.ForceInitialize();
            treeListKeHoachChiTietGiaoHang.ExpandAll();
            treeListKeHoachChiTietGiaoHang.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
            treeListKeHoachChiTietGiaoHang.Appearance.Row.Font = new Font("Segoe UI", 7f);
        }

        private void btnExportRePort_Click(object sender, EventArgs e)
        {
            treeListKeHoachChiTietGiaoHang.ShowPrintPreview();
        }

        private void grOrderDetailfollowDate_DoubleClick(object sender, EventArgs e)
        {
            TheHienDonHangDaTrienKhai();
        }

        private void btnKeHoachVatLieu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi Connect = new ketnoi();
            dt = Connect.laybang("select * from PHIEUSANXUAT where madh like N'" + cbMaDonHang.Text + "'");
            XRPhieuSX_DaDuyet rpPHIEUSANXUAT_Duyet = new XRPhieuSX_DaDuyet();
            rpPHIEUSANXUAT_Duyet.DataSource = dt;
            rpPHIEUSANXUAT_Duyet.DataMember = "Table";
            rpPHIEUSANXUAT_Duyet.CreateDocument(false);
            rpPHIEUSANXUAT_Duyet.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbMaDonHang.Text;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT_Duyet.PrintingSystem);
            rpPHIEUSANXUAT_Duyet.ShowPreviewDialog();
            Connect.dongketnoi();
        }

        private void ckEditTable_CheckedChanged(object sender, EventArgs e)
        {
            if (ckEditTable.Checked == true)
            {
                gvOrderDetailfollowDate.OptionsBehavior.Editable = true;
            }
            else
            {
                gvOrderDetailfollowDate.OptionsBehavior.Editable = false;
            }
        }

        private void btnExportCongDoanSanPham_Click(object sender, EventArgs e)
        {
            treeListProductionStages.ShowPrintPreview();
        }

        private void btnExportCongDoanSanXuatTheoNhom_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi Connect = new ketnoi();
            string sqlQuery = string.Format(@"select * from viewCongDoanSanXuat 
                                    where IDChiTietDonHang like '{0}' order by ThuTu ASC", iddetailorder);
            dt = Connect.laybang(sqlQuery);
            XRCongDoanSanPham congDoanSanPham = new XRCongDoanSanPham();
            congDoanSanPham.DataSource = dt;
            congDoanSanPham.DataMember = "Table";
            congDoanSanPham.CreateDocument(false);
            congDoanSanPham.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbMaDonHang.Text;
            PrintTool tool = new PrintTool(congDoanSanPham.PrintingSystem);
            congDoanSanPham.ShowPreviewDialog();
            Connect.dongketnoi();
        }

        private void btnReportNhom_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi Connect = new ketnoi();
            string sqlQuery = string.Format(@"select * from viewLichSanXuatNhom 
                    where MaDonHang like '{0}'", cbMaDonHang.Text);
            dt = Connect.laybang(sqlQuery);
            XRLapLichSanXuatNhom lichnhom = new XRLapLichSanXuatNhom();
            lichnhom.DataSource = dt;
            lichnhom.DataMember = "Table";
            lichnhom.CreateDocument(false);
            lichnhom.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbMaDonHang.Text;
            PrintTool tool = new PrintTool(lichnhom.PrintingSystem);
            lichnhom.ShowPreviewDialog();
            Connect.dongketnoi();
        }

        private void btnCapNhatNgayBatDauKetThuc_Click(object sender, EventArgs e)
        {
            CapNhatNgayBatDauKetThuc();
        }
        private void CapNhatNgayBatDauKetThuc()
        {
            List<TreeListNode> list = treeListProductionStagesPlan.GetAllCheckedNodes();
            Function.ConnectSanXuat();//Mo ket noi
            foreach (TreeListNode node in list)
            {
                string IDTrienKhai = node.GetDisplayText(treeListPlanIDTrienKhai);
                string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuat set
                        BatDau='{0}',
                        KetThuc='{1}',
                        NgayHieuChinh=GetDate(),
                        NgayLap='{2}'
                        where IDTrienKhai like '{3}'",
                      node.GetDisplayText(treeListPlanDateStar) == "" ? "" :
                      Convert.ToDateTime(node.GetDisplayText(treeListPlanDateStar)).ToString("MM-dd-yyyy"),
                      node.GetDisplayText(treeListPlanDateEnd) == "" ? "" :
                      Convert.ToDateTime(node.GetDisplayText(treeListPlanDateEnd)).ToString("MM-dd-yyyy"),
                      dpNgayCapNhat.Value.ToString("yyyy-MM-dd"),
                      IDTrienKhai);
                var dt = Function.GetDataTable(strQuery);
            }
            Function.Disconnect();//dong ket noi
            CapNhatNgayMinMax();//Cập nhật ngày min, max cho sản phẩm vừa ghi các ngày
            ShowTreeListStagesPlanfollowProductionID();
        }

        private void btnLenhSanXuatDaKyCamKet_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(madonhang, path.pathkinhdoanh);
            f2.Show();
        }

        private void btnBoSungThemCongDoan_Click(object sender, EventArgs e)
        {
            RanDonPoint();
            List<TreeListNode> list = treeListProductionStages.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string id = this.id;
                double parentid = double.Parse(node.GetDisplayText(treeListStagesParentID)) + double.Parse(this.randonpoint);
                string MaSanPham = node.GetDisplayText(treeListStagesMaLoai);
                string TenSanPham = node.GetDisplayText(treeListStagesTenLoai);
                string SoChiTietSanPham = node.GetDisplayText(treeListStagesSoChiTiet);
                string DonViSanPham = cbInventoryUnit.Text;     
                string ToThucHien = node.GetDisplayText(treeListStagesToThucHien);
                string MaCongDoan = node.GetDisplayText(treeListStagesMaCongDoan);
                string DinhMuc = node.GetDisplayText(treeListStagesDinhMuc);
                string SoThuTu = node.GetDisplayText(treeListStagesThuTu);
                string strQuery = string.Format(
                    @"insert into TrienKhaiKeHoachSanXuat
                        (ID,ParentID,
                         IDDonHang,IDChiTietDonHang,
                         IDChiTietSanPham,MaDonHang,
                         MaSanPham,TenSanPham,SoLuongDonHang,
                         DonViSanPham,MaLoai,TenLoai,SoLuongLoai,
                         DonViChiTiet,SoLuongChiTietDonHang,
                         TonKho,SoLuongYCSanXuat,ToThucHien,
                         MaCongDoan,TenCongDoan,
                         DinhMuc,SoThuTu,NgayLap,NguoiLap,IDSanPham,
                         MaChiTiet,TenChiTiet,SoChiTiet,MucDo,
                         MaPo,NgoaiQuan,GhiChu,KhoGiaoHang) values 
                       ('{0}','{1}','{2}','{3}',
                        '{4}',N'{5}',N'{6}',N'{7}','{8}',
                       N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',
                        '{14}','{15}','{16}',N'{17}',N'{18}',
                       N'{19}','{20}',N'{21}','{22}',N'{23}',
                        '{24}',N'{25}',N'{26}',N'{27}','{28}',
                       N'{29}',N'{30}',N'{31}',N'{32}')",
                        id,
                        parentid,
                        idorder, iddetailorder,
                        node.GetDisplayText(treeListStagesID),
                        cbMaDonHang.Text,
                        txtMaSanPham.Text,
                        tenquycach, soluongdathang,
                        node.GetDisplayText(treeListStagesDonVi), MaSanPham, TenSanPham,
                        Convert.ToDouble(SoChiTietSanPham), node.GetDisplayText(treeListStagesDonVi),
                        (Convert.ToDouble(soluongdathang) * Convert.ToDouble(SoChiTietSanPham)),
                        Convert.ToDouble(txtTonKhoThanhPham.Text) * Convert.ToDouble(SoChiTietSanPham),
                        (Convert.ToDouble(soluongdathang) - Convert.ToDouble(txtTonKhoThanhPham.Text)) * Convert.ToDouble(SoChiTietSanPham),
                        ToThucHien,
                        MaCongDoan, TenSanPham,
                        DinhMuc, SoThuTu,
                        dpNgayGhi.Value.ToString("yyyy-MM-dd"),
                        Login.Username, idproduction,
                        node.GetDisplayText(treeListStagesMaChiTiet),
                        node.GetDisplayText(treeListStagesTenChiTiet),
                        node.GetDisplayText(treeListStagesSoChiTiet),
                        node.GetDisplayText(treeListStagesMucDo),
                        txtMaPo.Text, txtNgoaiQuan.Text, txtGhiChu.Text, txtKhoGiao.Text);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            ShowTreeListStagesPlanfollowProductionID();
            TheHienDanhSachTrienKhai();
        }

        private void btnReportLenhSanXuat_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT * from IN_LENHSANXUAT where madh like N'" + cbMaDonHang.Text + "'");
            RpPhieusanxuat lenhSanXuat = new RpPhieusanxuat();
            lenhSanXuat.DataSource = dt;
            lenhSanXuat.DataMember = "Table";
            lenhSanXuat.CreateDocument(false);
            lenhSanXuat.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbMaDonHang.Text;
            PrintTool tool = new PrintTool(lenhSanXuat.PrintingSystem);
            lenhSanXuat.ShowPreviewDialog();
            kn.dongketnoi();
        }
    }
}
