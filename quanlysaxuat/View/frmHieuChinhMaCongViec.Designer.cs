namespace quanlysanxuat
{
    partial class frmHieuChinhMaCongViec
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHieuChinhMaCongViec));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnNguyenCong = new DevExpress.XtraEditors.SimpleButton();
            this.txtMaSanPham = new System.Windows.Forms.TextBox();
            this.btnHieuChinh = new DevExpress.XtraEditors.SimpleButton();
            this.txtNguoiDung = new System.Windows.Forms.TextBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.iDCD_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.maSanPham_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenSanPham_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.maCongDoan_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenCongDoan_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dinhMuc_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.maCongViec_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.heSoDinhMuc_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nguyenCong_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.maCong_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenCong_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.donGiaCong_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nguoiHieuChinh_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ngayHieuChinh_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView28 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDocDanhSachDinhMuc = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView28)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(967, 42);
            this.panelControl1.TabIndex = 1120;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnNguyenCong);
            this.layoutControl1.Controls.Add(this.txtMaSanPham);
            this.layoutControl1.Controls.Add(this.btnHieuChinh);
            this.layoutControl1.Controls.Add(this.txtNguoiDung);
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(660, 177, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(659, 36);
            this.layoutControl1.TabIndex = 1094;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnNguyenCong
            // 
            this.btnNguyenCong.Location = new System.Drawing.Point(576, 7);
            this.btnNguyenCong.Name = "btnNguyenCong";
            this.btnNguyenCong.Size = new System.Drawing.Size(76, 22);
            this.btnNguyenCong.StyleController = this.layoutControl1;
            this.btnNguyenCong.TabIndex = 1095;
            this.btnNguyenCong.Text = "Nguyên công";
            this.btnNguyenCong.Click += new System.EventHandler(this.btnNguyenCong_Click);
            // 
            // txtMaSanPham
            // 
            this.txtMaSanPham.Location = new System.Drawing.Point(73, 7);
            this.txtMaSanPham.Name = "txtMaSanPham";
            this.txtMaSanPham.ReadOnly = true;
            this.txtMaSanPham.Size = new System.Drawing.Size(178, 20);
            this.txtMaSanPham.TabIndex = 1095;
            // 
            // btnHieuChinh
            // 
            this.btnHieuChinh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHieuChinh.ImageOptions.Image")));
            this.btnHieuChinh.Location = new System.Drawing.Point(495, 7);
            this.btnHieuChinh.Name = "btnHieuChinh";
            this.btnHieuChinh.Size = new System.Drawing.Size(77, 22);
            this.btnHieuChinh.StyleController = this.layoutControl1;
            this.btnHieuChinh.TabIndex = 5;
            this.btnHieuChinh.Text = "Hiệu chỉnh";
            this.btnHieuChinh.Click += new System.EventHandler(this.btnHieuChinh_Click);
            // 
            // txtNguoiDung
            // 
            this.txtNguoiDung.Location = new System.Drawing.Point(321, 7);
            this.txtNguoiDung.Name = "txtNguoiDung";
            this.txtNguoiDung.ReadOnly = true;
            this.txtNguoiDung.Size = new System.Drawing.Size(170, 20);
            this.txtNguoiDung.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(659, 36);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnHieuChinh;
            this.layoutControlItem2.Location = new System.Drawing.Point(488, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(81, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtNguoiDung;
            this.layoutControlItem1.Location = new System.Drawing.Point(248, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(240, 26);
            this.layoutControlItem1.Text = "Người dùng";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtMaSanPham;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(248, 26);
            this.layoutControlItem3.Text = "Mã sản phẩm";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnNguyenCong;
            this.layoutControlItem4.Location = new System.Drawing.Point(569, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 42);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1});
            this.gridControl1.Size = new System.Drawing.Size(967, 413);
            this.gridControl1.TabIndex = 1122;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView28});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.iDCD_grid,
            this.maSanPham_grid,
            this.tenSanPham_grid,
            this.maCongDoan_grid,
            this.tenCongDoan_grid,
            this.dinhMuc_grid,
            this.maCongViec_grid,
            this.heSoDinhMuc_grid,
            this.nguyenCong_grid,
            this.donGiaCong_grid,
            this.nguoiHieuChinh_grid,
            this.ngayHieuChinh_grid});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // iDCD_grid
            // 
            this.iDCD_grid.Caption = "CÔNG ĐOẠN ID";
            this.iDCD_grid.FieldName = "id";
            this.iDCD_grid.Name = "iDCD_grid";
            this.iDCD_grid.Visible = true;
            this.iDCD_grid.VisibleIndex = 12;
            // 
            // maSanPham_grid
            // 
            this.maSanPham_grid.Caption = "MÃ SẢN PHẨM";
            this.maSanPham_grid.FieldName = "Masp";
            this.maSanPham_grid.Name = "maSanPham_grid";
            this.maSanPham_grid.Visible = true;
            this.maSanPham_grid.VisibleIndex = 1;
            this.maSanPham_grid.Width = 114;
            // 
            // tenSanPham_grid
            // 
            this.tenSanPham_grid.Caption = "TÊN SẢN PHẨM";
            this.tenSanPham_grid.FieldName = "Tensp";
            this.tenSanPham_grid.Name = "tenSanPham_grid";
            this.tenSanPham_grid.Visible = true;
            this.tenSanPham_grid.VisibleIndex = 2;
            this.tenSanPham_grid.Width = 151;
            // 
            // maCongDoan_grid
            // 
            this.maCongDoan_grid.Caption = "MÃ CÔNG ĐOẠN";
            this.maCongDoan_grid.FieldName = "Macongdoan";
            this.maCongDoan_grid.Name = "maCongDoan_grid";
            this.maCongDoan_grid.Visible = true;
            this.maCongDoan_grid.VisibleIndex = 3;
            this.maCongDoan_grid.Width = 88;
            // 
            // tenCongDoan_grid
            // 
            this.tenCongDoan_grid.Caption = "TÊN CÔNG ĐOẠN";
            this.tenCongDoan_grid.FieldName = "Tencondoan";
            this.tenCongDoan_grid.Name = "tenCongDoan_grid";
            this.tenCongDoan_grid.Visible = true;
            this.tenCongDoan_grid.VisibleIndex = 4;
            this.tenCongDoan_grid.Width = 161;
            // 
            // dinhMuc_grid
            // 
            this.dinhMuc_grid.Caption = "ĐỊNH MỨC";
            this.dinhMuc_grid.DisplayFormat.FormatString = "{0:#,#}";
            this.dinhMuc_grid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.dinhMuc_grid.FieldName = "Dinhmuc";
            this.dinhMuc_grid.Name = "dinhMuc_grid";
            this.dinhMuc_grid.Visible = true;
            this.dinhMuc_grid.VisibleIndex = 5;
            this.dinhMuc_grid.Width = 61;
            // 
            // maCongViec_grid
            // 
            this.maCongViec_grid.Caption = "MÃ CÔNG VIỆC";
            this.maCongViec_grid.FieldName = "Macv";
            this.maCongViec_grid.Name = "maCongViec_grid";
            this.maCongViec_grid.Visible = true;
            this.maCongViec_grid.VisibleIndex = 6;
            this.maCongViec_grid.Width = 81;
            // 
            // heSoDinhMuc_grid
            // 
            this.heSoDinhMuc_grid.Caption = "HỆ SỐ ĐỊNH MỨC";
            this.heSoDinhMuc_grid.FieldName = "HeSoDinhMuc";
            this.heSoDinhMuc_grid.Name = "heSoDinhMuc_grid";
            this.heSoDinhMuc_grid.Visible = true;
            this.heSoDinhMuc_grid.VisibleIndex = 7;
            this.heSoDinhMuc_grid.Width = 99;
            // 
            // nguyenCong_grid
            // 
            this.nguyenCong_grid.Caption = "NGUYÊN CÔNG";
            this.nguyenCong_grid.ColumnEdit = this.repositoryItemGridLookUpEdit1;
            this.nguyenCong_grid.FieldName = "NguyenCong";
            this.nguyenCong_grid.Name = "nguyenCong_grid";
            this.nguyenCong_grid.Visible = true;
            this.nguyenCong_grid.VisibleIndex = 8;
            this.nguyenCong_grid.Width = 92;
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.PopupView = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.maCong_grid,
            this.tenCong_grid});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // maCong_grid
            // 
            this.maCong_grid.Caption = "MÃ CÔNG";
            this.maCong_grid.FieldName = "Ma_Nguonluc";
            this.maCong_grid.Name = "maCong_grid";
            this.maCong_grid.Visible = true;
            this.maCong_grid.VisibleIndex = 0;
            // 
            // tenCong_grid
            // 
            this.tenCong_grid.Caption = "TÊN CÔNG";
            this.tenCong_grid.FieldName = "Ten_Nguonluc";
            this.tenCong_grid.Name = "tenCong_grid";
            this.tenCong_grid.Visible = true;
            this.tenCong_grid.VisibleIndex = 1;
            // 
            // donGiaCong_grid
            // 
            this.donGiaCong_grid.Caption = "ĐƠN GIÁ CÔNG";
            this.donGiaCong_grid.FieldName = "Dongia_CongDoan";
            this.donGiaCong_grid.Name = "donGiaCong_grid";
            this.donGiaCong_grid.Visible = true;
            this.donGiaCong_grid.VisibleIndex = 9;
            this.donGiaCong_grid.Width = 100;
            // 
            // nguoiHieuChinh_grid
            // 
            this.nguoiHieuChinh_grid.Caption = "NGƯỜI HIỆU CHỈNH";
            this.nguoiHieuChinh_grid.FieldName = "NguoiHC_CV";
            this.nguoiHieuChinh_grid.Name = "nguoiHieuChinh_grid";
            this.nguoiHieuChinh_grid.Visible = true;
            this.nguoiHieuChinh_grid.VisibleIndex = 10;
            this.nguoiHieuChinh_grid.Width = 102;
            // 
            // ngayHieuChinh_grid
            // 
            this.ngayHieuChinh_grid.Caption = "NGÀY HIỆU CHỈNH";
            this.ngayHieuChinh_grid.FieldName = "NgayHC_CV";
            this.ngayHieuChinh_grid.Name = "ngayHieuChinh_grid";
            this.ngayHieuChinh_grid.Visible = true;
            this.ngayHieuChinh_grid.VisibleIndex = 11;
            this.ngayHieuChinh_grid.Width = 106;
            // 
            // gridView28
            // 
            this.gridView28.GridControl = this.gridControl1;
            this.gridView28.Name = "gridView28";
            // 
            // btnDocDanhSachDinhMuc
            // 
            this.btnDocDanhSachDinhMuc.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDocDanhSachDinhMuc.ImageOptions.Image")));
            this.btnDocDanhSachDinhMuc.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDocDanhSachDinhMuc.Location = new System.Drawing.Point(0, 43);
            this.btnDocDanhSachDinhMuc.Name = "btnDocDanhSachDinhMuc";
            this.btnDocDanhSachDinhMuc.Size = new System.Drawing.Size(18, 19);
            this.btnDocDanhSachDinhMuc.TabIndex = 1216;
            this.btnDocDanhSachDinhMuc.TabStop = false;
            this.btnDocDanhSachDinhMuc.Click += new System.EventHandler(this.btnDocDanhSachDinhMuc_Click);
            // 
            // frmHieuChinhMaCongViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 455);
            this.Controls.Add(this.btnDocDanhSachDinhMuc);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHieuChinhMaCongViec";
            this.Text = "HIỆU CHỈNH NGUYÊN CÔNG - TỶ LỆ CÔNG ĐOẠN";
            this.Load += new System.EventHandler(this.frmHieuChinhMaCongViec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView28)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnHieuChinh;
        private System.Windows.Forms.TextBox txtNguoiDung;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView28;
        private DevExpress.XtraEditors.SimpleButton btnDocDanhSachDinhMuc;
        private System.Windows.Forms.TextBox txtMaSanPham;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn iDCD_grid;
        private DevExpress.XtraGrid.Columns.GridColumn maSanPham_grid;
        private DevExpress.XtraGrid.Columns.GridColumn tenSanPham_grid;
        private DevExpress.XtraGrid.Columns.GridColumn maCongDoan_grid;
        private DevExpress.XtraGrid.Columns.GridColumn tenCongDoan_grid;
        private DevExpress.XtraGrid.Columns.GridColumn dinhMuc_grid;
        private DevExpress.XtraGrid.Columns.GridColumn maCongViec_grid;
        private DevExpress.XtraGrid.Columns.GridColumn heSoDinhMuc_grid;
        private DevExpress.XtraGrid.Columns.GridColumn donGiaCong_grid;
        private DevExpress.XtraGrid.Columns.GridColumn nguoiHieuChinh_grid;
        private DevExpress.XtraGrid.Columns.GridColumn ngayHieuChinh_grid;
        private DevExpress.XtraGrid.Columns.GridColumn nguyenCong_grid;
        private DevExpress.XtraEditors.SimpleButton btnNguyenCong;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn maCong_grid;
        private DevExpress.XtraGrid.Columns.GridColumn tenCong_grid;
    }
}