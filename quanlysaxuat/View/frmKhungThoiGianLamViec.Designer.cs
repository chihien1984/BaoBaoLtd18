namespace quanlysanxuat.View
{
    partial class frmKhungThoiGianLamViec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKhungThoiGianLamViec));
            this.grKhungThoiGianLamViec = new DevExpress.XtraGrid.GridControl();
            this.gvKhungThoiGianLamViec = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEditTenGoi = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEditTu = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEditDen = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEditBatDau = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTimeEditKetThuc = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.btnThem = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnTraCuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaoMoi = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grKhungThoiGianLamViec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKhungThoiGianLamViec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEditTenGoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditDen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEditBatDau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEditKetThuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grKhungThoiGianLamViec
            // 
            this.grKhungThoiGianLamViec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grKhungThoiGianLamViec.Location = new System.Drawing.Point(0, 26);
            this.grKhungThoiGianLamViec.MainView = this.gvKhungThoiGianLamViec;
            this.grKhungThoiGianLamViec.Name = "grKhungThoiGianLamViec";
            this.grKhungThoiGianLamViec.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTimeEditBatDau,
            this.repositoryItemTimeEditKetThuc,
            this.repositoryItemTextEditTu,
            this.repositoryItemTextEditDen,
            this.repositoryItemMemoEditTenGoi});
            this.grKhungThoiGianLamViec.Size = new System.Drawing.Size(800, 424);
            this.grKhungThoiGianLamViec.TabIndex = 1134;
            this.grKhungThoiGianLamViec.UseEmbeddedNavigator = true;
            this.grKhungThoiGianLamViec.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvKhungThoiGianLamViec});
            this.grKhungThoiGianLamViec.MouseMove += new System.Windows.Forms.MouseEventHandler(this.grKhungThoiGianLamViec_MouseMove);
            // 
            // gvKhungThoiGianLamViec
            // 
            this.gvKhungThoiGianLamViec.Appearance.Row.Options.UseTextOptions = true;
            this.gvKhungThoiGianLamViec.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvKhungThoiGianLamViec.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn8,
            this.gridColumn6,
            this.gridColumn7});
            this.gvKhungThoiGianLamViec.GridControl = this.grKhungThoiGianLamViec;
            this.gvKhungThoiGianLamViec.Name = "gvKhungThoiGianLamViec";
            this.gvKhungThoiGianLamViec.OptionsBehavior.AllowIncrementalSearch = true;
            this.gvKhungThoiGianLamViec.OptionsSelection.MultiSelect = true;
            this.gvKhungThoiGianLamViec.OptionsView.ColumnAutoWidth = false;
            this.gvKhungThoiGianLamViec.OptionsView.RowAutoHeight = true;
            this.gvKhungThoiGianLamViec.OptionsView.ShowAutoFilterRow = true;
            this.gvKhungThoiGianLamViec.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 20;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Mã";
            this.gridColumn2.FieldName = "Ma";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 59;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Tên gọi";
            this.gridColumn3.ColumnEdit = this.repositoryItemMemoEditTenGoi;
            this.gridColumn3.FieldName = "TenGoi";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 199;
            // 
            // repositoryItemMemoEditTenGoi
            // 
            this.repositoryItemMemoEditTenGoi.Name = "repositoryItemMemoEditTenGoi";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Từ";
            this.gridColumn4.ColumnEdit = this.repositoryItemTextEditTu;
            this.gridColumn4.DisplayFormat.FormatString = "t";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn4.FieldName = "Tu";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 68;
            // 
            // repositoryItemTextEditTu
            // 
            this.repositoryItemTextEditTu.AutoHeight = false;
            this.repositoryItemTextEditTu.Mask.EditMask = "[0-1]?[0-9]|[2][0-3]):([0-5][0-9]";
            this.repositoryItemTextEditTu.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEditTu.Name = "repositoryItemTextEditTu";
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Đến";
            this.gridColumn5.ColumnEdit = this.repositoryItemTextEditDen;
            this.gridColumn5.DisplayFormat.FormatString = "t";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn5.FieldName = "Den";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 78;
            // 
            // repositoryItemTextEditDen
            // 
            this.repositoryItemTextEditDen.AutoHeight = false;
            this.repositoryItemTextEditDen.Mask.EditMask = "[0-1]?[0-9]|[2][0-3]):([0-5][0-9]";
            this.repositoryItemTextEditDen.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEditDen.Name = "repositoryItemTextEditDen";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Hệ số";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "HeSo";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 51;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Người ghi";
            this.gridColumn6.FieldName = "NguoiGhi";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 58;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Ngày ghi";
            this.gridColumn7.FieldName = "NgayGhi";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 63;
            // 
            // repositoryItemTimeEditBatDau
            // 
            this.repositoryItemTimeEditBatDau.AutoHeight = false;
            this.repositoryItemTimeEditBatDau.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEditBatDau.Mask.EditMask = "";
            this.repositoryItemTimeEditBatDau.Name = "repositoryItemTimeEditBatDau";
            // 
            // repositoryItemTimeEditKetThuc
            // 
            this.repositoryItemTimeEditKetThuc.AutoHeight = false;
            this.repositoryItemTimeEditKetThuc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEditKetThuc.Mask.EditMask = "";
            this.repositoryItemTimeEditKetThuc.Name = "repositoryItemTimeEditKetThuc";
            // 
            // btnThem
            // 
            this.btnThem.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Appearance.Options.UseFont = true;
            this.btnThem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.ImageOptions.Image")));
            this.btnThem.Location = new System.Drawing.Point(53, 1);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(47, 24);
            this.btnThem.TabIndex = 1137;
            this.btnThem.TabStop = false;
            this.btnThem.Text = "Ghi";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.ImageOptions.Image")));
            this.btnSua.Location = new System.Drawing.Point(103, 1);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(46, 24);
            this.btnSua.TabIndex = 1135;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.ImageOptions.Image")));
            this.btnXoa.Location = new System.Drawing.Point(152, 1);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(51, 24);
            this.btnXoa.TabIndex = 1136;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnExport);
            this.panelControl1.Controls.Add(this.btnTraCuu);
            this.panelControl1.Controls.Add(this.btnTaoMoi);
            this.panelControl1.Controls.Add(this.btnThem);
            this.panelControl1.Controls.Add(this.btnSua);
            this.panelControl1.Controls.Add(this.btnXoa);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 26);
            this.panelControl1.TabIndex = 1138;
            // 
            // btnExport
            // 
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.ImageOptions.Image")));
            this.btnExport.Location = new System.Drawing.Point(276, 1);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(24, 25);
            this.btnExport.TabIndex = 1139;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTraCuu.ImageOptions.Image")));
            this.btnTraCuu.Location = new System.Drawing.Point(207, 1);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(67, 24);
            this.btnTraCuu.TabIndex = 1139;
            this.btnTraCuu.Text = "Tra cứu";
            this.btnTraCuu.Click += new System.EventHandler(this.btnTraCuu_Click);
            // 
            // btnTaoMoi
            // 
            this.btnTaoMoi.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoMoi.Appearance.Options.UseFont = true;
            this.btnTaoMoi.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoMoi.ImageOptions.Image")));
            this.btnTaoMoi.Location = new System.Drawing.Point(1, 1);
            this.btnTaoMoi.Name = "btnTaoMoi";
            this.btnTaoMoi.Size = new System.Drawing.Size(49, 24);
            this.btnTaoMoi.TabIndex = 1137;
            this.btnTaoMoi.TabStop = false;
            this.btnTaoMoi.Text = "New";
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);
            // 
            // frmKhungThoiGianLamViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grKhungThoiGianLamViec);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmKhungThoiGianLamViec";
            this.Text = "KHUNG THỜI GIAN LÀM VIỆC TRONG NGÀY";
            this.Load += new System.EventHandler(this.frmKhungThoiGianLamViec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grKhungThoiGianLamViec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKhungThoiGianLamViec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEditTenGoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditDen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEditBatDau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEditKetThuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grKhungThoiGianLamViec;
        private DevExpress.XtraGrid.Views.Grid.GridView gvKhungThoiGianLamViec;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEditBatDau;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEditKetThuc;
        private DevExpress.XtraEditors.SimpleButton btnThem;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SimpleButton btnTaoMoi;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditTu;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditDen;
        private DevExpress.XtraEditors.SimpleButton btnTraCuu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEditTenGoi;
        private DevExpress.XtraEditors.SimpleButton btnExport;
    }
}