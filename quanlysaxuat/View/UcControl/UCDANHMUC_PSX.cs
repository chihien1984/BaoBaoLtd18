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
    public partial class UCDANHMUC_PSX : UserControl
    {
        public UCDANHMUC_PSX()
        {
            InitializeComponent();
        }
        private void LoaDH_CTTime()
        {
            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.laybang("select * from tblDHCT  where convert(nvarchar,thoigianthaydoi,111) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            Connect.dongketnoi();
        }
        private void LoaDH_CT()
        {
            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.laybang("select * from tblDHCT ");
            Connect.dongketnoi();
        }
        private void LoaDH_CTTime(object sender,EventArgs e)
        {
            LoaDH_CTTime();
        }
        private void LoaDH_CT_ALL(object sender, EventArgs e)
        {
            LoaDH_CT();
        }

        private void UCDANHMUC_PSX_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
            ketnoi kn = new ketnoi();
        }

        private void ExportDH_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }
    }
}
