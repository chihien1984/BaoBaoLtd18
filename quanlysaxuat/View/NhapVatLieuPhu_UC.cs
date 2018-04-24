using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using quanlysanxuat.Model;
using System.Threading;

namespace quanlysanxuat.View
{
    public partial class NhapVatLieuPhu_UC :DevExpress.XtraEditors.XtraForm
    {
        public NhapVatLieuPhu_UC()
        {
            InitializeComponent();
        }
        SANXUATDbContext db = new SANXUATDbContext();
        private void btnMaPhieuNhap_Click(object sender, EventArgs e)
        {
            Code_Receipt();
        }
        void Code_Receipt()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"DECLARE @d DATE= GetDate()
                SELECT 'PN'+' '+Right(CONVERT(nvarchar(10), @d, 112),6)", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaPhieuNhap.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void gridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            Show_Save_Edit_Delete();
        }
        private void frmnhap_vat_lieu_phu_Load(object sender, EventArgs e)
        {
            btnSave_input_material.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnDeleteAll_output_voucher.Enabled = false;

            btnDelete_category_kho.Enabled = false;//xóa sổ chi tiết nhập kho
            btnEdit_category_kho.Enabled = true;//Xóa sổ chi tiết nhập kho

            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            txtNguoiLap.Text = Login.Username;
            dpStar.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpEnd.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dpStar_store_input.Text = dpStar.Text;
            dpEnd_store_input.Text = dpEnd.Text;
            dpstar_statistical.Text = dpStar.Text;
            dpend_statistical.Text = dpEnd.Text;
            LoadItemMaterial();
            ListDeliveryMaterial();
            store_input_slip();
            //Thread t = new Thread(new ThreadStart(ItemMaterial));
            //t.Start();
            gridView5.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            //gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            NewListMaterial();
        }
        private void NewListMaterial()
        {
            ketnoi kn = new ketnoi();
            gridControl5.DataSource = kn.laybang(@"select top 0 ViTri,DinhMucTon,Mavlphu,Tenvlphu,
                Donvi,Nguoilap,Ngaylap from tblDM_VATLIEUPHU");
            kn.dongketnoi();
            gridView5.OptionsView.ShowAutoFilterRow = false;
        }
        private void ItemMaterial()
        {
            store_input_slip();
            Thread.Sleep(5000);
        }
        #region receiver-supplier-partuser-object_supplier
        void object_supplier()
        {
            cbDoiTuongCungCap.Properties.Items.Clear();
               ketnoi cn = new ketnoi();
            var dt = cn.laybang(@"select distinct DoiTuongCungCap 
                from tblNHAP_VATLIEUPHU 
                where DoiTuongCungCap !=''");
            cn.dongketnoi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbDoiTuongCungCap.Properties.Items.Add(dt.Rows[i]["DoiTuongCungCap"]);
            }
        }
        void receiver()
        {
            //ketnoi cn = new ketnoi();
            //var dt = cn.laybang(@"
            //    select Distinct Nguoinhan from
            //    tblNHAP_VATLIEUPHU where Nguoinhan !=''");
            //cn.dongketnoi();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    repositoryItemComboBoxNguoiNhan.Items.Add(dt.Rows[i]["Nguoinhan"]);
            //}
        }

        void PartUser()
        {
            //ketnoi cn = new ketnoi();
            //var dt = cn.laybang(@"
            //    select Distinct Noinhan from
            //    tblNHAP_VATLIEUPHU where Noinhan !=''");
            //cn.dongketnoi();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    repositoryItemComboBoxBoPhanSD.Items.Add(dt.Rows[i]["Noinhan"]);
            //}
        }
        void Supplier()//Nhà cung cấp
        {
            repositoryItemComboBoxNhaCC.Items.Clear();
               ketnoi cn = new ketnoi();
            var dt = cn.laybang(@"
                select Nguoigiao from
                tblNHAP_VATLIEUPHU 
                where Nguoigiao !='' group by Nguoigiao");
            cn.dongketnoi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemComboBoxNhaCC.Items.Add(dt.Rows[i]["Nguoigiao"]);
            }
        }
        #endregion
        #region autocomplete Đối tượng cung cấp
        void autocompelte_object_supplier()
        {

        }

        #endregion
        void Show_Save_Edit_Delete()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                btnSave_input_material.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnDeleteAll_output_voucher.Enabled = true;
            }
            else
            {
                btnSave_input_material.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnDeleteAll_output_voucher.Enabled = false;
            }
        }
        void ListDeliveryMaterial()
        {
            var dt = db.DeliveryMaterials.ToList();
            gridControl1.DataSource = new BindingList<DeliveryMaterial>(dt);
            dt.Clear();
            this.gridView1.OptionsView.NewItemRowPosition
                = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }
        void LoadItemMaterial()
        {
            ketnoi kn = new ketnoi();
            repositoryItem.DataSource = kn.laybang(@"select Mavlphu,
                    Tenvlphu
                    from tblDM_VATLIEUPHU order by Ngaylap desc");
            repositoryItem.DisplayMember = "Tenvlphu";
            repositoryItem.ValueMember = "Tenvlphu";
            repositoryItem.NullText = @"CHỌN TÊN VẬT TƯ";
            name_col.ColumnEdit = repositoryItem;
        }
        #region Qty x price = anount
        double Qty = 0;
        double price = 0;
        double amount = 0;
        double stock = 0;
        double userQtyDicrect = 0;
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Tenvlphu")
            {
                var value = gridView1.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblDM_VATLIEUPHU.FirstOrDefault(x => x.Tenvlphu == (string)value);
                if (dt != null)
                {
                    gridView1.SetRowCellValue(e.RowHandle, "Mavlphu", dt.Mavlphu);
                    gridView1.SetRowCellValue(e.RowHandle, "Donvi", dt.Donvi);
                    if (gridView1.GetFocusedRowCellValue(Qty_col) != "")
                    {
                        Qty = Convert.ToDouble(gridView1.GetFocusedRowCellValue(Qty_col));
                        price = Convert.ToDouble(gridView1.GetFocusedRowCellValue(price_col));
                        stock = Convert.ToDouble(gridView1.GetFocusedRowCellValue(warehousing_col));
                        userQtyDicrect = Convert.ToDouble(gridView1.GetFocusedRowCellValue(userQtyDicrect_col));
                        amount = Qty * price;
                        stock = Qty - userQtyDicrect;
                        gridView1.SetFocusedRowCellValue(amount_col, amount);
                        gridView1.SetFocusedRowCellValue(warehousing_col, stock);
                    }
                    else
                    {
                        Qty = 0;
                    }
                }
            }
            if (e.Column == Qty_col || e.Column == price_col || e.Column == userQtyDicrect_col)
            {
                Qty = Convert.ToDouble(gridView1.GetFocusedRowCellValue(Qty_col));
                //gridView1.SetFocusedRowCellValue(userQtyDicrect_col, Qty);
                price = Convert.ToDouble(gridView1.GetFocusedRowCellValue(price_col));
                stock = Convert.ToDouble(gridView1.GetFocusedRowCellValue(warehousing_col));
                userQtyDicrect = Convert.ToDouble(gridView1.GetFocusedRowCellValue(userQtyDicrect_col));
                amount = Qty * price;
                stock = Qty - userQtyDicrect;
                if (userQtyDicrect > Qty)
                {
                    MessageBox.Show("Số cấp cho tổ > số mua", "Thông báo");
                    gridView1.SetFocusedRowCellValue(userQtyDicrect_col, 0);
                    stock = Qty - 0;
                }
                gridView1.SetFocusedRowCellValue(amount_col, amount);
                gridView1.SetFocusedRowCellValue(warehousing_col, stock);
            }
        }
        #endregion
        private void btnSave_input_material_Click(object sender, EventArgs e)
        {
            if (txtMaPhieuNhap.Text == "")
            { MessageBox.Show("Mã phiếu không hợp lệ", "Thông báo"); return; }
            if (cbDoiTuongCungCap.Text == "")
            { MessageBox.Show("Phải có đối tượng cung ứng", "Thông báo"); return; }

            else
            {
                int[] listRowList = this.gridView1.GetSelectedRows();
                if (listRowList.Count() > 0)
                    try
                    {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = Connect.mConnect;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        DataRow rowData;
                        for (int i = 0; i < listRowList.Length; i++)
                        {
                            rowData = this.gridView1.GetDataRow(listRowList[i]);
                            string strQuery = string.Format(@"insert into tblNHAP_VATLIEUPHU
                        (Manhap,Ngaynhap,Mavlphu,Tenvlphu,
                        Donvi,Soluong,Dongia,Thanhtien,
                        Nguoigiao,Diengiai,Nguoinhan,Noinhan,
                        Nhaptam,XuatTT,LoaiNV,Nguoilap,DoiTuongCungCap,Ngayghi)
                        values
                        (
                        N'{0}','{1}',N'{2}',N'{3}',
                        N'{4}','{5}','{6}','{7}',
                        N'{8}',N'{9}',N'{10}',N'{11}',
                        '{12}','{13}',N'{14}',N'{15}',N'{16}',GetDate()
                        )",
                            txtMaPhieuNhap.Text,
                            dpNgayLap.Value.ToString("yyyy/MM/dd"),
                            gridView1.GetRowCellValue(i, "Mavlphu"),
                            gridView1.GetRowCellValue(i, "Tenvlphu"),
                            gridView1.GetRowCellValue(i, "Donvi"),
                            gridView1.GetRowCellValue(i, "Soluong"),
                            gridView1.GetRowCellValue(i, "Dongia"),
                            gridView1.GetRowCellValue(i, "Thanhtien"),
                            gridView1.GetRowCellValue(i, "Nguoigiao"),
                            gridView1.GetRowCellValue(i, "Diengiai"),
                            gridView1.GetRowCellValue(i, "Nguoinhan"),
                            gridView1.GetRowCellValue(i, "Noinhan"),
                            gridView1.GetRowCellValue(i, "Nhaptam"),
                            gridView1.GetRowCellValue(i, "XuatTT"),
                            gridView1.GetRowCellValue(i, "LoaiNV"),
                            txtNguoiLap.Text,
                            cbDoiTuongCungCap.Text);
                            SqlCommand cmd = new SqlCommand(strQuery, con);
                            cmd.ExecuteNonQuery();
                        }

                        con.Close();
                        Updateimport_export_inventory();
                        store_input_slip();
                        detail_store_input_slip();
                        MessageBox.Show("Success", "Message");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("reason: " + ex, "Error message");
                    }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtMaPhieuNhap.Text == "")
            { MessageBox.Show("Mã phiếu không hợp lệ", "Thông báo"); return; }
            if (cbDoiTuongCungCap.Text == "")
            { MessageBox.Show("Phải có đối tượng cung ứng", "Thông báo"); return; }
            else
            {
                //Update_IntoZero();//gán các mã vật tư về 0 sau đó update vào xuất nhập tồn
                int[] listRowList = this.gridView1.GetSelectedRows();
                if (listRowList.Count() > 0)
                    try
                    {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = Connect.mConnect;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        DataRow rowData;
                        for (int i = 0; i < listRowList.Length; i++)
                        {
                            rowData = this.gridView1.GetDataRow(listRowList[i]);
                            string strQuery = string.Format(@"update tblNHAP_VATLIEUPHU
                            set Manhap=N'{0}',Ngaynhap='{1}',Mavlphu=N'{2}',Tenvlphu=N'{3}',
                            Donvi=N'{4}',Soluong='{5}',Dongia='{6}',Thanhtien='{7}',
                            Nguoigiao=N'{8}',Diengiai=N'{9}',Nguoinhan=N'{10}',Noinhan=N'{11}',
                            Nhaptam='{12}',XuatTT='{13}',LoaiNV=N'{14}',
                            Nguoilap=N'{15}',DoiTuongCungCap=N'{16}',Ngayghi=GetDate() where id='{17}'",
                            txtMaPhieuNhap.Text,
                            dpNgayLap.Value.ToString("yyyy/MM/dd"),
                            rowData["Mavlphu"],
                            rowData["Tenvlphu"],
                            rowData["Donvi"],
                            rowData["Soluong"],
                            rowData["Dongia"],
                            rowData["Thanhtien"],
                            rowData["Nguoigiao"],
                            rowData["Diengiai"],
                            rowData["Nguoinhan"],
                            rowData["Noinhan"],
                            rowData["Nhaptam"],
                            rowData["XuatTT"],
                            rowData["LoaiNV"],
                            txtNguoiLap.Text,
                            cbDoiTuongCungCap.Text, rowData["id"]);
                            SqlCommand cmd = new SqlCommand(strQuery, con);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        Updateimport_export_inventory();
                        store_input_slip();
                        detail_store_input_slip();
                        MessageBox.Show("Success", "Message");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("reason: " + ex, "Error message");
                    }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Update_IntoZero();//Trả về zero
            Updateimport_export_inventory();//Cập nhật lại số lượng tồn
            int[] listRowList = this.gridView1.GetSelectedRows();
            if (listRowList.Count() > 0)
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gridView1.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"delete from tblNHAP_VATLIEUPHU where id='{0}'", rowData["id"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Success", "Message");
                    con.Close();
                    store_input_slip();
                    detail_store_input_slip();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("reason: " + ex, "Error message");
                }
        }
        void store_input_slip()//phiếu nhập kho
        {
            ketnoi cn = new ketnoi();
            gridControl4.DataSource = cn.laybang(@"select Manhap,max(Ngaynhap)Ngaynhap,
                max(Nguoigiao)Nguoigiao,max(DoiTuongCungCap)DoiTuongCungCap,
                sum(Thanhtien)Thanhtien from tblNHAP_VATLIEUPHU
                where Ngaynhap between '" + dpStar.Value.ToString("MM/dd/yyyy") + "' and '" + dpEnd.Value.ToString("MM/dd/yyyy") + "' group by Manhap order by Ngaynhap desc");
            cn.dongketnoi();
        }

        void list_store_input_slip()//Danh sach nhập kho
        {
            ketnoi cn = new ketnoi();
            gridControl2.DataSource = cn.laybang(@"select id,Manhap,Ngaynhap,Mavlphu,Tenvlphu,
                            Donvi,Soluong,Dongia,Thanhtien,
                            Nguoigiao,Diengiai,Nguoinhan,Noinhan,
                            Nhaptam,XuatTT,LoaiNV,
                            Nguoilap,DoiTuongCungCap,Ngayghi
                            from tblNHAP_VATLIEUPHU
                            where Manhap !='' and Ngaynhap between '" + dpStar_store_input.Value.ToString("MM/dd/yyyy") + "' and '" + dpEnd_store_input.Value.ToString("MM/dd/yyyy") + "' order by Ngaynhap ASC");
            cn.dongketnoi();
        }

        void detail_store_input_slip()//Chi tiết nhập kho
        {
            ketnoi cn = new ketnoi();
            gridControl1.DataSource = cn.laybang(@"
                select * from tblNHAP_VATLIEUPHU where Manhap ='" + txtMaPhieuNhap.Text + "'");
            cn.dongketnoi();
        }
        //autocomplete
        //https://stackoverflow.com/questions/12972761/textbox-auto-complete-multi-line
        private void btnDonHangTK_Click(object sender, EventArgs e)
        {
            store_input_slip();
        }

        private void btnExport__Click(object sender, EventArgs e)
        {
            if (txtMaPhieuNhap.Text == "") { MessageBox.Show("No Code OUTPUT VOUCHER", ""); return; }
            else
            {
                DataTable dt = new DataTable();
                ketnoi kn = new ketnoi();
                dt = kn.laybang("select * from ViewNhapKho_VTPhu where Manhap like N'" + txtMaPhieuNhap.Text + "' ");
                XRNhapvatlieuphu RpNhapVTPhu = new XRNhapvatlieuphu();
                RpNhapVTPhu.DataSource = dt;
                RpNhapVTPhu.DataMember = "Table";
                RpNhapVTPhu.CreateDocument(false);
                RpNhapVTPhu.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaPhieuNhap.Text;
                PrintTool tool = new PrintTool(RpNhapVTPhu.PrintingSystem);
                RpNhapVTPhu.ShowPreviewDialog();
            }
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gridView4.GetFocusedDisplayText();
            txtMaPhieuNhap.Text = gridView4.GetFocusedRowCellDisplayText(maPhieuNhap_col4);
            dpNgayLap.Text = gridView4.GetFocusedRowCellDisplayText(ngayLap_col4);
            cbDoiTuongCungCap.Text = gridView4.GetFocusedRowCellDisplayText(doiTuongCungCap_col4);
            detail_store_input_slip();
            this.gridView1.OptionsView.NewItemRowPosition =
                DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void btnDeleteAll_output_voucher_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn hủy phiếu nhập kho số: " + txtMaPhieuNhap.Text,
                "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            else
            {
                Update_IntoZero();//Trả về zero
                Updateimport_export_inventory();//Cập nhật lại số lượng tồn
                ketnoi cn = new ketnoi();
                gridControl4.DataSource = cn.xulydulieu("delete from tblNHAP_VATLIEUPHU where Manhap='" + txtMaPhieuNhap.Text + "'");
                cn.dongketnoi();
            }
            store_input_slip();
            detail_store_input_slip();
        }

        private void btnNeu_material_Click(object sender, EventArgs e)
        {
            ListDeliveryMaterial();
            Supplier();
        }
        #region cập nhật lại danh mục tồn cập nhật vật tư về 0 xong update về DM tồn kho rồi xóa đi
        void Updateimport_export_inventory()//cập nhật tồn cuối
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateNhap_VatTuphu", cn);
                queryCM.CommandType = CommandType.StoredProcedure;
                cn.Open();
                queryCM.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception)
            { }
        }

        void Update_IntoZero()//Cập nhật về 0
        {

            int[] listRowList = this.gridView1.GetSelectedRows();
            if (listRowList.Count() > 0)
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gridView1.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"update tblNHAP_VATLIEUPHU
                      Soluong=0 where id='{0}'",
                         rowData["id"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                    Updateimport_export_inventory();
                    MessageBox.Show("Success", "Message");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Reason: " + ex, "Error message");
                }
        }
        #endregion
        private void btnThemVatTuMoi_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView5.GetSelectedRows();
            if (listRowList.Count() > 0)
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gridView5.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"insert tblDM_VATLIEUPHU 
                          (Mavlphu,Tenvlphu,Donvi,Nguoilap,DinhMucTon,ViTri,Ngaylap)
                          values (N'{0}',N'{1}',N'{2}',N'{3}','{4}',N'{5}',GetDate())",
                        rowData["Mavlphu"],
                        rowData["Tenvlphu"],
                        rowData["Donvi"],
                        txtNguoiLap.Text,
                        rowData["DinhMucTon"],
                        rowData["ViTri"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    LoadItemMaterial();
                    CagoryMaterial();
                    MessageBox.Show("Success", "Message");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("reason: " + ex, "Error message");
                }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewListMaterial();
        }

        private void btnEdit_ListMaterial_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView5.GetSelectedRows();
            if (listRowList.Count() > 0)
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gridView5.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"update tblDM_VATLIEUPHU set
                         Mavlphu=N'{0}',Tenvlphu=N'{1}',Donvi=N'{2}',
                         Nguoilap=N'{3}',DinhMucTon='{4}',
                         Ngaylap=GetDate() 
                         where id='{5}'",
                        rowData["Mavlphu"],
                        rowData["Tenvlphu"],
                        rowData["Donvi"],
                        txtNguoiLap.Text,
                        rowData["DinhMucTon"],
                        rowData["id"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    LoadItemMaterial();
                    CagoryMaterial();
                    MessageBox.Show("Success", "Message");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("reason: " + ex, "Error message");
                }
        }

        private void btnDeleteMaterial_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView5.GetSelectedRows();
            if (listRowList.Count() > 0)
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gridView5.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"delete from tblDM_VATLIEUPHU where id='{0}'",
                            rowData["id"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    LoadItemMaterial();
                    CagoryMaterial();
                    MessageBox.Show("Success", "Message");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("reason: " + ex, "Error message");
                }
        }

        private void btnCagoryMaterial_Click(object sender, EventArgs e)
        {
            CagoryMaterial();
        }
        private void CagoryMaterial()
        {
            ketnoi cn = new ketnoi();
            gridControl5.DataSource = 
                cn.laybang(@"select *
                  from tblDM_VATLIEUPHU 
                  order by Ngaylap
                  DESC,Tenvlphu ASC");
            cn.dongketnoi();
            gridView5.OptionsView.ShowAutoFilterRow = true;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select DinhMucTon,
                'MUN-'+cast(Max(right(Mavlphu,4))+1 as varchar)
                from tblDM_VATLIEUPHU where 
                left(Mavlphu,3)='MUN' and len(Mavlphu)=8", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtCode_suggestions.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void btnDetail_voucher_Click(object sender, EventArgs e)
        {
            list_store_input_slip();
            gridView2.Columns["Manhap"].GroupIndex = -1;
            this.gridView2.OptionsSelection.MultiSelectMode
           = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #region thống kê
        void statistical()//thống kê
        {
            ketnoi cn = new ketnoi();
            gridControl3.DataSource = cn.laybang(@"
            select Noinhan,sum(Thanhtien)Thanhtien
            from tblNHAP_VATLIEUPHU 
            where Ngaynhap between '" + dpstar_statistical.Value.ToString("MM/dd/yyyy") + "' and '" + dpend_statistical.Value.ToString("MM/dd/yyyy") + "' group by Noinhan having sum(Thanhtien)>0 and len(Noinhan)>0 order by Noinhan DESC");
            cn.dongketnoi();
        }
        private void btn_statistical_Click(object sender, EventArgs e)
        {
            statistical();
        }
        #endregion

        private void btnExport_phieu_nhap_Click(object sender, EventArgs e)
        {
            gridControl4.ShowPrintPreview();
        }

        private void btnExport_so_nhap_kho_Click(object sender, EventArgs e)
        {
            gridView2.Columns["Manhap"].GroupIndex = -1;
            this.gridView2.OptionsSelection.MultiSelectMode
           = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            gridControl2.ShowPrintPreview();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            list_store_input_slip();
            gridView2.Columns["Manhap"].GroupIndex = 0;
            gridView2.ExpandAllGroups();
            this.gridView2.OptionsSelection.MultiSelectMode
           = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
        }

        private void btnEdit_Category_input_Click(object sender, EventArgs e)
        {
            this.gridView2.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
        }
        private void Show_Button_Edit_Delete()
        {
            if (gridView2.GetSelectedRows().Count() > 0)
            {
                btnEdit_category_kho.Enabled = true;
                btnDelete_category_kho.Enabled = true;
            }
            else
            {
                btnEdit_category_kho.Enabled = false;
                btnDelete_category_kho.Enabled = false;
            }
        }

        private void btn_category_kho_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView2.GetSelectedRows();
            if (listRowList.Count() > 0)
             try
             {
                 SqlConnection con = new SqlConnection();
                 con.ConnectionString = Connect.mConnect;
                 if (con.State == ConnectionState.Closed)
                     con.Open();
                 DataRow rowData;
                 for (int i = 0; i < listRowList.Length; i++)
                 {
                     rowData = this.gridView2.GetDataRow(listRowList[i]);
                     string strQuery = string.Format(@"update tblNHAP_VATLIEUPHU
                         set Manhap=N'{0}',Ngaynhap='{1}',Mavlphu=N'{2}',Tenvlphu=N'{3}',
                         Donvi=N'{4}',Soluong='{5}',Dongia='{6}',Thanhtien='{7}',
                         Nguoigiao=N'{8}',Diengiai=N'{9}',Nguoinhan=N'{10}',Noinhan=N'{11}',
                         Nhaptam='{12}',XuatTT='{13}',LoaiNV=N'{14}',
                         Nguoilap=N'{15}',DoiTuongCungCap=N'{16}',Ngayghi=GetDate() where id='{17}'",
                     txtMaPhieuNhap.Text,
                     dpNgayLap.Value.ToString("yyyy/MM/dd"),
                     rowData["Mavlphu"],
                     rowData["Tenvlphu"],
                     rowData["Donvi"],
                     rowData["Soluong"],
                     rowData["Dongia"],
                     rowData["Thanhtien"],
                     rowData["Nguoigiao"],
                     rowData["Diengiai"],
                     rowData["Nguoinhan"],
                     rowData["Noinhan"],
                     rowData["Nhaptam"],
                     rowData["XuatTT"],
                     rowData["LoaiNV"],
                     txtNguoiLap.Text,
                     cbDoiTuongCungCap.Text, rowData["id"]);
                     SqlCommand cmd = new SqlCommand(strQuery, con);
                     cmd.ExecuteNonQuery();
                 }
                 con.Close();
                 Updateimport_export_inventory();
                 store_input_slip();
                 detail_store_input_slip();
                 MessageBox.Show("Success", "Message");
             }
             catch (Exception ex)
             {
                 MessageBox.Show("reason: " + ex, "Error message");
             }
        }

        private void btnDelete_category_kho_Click(object sender, EventArgs e)
        {
            Update_IntoZero();//Trả về zero
            Updateimport_export_inventory();//Cập nhật lại số lượng tồn
            int[] listRowList = this.gridView2.GetSelectedRows();
            if (listRowList.Count() > 0)
             try
             {
                 SqlConnection con = new SqlConnection();
                 con.ConnectionString = Connect.mConnect;
                 if (con.State == ConnectionState.Closed)
                     con.Open();
                 DataRow rowData;
                 for (int i = 0; i < listRowList.Length; i++)
                 {
                     rowData = this.gridView2.GetDataRow(listRowList[i]);
                     string strQuery = string.Format(@"delete from tblNHAP_VATLIEUPHU where id='{0}'",
                         rowData["id"]);
                     SqlCommand cmd = new SqlCommand(strQuery, con);
                     cmd.ExecuteNonQuery();
                 }
                 con.Close();
                 LoadItemMaterial();
                 CagoryMaterial();
                 list_store_input_slip();
                 MessageBox.Show("Success", "Message");
             }
             catch (Exception ex)
             {
                 MessageBox.Show("reason: " + ex, "Error message");
             }
        }

        private void gridControl2_MouseMove(object sender, MouseEventArgs e)
        {
            Show_Button_Edit_Delete();
        }
    }
}
