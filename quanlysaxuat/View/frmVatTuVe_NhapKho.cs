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
    public partial class frmVatTuVe_NhapKho : DevExpress.XtraEditors.XtraForm
    {
       
        string Gol = "";
        public frmVatTuVe_NhapKho()
        {
            InitializeComponent();
        }
        public void LoadDataVattu()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT CodeVatllieu,Iden,madh,Codedetail "
                    + ", Tenquicachsp, Soluongsanpham, Donvi_sanpham, Ten_vattu "
                    + ", SL_vattucan, KL_vattucan, SL_vattutonkho, KL_vattutonkho, SL_vattumua, KL_vattumua "
                    + ", Donvi_vattu, NCC, NguoiGD, Dongia, Donviquydoi, Ngaydat_vattu, NgayDK_ve, Ngayve_TT, "
                    + "SL_vattuve, KL_vattuve, SL_tinhgia, Dvt_gia, Ghichu_dathangmua, Ghichu_denghimua, "
                    + "DK_TCmua, VAT, quyetdinh, nhanviendathang, nguoikiemkho, ngaykiemkho, nguoinhap, ngaynhap "
                    + " FROM dbo.tblvattu_dauvao");
        }
        private void Trusoluongtonkho()// TRỪ SỐ LƯỢNG VẬT TƯ TỒN KHO
        {
            //decimal KLtonkho = Convert.ToDecimal(txtKLtonkho.Text);
            double SLCD = double.Parse(txtSLcandat.Text);
            double KLCD = double.Parse(txtkhoiluongcandat.Text);
            double SLTK = double.Parse(txtSLtonkho.Text);
            double KLTK = double.Parse(txtKLtonkho.Text);
            double STMUA = SLCD - SLTK;
            double KLMUA = KLCD - KLTK;
            txtsoluongmua.Text = Convert.ToString(STMUA);
            txtkhoiluongmua.Text = Convert.ToString(KLMUA);
        }
        private void frmVatTuVe_NhapKho_Load(object sender, EventArgs e)
        {
            txtnguoinhap.Text = Login.Username;
        }

        private void btnVatTuVe_NhapKho_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            decimal SOLUONGSANPHAM = Convert.ToDecimal(txtsoluongsp.Text);//ĐỔI DỮ LIỆU TEXTBOX SANG DECIMAL SAVE VÀO SQL
            decimal SoluongVT_CANDAT = Convert.ToDecimal(txtSLcandat.Text);
            decimal SoluongVT_TONKHO = Convert.ToDecimal(txtSLtonkho.Text);
            decimal SoluongVT_MUA = Convert.ToDecimal(txtsoluongmua.Text);
            //decimal DONGIAMUAVATTU = Convert.ToDecimal(txtdongia.Text);
            con.ConnectionString = ConfigurationManager.ConnectionStrings["project"].ConnectionString;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE tblvattu_dauvao SET "
                           + " SL_vattucan=@SL_vattucan,KL_vattucan=@KL_vattucan,SL_vattutonkho=@SL_vattutonkho"
                           + " ,KL_vattutonkho=@KL_vattutonkho,SL_vattumua=@SL_vattumua,KL_vattumua=@KL_vattumua)", con))
                {
                    //cmd.Parameters.Add("@Iden", SqlDbType.Int).Value = txtidentitysanpham.Text;//Số Identity(số định danh tính) sản phẩm 
                    //cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = cbmadh.Text;//mã đơn hàng
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
                    //cmd.Parameters.Add("@NguoiGD", SqlDbType.NVarChar).Value = txtnguoigiaodich.Text;//Người GD
                    //cmd.Parameters.Add("@Dongia", SqlDbType.Int).Value = DONGIAMUAVATTU; // Đơn giá đặt mua VT                *
                    //cmd.Parameters.Add("@Donviquydoi", SqlDbType.NVarChar).Value = txtdonviquidoi.Text;// Đơn vị quy đổi
                    cmd.Parameters.Add("@NgayDK_ve", SqlDbType.Date).Value = dpNgaykhvt.Text;// Ngày DK về
                    //cmd.Parameters.Add("@Ghichu_dathangmua", SqlDbType.NVarChar).Value = txtghichudathang.Text;//Ghi chú đặt hàng
                    //cmd.Parameters.Add("@Ghichu_denghimua", SqlDbType.NVarChar).Value = txtghichudenghi.Text;// Ghi chú đề nghị mua
                    cmd.Parameters.Add("@DK_TCmua", SqlDbType.NVarChar).Value = txtdieukiengiaodich.Text;//ĐIỀU KIỆN TRAO ĐỔI
                    //cmd.Parameters.Add("@VAT", SqlDbType.NVarChar).Value = cbVAT.Text;// XÁC ĐỊNH GIÁ ĐÃ CÓ THUẾ HAY CHƯA THUẾ
                    cmd.Parameters.Add("@quyetdinh", SqlDbType.NVarChar).Value = cbquyetdinhvattu.Text;//QUYẾT ĐỊNH MUA HÀNG HAY SỬ DỤNG TỒN KHO
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtcodevatlieu.Text = gridView1.GetFocusedRowCellDisplayText(idvatlieu1);
            txtidentitysanpham.Text = gridView1.GetFocusedRowCellDisplayText(Idensanpham1);
            cbmadh.Text = gridView1.GetFocusedRowCellDisplayText(mdh1);
            txtcodechitiet.Text = gridView1.GetFocusedRowCellDisplayText(mact1);
            txtsanpham.Text = gridView1.GetFocusedRowCellDisplayText(tenquicachchitiet1);
            txtsoluongsp.Text = gridView1.GetFocusedRowCellDisplayText(soluongsp1);
            txtdonvisanpham.Text = gridView1.GetFocusedRowCellDisplayText(donvisp1);
            txtvatlieu_chitiet.Text = gridView1.GetFocusedRowCellDisplayText(tenchitietvattu1);
            txtSLcandat.Text = gridView1.GetFocusedRowCellDisplayText(soluongcan1);
            txtkhoiluongcandat.Text = gridView1.GetFocusedRowCellDisplayText(khoiluongcan1);
            txtSLtonkho.Text = gridView1.GetFocusedRowCellDisplayText(soluongtonkho1);
            txtKLtonkho.Text = gridView1.GetFocusedRowCellDisplayText(khoiluongtonkho1);
            txtsoluongmua.Text = gridView1.GetFocusedRowCellDisplayText(soluongmua1);
            txtkhoiluongmua.Text = gridView1.GetFocusedRowCellDisplayText(khoiluongmua1);
            txtdonvivattu.Text = gridView1.GetFocusedRowCellDisplayText(donvivattu1);
            txtNCC.Text = gridView1.GetFocusedRowCellDisplayText(donvicungcap1);
            dpNgaykhvt.Text = gridView1.GetFocusedRowCellDisplayText(ngaydukienve1);
            txtsoluongtinhgia.Text = gridView1.GetFocusedRowCellDisplayText(soluongtinhgia1);
            txtdonvitinhgia.Text = gridView1.GetFocusedRowCellDisplayText(donvitinhgia1);
            txtdieukiengiaodich.Text = gridView1.GetFocusedRowCellDisplayText(tieuchuandieukienmua1);
            cbquyetdinhvattu.Text = gridView1.GetFocusedRowCellDisplayText(quyetdinh1);
            txtnguoikiemkho.Text = gridView1.GetFocusedRowCellDisplayText(nguoikiemkho1);
            dpngaykiemkho.Text = gridView1.GetFocusedRowCellDisplayText(ngaykiemkho1);
        }
    }
}
