using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View.UcControl
{
    public partial class frmGiaoHangCacTo : Form
    {
        public string pointsave;
        public frmGiaoHangCacTo(string pointsave)
        {
            this.pointsave = pointsave;
            InitializeComponent();
        }

        private void ShowReceiveMadeProduction()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select *
				    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where PointSave like '{0}'", pointsave);
            grGiaoNhanHangHoaCacTo.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvGiaoNhanHangHoaCacTo.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void ShowAllReceiveMadeProduction()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select *
				    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet", pointsave);
            grGiaoNhanHangHoaCacTo.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvGiaoNhanHangHoaCacTo.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void frmGiaoHangCacTo_Load(object sender, EventArgs e)
        {
            ShowReceiveMadeProduction();
        }

        private void btnUpdateQualityReceive_Click(object sender, EventArgs e)
        {
            int[] listRowList = gvGiaoNhanHangHoaCacTo.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvGiaoNhanHangHoaCacTo.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
				 set SoGiao='{0}',HangLoiHu='{1}',DienGiai=N'{2}',
                 NguoiGhiGiao=N'{3}',NgayGhiGiao=GetDate()
				 where ID like '{4}'",
                 rowData["SoGiao"],
                 rowData["HangLoiHu"],
                 rowData["DienGiai"],
                 MainDev.username,
                 rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ShowAllReceiveMadeProduction();
        }
    }
}
