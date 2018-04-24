namespace quanlysanxuat
{
    partial class frmThemDM_vatlieuvt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemDM_vatlieuvt));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnthem = new DevExpress.XtraEditors.SimpleButton();
            this.btnxoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnsua = new DevExpress.XtraEditors.SimpleButton();
            this.txtTenVatLieu = new System.Windows.Forms.TextBox();
            this.txtMaVatLieu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnallDSnhanvien = new DevExpress.XtraEditors.SimpleButton();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.dpden_ngay = new System.Windows.Forms.DateTimePicker();
            this.dptu_ngay = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.id_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Maclvattu_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Tenclvattu_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Nguoilap_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Ngaylap_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnthem);
            this.panelControl1.Controls.Add(this.btnxoa);
            this.panelControl1.Controls.Add(this.btnsua);
            this.panelControl1.Controls.Add(this.txtTenVatLieu);
            this.panelControl1.Controls.Add(this.txtMaVatLieu);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1004, 28);
            this.panelControl1.TabIndex = 3;
            // 
            // btnthem
            // 
            this.btnthem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnthem.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthem.Appearance.Options.UseFont = true;
            this.btnthem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnthem.ImageOptions.Image")));
            this.btnthem.Location = new System.Drawing.Point(847, 6);
            this.btnthem.Name = "btnthem";
            this.btnthem.Size = new System.Drawing.Size(55, 18);
            this.btnthem.TabIndex = 1025;
            this.btnthem.TabStop = false;
            this.btnthem.Text = "Thêm";
            this.btnthem.Click += new System.EventHandler(this.them);
            // 
            // btnxoa
            // 
            this.btnxoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnxoa.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnxoa.Appearance.Options.UseFont = true;
            this.btnxoa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnxoa.ImageOptions.Image")));
            this.btnxoa.Location = new System.Drawing.Point(954, 6);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(46, 18);
            this.btnxoa.TabIndex = 1023;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.Click += new System.EventHandler(this.xoa);
            // 
            // btnsua
            // 
            this.btnsua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnsua.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsua.Appearance.Options.UseFont = true;
            this.btnsua.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnsua.ImageOptions.Image")));
            this.btnsua.Location = new System.Drawing.Point(905, 6);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(45, 18);
            this.btnsua.TabIndex = 1021;
            this.btnsua.Text = "Sửa";
            this.btnsua.Click += new System.EventHandler(this.sua);
            // 
            // txtTenVatLieu
            // 
            this.txtTenVatLieu.Location = new System.Drawing.Point(215, 3);
            this.txtTenVatLieu.Name = "txtTenVatLieu";
            this.txtTenVatLieu.Size = new System.Drawing.Size(538, 21);
            this.txtTenVatLieu.TabIndex = 2;
            // 
            // txtMaVatLieu
            // 
            this.txtMaVatLieu.Location = new System.Drawing.Point(68, 3);
            this.txtMaVatLieu.Name = "txtMaVatLieu";
            this.txtMaVatLieu.Size = new System.Drawing.Size(74, 21);
            this.txtMaVatLieu.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 1016;
            this.label3.Text = "Mã vật liệu";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 1017;
            this.label4.Text = "Tên Vật liệu";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnallDSnhanvien);
            this.groupControl1.Controls.Add(this.txtUser);
            this.groupControl1.Controls.Add(this.label39);
            this.groupControl1.Controls.Add(this.dpden_ngay);
            this.groupControl1.Controls.Add(this.dptu_ngay);
            this.groupControl1.Controls.Add(this.label40);
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Controls.Add(this.txtid);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 28);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1004, 296);
            this.groupControl1.TabIndex = 4;
            // 
            // btnallDSnhanvien
            // 
            this.btnallDSnhanvien.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btnallDSnhanvien.Appearance.Options.UseForeColor = true;
            this.btnallDSnhanvien.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnallDSnhanvien.ImageOptions.Image")));
            this.btnallDSnhanvien.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnallDSnhanvien.Location = new System.Drawing.Point(1, 19);
            this.btnallDSnhanvien.Name = "btnallDSnhanvien";
            this.btnallDSnhanvien.Size = new System.Drawing.Size(16, 20);
            this.btnallDSnhanvien.TabIndex = 1039;
            this.btnallDSnhanvien.Click += new System.EventHandler(this.LoadDMCL_VatTu);
            // 
            // txtUser
            // 
            this.txtUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(906, 0);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(96, 18);
            this.txtUser.TabIndex = 1038;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Indigo;
            this.label39.Location = new System.Drawing.Point(152, 4);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(40, 11);
            this.label39.TabIndex = 1035;
            this.label39.Text = "Từ ngày";
            // 
            // dpden_ngay
            // 
            this.dpden_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpden_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpden_ngay.Location = new System.Drawing.Point(368, 0);
            this.dpden_ngay.Name = "dpden_ngay";
            this.dpden_ngay.Size = new System.Drawing.Size(93, 18);
            this.dpden_ngay.TabIndex = 1037;
            this.dpden_ngay.TabStop = false;
            // 
            // dptu_ngay
            // 
            this.dptu_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptu_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptu_ngay.Location = new System.Drawing.Point(215, 0);
            this.dptu_ngay.Name = "dptu_ngay";
            this.dptu_ngay.Size = new System.Drawing.Size(99, 18);
            this.dptu_ngay.TabIndex = 1036;
            this.dptu_ngay.TabStop = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.Indigo;
            this.label40.Location = new System.Drawing.Point(319, 4);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(46, 11);
            this.label40.TabIndex = 1034;
            this.label40.Text = "Đến ngày";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 20);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1000, 274);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.Binding);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.id_grid,
            this.Maclvattu_grid,
            this.Tenclvattu_grid,
            this.Nguoilap_grid,
            this.Ngaylap_grid});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // id_grid
            // 
            this.id_grid.Caption = "Id";
            this.id_grid.FieldName = "id";
            this.id_grid.Name = "id_grid";
            this.id_grid.Visible = true;
            this.id_grid.VisibleIndex = 0;
            this.id_grid.Width = 85;
            // 
            // Maclvattu_grid
            // 
            this.Maclvattu_grid.Caption = "Mã chất liệu";
            this.Maclvattu_grid.FieldName = "Macl_vattu";
            this.Maclvattu_grid.Name = "Maclvattu_grid";
            this.Maclvattu_grid.Visible = true;
            this.Maclvattu_grid.VisibleIndex = 1;
            // 
            // Tenclvattu_grid
            // 
            this.Tenclvattu_grid.Caption = "Tên chất liệu";
            this.Tenclvattu_grid.FieldName = "Tencl_vattu";
            this.Tenclvattu_grid.Name = "Tenclvattu_grid";
            this.Tenclvattu_grid.Visible = true;
            this.Tenclvattu_grid.VisibleIndex = 2;
            // 
            // Nguoilap_grid
            // 
            this.Nguoilap_grid.Caption = "Người lập";
            this.Nguoilap_grid.FieldName = "Nguoilap";
            this.Nguoilap_grid.Name = "Nguoilap_grid";
            this.Nguoilap_grid.Visible = true;
            this.Nguoilap_grid.VisibleIndex = 3;
            // 
            // Ngaylap_grid
            // 
            this.Ngaylap_grid.Caption = "Ngày lập";
            this.Ngaylap_grid.FieldName = "Ngaylap";
            this.Ngaylap_grid.Name = "Ngaylap_grid";
            this.Ngaylap_grid.Visible = true;
            this.Ngaylap_grid.VisibleIndex = 4;
            // 
            // txtid
            // 
            this.txtid.Enabled = false;
            this.txtid.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtid.Location = new System.Drawing.Point(68, 1);
            this.txtid.Name = "txtid";
            this.txtid.ReadOnly = true;
            this.txtid.Size = new System.Drawing.Size(74, 18);
            this.txtid.TabIndex = 1018;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(872, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1016;
            this.label2.Text = "User";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1016;
            this.label1.Text = "Iden";
            // 
            // frmThemDM_vatlieuvt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 324);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmThemDM_vatlieuvt";
            this.Text = "THÊM DANH MỤC CHẤT LIỆU VẬT TƯ";
            this.Load += new System.EventHandler(this.frmThemDM_vatlieuvt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnthem;
        private DevExpress.XtraEditors.SimpleButton btnxoa;
        private DevExpress.XtraEditors.SimpleButton btnsua;
        private System.Windows.Forms.TextBox txtTenVatLieu;
        private System.Windows.Forms.TextBox txtMaVatLieu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.DateTimePicker dpden_ngay;
        private System.Windows.Forms.DateTimePicker dptu_ngay;
        private System.Windows.Forms.Label label40;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn id_grid;
        private DevExpress.XtraGrid.Columns.GridColumn Maclvattu_grid;
        private DevExpress.XtraGrid.Columns.GridColumn Tenclvattu_grid;
        private DevExpress.XtraGrid.Columns.GridColumn Nguoilap_grid;
        private DevExpress.XtraGrid.Columns.GridColumn Ngaylap_grid;
        private DevExpress.XtraEditors.SimpleButton btnallDSnhanvien;

    }
}