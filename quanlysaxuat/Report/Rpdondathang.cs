using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using quanlysanxuat.Model;
using System.Data;
using System.Linq;

namespace quanlysanxuat
{
    public partial class Rpdondathang : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpdondathang()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Rpdondathang_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sqlQueryFinal = string.Format(@"select sum(ThanhTien)ThanhTien,madh
                from tblDHCT where 
                madh like N'{0}'
                group by madh", UcDONDH.madonHang);
            var dtReadNum = Function.GetDataTable(sqlQueryFinal);
            if (dtReadNum == null) { return; }
            else if (dtReadNum != null)
            {
                var dataRow = dtReadNum.AsEnumerable().Where(x => x.Field<string>("madh") == UcDONDH.madonHang).FirstOrDefault();
                dataRow["ThanhTien"].ToString();
                Clsdocsothanhchu docSo = new Clsdocsothanhchu();
                string thanhChu = docSo.NumberToTextVN(Convert.ToDecimal(dataRow["ThanhTien"].ToString()));
                xrTableCellDocSoThanhChu.Text = thanhChu;
            }
        }
    }
}
