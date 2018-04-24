namespace quanlysanxuat
{
    partial class frmCapNhatDanhMucSanPham
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCapNhatDanhMucSanPham));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression1 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnXemBanVe = new DevExpress.XtraEditors.SimpleButton();
            this.cbTen_vatlieu = new System.Windows.Forms.ComboBox();
            this.txtMa_Vatlieu = new System.Windows.Forms.TextBox();
            this.btnTheVatLieu = new DevExpress.XtraEditors.SimpleButton();
            this.txtCodeKhachHang = new System.Windows.Forms.TextBox();
            this.btnThemKH = new DevExpress.XtraEditors.SimpleButton();
            this.txtDacDiem = new System.Windows.Forms.TextBox();
            this.txtSoThuTu = new System.Windows.Forms.TextBox();
            this.txtTenSanPham = new System.Windows.Forms.TextBox();
            this.txtmasp = new System.Windows.Forms.TextBox();
            this.cbTenKhachHang = new System.Windows.Forms.ComboBox();
            this.btnthem = new DevExpress.XtraEditors.SimpleButton();
            this.btnxoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportsx = new DevExpress.XtraEditors.SimpleButton();
            this.btnhuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnsua = new DevExpress.XtraEditors.SimpleButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.Code = new System.Windows.Forms.Label();
            this.dpden_ngay = new System.Windows.Forms.DateTimePicker();
            this.dptu_ngay = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.dpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTraCuuTatCa = new DevExpress.XtraEditors.SimpleButton();
            this.grDanhMucSanPham = new DevExpress.XtraGrid.GridControl();
            this.gvDanhMucSanPham = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Masp_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Tensp_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colQuyTrinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDinhMuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Vatlieu_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Dacdiem_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Ngaylap_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HotenNV_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Makh_grid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Tenkh_grid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Code_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.repositoryItemCalcEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.repositoryItemCalcEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ckDinhMucVatTu = new System.Windows.Forms.CheckBox();
            this.ckQuyTrinhSanXuat = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grDanhMucSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhMucSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.ImageOptions.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(400, 123);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(64, 20);
            this.btnUpdate.TabIndex = 1066;
            this.btnUpdate.Text = "Remote";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnXemBanVe
            // 
            this.btnXemBanVe.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.exporttopdf_16x1610;
            this.btnXemBanVe.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnXemBanVe.Location = new System.Drawing.Point(310, 122);
            this.btnXemBanVe.Name = "btnXemBanVe";
            this.btnXemBanVe.Size = new System.Drawing.Size(85, 20);
            this.btnXemBanVe.TabIndex = 1063;
            this.btnXemBanVe.Text = "Bản vẽ";
            this.btnXemBanVe.Click += new System.EventHandler(this.SukienGoiMASP_Click);
            // 
            // cbTen_vatlieu
            // 
            this.cbTen_vatlieu.FormattingEnabled = true;
            this.cbTen_vatlieu.Location = new System.Drawing.Point(389, 3);
            this.cbTen_vatlieu.Name = "cbTen_vatlieu";
            this.cbTen_vatlieu.Size = new System.Drawing.Size(76, 21);
            this.cbTen_vatlieu.TabIndex = 1017;
            this.cbTen_vatlieu.SelectedIndexChanged += new System.EventHandler(this.cbTen_vatlieu_SelectedIndexChanged_1);
            // 
            // txtMa_Vatlieu
            // 
            this.txtMa_Vatlieu.Location = new System.Drawing.Point(388, 30);
            this.txtMa_Vatlieu.Name = "txtMa_Vatlieu";
            this.txtMa_Vatlieu.Size = new System.Drawing.Size(76, 21);
            this.txtMa_Vatlieu.TabIndex = 1016;
            // 
            // btnTheVatLieu
            // 
            this.btnTheVatLieu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTheVatLieu.ImageOptions.Image")));
            this.btnTheVatLieu.Location = new System.Drawing.Point(469, 3);
            this.btnTheVatLieu.Name = "btnTheVatLieu";
            this.btnTheVatLieu.Size = new System.Drawing.Size(33, 21);
            this.btnTheVatLieu.TabIndex = 1014;
            this.btnTheVatLieu.Click += new System.EventHandler(this.ThemVatlieu_Click);
            // 
            // txtCodeKhachHang
            // 
            this.txtCodeKhachHang.Location = new System.Drawing.Point(62, 30);
            this.txtCodeKhachHang.Name = "txtCodeKhachHang";
            this.txtCodeKhachHang.Size = new System.Drawing.Size(234, 21);
            this.txtCodeKhachHang.TabIndex = 1012;
            // 
            // btnThemKH
            // 
            this.btnThemKH.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnThemKH.ImageOptions.Image")));
            this.btnThemKH.Location = new System.Drawing.Point(296, 3);
            this.btnThemKH.Name = "btnThemKH";
            this.btnThemKH.Size = new System.Drawing.Size(24, 19);
            this.btnThemKH.TabIndex = 1011;
            this.btnThemKH.TabStop = false;
            this.btnThemKH.Click += new System.EventHandler(this.btnThemKH_Click);
            // 
            // txtDacDiem
            // 
            this.txtDacDiem.Location = new System.Drawing.Point(62, 79);
            this.txtDacDiem.Name = "txtDacDiem";
            this.txtDacDiem.Size = new System.Drawing.Size(642, 21);
            this.txtDacDiem.TabIndex = 469;
            // 
            // txtSoThuTu
            // 
            this.txtSoThuTu.Location = new System.Drawing.Point(548, 3);
            this.txtSoThuTu.Name = "txtSoThuTu";
            this.txtSoThuTu.Size = new System.Drawing.Size(156, 21);
            this.txtSoThuTu.TabIndex = 465;
            this.txtSoThuTu.TextChanged += new System.EventHandler(this.SUKIENAPMA);
            // 
            // txtTenSanPham
            // 
            this.txtTenSanPham.Location = new System.Drawing.Point(62, 55);
            this.txtTenSanPham.Name = "txtTenSanPham";
            this.txtTenSanPham.Size = new System.Drawing.Size(642, 21);
            this.txtTenSanPham.TabIndex = 465;
            // 
            // txtmasp
            // 
            this.txtmasp.Location = new System.Drawing.Point(548, 30);
            this.txtmasp.Name = "txtmasp";
            this.txtmasp.ReadOnly = true;
            this.txtmasp.Size = new System.Drawing.Size(156, 21);
            this.txtmasp.TabIndex = 465;
            // 
            // cbTenKhachHang
            // 
            this.cbTenKhachHang.FormattingEnabled = true;
            this.cbTenKhachHang.Location = new System.Drawing.Point(62, 3);
            this.cbTenKhachHang.Name = "cbTenKhachHang";
            this.cbTenKhachHang.Size = new System.Drawing.Size(234, 21);
            this.cbTenKhachHang.TabIndex = 464;
            this.cbTenKhachHang.SelectedIndexChanged += new System.EventHandler(this.cbtenkh_SelectedIndexChanged);
            // 
            // btnthem
            // 
            this.btnthem.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthem.Appearance.Options.UseFont = true;
            this.btnthem.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.save_16x164;
            this.btnthem.Location = new System.Drawing.Point(84, 122);
            this.btnthem.Name = "btnthem";
            this.btnthem.Size = new System.Drawing.Size(56, 20);
            this.btnthem.TabIndex = 460;
            this.btnthem.TabStop = false;
            this.btnthem.Text = "Thêm";
            this.btnthem.Click += new System.EventHandler(this.btnthem_Click);
            // 
            // btnxoa
            // 
            this.btnxoa.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnxoa.Appearance.Options.UseFont = true;
            this.btnxoa.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.delete_16x164;
            this.btnxoa.Location = new System.Drawing.Point(239, 122);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(65, 20);
            this.btnxoa.TabIndex = 456;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click);
            // 
            // btnExportsx
            // 
            this.btnExportsx.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.exporttoxls_16x164;
            this.btnExportsx.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExportsx.Location = new System.Drawing.Point(1, 143);
            this.btnExportsx.Name = "btnExportsx";
            this.btnExportsx.Size = new System.Drawing.Size(17, 20);
            this.btnExportsx.TabIndex = 416;
            this.btnExportsx.Click += new System.EventHandler(this.btnExportsx_Click);
            // 
            // btnhuy
            // 
            this.btnhuy.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.Appearance.Options.UseFont = true;
            this.btnhuy.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.newtask_16x163;
            this.btnhuy.Location = new System.Drawing.Point(57, 122);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(24, 20);
            this.btnhuy.TabIndex = 423;
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // btnsua
            // 
            this.btnsua.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsua.Appearance.Options.UseFont = true;
            this.btnsua.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.edittask_16x162;
            this.btnsua.Location = new System.Drawing.Point(145, 122);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(83, 20);
            this.btnsua.TabIndex = 422;
            this.btnsua.Text = "Cập nhật";
            this.btnsua.Click += new System.EventHandler(this.btnsua_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Khách";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(508, 7);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Mã số";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Code";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Đặc điểm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mã vật liệu";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(328, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Vật liệu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên gọi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã sản phẩm";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(619, 123);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(85, 18);
            this.txtCode.TabIndex = 1064;
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            // 
            // Code
            // 
            this.Code.AutoSize = true;
            this.Code.BackColor = System.Drawing.Color.Transparent;
            this.Code.ForeColor = System.Drawing.Color.Black;
            this.Code.Location = new System.Drawing.Point(619, 104);
            this.Code.Name = "Code";
            this.Code.Size = new System.Drawing.Size(18, 13);
            this.Code.TabIndex = 1008;
            this.Code.Text = "ID";
            this.Code.Click += new System.EventHandler(this.Code_Click);
            // 
            // dpden_ngay
            // 
            this.dpden_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpden_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpden_ngay.Location = new System.Drawing.Point(310, 102);
            this.dpden_ngay.Name = "dpden_ngay";
            this.dpden_ngay.Size = new System.Drawing.Size(85, 18);
            this.dpden_ngay.TabIndex = 1010;
            this.dpden_ngay.TabStop = false;
            // 
            // dptu_ngay
            // 
            this.dptu_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptu_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptu_ngay.Location = new System.Drawing.Point(188, 102);
            this.dptu_ngay.Name = "dptu_ngay";
            this.dptu_ngay.Size = new System.Drawing.Size(88, 18);
            this.dptu_ngay.TabIndex = 1009;
            this.dptu_ngay.TabStop = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.ForeColor = System.Drawing.Color.Black;
            this.label40.Location = new System.Drawing.Point(279, 105);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(27, 13);
            this.label40.TabIndex = 1007;
            this.label40.Text = "Đến";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.ForeColor = System.Drawing.Color.Black;
            this.label39.Location = new System.Drawing.Point(162, 105);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(20, 13);
            this.label39.TabIndex = 1008;
            this.label39.Text = "Từ";
            // 
            // dpNgayLap
            // 
            this.dpNgayLap.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpNgayLap.CalendarForeColor = System.Drawing.Color.LawnGreen;
            this.dpNgayLap.CalendarMonthBackground = System.Drawing.Color.LawnGreen;
            this.dpNgayLap.CalendarTitleBackColor = System.Drawing.Color.LawnGreen;
            this.dpNgayLap.CalendarTitleForeColor = System.Drawing.Color.LawnGreen;
            this.dpNgayLap.CalendarTrailingForeColor = System.Drawing.Color.LawnGreen;
            this.dpNgayLap.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpNgayLap.Location = new System.Drawing.Point(62, 102);
            this.dpNgayLap.Name = "dpNgayLap";
            this.dpNgayLap.Size = new System.Drawing.Size(85, 18);
            this.dpNgayLap.TabIndex = 461;
            this.dpNgayLap.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ngày lập";
            // 
            // btnTraCuuTatCa
            // 
            this.btnTraCuuTatCa.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btnTraCuuTatCa.Appearance.Options.UseForeColor = true;
            this.btnTraCuuTatCa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTraCuuTatCa.ImageOptions.Image")));
            this.btnTraCuuTatCa.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnTraCuuTatCa.Location = new System.Drawing.Point(401, 101);
            this.btnTraCuuTatCa.Name = "btnTraCuuTatCa";
            this.btnTraCuuTatCa.Size = new System.Drawing.Size(64, 21);
            this.btnTraCuuTatCa.TabIndex = 425;
            this.btnTraCuuTatCa.Text = "Tra cứu";
            this.btnTraCuuTatCa.Click += new System.EventHandler(this.btnShowall_Click);
            // 
            // grDanhMucSanPham
            // 
            this.grDanhMucSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grDanhMucSanPham.Location = new System.Drawing.Point(0, 143);
            this.grDanhMucSanPham.MainView = this.gvDanhMucSanPham;
            this.grDanhMucSanPham.Name = "grDanhMucSanPham";
            this.grDanhMucSanPham.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit1,
            this.repositoryItemCalcEdit2,
            this.repositoryItemCalcEdit3,
            this.repositoryItemFontEdit1,
            this.repositoryItemMemoEdit1});
            this.grDanhMucSanPham.Size = new System.Drawing.Size(990, 309);
            this.grDanhMucSanPham.TabIndex = 442;
            this.grDanhMucSanPham.UseEmbeddedNavigator = true;
            this.grDanhMucSanPham.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDanhMucSanPham});
            this.grDanhMucSanPham.Click += new System.EventHandler(this.gridControl1_Click);
            this.grDanhMucSanPham.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            this.grDanhMucSanPham.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseClick);
            // 
            // gvDanhMucSanPham
            // 
            this.gvDanhMucSanPham.Appearance.Row.Options.UseTextOptions = true;
            this.gvDanhMucSanPham.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvDanhMucSanPham.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Masp_grid,
            this.Tensp_grid,
            this.colQuyTrinh,
            this.colDinhMuc,
            this.Vatlieu_grid,
            this.Dacdiem_grid,
            this.Ngaylap_grid,
            this.HotenNV_grid,
            this.Makh_grid1,
            this.Tenkh_grid1,
            this.Code_grid});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleExpression1.Appearance.BackColor = System.Drawing.Color.GreenYellow;
            formatConditionRuleExpression1.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression1.Expression = "[SOLUONGTP] >= [soluongsx]";
            gridFormatRule1.Rule = formatConditionRuleExpression1;
            this.gvDanhMucSanPham.FormatRules.Add(gridFormatRule1);
            this.gvDanhMucSanPham.GridControl = this.grDanhMucSanPham;
            this.gvDanhMucSanPham.Name = "gvDanhMucSanPham";
            this.gvDanhMucSanPham.OptionsBehavior.Editable = false;
            this.gvDanhMucSanPham.OptionsSelection.MultiSelect = true;
            this.gvDanhMucSanPham.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvDanhMucSanPham.OptionsView.ColumnAutoWidth = false;
            this.gvDanhMucSanPham.OptionsView.RowAutoHeight = true;
            this.gvDanhMucSanPham.OptionsView.ShowAutoFilterRow = true;
            this.gvDanhMucSanPham.OptionsView.ShowGroupPanel = false;
            // 
            // Masp_grid
            // 
            this.Masp_grid.Caption = "Mã sản phẩm";
            this.Masp_grid.FieldName = "Masp";
            this.Masp_grid.Name = "Masp_grid";
            this.Masp_grid.Visible = true;
            this.Masp_grid.VisibleIndex = 1;
            this.Masp_grid.Width = 116;
            // 
            // Tensp_grid
            // 
            this.Tensp_grid.Caption = "Tên sản phẩm";
            this.Tensp_grid.ColumnEdit = this.repositoryItemMemoEdit1;
            this.Tensp_grid.FieldName = "Tensp";
            this.Tensp_grid.Name = "Tensp_grid";
            this.Tensp_grid.Visible = true;
            this.Tensp_grid.VisibleIndex = 2;
            this.Tensp_grid.Width = 197;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colQuyTrinh
            // 
            this.colQuyTrinh.Caption = "Quy trình";
            this.colQuyTrinh.FieldName = "check1";
            this.colQuyTrinh.Name = "colQuyTrinh";
            this.colQuyTrinh.Visible = true;
            this.colQuyTrinh.VisibleIndex = 3;
            this.colQuyTrinh.Width = 45;
            // 
            // colDinhMuc
            // 
            this.colDinhMuc.Caption = "Định mức vật tư";
            this.colDinhMuc.FieldName = "check2";
            this.colDinhMuc.Name = "colDinhMuc";
            this.colDinhMuc.Visible = true;
            this.colDinhMuc.VisibleIndex = 4;
            this.colDinhMuc.Width = 45;
            // 
            // Vatlieu_grid
            // 
            this.Vatlieu_grid.Caption = "Loại vật liệu";
            this.Vatlieu_grid.FieldName = "Vatlieu";
            this.Vatlieu_grid.Name = "Vatlieu_grid";
            this.Vatlieu_grid.Visible = true;
            this.Vatlieu_grid.VisibleIndex = 5;
            this.Vatlieu_grid.Width = 50;
            // 
            // Dacdiem_grid
            // 
            this.Dacdiem_grid.Caption = "Đặc tính kỹ thuật";
            this.Dacdiem_grid.FieldName = "Dacdiem";
            this.Dacdiem_grid.Name = "Dacdiem_grid";
            this.Dacdiem_grid.Visible = true;
            this.Dacdiem_grid.VisibleIndex = 6;
            this.Dacdiem_grid.Width = 82;
            // 
            // Ngaylap_grid
            // 
            this.Ngaylap_grid.Caption = "Ngày tạo";
            this.Ngaylap_grid.FieldName = "Ngaylap";
            this.Ngaylap_grid.Name = "Ngaylap_grid";
            this.Ngaylap_grid.Visible = true;
            this.Ngaylap_grid.VisibleIndex = 9;
            this.Ngaylap_grid.Width = 88;
            // 
            // HotenNV_grid
            // 
            this.HotenNV_grid.Caption = "Người tạo";
            this.HotenNV_grid.FieldName = "hotennv";
            this.HotenNV_grid.Name = "HotenNV_grid";
            this.HotenNV_grid.Visible = true;
            this.HotenNV_grid.VisibleIndex = 10;
            this.HotenNV_grid.Width = 86;
            // 
            // Makh_grid1
            // 
            this.Makh_grid1.Caption = "Mã khách hàng";
            this.Makh_grid1.FieldName = "Makh";
            this.Makh_grid1.Name = "Makh_grid1";
            this.Makh_grid1.Visible = true;
            this.Makh_grid1.VisibleIndex = 7;
            this.Makh_grid1.Width = 20;
            // 
            // Tenkh_grid1
            // 
            this.Tenkh_grid1.Caption = "Tên khách hàng";
            this.Tenkh_grid1.FieldName = "TenKH";
            this.Tenkh_grid1.Name = "Tenkh_grid1";
            this.Tenkh_grid1.Visible = true;
            this.Tenkh_grid1.VisibleIndex = 8;
            this.Tenkh_grid1.Width = 20;
            // 
            // Code_grid
            // 
            this.Code_grid.Caption = "ID";
            this.Code_grid.FieldName = "Code";
            this.Code_grid.Name = "Code_grid";
            this.Code_grid.Visible = true;
            this.Code_grid.VisibleIndex = 0;
            this.Code_grid.Width = 52;
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // repositoryItemCalcEdit2
            // 
            this.repositoryItemCalcEdit2.AutoHeight = false;
            this.repositoryItemCalcEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit2.Name = "repositoryItemCalcEdit2";
            // 
            // repositoryItemCalcEdit3
            // 
            this.repositoryItemCalcEdit3.AutoHeight = false;
            this.repositoryItemCalcEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit3.Name = "repositoryItemCalcEdit3";
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ckDinhMucVatTu);
            this.panelControl1.Controls.Add(this.ckQuyTrinhSanXuat);
            this.panelControl1.Controls.Add(this.btnUpdate);
            this.panelControl1.Controls.Add(this.btnXemBanVe);
            this.panelControl1.Controls.Add(this.dpden_ngay);
            this.panelControl1.Controls.Add(this.label40);
            this.panelControl1.Controls.Add(this.cbTen_vatlieu);
            this.panelControl1.Controls.Add(this.txtCode);
            this.panelControl1.Controls.Add(this.txtMa_Vatlieu);
            this.panelControl1.Controls.Add(this.btnTraCuuTatCa);
            this.panelControl1.Controls.Add(this.btnthem);
            this.panelControl1.Controls.Add(this.btnTheVatLieu);
            this.panelControl1.Controls.Add(this.btnxoa);
            this.panelControl1.Controls.Add(this.dptu_ngay);
            this.panelControl1.Controls.Add(this.txtCodeKhachHang);
            this.panelControl1.Controls.Add(this.Code);
            this.panelControl1.Controls.Add(this.btnhuy);
            this.panelControl1.Controls.Add(this.label39);
            this.panelControl1.Controls.Add(this.btnThemKH);
            this.panelControl1.Controls.Add(this.btnsua);
            this.panelControl1.Controls.Add(this.txtDacDiem);
            this.panelControl1.Controls.Add(this.dpNgayLap);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.cbTenKhachHang);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.txtSoThuTu);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.txtTenSanPham);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.txtmasp);
            this.panelControl1.Controls.Add(this.label12);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.label15);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(990, 143);
            this.panelControl1.TabIndex = 1065;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // ckDinhMucVatTu
            // 
            this.ckDinhMucVatTu.AutoSize = true;
            this.ckDinhMucVatTu.Location = new System.Drawing.Point(471, 124);
            this.ckDinhMucVatTu.Name = "ckDinhMucVatTu";
            this.ckDinhMucVatTu.Size = new System.Drawing.Size(104, 17);
            this.ckDinhMucVatTu.TabIndex = 1068;
            this.ckDinhMucVatTu.Text = "Định mức vật tư";
            this.ckDinhMucVatTu.UseVisualStyleBackColor = true;
            // 
            // ckQuyTrinhSanXuat
            // 
            this.ckQuyTrinhSanXuat.AutoSize = true;
            this.ckQuyTrinhSanXuat.Location = new System.Drawing.Point(471, 104);
            this.ckQuyTrinhSanXuat.Name = "ckQuyTrinhSanXuat";
            this.ckQuyTrinhSanXuat.Size = new System.Drawing.Size(116, 17);
            this.ckQuyTrinhSanXuat.TabIndex = 1067;
            this.ckQuyTrinhSanXuat.Text = "Quy trình sản xuất";
            this.ckQuyTrinhSanXuat.UseVisualStyleBackColor = true;
            // 
            // frmCapNhatDanhMucSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 452);
            this.Controls.Add(this.btnExportsx);
            this.Controls.Add(this.grDanhMucSanPham);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmCapNhatDanhMucSanPham";
            this.Text = "Cập nhật danh mục sản phẩm";
            this.Load += new System.EventHandler(this.UcDMSanPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grDanhMucSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhMucSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnExportsx;
        private DevExpress.XtraEditors.SimpleButton btnTraCuuTatCa;
        private DevExpress.XtraGrid.GridControl grDanhMucSanPham;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDanhMucSanPham;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn Masp_grid;
        private DevExpress.XtraGrid.Columns.GridColumn Tensp_grid;
        private DevExpress.XtraGrid.Columns.GridColumn Vatlieu_grid;
        private DevExpress.XtraGrid.Columns.GridColumn Dacdiem_grid;
        private DevExpress.XtraEditors.SimpleButton btnhuy;
        private DevExpress.XtraEditors.SimpleButton btnsua;
        private DevExpress.XtraEditors.SimpleButton btnxoa;
        private DevExpress.XtraEditors.SimpleButton btnthem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbTenKhachHang;
        private System.Windows.Forms.DateTimePicker dpNgayLap;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.Columns.GridColumn Ngaylap_grid;
        private System.Windows.Forms.TextBox txtTenSanPham;
        private System.Windows.Forms.TextBox txtmasp;
        private System.Windows.Forms.TextBox txtDacDiem;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraGrid.Columns.GridColumn HotenNV_grid;
        private System.Windows.Forms.DateTimePicker dpden_ngay;
        private System.Windows.Forms.DateTimePicker dptu_ngay;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private DevExpress.XtraEditors.SimpleButton btnThemKH;
        private System.Windows.Forms.TextBox txtCodeKhachHang;
        private DevExpress.XtraGrid.Columns.GridColumn Makh_grid1;
        private DevExpress.XtraGrid.Columns.GridColumn Tenkh_grid1;
        private System.Windows.Forms.TextBox txtSoThuTu;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnTheVatLieu;
        private System.Windows.Forms.TextBox txtMa_Vatlieu;
        private System.Windows.Forms.ComboBox cbTen_vatlieu;
        private DevExpress.XtraEditors.SimpleButton btnXemBanVe;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label Code;
        private DevExpress.XtraGrid.Columns.GridColumn Code_grid;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colQuyTrinh;
        private DevExpress.XtraGrid.Columns.GridColumn colDinhMuc;
        private System.Windows.Forms.CheckBox ckDinhMucVatTu;
        private System.Windows.Forms.CheckBox ckQuyTrinhSanXuat;
    }
}
