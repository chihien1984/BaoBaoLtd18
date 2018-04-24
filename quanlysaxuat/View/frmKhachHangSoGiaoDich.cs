using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class frmKhachHangSoGiaoDich : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHangSoGiaoDich()
        {
            InitializeComponent();
        }
        //formload
        private void frmKhachHangSoGiaoDich_Load(object sender, EventArgs e)
        {
            dpSoChiTiet_Min.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpSoChiTiet_Max.Text = DateTime.Now.ToString("dd-MM-yyyy");
            ThSoGiaoDichKhachHang();
            ThDanhMucKhachHang();
        }
        private async void ThDanhMucKhachHang()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select TenKH,
                case when DiemBQ is null then '' else DiemBQ end DGDiemBQ,
		        case when Loai is null then '#' else Loai end DGLoai
		        from (select TenKH,DiemBQ,
			    case when DiemBQ >= 3.8 then 'Trung-Thanh'
			    when DiemBQ < 3.8 and DiemBQ>=2 then 'Tiem-Nang'
			    when DiemBQ < 2 then 'Da-mua'
			    end Loai from tblKHACHHANG a
			    left outer join
			    (select CongTyLienHe,
			    sum(DiemBQ)/count(DiemBQ) DiemBQ
			    from KhachHangHoSo group by CongTyLienHe)b
			    on a.TenKH=b.CongTyLienHe)c");
                Invoke((Action)(() =>
                {
                    gridControlDMKhachHang.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private async void ThSoGiaoDichKhachHang()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,NgayLap,NoiDungLienHe,CongTyLienHe,
                    NgayGhi,NguoiGhi,
                    KetQuaLienHePhanHoi,DiemDatHang,
                    DiemPhanHoi,DiemCongNo,DiemDH,DiemPH,DiemCN,DiemBQ,
                    DiemDoanhSo,DiemDaDang,ThongTinNguoiLienHe,ThongTinKhachHang
                    from KhachHangHoSo where NgayLap 
                    between '{0}' and '{1}' and 
                    NguoiGhi like N'{2}'",
                    dpSoChiTiet_Min.Value.ToString("MM-dd-yyyy"),
                    dpSoChiTiet_Max.Value.ToString("MM-dd-yyyy"),
                    MainDev.username);
                Invoke((Action)(() =>
                {
                    gridControlSoGiaoDich.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }

        private void btnTraCuuSoChiTiet_Click(object sender, EventArgs e)
        {
            ThSoGiaoDichKhachHang();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridControlSoGiaoDich.ShowPrintPreview();
        }

        private void btnExportDanhMucKhachHang_Click(object sender, EventArgs e)
        {
            gridControlDMKhachHang.ShowPrintPreview();
        }

        private void btnTraCuuDanhMuc_Click(object sender, EventArgs e)
        {
            ThDanhMucKhachHang();
        }
    }
}