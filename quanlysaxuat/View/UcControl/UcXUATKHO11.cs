using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.XtraReports.UI;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;

namespace quanlysanxuat
{
    public partial class UcXUATKHO11 : DevExpress.XtraEditors.XtraForm
    {
        public UcXUATKHO11()
        {
            InitializeComponent();
        }
        Clsketnoi kn = new Clsketnoi();
        string Gol = "";
        //SqlCommand cmd;
        private void tich()
        {
            double SL = double.Parse(txtsoluongdonhang.Text);
            double TPDX = double.Parse(txtsoluongdaxuat.Text);
            double SOLUONGCONLAI = SL - TPDX;
            txtsoluongchuaxuat.Text = Convert.ToString(SOLUONGCONLAI);
        }

        private void loadData1()
        {
            gridControl1.DataSource = kn.laydulieu(" select CTKH.IDSP,nvkd,madh,sanpham,T11.SOLUONGTP,T11.TRONGLUONGTP,soluongyc,tonkho, "
                                   +" soluongsx, ngoaiquang, mabv, donvi, daystar, dayend, BatDauTo11, "
                                   +" KetThucTo11, khachhang, xeploai, Ghichu from tblchitietkehoach CTKH left join "
                                   +" (select IDSP, sum(BTPT11) as SOLUONGTP, sum(TRONGLUONG11) AS TRONGLUONGTP from tbl11 group by IDSP) AS T11 "
                                   +" ON CTKH.IDSP = T11.IDSP where madh is not null and madh like N'"+cbmadh.Text+"' ");
        }
        private void loadData2()
        {
            gridControl2.DataSource = kn.laydulieu("SELECT IDSP,ngaynhan,nvkd,madh,tendh,sanpham,chitietsanpham,cdthanhpham,soluongsx,soluongyc, "
                                        + "ngoaiquang, mabv, donvi, daystar, dayend, BTPT11, TRONGLUONG11, khachhang, xeploai, ghichu, "
                                        +"Diengiai, Num, MaBPnhan, TenBPnhan, Giaidoan, MaSQL, TenSQL from tbl11 "
                                        +"WHERE DATEPART(MM, ngaynhan) = DATEPART(MM, GETDATE()) order by idsp ASC");
        }

        private void autocomple()
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select sanpham from tblchitietkehoach where sanpham is not null", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtQuycachchitiet.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }

        private void autocomplecb()
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select TenKH from tblKHACHHANG where TenKH is not null", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection Collection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    Collection.Add(reader.GetString(0));
                }
                cbkhachhang.AutoCompleteCustomSource = Collection;
                con.Close();
            }
        }
        public void LoadDM_XUATHANG()
        {
            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.laybang("SELECT IDSP,ngaynhan,nvkd,madh,tendh,sanpham,chitietsanpham,cdthanhpham,soluongsx,soluongyc, "
                                        + "ngoaiquang, mabv, donvi, daystar, dayend, BTPT11, TRONGLUONG11, khachhang, xeploai, ghichu, "
                                        + "Diengiai, Num, MaBPnhan, TenBPnhan, Giaidoan, MaSQL, TenSQL from tbl11 where convert(Date,ngaynhan,103) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            Connect.dongketnoi();
        }
        private void UcXUATKHO11_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
            cbmadh.DataSource = kn.laydulieu("Select TOP 1 madh From tblchitietkehoach where madh is not null order by IDSP desc");
            cbmadh.DisplayMember = "madh";
            cbmadh.ValueMember = "madh";
            //loadData1();
            //loadData2();
        }

        private void btaddchitiet_Click(object sender, EventArgs e)
        {
            
        }
        private void btnalldonhang_Click(object sender, EventArgs e)
        {
            //gridControl2.DataSource = kn.laydulieu("SELECT IDSP,ngaynhan,nvkd,madh,sanpham,chitietsanpham,cdthanhpham,soluongsx,soluongyc "
            //          + "  ngoaiquang, mabv, donvi, daystar, dayend, BTPT11, TRONGLUONG11, khachhang, xeploai, ghichu, "
            //          + " Diengiai, Num, MaBPnhan, TenBPnhan, Giaidoan, MaSQL, TenSQL from tbl11 where convert(nvarchar,ngaynhan,103) between '" + dptu_ngay.Value.ToString("dd/MM/yyyy") + "' and '" + dpden_ngay.Value.ToString("dd/MM/yyyy") + "'");
            LoadDM_XUATHANG();
        }

        private void viewdetail_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = kn.laydulieu("select CTKH.IDSP,nvkd,madh,sanpham,T11.SOLUONGTP,T11.TRONGLUONGTP,soluongyc,tonkho, "
                                   + " soluongsx, ngoaiquang, mabv, donvi, daystar, dayend, BatDauTo11, "
                                   + " KetThucTo11, khachhang, xeploai, Ghichu from tblchitietkehoach CTKH left join "
                                   + " (select IDSP, sum(BTPT11) as SOLUONGTP, sum(TRONGLUONG11) AS TRONGLUONGTP from tbl11 group by IDSP) AS T11 "
                                   + " ON CTKH.IDSP = T11.IDSP where madh is not null and convert(Date,ngaytrienkhai,103) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
        }

        private void btnxoachitiet_Click(object sender, EventArgs e)
        {
            if (cbmadh.Text != "")
            {
                ketnoi kn = new ketnoi();
                int kq = kn.xulydulieu("delete tblDONHANG where MADDH = N'" + cbmadh.Text + "' and Code = '" + txtQuycachchitiet.Text + "'");
                if (kq > 0)
                {
                    MessageBox.Show("XOÁ THÀNH CÔNG", "THÔNG BÁO");
                    UcXUATKHO11_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Không Thành Công ?");

                }
            }
        }
        private void txtidsp_TextChanged(object sender, EventArgs e) // TÌM SỐ LẦN GIAO HÀNG THEO CODE SẢN PHẨM
        {
            gridControl2.DataSource = kn.laydulieu("SELECT IDSP,ngaynhan,nvkd,madh,sanpham,chitietsanpham,cdthanhpham,soluongsx, "
                      + "  ngoaiquang, mabv, donvi, daystar, dayend, BTPT11, TRONGLUONG11, khachhang, xeploai, ghichu, "
                      + " Diengiai, Num, MaBPnhan, TenBPnhan, Giaidoan, MaSQL, TenSQL from tbl11 where IDSP like '"+txtidsp.Text+"'");
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtidsp.Text != "" && txtnvkd.Text != "" && cbmadh.Text != "")
            {

                SqlConnection cn = new SqlConnection();
                decimal soluongdonhang = Convert.ToDecimal(txtsoluongdonhang.Text);
                decimal soluongtpxuatkho = Convert.ToDecimal(txtsoluongthanhpham.Text);
                decimal trongluongthanpham = Convert.ToDecimal(txttrongluongthanhpham.Text);
                cn.ConnectionString = Connect.mConnect;

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO tbl11(IDSP,ngaynhan,nvkd,madh,sanpham,"
                + "chitietsanpham,cdthanhpham,soluongsx,ngoaiquang,mabv,donvi,daystar,dayend,BTPT11,"
                + "TRONGLUONG11,khachhang,xeploai,ghichu,MaBPnhan,TenBPnhan,MaSQL,TenSQL)"
                + "values (@IDSP,GETDATE(),@nvkd,@madh,@sanpham,"
                + "@chitietsanpham,@cdthanhpham,@soluongsx,@ngoaiquang,@mabv,@donvi,@daystar,@dayend,@BTPT11,"
                + "@TRONGLUONG11,@khachhang,@xeploai,@ghichu,@MaBPnhan,@TenBPnhan,@MaSQL,@TenSQL)", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                try
                {
                    if (dpNgaybd.Text == "")
                        command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = dpNgaybd.Text;
                    if (dpNgaykt.Text == "")
                        command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = dpNgaykt.Text;
                }
                catch (Exception)
                {
                    throw;
                }
                command.Parameters.Add(new SqlParameter("@IDSP", SqlDbType.Int)).Value = txtidsp.Text;
                command.Parameters.Add(new SqlParameter("@nvkd", SqlDbType.NVarChar)).Value = txtnvkd.Text;
                command.Parameters.Add(new SqlParameter("@madh", SqlDbType.NVarChar)).Value = cbmadh.Text;
                command.Parameters.Add(new SqlParameter("@sanpham", SqlDbType.NVarChar)).Value = txtQuycachchitiet.Text;
                command.Parameters.Add(new SqlParameter("@chitietsanpham", SqlDbType.NVarChar)).Value = ctsanpham.Text;
                command.Parameters.Add(new SqlParameter("@cdthanhpham", SqlDbType.NVarChar)).Value = cbCDthanhpham.Text;
                command.Parameters.Add(new SqlParameter("@soluongsx", SqlDbType.Int)).Value = soluongdonhang;//số lượng đơn hàng
                command.Parameters.Add(new SqlParameter("@ngoaiquang", SqlDbType.NVarChar)).Value = txtngoaiquang.Text;
                command.Parameters.Add(new SqlParameter("@mabv", SqlDbType.NVarChar)).Value = txtmau_banve.Text;
                command.Parameters.Add(new SqlParameter("@donvi", SqlDbType.NVarChar)).Value = txtdonvi.Text;
                command.Parameters.Add(new SqlParameter("@BTPT11", SqlDbType.NVarChar)).Value = soluongtpxuatkho;// số lượng thành phẩm xuất kho
                command.Parameters.Add(new SqlParameter("@TRONGLUONG11", SqlDbType.Decimal)).Value = trongluongthanpham;// trọng lượng thành phẩm xuất kho
                command.Parameters.Add(new SqlParameter("@khachhang", SqlDbType.NVarChar)).Value = cbkhachhang.Text;
                command.Parameters.Add(new SqlParameter("@xeploai", SqlDbType.NVarChar)).Value = cbloaiKH.Text;
                command.Parameters.Add(new SqlParameter("@ghichu", SqlDbType.NVarChar)).Value = txtghichu.Text;
                command.Parameters.Add(new SqlParameter("@MaBPnhan", SqlDbType.NVarChar)).Value = txttenbophan.Text;
                command.Parameters.Add(new SqlParameter("@TenBPnhan", SqlDbType.NVarChar)).Value = txtmabophan.Text;
                command.Parameters.Add(new SqlParameter("@MaSQL", SqlDbType.NVarChar)).Value = txttenbophan.Text;
                command.Parameters.Add(new SqlParameter("@TenSQL", SqlDbType.NVarChar)).Value = txttenbophan.Text;
                adapter.Fill(dt);
                MessageBox.Show("GHI DỮ LIỆU SẢN XUẤT THÀNH CÔNG LÊN HỆ THỐNG", "THÔNG BÁO");
                //UcXUATKHO11_Load(sender,e);
                LoadDM_XUATHANG();
                txtidsp_TextChanged(sender,e);
                txtsoluongthanhpham.Text = "0";
                txttrongluongthanhpham.Text = "0";
                txttrongluongthanhpham.Text = "0";
                cn.Close();
            }
        }
        private void Rpxuatkho()
        {
            //frmreportxuatkho fxuatkho = new frmreportxuatkho();
            //fxuatkho.ShowDialog();
        }
        private void Exportsx_Click(object sender, EventArgs e)
        {
            gridView2.ShowPrintPreview();
        }
        private void ShowTK11()//thống kê
        {
            //frmTK11 fTK11 = new frmTK11();
            //fTK11.ShowDialog();
        }
        private void btnthongke_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(ShowTK11)); //thống kê
            thread.Start();
        }

        private void ShowPj11()//phân tích
        {
            //frmPr11 fpj11 = new frmPr11();
            //fpj11.ShowDialog();
        }
        private void btnphantich_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(ShowPj11)); //phân tích
            thread.Start();//
        }

        private void txtsoluongthanhpham_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            //    e.Handled = true;
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }
        private void txttrongluongthanhpham_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            //    e.Handled = true;
            //if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
            //    e.Handled = true;

        }

        private void txtsoluongdonhang_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            //    e.Handled = true;
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtidsp.Text = gridView1.GetFocusedRowCellDisplayText(idsp);
            cbmadh.Text= gridView1.GetFocusedRowCellDisplayText(madh);
            cbkhachhang.Text= gridView1.GetFocusedRowCellDisplayText(khachhang);
            txtnvkd.Text= gridView1.GetFocusedRowCellDisplayText(nvkd);
            cbloaiKH.Text= gridView1.GetFocusedRowCellDisplayText(phanloai);
            dpNgaybd.Text= gridView1.GetFocusedRowCellDisplayText(ngaybd);
            dpNgaykt.Text= gridView1.GetFocusedRowCellDisplayText(ngaykt);
            txtQuycachchitiet.Text= gridView1.GetFocusedRowCellDisplayText(sanpham);
            txtsoluongdonhang.Text= gridView1.GetFocusedRowCellDisplayText(soluongdonhang);
            txtngoaiquang.Text= gridView1.GetFocusedRowCellDisplayText(ngoaiquang);
            txtmasp.Text=gridView1.GetFocusedRowCellDisplayText(mabv);
            txtmau_banve.Text= gridView1.GetFocusedRowCellDisplayText(mabv);
            txtdonvi.Text= gridView1.GetFocusedRowCellDisplayText(donvi);
            txtghichu.Text= gridView1.GetFocusedRowCellDisplayText(ghichu);
            txtsoluongdaxuat.Text= gridView1.GetFocusedRowCellDisplayText(soluongdaxuat);
            txttrongluongdaxuat.Text = gridView1.GetFocusedRowCellDisplayText(trongluongdaxuat);
            tich();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();// = gridView1.GetFocusedRowCellDisplayText();
            txtQuycachchitiet.Text = gridView2.GetFocusedRowCellDisplayText(sanpham2);
            txtide.Text = gridView2.GetFocusedRowCellDisplayText(Idekey);
        }

        private void txtsoluongthanhpham_TextChanged(object sender, EventArgs e)
        {
            if (txtsoluongthanhpham.Text == "")
            {
                txtsoluongthanhpham.Text = "0";
            }
            txtsoluongthanhpham.Text = string.Format("{0:0,0}", decimal.Parse(txtsoluongthanhpham.Text));
            txtsoluongthanhpham.SelectionStart = txtsoluongthanhpham.Text.Length;
        }

        private void txttrongluongthanhpham_TextChanged(object sender, EventArgs e)
        {
            if (txttrongluongthanhpham.Text == "") { txttrongluongthanhpham.Text = "0"; }
        }

        private void txtsoluongdonhang_TextChanged(object sender, EventArgs e)
        {
            if (txtsoluongdonhang.Text == "") { txtsoluongdonhang.Text = "0"; }
            txtsoluongdonhang.Text = string.Format("{0:0,0}", decimal.Parse(txtsoluongdonhang.Text));
                txtsoluongdonhang.SelectionStart = txtsoluongdonhang.Text.Length;
                tich();
        }

        private void txtsoluongdaxuat_EditValueChanged(object sender, EventArgs e)
        {
            if (txtsoluongdaxuat.Text=="")
            {
                txtsoluongdaxuat.Text = "0";
            }
            else
            {
                txtsoluongdaxuat.Text = string.Format("{0:0,0}", decimal.Parse(txtsoluongdaxuat.Text));
                txtsoluongdaxuat.SelectionStart = txtsoluongdaxuat.Text.Length;
                tich();
            }
        }

        private void txtsoluongchuaxuat_EditValueChanged(object sender, EventArgs e)
        {
            if (txtsoluongchuaxuat.Text == "") { txtsoluongchuaxuat.Text = "0"; }
            //txtsoluongchuaxuat.Text = string.Format("{0:0,0}", decimal.Parse(txtsoluongchuaxuat.Text));
            //txtsoluongchuaxuat.SelectionStart = txtsoluongchuaxuat.Text.Length;
        }

        private void btncapnhatht_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Connect.mConnect;
                conn.Open();
                DataSet ds = SqlHelper.ExecuteDataset(conn, "[T11_UPDATE]");
                MessageBox.Show("CẬP NHẬT TIẾN ĐỘ SẢN XUẤT THÀNH CÔNG LÊN HỆ THỐNG.");
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công");
            }
        }

        private void btnxoadonhang_Click(object sender, EventArgs e)
        {
            if (txtQuycachchitiet.Text != "" && txtide.Text != "")
            {
                ketnoi kn = new ketnoi();
                int kq = kn.xulydulieu("UPDATE tbl11 SET BTPT11 = 0,TRONGLUONG11 = 0  Where Num ='" + txtide.Text + "'");
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Connect.mConnect;
                conn.Open();
                DataSet ds = SqlHelper.ExecuteDataset(conn, "[T11_UPDATE]");
                int kq1 = kn.xulydulieu("DELETE tbl11 Where Num ='" + txtide.Text + "'");
                if (kq > 0 && kq1>0)
                {
                    LoadDM_XUATHANG();
                    MessageBox.Show("XOÁ THÀNH CÔNG", "THÔNG BÁO");
                }
                else
                {
                    MessageBox.Show("Không Thành Công", "THÔNG BÁO");
                }
            }
        }
    }
}
