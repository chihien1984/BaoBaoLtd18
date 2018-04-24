using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class UcDinhMucCongDoan : DevExpress.XtraEditors.XtraForm
    {
        public UcDinhMucCongDoan()
        {
            InitializeComponent();
        }

        private void BtnList_CongDoan_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select 
                id,Ngayghi,LD.Masp,LD.Tensp,Macongdoan,Tencondoan,Dinhmuc,
                Dongia_CongDoan,Tothuchien,Nguoilap,LD.Ngaylap,Trangthai,LD.DonGia_ApDung,
                LD.NgayApDung from tblDMuc_LaoDong LD left outer join tblSANPHAM SP
				on LD.Masp=SP.Masp");
            kn.dongketnoi();
            gridView1.ExpandAllGroups();
        }

        private void BtnXuatPhieu_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //ketnoi kn = new ketnoi();
            //dt = kn.laybang("select * from tblDMuc_LaoDong where Masp like N'" + txtMasp.Text + "'");
            //XRCongDoan CongDoan = new XRCongDoan();
            //CongDoan.DataSource = dt;
            //CongDoan.DataMember = "Table";
            //CongDoan.ShowPreviewDialog();
            //kn.dongketnoi();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            //string Gol;
            //Gol = gridView1.GetFocusedDisplayText();
            //txtMasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
        }

        private void frmDinhMucCongDoan_Load(object sender, EventArgs e)
        {
            //txtDonHangID.Text = DonHangID;
            //BtnList_CongDoan_Click(sender,e);
            //DocDonHangTheoID();
        }
        private void DocDonHangTheoID()
        {
            //ketnoi kn = new ketnoi();
            //gridControl2.DataSource = kn.laybang(@"select CT.Iden,CT.madh,CD.Masp,CT.Tenquicach,Tencondoan,Dinhmuc,CT.Soluong,
            //        CAST(CT.Soluong/
            //        (case when Dinhmuc>0 then Dinhmuc end) as float) GioLam,CD.NguyenCong,CD.Tothuchien
            //        from tblDMuc_LaoDong CD
            //        left outer join tblDHCT CT
            //        on CD.Masp=CT.MaSP where Iden like '"+txtDonHangID.Text+"' ");
            //kn.dongketnoi();
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 = new frmLoading(maSanPham, path.pathbanve);
            f2.Show();
        }

        private void btnXuatvlphu_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void btnloadgrid1_Click(object sender, EventArgs e)
        {
            THCongDonHang();
        }
        private void THCongDonHang()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select DonGiaHeSo,HeSo,ChiTietSanPham,SoChiTietSanPham,
                        case when TrungCongDoan='x' 
                        then 0 else SoChiTiet*Dongia_CongDoan end  DonGiaBoSanPham,
				        SoChiTiet,TrungCongDoan,NguyenCong,id,Ngayghi,LD.Masp,LD.Tensp,Macongdoan,Tencondoan,Dinhmuc,
				        Dongia_CongDoan,Tothuchien,Nguoilap,LD.Ngaylap,Trangthai,LD.DonGia_ApDung,
				        LD.NgayApDung from tblDMuc_LaoDong LD left outer join tblSANPHAM SP
				        on LD.Masp=SP.Masp");
            grDanhMucCongDoan.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
            gvDanhMucCongDoan.ExpandAllGroups();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            //string Gol;
            //Gol = gridView2.GetFocusedDisplayText();
            //txtMasp.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
        }
        //formload
        private void UcDinhMucCongDoan_Load(object sender, EventArgs e)
        {
            THCongDonHang();
        }
        string maSanPham;
        private void grDanhMucCongDoan_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gvDanhMucCongDoan.GetFocusedDisplayText();
            maSanPham = gvDanhMucCongDoan.GetFocusedRowCellDisplayText(Masp_grid1);
        }
    }
}
