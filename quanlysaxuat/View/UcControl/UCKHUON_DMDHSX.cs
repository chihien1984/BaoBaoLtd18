using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class UCKHUON_DMDHSX : UserControl
    {
        public UCKHUON_DMDHSX()
        {
            InitializeComponent();
        }
        private void LOADDMKHUON_DHSX()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("SELECT Iden,Codedetail,Tenkhachhang,Masp_KH  "
                +" , Tenquicach, madh, Khachhang, CT.MaSP, Ma_khuon, Mact, Manhom_khuon, Ten_khuon "
                +" , Dacdiem_khuon, Soluong_khuon, Ngaylap, Ngaybatdau, Ngayhoanthanh "
                +", Nguoilap, Manv, KH.Ghichu, Ngaycat_Hoanthanh, Ngayrap_Hoanthanh, Mabp "
                +", BPsudung, Nguoiluukhuon, Vitrikhuon, Tinhtrang_khuon "
                +", Ngaylukho "
                +", dvt, Mau_banve, Tonkho "
                +", Soluong, ngaygiao, ngoaiquang, nguoithaydoi, thoigianthaydoi  FROM dbo.tblDHCT CT "
                +"left outer join tblDM_KHUON KH on CT.MaSP = KH.Masp where  convert(nvarchar,thoigianthaydoi,103)  between '" + dptu_ngay.Value.ToString("dd/MM/yyyy") + "' and '" + dpden_ngay.Value.ToString("dd/MM/yyyy") + "'");
        }
        private void UCKHUON_DMDHSX_Load(object sender, EventArgs e)
        {dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();}

        private void show_CTsanpham_Click(object sender, EventArgs e)
        { LOADDMKHUON_DHSX(); }
    }
}
