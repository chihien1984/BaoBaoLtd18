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

namespace quanlysanxuat.View
{
    public partial class frmNhuCauNhanCong : DevExpress.XtraEditors.XtraForm
    {
        public frmNhuCauNhanCong()
        {
            InitializeComponent();
        }
        //formload
        private void frmNhuCauNhanCong_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01/01/yyyy");
            dpDen.Text = DateTime.Now.ToString("dd/MM/yyyy");
            THTongNhuCauLaoDongNam();
            THTongNhuCauLaoDongNamTheoTo();
            ChiTietTongNhuCau();
            TongNhuCau();
        }
        private void THTongNhuCauLaoDongNam()
        {
            xtraTabControl1.SelectedTabPage = xtraTabPageLaoDongTrongNam;
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select 
            sum(Thang01)Thang01,
            sum(Thang02)Thang02,
            sum(Thang03)Thang03,
            sum(Thang04)Thang04,
            sum(Thang05)Thang05,
            sum(Thang06)Thang06,
            sum(Thang07)Thang07,
            sum(Thang08)Thang08,
            sum(Thang09)Thang09,
            sum(Thang10)Thang10,
            sum(Thang11)Thang11,
            sum(Thang12)Thang12
            from
            (select IDTrienKhai,MaDonHang,MaSanPham,SoLuong,BD,KT,
                    thang01*nhancong Thang01,
                    thang02*nhancong Thang02,
                    thang03*nhancong Thang03,
                    thang04*nhancong Thang04,
                    thang05*nhancong Thang05,
                    thang06*nhancong Thang06,
                    thang07*nhancong Thang07,
                    thang08*nhancong Thang08,
                    thang09*nhancong Thang09,
                    thang10*nhancong Thang10,
                    thang11*nhancong Thang11,
                    thang12*nhancong Thang12
                    from 
                    (select idtrienkhai,madonhang,masanpham,soluong,bd,kt,datediff(month,bd,kt)+1 thang,
                    case when month(bd) = '01'						then soluong/(datediff(month,bd,kt)+1) end thang01,
                    case when month(bd)<= '02' and month(kt)>= '02' then soluong/(datediff(month,bd,kt)+1) end thang02,
                    case when month(bd)<= '03' and month(kt)>= '03' then soluong/(datediff(month,bd,kt)+1) end thang03,
                    case when month(bd)<= '04' and month(kt)>= '04' then soluong/(datediff(month,bd,kt)+1) end thang04,
                    case when month(bd)<= '05' and month(kt)>= '05' then soluong/(datediff(month,bd,kt)+1) end thang05,
                    case when month(bd)<= '06' and month(kt)>= '06' then soluong/(datediff(month,bd,kt)+1) end thang06,
                    case when month(bd)<= '07' and month(kt)>= '07' then soluong/(datediff(month,bd,kt)+1) end thang07,
                    case when month(bd)<= '08' and month(kt)>= '08' then soluong/(datediff(month,bd,kt)+1) end thang08,
                    case when month(bd)<= '09' and month(kt)>= '09' then soluong/(datediff(month,bd,kt)+1) end thang09,
                    case when month(bd)<= '10' and month(kt)>= '10' then soluong/(datediff(month,bd,kt)+1) end thang10,
                    case when month(bd)<= '11' and month(kt)>= '11' then soluong/(datediff(month,bd,kt)+1) end thang11,
                    case when month(bd)<= '12' and month(kt)>= '12' then soluong/(datediff(month,bd,kt)+1) end thang12
                    from (select max(IDTrienKhai)IDTrienKhai,
                    MaDonHang,MaSanPham,
                    min(batdau)BD,max(ketthuc)KT,SoLuongYCSanXuat soluong
                    from trienkhaikehoachsanxuat
		            where macongdoan <>'' 
		            and MaSanPham <> 'TEM-NUL-000'
                    and batdau <>''
		            and KetThuc <>''
		            and year(batdau)=year('{0}')
                    and year(ketthuc)=year('{0}')
		            and MaCongDoan like 'GHA'
                    group by MaDonHang,MaSanPham,SoLuongYCSanXuat)n)a
                    left outer join
                    (select masp,sum(nhancong)nhancong
                    from viewdinhmucnhancong
                    group by masp)b
                    on a.masanpham=b.masp)x
		            group by year(BD)",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            grLaoDongTrongNam.DataSource = Model.Function.GetDataTable(sqlQuery);
        }

        private void btnTraCuuTongLaoDongTrongNam_Click(object sender, EventArgs e)
        {
            THTongNhuCauLaoDongNam();
        }

        private void btnTraCuuTheoToThucHien_Click(object sender, EventArgs e)
        {
            THTongNhuCauLaoDongNamTheoTo();
        }
        private void THTongNhuCauLaoDongNamTheoTo()
        {
            //xtraTabControl1.SelectedTabPage = xtraTabPageChiTietLaoDongTrongNamTheoTo;
            //Model.Function.ConnectSanXuat();
            //string sqlQuery = string.Format(@"select Thang,Tothuchien,sum(c.TongNC)TongNguoi from
            //    (select Thang,b.Tothuchien,a.MaSanPham,a.TenSanPham,(a.SoLuongSanXuat*b.NhanCong)TongNC from
            //    (select month(KetThuc)Thang,MaSanPham,TenSanPham,
            //    sum(SoLuongYCSanXuat)SoLuongSanXuat
            //    from TrienKhaiKeHoachSanXuat 
            //    where 
            //    KetThuc between '{0}' and '{1}' 
            //    and BatDau between '{0}' and '{1}'
            //    and MaCongDoan like 'GHA'
            //    group by month(KetThuc),MaSanPham,TenSanPham)a
            //    left outer join
            //    (select Masp,sum(NhanCong)NhanCong,Tothuchien
            //    from viewDinhMucNhanCong group by Masp,Tothuchien)b
            //    on a.MaSanPham=b.Masp)c where TongNC <>''
            //    group by Thang,Tothuchien
            //    order by Thang desc",
            //    dpTu.Value.ToString("yyyy-MM-dd"),
            //    dpDen.Value.ToString("yyyy-MM-dd"));
            //grLaoDongCanThietCacTo.DataSource = Model.Function.GetDataTable(sqlQuery);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTongNhuCau_Click(object sender, EventArgs e)
        {
            TongNhuCau();
        }
        private void TongNhuCau()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select IDTrienKhai,MaDonHang,MaSanPham,SoLuong,BD,KT,
Thang01*NhanCong Thang01,
Thang02*NhanCong Thang02,
Thang03*NhanCong Thang03,
Thang04*NhanCong Thang04,
Thang05*NhanCong Thang05,
Thang06*NhanCong Thang06,
Thang07*NhanCong Thang07,
Thang08*NhanCong Thang08,
Thang09*NhanCong Thang09,
Thang10*NhanCong Thang10,
Thang11*NhanCong Thang11,
Thang12*NhanCong Thang12
from 
(select IDTrienKhai,MaDonHang,MaSanPham,SoLuong,BD,KT,datediff(month,BD,KT)+1 Thang,
case when month(BD) = '01' then SoLuong/(datediff(month,BD,KT)+1) end Thang01,
case when month(BD)<= '02' and month(KT)>= '02' then SoLuong/(datediff(month,BD,KT)+1) end Thang02,
case when month(BD)<= '03' and month(KT)>= '03' then SoLuong/(datediff(month,BD,KT)+1) end Thang03,
case when month(BD)<= '04' and month(KT)>= '04' then SoLuong/(datediff(month,BD,KT)+1) end Thang04,
case when month(BD)<= '05' and month(KT)>= '05' then SoLuong/(datediff(month,BD,KT)+1) end Thang05,
case when month(BD)<= '06' and month(KT)>= '06' then SoLuong/(datediff(month,BD,KT)+1) end Thang06,
case when month(BD)<= '07' and month(KT)>= '07' then SoLuong/(datediff(month,BD,KT)+1) end Thang07,
case when month(BD)<= '08' and month(KT)>= '08' then SoLuong/(datediff(month,BD,KT)+1) end Thang08,
case when month(BD)<= '09' and month(KT)>= '09' then SoLuong/(datediff(month,BD,KT)+1) end Thang09,
case when month(BD)<= '10' and month(KT)>= '10' then SoLuong/(datediff(month,BD,KT)+1) end Thang10,
case when month(BD)<= '11' and month(KT)>= '11' then SoLuong/(datediff(month,BD,KT)+1) end Thang11,
case when month(BD)<= '12' and month(KT)>= '12' then SoLuong/(datediff(month,BD,KT)+1) end Thang12
from (select max(IDTrienKhai)IDTrienKhai,
        MaDonHang,MaSanPham,
        min(batdau)BD,max(ketthuc)KT,SoLuongYCSanXuat soluong
        from trienkhaikehoachsanxuat
		where macongdoan <>'' 
		and MaSanPham <> 'TEM-NUL-000'
        and batdau <>''
		and KetThuc <>''
		and year(batdau)=year(getdate())
        and year(ketthuc)=year(getdate())
		and MaCongDoan like 'GHA'
        group by MaDonHang,MaSanPham,SoLuongYCSanXuat)n)a
left outer join
(select Masp,sum(NhanCong)NhanCong
from viewDinhMucNhanCong
group by Masp)b
on a.MaSanPham=b.Masp");
            grTongNhuCauTrongNam.DataSource = Model.Function.GetDataTable(sqlQuery);
        }

        private void btnChiTietTongNhuCau_Click(object sender, EventArgs e)
        {
            ChiTietTongNhuCau();
        }
        private void ChiTietTongNhuCau()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select IDTrienKhai,Tothuchien,MaDonHang,MaSanPham,SoLuong,BD,KT,
        thang01*nhancong Thang01,
        thang02*nhancong Thang02,
        thang03*nhancong Thang03,
        thang04*nhancong Thang04,
        thang05*nhancong Thang05,
        thang06*nhancong Thang06,
        thang07*nhancong Thang07,
        thang08*nhancong Thang08,
        thang09*nhancong Thang09,
        thang10*nhancong Thang10,
        thang11*nhancong Thang11,
        thang12*nhancong Thang12
        from 
        (select idtrienkhai,madonhang,masanpham,soluong,bd,kt,datediff(month,bd,kt)+1 thang,
        case when month(bd) = '01'						then soluong/(datediff(month,bd,kt)+1) end thang01,
        case when month(bd)<= '02' and month(kt)>= '02' then soluong/(datediff(month,bd,kt)+1) end thang02,
        case when month(bd)<= '03' and month(kt)>= '03' then soluong/(datediff(month,bd,kt)+1) end thang03,
        case when month(bd)<= '04' and month(kt)>= '04' then soluong/(datediff(month,bd,kt)+1) end thang04,
        case when month(bd)<= '05' and month(kt)>= '05' then soluong/(datediff(month,bd,kt)+1) end thang05,
        case when month(bd)<= '06' and month(kt)>= '06' then soluong/(datediff(month,bd,kt)+1) end thang06,
        case when month(bd)<= '07' and month(kt)>= '07' then soluong/(datediff(month,bd,kt)+1) end thang07,
        case when month(bd)<= '08' and month(kt)>= '08' then soluong/(datediff(month,bd,kt)+1) end thang08,
        case when month(bd)<= '09' and month(kt)>= '09' then soluong/(datediff(month,bd,kt)+1) end thang09,
        case when month(bd)<= '10' and month(kt)>= '10' then soluong/(datediff(month,bd,kt)+1) end thang10,
        case when month(bd)<= '11' and month(kt)>= '11' then soluong/(datediff(month,bd,kt)+1) end thang11,
        case when month(bd)<= '12' and month(kt)>= '12' then soluong/(datediff(month,bd,kt)+1) end thang12
        from (select max(IDTrienKhai)IDTrienKhai,
        MaDonHang,MaSanPham,
        min(batdau)BD,max(ketthuc)KT,SoLuongYCSanXuat soluong
        from trienkhaikehoachsanxuat
		where macongdoan <>'' 
		and MaSanPham <> 'TEM-NUL-000'
        and batdau <>''
		and KetThuc <>''
		and year(batdau)=year(getdate())
        and year(ketthuc)=year(getdate())
		and MaCongDoan like 'GHA'
        group by MaDonHang,MaSanPham,SoLuongYCSanXuat)n)a
        left outer join
        (select masp,tothuchien,sum(NhanCong)nhancong
        from viewdinhmucnhancong
		where NhanCong >0 and Tothuchien<>''
        group by masp,tothuchien)b
        on a.masanpham=b.masp
        where Tothuchien <>''");
            grNhuCauTrongNamTheoTo.DataSource = Model.Function.GetDataTable(sqlQuery);
        }

        private void btnNhanSuCanTo_Click(object sender, EventArgs e)
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select Tothuchien,
        sum(Thang01)Thang01,
        sum(Thang02)Thang02,
        sum(Thang03)Thang03,
        sum(Thang04)Thang04,
        sum(Thang05)Thang05,
        sum(Thang06)Thang06,
        sum(Thang07)Thang07,
        sum(Thang08)Thang08,
        sum(Thang08)Thang09,
        sum(Thang10)Thang10,
        sum(Thang11)Thang11,
        sum(Thang12)Thang12
        from
        (select IDTrienKhai,Tothuchien,MaDonHang,MaSanPham,SoLuong,BD,KT,
        thang01*nhancong Thang01,
        thang02*nhancong Thang02,
        thang03*nhancong Thang03,
        thang04*nhancong Thang04,
        thang05*nhancong Thang05,
        thang06*nhancong Thang06,
        thang07*nhancong Thang07,
        thang08*nhancong Thang08,
        thang09*nhancong Thang09,
        thang10*nhancong Thang10,
        thang11*nhancong Thang11,
        thang12*nhancong Thang12
        from 
        (select idtrienkhai,madonhang,masanpham,soluong,bd,kt,datediff(month,bd,kt)+1 thang,
        case when month(bd) = '01'						then soluong/(datediff(month,bd,kt)+1) end thang01,
        case when month(bd)<= '02' and month(kt)>= '02' then soluong/(datediff(month,bd,kt)+1) end thang02,
        case when month(bd)<= '03' and month(kt)>= '03' then soluong/(datediff(month,bd,kt)+1) end thang03,
        case when month(bd)<= '04' and month(kt)>= '04' then soluong/(datediff(month,bd,kt)+1) end thang04,
        case when month(bd)<= '05' and month(kt)>= '05' then soluong/(datediff(month,bd,kt)+1) end thang05,
        case when month(bd)<= '06' and month(kt)>= '06' then soluong/(datediff(month,bd,kt)+1) end thang06,
        case when month(bd)<= '07' and month(kt)>= '07' then soluong/(datediff(month,bd,kt)+1) end thang07,
        case when month(bd)<= '08' and month(kt)>= '08' then soluong/(datediff(month,bd,kt)+1) end thang08,
        case when month(bd)<= '09' and month(kt)>= '09' then soluong/(datediff(month,bd,kt)+1) end thang09,
        case when month(bd)<= '10' and month(kt)>= '10' then soluong/(datediff(month,bd,kt)+1) end thang10,
        case when month(bd)<= '11' and month(kt)>= '11' then soluong/(datediff(month,bd,kt)+1) end thang11,
        case when month(bd)<= '12' and month(kt)>= '12' then soluong/(datediff(month,bd,kt)+1) end thang12
        from (select max(IDTrienKhai)IDTrienKhai,
        MaDonHang,MaSanPham,
        min(batdau)BD,max(ketthuc)KT,SoLuongYCSanXuat soluong
        from trienkhaikehoachsanxuat
		where macongdoan <>'' 
		and MaSanPham <> 'TEM-NUL-000'
        and batdau <>''
		and KetThuc <>''
		and year(batdau)=year(getdate())
        and year(ketthuc)=year(getdate())
		and MaCongDoan like 'GHA'
        group by MaDonHang,MaSanPham,SoLuongYCSanXuat)n)a
        left outer join
        (select masp,tothuchien,sum(NhanCong)nhancong
        from viewdinhmucnhancong
		where NhanCong >0 and Tothuchien<>''
        group by masp,tothuchien)b
        on a.masanpham=b.masp
		where Tothuchien <>'')p group by Tothuchien");
            grNhanSuCan.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
    }
}