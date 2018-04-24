using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.XtraReports.UI;

namespace quanlysanxuat
{
    public partial class frmSuaVTDAUVAO : DevExpress.XtraEditors.XtraForm
    {

        Clsketnoi kn = new Clsketnoi();
        public frmSuaVTDAUVAO()
        {
            InitializeComponent();
        }
        public static string Ide = "";
        public static string madh = "";
        public static string CodeVatllieu = "";
        public static string Codedetail = "";
        public static string Tenquicachsp = "";
        public static string Soluongsanpham = "";
        public static string Donvi_sanpham = "";
        public static string Ten_vattu = "";
        public static string SL_vattucan = "";
        public static string KL_vattucan = "";
        public static string SL_vattutonkho = "";
        public static string KL_vattutonkho = "";
        public static string SL_vattumua = "";
        public static string KL_vattumua = "";
        public static string Donvi_vattu = "";
        public static string NCC = "";
        public static string NguoiGD = "";
        public static string Dongia = "";
        public static string Donviquydoi = "";
        public static string Ngaydat_vattu = "";
        public static string NgayDK_ve = "";
        public static string Ngayve_TT = "";
        public static string SL_vattuve = "";
        public static string KL_vattuve = "";
        public static string SL_tinhgia = "";
        public static string Dvt_gia = "";
        public static string Ghichu_dathangmua = "";
        public static string Ghichu_denghimua = "";
        public static string DK_TCmua = "";
        public static string VAT = "";
        public static string quyetdinh = "";
        public static string nhanviendathang = "";
        public static string nguoikiemkho = "";
        public static string ngaykiemkho = "";
        public static string nguoinhap = "";
        public static string ngaynhap = "";
        public static string LoadGridview1 = "";

        private void frmSuaVTDAUVAO_Load(object sender, EventArgs e)// LOAD DATA TỪ TEXTBOX CỦA DỮ LIỆU VẬT TƯ VÀO 
        {
            txtidentitysanpham.Text = Ide;
            cbmadh.Text = madh;
            txtcodevatlieu.Text = CodeVatllieu;
            txtcodechitiet.Text=Codedetail;
            txtsanpham.Text=Tenquicachsp;
            txtsoluongsp.Text=Soluongsanpham;
            txtdonvisanpham.Text=Donvi_sanpham;
            txtvatlieu_chitiet.Text=Ten_vattu;
            txtSLcandat.Text=SL_vattucan;
            txtkhoiluongcandat.Text=KL_vattucan;
            txtSLtonkho.Text=SL_vattutonkho;
            txtKLtonkho.Text=KL_vattutonkho;
            txtsoluongmua.Text=SL_vattumua;
            txtkhoiluongmua.Text=KL_vattumua;
            txtdonvivattu.Text=Donvi_vattu;
            txtNCC.Text=NCC;
            txtnguoigiaodich.Text=NguoiGD;
            txtdongia.Text=Dongia;
            txtdonviquidoi.Text=Donviquydoi;
            dpNgaydatvattu.Text=Ngaydat_vattu;
            dpngaynhapvattu.Text=ngaynhap;
            txtsoluongthucnhap.Text=SL_vattuve;
            txttronluongthucnhap.Text=KL_vattuve;
            txtsoluongtinhgia.Text=SL_tinhgia;
            txtdonvitinhgia.Text=Dvt_gia;
            txtghichudathang.Text=Ghichu_dathangmua;
            txtghichudenghi.Text=Ghichu_denghimua;
            txtdieukiengiaodich.Text=DK_TCmua;
            cbVAT.Text=VAT;
            cbquyetdinhvattu.Text=quyetdinh;
            txtnguoidatmuahang.Text= nhanviendathang;
            txtnguoikiemkho.Text=nguoikiemkho;
            dpngaykiemkho.Text=ngaykiemkho;
            txtnguoinhap.Text=nguoinhap;           
        }

        private void btnghidulieu_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            decimal SOLUONGSANPHAM = Convert.ToDecimal(txtsoluongsp.Text);//ĐỔI DỮ LIỆU TEXTBOX SANG DECIMAL SAVE VÀO SQL
            decimal SoluongVT_CANDAT = Convert.ToDecimal(txtSLcandat.Text);
            decimal SoluongVT_TONKHO = Convert.ToDecimal(txtSLtonkho.Text);
            decimal SoluongVT_MUA = Convert.ToDecimal(txtsoluongmua.Text);
            decimal DONGIAMUAVATTU = Convert.ToDecimal(txtdongia.Text);
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("update tblvattu_dauvao set Iden=@Iden,madh=@madh,Codedetail=@Codedetail "
                           +" ,Tenquicachsp=@Tenquicachsp,Soluongsanpham=@Soluongsanpham "
                           +" ,Donvi_sanpham=@Donvi_sanpham,Ten_vattu=@Ten_vattu,SL_vattucan=@SL_vattucan,KL_vattucan=@KL_vattucan, "
                           +" Donvi_vattu=@Donvi_vattu,SL_vattutonkho=@SL_vattutonkho ,KL_vattutonkho=@KL_vattutonkho, "
                           +" SL_vattumua=@SL_vattumua,KL_vattumua=@KL_vattumua,NCC=@NCC,NguoiGD=@NguoiGD,Dongia=@Dongia,Donviquydoi=@Donviquydoi "
                           +" ,Ngaydat_vattu=Getdate(),NgayDK_ve=@NgayDK_ve,Ghichu_dathangmua=@Ghichu_dathangmua, "
                           + " Ghichu_denghimua=@Ghichu_denghimua,DK_TCmua=@DK_TCmua,VAT=@VAT,quyetdinh=@quyetdinh where CodeVatllieu like '"+txtcodevatlieu.Text+"'", con))
                {
                    cmd.Parameters.Add("@Iden", SqlDbType.Int).Value = txtidentitysanpham.Text;//Số Identity(số định danh tính) sản phẩm 
                    cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = cbmadh.Text;//mã đơn hàng
                    cmd.Parameters.Add("@Codedetail", SqlDbType.NVarChar).Value = txtcodechitiet.Text;// mã chi tiết đh
                    cmd.Parameters.Add("@Tenquicachsp", SqlDbType.NVarChar).Value = txtsanpham.Text;//Sản phẩm quy cách
                    cmd.Parameters.Add("@Soluongsanpham", SqlDbType.Int).Value = SOLUONGSANPHAM;//Số lượng sản phẩm           *
                    cmd.Parameters.Add("@Donvi_sanpham", SqlDbType.NVarChar).Value = txtdonvisanpham.Text;// đơn vị sản phẩm
                    cmd.Parameters.Add("@Ten_vattu", SqlDbType.NVarChar).Value = txtvatlieu_chitiet.Text;// tên vật tư
                    cmd.Parameters.Add("@SL_vattucan", SqlDbType.Int).Value = SoluongVT_CANDAT;//số lượng VT cần              *
                    cmd.Parameters.Add("@KL_vattucan", SqlDbType.Decimal).Value = txtkhoiluongcandat.Text;//KL vật tư cần đặt      
                    cmd.Parameters.Add("@Donvi_vattu", SqlDbType.NVarChar).Value = txtdonvivattu.Text;//Đơn vị vật tư
                    cmd.Parameters.Add("@SL_vattutonkho", SqlDbType.Int).Value = SoluongVT_TONKHO;//SL VT tồn kho             *
                    cmd.Parameters.Add("@KL_vattutonkho", SqlDbType.Decimal).Value = txtKLtonkho.Text;//KL VT tồn kho   
                    cmd.Parameters.Add("@SL_vattumua", SqlDbType.Int).Value = SoluongVT_MUA;//SL VT mua                       *
                    cmd.Parameters.Add("@KL_vattumua", SqlDbType.Decimal).Value = txtkhoiluongmua.Text;//KL VT mua             
                    cmd.Parameters.Add("@NCC", SqlDbType.NVarChar).Value = txtNCC.Text;              // Nhà cung cấp
                    cmd.Parameters.Add("@NguoiGD", SqlDbType.NVarChar).Value = txtnguoigiaodich.Text;//Người GD
                    cmd.Parameters.Add("@Dongia", SqlDbType.Int).Value = DONGIAMUAVATTU; // Đơn giá đặt mua VT                *
                    cmd.Parameters.Add("@Donviquydoi", SqlDbType.NVarChar).Value = txtdonviquidoi.Text;// Đơn vị quy đổi
                    cmd.Parameters.Add("@NgayDK_ve", SqlDbType.Date).Value = dpNgaykhvt.Text;// Ngày DK về
                    cmd.Parameters.Add("@Ghichu_dathangmua", SqlDbType.NVarChar).Value = txtghichudathang.Text;//Ghi chú đặt hàng
                    cmd.Parameters.Add("@Ghichu_denghimua", SqlDbType.NVarChar).Value = txtghichudenghi.Text;// Ghi chú đề nghị mua
                    cmd.Parameters.Add("@DK_TCmua", SqlDbType.NVarChar).Value = txtdieukiengiaodich.Text;//ĐIỀU KIỆN TRAO ĐỔI
                    cmd.Parameters.Add("@VAT", SqlDbType.NVarChar).Value = cbVAT.Text;// XÁC ĐỊNH GIÁ ĐÃ CÓ THUẾ HAY CHƯA THUẾ
                    cmd.Parameters.Add("@quyetdinh", SqlDbType.NVarChar).Value = cbquyetdinhvattu.Text;//QUYẾT ĐỊNH MUA HÀNG HAY SỬ DỤNG TỒN KHO
                    cmd.ExecuteNonQuery();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void frmSuaVTDAUVAO_FormClosing(object sender, FormClosingEventArgs e) // SỰ KIỆN CLODE FORM
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
