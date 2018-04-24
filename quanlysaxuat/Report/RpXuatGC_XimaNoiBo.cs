using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.BarCode;

namespace quanlysanxuat
{
    public partial class RpXuatGC_XimaNoiBo : DevExpress.XtraReports.UI.XtraReport
    {
        public RpXuatGC_XimaNoiBo()
        {
            InitializeComponent();
        }

        private void RpXuatGC_XimaNoiBo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.Detail.Controls.Add(TaoQRCode());
        }
        //public XRBarCode TaoQRCode()
        //{
        //    //// Tạo 1 barcode mới
        //    //XRBarCode barCode = new XRBarCode();

        //    //// Đặt cho nó là kiểu QRCode.
        //    //barCode.Symbology = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();

        //    ////Đưa dữ liệu vào mã QRcode
        //    //const string input = "";

        //    ////Chuyển dữ liệu qua dạng mãng byte
        //    //byte[] array = System.Text.Encoding.ASCII.GetBytes(input);

        //    ////Gán mãng byte vào barcode
        //    //barCode.BinaryData = array;

        //    ////Đặt thêm một số thuộc tính cho barcode
        //    //barCode.Width = 400;
        //    //barCode.Height = 150;
        //    //barCode.AutoModule = true;
        //    //((QRCodeGenerator)barCode.Symbology).CompactionMode = QRCodeCompactionMode.Byte;
        //    //((QRCodeGenerator)barCode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
        //    //((QRCodeGenerator)barCode.Symbology).Version = QRCodeVersion.AutoVersion;
        //    //return barCode;
        //}

    }
}

