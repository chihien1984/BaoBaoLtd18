using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace quanlysanxuat
{
    class tool
    {
        public static void style_dgview(System.Windows.Forms.DataGridView luoi)
        {
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.ForeColor = Color.Blue;
            style1.BackColor = Color.Linen;
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            style2.ForeColor = Color.Red;
            style2.BackColor = Color.White;
            for (int i = luoi.RowCount - 1; i >= 0; i--)
            {
                if (i % 2 == 0)
                {
                    luoi.Rows[i].DefaultCellStyle = style1;
                }
                else if (i % 2 != 0)
                {
                    luoi.Rows[i].DefaultCellStyle = style2;
                }
            }
        }
    }
}
