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
    public partial class frmLichSanXuat : Form
    {
        public frmLichSanXuat()
        {
            InitializeComponent();
        }

        private void frmLichSanXuat_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            DSDonHangTrienKhai();
        }

        private void btnDMDonHangTrienKhai_Click(object sender, EventArgs e)
        {
            DSDonHangTrienKhai();
        }
        private void DSDonHangTrienKhai()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"Select IdPSX,SPLR,SLSPLR,So_CT,Donvisp,ChatlieuCT,IDSP,Maubv,nvkd,ngaytrienkhai,madh,mabv,sanpham,Mact,Ten_ct,So_CT,soluongyc, 
                                     tonkho, soluongsx, ngoaiquang, donvi, daystar, dayend,
                                     khachhang,KetThucTo_khuon,KetThucTo1,KetThucToRapLD,KetThucTo2, 
                                     KetThucTo3,KetThucTo4,KetThucTo5,KetThucTo6,KetThucTo7, 
                                     KetThucTo8,KetThucTo9,KetThucTo10,KetThucTo11,KetThucTo12,KetThucTo13,KetThucTo14, 
                                     KetThucTo15,KetThucTo16,KetThucTo17,KetThucTo18,KetThucTo19,KetThucTo20,KetThucTo21, 
                                     KetThucToAnMon,KetThucTo_HanBam, 
                                     xeploai,Ghichu,Trangthai from tblchitietkehoach 
                                     where ngaytrienkhai 
                                     between '{0}' and '{1}'",
                                     dptu_ngay.Value.ToString("MM/dd/yyyy"),
                                     dpden_ngay.Value.ToString("MM/dd/yyyy"));
            gridControl3.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
    }
}
