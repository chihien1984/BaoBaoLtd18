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
using System.Configuration;

namespace quanlysanxuat
{
    public partial class UCGiaoNhanHang : UserControl
    {
        public UCGiaoNhanHang()
        {
            InitializeComponent();
        }

      
        public static string IdKey;
        private void LOADM_GIAOHANG()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("Select Num,IDSP,MaGH,ngaynhan,nvkd "
                  + ",madh,tendh,mabv,sanpham,chitietsanpham,Mact,So_CT "
                  + ",SL_CTHT,TL_CTHT,Maubv,LoaiDH,MaPo "
                  + ",cdthanhpham,soluongsx,ngoaiquang "
                  + ",donvi,daystar,dayend," + txtfield_SL.Text + " BTP," + txtfield_TL.Text + " TRONGLUONG"
                  + ",khachhang,xeploai,ghichu,Diengiai "
                  + ",MaBPnhan,TenBPnhan,Giaidoan,MaSQL,TenSQL "
                  + ",SoluongSP,Donvisp,Ketqua_GD,NguoiNhan "
                  + ",Ngaychapnhan,DonHangID,case  when Ketqua_GD<>'' then (N'Mã GD:'+MaGH+N'=>SốGD:'+convert(nvarchar,format(" + txtfield_SL.Text + ",'#,#0'))+ "
                 + " N' đơnvị; ■GIAO BỞI :'+TenSQL+N'; ■XÁC NHẬN BỞI: '+TenBPnhan+ "
                 + " N' ■LÚC: '+convert(nvarchar,DATEPART(HH,Ngaychapnhan))+ "
                 + " N' Giờ '+convert(nvarchar,DATEPART(MI,Ngaychapnhan))+ "
                 + " N' Phút '+convert(nvarchar,Ngaychapnhan,104))end Descrip "
                 + " from " + txtMatable.Text + " where "
                 + " convert(Date, ngaynhan, 103)   between @bd and @kt ", con);
            cmd.Parameters.Add(new SqlParameter("@bd", SqlDbType.Date)).Value = dptu_ngay.Text;
            cmd.Parameters.Add(new SqlParameter("@kt", SqlDbType.Date)).Value = dpden_ngay.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            con.Close();
        }
        private void LOAD_KQGIAODICH() {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand cmd = new SqlCommand("Select Num,IDSP,MaGH,ngaynhan,nvkd "
                   + ",madh,tendh,mabv,sanpham,chitietsanpham,Mact,So_CT "
                   + ",SL_CTHT,TL_CTHT,Maubv,LoaiDH,MaPo "
                   + ",cdthanhpham,soluongsx,ngoaiquang "
                   + ",donvi,daystar,dayend," + txtfield_SL.Text + " BTP," + txtfield_TL.Text + " TRONGLUONG "
                   + ",khachhang,xeploai,ghichu,Diengiai "
                   + ",MaBPnhan,TenBPnhan,Giaidoan,MaSQL,TenSQL "
                   + ",SoluongSP,Donvisp,Ketqua_GD,NguoiNhan "
                   + ",Ngaychapnhan,DonHangID,case  when Ketqua_GD<>'' then (N'Mã GD:'+MaGH+N'=>SốGD:'+convert(nvarchar,format(" + txtfield_SL.Text + ",'#,#0'))+ "
                  + " N' đơnvị; ■GIAO BỞI :'+TenSQL+N'; ■XÁC NHẬN BỞI: '+TenBPnhan+ "
                  + " N' ■LÚC: '+convert(nvarchar,DATEPART(HH,Ngaychapnhan))+ "
                  + " N' Giờ '+convert(nvarchar,DATEPART(MI,Ngaychapnhan))+ "
                  + " N' Phút '+convert(nvarchar,Ngaychapnhan,104)) end Descrip from " + txtMatable.Text + " where Num like " + txtIdenKey_Giaonhan.Text + "", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            con.Close();
        }

        private void BINDING_GIAOHANG(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtKetQuaGiaoDich.Text = gridView2.GetFocusedRowCellDisplayText(Col2Description);
            txtMagiao.Text = gridView2.GetFocusedRowCellDisplayText(Magh_grid2);
            txtMaDH.Text = gridView2.GetFocusedRowCellDisplayText(MaPSX_grid2);
            txtMasp.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
            txtSanPham.Text = gridView2.GetFocusedRowCellDisplayText(Chitietthuchien_grid2);
            txtChiTietSP.Text = gridView2.GetFocusedRowCellDisplayText(Sanpham_grid2);
            txtSoluonggiao.Text = gridView2.GetFocusedRowCellDisplayText(SoluongHTgiao_grid2);
            txtTrongLuonggiao.Text = gridView2.GetFocusedRowCellDisplayText(TrongluongHTgiao_grid2);
            txtKqGD.Text = gridView2.GetFocusedRowCellDisplayText(KetQuaGD_grid2);
            txtIdenKey_Giaonhan.Text = gridView2.GetFocusedRowCellDisplayText(Id_grid2);
            KiemTraMaGH();
        }

        private void UCGiaoNhanHang_Load(object sender, EventArgs e)
        {
            txtUser.Text = Login.Username;
            if (IdKey == "1039" || IdKey == "2039")
            { btnXoaGhiSai.Enabled = true;
            btnHuyGiaoDich.Enabled = true;
            }
            dptu_ngay.Text = DateTime.Now.ToString(); dpden_ngay.Text = DateTime.Now.ToString();
            LOAD_DMBPGIAO();
            LOAD_DMBPNHAN();

        }
        private void LOAD_DMBPGIAO()
        {
            ketnoi Connect = new ketnoi();
            CbNguoigiao.DataSource = Connect.laybang("select * from tblPHONGBAN order by To_bophan DESC");
            CbNguoigiao.DisplayMember = "To_bophan";
            CbNguoigiao.ValueMember = "To_bophan";
            Connect.dongketnoi();
        }
        private void LOAD_DMBPNHAN()
        {
            ketnoi Connect = new ketnoi();
            CBNguoiNhan.DataSource = Connect.laybang("select * from tblPHONGBAN order by To_bophan DESC");
            CBNguoiNhan.DisplayMember = "To_bophan";
            CBNguoiNhan.ValueMember = "To_bophan";
            Connect.dongketnoi();
        }
        private void MaTableGiao(object Sender, EventArgs e)
        {
            GiaoNhanfield();
        }
        private void MaTableNhan(object Sender, EventArgs e)
        { //GiaoNhanfield();  
        }
        #region Lấy MaView,Ma_bophan,fieldSL,fieldTL,Matable from tblPHONGBAN
        private void GiaoNhanfield()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand(@"select MaView,fieldSL,fieldTL,Matable,CapNhatHT 
                    from tblPHONGBAN where To_bophan like N'" + CbNguoigiao.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtMaviewGiao.Text = reader.GetString(0);
                    txtfield_SL.Text = reader.GetString(1);
                    txtfield_TL.Text = reader.GetString(2);
                    txtMatable.Text = reader.GetString(3);
                    txtUpdate.Text = reader.GetString(4);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }
        #endregion
        private void btnDHGiao_Click(object sender, EventArgs e)
        {
            try
            {
                LOADM_GIAOHANG();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Rỗng");
            }

        }
        private void KiemTraMaGH()
        {
            if (txtMagiao.Text != "" && txtKqGD.Text == "")
                btnKhoaGiaoDich.Enabled = true;
            if (txtMagiao.Text == "" || txtKqGD.Text != "")
                btnKhoaGiaoDich.Enabled = false;
        }
        private void CapNhatGiaoDich(object sender, EventArgs e)
        {
            try
            {
                ketnoi Ketnoi = new ketnoi();
                gridControl2.DataSource = Ketnoi.xulydulieu("update " + txtMatable.Text + " set Ketqua_GD = N'GD thành công', "
                 + "  " + txtfield_SL.Text + " = '" + txtSoluongnhan.Text + "', "
                 + "  " + txtfield_TL.Text + " = '" + txtTrongluongNhan.Text + "', NguoiNhan =N'" + txtUser.Text + "',Ngaychapnhan=GetDate()"
                 + "  where Num = " + txtIdenKey_Giaonhan.Text + "");
                LOAD_KQGIAODICH();
                btnKhoaGiaoDich.Enabled = false;
                CapNhatHeThong();
            }
            catch { MessageBox.Show("Không thành công"); }
        }
        private void CapNhatHeThong() {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand(txtUpdate.Text, cn);
                queryCM.CommandType = CommandType.StoredProcedure;
                cn.Open();
                queryCM.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception)
            { }
        }

        private void btnHuy_GD_Click(object sender, EventArgs e)
        {
            try
            {
                XoaGiaoDich();
                LOAD_KQGIAODICH(); 
                btnKhoaGiaoDich.Enabled = false;
                CapNhatHeThong();
            }
            catch { MessageBox.Show("Không thành công"); }
        }
        private void HuyGiaoDich_click(object sender,EventArgs e) {
            XoaGiaoDich();
            HuyGiaoDich();
            LOAD_KQGIAODICH();
            btnKhoaGiaoDich.Enabled = false;
            CapNhatHeThong();

        }
        private void XoaGiaoDich()//Đưa số lượng giao dịch về 0 trước khi hủy
        {
            try
            {
                ketnoi Ketnoi = new ketnoi();
                gridControl2.DataSource = Ketnoi.xulydulieu("update " + txtMatable.Text + " set Ketqua_GD = '',MaGH='', "
                 + "  " + txtfield_SL.Text + " = '0', "
                 + "  " + txtfield_TL.Text + " = '0', NguoiNhan =N'" + txtUser.Text + "',Ngaychapnhan=GetDate()"
                 + "  where Num = " + txtIdenKey_Giaonhan.Text + "");
            }
            catch { MessageBox.Show("Không thành công"); }
        }
        private void HuyGiaoDich()//Hủy giao dịch
        {
            try
            {
                ketnoi Ketnoi = new ketnoi();
                gridControl2.DataSource = Ketnoi.xulydulieu("delete from " + txtMatable.Text + " "
              + " where Num = '" + txtIdenKey_Giaonhan.Text + "'");
            }
            catch { MessageBox.Show("Không thành công"); }
        }
    }
}
