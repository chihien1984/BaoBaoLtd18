namespace quanlysanxuat
{
    partial class UCDANHMUC_PSX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDANHMUC_PSX));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnrefresh_fomload = new DevExpress.XtraEditors.SimpleButton();
            this.dpden_ngay = new System.Windows.Forms.DateTimePicker();
            this.ExportDH = new DevExpress.XtraEditors.SimpleButton();
            this.dptu_ngay = new System.Windows.Forms.DateTimePicker();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.btnrefresh_DH = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.thoigiancapnhat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nguoicapnhat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.madonhang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.machitiet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenquicach = new DevExpress.XtraGrid.Columns.GridColumn();
            this.donvitinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Soluong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ngaygiaohang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.khachhangchitiet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.masp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.maubanve = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tonkho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ghichu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ngoaiquang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Iden = new DevExpress.XtraGrid.Columns.GridColumn();
            this.codeDH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ngoaite = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tygia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Pheduyet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nvkd1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.loaidonhang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.loaikhachhang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.diengiaidonhang = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnrefresh_fomload);
            this.groupControl2.Controls.Add(this.dpden_ngay);
            this.groupControl2.Controls.Add(this.ExportDH);
            this.groupControl2.Controls.Add(this.dptu_ngay);
            this.groupControl2.Controls.Add(this.label29);
            this.groupControl2.Controls.Add(this.label30);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1209, 47);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "DANH MỤC ĐƠN HÀNG";
            // 
            // btnrefresh_fomload
            // 
            this.btnrefresh_fomload.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btnrefresh_fomload.Appearance.Options.UseForeColor = true;
            this.btnrefresh_fomload.Image = ((System.Drawing.Image)(resources.GetObject("btnrefresh_fomload.Image")));
            this.btnrefresh_fomload.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnrefresh_fomload.Location = new System.Drawing.Point(318, 21);
            this.btnrefresh_fomload.Name = "btnrefresh_fomload";
            this.btnrefresh_fomload.Size = new System.Drawing.Size(47, 22);
            this.btnrefresh_fomload.TabIndex = 1058;
            this.btnrefresh_fomload.Text = "Lọc";
            this.btnrefresh_fomload.Click += new System.EventHandler(this.LoaDH_CTTime);
            // 
            // dpden_ngay
            // 
            this.dpden_ngay.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpden_ngay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpden_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpden_ngay.Location = new System.Drawing.Point(220, 22);
            this.dpden_ngay.Name = "dpden_ngay";
            this.dpden_ngay.Size = new System.Drawing.Size(95, 21);
            this.dpden_ngay.TabIndex = 1057;
            this.dpden_ngay.TabStop = false;
            // 
            // ExportDH
            // 
            this.ExportDH.Image = ((System.Drawing.Image)(resources.GetObject("ExportDH.Image")));
            this.ExportDH.Location = new System.Drawing.Point(370, 21);
            this.ExportDH.Name = "ExportDH";
            this.ExportDH.Size = new System.Drawing.Size(62, 22);
            this.ExportDH.TabIndex = 440;
            this.ExportDH.Text = "Export";
            this.ExportDH.Click += new System.EventHandler(this.ExportDH_Click);
            // 
            // dptu_ngay
            // 
            this.dptu_ngay.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptu_ngay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptu_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptu_ngay.Location = new System.Drawing.Point(88, 22);
            this.dptu_ngay.Name = "dptu_ngay";
            this.dptu_ngay.Size = new System.Drawing.Size(96, 21);
            this.dptu_ngay.TabIndex = 1055;
            this.dptu_ngay.TabStop = false;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.ForeColor = System.Drawing.Color.Green;
            this.label29.Location = new System.Drawing.Point(190, 26);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(27, 13);
            this.label29.TabIndex = 1053;
            this.label29.Text = "Đến";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.ForeColor = System.Drawing.Color.Green;
            this.label30.Location = new System.Drawing.Point(15, 26);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(68, 13);
            this.label30.TabIndex = 1054;
            this.label30.Text = "Đơn hàng từ";
            // 
            // btnrefresh_DH
            // 
            this.btnrefresh_DH.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh_DH.Appearance.Options.UseFont = true;
            this.btnrefresh_DH.Image = ((System.Drawing.Image)(resources.GetObject("btnrefresh_DH.Image")));
            this.btnrefresh_DH.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnrefresh_DH.Location = new System.Drawing.Point(0, 48);
            this.btnrefresh_DH.Name = "btnrefresh_DH";
            this.btnrefresh_DH.Size = new System.Drawing.Size(18, 19);
            this.btnrefresh_DH.TabIndex = 1062;
            this.btnrefresh_DH.Click += new System.EventHandler(this.LoaDH_CT_ALL);
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 47);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1209, 391);
            this.gridControl2.TabIndex = 1063;
            this.gridControl2.UseEmbeddedNavigator = true;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.thoigiancapnhat,
            this.nguoicapnhat,
            this.madonhang,
            this.machitiet,
            this.tenquicach,
            this.donvitinh,
            this.Soluong,
            this.ngaygiaohang,
            this.khachhangchitiet,
            this.masp,
            this.maubanve,
            this.tonkho,
            this.ghichu,
            this.ngoaiquang,
            this.Iden,
            this.codeDH,
            this.ngoaite,
            this.tygia,
            this.Pheduyet,
            this.nvkd1,
            this.loaidonhang,
            this.loaikhachhang,
            this.diengiaidonhang});
            gridFormatRule2.Name = "Format0";
            gridFormatRule2.Rule = null;
            this.gridView2.FormatRules.Add(gridFormatRule2);
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowAutoFilterRow = true;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // thoigiancapnhat
            // 
            this.thoigiancapnhat.Caption = "Ngày lập";
            this.thoigiancapnhat.DisplayFormat.FormatString = "dd/MM/yyyy H:mm";
            this.thoigiancapnhat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.thoigiancapnhat.FieldName = "thoigianthaydoi";
            this.thoigiancapnhat.Name = "thoigiancapnhat";
            this.thoigiancapnhat.Visible = true;
            this.thoigiancapnhat.VisibleIndex = 3;
            this.thoigiancapnhat.Width = 121;
            // 
            // nguoicapnhat
            // 
            this.nguoicapnhat.Caption = "Người lập";
            this.nguoicapnhat.FieldName = "nguoithaydoi";
            this.nguoicapnhat.Name = "nguoicapnhat";
            this.nguoicapnhat.Visible = true;
            this.nguoicapnhat.VisibleIndex = 2;
            this.nguoicapnhat.Width = 100;
            // 
            // madonhang
            // 
            this.madonhang.Caption = "MÃ ĐH";
            this.madonhang.DisplayFormat.FormatString = "d";
            this.madonhang.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.madonhang.FieldName = "madh";
            this.madonhang.Name = "madonhang";
            this.madonhang.Visible = true;
            this.madonhang.VisibleIndex = 1;
            this.madonhang.Width = 109;
            // 
            // machitiet
            // 
            this.machitiet.Caption = "Số CT ĐH";
            this.machitiet.FieldName = "Codedetail";
            this.machitiet.Name = "machitiet";
            this.machitiet.Visible = true;
            this.machitiet.VisibleIndex = 4;
            this.machitiet.Width = 80;
            // 
            // tenquicach
            // 
            this.tenquicach.Caption = "TÊN HÀNG VÀ QC SP";
            this.tenquicach.FieldName = "Tenquicach";
            this.tenquicach.Name = "tenquicach";
            this.tenquicach.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Tenquicach", "Đếm: {0}")});
            this.tenquicach.Visible = true;
            this.tenquicach.VisibleIndex = 6;
            this.tenquicach.Width = 174;
            // 
            // donvitinh
            // 
            this.donvitinh.Caption = "ĐVT";
            this.donvitinh.FieldName = "dvt";
            this.donvitinh.Name = "donvitinh";
            this.donvitinh.Visible = true;
            this.donvitinh.VisibleIndex = 7;
            this.donvitinh.Width = 34;
            // 
            // Soluong
            // 
            this.Soluong.Caption = "SỐ LƯỢNG";
            this.Soluong.DisplayFormat.FormatString = "n0";
            this.Soluong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Soluong.FieldName = "Soluong";
            this.Soluong.Name = "Soluong";
            this.Soluong.Visible = true;
            this.Soluong.VisibleIndex = 8;
            // 
            // ngaygiaohang
            // 
            this.ngaygiaohang.Caption = "NGÀY GIAO";
            this.ngaygiaohang.FieldName = "ngaygiao";
            this.ngaygiaohang.Name = "ngaygiaohang";
            this.ngaygiaohang.Visible = true;
            this.ngaygiaohang.VisibleIndex = 9;
            // 
            // khachhangchitiet
            // 
            this.khachhangchitiet.Caption = "KHÁCH HÀNG";
            this.khachhangchitiet.FieldName = "Khachhang";
            this.khachhangchitiet.Name = "khachhangchitiet";
            this.khachhangchitiet.Visible = true;
            this.khachhangchitiet.VisibleIndex = 10;
            // 
            // masp
            // 
            this.masp.Caption = "MÃ SP";
            this.masp.FieldName = "MaSP";
            this.masp.Name = "masp";
            this.masp.Visible = true;
            this.masp.VisibleIndex = 5;
            this.masp.Width = 93;
            // 
            // maubanve
            // 
            this.maubanve.Caption = "MẪU BV";
            this.maubanve.FieldName = "Mau_banve";
            this.maubanve.Name = "maubanve";
            this.maubanve.Visible = true;
            this.maubanve.VisibleIndex = 11;
            // 
            // tonkho
            // 
            this.tonkho.Caption = "TỒN KHO";
            this.tonkho.FieldName = "Tonkho";
            this.tonkho.Name = "tonkho";
            this.tonkho.Visible = true;
            this.tonkho.VisibleIndex = 12;
            // 
            // ghichu
            // 
            this.ghichu.Caption = "GHI CHÚ";
            this.ghichu.FieldName = "ghichu";
            this.ghichu.Name = "ghichu";
            this.ghichu.Visible = true;
            this.ghichu.VisibleIndex = 13;
            // 
            // ngoaiquang
            // 
            this.ngoaiquang.Caption = "NGOẠI QUANG";
            this.ngoaiquang.FieldName = "ngoaiquang";
            this.ngoaiquang.Name = "ngoaiquang";
            this.ngoaiquang.Visible = true;
            this.ngoaiquang.VisibleIndex = 14;
            // 
            // Iden
            // 
            this.Iden.Caption = "Iden";
            this.Iden.FieldName = "Iden";
            this.Iden.Name = "Iden";
            this.Iden.Visible = true;
            this.Iden.VisibleIndex = 15;
            // 
            // codeDH
            // 
            this.codeDH.Caption = "Code";
            this.codeDH.FieldName = "Code";
            this.codeDH.Name = "codeDH";
            this.codeDH.Visible = true;
            this.codeDH.VisibleIndex = 16;
            // 
            // ngoaite
            // 
            this.ngoaite.Caption = "Ngoại tệ";
            this.ngoaite.FieldName = "usd";
            this.ngoaite.Name = "ngoaite";
            this.ngoaite.Visible = true;
            this.ngoaite.VisibleIndex = 17;
            this.ngoaite.Width = 60;
            // 
            // tygia
            // 
            this.tygia.Caption = "Tỷ giá";
            this.tygia.FieldName = "tygia";
            this.tygia.Name = "tygia";
            this.tygia.Visible = true;
            this.tygia.VisibleIndex = 18;
            // 
            // Pheduyet
            // 
            this.Pheduyet.Caption = "PHÊ DUYỆT";
            this.Pheduyet.FieldName = "pheduyet";
            this.Pheduyet.Name = "Pheduyet";
            this.Pheduyet.Visible = true;
            this.Pheduyet.VisibleIndex = 0;
            this.Pheduyet.Width = 67;
            // 
            // nvkd1
            // 
            this.nvkd1.Caption = "Kinh Doanh";
            this.nvkd1.FieldName = "nvkd";
            this.nvkd1.Name = "nvkd1";
            this.nvkd1.Visible = true;
            this.nvkd1.VisibleIndex = 19;
            this.nvkd1.Width = 113;
            // 
            // loaidonhang
            // 
            this.loaidonhang.Caption = "Loại ĐH";
            this.loaidonhang.FieldName = "LoaiDH";
            this.loaidonhang.Name = "loaidonhang";
            this.loaidonhang.Visible = true;
            this.loaidonhang.VisibleIndex = 20;
            this.loaidonhang.Width = 57;
            // 
            // loaikhachhang
            // 
            this.loaikhachhang.Caption = "Loại KH";
            this.loaikhachhang.FieldName = "PhanloaiKH";
            this.loaikhachhang.Name = "loaikhachhang";
            this.loaikhachhang.Visible = true;
            this.loaikhachhang.VisibleIndex = 21;
            this.loaikhachhang.Width = 55;
            // 
            // diengiaidonhang
            // 
            this.diengiaidonhang.Caption = "DIỄN GIẢI ĐH";
            this.diengiaidonhang.FieldName = "Diengiai";
            this.diengiaidonhang.Name = "diengiaidonhang";
            this.diengiaidonhang.Visible = true;
            this.diengiaidonhang.VisibleIndex = 22;
            this.diengiaidonhang.Width = 100;
            // 
            // UCDANHMUC_PSX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnrefresh_DH);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.groupControl2);
            this.Name = "UCDANHMUC_PSX";
            this.Size = new System.Drawing.Size(1209, 438);
            this.Load += new System.EventHandler(this.UCDANHMUC_PSX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.DateTimePicker dpden_ngay;
        private DevExpress.XtraEditors.SimpleButton ExportDH;
        private System.Windows.Forms.DateTimePicker dptu_ngay;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private DevExpress.XtraEditors.SimpleButton btnrefresh_DH;
        private DevExpress.XtraEditors.SimpleButton btnrefresh_fomload;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn madonhang;
        private DevExpress.XtraGrid.Columns.GridColumn machitiet;
        private DevExpress.XtraGrid.Columns.GridColumn tenquicach;
        private DevExpress.XtraGrid.Columns.GridColumn donvitinh;
        private DevExpress.XtraGrid.Columns.GridColumn Soluong;
        private DevExpress.XtraGrid.Columns.GridColumn ngaygiaohang;
        private DevExpress.XtraGrid.Columns.GridColumn khachhangchitiet;
        private DevExpress.XtraGrid.Columns.GridColumn masp;
        private DevExpress.XtraGrid.Columns.GridColumn maubanve;
        private DevExpress.XtraGrid.Columns.GridColumn tonkho;
        private DevExpress.XtraGrid.Columns.GridColumn ghichu;
        private DevExpress.XtraGrid.Columns.GridColumn ngoaiquang;
        private DevExpress.XtraGrid.Columns.GridColumn Iden;
        private DevExpress.XtraGrid.Columns.GridColumn codeDH;
        private DevExpress.XtraGrid.Columns.GridColumn ngoaite;
        private DevExpress.XtraGrid.Columns.GridColumn tygia;
        private DevExpress.XtraGrid.Columns.GridColumn Pheduyet;
        private DevExpress.XtraGrid.Columns.GridColumn nvkd1;
        private DevExpress.XtraGrid.Columns.GridColumn loaidonhang;
        private DevExpress.XtraGrid.Columns.GridColumn loaikhachhang;
        private DevExpress.XtraGrid.Columns.GridColumn diengiaidonhang;
        private DevExpress.XtraGrid.Columns.GridColumn nguoicapnhat;
        private DevExpress.XtraGrid.Columns.GridColumn thoigiancapnhat;
    }
}
