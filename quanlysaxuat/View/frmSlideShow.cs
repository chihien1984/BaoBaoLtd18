using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.SqlClient;

namespace quanlysanxuat
{
    public partial class frmSlideShow : DevExpress.XtraEditors.XtraForm
    {
        public frmSlideShow()
        {
            InitializeComponent();
        }
        private void List_TienDo()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select idsp,madh,mabv,sanpham,Ten_ct,soluongyc,tonkho, 
                soluongsx,isnull(BTPT11,0)-isnull(soluongsx,0) as CONLAI,donvi,ngoaiquang,ngaytrienkhai,dayend, 
                 BTPT1,BTPT2,BTPT3,BTPT4,BTPT5,BTPT6,BTPT7,BTPT8,BTPT9,BTPT10, 
                 BTPT11,BTPT12,BTPT13,BTPT14,BTPT15,BTPT16,BTPT17,BTPT18,BTPT19,BTPT20,BTPT21 
                 from tblchitietkehoach where ngaytrienkhai 
                 between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
        }
        private void List_TienDoTong()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select idsp,madh,mabv,sanpham,Ten_ct,soluongyc,tonkho,soluongsx, 
                isnull(BTPT16,0)-isnull(soluongsx,0) as CONLAI,donvi,ngoaiquang,ngaytrienkhai,dayend, 
                 BTPT1,BTPT2,BTPT3,BTPT4,BTPT5,BTPT6,BTPT7,BTPT8,BTPT9,BTPT10, 
                 BTPT11,BTPT12,BTPT13,BTPT14,BTPT15,BTPT16,BTPT17,BTPT18,BTPT19,BTPT20,BTPT21 
                 from tblchitietkehoach where ngaytrienkhai 
                 between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
        }
       
        private void List_GIAOHANG1()//to1
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * from tbl01 "
            + " where idsp like '"+txtid.Text+"'");
            kn.dongketnoi(); gridView1.ExpandAllGroups();
        }
        private void List_GIAOHANG2()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("select * from tbl02 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView3.ExpandAllGroups();
        }
        private void List_GIAOHANG3()
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang("select * from tbl03 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView4.ExpandAllGroups();
        }
        private void List_GIAOHANG4()//Cat tole
        {
            ketnoi kn = new ketnoi();
            gridControl5.DataSource = kn.laybang("select * from tbl04 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView5.ExpandAllGroups();
        }
        private void List_GIAOHANG5()
        {
            ketnoi kn = new ketnoi();
            gridControl6.DataSource = kn.laybang("select * from tbl05 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView6.ExpandAllGroups();
        }
        private void List_GIAOHANG6()
        {
            ketnoi kn = new ketnoi();
            gridControl7.DataSource = kn.laybang("select * from tbl06 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView7.ExpandAllGroups();
        }
        private void List_GIAOHANG7()//Ban ghe
        {
            ketnoi kn = new ketnoi();
            gridControl8.DataSource = kn.laybang("select * from tbl07 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView8.ExpandAllGroups();
        }
        private void List_GIAOHANG8()// Han pat
        {
            ketnoi kn = new ketnoi();
            gridControl9.DataSource = kn.laybang("select * from tbl08 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView9.ExpandAllGroups();
        }
        private void List_GIAOHANG9()//Danh bong
        {
            ketnoi kn = new ketnoi();
            gridControl10.DataSource = kn.laybang("select * from tbl09 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView10.ExpandAllGroups();
        }
        private void List_GIAOHANG10()//Son
        {
            ketnoi kn = new ketnoi();
            gridControl11.DataSource = kn.laybang("select * from tbl10 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView11.ExpandAllGroups();
        }
        private void List_GIAOHANG11()//Xuat kho
        {
            ketnoi kn = new ketnoi();
            gridControl12.DataSource = kn.laybang("select * from tbl11 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView12.ExpandAllGroups();
        }
        private void List_GIAOHANG12()//Tien
        {
            ketnoi kn = new ketnoi();
            gridControl13.DataSource = kn.laybang("select * from tbl12 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView13.ExpandAllGroups();
        }
        private void List_GIAOHANG14()//Bulon
        {
            ketnoi kn = new ketnoi();
            gridControl14.DataSource = kn.laybang("select * from tbl14 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView14.ExpandAllGroups();
        }
        private void List_GIAOHANG15()//Gia cong
        {
            ketnoi kn = new ketnoi();
            gridControl15.DataSource = kn.laybang("select * from tbl15 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView15.ExpandAllGroups();
        }
        private void List_GIAOHANG16()//Nhap kho tp
        {
            ketnoi kn = new ketnoi();
            gridControl16.DataSource = kn.laybang("select * from tbl16 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView16.ExpandAllGroups();
        }
        private void List_GIAOHANG17()
        {
            ketnoi kn = new ketnoi();
            gridControl18.DataSource = kn.laybang("select * from tbl17 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView18.ExpandAllGroups();
        }
        private void List_GIAOHANG18()//A phi han
        {
            ketnoi kn = new ketnoi();
            gridControl18.DataSource = kn.laybang("select * from tbl18 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView18.ExpandAllGroups();
        }
        private void List_GIAOHANG19()//a Lam dap
        {
            ketnoi kn = new ketnoi();
            gridControl19.DataSource = kn.laybang("select * from tbl19 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView19.ExpandAllGroups();
        }
        private void List_GIAOHANG20()
        {
            ketnoi kn = new ketnoi();
            gridControl20.DataSource = kn.laybang("select * from tbl20 "
            + " where idsp like '" + txtid.Text + "'");
            kn.dongketnoi(); gridView20.ExpandAllGroups();
        }
        private void ChiTietTienDo()
        {
            try
            {
                SqlConnection con = new SqlConnection(Connect.mConnect);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select KH.AllProgress from tblchitietkehoach CT "
                       +" left outer join viewCHITIET_BPSX KH on CT.idsp=KH.idsp where KH.IDSP like " + txtid.Text + "", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtProject.Text = Convert.ToString(reader[0]);
                }
                con.Close();
            }
            catch { };
        }
        private void List_TienDoChung(Object sender,EventArgs e)
        {
            List_TienDoTong();
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMasp.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            {
                MessageBox.Show("Mã sản không khớp đúng");
            }
        }
        private void Layout_Sanpham(object Sender,EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void Layout_DonHang(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMadh.Text, txtPath_Madh.Text);
            f2.Show();
        }
        private void Layout_PSX()//Hàm gọi phiếu sản xuất
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_Madh.Text, txtMadh.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Hiện mã phiếu sản xuất này chưa đúng"); }
        }
        
        private void frmSlideShow_Load(object sender, EventArgs e)// From load
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); 
            dpden_ngay.Text = DateTime.Now.ToString();
            List_TienDo();
        }
        private void List_ChiTiet()
        {
            List_GIAOHANG1(); List_GIAOHANG4(); List_GIAOHANG7(); List_GIAOHANG8(); List_GIAOHANG9(); List_GIAOHANG10();
            List_GIAOHANG11();List_GIAOHANG12(); List_GIAOHANG14(); List_GIAOHANG15();
            List_GIAOHANG16();List_GIAOHANG18(); List_GIAOHANG19();
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtid.Text = gridView2.GetFocusedRowCellDisplayText(id_grid2);
            txtMadh.Text = gridView2.GetFocusedRowCellDisplayText(Madh_grid2);
            txtMasp.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
            ChiTietTienDo(); List_ChiTiet();
        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {
            List_ChiTiet();
        }
        private void List_GH01(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * from tbl01 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView1.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH02(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("select * from tbl02 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView3.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH03(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang("select * from tbl03 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView4.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH04(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl5.DataSource = kn.laybang("select * from tbl04 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView5.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH05(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl6.DataSource = kn.laybang("select * from tbl05 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView6.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH06(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl7.DataSource = kn.laybang("select * from tbl06 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView7.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH07(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl8.DataSource = kn.laybang("select * from tbl07 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView8.ExpandAllGroups();
            kn.dongketnoi();

        }
        private void List_GH08(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl9.DataSource = kn.laybang("select * from tbl08 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView9.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH09(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl10.DataSource = kn.laybang("select * from tbl09 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView10.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH10(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl11.DataSource = kn.laybang("select * from tbl10 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView11.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH11(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl12.DataSource = kn.laybang("select * from tbl11 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView12.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH12(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl13.DataSource = kn.laybang("select * from tbl12 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView13.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH14(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl14.DataSource = kn.laybang("select * from tbl14 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView14.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH15(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl15.DataSource = kn.laybang("select * from tbl15 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView15.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH16(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl16.DataSource = kn.laybang("select * from tbl16 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView16.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH17(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tbl17 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView2.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH18(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl18.DataSource = kn.laybang("select * from tbl18 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView18.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH19(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl19.DataSource = kn.laybang("select * from tbl19 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView19.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void List_GH20(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl20.DataSource = kn.laybang("select * from tbl20 where convert(Date,ngaynhan,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            gridView20.ExpandAllGroups();
            kn.dongketnoi();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.txtProject.ForeColor = Color.Red;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.txtProject.ForeColor = Color.Green;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.txtProject.ForeColor = Color.Red;
        }
    }
}