using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class gridcontrol_and_LookupEdit : Form
    {
        
        public gridcontrol_and_LookupEdit()
        {
            InitializeComponent();
        }

        private void gridcontrol_and_LookupEdit_Load(object sender, EventArgs e)
        {
            LoadNull();
            LoadItem();
        }

        private void LoadNull()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select top 1 Mavlphu='',Soluong='',Dongia='',Thanhtien='' from tblNHAP_VATLIEUPHU"); kn.dongketnoi();
        }

        private void Grid()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select Mavlphu,Soluong,Dongia,Thanhtien from tblNHAP_VATLIEUPHU"); kn.dongketnoi();
        }

        private void LoadItem()
        {
            ketnoi kn = new ketnoi();
            repositoryItem.DataSource = kn.laybang("select Mavlphu,Dongia from tblNHAP_VATLIEUPHU");
            repositoryItem.DisplayMember = "Mavlphu";
            repositoryItem.ValueMember = "Mavlphu";
            Mavlphu_gd.ColumnEdit = repositoryItem;
        }

        private void Lookup(string MaVL,string DonGia)
        {

        }
      
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ketnoi kn = new ketnoi();
            DataTable dt = new DataTable();
            dt = kn.laybang("select Ma_Nguonluc,Ten_Nguonluc from tblResources");
            List<GridControl_And_LookupEdit> details = new List<GridControl_And_LookupEdit>();
            details = Utils.ConvertDataTable<GridControl_And_LookupEdit>(dt);
            gridControl1.DataSource = new BindingList<GridControl_And_LookupEdit>(details);
            gridControl1.DataSource = dt;

            //gridView1.SetRowCellValue(e.RowHandle, "Mavlphu",dt.);
            //gridView1.SetRowCellValue(e.RowHandle, "Dongia", );
        }
    }
}