using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace quanlysanxuat
{
    public partial class XRNguyenCong : DevExpress.XtraReports.UI.XtraReport
    {
        public static string maDonHang;
        public static string soLuongDonHang;
        public XRNguyenCong()
        {
            InitializeComponent();
        }

        private void XRNguyenCong_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            xrMaDonHang.Text = maDonHang;
            xrSoLuongSanXuat.Text = soLuongDonHang;
        }

        private void XRNguyenCong_ParametersRequestSubmit(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            //xrMaDonHang.Text = maDonHang;
            //xrSoLuongDH.Text = soLuongDonHang;
        }
    }
}
