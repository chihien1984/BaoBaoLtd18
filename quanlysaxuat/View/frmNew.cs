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
using System.Reflection;
using System.Data.SqlClient;

namespace quanlysanxuat
{
    public partial class frmNew : DevExpress.XtraEditors.XtraForm
    {
        public frmNew()
        {
            InitializeComponent();
        }

        private void frmNew_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); 
            dpden_ngay.Text = DateTime.Now.ToString();
        }
        
        private void loadDM()
        {
            ketnoi Connect = new ketnoi();
            repositoryItemGridLookUpEdit1.DataSource = Connect.laybang("select Ma_Nguonluc,Ten_Nguonluc,Ngay,Nguoi from tblResources");
            repositoryItemGridLookUpEdit1.DisplayMember = "Ma_Nguonluc";
            repositoryItemGridLookUpEdit1.ValueMember = "Ma_Nguonluc";
            repositoryItemGridLookUpEdit1.NullText = null;
            repositoryItemGridLookUpEdit1.PopupFilterMode = PopupFilterMode.Contains;
            repositoryItemGridLookUpEdit1.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            repositoryItemGridLookUpEdit1.View.OptionsView.ShowAutoFilterRow = true;
            //repositoryItemGridLookUpEdit1.EditValueChanged += repositoryItemGridLookUpEdit1View_CellValueChanged;
            repositoryItemGridLookUpEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemGridLookUpEdit1.ImmediatePopup = true;
            Connect.dongketnoi();
        }

        private void repositoryItemGridLookUpEdit1View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void CrossTable()
        {
            
            try
            {
                {  
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("GrossFullMonth", cn);
                    cmd.Parameters.Add(new SqlParameter("@BatDau", SqlDbType.Date)).Value = dptu_ngay.Text;
                    cmd.Parameters.Add(new SqlParameter("@KetThuc", SqlDbType.Date)).Value = dpden_ngay.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguonluc", SqlDbType.Int)).Value = txtGioCong_Dap.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet das = new DataSet();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    cmd.ExecuteNonQuery();
                    gridControl1.DataSource = dt;
                    cn.Close();
                }
            }
            catch (Exception EX) { MessageBox.Show("Erorr"+EX); }
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            gridView2.Columns.Clear();
            CrossTable();
            //gridView2.Columns["3"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Sum={0}");
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {}
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {        }       
    }
}