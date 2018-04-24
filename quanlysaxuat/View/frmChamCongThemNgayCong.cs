using DevExpress.ExpressApp.SystemModule;
using DevExpress.XtraEditors.Filtering.Templates;
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

namespace quanlysanxuat.View
{
    public partial class frmChamCongThemNgayCong : Form
    {
        public frmChamCongThemNgayCong(string userid, string name, DateTime date)
        {
            InitializeComponent();
            this.userid = userid;
            this.name = name;
            this.date = date;
        }
        string userid;
        string name;
        DateTime date;
        //formload
        private void frmChamCongThemNgayCong_Load(object sender, EventArgs e)
        {
            this.gvThemNgayCong.Appearance.Row.Font = new Font("Segoe UI", 12f);
            gvThemNgayCong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            txtUserID.Text = userid;
            txtName.Text = name;
            ThemNgayCong();
            repositoryItemComboBoxRaVao.Items.Add("I");
            repositoryItemComboBoxRaVao.Items.Add("O");
        }

        private void ThemNgayCong()
        {
            Connect connect = new Connect();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connect.ConnectChamCong;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select '{0}' UserEnrollNumber,'{1}' TimeDate,'07:30' TimeStr,
                'I' OriginType,'' NewType,'PIN' Source,'0' MachineNo,'0' WorkCode
				union
				select '{0}' UserEnrollNumber,'{1}' TimeDate,'16:30' TimeStr,
                'O' OriginType,'' NewType,'PIN' Source,'0' MachineNo,'0' WorkCode",
                userid, date.ToString("dd-MM-yyyy"));
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grThemNgayCong.DataSource = dt;
            con.Close();
            gvThemNgayCong.SelectAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemNgay_Click(object sender, EventArgs e)
        {
            ThemGioVao();
            ThemGioRa();
            gvThemNgayCong.SelectAll();
        }

        private int i;
        private void ThemGioVao()
        {
            i = i + 1;
            DataTable sourceTable = grThemNgayCong.DataSource as DataTable;
            DataRow row = sourceTable.NewRow();
            row["TimeDate"] = date.AddDays(i).ToString("dd-MM-yyyy");
            row["TimeStr"] = "07:30";
            row["OriginType"] = "I";
            row["Source"] = "PIN";
            row["MachineNo"] = "0";
            row["WorkCode"] = "0";
            sourceTable.Rows.Add(row);
        }

        private void ThemGioRa()
        {
            DataTable sourceTable = grThemNgayCong.DataSource as DataTable;
            DataRow row = sourceTable.NewRow();
            row["TimeDate"] = date.AddDays(i).ToString("dd-MM-yyyy");
            row["TimeStr"] = "16:30";
            row["OriginType"] = "O";
            row["Source"] = "PIN";
            row["MachineNo"] = "0";
            row["WorkCode"] = "0";
            sourceTable.Rows.Add(row);
        }

        private void btnGhiNgay_Click(object sender, EventArgs e)
        {
            try
            {
                Connect cn = new Connect();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = cn.ConnectChamCong;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvThemNgayCong.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThemNgayCong.GetDataRow(listRowList[i]);
                    var date = Convert.ToDateTime(rowData["TimeDate"].ToString());
                    var time = TimeSpan.Parse(rowData["TimeStr"].ToString());
                    DateTime dateTime = date + time;
                    //MessageBox.Show("",date+"|"+dateTime);
                    string strQuery = string.Format(@"insert into CheckInOut 
                        (UserEnrollNumber,TimeDate,TimeStr,
                         OriginType,NewType,Source,MachineNo,WorkCode) values 
                        ('{0}','{1}','{2}',
                         '{3}','{4}','{5}','{6}','{7}')",
                      userid, Convert.ToDateTime(rowData["TimeDate"]).ToString("yyyy-MM-dd"),
                      Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd HH:mm:ss"),
                      rowData["OriginType"], rowData["NewType"], rowData["Source"],
                      rowData["MachineNo"], rowData["WorkCode"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Success", "!");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Warning");
            }
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gvThemNgayCong == null || gvThemNgayCong.SelectedRowsCount == 0) return;
            DataRow[] rows = new DataRow[gvThemNgayCong.SelectedRowsCount];
            for (int i = 0; i < gvThemNgayCong.SelectedRowsCount; i++)
                rows[i] = gvThemNgayCong.GetDataRow(gvThemNgayCong.GetSelectedRows()[i]);
            gvThemNgayCong.BeginSort();
            try
            {
                foreach (DataRow row in rows)
                    row.Delete();
            }
            finally
            {
                gvThemNgayCong.EndSort();
            }
        }
    }
}
