using DevExpress.XtraTreeList.Nodes;
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
    public partial class ucQuanLyLuongQuanLy : DevExpress.XtraEditors.XtraForm
    {
        public ucQuanLyLuongQuanLy()
        {
            InitializeComponent();
        }
   
        private void ketnoi(string sqlQuery)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
        }
        private void TheHienDanhSachCongDoanSanPham()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select * from tblSanPhamTreeList 
				order by IDSanPham Desc, ThuTu ASC");
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            treeListApGiaCong.DataSource =dt;
            treeListApGiaCong.ForceInitialize();
            treeListApGiaCong.ExpandAll();
            //treeListProductionStages.BestFitColumns();
            treeListApGiaCong.OptionsSelection.MultiSelect = true;
        }
        private void TheHienSoLuongGiaoHang()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select a.*,b.DonGiaCongDoan
                from TrienKhaiKeHoachSanXuat a
                left outer join
                (select ID,DonGiaCongDoan from tblSanPhamTreeList)b
                on a.IDChiTietSanPham=b.ID where NgayLap
				between '{0}' and '{1}' order by IDChiTietDonHang desc,KetThuc asc",
                  dpTu.Value.ToString("yyyy-MM-dd"),
                  dpDen.Value.ToString("yyyy-MM-dd"));
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            treeListSoLuongHoanThanh.DataSource = dt;
            treeListSoLuongHoanThanh.ForceInitialize();
            treeListSoLuongHoanThanh.ExpandAll();
            //treeListProductionStagesPlan.BestFitColumns();
            treeListSoLuongHoanThanh.OptionsSelection.MultiSelect = true;
        }
        #region formload
        private void frmLuongKhoanQuanLy_Load(object sender, EventArgs e)
        {
            //txtuser.Text = Login.Username;
            dpTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDen.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            //DocDonDatHangTheoNgay();
            //gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            //gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            //gridView4.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            treeListSoLuongHoanThanh.Appearance.Row.Font = new Font("Segoe UI", 7f);
            treeListApGiaCong.Appearance.Row.Font = new Font("Segoe UI", 7f);
            TheHienDanhSachCongDoanSanPham();
            TheHienSoLuongGiaoHang();
            gvChiTietTienCongKhoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            TheHienSoTienChiTet();
            TheHienTongTien();
        }
        #endregion
        #region Doc danh sách đơn đặt hàng

        private void DocDonDatHangTheoNgay()
        {
    //        ketnoi kn = new ketnoi();
    //        string sqlQuery = string.Format(@"select Iden,madh,C.Masp,
    //            Cast(thoigianthaydoi as date) Ngaylap,case when D.Masp<>'' then 'x' end CoDinhMuc,
				//Tenquicach,dvt,Soluong,ngaygiao,
				//Khachhang,Mau_banve,Tonkho,ghichu,
				//ngoaiquang,pheduyet,Diengiai,nguoithaydoi,
				//DonGiaKhoan,TongTienKhoan,NgayLapGiaKhoan,NguoiGhiGiaKhoan from tblDHCT C
    //            left outer join(select distinct(Masp) from tblDMuc_LaoDong) D
				//on D.MaSP=C.MaSP
    //            where convert( datetime,thoigianthaydoi) 
    //            between '{0}' and '{1}' order by Iden Desc",
    //            dpTu.Value.ToString("yyyy-MM-dd"),
    //            dpDen.Value.ToString("yyyy-MM-dd"));
    //        gridControl4.DataSource = kn.laybang(sqlQuery);
    //        kn.dongketnoi();
        }
        #endregion
        
        private void DocDanhSachBoPhan()
        {
            //ketnoi kn = new ketnoi();
            //cbBoPhanLam.DataSource = kn.laybang(@"select BoPhan from tblPHONGBAN_TK");
            //cbBoPhanLam.ValueMember = "BoPhan";
            //cbBoPhanLam.DisplayMember = "BoPhan";
            //kn.dongketnoi();
        }
     
        private void LamMoiNguyenCongGhi()
        {
            //ketnoi kn = new ketnoi();
            //string sqlQurey = string.Format(@"select top 0 * from tblDMuc_LaoDong");
            //gridControl2.DataSource = kn.laybang(sqlQurey);
            //kn.dongketnoi();
        }
     

        private void btnDonHang_TrienKhai_Click(object sender, EventArgs e)
        {
            TheHienSoLuongGiaoHang();
        }

        private string maDonHang;
        private string sanPham;
        private string maSanPham;
        private string idDonHang;
        private void gridControl4_Click(object sender, EventArgs e)
        {
            //string point = "";
            //point = gridView4.GetFocusedDisplayText();
            //this.maDonHang = gridView4.GetFocusedRowCellDisplayText(Madh_col1);
            //this.maSanPham= gridView4.GetFocusedRowCellDisplayText(masp_col1);
            //this.sanPham = gridView4.GetFocusedRowCellDisplayText(sanpham_col1);
            //this.idDonHang = gridView4.GetFocusedRowCellDisplayText(donHangID_col1);
            //DocDSNguyenCongSanPham();
            //DocDSCongDoanTheoMaSanPham();
            //DocSoGhiGiaKhoanTheoDonHang();
        }
        private void DocDSNguyenCongSanPham()
        {
            //ketnoi kn = new ketnoi();
            //string sqlStr = string.Format(@"select DonGiaKhoan='',
            //    NguyenCong,ChiTietSanPham,
            //    Max(SoChiTiet)SoChiTietSanPham
            //    from tblDMuc_LaoDong  where Masp='{0}' 
            //    group by NguyenCong,ChiTietSanPham", this.maSanPham);
            //gridControl2.DataSource = kn.laybang(sqlStr);
            //kn.dongketnoi();
        }
        private void DocDSCongDoanTheoMaSanPham()
        {
            //ketnoi kn = new ketnoi();
            //string sqlStr = string.Format(@"select id,Tencondoan,NguyenCong,
            //    ChiTietSanPham,SoChiTietSanPham
            //    from tblDMuc_LaoDong
            //    where Masp ='{0}'", this.maSanPham);
            //gridControl5.DataSource = kn.laybang(sqlStr);
            //kn.dongketnoi();
        }
        private void DocTatCongDoan()
        {
            //ketnoi kn = new ketnoi();
            //string sqlStr = string.Format(@"select id,Tencondoan,NguyenCong,
            //    ChiTietSanPham,SoChiTietSanPham
            //    from tblDMuc_LaoDong", this.maSanPham);
            //gridControl5.DataSource = kn.laybang(sqlStr);
            //kn.dongketnoi();
        }
        private void btnGhiDonGiaNguyenCong_Click(object sender, EventArgs e)
        {
            GhiDonGiaNguyenCong();
            DocSoGhiGiaKhoan();
        }
        private void btnXoaDonGiaKhoanQL_Click(object sender, EventArgs e)
        {
            XoaKhoanNguyenCong();
            XoaSoGhiSoLuong();
        }
        private void XoaKhoanNguyenCong()
        {
            //ketnoi kn = new ketnoi();
            //int kq = kn.xulydulieu("delete from KhoanLuongQuanLy_NguyenCong where IDKhoanQL = " + txtIDKhoanQL.Text + "");
            //kn.dongketnoi();
            //DocSoGhiGiaKhoan();
        }
        private void XoaSoGhiSoLuong()
        {
            //ketnoi kn = new ketnoi();
            //int kq = kn.xulydulieu("delete from KhoanLuongQuanLy_ChiTiet where IDKhoanQL = " + txtIDKhoanQL.Text + "");
            //kn.dongketnoi();
            //TraCuuSoTinhTienKhoan();
        }

        private void GhiDonGiaNguyenCong()
        {
            //int[] listRowList = this.gridView2.GetSelectedRows();
            //if (listRowList.Count() < 1)
            //{
            //    MessageBox.Show("Chưa check", "Thông báo"); return;
            //}
            //else
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open();
            //    DataRow rowData;
            //    for (int i = 0; i < listRowList.Length; i++)
            //    {
            //        rowData = this.gridView2.GetDataRow(listRowList[i]);
            //        string strQuery = string.Format(@"insert into KhoanLuongQuanLy_NguyenCong 
            //                (MaDonHang,MaSanPham,
            //                SanPham,ChiTietSanPham,
            //                NguyenCong,DonGiaKhoan,
            //                NguoiLap,NgayLap,
            //                IDDonHang,SoChiTiet)
            //         values ('{0}','{1}',
            //                N'{2}',N'{3}',
            //                N'{4}','{5}',
            //                N'{6}',N'{7}',
            //                {8},{9})",
            //                this.maDonHang,this.maSanPham,
            //                this.sanPham,rowData["ChiTietSanPham"],
            //                rowData["NguyenCong"], rowData["DonGiaKhoan"],
            //                txtuser.Text,
            //                dpNgayAp.Value.ToString("yyyy-MM-dd"),
            //                this.idDonHang,rowData["SoChiTietSanPham"]);
            //        SqlCommand cmd = new SqlCommand(strQuery, con);
            //        cmd.ExecuteNonQuery();
            //    }
            //    con.Close();
            //}
        }

        private void btnGhiSoLuongLamRa_Click(object sender, EventArgs e)
        {
            GhiSoLuongLamRa();
            TraCuuSoTinhTienKhoan();
        }

        //Ghi chi tiết nguyên công từ định mức vào bảng tính tiền chi tiết nguyên công của tổ trưởng
        private void GhiSoLuongLamRa()
        {
            //double donGiaLam = double.Parse(txtDonGiaQuanLy.Text);
            //double soLuongLam = double.Parse(txtSoLuongLam.Text);
            //double thanhTien = soLuongLam *donGiaLam;
            //ketnoi kn = new ketnoi();
            //string sqlStr = string.Format(@"insert into KhoanLuongQuanLy_ChiTiet
            //    (IDKhoanQL,
            //    SoLuongLam,
            //    DonGiaKhoan,
            //    ThanhTien,
            //    BoPhanLam,
            //    NgayLam,IDDonHang) 
            //    values('{0}','{1}','{2}','{3}',N'{4}','{5}','{6}')",
            //    txtIDKhoanQL.Text,
            //    soLuongLam,
            //    donGiaLam,
            //    thanhTien,
            //    cbBoPhanLam.Text,
            //    dpNgayLamRaSanPham.Value.ToString("MM-dd-yyyy"),
            //    idDonHangKhoan);
            //gridControl1.DataSource = kn.laybang(sqlStr);
            //kn.dongketnoi();
            //TraCuuSoTinhTienKhoan();
        }
        private void btnXoaTinhTienQuanLy_Click(object sender, EventArgs e)
        {
            //ketnoi kn = new ketnoi();
            //int kq = kn.xulydulieu("delete from KhoanLuongQuanLy_ChiTiet where IDGiaChiTietQuanLy = " + txtIDGiaChiTietQuanLy.Text + "");
            //kn.dongketnoi();
            //TraCuuSoTinhTienKhoan();
        }
        private void btnTraCuSoTienKhoanQuanLy_Click(object sender, EventArgs e)
        {
            //TraCuuSoTinhTienKhoan();
            //gridView1.Columns["BoPhanLam"].GroupIndex = -1;
        }
        private void TraCuuSoTinhTienKhoan()
        {
    //        ketnoi kn = new ketnoi();
    //        string sqlStr = string.Format(@"select C.*,N.madh,N.MaSP,
    //            N.Tenquicach,N.Soluong,k.ChiTietSanPham
    //            from KhoanLuongQuanLy_ChiTiet C
    //            left outer join  tblDHCT N
    //            on C.IDDonHang=N.Iden
				//left outer join KhoanLuongQuanLy_NguyenCong k
				//on C.IDKhoanQL=k.IDKhoanQL
				//where NgayLam between '{0}' and '{1}' order by NgayLam DESC",
    //            dpTu.Value.ToString("MM-dd-yyyy"),
    //            dpDen.Value.ToString("MM-dd-yyyy"));
    //        gridControl1.DataSource = kn.laybang(sqlStr);
    //        kn.dongketnoi();
        }
        private void TraCuuSoTinhTienKhoanTheoIDKhoan()
        {
            //ketnoi kn = new ketnoi();
            //string sqlStr = string.Format(@"select C.*,N.madh,N.MaSP,N.Tenquicach,N.Soluong
            //from KhoanLuongQuanLy_ChiTiet C
            //left outer join  tblDHCT N
            //on C.IDDonHang=N.Iden
            //    where C.IDKhoanQL ='{0}'", txtIDKhoanQL.Text);
            //gridControl1.DataSource = kn.laybang (sqlStr);
            //kn.dongketnoi();
        }
       
        private void btnGhiSoLuongHoanThanh_Click(object sender, EventArgs e)
        {}

        private void gridControl5_Click(object sender, EventArgs e)
        {
            //string point = "";
            //point = gridView5.GetFocusedDisplayText();
            //txtIDDonHang.Text = gridView5.GetFocusedRowCellDisplayText(idDonHang_col5);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
         
        }
        string idDonHangKhoan;
        private void gridControl3_Click(object sender, EventArgs e)
        {
            //string point = "";
            //point = gridView3.GetFocusedDisplayText();
            //txtIDKhoanQL.Text = gridView3.GetFocusedRowCellDisplayText(idKhoanQL_col3);
            //txtDonGiaQuanLy.Text = gridView3.GetFocusedRowCellDisplayText(donGiaQuanLy_col3);
            //this.idDonHangKhoan= gridView3.GetFocusedRowCellDisplayText(idDonHang_col3);
            //TraCuuSoTinhTienKhoanTheoIDKhoan();
        }

        private void btnTraCuuSoGhiGia_Click(object sender, EventArgs e)
        {
            DocSoGhiGiaKhoan();
        }
        private void DocSoGhiGiaKhoan()
        {
            //ketnoi kn = new ketnoi();
            //gridControl3.DataSource = kn.laybang(@"select SoChiTiet*DonGiaKhoan DonGiaBo,* from 
            //    KhoanLuongQuanLy_NguyenCong");
            //kn.dongketnoi();
        }
        private void DocSoGhiGiaKhoanTheoDonHang()
        {
            //ketnoi kn = new ketnoi();
            //string sqlStr = string.Format(@"select SoChiTiet*DonGiaKhoan DonGiaBo,* from 
            //    KhoanLuongQuanLy_NguyenCong 
            //where IDDonHang={0}", idDonHang);
            //gridControl3.DataSource = kn.laybang(sqlStr);
            //kn.dongketnoi();
        }
        private void btnGhiChiTietSanPham_Click(object sender, EventArgs e)
        {
            //int[] listRowList = this.gridView5.GetSelectedRows();
            //if (listRowList.Count() < 1)
            //{
            //    MessageBox.Show("Chưa check", "Thông báo"); return;
            //}
            //else
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open();
            //    DataRow rowData;
            //    for (int i = 0; i < listRowList.Length; i++)
            //    {
            //        rowData = this.gridView5.GetDataRow(listRowList[i]);
            //        string strQuery = string.Format(@"update tblDMuc_LaoDong set 
            //        ChiTietSanPham ='{0}',
            //        SoChiTietSanPham='{1}'
            //        where id ='{2}'",
            //        rowData["ChiTietSanPham"],
            //        rowData["SoChiTietSanPham"],
            //        rowData["id"]);
            //        SqlCommand cmd = new SqlCommand(strQuery, con);
            //        cmd.ExecuteNonQuery();
            //    }
            //    con.Close();
            //}
        }

        private void btnTraCuDMCongDoan_Click(object sender, EventArgs e)
        {
            DocTatCongDoan();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            //string point = "";
            //point = gridView1.GetFocusedDisplayText();
            //txtIDGiaChiTietQuanLy.Text = gridView1.GetFocusedRowCellDisplayText(IDGiaChiTietQuanLy_grid1);
        }

        private void btnSuaDonGia_Click(object sender, EventArgs e)
        {
            SuaKhoanLuongQL();
            SuaKhoanSoLuongLamRa();
            DocDSKhoanNguyenCongTheoID();//Sô ghi gia khoan
            DocDSKhoanSoLuongTheoID(); //Sổ ghi tiền khoán
        }
        private void SuaKhoanLuongQL ()
        {
            //ketnoi kn = new ketnoi();
            //string sqlStr = string.Format(@"update KhoanLuongQuanLy_NguyenCong
            //    set DonGiaKhoan = '{0}',NguoiLap='{1}',NgayLap='{2}'
            //    where IDKhoanQL = '{3}'",
            //    txtDonGiaQuanLy.Text,
            //    txtuser.Text,
            //    dpNgayAp.Value.ToString("yyyy-MM-dd"),
            //    txtIDKhoanQL.Text);
            //gridControl3.DataSource = kn.laybang(sqlStr);
            //kn.dongketnoi();
        }
        private void DocDSKhoanNguyenCongTheoID()//Đọc danh sach khoán nguyen cong
        {
            //ketnoi kn = new ketnoi();
            //gridControl3.DataSource = kn.laybang(@"select SoChiTiet*DonGiaKhoan DonGiaBo,* from 
            //    KhoanLuongQuanLy_NguyenCong where IDKhoanQL=" + txtIDKhoanQL.Text+"");
            //kn.dongketnoi();
        }
        private void SuaKhoanSoLuongLamRa()
        {
            //ketnoi kn = new ketnoi();
            //string sqlStr = string.Format(@"update KhoanLuongQuanLy_ChiTiet
            //    set DonGiaKhoan='{0}',ThanhTien=(SoLuongLam*DonGiaKhoan)
            //    where IDKhoanQL='{1}'",txtDonGiaQuanLy.Text,txtIDKhoanQL.Text);
            //gridControl1.DataSource = kn.laybang(sqlStr);
            //kn.dongketnoi();
        }
        private void DocDSKhoanSoLuongTheoID()//Đọc danh sách số lượng làm ra
        {
            //ketnoi kn = new ketnoi();
            //gridControl1.DataSource = kn.laybang(
            //    @"select C.*,N.madh,N.MaSP,N.Tenquicach,N.Soluong
            //    from KhoanLuongQuanLy_ChiTiet C
            //    left outer join  tblDHCT N
            //    on C.IDDonHang=N.Iden where C.IDKhoanQL=" + txtIDKhoanQL.Text+"");
            //kn.dongketnoi();
        }

        private void btnSuaSoLuongLamRa_Click(object sender, EventArgs e)
        {
            //ketnoi kn = new ketnoi();
            //string sqlStr = string.Format(@"update KhoanLuongQuanLy_ChiTiet
            //    set SoLuongLam='{0}',ThanhTien=(SoLuongLam*DonGiaKhoan)
            //    where IDGiaChiTietQuanLy='{1}'", 
            //    txtSoLuongLamRaSua.Text, 
            //    txtIDGiaChiTietQuanLy.Text);
            //gridControl1.DataSource = kn.laybang(sqlStr);
            //kn.dongketnoi();
            //DocDSKhoanSoLuongTheoID();
        }

        private void btnGroupSumThanhTien_Click(object sender, EventArgs e)
        {
            //gridView1.Columns["BoPhanLam"].GroupIndex = 0;
            //gridView1.ExpandAllGroups();
        }

        private void btnCapNhatGiaCong_Click(object sender, EventArgs e)
        {
            List<TreeListNode> list = treeListApGiaCong.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                      @"update tblSanPhamTreeList set DonGiaCongDoan='{0}',NguoiLapDonGia=N'{1}',
                        NgayLapDonGia='{2}',NgayChinhSuaGia=GetDate() where ID like '{3}'", 
                        node.GetDisplayText(treeListSanPhamDonGiaCong),
                        Login.Username,
                        dpNgayApGia.Value.ToString("yyyy-MM-dd"),
                        node.GetDisplayText(treeListSanPhamID));
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            TheHienDanhSachCongDoanSanPham();
        }

        private void btnTraCuuSoLuongGiaoHang_Click(object sender, EventArgs e)
        {
            TheHienSoLuongGiaoHang();
        }

        private void btnCapNhatTienCongQuanLy_Click(object sender, EventArgs e)
        {
            List<TreeListNode> list = treeListSoLuongHoanThanh.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                      @"update TrienKhaiKeHoachSanXuat 
                        set DonGiaKhoanTo='{0}',SoLuongTinhGiaKhoan ='{1}',
                        ThanhTienKhoanTo=DonGiaKhoanTo*SoLuongTinhGiaKhoan
                        where IDTrienKhai like '{2}';
                        update TrienKhaiKeHoachSanXuat 
                        set ThanhTienKhoanTo=DonGiaKhoanTo*SoLuongTinhGiaKhoan
                        where IDTrienKhai like '{2}'",
                        node.GetDisplayText(treeListDonGiaCong),
                        node.GetDisplayText(treeListSoHoanThanh),
                        node.GetDisplayText(treeListPlanIDTrienKhai));
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            TheHienSoLuongGiaoHang();
        }

        private void treeListProductionStagesPlan_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }

        private void btnTraCuuChiTet_Click(object sender, EventArgs e)
        {
            TheHienSoTienChiTet();
        }
        private void TheHienSoTienChiTet()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select * from TrienKhaiKeHoachSanXuat 
						where NgayLap  between '{0}' and '{1}' and
						ThanhTienKhoanTo >0",
                  dpTu.Value.ToString("yyyy-MM-dd"),
                  dpDen.Value.ToString("yyyy-MM-dd"));
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grChiTietTienCongKhoan.DataSource = dt;
            con.Close();
        }
        private void TheHienTongTien()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select sum(ThanhTienKhoanTo)ThanhTienKhoanTo,ToThucHien 
						from TrienKhaiKeHoachSanXuat where 
						NgayLap  between '{0}' and '{1}' 
						and ThanhTienKhoanTo>0
						group by ToThucHien",
                  dpTu.Value.ToString("yyyy-MM-dd"),
                  dpDen.Value.ToString("yyyy-MM-dd"));
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grTongTien.DataSource = dt;
            con.Close();
        }
        private void btnTraCuuTong_Click(object sender, EventArgs e)
        {
            TheHienTongTien();
        }

        private void btnSuaSoGiaoThucTe_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gvChiTietTienCongKhoan.GetSelectedRows();
            if (listRowList.Count() < 1)
            {
                MessageBox.Show("Chưa check", "Thông báo"); return;
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChiTietTienCongKhoan.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuat 
                        set SoLuongTinhGiaKhoan ='{0}'
                        where IDTrienKhai like '{1}';
                        update TrienKhaiKeHoachSanXuat 
                        set ThanhTienKhoanTo=DonGiaKhoanTo*SoLuongTinhGiaKhoan
                        where IDTrienKhai like '{1}'",
                    rowData["SoLuongTinhGiaKhoan"],
                    rowData["IDTrienKhai"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                TheHienSoTienChiTet();
                TheHienTongTien();
            }
        }
    }
}
