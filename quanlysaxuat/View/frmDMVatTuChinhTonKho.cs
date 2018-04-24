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
using com.google.zxing.qrcode.decoder;

namespace quanlysanxuat
{
    public partial class frmRPNhapXuatVatTu : DevExpress.XtraEditors.XtraForm
    {
        public frmRPNhapXuatVatTu()
        {
            InitializeComponent();
        }
        private void XuatNhapTon(object sender,EventArgs e)
        {
            TheHienXuatNhapTon();
            TraCuTatCaNhap();
            TraCuuTatCaXuat();
        }
        private async void TheHienXuatNhapTon()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from vwDanhMucVatTuChinh");
                Invoke((Action)(() => {
                    grTongHopXuatNhapTonVatTuChinh.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            }
          );
        }
        
        private void TraCuTatCaNhap()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select D.Iden IDDonHang,D.madh,
                Soluongsanpham,Khachhang,AllProgress,a.* from tblNHAP_VATTU a
                left outer join
                tblvattu_dauvao D on 
                D.CodeVatllieu=a.idvattu
                order by Ngay_lap Desc");
            grChiTietNhap.DataSource = kn.laybang(sqlQuery);
            gvChiTietNhap.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void TraCuuTatCaXuat()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select D.Iden IDDonHang,D.madh,
                    Soluongsanpham,a.* from tblXuatKho a
                    left outer join
                    tblvattu_dauvao D on 
                    D.CodeVatllieu=a.idvattu
                    order by Ngaylap Desc");
            grChiTietXuat.DataSource = kn.laybang(sqlStr);
            gvChiTietXuat.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void ChiTietNhap(object sender, EventArgs e)
        {
            ThChiTietNhapTheoThoiGian();
        }
        private async void ThChiTietNhapTheoThoiGian()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select D.Iden IDDonHang,D.madh,
                Soluongsanpham,a.* from tblNHAP_VATTU a
                left outer join
                tblvattu_dauvao D on 
                D.CodeVatllieu=a.idvattu where 
                Ngay_chung_tu between '{0}' and '{1}'",
               dpNhapTu.Value.ToString("yyyy-MM-dd"),
               dpNhapDen.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    grChiTietNhap.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gvChiTietNhap.ExpandAllGroups();
                }));
            }
          );
        }
        private void TraCuuChiTietNhapTheoThoiGian()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select D.Iden IDDonHang,D.madh,
                Soluongsanpham,a.* from tblNHAP_VATTU a
                left outer join
                tblvattu_dauvao D on 
                D.CodeVatllieu=a.idvattu where 
                Ngay_chung_tu between '{0}' and '{1}'",
                dpXuatTu.Value.ToString("yyyy-MM-dd"),
                dpXuatDen.Value.ToString("yyyy-MM-dd"));
            grChiTietNhap.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvChiTietNhap.ExpandAllGroups();
        }
        private void ChiTietXuat(object sender, EventArgs e)
        {
            ThChiTietXuatTheoThoiGian();
        }
        private void TraCuuChiTietXuatTheoThoiGian()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select D.Iden IDDonHang,D.madh,
                    Soluongsanpham,a.* from tblXuatKho a
                    left outer join
                    tblvattu_dauvao D on 
                    D.CodeVatllieu=a.idvattu
                    where Ngayxuat between '{0}' and '{1}' order by Ngaylap Desc",
                    dpXuatTu.Value.ToString("yyyy-MM-dd"),
                    dpXuatDen.Value.ToString("yyyy-MM-dd"));
            grChiTietXuat.DataSource = kn.laybang(sqlStr);
            gvChiTietXuat.ExpandAllGroups();
            kn.dongketnoi();
        }
        private async void ThChiTietXuatTheoThoiGian()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select D.Iden IDDonHang,D.madh,
                    Soluongsanpham,a.* from tblXuatKho a
                    left outer join
                    tblvattu_dauvao D on 
                    D.CodeVatllieu=a.idvattu
                    where Ngayxuat between '{0}' and '{1}'
                    order by Ngaylap Desc",
                    dpXuatTu.Value.ToString("yyyy-MM-dd"),
                    dpXuatDen.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    grChiTietXuat.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gvChiTietXuat.ExpandAllGroups();
                }));
            }
          );
        }
        #region formload
        private void frmRPNhapXuatVatTu_Load(object sender, EventArgs e)
        {
            dpNhapTu.Text= DateTime.Now.ToString("01/MM/yyyy");
            dpNhapDen.Text= DateTime.Now.ToString();
            dpXuatTu.Text= DateTime.Now.ToString("01/MM/yyyy");
            dpXuatDen.Text= DateTime.Now.ToString();
            dpFrom.Text= DateTime.Now.ToString("01/MM/yyyy");
            dpEnd.Text= DateTime.Now.ToString();
            TheHienXuatNhapTon();
            THTonKhoTheoThoiGian();
            //ThXuatNhapTonTheoNgay();
            ThChiTietNhapTheoThoiGian();
            ThChiTietXuatTheoThoiGian();
        }
        #endregion
        private void btnXuatNhapTon_Click(object sender, EventArgs e)
        {
            gvTongHopXuatNhapTonVatTuChinh.ShowPrintPreview();
        }

        private void ExportChiTietNhap_Click(object sender, EventArgs e)
        {
            gvChiTietNhap.ShowPrintPreview();
        }

        private void ExportChiTietXuat_Click(object sender, EventArgs e)
        {
            gvChiTietXuat.ShowPrintPreview();
        }

        private void btnTraCuuTheoThoiGian(object sender, EventArgs e)
        {
            THTonKhoTheoThoiGian();
        }

        private void btnLayout_PSX_Click(object sender, EventArgs e)
        {

        }

        private void btnBaoCaoNgay_Click(object sender, EventArgs e)
        {
            //Model.Function.ConnectSanXuat();//Mo ket noi
            //string sqlQuery = string.Format(@"execute BaoCaoXuatKhoVatTu_proc '{0}'",dpBaoCaoNgay.Value.ToString("MM-dd-yyyy"));
            //grBaoCaoNgay.DataSource = Model.Function.GetDataTable(sqlQuery);
            //Model.Function.Disconnect();//dong ket noi
            //XtratabControl1.SelectedTab = tabPage4;
            ThBaoCaoNgay();
        }

        private async void ThBaoCaoNgay()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoXuatKhoVatTu_proc '{0}'", dpBaoCaoNgay.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() => {
                    grBaoCaoNgay.DataSource = Model.Function.GetDataTable(sqlQuery);
                    XtratabControl1.SelectedTab = tabPage4;
                }));
            }
            );
        }


        private void btnExportBaoCaoNgay_Click(object sender, EventArgs e)
        {
            grBaoCaoNgay.ShowPrintPreview();
        }
        string maVatLieu;
        private void gvTongHopXuatNhapTonVatTuChinh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
        }
        private async void ThChiTietNhapTheoMaVatLieu()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select D.Iden IDDonHang,D.madh,
                Soluongsanpham,a.* from tblNHAP_VATTU a
                left outer join
                tblvattu_dauvao D on 
                D.CodeVatllieu=a.idvattu where 
                Ngay_chung_tu between '{0}' and '{1}'
				and a.Ma_vat_lieu like N'{2}'",
                 dpFrom.Value.ToString("yyyy-MM-dd"),
                 dpEnd.Value.ToString("yyyy-MM-dd"),
                 maVatLieu);
                Invoke((Action)(() => {
                    grChiTietNhap.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gvChiTietNhap.ExpandAllGroups();
                }));
            }
            );
        }
        private async void ThChiTietXuatTheoMaVatLieu()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select D.Iden IDDonHang,D.madh,
                    Soluongsanpham,a.* from tblXuatKho a
                    left outer join
                    tblvattu_dauvao D on 
                    D.CodeVatllieu=a.idvattu
                    where Ngayxuat between '{0}' and '{1}'
					and a.Mavattu like N'{2}'
					order by Ngaylap Desc",
                    dpFrom.Value.ToString("yyyy-MM-dd"),
                    dpEnd.Value.ToString("yyyy-MM-dd"),
                    maVatLieu);
                Invoke((Action)(() => {
                    grChiTietXuat.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gvChiTietXuat.ExpandAllGroups();
                }));
            }
           );
        }

        private void grTongHopXuatNhapTonVatTuChinh_Click(object sender, EventArgs e)
        {

        }

        private void btnTraCuuXuatNhapTonTheoNgay_Click(object sender, EventArgs e)
        {
            THTonKhoTheoThoiGian();
        }
        private async void THTonKhoTheoThoiGian()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"Execute NhapXuatTonKhoVatTuChinhTheoNgay '{0}','{1}'",
                      dpFrom.Value.ToString("yyyy-MM-dd"),
                      dpEnd.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    grNhapXuatTonTheoNgay.DataSource =
                    Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        //function bi lệch số lượng tồng cuối cần kiểm tra lại
        private async void ThXuatNhapTonTheoNgay()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute NhapXuatTonKhoVatTuChinhTheoNgay '{0}','{1}'",
                    dpFrom.Value.ToString("yyyy-MM-dd"),
                    dpEnd.Value.ToString("yyyy-MM-dd"),
                    maVatLieu);
                Invoke((Action)(() => {
                    grNhapXuatTonTheoNgay.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            }
          );
        }

        private void btnExportTheoNgay_Click(object sender, EventArgs e)
        {
            grNhapXuatTonTheoNgay.ShowPrintPreview();
        }

        private void gvNhapXuatTonTheoNgay_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var a = gvNhapXuatTonTheoNgay.
               GetRowCellValue(gvNhapXuatTonTheoNgay.FocusedRowHandle,
               gvNhapXuatTonTheoNgay.Columns["Ma_vl"]);
            if (gvNhapXuatTonTheoNgay.
                GetRowCellValue(gvNhapXuatTonTheoNgay.FocusedRowHandle,
                gvNhapXuatTonTheoNgay.Columns["Ma_vl"]) == null)
            {
                return;
            }
            else
            {
                maVatLieu = gvNhapXuatTonTheoNgay.
                    GetRowCellValue(gvNhapXuatTonTheoNgay.FocusedRowHandle,
                    gvNhapXuatTonTheoNgay.Columns["Ma_vl"]).ToString();
                ThChiTietNhapTheoMaVatLieu();
                ThChiTietXuatTheoMaVatLieu();
            }
        }
    }
}