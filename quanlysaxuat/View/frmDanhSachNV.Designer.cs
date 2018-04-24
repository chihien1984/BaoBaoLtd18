namespace quanlysanxuat
{
    partial class frmDanhSachNV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDanhSachNV));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBo_Phan = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaoMoi = new DevExpress.XtraEditors.SimpleButton();
            this.txtMember = new System.Windows.Forms.TextBox();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDanhSachNV = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Masothe_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.hotennhanvien_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mabophan_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.BoPhan_repoint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaBoPhan_repoin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenbophan_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ID_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.btnBo_Phan);
            this.groupControl1.Controls.Add(this.btnTaoMoi);
            this.groupControl1.Controls.Add(this.txtMember);
            this.groupControl1.Controls.Add(this.btnXoa);
            this.groupControl1.Controls.Add(this.btnSua);
            this.groupControl1.Controls.Add(this.btnLuu);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1025, 20);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "THÔNG TIN NHÂN VIÊN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(830, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1252;
            this.label1.Text = "Người dùng:";
            // 
            // btnBo_Phan
            // 
            this.btnBo_Phan.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBo_Phan.ImageOptions.Image")));
            this.btnBo_Phan.Location = new System.Drawing.Point(375, 1);
            this.btnBo_Phan.Name = "btnBo_Phan";
            this.btnBo_Phan.Size = new System.Drawing.Size(76, 19);
            this.btnBo_Phan.TabIndex = 1215;
            this.btnBo_Phan.Text = "Bộ phận";
            this.btnBo_Phan.Visible = false;
            this.btnBo_Phan.Click += new System.EventHandler(this.btnBo_Phan_Click);
            // 
            // btnTaoMoi
            // 
            this.btnTaoMoi.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoMoi.Appearance.Options.UseFont = true;
            this.btnTaoMoi.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoMoi.ImageOptions.Image")));
            this.btnTaoMoi.Location = new System.Drawing.Point(136, 1);
            this.btnTaoMoi.Name = "btnTaoMoi";
            this.btnTaoMoi.Size = new System.Drawing.Size(72, 19);
            this.btnTaoMoi.TabIndex = 1215;
            this.btnTaoMoi.TabStop = false;
            this.btnTaoMoi.Text = "Tạo mới";
            this.btnTaoMoi.Visible = false;
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click_1);
            // 
            // txtMember
            // 
            this.txtMember.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMember.Location = new System.Drawing.Point(902, 2);
            this.txtMember.Name = "txtMember";
            this.txtMember.ReadOnly = true;
            this.txtMember.Size = new System.Drawing.Size(121, 18);
            this.txtMember.TabIndex = 1222;
            // 
            // btnXoa
            // 
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.ImageOptions.Image")));
            this.btnXoa.Location = new System.Drawing.Point(325, 1);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(44, 19);
            this.btnXoa.TabIndex = 11;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Visible = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click_1);
            // 
            // btnSua
            // 
            this.btnSua.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.ImageOptions.Image")));
            this.btnSua.Location = new System.Drawing.Point(266, 1);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(46, 19);
            this.btnSua.TabIndex = 12;
            this.btnSua.Text = "Sửa";
            this.btnSua.Visible = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click_1);
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.ImageOptions.Image")));
            this.btnLuu.Location = new System.Drawing.Point(212, 1);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(44, 19);
            this.btnLuu.TabIndex = 13;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Visible = false;
            this.btnLuu.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnDanhSachNV);
            this.panelControl1.Controls.Add(this.gridControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 20);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1025, 468);
            this.panelControl1.TabIndex = 1;
            // 
            // btnDanhSachNV
            // 
            this.btnDanhSachNV.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDanhSachNV.ImageOptions.Image")));
            this.btnDanhSachNV.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDanhSachNV.Location = new System.Drawing.Point(2, 2);
            this.btnDanhSachNV.Name = "btnDanhSachNV";
            this.btnDanhSachNV.Size = new System.Drawing.Size(18, 20);
            this.btnDanhSachNV.TabIndex = 1214;
            this.btnDanhSachNV.TabStop = false;
            this.btnDanhSachNV.Click += new System.EventHandler(this.btnDanhSachNV_Click);
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 2);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1});
            this.gridControl2.Size = new System.Drawing.Size(1021, 464);
            this.gridControl2.TabIndex = 2;
            this.gridControl2.UseEmbeddedNavigator = true;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Masothe_grid2,
            this.hotennhanvien_grid2,
            this.mabophan_grid2,
            this.tenbophan_grid2,
            this.gridColumn2,
            this.gridColumn1,
            this.ID_grid2});
            gridFormatRule1.Name = "Format0";
            gridFormatRule1.Rule = null;
            this.gridView2.FormatRules.Add(gridFormatRule1);
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowAutoFilterRow = true;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // Masothe_grid2
            // 
            this.Masothe_grid2.Caption = "Mã số thẻ NV";
            this.Masothe_grid2.FieldName = "Sothe";
            this.Masothe_grid2.Name = "Masothe_grid2";
            this.Masothe_grid2.Visible = true;
            this.Masothe_grid2.VisibleIndex = 1;
            // 
            // hotennhanvien_grid2
            // 
            this.hotennhanvien_grid2.Caption = "Họ tên NV";
            this.hotennhanvien_grid2.FieldName = "HoTen";
            this.hotennhanvien_grid2.Name = "hotennhanvien_grid2";
            this.hotennhanvien_grid2.Visible = true;
            this.hotennhanvien_grid2.VisibleIndex = 2;
            this.hotennhanvien_grid2.Width = 248;
            // 
            // mabophan_grid2
            // 
            this.mabophan_grid2.Caption = "Mã bộ phận";
            this.mabophan_grid2.ColumnEdit = this.repositoryItemGridLookUpEdit1;
            this.mabophan_grid2.FieldName = "MaBP";
            this.mabophan_grid2.Name = "mabophan_grid2";
            this.mabophan_grid2.Visible = true;
            this.mabophan_grid2.VisibleIndex = 3;
            this.mabophan_grid2.Width = 116;
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
            this.BoPhan_repoint,
            this.MaBoPhan_repoin});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // BoPhan_repoint
            // 
            this.BoPhan_repoint.Caption = "TÊN TỔ";
            this.BoPhan_repoint.FieldName = "BoPhan";
            this.BoPhan_repoint.Name = "BoPhan_repoint";
            this.BoPhan_repoint.Visible = true;
            this.BoPhan_repoint.VisibleIndex = 0;
            // 
            // MaBoPhan_repoin
            // 
            this.MaBoPhan_repoin.Caption = "MÃ TỔ";
            this.MaBoPhan_repoin.FieldName = "MaBoPhan";
            this.MaBoPhan_repoin.Name = "MaBoPhan_repoin";
            this.MaBoPhan_repoin.Visible = true;
            this.MaBoPhan_repoin.VisibleIndex = 1;
            // 
            // tenbophan_grid2
            // 
            this.tenbophan_grid2.Caption = "Tên bộ phận";
            this.tenbophan_grid2.FieldName = "BoPhan";
            this.tenbophan_grid2.Name = "tenbophan_grid2";
            this.tenbophan_grid2.Width = 214;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Người ghi";
            this.gridColumn2.FieldName = "NguoiLap";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 114;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ngày ghi";
            this.gridColumn1.FieldName = "NgayLap";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 5;
            this.gridColumn1.Width = 96;
            // 
            // ID_grid2
            // 
            this.ID_grid2.Caption = "NhanVienID";
            this.ID_grid2.FieldName = "ID";
            this.ID_grid2.Name = "ID_grid2";
            this.ID_grid2.Visible = true;
            this.ID_grid2.VisibleIndex = 6;
            this.ID_grid2.Width = 53;
            // 
            // frmDanhSachNV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 488);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDanhSachNV";
            this.Text = "DANH SÁCH NHÂN VIÊN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDanhSachNV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn Masothe_grid2;
        private DevExpress.XtraGrid.Columns.GridColumn hotennhanvien_grid2;
        private DevExpress.XtraGrid.Columns.GridColumn mabophan_grid2;
        private DevExpress.XtraGrid.Columns.GridColumn tenbophan_grid2;
        private DevExpress.XtraGrid.Columns.GridColumn ID_grid2;
        private DevExpress.XtraEditors.SimpleButton btnDanhSachNV;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private System.Windows.Forms.TextBox txtMember;
        private DevExpress.XtraEditors.SimpleButton btnTaoMoi;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn BoPhan_repoint;
        private DevExpress.XtraGrid.Columns.GridColumn MaBoPhan_repoin;
        private DevExpress.XtraEditors.SimpleButton btnBo_Phan;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}