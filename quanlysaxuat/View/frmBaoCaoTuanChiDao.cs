using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class frmBaoCaoTuanChiDao : DevExpress.XtraEditors.XtraForm
    {
        public frmBaoCaoTuanChiDao()
        {
            InitializeComponent();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }
   
        private async void ThNoiDungTienDo()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}' 
				and IDNoiDung like '{1}'",reportCode, lbIDTienDo.Text);
                Invoke((Action)(() =>
                {
                    gridControlTienDo.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private async void ThNoiDungChatLuong()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}' 
				and IDNoiDung like '{1}'", reportCode, lbIDChatLuong.Text);
                Invoke((Action)(() =>
                {
                    gridControlChatLuong.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }  
        private async void ThNoiDungCao5S()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}' 
				and IDNoiDung like '{1}'", reportCode, lbIDTieuChuan5S.Text);
                Invoke((Action)(() =>
                {
                    gridControlThucHanh5S.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private async void ThNoiDungKaiZen()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}' 
				and IDNoiDung like '{1}'", reportCode, lbIDCaiTien.Text);
                Invoke((Action)(() =>
                {
                    gridControlCaiTien.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private async void ThNoiDungViPhamChiPhi()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}' 
				and IDNoiDung like '{1}'", reportCode, lbIDViPhamTangChiPhi.Text);
                Invoke((Action)(() =>
                {
                    gridControlViPhamTangChiPhi.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {

        }
        //formload
        private void frmBaoCaoTuanChiDao_Load(object sender, EventArgs e)
        {
            dpTongHopMin.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpTongHopMin.Text = DateTime.Now.ToString("dd-MM-yyyy");
            TraCuuSoBaoCaoTuan();
            ThChiTieuTienDo();ThChiTieuChatLuong();ThChiTieuCaiTien();ThChiTieu5S();ThChiTieuViPhamTangChiPhi();
            ThDanhGiaChiTieuTienDo();ThDanhGiaChiTieuChatLuong();ThDanhGiaChiTieuCaiTien();ThDanhGiaChiTieu5S();ThDanhGiaChiTieuViPhamTangChiPhi();
            this.gvBaoCaoToChiTietSo.Appearance.Row.Font = new Font("Tahoma", 8f);
        }

        private void btnTraCuuBaoCaoTuan_Click(object sender, EventArgs e)
        {
            TraCuuSoBaoCaoTuan();
        }
        private async void TraCuuSoBaoCaoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"
                    select *,(DiemTienDo+DiemChatLuong+DiemKaizen+DiemAnToan5S+DiemNoiQuy)TongDiem
                    from (select ID,CodeReport,
				                ToBaoCao,Tuan,Nam,Thang,DateMin,DateMax,NoiDung,ChiSo,NguoiBaoCao,
				                NgayGhi,NgayHieuChinh,QHTrongVongKiemSoat,QHVuotVongKiemSoat,
				                ChuanBiThucHien,DangThucHien,RuiRoTiemAn,HoanThanh,
                    TienDoDTB*TienDoTrongSo/5 DiemTienDo,
                    ChatLuongDTB*ChatLuongTrongSo/5 DiemChatLuong,
                    KaizenDTB*KaiZenTrongSo/5 DiemKaizen,
                    AnToan5SDTB*Method5STrongSo/5 DiemAnToan5S,
                    NoiQuyDTB*ChiPhiNoiQuyTrongSo/5 DiemNoiQuy from
                    (select ID,CodeReport,
				     ToBaoCao,Tuan,Nam,Thang,DateMin,DateMax,NoiDung,ChiSo,NguoiBaoCao,
				     NgayGhi,NgayHieuChinh,QHTrongVongKiemSoat,QHVuotVongKiemSoat,
				     ChuanBiThucHien,DangThucHien,RuiRoTiemAn,HoanThanh,
                    (TienDoDiemCongSuDG*2+TienDoDiemTuDG)/3 TienDoDTB,
                    (ChatLuongDiemCongSuDG*2+ChatLuongDiemTuDG)/3 ChatLuongDTB,
                    (KaiZenDiemCongSuDG*2+KaiZenDiemTuDG)/3 KaizenDTB,
                    (Method5SDiemCongSuDG*2+Method5SDiemTuDG)/3 AnToan5SDTB,
                    (ChiPhiNoiQuyDiemCongSuDG*2+ChiPhiNoiQuyDiemTuDG)/3 NoiQuyDTB,
                    TienDoTrongSo,
                    ChatLuongTrongSo,
                    KaiZenTrongSo,
                    Method5STrongSo,
                    ChiPhiNoiQuyTrongSo
                    from BaoCaoToNoiDung)a)b");
                Invoke((Action)(() =>
                {
                    grBaoCaoToChiTietSo.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        //Load data evaluation criteria to gridlookupEdit - chỉ tiêu đánh giá - Tiến độ - Chất lượng - Thực hành 5s - Kaizen - Vi phạm & Tăng chí phí
        private async void ThChiTieuTienDo()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Tiến độ'");
                Invoke((Action)(() =>
                {
                    gridLookUpEditTuDGTienDo.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEditTuDGTienDo.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEditTuDGTienDo.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }
        private async void ThChiTieuChatLuong()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Chất lượng'");
                Invoke((Action)(() =>
                {
                    gridLookUpEditTuDGChatLuong.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEditTuDGChatLuong.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEditTuDGChatLuong.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }        
        private async void ThChiTieuCaiTien()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Đề xuất cải tiến'");
                Invoke((Action)(() =>
                {
                    gridLookUpEditTuDGCaiTien.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEditTuDGCaiTien.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEditTuDGCaiTien.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }
        private async void ThChiTieu5S()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Thực hành 5S'");
                Invoke((Action)(() =>
                {
                    gridLookUpEditTuDGThucHanh5S.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEditTuDGThucHanh5S.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEditTuDGThucHanh5S.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }  
        private async void ThChiTieuViPhamTangChiPhi()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Tăng chi phí'");
                Invoke((Action)(() =>
                {
                    gridLookUpEditTuDGViPhamTangChiPhi.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEditTuDGViPhamTangChiPhi.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEditTuDGViPhamTangChiPhi.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }
        string boPhanBaoCao; string tuan; string thang; string nam;
        //Đánh giá Load data evaluation criteria to gridlookupEdit - chỉ tiêu đánh giá - Tiến độ - Chất lượng - Thực hành 5s - Kaizen - Vi phạm & Tăng chí phí
        private async void ThDanhGiaChiTieuTienDo()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Tiến độ'");
                Invoke((Action)(() =>
                {
                    gridLookUpEditCongSuDGTienDo.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEditCongSuDGTienDo.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEditCongSuDGTienDo.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }
        private async void ThDanhGiaChiTieuChatLuong()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Chất lượng'");
                Invoke((Action)(() =>
                {
                    gridLookUpEdiCongSuDGChatLuong.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEdiCongSuDGChatLuong.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEdiCongSuDGChatLuong.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }
        private async void ThDanhGiaChiTieuCaiTien()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Đề xuất cải tiến'");
                Invoke((Action)(() =>
                {
                    gridLookUpEditCongSuDGCaiTien.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEditCongSuDGCaiTien.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEditCongSuDGCaiTien.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }
        private async void ThDanhGiaChiTieu5S()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Thực hành 5S'");
                Invoke((Action)(() =>
                {
                    gridLookUpEditCongSuDGThucHanh5S.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEditCongSuDGThucHanh5S.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEditCongSuDGThucHanh5S.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }
        private async void ThDanhGiaChiTieuViPhamTangChiPhi()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToThangDiem where NoiDung like N'Tăng chi phí'");
                Invoke((Action)(() =>
                {
                    gridLookUpEditCongSuDGVPTangChiPhi.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
                    gridLookUpEditCongSuDGVPTangChiPhi.Properties.DisplayMember = "NoiDungChoDiem";
                    gridLookUpEditCongSuDGVPTangChiPhi.Properties.ValueMember = "NoiDungChoDiem";
                }));
            });
        }
        string reportCode;
        string id;
        private void gvBaoCaoToChiTietSo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["CodeReport"]) == null)
            {
                return;
            }
            else
            {
                reportCode = gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["CodeReport"]).ToString();
                id = gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["ID"]).ToString();
            }
            ThNoiDungTienDo();
            ThNoiDungChatLuong();
            ThNoiDungCao5S();
            ThNoiDungKaiZen();
            ThNoiDungViPhamChiPhi();
        }

        private void btnCapNhatNCDanhGiaCongSu_Click(object sender, EventArgs e)
        {
            CapNhatDanhGiaCongSu();
            TraCuuSoBaoCaoTuan();
        }
        private void CapNhatDanhGiaCongSu()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"update BaoCaoToNoiDung 
				set TienDoCongTacDG=N'{0}',TienDoTieuChiCongTac=N'{1}',
				ChatLuongCongTacDG=N'{2}',ChatLuongTieuChiCongTac=N'{3}',
				KaiZenCongTacDG=N'{4}',KaiZenTieuChiCongTac=N'{5}',
				Method5SCongTacDG=N'{6}',Method5STieuChiCongTac=N'{7}',
				ChiPhiNoiQuyCongTacDG=N'{8}',ChiPhiNoiQuyTieuChiCongTac=N'{9}',
                TienDoDiemCongSuDG='{10}',
                ChatLuongDiemCongSuDG=N'{11}',
                KaiZenDiemCongSuDG=N'{12}',
                Method5SDiemCongSuDG=N'{13}',
                ChiPhiNoiQuyDiemCongSuDG=N'{14}',
				NguoiTongHop=N'{15}',NgayTongHop=GetDate() where ID like '{16}'",
                txtTienDoCongTacDG.Text, gridLookUpEditCongSuDGTienDo.Text,
                txtChatLuongCongTacDG.Text, gridLookUpEdiCongSuDGChatLuong.Text,
                txtKaiZenCongTacDG.Text, gridLookUpEditCongSuDGCaiTien.Text,
                txtMethod5SCongTacDG.Text, gridLookUpEditCongSuDGThucHanh5S,
                txtChiPhiNoiCongTacDG.Text, gridLookUpEditCongSuDGVPTangChiPhi.Text,
                txtDiemTienDoCongSuDG.Text,
                txtDiemChatLuongCongSuDG.Text,
                txtDiemKaizenCongSuDG.Text,
                txtDiem5SCongSuDG.Text,
                txtDiemViPhamCongSuDG.Text,
                MainDev.username, id);
            var kq = Model.Function.GetDataTable(sqlQuery);
            if (kq.Rows.Count > 0)
            { MessageBox.Show("Success text", "Success"); }
        }
        

        private void btnCapNhatTuDanhGia_Click(object sender, EventArgs e)
        {
            CapNhatTuDanhGia();
            TraCuuSoBaoCaoTuan();
        }
        private void CapNhatTuDanhGia()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"update BaoCaoToNoiDung 
				set TienDoNoiDungPS=N'{0}',TienDoThangDiem=N'{1}',
				ChatLuongNoiDungPS=N'{2}',ChatLuongThangDiem=N'{3}',
				KaiZenNoiDungPS=N'{4}',KaiZenThangDiem=N'{5}',
				Method5SNoiDungPS=N'{6}',Method5SThangDiem=N'{7}',
				ChiPhiNoiQuyNoiDungPS=N'{8}',ChiPhiNoiQuyThangDiem=N'{9}',
				TienDoDiemTuDG='{10}',TienDoTrongSo='{11}',
                ChatLuongDiemTuDG='{12}',ChatLuongTrongSo='{13}',
                KaiZenDiemTuDG='{14}',KaiZenTrongSo='{15}',
                Method5SDiemTuDG='{16}',Method5STrongSo='{17}',
                ChiPhiNoiQuyDiemTuDG='{18}',ChiPhiNoiQuyTrongSo='{19}',
                NguoiTongHop=N'{20}',NgayTongHop=GetDate() where ID like '{21}'",
                gridLookUpEditTuDGTienDo.Text, gridLookUpEditTuDGTienDo.Text,
                gridLookUpEditTuDGChatLuong.Text, gridLookUpEditTuDGChatLuong.Text,
                gridLookUpEditTuDGCaiTien.Text, gridLookUpEditTuDGCaiTien.Text,
                gridLookUpEditTuDGThucHanh5S.Text, gridLookUpEditTuDGThucHanh5S.Text,
                gridLookUpEditTuDGViPhamTangChiPhi.Text, gridLookUpEditTuDGViPhamTangChiPhi.Text,
                txtDiemTienDoCongSuDG.Text, lbTrongSoTienDo.Text,
                txtDiemChatLuongTuDG.Text, lbTrongSoChatLuong.Text,
                txtDiemKaiZenTuDG.Text, lbTrongSoKaizen.Text,
                txtDiemAnToan5STuDG.Text, lbTrongSoAnToan5S.Text,
                txtDiemViPhamTuDG.Text, lbTrongSoViPham.Text,
                MainDev.username, id);
            var kq = Model.Function.GetDataTable(sqlQuery);
            if (kq.Rows.Count > 0)
            { MessageBox.Show("Success text", "Success"); }
        }
        //Thể hiện điểm và trọng số tự đánh giá
        private void gridViewTienDoTuDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewTienDoTuDG.GetRowCellValue(gridViewTienDoTuDG.FocusedRowHandle, gridViewTienDoTuDG.Columns["NoiDungChoDiem"]) == null)
            {
                return;
            }
            else
            {
                txtDiemTienDoTuDG.Text = gridViewTienDoTuDG.GetRowCellValue(gridViewTienDoTuDG.FocusedRowHandle, gridViewTienDoTuDG.Columns["DiemSo"]).ToString();
                lbTrongSoTienDo.Text = gridViewTienDoTuDG.GetRowCellValue(gridViewTienDoTuDG.FocusedRowHandle, gridViewTienDoTuDG.Columns["TrongSo"]).ToString();
            }
        }

        private void gridViewChatLuongTuDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewChatLuongTuDG.GetRowCellValue(gridViewChatLuongTuDG.FocusedRowHandle, gridViewChatLuongTuDG.Columns["NoiDungChoDiem"]) == null){return;}
            else
            {
                txtDiemChatLuongTuDG.Text = gridViewChatLuongTuDG.GetRowCellValue(gridViewChatLuongTuDG.FocusedRowHandle, gridViewChatLuongTuDG.Columns["DiemSo"]).ToString();
                lbTrongSoChatLuong.Text = gridViewChatLuongTuDG.GetRowCellValue(gridViewChatLuongTuDG.FocusedRowHandle, gridViewChatLuongTuDG.Columns["TrongSo"]).ToString();
            }
        }

        private void gridViewKaiZenTuDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewKaiZenTuDG.GetRowCellValue(gridViewKaiZenTuDG.FocusedRowHandle, gridViewKaiZenTuDG.Columns["NoiDungChoDiem"]) == null) { return; }
            else
            {
                txtDiemKaiZenTuDG.Text = gridViewKaiZenTuDG.GetRowCellValue(gridViewKaiZenTuDG.FocusedRowHandle, gridViewKaiZenTuDG.Columns["DiemSo"]).ToString();
                lbTrongSoKaizen.Text = gridViewKaiZenTuDG.GetRowCellValue(gridViewKaiZenTuDG.FocusedRowHandle, gridViewKaiZenTuDG.Columns["TrongSo"]).ToString();
            }
        }
        private void gridView5STuDanhGia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView5STuDanhGia.GetRowCellValue(gridView5STuDanhGia.FocusedRowHandle, gridView5STuDanhGia.Columns["NoiDungChoDiem"]) == null) { return; }
            else
            {
                txtDiemAnToan5STuDG.Text = gridView5STuDanhGia.GetRowCellValue(gridView5STuDanhGia.FocusedRowHandle, gridView5STuDanhGia.Columns["DiemSo"]).ToString();
                lbTrongSoAnToan5S.Text = gridView5STuDanhGia.GetRowCellValue(gridView5STuDanhGia.FocusedRowHandle, gridView5STuDanhGia.Columns["TrongSo"]).ToString();
            }
        }
        private void gridViewViPhamTuDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewViPhamTuDG.GetRowCellValue(gridViewViPhamTuDG.FocusedRowHandle, gridViewViPhamTuDG.Columns["NoiDungChoDiem"]) == null) { return; }
            else
            {
                txtDiemViPhamTuDG.Text = gridViewViPhamTuDG.GetRowCellValue(gridViewViPhamTuDG.FocusedRowHandle, gridViewViPhamTuDG.Columns["DiemSo"]).ToString();
                lbTrongSoViPham.Text = gridViewViPhamTuDG.GetRowCellValue(gridViewViPhamTuDG.FocusedRowHandle, gridViewViPhamTuDG.Columns["TrongSo"]).ToString();
            }
        }
        //thể hiện điểm số do cộng sự đánh giá
        private void gridViewTienDoCongSuDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewTienDoCongSuDG.GetRowCellValue(gridViewTienDoCongSuDG.FocusedRowHandle, gridViewTienDoCongSuDG.Columns["NoiDungChoDiem"]) == null) { return; }
            else
            {
                txtDiemTienDoCongSuDG.Text = gridViewTienDoCongSuDG.GetRowCellValue(gridViewTienDoCongSuDG.FocusedRowHandle, gridViewTienDoCongSuDG.Columns["DiemSo"]).ToString();
            }
        }

        private void gridViewChatLuongCongSuDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewChatLuongCongSuDG.GetRowCellValue(gridViewChatLuongCongSuDG.FocusedRowHandle, gridViewChatLuongCongSuDG.Columns["NoiDungChoDiem"]) == null) { return; }
            else
            {
                txtDiemChatLuongCongSuDG.Text = gridViewChatLuongCongSuDG.GetRowCellValue(gridViewChatLuongCongSuDG.FocusedRowHandle, gridViewChatLuongCongSuDG.Columns["DiemSo"]).ToString();
            }
        }

        private void gridViewKaizenCongSuDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewKaizenCongSuDG.GetRowCellValue(gridViewKaizenCongSuDG.FocusedRowHandle, gridViewKaizenCongSuDG.Columns["NoiDungChoDiem"]) == null) { return; }
            else
            {
                txtDiemKaizenCongSuDG.Text = gridViewKaizenCongSuDG.GetRowCellValue(gridViewKaizenCongSuDG.FocusedRowHandle, gridViewKaizenCongSuDG.Columns["DiemSo"]).ToString();
            }
        }

        private void gridView5SCongSuDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView5SCongSuDG.GetRowCellValue(gridView5SCongSuDG.FocusedRowHandle, gridView5SCongSuDG.Columns["NoiDungChoDiem"]) == null) { return; }
            else
            {
                txtDiem5SCongSuDG.Text = gridView5SCongSuDG.GetRowCellValue(gridView5SCongSuDG.FocusedRowHandle, gridView5SCongSuDG.Columns["DiemSo"]).ToString();
            }
        }

        private void gridViewViPhamCongSuDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewViPhamCongSuDG.GetRowCellValue(gridViewViPhamCongSuDG.FocusedRowHandle, gridViewViPhamCongSuDG.Columns["NoiDungChoDiem"]) == null) { return; }
            else
            {
                txtDiemViPhamCongSuDG.Text = gridViewViPhamCongSuDG.GetRowCellValue(gridViewViPhamCongSuDG.FocusedRowHandle, gridViewViPhamCongSuDG.Columns["DiemSo"]).ToString();
            }
        }
    }
}