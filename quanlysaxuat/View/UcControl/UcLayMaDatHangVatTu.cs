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
    public partial class UcLayMaDatHangVatTu : DevExpress.XtraEditors.XtraForm
    {
        ketnoi kn = new ketnoi();
        string Gol = "";
        public UcLayMaDatHangVatTu()
        {
            InitializeComponent();
        }

        private void LoadCTDH()
        {
       ketnoi Connect = new ketnoi();
       gridControl1.DataSource = Connect.laybang("SELECT CodeVatllieu,Purchase_order,Iden,madh,Codedetail  "
       +" , Tenquicachsp, Soluongsanpham, Donvi_sanpham, Ten_vattu, SL_vattucan, KL_vattucan, SL_vattutonkho, KL_vattutonkho, SL_vattumua "
       +" , KL_vattumua, Donvi_vattu, NCC, NguoiGD, Dongia, Donviquydoi, Ngaydat_vattu, NgayDK_ve, Ngayve_TT, SL_vattuve, "
       +" KL_vattuve, SL_tinhgia, Dvt_gia, Ghichu_dathangmua, Ghichu_denghimua, DK_TCmua, VAT, quyetdinh, nhanviendathang, "
       +" nguoikiemkho, ngaykiemkho, nguoinhap, ngaynhap, fax, NVKD, CodePO, NgayDinhViDatVatTu "
       +"  FROM dbo.tblvattu_dauvao");
        }
        private void LoadCHITIET_VATTU() {
            ketnoi Connect = new ketnoi();
            gridControl1.DataSource = Connect.laybang("");
        }


        private void btnlaymadonhang_Click(object sender, EventArgs e)
        {
            if (txtvatlieu_chitiet.Text != "")// UPDATE NGÀY GIỜ ĐẶT VẬT TƯ ĐÃ CHỌN VÀO DANH MỤC ĐẶT HÀNG 
            {
               kn.xulydulieu(" update tblvattu_dauvao set Ngaydat_vattu = "
                           + " (CONVERT(VARCHAR(10), GETDATE(), 103) + ' ' + convert(VARCHAR(8), GETDATE(), 14)) "
                           + " where CodeVatllieu like '" + txtcodevatlieu.Text + "'");
            }
            else
            {
                MessageBox.Show("Cần có đầy đủ thông tin để Ứng dụng tự thêm đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void themmadatvattu_tudong()
        {
            kn.xulydulieu(" update tblvattu_dauvao set tblvattu_dauvao.Purchase_order = DatHangVatTuDauVao.Purchase_order "
                          +" from tblvattu_dauvao, DatHangVatTuDauVao where tblvattu_dauvao.CodeVatllieu = DatHangVatTuDauVao.CodeVatllieu");
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtcodevatlieu.Text = gridView1.GetFocusedRowCellDisplayText(idvatlieu1);
            //txtidentitysanpham.Text = gridView1.GetFocusedRowCellDisplayText(Idensanpham1);
            //cbmadh.Text = gridView1.GetFocusedRowCellDisplayText(mdh1);
            //txtcodechitiet.Text = gridView1.GetFocusedRowCellDisplayText(mact1);
            //txtsanpham.Text = gridView1.GetFocusedRowCellDisplayText(tenquicachchitiet1);
            //txtsoluongsp.Text = gridView1.GetFocusedRowCellDisplayText(soluongsp1);
            //txtdonvisanpham.Text = gridView1.GetFocusedRowCellDisplayText(donvisp1);
            txtvatlieu_chitiet.Text = gridView1.GetFocusedRowCellDisplayText(tenchitietvattu1);
            txtSLcandat.Text = gridView1.GetFocusedRowCellDisplayText(soluongcan1);
            txtkhoiluongcandat.Text = gridView1.GetFocusedRowCellDisplayText(khoiluongcan1);
            txtSLtonkho.Text = gridView1.GetFocusedRowCellDisplayText(soluongtonkho1);
            txtKLtonkho.Text = gridView1.GetFocusedRowCellDisplayText(khoiluongtonkho1);
            txtsoluongmua.Text = gridView1.GetFocusedRowCellDisplayText(soluongmua1);
            txtkhoiluongmua.Text = gridView1.GetFocusedRowCellDisplayText(khoiluongmua1);
            txtdonvivattu.Text = gridView1.GetFocusedRowCellDisplayText(donvivattu1);
            txtNCC.Text = gridView1.GetFocusedRowCellDisplayText(donvicungcap1);
            txtnguoigiaodich.Text = gridView1.GetFocusedRowCellDisplayText(nguoigiaodich1);
            txtdonviquidoi.Text = gridView1.GetFocusedRowCellDisplayText(donviquidoi1);
            dpNgaydatvattu.Text = gridView1.GetFocusedRowCellDisplayText(ngaydathang1);
            dpNgaykhvt.Text = gridView1.GetFocusedRowCellDisplayText(ngaydukienve1);
            //dpngaynhapvattu.Text = gridView1.GetFocusedRowCellDisplayText(ngayvethucte1);
            //txtsoluongthucnhap.Text = gridView1.GetFocusedRowCellDisplayText(soluongve1);
            //txttronluongthucnhap.Text = gridView1.GetFocusedRowCellDisplayText(khoiluongve1);
            //txtsoluongtinhgia.Text = gridView1.GetFocusedRowCellDisplayText(soluongtinhgia1);
            //txtdonvitinhgia.Text = gridView1.GetFocusedRowCellDisplayText(donvitinhgia1);
            txtghichudathang.Text = gridView1.GetFocusedRowCellDisplayText(ghichudathang1);
            txtghichudenghi.Text = gridView1.GetFocusedRowCellDisplayText(ghichudenghi1);
            txtdieukiengiaodich.Text = gridView1.GetFocusedRowCellDisplayText(tieuchuandieukienmua1);
            cbVAT.Text = gridView1.GetFocusedRowCellDisplayText(thuegtgt1);
            cbquyetdinhvattu.Text = gridView1.GetFocusedRowCellDisplayText(quyetdinh1);
            //txtnguoidatmuahang.Text = gridView1.GetFocusedRowCellDisplayText(nhanviendathang1);
            txtnguoikiemkho.Text = gridView1.GetFocusedRowCellDisplayText(nguoikiemkho1);
            dpngaykiemkho.Text = gridView1.GetFocusedRowCellDisplayText(ngaykiemkho1);
            //txtnguoinhap.Text = gridView1.GetFocusedRowCellDisplayText(nguoinhap1);
            //dpngaynhapvattu.Text = gridView1.GetFocusedRowCellDisplayText(ngaynhap1);
        }

        private void viewdh_Click(object sender, EventArgs e)
        {

        }

        private void btnChiTiet_DatVatTu_Click(object sender, EventArgs e)
        {

        }
    }
}
