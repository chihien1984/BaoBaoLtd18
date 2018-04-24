namespace quanlysanxuat
{
    partial class frmThem_KH
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThem_KH));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.btnTraCuu = new DevExpress.XtraEditors.SimpleButton();
            this.txtDiachi_KH = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDKhachHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.makh_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKh_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Diachi_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sodienthoai_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fax_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.email_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nguoigiaodich_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.phanloai_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.hotennv_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ngaycapnhat_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Code_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMaKhachHang = new System.Windows.Forms.TextBox();
            this.txtTenKhachHang = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dpngay_them = new System.Windows.Forms.DateTimePicker();
            this.btnthem = new DevExpress.XtraEditors.SimpleButton();
            this.btnxoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaoMoi = new DevExpress.XtraEditors.SimpleButton();
            this.btnhuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnsua = new DevExpress.XtraEditors.SimpleButton();
            this.txtLoai_KH = new System.Windows.Forms.TextBox();
            this.txtNguoiGiaoDich = new System.Windows.Forms.TextBox();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.txtfax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnExportsx = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Enabled = false;
            this.txtDienThoai.Location = new System.Drawing.Point(455, 4);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(164, 21);
            this.txtDienThoai.TabIndex = 403;
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btnTraCuu.Appearance.Options.UseForeColor = true;
            this.btnTraCuu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTraCuu.ImageOptions.Image")));
            this.btnTraCuu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnTraCuu.Location = new System.Drawing.Point(540, 81);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(80, 21);
            this.btnTraCuu.TabIndex = 402;
            this.btnTraCuu.Text = "Tra cứu";
            this.btnTraCuu.Click += new System.EventHandler(this.btnallDSnhanvien_Click);
            // 
            // txtDiachi_KH
            // 
            this.txtDiachi_KH.Enabled = false;
            this.txtDiachi_KH.Location = new System.Drawing.Point(62, 56);
            this.txtDiachi_KH.Name = "txtDiachi_KH";
            this.txtDiachi_KH.Size = new System.Drawing.Size(557, 21);
            this.txtDiachi_KH.TabIndex = 3;
            this.txtDiachi_KH.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(393, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "Điện thoại";
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDKhachHang,
            this.makh_grid,
            this.TenKh_grid,
            this.Diachi_grid,
            this.sodienthoai_grid,
            this.fax_grid,
            this.email_grid,
            this.nguoigiaodich_grid,
            this.phanloai_grid,
            this.hotennv_grid,
            this.ngaycapnhat_grid,
            this.Code_grid});
            gridFormatRule1.Name = "Format0";
            gridFormatRule1.Rule = null;
            this.gridView2.FormatRules.Add(gridFormatRule1);
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowAutoFilterRow = true;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colIDKhachHang
            // 
            this.colIDKhachHang.Caption = "Code";
            this.colIDKhachHang.FieldName = "ID";
            this.colIDKhachHang.Name = "colIDKhachHang";
            this.colIDKhachHang.Visible = true;
            this.colIDKhachHang.VisibleIndex = 0;
            // 
            // makh_grid
            // 
            this.makh_grid.Caption = "Mã khách hàng";
            this.makh_grid.FieldName = "MKH";
            this.makh_grid.Name = "makh_grid";
            this.makh_grid.Visible = true;
            this.makh_grid.VisibleIndex = 1;
            this.makh_grid.Width = 102;
            // 
            // TenKh_grid
            // 
            this.TenKh_grid.Caption = "Tên khách hàng";
            this.TenKh_grid.FieldName = "TenKH";
            this.TenKh_grid.Name = "TenKh_grid";
            this.TenKh_grid.Visible = true;
            this.TenKh_grid.VisibleIndex = 2;
            this.TenKh_grid.Width = 256;
            // 
            // Diachi_grid
            // 
            this.Diachi_grid.Caption = "Đia chỉ";
            this.Diachi_grid.FieldName = "Diachi";
            this.Diachi_grid.Name = "Diachi_grid";
            this.Diachi_grid.Visible = true;
            this.Diachi_grid.VisibleIndex = 3;
            this.Diachi_grid.Width = 214;
            // 
            // sodienthoai_grid
            // 
            this.sodienthoai_grid.Caption = "Số điện thoại";
            this.sodienthoai_grid.FieldName = "Sodienthoai";
            this.sodienthoai_grid.Name = "sodienthoai_grid";
            this.sodienthoai_grid.Visible = true;
            this.sodienthoai_grid.VisibleIndex = 4;
            this.sodienthoai_grid.Width = 82;
            // 
            // fax_grid
            // 
            this.fax_grid.Caption = "Fax";
            this.fax_grid.FieldName = "Fax";
            this.fax_grid.Name = "fax_grid";
            this.fax_grid.Visible = true;
            this.fax_grid.VisibleIndex = 5;
            // 
            // email_grid
            // 
            this.email_grid.Caption = "Email";
            this.email_grid.FieldName = "Email";
            this.email_grid.Name = "email_grid";
            this.email_grid.Visible = true;
            this.email_grid.VisibleIndex = 6;
            // 
            // nguoigiaodich_grid
            // 
            this.nguoigiaodich_grid.Caption = "Người giao dịch";
            this.nguoigiaodich_grid.FieldName = "Nguoi_gd";
            this.nguoigiaodich_grid.Name = "nguoigiaodich_grid";
            this.nguoigiaodich_grid.Visible = true;
            this.nguoigiaodich_grid.VisibleIndex = 7;
            this.nguoigiaodich_grid.Width = 101;
            // 
            // phanloai_grid
            // 
            this.phanloai_grid.Caption = "Phân loại";
            this.phanloai_grid.FieldName = "Phanloai_KH";
            this.phanloai_grid.Name = "phanloai_grid";
            this.phanloai_grid.Visible = true;
            this.phanloai_grid.VisibleIndex = 8;
            // 
            // hotennv_grid
            // 
            this.hotennv_grid.Caption = "Người cập nhật";
            this.hotennv_grid.Name = "hotennv_grid";
            this.hotennv_grid.Visible = true;
            this.hotennv_grid.VisibleIndex = 9;
            this.hotennv_grid.Width = 77;
            // 
            // ngaycapnhat_grid
            // 
            this.ngaycapnhat_grid.Caption = "Ngày cập nhật";
            this.ngaycapnhat_grid.FieldName = "Ngaycapnhat";
            this.ngaycapnhat_grid.Name = "ngaycapnhat_grid";
            this.ngaycapnhat_grid.Visible = true;
            this.ngaycapnhat_grid.VisibleIndex = 10;
            this.ngaycapnhat_grid.Width = 86;
            // 
            // Code_grid
            // 
            this.Code_grid.Caption = "ID";
            this.Code_grid.FieldName = "ID";
            this.Code_grid.Name = "Code_grid";
            this.Code_grid.Visible = true;
            this.Code_grid.VisibleIndex = 11;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 104);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1206, 350);
            this.gridControl2.TabIndex = 2;
            this.gridControl2.UseEmbeddedNavigator = true;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.Click += new System.EventHandler(this.gridControl2_Click);
            this.gridControl2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControl2_MouseClick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 61);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "Địa Chỉ";
            // 
            // txtMaKhachHang
            // 
            this.txtMaKhachHang.Enabled = false;
            this.txtMaKhachHang.Location = new System.Drawing.Point(62, 4);
            this.txtMaKhachHang.Name = "txtMaKhachHang";
            this.txtMaKhachHang.ReadOnly = true;
            this.txtMaKhachHang.Size = new System.Drawing.Size(80, 21);
            this.txtMaKhachHang.TabIndex = 1;
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.Enabled = false;
            this.txtTenKhachHang.Location = new System.Drawing.Point(62, 30);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.Size = new System.Drawing.Size(557, 21);
            this.txtTenKhachHang.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(783, 30);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(67, 21);
            this.txtCode.TabIndex = 1027;
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(759, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 1024;
            this.label10.Text = "ID";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(622, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 1024;
            this.label7.Text = "Kinh doanh";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(686, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 21);
            this.comboBox1.TabIndex = 1023;
            // 
            // dpngay_them
            // 
            this.dpngay_them.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpngay_them.Location = new System.Drawing.Point(62, 80);
            this.dpngay_them.Name = "dpngay_them";
            this.dpngay_them.Size = new System.Drawing.Size(112, 21);
            this.dpngay_them.TabIndex = 1022;
            // 
            // btnthem
            // 
            this.btnthem.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthem.Appearance.Options.UseFont = true;
            this.btnthem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnthem.ImageOptions.Image")));
            this.btnthem.Location = new System.Drawing.Point(261, 81);
            this.btnthem.Name = "btnthem";
            this.btnthem.Size = new System.Drawing.Size(58, 21);
            this.btnthem.TabIndex = 1017;
            this.btnthem.TabStop = false;
            this.btnthem.Text = "Thêm";
            this.btnthem.Click += new System.EventHandler(this.btnthem_Click);
            // 
            // btnxoa
            // 
            this.btnxoa.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnxoa.Appearance.Options.UseFont = true;
            this.btnxoa.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.delete_16x165;
            this.btnxoa.Location = new System.Drawing.Point(405, 81);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(67, 21);
            this.btnxoa.TabIndex = 1015;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click);
            // 
            // btnTaoMoi
            // 
            this.btnTaoMoi.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoMoi.ImageOptions.Image")));
            this.btnTaoMoi.Location = new System.Drawing.Point(208, 81);
            this.btnTaoMoi.Name = "btnTaoMoi";
            this.btnTaoMoi.Size = new System.Drawing.Size(48, 21);
            this.btnTaoMoi.TabIndex = 1016;
            this.btnTaoMoi.Text = "Mới";
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);
            // 
            // btnhuy
            // 
            this.btnhuy.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.Appearance.Options.UseFont = true;
            this.btnhuy.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.clear_16x16;
            this.btnhuy.Location = new System.Drawing.Point(476, 81);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(58, 21);
            this.btnhuy.TabIndex = 1014;
            this.btnhuy.Text = "HỦY";
            // 
            // btnsua
            // 
            this.btnsua.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsua.Appearance.Options.UseFont = true;
            this.btnsua.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.edittask_16x163;
            this.btnsua.Location = new System.Drawing.Point(325, 81);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(74, 21);
            this.btnsua.TabIndex = 1013;
            this.btnsua.Text = "Cập nhật";
            this.btnsua.Click += new System.EventHandler(this.btnsua_Click);
            // 
            // txtLoai_KH
            // 
            this.txtLoai_KH.Enabled = false;
            this.txtLoai_KH.Location = new System.Drawing.Point(686, 80);
            this.txtLoai_KH.Name = "txtLoai_KH";
            this.txtLoai_KH.Size = new System.Drawing.Size(164, 21);
            this.txtLoai_KH.TabIndex = 404;
            this.txtLoai_KH.Text = "TN";
            // 
            // txtNguoiGiaoDich
            // 
            this.txtNguoiGiaoDich.Enabled = false;
            this.txtNguoiGiaoDich.Location = new System.Drawing.Point(193, 4);
            this.txtNguoiGiaoDich.Name = "txtNguoiGiaoDich";
            this.txtNguoiGiaoDich.Size = new System.Drawing.Size(184, 21);
            this.txtNguoiGiaoDich.TabIndex = 403;
            // 
            // txtemail
            // 
            this.txtemail.Enabled = false;
            this.txtemail.Location = new System.Drawing.Point(686, 56);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(164, 21);
            this.txtemail.TabIndex = 403;
            // 
            // txtfax
            // 
            this.txtfax.Enabled = false;
            this.txtfax.Location = new System.Drawing.Point(686, 30);
            this.txtfax.Name = "txtfax";
            this.txtfax.Size = new System.Drawing.Size(68, 21);
            this.txtfax.TabIndex = 403;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(624, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Email";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Ngày lập";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(624, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Phân loại";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(624, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Fax";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Liên hệ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtCode);
            this.panelControl2.Controls.Add(this.dpngay_them);
            this.panelControl2.Controls.Add(this.label10);
            this.panelControl2.Controls.Add(this.comboBox1);
            this.panelControl2.Controls.Add(this.txtLoai_KH);
            this.panelControl2.Controls.Add(this.label6);
            this.panelControl2.Controls.Add(this.btnTraCuu);
            this.panelControl2.Controls.Add(this.label1);
            this.panelControl2.Controls.Add(this.label2);
            this.panelControl2.Controls.Add(this.txtTenKhachHang);
            this.panelControl2.Controls.Add(this.label7);
            this.panelControl2.Controls.Add(this.txtMaKhachHang);
            this.panelControl2.Controls.Add(this.label17);
            this.panelControl2.Controls.Add(this.btnthem);
            this.panelControl2.Controls.Add(this.label3);
            this.panelControl2.Controls.Add(this.btnxoa);
            this.panelControl2.Controls.Add(this.label4);
            this.panelControl2.Controls.Add(this.btnTaoMoi);
            this.panelControl2.Controls.Add(this.label8);
            this.panelControl2.Controls.Add(this.btnhuy);
            this.panelControl2.Controls.Add(this.label16);
            this.panelControl2.Controls.Add(this.btnsua);
            this.panelControl2.Controls.Add(this.label5);
            this.panelControl2.Controls.Add(this.txtDiachi_KH);
            this.panelControl2.Controls.Add(this.txtNguoiGiaoDich);
            this.panelControl2.Controls.Add(this.txtDienThoai);
            this.panelControl2.Controls.Add(this.txtemail);
            this.panelControl2.Controls.Add(this.txtfax);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1206, 104);
            this.panelControl2.TabIndex = 4;
            // 
            // btnExportsx
            // 
            this.btnExportsx.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.exporttoxls_16x164;
            this.btnExportsx.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExportsx.Location = new System.Drawing.Point(0, 104);
            this.btnExportsx.Name = "btnExportsx";
            this.btnExportsx.Size = new System.Drawing.Size(19, 20);
            this.btnExportsx.TabIndex = 417;
            this.btnExportsx.Click += new System.EventHandler(this.btnExportsx_Click);
            // 
            // frmThem_KH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 454);
            this.Controls.Add(this.btnExportsx);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.panelControl2);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmThem_KH.IconOptions.Icon")));
            this.Name = "frmThem_KH";
            this.Text = "Cập nhật danh mục khách hàng";
            this.Load += new System.EventHandler(this.frmThem_KH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtDienThoai;
        private DevExpress.XtraEditors.SimpleButton btnTraCuu;
        private System.Windows.Forms.TextBox txtDiachi_KH;
        private System.Windows.Forms.Label label16;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtMaKhachHang;
        private System.Windows.Forms.TextBox txtTenKhachHang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLoai_KH;
        private System.Windows.Forms.TextBox txtNguoiGiaoDich;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.TextBox txtfax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnthem;
        private DevExpress.XtraEditors.SimpleButton btnxoa;
        private DevExpress.XtraEditors.SimpleButton btnTaoMoi;
        private DevExpress.XtraEditors.SimpleButton btnhuy;
        private DevExpress.XtraEditors.SimpleButton btnsua;
        private System.Windows.Forms.DateTimePicker dpngay_them;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraGrid.Columns.GridColumn makh_grid;
        private DevExpress.XtraGrid.Columns.GridColumn TenKh_grid;
        private DevExpress.XtraGrid.Columns.GridColumn Diachi_grid;
        private DevExpress.XtraGrid.Columns.GridColumn sodienthoai_grid;
        private DevExpress.XtraGrid.Columns.GridColumn fax_grid;
        private DevExpress.XtraGrid.Columns.GridColumn email_grid;
        private DevExpress.XtraGrid.Columns.GridColumn nguoigiaodich_grid;
        private DevExpress.XtraGrid.Columns.GridColumn phanloai_grid;
        private DevExpress.XtraGrid.Columns.GridColumn hotennv_grid;
        private DevExpress.XtraGrid.Columns.GridColumn ngaycapnhat_grid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn Code_grid;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraGrid.Columns.GridColumn colIDKhachHang;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExportsx;
    }
}