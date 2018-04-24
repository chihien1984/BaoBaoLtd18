namespace quanlysanxuat
{
    partial class frmKHBaoGia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKHBaoGia));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnXuatvlphu = new DevExpress.XtraEditors.SimpleButton();
            this.btnThem = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtid = new System.Windows.Forms.TextBox();
            this.dpden_ngay = new System.Windows.Forms.DateTimePicker();
            this.dptu_ngay = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.txtDiachi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnLoadDMNCC = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.id_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Makhach_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenkhach_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Diachi_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Nguoilap_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Ngaylap_grid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnXuatvlphu);
            this.groupControl1.Controls.Add(this.btnThem);
            this.groupControl1.Controls.Add(this.btnXoa);
            this.groupControl1.Controls.Add(this.btnSua);
            this.groupControl1.Controls.Add(this.txtUser);
            this.groupControl1.Controls.Add(this.txtid);
            this.groupControl1.Controls.Add(this.dpden_ngay);
            this.groupControl1.Controls.Add(this.dptu_ngay);
            this.groupControl1.Controls.Add(this.label40);
            this.groupControl1.Controls.Add(this.label39);
            this.groupControl1.Controls.Add(this.txtDiachi);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.label17);
            this.groupControl1.Controls.Add(this.txtMaKH);
            this.groupControl1.Controls.Add(this.txtTenKH);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1175, 95);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "CẬP NHẬT DANH MỤC NHÀ CUNG CẤP";
            // 
            // btnXuatvlphu
            // 
            this.btnXuatvlphu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXuatvlphu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnXuatvlphu.ImageOptions.Image")));
            this.btnXuatvlphu.Location = new System.Drawing.Point(222, 74);
            this.btnXuatvlphu.Name = "btnXuatvlphu";
            this.btnXuatvlphu.Size = new System.Drawing.Size(56, 18);
            this.btnXuatvlphu.TabIndex = 1104;
            this.btnXuatvlphu.Text = "Expor";
            // 
            // btnThem
            // 
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThem.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Appearance.Options.UseFont = true;
            this.btnThem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.ImageOptions.Image")));
            this.btnThem.Location = new System.Drawing.Point(55, 74);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(47, 18);
            this.btnThem.TabIndex = 1103;
            this.btnThem.TabStop = false;
            this.btnThem.Text = "Ghi";
            this.btnThem.Click += new System.EventHandler(this.them);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.ImageOptions.Image")));
            this.btnXoa.Location = new System.Drawing.Point(167, 74);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(49, 18);
            this.btnXoa.TabIndex = 1102;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.xoa);
            // 
            // btnSua
            // 
            this.btnSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSua.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.ImageOptions.Image")));
            this.btnSua.Location = new System.Drawing.Point(113, 74);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(48, 18);
            this.btnSua.TabIndex = 1101;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.sua);
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(1061, 0);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(109, 18);
            this.txtUser.TabIndex = 1026;
            // 
            // txtid
            // 
            this.txtid.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtid.Location = new System.Drawing.Point(980, 0);
            this.txtid.Name = "txtid";
            this.txtid.ReadOnly = true;
            this.txtid.Size = new System.Drawing.Size(42, 18);
            this.txtid.TabIndex = 1026;
            // 
            // dpden_ngay
            // 
            this.dpden_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpden_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpden_ngay.Location = new System.Drawing.Point(500, 0);
            this.dpden_ngay.Name = "dpden_ngay";
            this.dpden_ngay.Size = new System.Drawing.Size(112, 18);
            this.dpden_ngay.TabIndex = 1021;
            this.dpden_ngay.TabStop = false;
            // 
            // dptu_ngay
            // 
            this.dptu_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptu_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptu_ngay.Location = new System.Drawing.Point(295, 0);
            this.dptu_ngay.Name = "dptu_ngay";
            this.dptu_ngay.Size = new System.Drawing.Size(113, 18);
            this.dptu_ngay.TabIndex = 1020;
            this.dptu_ngay.TabStop = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label40.Location = new System.Drawing.Point(444, 3);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(54, 13);
            this.label40.TabIndex = 1018;
            this.label40.Text = "Đến ngày";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label39.Location = new System.Drawing.Point(245, 3);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(47, 13);
            this.label39.TabIndex = 1019;
            this.label39.Text = "Từ ngày";
            // 
            // txtDiachi
            // 
            this.txtDiachi.Location = new System.Drawing.Point(55, 49);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(557, 21);
            this.txtDiachi.TabIndex = 4;
            this.txtDiachi.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1026, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "User";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(959, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "id";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(5, 53);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "Địa Chỉ";
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(55, 23);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(80, 21);
            this.txtMaKH.TabIndex = 1;
            // 
            // txtTenKH
            // 
            this.txtTenKH.Location = new System.Drawing.Point(141, 23);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(471, 21);
            this.txtTenKH.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnLoadDMNCC);
            this.panelControl1.Controls.Add(this.gridControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 95);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1175, 279);
            this.panelControl1.TabIndex = 406;
            // 
            // btnLoadDMNCC
            // 
            this.btnLoadDMNCC.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btnLoadDMNCC.Appearance.Options.UseForeColor = true;
            this.btnLoadDMNCC.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadDMNCC.ImageOptions.Image")));
            this.btnLoadDMNCC.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLoadDMNCC.Location = new System.Drawing.Point(2, 3);
            this.btnLoadDMNCC.Name = "btnLoadDMNCC";
            this.btnLoadDMNCC.Size = new System.Drawing.Size(16, 19);
            this.btnLoadDMNCC.TabIndex = 403;
            this.btnLoadDMNCC.Click += new System.EventHandler(this.Listkhachhang);
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 2);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1171, 275);
            this.gridControl2.TabIndex = 5;
            this.gridControl2.UseEmbeddedNavigator = true;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.Click += new System.EventHandler(this.Binding);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.id_grid2,
            this.Makhach_grid2,
            this.tenkhach_grid2,
            this.Diachi_grid2,
            this.Nguoilap_grid2,
            this.Ngaylap_grid2});
            gridFormatRule1.Name = "Format0";
            gridFormatRule1.Rule = null;
            this.gridView2.FormatRules.Add(gridFormatRule1);
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowAutoFilterRow = true;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // id_grid2
            // 
            this.id_grid2.Caption = "id";
            this.id_grid2.FieldName = "id";
            this.id_grid2.Name = "id_grid2";
            this.id_grid2.Visible = true;
            this.id_grid2.VisibleIndex = 0;
            this.id_grid2.Width = 37;
            // 
            // Makhach_grid2
            // 
            this.Makhach_grid2.Caption = "Mã khách hàng";
            this.Makhach_grid2.FieldName = "makh";
            this.Makhach_grid2.Name = "Makhach_grid2";
            this.Makhach_grid2.Visible = true;
            this.Makhach_grid2.VisibleIndex = 1;
            this.Makhach_grid2.Width = 158;
            // 
            // tenkhach_grid2
            // 
            this.tenkhach_grid2.Caption = "Khách hàng";
            this.tenkhach_grid2.FieldName = "tenkh";
            this.tenkhach_grid2.Name = "tenkhach_grid2";
            this.tenkhach_grid2.Visible = true;
            this.tenkhach_grid2.VisibleIndex = 2;
            this.tenkhach_grid2.Width = 338;
            // 
            // Diachi_grid2
            // 
            this.Diachi_grid2.Caption = "Địa chỉ";
            this.Diachi_grid2.FieldName = "diachi";
            this.Diachi_grid2.Name = "Diachi_grid2";
            this.Diachi_grid2.Visible = true;
            this.Diachi_grid2.VisibleIndex = 3;
            this.Diachi_grid2.Width = 332;
            // 
            // Nguoilap_grid2
            // 
            this.Nguoilap_grid2.Caption = "Người lập";
            this.Nguoilap_grid2.FieldName = "nguoilap";
            this.Nguoilap_grid2.Name = "Nguoilap_grid2";
            this.Nguoilap_grid2.Visible = true;
            this.Nguoilap_grid2.VisibleIndex = 4;
            this.Nguoilap_grid2.Width = 156;
            // 
            // Ngaylap_grid2
            // 
            this.Ngaylap_grid2.Caption = "Ngày lập";
            this.Ngaylap_grid2.FieldName = "ngaylap";
            this.Ngaylap_grid2.Name = "Ngaylap_grid2";
            this.Ngaylap_grid2.Visible = true;
            this.Ngaylap_grid2.VisibleIndex = 5;
            this.Ngaylap_grid2.Width = 117;
            // 
            // frmKHBaoGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 374);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmKHBaoGia";
            this.Text = "DANH MỤC KHÁCH HÀNG (z)";
            this.Load += new System.EventHandler(this.frmKHBaoGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.DateTimePicker dpden_ngay;
        private System.Windows.Forms.DateTimePicker dptu_ngay;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtDiachi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLoadDMNCC;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnXuatvlphu;
        private DevExpress.XtraEditors.SimpleButton btnThem;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraGrid.Columns.GridColumn id_grid2;
        private DevExpress.XtraGrid.Columns.GridColumn Makhach_grid2;
        private DevExpress.XtraGrid.Columns.GridColumn tenkhach_grid2;
        private DevExpress.XtraGrid.Columns.GridColumn Diachi_grid2;
        private DevExpress.XtraGrid.Columns.GridColumn Nguoilap_grid2;
        private DevExpress.XtraGrid.Columns.GridColumn Ngaylap_grid2;
    }
}