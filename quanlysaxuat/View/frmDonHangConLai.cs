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
    public partial class frmDonHangConLai : Form
    {
        public frmDonHangConLai()
        {
            InitializeComponent();
        }

        private void btnTraCuDanhMucSanPham_Click(object sender, EventArgs e)
        {
            DocDSSanPham();
        }
        private void DocDSSanPham()
        {
            ketnoi kn = new ketnoi();
        gridControl1.DataSource = kn.laybang(@"select madh,MaPo,Tenquicach,MaSP,Mau_banve,
                Soluong,t.DaGiao,dvt,ngaygiao,ngoaiquang,ghichu,nguoithaydoi
                from tblDHCT c,
                (select IdPSX,max(BTPT11)DaGiao from PHANTICH_TIENDO11 
                where STATUS <>'HOAN THANH' group by IdPSX)t
                where c.Iden=t.IdPSX");
            kn.dongketnoi();
        }
        private void DocCongDoanDongHang()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select k.*,d.* from
                (select madh,MaPo,Tenquicach,MaSP,Mau_banve,
                Soluong,t.DaGiao,dvt,ngaygiao,ngoaiquang,ghichu,nguoithaydoi
                from tblDHCT c,
                (select IdPSX,max(BTPT11)DaGiao from PHANTICH_TIENDO11 
                where STATUS <>'HOAN THANH' group by IdPSX)t
                where c.Iden=t.IdPSX)k
                left outer join
                (select Masp,NguyenCong,Tencondoan,Dinhmuc
                from tblDMuc_LaoDong where Dinhmuc >0 and Masp <>'')d
                on k.MaSP=d.Masp where k.Masp<>''");
            kn.dongketnoi();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void frmDonHangConLai_Load(object sender, EventArgs e)
        {
            dpDonHangTKTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDonHangTKDen.Text = DateTime.Now.ToString("dd/MM/yyyy");

            DocDSSanPham();
            DocCongDoanDongHang();
        }
        private void DocDonDatHangTheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select 
                case when D.MaSP<>'' then 'x' end CoDinhMuc,
	            case when t.IdPSX <>'' then 'x' end DaTrienKhai,C.*
				from
				(select madh,Masp,
				Tenquicach,dvt,Soluong,ngaygiao,
				Khachhang,Mau_banve,Tonkho,ghichu,
				ngoaiquang,pheduyet,Diengiai,nguoithaydoi,Iden from tblDHCT where cast(thoigianthaydoi as date)
				between '{0}' and '{1}')C
                left outer join (select distinct(Masp) MaSP from tblDMuc_LaoDong)D
				on D.MaSP=C.MaSP left outer join
				(select IdPSX from tblchitietkehoach where IdPSX>0 group by idPSX)t
				on C.Iden=t.IdPSX",
                dpDonHangTKTu.Value.ToString("MM-dd-yyyy"),
                dpDonHangTKDen.Value.ToString("MM-dd-yyyy"));
            gridControl3.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void btnTraCongDoanDonHang_Click(object sender, EventArgs e)
        {
            DocCongDoanDongHang();
        }

        private void btnExportCong_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void btnDonHang_TrienKhai_Click(object sender, EventArgs e)
        {
            DocDonDatHangTheoNgay();
        }

        private void btnExportDonHang_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }

        private void btnDocNguyenCong_Click(object sender, EventArgs e)
        {
            DocDonDatHangTheoNgayCongDoan();
        }
        private void DocDonDatHangTheoNgayCongDoan()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select 
                D.Tencondoan,D.SoChiTiet,D.Dinhmuc,
	            case when t.IdPSX <>'' then 'x' end DaTrienKhai,C.*
				from
				(select madh,Masp,
				Tenquicach,dvt,Soluong,ngaygiao,
				Khachhang,Mau_banve,Tonkho,ghichu,
				ngoaiquang,pheduyet,Diengiai,nguoithaydoi,Iden from tblDHCT where cast(thoigianthaydoi as date)
				between '{0}' and '{1}')C
                left outer join (select  Masp MaSP,Tencondoan,SoChiTiet,Dinhmuc from tblDMuc_LaoDong where Dinhmuc>0 and Masp<>'')D
				on D.MaSP=C.MaSP left outer join
				(select IdPSX from tblchitietkehoach where IdPSX>0 group by idPSX)t
				on C.Iden=t.IdPSX",
                dpDonHangTKTu.Value.ToString("MM-dd-yyyy"),
                dpDonHangTKDen.Value.ToString("MM-dd-yyyy"));
            gridControl4.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void CongDoan()
        {
            ketnoi kn = new ketnoi();
            gridControl5.DataSource = kn.laybang(@"select ChiTietSanPham,SoChiTietSanPham,case when TrungCongDoan='x' 
                        then 0 else SoChiTiet*Dongia_CongDoan end  DonGiaBoSanPham,
				SoChiTiet,TrungCongDoan,NguyenCong,id,Ngayghi,LD.Masp,LD.Tensp,Macongdoan,Tencondoan,Dinhmuc,
				Dongia_CongDoan,Tothuchien,Nguoilap,LD.Ngaylap,Trangthai,LD.DonGia_ApDung,
				LD.NgayApDung from tblDMuc_LaoDong LD left outer join tblSANPHAM SP
				on LD.Masp=SP.Masp  where SP.Masp <>'' order by id DESC ");
            kn.dongketnoi();
            gridView1.ExpandAllGroups();
        }

        private void BtnList_CongDoan_Click(object sender, EventArgs e)
        {
            CongDoan();
        }
        private void ChiTiet()
        {
            ketnoi kn = new ketnoi();
            gridControl6.DataSource = kn.laybang(@" select SP.Trongluong,CT.Masp,SP.Tensp,Mact,Ten_ct, 
                                      Soluong_CT, Chatlieu_chitiet, SP.Tensp, Ngaycapnhat, 
                                      CT.Manv, CT.hotennv  from tblSANPHAM_CT CT, tblSANPHAM SP 
                                      where CT.Masp = SP.Masp order by Ngaycapnhat DESC,Mact ASC ");
            gridView2.ExpandAllGroups();
        }

        private void show_CTsanpham_Click(object sender, EventArgs e)
        {
            ChiTiet();
        }

        private void btnExportChiTiet_Click(object sender, EventArgs e)
        {
            gridControl6.ShowPrintPreview();
        }

        private void btnExportNguyenCong_Click(object sender, EventArgs e)
        {
            gridControl5.ShowPrintPreview();
        }
    }

}
