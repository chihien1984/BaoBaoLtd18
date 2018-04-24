using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Spire.Pdf;
using Spire.Pdf.Graphics;

namespace quanlysanxuat
{
    public partial class UcDM_SANPHAM : DevExpress.XtraEditors.XtraForm
    {
        public UcDM_SANPHAM()
        {
            InitializeComponent();
        }
       
        private void TheHienCauTrucSanPham()
        {
            ketnoi kn = new ketnoi();
            grSanPham.DataSource = kn.laybang(@"select c.Masp,s.Tensp,MaCum,TenCum,
                SoLuongCum,Mact,Ten_ct,Soluong_CT,''KichThuc,''GhiChu
                from tblSANPHAM_CT c
                inner join tblSANPHAM s
                on c.SanPhamID=s.Code
                where MaCum <>'' and TenCum <>''");
            kn.dongketnoi();
        }

    
        private void show_CTsanpham_Click(object sender, EventArgs e)
        {
            TheHienTraCuuCongDoanTreeListStages();
        }
        private async void TheHienTraCuuCongDoanTreeListStages()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select case when DonGiaCongDoan>0 then 'x' end CoGia,* from tblSanPhamTreeList
				order by IDSanPham Desc, ThuTu ASC");
                Invoke((Action)(() => {
                    treeListProductionStages.DataSource = Model.Function.GetDataTable(sqlQuery);
                    treeListProductionStages.ForceInitialize();
                    treeListProductionStages.ExpandAll();
                    treeListProductionStages.OptionsSelection.MultiSelect = true;
                    //treeListProductionStages.BestFitColumns();
                }));
            });
        }

        private void TheHienTraCuuCongDoanTreeListStagesTheoSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"	select case when DonGiaCongDoan>0 then 'x'
				end CoGia,* from tblSanPhamTreeList
				where MaSanPham like N'{0}'
				order by IDSanPham Desc, ThuTu ASC",maSanPham);
            treeListProductionStages.DataSource =
                kn.laybang(sqlQuery);
            treeListProductionStages.ForceInitialize();
            treeListProductionStages.ExpandAll();
            //treeListProductionStages.BestFitColumns();
            treeListProductionStages.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
        }

        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            Path path = new Path();
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, path.pathbanve);
            f2.Show();
        }
        private void BackgroundPDF()
        {
            string pat = string.Format(@"{0}\{1}.pdf", AppDomain.CurrentDomain.BaseDirectory, "Han");
            string pat1 = string.Format(@"{0}\{1}.ico", AppDomain.CurrentDomain.BaseDirectory, "LogoBaoBao");
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(pat);
            PdfImage image = PdfImage.FromFile(pat1);
            foreach (PdfPageBase page in pdf.Pages)
            {
                //PdfTilingBrush brush = new PdfTilingBrush(new SizeF(page.Canvas.Size.Width / 3, page.Canvas.Size.Height / 5));
                //brush.Graphics.SetTransparency(0.2f);
                //brush.Graphics.DrawImage(image, new PointF((brush.Size.Width - image.Width) / 2, (brush.Size.Height - image.Height) / 2));
                //page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page.Canvas.Size));
                PdfTilingBrush brush = new PdfTilingBrush(new SizeF(page.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
                brush.Graphics.SetTransparency(0.3f);
                brush.Graphics.Save();
                brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
                brush.Graphics.RotateTransform(-45);
                brush.Graphics.DrawString(Login.Username, new PdfFont(PdfFontFamily.Helvetica, 24), PdfBrushes.Blue, 0, 0, new PdfStringFormat(PdfTextAlignment.Center));
                brush.Graphics.Restore();
                brush.Graphics.SetTransparency(1);
                page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page.Canvas.ClientSize));
            }
            pdf.SaveToFile(txtMaSanPham.Text + ".pdf");
        }
        public static Icon GetIcon(string text)
        {
            string pat1 = string.Format(@"{0}\{1}.ico", AppDomain.CurrentDomain.BaseDirectory, "LogoBaoBao");
            //Create bitmap, kind of canvas
            Bitmap bitmap = new Bitmap(32, 32);
            Icon icon = new Icon(pat1);
            System.Drawing.Font drawFont = new System.Drawing.Font("Calibri", 16, FontStyle.Bold);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            graphics.DrawIcon(icon, 0, 0);
            graphics.DrawString(text, drawFont, drawBrush, 1, 2);
            //To Save icon to disk
            bitmap.Save("icon.ico", System.Drawing.Imaging.ImageFormat.Icon);
            Icon createdIcon = Icon.FromHandle(bitmap.GetHicon());
            drawFont.Dispose();
            drawBrush.Dispose();
            graphics.Dispose();
            bitmap.Dispose();
            return createdIcon;
        }
        private void LoadLayout_KHSX(object sender, EventArgs e)//Sự kiện gọi kế hoạch sản xuất 
        {
        
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            grSanPham.ShowPrintPreview();
        }
     

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            DataRow rowData;
            int[] listRowList = this.gvSanPham.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvSanPham.GetDataRow(listRowList[i]);
                string sqlQuery = string.Format(@"update tblSANPHAM set Trongluong = '{0}'
                    where Code like '{1}'",
                    rowData["Trongluong"],
                    rowData["Code"]);
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
          
        }
        #region formload
        private void UcDM_SANPHAM_Load(object sender, EventArgs e)
        {
            gvSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            TheHienDanhSachSanPham();
            TheHienTraCuuCongDoanTreeListStages();
        }
        #endregion
        private void btnTraCuuSanPham_Click(object sender, EventArgs e)
        {
            TheHienDanhSachSanPham();
        }
        private async void TheHienDanhSachSanPham()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select check1,check2,case when t.IDSanPham 
                is not null then 'x' else '' end TreeList,
				* from tblSANPHAM p left outer join
				(select IDSanPham from tblSanPhamTreeList group by IDSanPham)t
				on p.Code=t.IDSanPham");
                Invoke((Action)(() => {
                    grSanPham.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            xtraTabControl1.SelectedTabPage = xtbsanpham;
        }
        private void HidenButtonTrongLuongSanPham()
        {
            if (gvSanPham.GetSelectedRows().Count() > 0)
            {
                btnCapNhatTrongLuongXuat.Enabled = true;
            }
            else
            {
                btnCapNhatTrongLuongXuat.Enabled = false;
            }
        }

        public virtual bool FixedWidth { get; set; }
        private void btnExportCauTrucSanPham_Click(object sender, EventArgs e)
        {}
        string maSanPham;
        private void grSanPham_Click(object sender, EventArgs e)
        {
            //if (gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
            //gridLookUpEditViewListView.Columns["Num"]) != null)
            //{
            //    txtMaSanPham.Text = gridLookUpEditViewListView.GetRowCellValue(gridLookUpEditViewListView.FocusedRowHandle,
            //        gridLookUpEditViewListView.Columns[""]).ToString();
            //}
            if (gvSanPham.GetRowCellValue(gvSanPham.FocusedRowHandle, gvSanPham.Columns["Code"]) != null)
            {
                maSanPham = gvSanPham.GetFocusedRowCellDisplayText(masp_sanpham);
                TheHienTraCuuCongDoanTreeListStagesTheoSanPham();
                txtMaSanPham.Text = maSanPham;
            }
        }

        private void grSanPham_MouseMove(object sender, MouseEventArgs e)
        {
            HidenButtonTrongLuongSanPham();
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            treeListProductionStages.ShowPrintPreview();
        }

        private void btnXemPhuKien_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 = new frmLoading(txtMaPhuKien.Text, path.pathbanve);
            f2.Show();
        }

        private void treeListProductionStages_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            //if (treeListProductionStages.GetRowCellValue(treeListProductionStages.FocusedRowHandle, treeListProductionStages.Columns["ID"]).ToString() == "")
            //{ return; }
            //else { 
                string point;
            point = treeListProductionStages.GetFocusedDisplayText();
            txtMaSanPhamChiTiet.Text = treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesMaSanPham);
            txtMaPhuKien.Text = treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesMaChiTiet);
            //}
        }

        private void btnCapNhatTrongLuongXuat_Click(object sender, EventArgs e)
        {}

        private void btnQuyTrinhSanXuat_Click(object sender, EventArgs e)
        {
            string maQuyTrinh = "QTSX-" + txtMaSanPham.Text;
            Path path = new Path();
            frmLoading f2 = new frmLoading(maQuyTrinh, path.pathquytrinh);
            f2.Show();
        }

        private void btnDinhMucVatLieu_Click(object sender, EventArgs e)
        {
            string maQuyTrinh = "DMVT-" + txtMaSanPham.Text;
            Path path = new Path();
            frmLoading f2 = new frmLoading(maQuyTrinh, path.pathDanhMucVatTu);
            f2.Show();
        }

        private void btnXemBanVeChiTiet_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 = new frmLoading(txtMaSanPhamChiTiet.Text, path.pathbanve);
            f2.Show();
        }
    }
}
