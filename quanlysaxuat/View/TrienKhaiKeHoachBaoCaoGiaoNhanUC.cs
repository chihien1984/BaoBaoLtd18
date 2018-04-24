using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using quanlysanxuat.Model;
using quanlysanxuat.Report;
using quanlysanxuat.View.UcControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class TrienKhaiKeHoachBaoCaoGiaoNhanUC : DevExpress.XtraEditors.XtraForm
    {
        public TrienKhaiKeHoachBaoCaoGiaoNhanUC()
        {
            InitializeComponent();
        }

        private void btnShowPlantMadeProductionStages_Click(object sender, EventArgs e)
        {
            ShowPlanMadeProductionStages();
        }
        private async void ShowPlanMadeProductionStages()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select b.SoLuongGiao,b.NgayGiao,
                 case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
              when cast(GetDate() as date)<=KetThuc and (isnull(b.SoLuongGiao,0) < 1 or b.SoLuongGiao is null) then N'Chưa khởi động'
              when cast(GetDate() as date)> KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
              when cast(GetDate() as date)<= KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
              end TinhTrang,a.* from
			  (select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
               MaDonHang,MaPo,MaSanPham,
               TenSanPham,TenLoai,SoLuongDonHang,
               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
               SoLuongYCSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
               TenCongDoan,SoThuTu,
               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu
               from TrienKhaiKeHoachSanXuat
               where NgayLap between '{0}' and '{1}')a 
				left outer join
				(select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai>0
			    group by IDTrienKhai
						 union all 
						 select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)b
			    on a.IDTrienKhai=b.IDTrienKhai order by NgayLap desc",
                      dpTu.Value.ToString("yyyy-MM-dd"),
                      dpDen.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    treeListProductionStagesPlan.DataSource = Function.GetDataTable(sqlQuery);
                    treeListProductionStagesPlan.ForceInitialize();
                    treeListProductionStagesPlan.ExpandAll();
                    treeListProductionStagesPlan.OptionsSelection.MultiSelect = true;
                    //treeListProductionStagesPlan.Appearance.Row.Font = new Font("Segoe UI", 8f);
                    xtraTabControl1.SelectedTabPage = xtraTabPageGiaoNhanHangSanXuat;
                }));
            });
        }
        private async void THTrienKhaiKeHoachTop100()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
             string sqlQuery = string.Format(@"select top 100 b.SoLuongGiao,b.NgayGiao,
              case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
              when cast(GetDate() as date)<=KetThuc and (isnull(b.SoLuongGiao,0) < 1 or b.SoLuongGiao is null) then N'Chưa khởi động'
              when cast(GetDate() as date)> KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
              when cast(GetDate() as date)<= KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
              end TinhTrang,a.* from
		       (select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
               MaDonHang,MaPo,MaSanPham,
               TenSanPham,TenLoai,SoLuongDonHang,
               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
               SoLuongYCSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
               TenCongDoan,SoThuTu,
               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu
               from TrienKhaiKeHoachSanXuat
               where NgayLap between '{0}' and '{1}')a 
				left outer join
				(select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai>0
			    group by IDTrienKhai
						 union all 
						 select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)b
			    on a.IDTrienKhai=b.IDTrienKhai order by NgayLap desc",
                      dpTu.Value.ToString("yyyy-MM-dd"),
                      dpDen.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    treeListProductionStagesPlan.DataSource = Function.GetDataTable(sqlQuery);
                    treeListProductionStagesPlan.ForceInitialize();
                    treeListProductionStagesPlan.ExpandAll();
                    treeListProductionStagesPlan.OptionsSelection.MultiSelect = true;
                    xtraTabControl1.SelectedTabPage = xtraTabPageGiaoNhanHangSanXuat;
                }));
            }
            );
        }

        private async void TheHienDonHangChuaXong()
        {
            ketnoi kn = new ketnoi();
            await Task.Run(() =>
            {
            string sqlQuery = string.Format(@"select b.SoLuongGiao,b.NgayGiao,
            case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
              when cast(GetDate() as date)<=KetThuc and (isnull(b.SoLuongGiao,0) < 1 or b.SoLuongGiao is null) then N'Chưa khởi động'
              when cast(GetDate() as date)> KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
              when cast(GetDate() as date)<= KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
         end TinhTrang,a.* from
				(select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
               MaDonHang,MaPo,MaSanPham,
               TenSanPham,TenLoai,SoLuongDonHang,
               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
               SoLuongYCSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
               TenCongDoan,SoThuTu,
               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu
               from TrienKhaiKeHoachSanXuat
               where NgayLap between '{0}' and '{1}')a 
				left outer join
				(select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai>0
			    group by IDTrienKhai
						 union all 
						 select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)b
			    on a.IDTrienKhai=b.IDTrienKhai
                where isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat
                order by NgayLap desc",
                  dpTu.Value.ToString("yyyy-MM-dd"),
                  dpDen.Value.ToString("yyyy-MM-dd"));
            Invoke((Action)(()=>{
                treeListProductionStagesPlan.DataSource = kn.laybang(sqlQuery);
                treeListProductionStagesPlan.ForceInitialize();
                treeListProductionStagesPlan.ExpandAll();
                //treeListProductionStagesPlan.BestFitColumns();
                treeListProductionStagesPlan.OptionsSelection.MultiSelect = true;
                kn.dongketnoi();
                treeListProductionStagesPlan.Appearance.Row.Font = new Font("Segoe UI", 8f);
                xtraTabControl1.SelectedTabPage = xtraTabPageGiaoNhanHangSanXuat;
                }));
            });
        }
        private void ShowPlanMadeDetailProductionStages()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"execute TraCuuTrienKhaiKeHoach '{0}','{1}'",
                  dpTu.Value.ToString("yyyy-MM-dd"),
                  dpDen.Value.ToString("yyyy-MM-dd"));
            treeListChiTietGiaoHang.DataSource = Function.GetDataTable(sqlQuery);
            treeListChiTietGiaoHang.ForceInitialize();
            treeListChiTietGiaoHang.ExpandAll();
            treeListChiTietGiaoHang.OptionsSelection.MultiSelect = true;
            treeListChiTietGiaoHang.Appearance.Row.Font = new Font("Segoe UI", 8f);
            xtraTabControl1.SelectedTabPage = xtraTabPageChiTietGiaoNhanSanXuat;
        }
        private void ShowPlanMadeDetailProductionStagesfollowIDChiTietDH()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from TrienKhaiKeHoachSanXuat 
				where NgayLap 
				between '{0}' and '{1}' and IDChiTietDonHang like '{2}'",
                  dpTu.Value.ToString("yyyy-MM-dd"),
                  dpDen.Value.ToString("yyyy-MM-dd"),
                  idchitietdonhang);
            treeListChiTietGiaoHang.DataSource =
            kn.laybang(sqlQuery);
            treeListChiTietGiaoHang.ForceInitialize();
            treeListChiTietGiaoHang.ExpandAll();
            //treeListChiTietGiaoHang.BestFitColumns();
            treeListChiTietGiaoHang.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
            treeListChiTietGiaoHang.Appearance.Row.Font = new Font("Segoe UI", 8f);
        }
        private void btnTraCuuChiTietGiaoHang_Click(object sender, EventArgs e)
        {
            ShowPlanMadeDetailProductionStages();
        }

        #region formload
        private void GiaoNhanHangCacToUC_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            gvXuatHangDenGiaCong.Appearance.Row.Font = new Font("Segoe UI", 7f);
            gvNhapKho.Appearance.Row.Font = new Font("Segoe UI", 7f);
            gvSoChiTietNhanHangDen.Appearance.Row.Font = new Font("Segoe UI", 7f);
            gvTongHopCongDoanSanXuat.Appearance.Row.Font = new Font("Segoe UI", 8f);
            treeListProductionStagesPlan.Appearance.Row.Font = new Font("Segoe UI", 8f);
            THTrienKhaiKeHoachTop100();
            THNoiDungBaoCao();
            //THThongKeBaoCao();


            //ThQuyenGhiToaGiao();
            //TheHienBoPhanGiaCongNoiBo();
            //TheHienDonViGiaCongNgoai();
            //TheHienNhanHangDenTheoNgay();//Sổ nhận hàng đen
            //TheHienTraCuuXuatHangDenGiaCong();//sổ xuất hàng đen gia công
            //TheHienDanhMucNhapKhoTheoNgay();//Sổ nhập kho hàng đen
            //TheHienDonHangChuaXong();
        }
        #endregion
        //Thể hiện nội dung báo cáo
        private void THNoiDungBaoCao()
        {
            string sqlQuery = string.Format(@"select NoiDungChiTiet from BaoCaoToChiTiet");
            var dt = Function.GetDataTable(sqlQuery);
            foreach (DataRow dr in dt.Rows)
            {
                string strCaption = dr["NoiDungChiTiet"].ToString();
                repositoryItemComboBox1.Items.Add(strCaption);
            }
        }

        string pointsave;
        private void CreateCodePointSave()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select Top 1 
				REPLACE(convert(nvarchar,GetDate(),11),'/','') 
				+replace(replace(left(CONVERT(time, GetDate()),12),':',''),'.','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                pointsave = Convert.ToString(reader[0]);
            reader.Close();
        }
        //ghi du lieu giao hang vao danh sach giao hang
        private void btnAddGiaoNhanHang_Click(object sender, EventArgs e)
        {
            CreateCodePointSave();
            List<TreeListNode> list = treeListProductionStagesPlan.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                      @"insert into TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				        (IDTrienKhai,ChildID,ParentID,
                         PointSave,MaCong,TenCong,
                         ToThucHien,SoLuongLoai,
                         NguoiGhiGiao,NgayGhiGiao) values 
				        ('{0}','{1}','{2}',
                         '{3}',N'{4}',N'{5}',N'{6}',
                         '{7}',N'{8}',GetDate())",
                      node.GetDisplayText(treeListPlanIDTrienKhai),
                      node.GetDisplayText(treeListPlanID),
                      node.GetDisplayText(treeListPlanParentID),
                      pointsave,
                      node.GetDisplayText(treeListPlanMaCongDoan),
                      node.GetDisplayText(treeListPlanTenLoai),
                      node.GetDisplayText(treeListPlanToThucHien),
                      node.GetDisplayText(treeListPlanSoLuongLoai),
                      node.GetDisplayText(treeListPlanIDDonHangChiTiet),
                      Login.Username);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            ShowReceivePointSaveMadeProduction();
        }
        //Sau khi dong bang du lieu neu không ghi so luong giao hang thi xoa di du lieu trống
        private void XoaDuLieuGiaoHangRong()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"delete from 
                         TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
						 where SoGiao is null and NgayGiao is 
                         null and PointSave like '{0}'", pointsave);
            var dt = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void UpdateSoLuongGiaoHangVaoSoTrienKhai()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuat
						 set SoLuongGiao =g.SoGiao,NgayGiao=g.NgayGiao
						 from (select IDTrienKhai,max(NgayGiao)NgayGiao,sum(SoGiao)SoGiao		 
						 from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet group by IDTrienKhai)g
						 inner join TrienKhaiKeHoachSanXuat t
						 on t.IDTrienKhai=g.IDTrienKhai");
            var dt = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void ShowReceivePointSaveMadeProduction()
        {
            //frmGiaoHangCacTo giaonhan = new frmGiaoHangCacTo(pointsave);
            //giaonhan.ShowDialog();
            UpdateSoLuongGiaoHangVaoSoTrienKhai();
            XoaDuLieuGiaoHangRong();//Xóa các dữ liệu có số giao là null
            ShowPlanMadeProductionStages();
        }
        #region sự kiện checkedchildNode đồng thời checkedParentNode
        private void treeListProductionStages_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node.Checked)
            {
                node.UncheckAll();
            }
            else
            {
                node.CheckAll();
            }
            while (node.ParentNode != null)
            {
                node = node.ParentNode;
                bool oneOfChildIsChecked = OneOfChildsIsChecked(node);
                if (oneOfChildIsChecked)    
                {
                    node.CheckState = CheckState.Checked;
                }
                else
                {
                    node.CheckState = CheckState.Unchecked;
                }
            }
        }
        private bool OneOfChildsIsChecked(TreeListNode node)
        {
            bool result = false;
            foreach (TreeListNode item in node.Nodes)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion
        private string idchitietdonhang;
        private string idtrienkhai;
        private string parentid;
        private string childid;
        private string masanpham;
        private string tensanpham;
        private string machitiet;
        private string tenchitiet;
        private string sochitiet;
        private string ngayghi;
        private string soluonghoanthanh;
        private string tothuchien;
        private string congdoan;
        private string madonhang;
        private string mapo;


        private void treeListProductionStagesPlan_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            btnReportBanVe.Visible = true;
            btnBanVe.Visible = false;


            btnPhuKien1.Visible = true;
            btnPhuKien.Visible = false;
            string point;
            point = treeListProductionStagesPlan.GetFocusedDisplayText();
            idtrienkhai = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanIDTrienKhai);
            parentid = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanParentID);
            childid = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanID);
            tothuchien = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanToThucHien);
            congdoan = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanTenLoai);
            masanpham = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMaSanPham);
            machitiet = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMaChiTiet);
            tensanpham = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanSanPham);
            tenchitiet = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanTenChiTiet);
            sochitiet = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanSoChiTiet);
            idchitietdonhang = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanIDDonHangChiTiet);
            madonhang = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMaDonHang);
            mapo = treeListProductionStagesPlan.GetFocusedRowCellDisplayText(treeListPlanMaPo);
            txtMaSanPham.Text = masanpham;
            txtPhuKien.Text = machitiet;
            ShowPlanMadeDetailProductionStagesfollowIDChiTietDH();
            TheHienNhanHangDenTheoDonHang();//Nhận bán thành phẩm hàng đen
            //TheHienSoLuongDonHang();//Thể hiện số lượng kế hoạch sản xuất
            TheHienSoLuongDonHangTheoTo();
        }


        private void treeListProductionStagesPlan_DoubleClick(object sender, EventArgs e)
        {
            /*  
             if (tothuchien == "")
            {
                MessageBox.Show("Đối tượng bạn chọn chưa có tổ thực hiện", "Thông báo"); return;
            }
            else
            {
                frmGiaoHang giaoHang = new frmGiaoHang
                (idtrienkhai, parentid, childid, masanpham, tensanpham, machitiet,
                tenchitiet, sochitiet, congdoan, tothuchien, idchitietdonhang);
                giaoHang.ShowDialog();
                ShowPlanMadeProductionStages();
                TreeListNode id = treeListProductionStagesPlan.FindNodeByKeyID(idtrienkhai);
                treeListProductionStagesPlan.FocusedNode = id;
            }
             */
       

            //treeListProductionStagesPlan.AddNewRow();
            //treeListProductionStagesPlan.FocusedColumn = treeListProductionStagesPlan.Columns["IDTrienKhai"];
            //treeListProductionStagesPlan.ShowEditor();
            //treeListProductionStagesPlan.GetFocusedRow =
        }

        private void treeListProductionStagesPlan_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Nodes == (sender as TreeList).Nodes[0].Nodes)
                e.CanFocus = true;
        }

        private void btnXemBV_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(txtMaSanPham.Text, path.pathbanve);
            f2.Show();
        }

        private void btnLayout_PSX_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT * from IN_LENHSANXUAT where madh like N'" + madonhang + "'");
            RpLenhSanXuat rpPHIEUSANXUAT = new RpLenhSanXuat();
            rpPHIEUSANXUAT.DataSource = dt;
            rpPHIEUSANXUAT.DataMember = "Table";
            rpPHIEUSANXUAT.CreateDocument(false);
            rpPHIEUSANXUAT.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = madonhang;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT.PrintingSystem);
            rpPHIEUSANXUAT.ShowPreviewDialog();
            kn.dongketnoi();
        }
        private void btnKeHoachVatLieu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi Connect = new ketnoi();
            dt = Connect.laybang("select * from PHIEUSANXUAT where madh like N'" + madonhang + "'");
            XRPhieuSX_DaDuyet rpPHIEUSANXUAT_Duyet = new XRPhieuSX_DaDuyet();
            rpPHIEUSANXUAT_Duyet.DataSource = dt;
            rpPHIEUSANXUAT_Duyet.DataMember = "Table";
            rpPHIEUSANXUAT_Duyet.CreateDocument(false);
            rpPHIEUSANXUAT_Duyet.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = madonhang;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT_Duyet.PrintingSystem);
            rpPHIEUSANXUAT_Duyet.ShowPreviewDialog();
            Connect.dongketnoi();
        }
        private void btnReportLichSanXuat_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from ViewLichSanXuatChiTiet where MucDo <>0 and MaDonHang like N'" + madonhang + "'");
            XRLapLichSanXuat rplich = new XRLapLichSanXuat();
            rplich.DataSource = dt;
            rplich.DataMember = "Table";
            rplich.CreateDocument(false);
            rplich.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = madonhang;
            PrintTool tool = new PrintTool(rplich.PrintingSystem);
            rplich.ShowPreviewDialog();
            kn.dongketnoi();
        }

        ////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////
        /// <summary>
        ///  /*Chức năng xuất hàng đen gia công - hoàn thành gia công nhập kho*/
        /// </summary>
        /*Chức năng xuất hàng đen gia công - hoàn thành gia công nhập kho*/
        private void TheHienNhanHangDenTheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select a.ID,a.IDTrienKhai,
                    a.NgayGiao,SoGiao,a.ToThucHien,
                    NguoiGhiGiao,a.ToNhan,DienGiai,
                    a.IDChiTietDonHang,a.IDTrienKhai,
					NgayGhiGiao,b.MaDonHang,b.MaSanPham,
					b.TenSanPham,b.MaChiTiet,b.TenChiTiet,b.SoChiTiet,b.TenCongDoan,
					a.IDChiTietDonHang,b.MaPo,b.DonViChiTiet,b.DonViSanPham
					from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet a
					inner join TrienKhaiKeHoachSanXuat b
					on a.IDTrienKhai=b.IDTrienKhai where a.NgayGiao 
					between  '{0}' and '{1}' order by ID desc",
                    dpTu.Value.ToString("yyyy-MM-dd"),
                    dpDen.Value.ToString("yyyy-MM-dd"));
            grSoChiTietNhanHangDen.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvSoChiTietNhanHangDen.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        //Tra cứu theo idtrienkhai
        private void TheHienNhanHangDenTheoDonHang()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select a.ID,a.IDTrienKhai,
                    a.NgayGiao,SoGiao,a.ToThucHien,
                    NguoiGhiGiao,a.ToNhan,DienGiai,
                    a.IDChiTietDonHang,a.IDTrienKhai,
					NgayGhiGiao,b.MaDonHang,b.MaSanPham,
					b.TenSanPham,b.MaChiTiet,b.TenChiTiet,b.SoChiTiet,b.TenCongDoan,
					a.IDChiTietDonHang,b.MaPo,b.DonViChiTiet,b.DonViSanPham
					from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet a
					inner join TrienKhaiKeHoachSanXuat b
					on a.IDTrienKhai=b.IDTrienKhai 
                    where a.IDTrienKhai like '{0}'", idtrienkhai);
            grSoChiTietNhanHangDen.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvSoChiTietNhanHangDen.OptionsSelection.CheckBoxSelectorColumnWidth = 20;

        }
        private void btnTraCuuNhanHangDen_Click(object sender, EventArgs e)
        {
            TheHienNhanHangDenTheoNgay();
        }

        private void btnTraCuuXuatHangDenGiaCong_Click(object sender, EventArgs e)
        {
            TheHienTraCuuXuatHangDenGiaCong();
        }
        private void TheHienTraCuuXuatHangDenGiaCong()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"SELECT *
                FROM dbo.TrienKhaiKeHoachSanXuatXuatGiaCong
                where NgayXuat between '{0}' and '{1}' 
                and Xoa is null order by ID desc",
                    dpTu.Value.ToString("yyyy-MM-dd"),
                    dpDen.Value.ToString("yyyy-MM-dd"));
            grXuatHangDenGiaCong.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvXuatHangDenGiaCong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void TheHienTraCuuXuatHangDenGiaCongTheoMaXuat()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"SELECT *
                FROM dbo.TrienKhaiKeHoachSanXuatXuatGiaCong
                where MaSo like '{0}' order by ID desc", txtMaXuatHang.Text);
            grXuatHangDenGiaCong.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvXuatHangDenGiaCong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void btnGhiXuatHang_Click(object sender, EventArgs e)
        {
            if (txtMaXuatHang.Text == "") { MessageBox.Show("Mã nhập ko được trống", "Message"); return; }
            else
            {
                int[] listRowList = gvSoChiTietNhanHangDen.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvSoChiTietNhanHangDen.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into TrienKhaiKeHoachSanXuatXuatGiaCong 
                    (IDTrienKhai,IDNhapHangDen,
                    MaSo,NgayXuat,SoXuat,MaDonHang,
                    MaPo,MaSanPham,TenSanPham,
                    MaChiTiet,TenChiTiet,SoChiTiet,
                    ToNhan,DonViGiaCong,DiaChi,
                    NguoiLap,DonViChiTiet,DonViSanPham,NgayGhi)
                    values('{0}','{1}',
                    N'{2}','{3}','{4}',N'{5}',
                    N'{6}',N'{7}',N'{8}',
                    N'{9}',N'{10}','{11}',
                    N'{12}',N'{13}',N'{14}',
                    N'{15}',N'{16}',N'{17}',GetDate())",
                        rowData["IDTrienKhai"], rowData["ID"],
                        txtMaXuatHang.Text, dpNgayXuatHang.Value.ToString("yyyy-MM-dd"),
                        "", rowData["MaDonHang"],
                        rowData["MaPo"], rowData["MaSanPham"], rowData["TenSanPham"],
                        rowData["MaChiTiet"], rowData["TenChiTiet"], rowData["SoChiTiet"],
                        "", "", "", Login.Username, rowData["DonViChiTiet"], rowData["DonViSanPham"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                TheHienTraCuuXuatHangDenGiaCongTheoMaXuat();
            }
        }

        private void btnSuaXuatHang_Click(object sender, EventArgs e)
        {
            int[] listRowList = gvXuatHangDenGiaCong.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvXuatHangDenGiaCong.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuatXuatGiaCong 
                   set SoXuat='{0}',ToNhan=N'{1}',DonViGiaCong=N'{2}',DiaChi=N'{3}',
                    NguoiLap=N'{4}',TrongLuongXuat='{5}',TyLe='{6}',NgayGhi=GetDate() where ID like '{7}'",
                    rowData["SoXuat"], rowData["ToNhan"],
                    rowData["DonViGiaCong"], rowData["DiaChi"],
                    Login.Username, rowData["TrongLuongXuat"], rowData["TyLe"], rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            TheHienTraCuuXuatHangDenGiaCongTheoMaXuat();
        }
        private void btnXoaXuatHang_Click(object sender, EventArgs e)
        {
            int[] listRowList = gvXuatHangDenGiaCong.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvXuatHangDenGiaCong.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuatXuatGiaCong 
                   set SoXuat='{0}',Xoa='1',NguoiLap='{1}',NgayGhi=GetDate() where ID like '{2}'",
                    "", Login.Username, rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            TheHienTraCuuXuatHangDenGiaCongTheoMaXuat();
        }

        private void TheHienBoPhanGiaCongNoiBo()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ToThucHien from tblResources order by ResourceID ASC");
            repositoryItemGridLookUpEditBPGiaCong.DataSource = kn.laybang(sqlQuery);
            repositoryItemGridLookUpEditBPGiaCong.DisplayMember = "ToThucHien";
            repositoryItemGridLookUpEditBPGiaCong.ValueMember = "ToThucHien";
            kn.dongketnoi();
        }
        private void TheHienDonViGiaCongNgoai()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select TenDVGC DonViGiaCong,DiaChi from tblDS_GIACONG");
            repositoryItemGridLookUpEditDonViGiaCong.DataSource = kn.laybang(sqlQuery);
            repositoryItemGridLookUpEditDonViGiaCong.DisplayMember = "DonViGiaCong";
            repositoryItemGridLookUpEditDonViGiaCong.ValueMember = "DonViGiaCong";
            kn.dongketnoi();
        }
        private void gvTongHopCongDoanSanXuat_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            btnReportBanVe.Visible = false;
            btnBanVe.Visible = true;
            btnPhuKien1.Visible = false;
            btnPhuKien.Visible = true;
            string point;
            point = gvTongHopCongDoanSanXuat.GetFocusedDisplayText();
            mabanve = gvTongHopCongDoanSanXuat.GetFocusedRowCellDisplayText(masanpham_grid);
            maphukien = gvTongHopCongDoanSanXuat.GetFocusedRowCellDisplayText(machitiet_);
            txtMaSanPham.Text = mabanve;
            txtPhuKien.Text = maphukien;
        }
        private void gvXuatHangDenGiaCong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            SANXUATDbContext db = new SANXUATDbContext();
            gvXuatHangDenGiaCong.SelectAll();//Khi thêm một công đoạn mới sẽ check tất cả các công đoạn cũ
            if (e.Column.FieldName == "DonViGiaCong")
            {
                var value = gvXuatHangDenGiaCong.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblDS_GIACONG.FirstOrDefault(x => x.TenDVGC == (string)value);
                if (dt != null)
                {
                    gvXuatHangDenGiaCong.SetRowCellValue(e.RowHandle, "DiaChi", dt.DiaChi);
                }
            }
        }
        private void TaoMaXuatHang()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select Top 1 
				'XK-'+REPLACE(convert(nvarchar,GetDate(),11),'/','') 
				+replace(replace(left(CONVERT(time, GetDate()),12),':',''),'.','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaXuatHang.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void btnTaoMaXuatGiao_Click(object sender, EventArgs e)
        {
            TaoMaXuatHang();
        }

        private void btnGroupSoLuongNhanHangDen_Click(object sender, EventArgs e)
        {

        }

        private void btnTraCuuGiaCongNhapKho_Click(object sender, EventArgs e)
        {
            TheHienDanhMucNhapKhoTheoNgay();
        }
        private void TheHienDanhMucNhapKhoTheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from TrienKhaiKeHoachSanXuatGiaCongNhap 
				                                where NgayNhap between '{0}' and '{1}' and Xoa is null
                                                order by ID desc",
                                                dpTu.Value.ToString("yyyy-MM-dd"),
                                                dpDen.Value.ToString("yyyy-MM-dd"));
            grNhapKho.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvNhapKho.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void GhiSoLuongGiaCongNhapKho()
        {
            if (txtMaNhap.Text == "") { MessageBox.Show("Mã nhập ko được trống", "Message"); return; }
            else
            {
                int[] listRowList = gvXuatHangDenGiaCong.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvXuatHangDenGiaCong.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into TrienKhaiKeHoachSanXuatGiaCongNhap
                    (IDTrienKhai,IDNhapHangDen,IDNhapXuatHang,
                    MaNhap,NgayNhap,SoNhapKho,MaDonHang,
                    MaPo,MaSanPham,TenSanPham,
                    MaChiTiet,TenChiTiet,
                    SoChiTiet,ToNhan,DonViGiaCong,
                    DiaChi,NguoiLap,DonViChiTiet,DonViSanPham,NgayGhi)
                    values ('{0}','{1}','{2}',
                    N'{3}','{4}','{5}',N'{6}',
                    N'{7}',N'{8}',N'{9}',
                    N'{10}',N'{11}',
                    '{12}',N'{13}',N'{14}',
                    N'{15}',N'{16}',N'{17}',N'{18}',GetDate())",
                    rowData["IDTrienKhai"], rowData["IDNhapHangDen"], rowData["ID"],
                    txtMaNhap.Text, dpNgayNhap.Value.ToString("yyyy-MM-dd"), "", rowData["MaDonHang"],
                    rowData["MaPo"], rowData["MaSanPham"], rowData["TenSanPham"],
                    rowData["MaChiTiet"], rowData["TenChiTiet"],
                    rowData["SoChiTiet"], rowData["ToNhan"], rowData["DonViGiaCong"],
                    rowData["DiaChi"], Login.Username, rowData["DonViChiTiet"], rowData["DonViSanPham"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                TheHienDanhMucNhapKhoTheoNgay();
                con.Close();
            }
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            GhiSoLuongGiaCongNhapKho();
        }

        private void btnCapNhatSoNhap_Click(object sender, EventArgs e)
        {
            int[] listRowList = gvNhapKho.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvNhapKho.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuatGiaCongNhap
                    set SoNhapKho='{0}',TrongLuongNhap='{1}',TyLeNhap='{2}',
                    NguoiLap='{3}',
                    NgayGhi=GetDate() where ID like '{4}'",
                    rowData["SoNhapKho"],
                    rowData["TrongLuongNhap"], rowData["TyLeNhap"],
                    Login.Username,
                    rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            TheHienDanhMucNhapKhoTheoNgay();
            con.Close();

        }

        private void btnXoaSoNhap_Click(object sender, EventArgs e)
        {
            int[] listRowList = gvNhapKho.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvNhapKho.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuatGiaCongNhap
                    set SoNhapKho='{0}',
                    NguoiLap='{1}',Xoa='1',
                    NgayGhi=GetDate() 
                    where ID like '{2}'", "",
                    Login.Username,
                    rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            TheHienDanhMucNhapKhoTheoNgay();
            con.Close();
        }

        private void btnTaoMaNhap_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select Top 1 
				'NK-'+REPLACE(convert(nvarchar,GetDate(),11),'/','') 
				+replace(replace(left(CONVERT(time, GetDate()),12),':',''),'.','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaNhap.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void btnPhieuXuatKhoGiaCong_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from TrienKhaiKeHoachSanXuatXuatGiaCong where MaSo like N'" + maxuathang + "' and Xoa is null");
            Rpxuatkho_giacong rpxuatgiacong = new Rpxuatkho_giacong();
            rpxuatgiacong.DataSource = dt;
            rpxuatgiacong.DataMember = "Table";
            rpxuatgiacong.CreateDocument(false);
            rpxuatgiacong.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = madonhang;
            PrintTool tool = new PrintTool(rpxuatgiacong.PrintingSystem);
            rpxuatgiacong.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnReportPhieuNhap_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from TrienKhaiKeHoachSanXuatGiaCongNhap where MaNhap like N'" + manhaphang + "' Xoa is null");
            Rpxuatkho_giacong rplich = new Rpxuatkho_giacong();
            rplich.DataSource = dt;
            rplich.DataMember = "Table";
            rplich.CreateDocument(false);
            rplich.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = madonhang;
            PrintTool tool = new PrintTool(rplich.PrintingSystem);
            rplich.ShowPreviewDialog();
            kn.dongketnoi();
        }
        private string manhaphang;
        private void grNhapKho_Click(object sender, EventArgs e)
        {
            string point;
            point = gvNhapKho.GetFocusedDisplayText();
            manhaphang = gvNhapKho.GetFocusedRowCellDisplayText(manhapkho_colNhap);
        }
        private string maxuathang;
        private void grXuatHangDenGiaCong_Click(object sender, EventArgs e)
        {
            string point;
            point = gvXuatHangDenGiaCong.GetFocusedDisplayText();
            maxuathang = gvXuatHangDenGiaCong.GetFocusedRowCellDisplayText(MaSo_col);
        }
        //sự kiện enable nút ghi dữ liệu xuất kho hàng đên gia công
        private void grSoChiTietNhanHangDen_MouseMove(object sender, MouseEventArgs e)
        {
            if (gvSoChiTietNhanHangDen.SelectedRowsCount > 0)
            {
                btnGhiXuatHang.Enabled = true;
            }
            else { btnGhiXuatHang.Enabled = false; }
        }
        //sự kiện enable nút ghi dữ liệu nhập kho hàng đên gia công - cập nhật số lượng nhập xóa số lượng nhập
        private void grXuatHangDenGiaCong_MouseMove(object sender, MouseEventArgs e)
        {
            if (gvXuatHangDenGiaCong.SelectedRowsCount > 0)
            {
                btnCapNhatXuatHang.Enabled = true;
                btnXoaXuatHang.Enabled = true;
                btnNhapKho.Enabled = true;
            }
            else { btnCapNhatXuatHang.Enabled = false; btnXoaXuatHang.Enabled = false; btnNhapKho.Enabled = false; }
        }
        //Sự kiện enable nút cập nhật số lượng nhập kho xóa số lượng nhập kho
        private void grNhapKho_MouseMove(object sender, MouseEventArgs e)
        {
            if (gvNhapKho.SelectedRowsCount > 0)
            {
                btnCapNhatSoNhap.Enabled = true; btnXoaSoNhap.Enabled = true;
            }
            else { btnCapNhatSoNhap.Enabled = false; btnXoaSoNhap.Enabled = false; }
        }

        private void btnDonViGiaCong_Click(object sender, EventArgs e)
        {
            frmDV_giacong donvigiacong = new frmDV_giacong();
            donvigiacong.ShowDialog();
            TheHienDonViGiaCongNgoai();
        }

        private void btnExportGiaoNhanSanXuat_Click(object sender, EventArgs e)
        {
            treeListProductionStagesPlan.ShowPrintPreview();
        }

        private void btnCongTheoDonHang_Click(object sender, EventArgs e)
        {
            btnTaoToaGiaoHang.Enabled = false;
            ckCheckListTongHop.Checked = false;
            TheHienSoLuongDonHangTheoNgay();
            this.gvTongHopCongDoanSanXuat.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            gvTongHopCongDoanSanXuat.Columns["SoDaSanXuat"].Visible = false;
        }
        private void TheHienSoLuongDonHangTheoTo()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select c.* from
            (select b.SoLuongGiao,b.NgayGiao,
             case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
              when cast(GetDate() as date)<=KetThuc and (isnull(b.SoLuongGiao,0) < 1 or b.SoLuongGiao is null) then N'Chưa khởi động'
              when cast(GetDate() as date)> KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
              when cast(GetDate() as date)<= KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
            end TinhTrang,a.* from
            (select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
               MaDonHang,MaPo,MaSanPham,
               TenSanPham,TenLoai,SoLuongDonHang,
               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
               SoLuongYCSanXuat,''SoDaSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
               TenCongDoan,SoThuTu,
               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu,
               NguyenNhan,DeXuatBienPhapKhacPhuc,
               NgayLapBaoCao,NguoiGhiBaoCao,NoiDungBaoCao
               from TrienKhaiKeHoachSanXuat)a left outer join
            (select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai>0
			    group by IDTrienKhai
						 union all 
						 select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)b
            on a.IDTrienKhai=b.IDTrienKhai)c
            where (ToThucHien in (select WorkLocation from tblResourcesUser where UserName like '{2}') 
            and TinhTrang like N'Quá hạn' 
            and BatDau <> '' and KetThuc <> '' 
            and ToThucHien<>'') or 
            (NgayLap between '{0}' and '{1}'
            and ToThucHien in (select WorkLocation from tblResourcesUser where UserName like '{2}')
            and BatDau <> '' 
            and KetThuc <> ''
            and ToThucHien<>'')
            order by NgayLap desc",
            dpTu.Value.ToString("yyyy-MM-dd"),
            dpDen.Value.ToString("yyyy-MM-dd"),
            ClassUser.User);
            grTongHopCongDoanSanXuat.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();
        }
        private async void TheHienSoLuongDonHangTheoNgay()
        {   
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string param;
                if (Login.role == "1" || Login.role == "1039")
                { param = "%"; }
                else { param = Login.Username; }

                string sqlQuery = string.Format(@"execute TraCuuThongKeTrienKhaiKeHoach '{0}','{1}',N'{2}'",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"), param);
                Invoke((Action)(() => {
                    grTongHopCongDoanSanXuat.DataSource = Function.GetDataTable(sqlQuery);
                    Function.Disconnect();
                    this.gvTongHopCongDoanSanXuat.OptionsSelection.MultiSelectMode
                  = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
                }));
            });
        }

        private void btnExportSoLuongGiao_Click(object sender, EventArgs e)
        {
            gvTongHopCongDoanSanXuat.ShowPrintPreview();
        }

        private void ckCheckListTongHop_CheckedChanged(object sender, EventArgs e)
        {
            if (ckCheckListTongHop.Checked == true)
            {
                btnTaoToaGiaoHang.Enabled = true;
                this.gvTongHopCongDoanSanXuat.OptionsSelection.MultiSelectMode
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                gvTongHopCongDoanSanXuat.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
                gvTongHopCongDoanSanXuat.Columns["SoDaSanXuat"].Visible = true;
                TheHienSoLuongDonHangTheoTo();
            }
            else
            {
                btnTaoToaGiaoHang.Enabled = false;
                this.gvTongHopCongDoanSanXuat.OptionsSelection.MultiSelectMode
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
                gvTongHopCongDoanSanXuat.Columns["SoDaSanXuat"].Visible = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
        string mabanve;
        string maphukien;

        private void grTongHopCongDoanSanXuat_Click(object sender, EventArgs e)
        {
            btnReportBanVe.Visible = false;
            btnBanVe.Visible = true;
            btnPhuKien1.Visible = false;
            btnPhuKien.Visible = true;
            string point;
            point = gvTongHopCongDoanSanXuat.GetFocusedDisplayText();
            mabanve = gvTongHopCongDoanSanXuat.GetFocusedRowCellDisplayText(masanpham_grid);
            maphukien = gvTongHopCongDoanSanXuat.GetFocusedRowCellDisplayText(machitiet_);
            txtMaSanPham.Text = mabanve;
            txtPhuKien.Text = maphukien;
        }
        //Quyền được tạo toa giao
        private void THQuyenTaoToaGiao()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select * from tblResourcesUser 
                where WorkLocation in ('{0}')", tothuchien);
            var kq = Function.GetDataTable(sqlQuery);
            if (kq.Rows.Count > 0)
            {
                btnTaoToaGiaoHang.Visible = true;
            }
            else
            {
                btnTaoToaGiaoHang.Visible = false;
            }
        }
        private void btnBanVe_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(txtMaSanPham.Text, path.pathbanve);
            f2.Show();
        }

        private void btnQuyTrinhCongNghe_Click(object sender, EventArgs e)
        {
            string maQuyTrinh = "QTSX-" + masanpham;
            Path path = new Path();
            string pat = string.Format(@"{0}\{1}.PDF", path.pathquytrinh, maQuyTrinh);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Vui lòng liên hệ bộ phận kỹ thuật", "Thông báo"); }
        }

        private void btnQuyTrinh_Click(object sender, EventArgs e)
        {
            string maQuyTrinh = "QTSX-" + mabanve;
            Path path = new Path();
            string pat = string.Format(@"{0}\{1}.PDF", path.pathquytrinh, maQuyTrinh);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Vui lòng liên hệ bộ phận kỹ thuật", "Thông báo"); }
        }

        private void btnPhuKien1_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(txtPhuKien.Text, path.pathbanve);
            f2.Show();
        }

        private void btnPhuKien_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(txtPhuKien.Text, path.pathbanve);
            f2.Show();
        }

        private void btnDonHangConLai_Click(object sender, EventArgs e)
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select c.* from
            (select b.SoLuongGiao,b.NgayGiao,
            case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
                 when GetDate()<BatDau and isnull(b.SoLuongGiao,0) < 1 then N'Chưa khởi động'
                 when cast(GetDate() as date)>= KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
                 when GetDate()<KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
            end TinhTrang,a.* from
            (select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
               MaDonHang,MaPo,MaSanPham,
               TenSanPham,TenLoai,SoLuongDonHang,
               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
               SoLuongYCSanXuat,''SoDaSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
               TenCongDoan,SoThuTu,
               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu
               from TrienKhaiKeHoachSanXuat)a left outer join
            (select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai>0
			    group by IDTrienKhai
						 union all 
						 select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)b
            on a.IDTrienKhai=b.IDTrienKhai)c
            where (MaCongDoan like 'GHA' 
            and TinhTrang like N'Quá hạn' 
            and BatDau <> '' and KetThuc <> '') or 
            (NgayLap between '{0}' and '{1}'
			and MaCongDoan like 'GHA'
            and BatDau <> '' 
            and KetThuc <> '')
            order by NgayLap desc",
            dpTu.Value.ToString("yyyy-MM-dd"),
            dpDen.Value.ToString("yyyy-MM-dd"));
            grDonHangConLai.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();//Mo ket noi
            xtraTabControlDonHangConLai.SelectedTabPage = xtraTabPageDonHangConLai;
        }

        private void btnXuatKhoGiao_Click(object sender, EventArgs e)
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select c.* from
            (select b.SoLuongGiao,b.NgayGiao,
            case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
                 when GetDate()<BatDau and isnull(b.SoLuongGiao,0) < 1 then N'Chưa khởi động'
                 when cast(GetDate() as date)>= KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
                 when GetDate()<KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
            end TinhTrang,a.* from
            (select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
               MaDonHang,MaPo,MaSanPham,
               TenSanPham,TenLoai,SoLuongDonHang,
               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
               SoLuongYCSanXuat,''SoDaSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
               TenCongDoan,SoThuTu,
               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu
               from TrienKhaiKeHoachSanXuat)a left outer join
            (select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai>0
			    group by IDTrienKhai
						 union all 
						 select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)b
            on a.IDTrienKhai=b.IDTrienKhai)c
            where (MaCongDoan like 'GHA' 
            and TinhTrang like N'Hoàn thành' 
            and BatDau <> '' and KetThuc <> '') or 
            (NgayLap between '{0}' and '{1}'
			and MaCongDoan like 'GHA'
            and BatDau <> '' 
            and KetThuc <> '')
            order by NgayLap desc",
            dpTu.Value.ToString("yyyy-MM-dd"),
            dpDen.Value.ToString("yyyy-MM-dd"));
            grDonHangConLai.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();//Mo ket noi
            xtraTabControlDonHangConLai.SelectedTabPage = xtraTabPageDonHangConLai;
        }

        private void btnTraCuuChiTietXuatKho_Click(object sender, EventArgs e)
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select mabv+'; '+sanpham+'; '+chitietsanpham Nhom,
             MaGH+'-'+cast(SoChungTu_XK as nvarchar) MaPhieu,ngaynhan,madh,MaPo,mabv,sanpham,
			 chitietsanpham,soluongyc,BTPT11,TRONGLUONG11,ngoaiquang,TongCongBaoBi,Num ID,IDTrienKhai,IdPSX
			 from tbl11 where IDTrienKhai is not null and ngaynhan between '{0}' and '{1}'
			 order by ngaynhan desc",
             dpTu.Value.ToString("yyyy-MM-dd"),
             dpDen.Value.ToString("yyyy-MM-dd"));
            grChiTietGiaoHang.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();//Mo ket noi
            xtraTabControlDonHangConLai.SelectedTabPage = xtraTabPageChiTietGiaoHang;
            gvChiTietGiaoHang.Columns["Nhom"].GroupIndex = 0;
            gvChiTietGiaoHang.ExpandAllGroups();
        }
        string idtrienkhaiconlai;
        private void grDonHangConLai_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gvDonHangConLai.GetFocusedDisplayText();
            idtrienkhaiconlai = gvDonHangConLai.GetFocusedRowCellDisplayText(idtrienkhai_);
            TheHienChiTietXuatKho();
        }
        private void TheHienChiTietXuatKho()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select MaGH+'-'+cast(SoChungTu_XK as nvarchar) MaPhieu,
             ngaynhan,madh,MaPo,mabv,sanpham,
			 chitietsanpham,soluongyc,BTPT11,TRONGLUONG11,ngoaiquang,TongCongBaoBi,Num ID,IDTrienKhai,IdPSX
			 from tbl11 where IDTrienKhai is not null and
             IDTrienKhai like '{0}'
			 order by ngaynhan desc",
             idtrienkhaiconlai);
            grChiTietGiaoHang.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();//Mo ket noi
        }

        private void btnLenhSanXuatDaKyCamKet_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(madonhang, path.pathkinhdoanh);
            f2.Show();
        }

        private void btnTaoToaGiaoHang_Click(object sender, EventArgs e)
        {
            GhiDuLieuGiaoHang();
        }
        private void GhiDuLieuGiaoHang()
        {
            CreateCodePointSave();//Tao điểm giao hàng cho từng đợt
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvTongHopCongDoanSanXuat.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvTongHopCongDoanSanXuat.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
                    (PointSave,IDTrienKhai,
                    IDChiTietDonHang,NgayGiao,
                    SoGiao,NguoiGhiGiao,
                    NgayGhiGiao,ToThucHien)
                    values 
                   (N'{0}',N'{1}',
                    N'{2}',N'{3}',
                    N'{4}',N'{5}',
                    GetDate(),N'{6}')",
                    pointsave, rowData["IDTrienKhai"],
                    rowData["IDChiTietDonHang"],
                    dpNgayHoanThanh.Value.ToString("yyyy-MM-dd"),
                    rowData["SoDaSanXuat"], Login.Username, rowData["ToThucHien"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
            TrienKhaiGiaoNhanChiTiet giaoHang = new TrienKhaiGiaoNhanChiTiet(pointsave);
            giaoHang.ShowDialog();
            TheHienSoLuongDonHangTheoTo();
        }
  

        private void btnTHSoGiaoHang_Click(object sender, EventArgs e)
        {
            TrienKhaiGiaoNhanChiTiet giaoHang = new TrienKhaiGiaoNhanChiTiet("");
            giaoHang.ShowDialog();
            TheHienSoLuongDonHangTheoTo();
        }

        private void btnTraTheoToSanXuat_Click(object sender, EventArgs e)
        {
            ThQuyenGhiToaGiao();
        }
        private void ThQuyenGhiToaGiao()
        {
            TheHienSoLuongDonHangTheoTo();
            //btnTaoToaGiaoHang.Enabled = true;
            //this.gvTongHopCongDoanSanXuat.OptionsSelection.MultiSelectMode
            //= DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            //gvTongHopCongDoanSanXuat.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            //gvTongHopCongDoanSanXuat.Columns["SoDaSanXuat"].Visible = true;
            //btnTaoToaGiaoHang.Visible = true;
            //ckCheckListTongHop.Visible = true;
        }
        private void btnUngroupChiTietXuatKho_Click(object sender, EventArgs e)
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select mabv+'; '+sanpham+'; '+chitietsanpham Nhom,
             MaGH+'-'+cast(SoChungTu_XK as nvarchar) MaPhieu,ngaynhan,madh,MaPo,mabv,sanpham,
			 chitietsanpham,soluongyc,BTPT11,TRONGLUONG11,ngoaiquang,TongCongBaoBi,Num ID,IDTrienKhai,IdPSX
			 from tbl11 where IDTrienKhai is not null and ngaynhan between '{0}' and '{1}'
			 order by ngaynhan desc",
             dpTu.Value.ToString("yyyy-MM-dd"),
             dpDen.Value.ToString("yyyy-MM-dd"));
            grChiTietGiaoHang.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();//Mo ket noi
            xtraTabControlDonHangConLai.SelectedTabPage = xtraTabPageChiTietGiaoHang;
            gvChiTietGiaoHang.Columns["Nhom"].GroupIndex = -1;
        }

        private void btnExportDonHangCon_Click(object sender, EventArgs e)
        {
            grDonHangConLai.ShowPrintPreview();
        }

        private void btnExportChiTietXuat_Click(object sender, EventArgs e)
        {
            grChiTietGiaoHang.ShowPrintPreview();
        }

        private void grSoChiTietNhanHangDen_Click(object sender, EventArgs e)
        {

        }
        //Cập nhật báo cáo tuần theo kế hoạch sản xuất
        private void btnCapNhatBaoCaoTuan_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridViewBaoCaoTuan.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridViewBaoCaoTuan.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuat 
                        set NguyenNhan = N'{0}',DeXuatBienPhapKhacPhuc = N'{1}',
                        NgayGhiBaoCao = GetDate(),NguoiGhiBaoCao = N'{2}',NoiDungBaoCao = N'{3}',NgayLapBaoCao = '{4}'
                        where IDTrienKhai like '{5}'",
                        rowData["NguyenNhan"],rowData["DeXuatBienPhapKhacPhuc"],
                        MainDev.username, rowData["NoiDungBaoCao"],
                        rowData["NgayLapBaoCao"].ToString()==""?
                        DateTime.Now.ToString("MM-dd-yyyy"):Convert.ToDateTime((rowData["NgayLapBaoCao"])).ToString("MM-dd-yyyy"),
                        rowData["IDTrienKhai"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                THThongKeBaoCao();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnTraCuuThongKeBaoCao_Click(object sender, EventArgs e)
        {
            THThongKeBaoCao();
        }
        private void THThongKeBaoCao()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select c.* from
            (select b.SoLuongGiao,b.NgayGiao,
             case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
              when cast(GetDate() as date)<=KetThuc and (isnull(b.SoLuongGiao,0) < 1 or b.SoLuongGiao is null) then N'Chưa khởi động'
              when cast(GetDate() as date)> KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
              when cast(GetDate() as date)<= KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
            end TinhTrang,a.* from
            (select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
               MaDonHang,MaPo,MaSanPham,
               TenSanPham,TenLoai,SoLuongDonHang,
               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
               SoLuongYCSanXuat,''SoDaSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
               TenCongDoan,SoThuTu,
               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu,
               NguyenNhan,DeXuatBienPhapKhacPhuc,
               NgayLapBaoCao,NguoiGhiBaoCao,NoiDungBaoCao
               from TrienKhaiKeHoachSanXuat)a left outer join
            (select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai>0
			    group by IDTrienKhai
						 union all 
						 select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)b
            on a.IDTrienKhai=b.IDTrienKhai)c
            where (ToThucHien in (select WorkLocation from tblResourcesUser where UserName like '{2}') 
            and TinhTrang like N'Quá hạn' 
            and BatDau <> '' and KetThuc <> '' 
            and ToThucHien<>'') or 
            (NgayLap between '{0}' and '{1}'
            and ToThucHien in (select WorkLocation from tblResourcesUser where UserName like '{2}')
            and BatDau <> '' 
            and KetThuc <> ''
            and ToThucHien<>'')
            order by NgayLap desc",
            dpTu.Value.ToString("yyyy-MM-dd"),
            dpDen.Value.ToString("yyyy-MM-dd"),
            ClassUser.User);
            gridControlBaoCaoTuan.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();
            gridViewBaoCaoTuan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
    }
}
