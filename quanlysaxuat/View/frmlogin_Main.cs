using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Net.NetworkInformation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using quanlysanxuat.Model;
using System.Linq;
using DevExpress.XtraEditors;

/// <summary> Chương trình quản lý sản xuất được phát triển bởi Nguyễn Chí Hiền
/// Do các yêu cầu từ các bộ phận gấp nên mình không làm MVC
///
namespace quanlysanxuat
{

    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public static string Username = "";
        public static string role = "";
        public static string dePartMent;
        SANXUATDbContext baobao = new SANXUATDbContext();
        public Login()
        {
            InitializeComponent();
        }
 
        private void GetRole()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = Connect.mConnect;
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            string sqlQuery = string.Format(@"select * from AspNetUsers where UserName like '{0}'", txtUserName.Text);
            SqlCommand cmd = new SqlCommand(sqlQuery, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                lbRole.Text = Convert.ToString(reader[0]);
            role = lbRole.Text;
            reader.Close();
        }
        private void THDecrypt()
        {
            //var result = await _signInManager.PasswordSignInAsync("xxx", "", model.RememberMe, lockoutOnFailure: false);
        }
        private void btmdangnhap_Click(object sender, EventArgs e)
        {
            /*
                 string sqlQuery = string.Format(@"execute LoginApp N'{0}',N'{1}'",userName,passWord);
                Model.Function.ConnectSanXuat();
                var dt = Model.Function.GetDataToTable(sqlQuery);
                if (dt.Rows.Count > 0)
                {
                    MainDev fMaindev = new MainDev(role);//Mở form Main lên truyền mã key vào form Main
                    Username = txtTaiKhoan.Text;
                    GetRole();//
                    writer_version();//Ghi file vesion
                    //ghi file tài khoản
                    if (chkGhiNhoMatKhau.Checked == true) { TaoMoiXML(); }//Nêu như người dùng check thì ghi ra file xml
                    else
                    {
                        string duongDanTenFile = Directory.GetCurrentDirectory() + @"\Login.xml";
                        File.Delete(duongDanTenFile);
                    }
                    fMaindev.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Bạn nhập sai tên truy cập hặc mật khẩu", "Thông báo",
                        MessageBoxButtons.OK);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
                return;
                 */
            /*
            if (user.AppPasswordHash == passWord)
           {
               ClassUser.User = user.UserName;
               ThUserName();
               MainDev frm = new MainDev();
               frm.Show();
               Hide();
           }
           else
           {
               XtraMessageBox.Show("Sai mật khẩu[password error]", "Thông báo[Message]",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
            */
            string userName = txtUserName.Text.Trim();
            string passWord = Mahoa.Encrypt(txtPassWord.Text.Trim());
            var userNameEqua = baobao.AspNetUsers.FirstOrDefault(x => x.UserName == userName);
            var passWordEqua = baobao.AspNetUsers.FirstOrDefault(x => x.AppPasswordHash == passWord);
            var user = baobao.AspNetUsers.FirstOrDefault(x => x.UserName == userName && x.AppPasswordHash == passWord);
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text)||userNameEqua==null)
                {
                    lbMessagerLogin.Text = "Tên đăng nhập không đúng";
                    txtUserName.Focus();
                    return;
                }

                if (txtPassWord.Text.Length == 0||passWordEqua==null)
                {
                    lbMessagerLogin.Text = "Mật khẩu không đúng";
                    txtPassWord.Focus();
                    return;
                }

                if (user != null)
                {
                        ClassUser.User = user.UserName;
                        ThUserName();//Lưu tên người dùng vào form login
                        //ghi file tài khoản
                        //Lưu tài khoản người dùng vào file xml
                        if (chkGhiNhoMatKhau.Checked == true) { TaoMoiXML(); }
                        else
                        {
                            string duongDanTenFile = Directory.GetCurrentDirectory() + @"\Login.xml";
                            File.Delete(duongDanTenFile);
                        }
                        MainDev frm = new MainDev();
                        frm.Show();
                        Hide();
                }
            }
            catch
            {
                //MessageBox.Show("Lỗi [error connect server]", "Thông báo[Message]");
                lbMessagerLogin.Text = "Lỗi [error connect server]";
            }
            //Application.Exit();
        }
        private void ThUserName()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select LastName+' '+FirstName from AspNetUsers where UserName like '{0}'",txtUserName.Text);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                Username = Convert.ToString(reader[0]);
            reader.Close();
        }
        #region tạo mới file XML lưu tài khoản
        private void TaoMoiXML()
        {
            //1-Khai báo& khởi tạo biến tài liệu kiểu XmlDocument
            XmlDocument tailieu = new XmlDocument();

            //2-Tao node gốc(rootNode)
            XmlElement rootNode = tailieu.CreateElement("FileMemberlogin");
            tailieu.AppendChild(rootNode);

            //3-Tạo node con(childNode) cấp 1
            XmlElement taikhoanNode = tailieu.CreateElement("Mylogin");//tạo element tai khoai

            rootNode.AppendChild(taikhoanNode);

            //-----------------------------------------------
            // 4- Tạo  và gán giá trị cho các thuộc tính của tài khoản
            taikhoanNode.SetAttribute("TaiKhoan", txtUserName.Text);

            //-----------------------------------------------

            //5- Lưu thành tập tin
            string duongDanTenFile = Directory.GetCurrentDirectory() + @"\Login.xml";
            tailieu.Save(duongDanTenFile);
            lblKetQua.Text = "Tài khoản bạn được lưu lại!";
        }
        #endregion
        #region
        private void writer_version()
        {
            //1-Khai báo& khởi tạo biến tài liệu kiểu XmlDocument
            XmlDocument tailieu = new XmlDocument();

            //2-Tao node gốc(rootNode)
            XmlElement rootNode = tailieu.CreateElement("FileMemberlogin");
            tailieu.AppendChild(rootNode);

            //3-Tạo node con(childNode) cấp 1
            XmlElement versionNode = tailieu.CreateElement("version");//tao element version

            rootNode.AppendChild(versionNode);

            //-----------------------------------------------
            // 4- Tạo  và gán giá trị cho các thuộc tính của tài khoản
            versionNode.SetAttribute("version", lbVersion.Text);
            //-----------------------------------------------

            //5- Lưu thành tập tin
            string duongDanTenFile = Directory.GetCurrentDirectory() + @"\version.xml";
            tailieu.Save(duongDanTenFile);
            lblKetQua.Text = "Tài khoản bạn được lưu lại!";
        }
        #endregion

        #region Đọc file XML lên tài khoản 
        private void DocFileXML()
        {
            //1-Khai báo& khởi tạo biến tài liệu kiểu XmlDocument
            XmlDocument tailieu = new XmlDocument();
            //2-Load Nội dung từ tập tin xml
            string duongDanTenFile = Directory.GetCurrentDirectory() + @"\Login.xml";
            tailieu.Load(duongDanTenFile);
            //3-Tham chiếu đến (đọc) các element có tageName là 'Mylogin'
            XmlNodeList tkNodeList = tailieu.GetElementsByTagName("Mylogin");

            //Doc danh sach xuat len lable
            StringBuilder sb = new StringBuilder();
            foreach (XmlNode tk in tkNodeList)
            {
                string taiKhoan = tk.Attributes["TaiKhoan"].InnerText;
                txtUserName.Text = taiKhoan;
            }
        }
        #endregion
        #region ping server
        public bool IsConnectedToInternet()
        {
            Ping p = new Ping();
            try
            {

                PingReply reply = p.Send("www.uic.co.il", 1000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        #endregion
        #region kiểm tra phiên bản của phần mềm với cơ sở dữ liệu
        private void TestVersion()
        {
            Ping ping = new Ping();
            PingReply pingresult = ping.Send("192.168.1.22", 1000);
            if (pingresult.Status.ToString() != "Success")
            {
                MessageBox.Show("Vui lòng kiểm tra đường truyền", "Không thể kết nối server...");
                this.Close();
            }
            else
            {
                try { 
                    //Doc verver từ CSDL
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT CurenVersion from tblVersion where id=1", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        lbVersion.Text = Convert.ToString(reader[0]);
                    reader.Close();

                    //1-Khai báo& khởi tạo biến tài liệu kiểu XmlDocument
                    XmlDocument tailieu = new XmlDocument();
                    //2-Load Nội dung từ tập tin xml
                    string duongDanTenFile = Directory.GetCurrentDirectory() + @"\version.xml";
                    tailieu.Load(duongDanTenFile);
                    //3-Tham chiếu đến (đọc) các element có tageName là 'Mylogin'
                    XmlNodeList tkNodeList = tailieu.GetElementsByTagName("version");
                    //Doc danh sach xuat len lable
                    StringBuilder sb = new StringBuilder();
                    foreach (XmlNode tk in tkNodeList)
                    {
                        string version = tk.Attributes["version"].InnerText;
                        if (lbVersion.Text != version)
                        {
                            DialogResult dialogResult = MessageBox.Show("Có cập nhật mới", "Thông báo", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                //MessageBox.Show("Cập nhật");
                                Process.Start(AppDomain.CurrentDomain.BaseDirectory + "UPDATE.EXE");
                                Application.Exit();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                SqlCommand cmdOld = new SqlCommand(@"SELECT CurenVersion from tblVersion where id=1", con);
                                SqlDataReader readerOld = cmdOld.ExecuteReader();
                                if (readerOld.Read())
                                    lbVersion.Text = Convert.ToString(readerOld[0]);
                                readerOld.Close();
                                //MessageBox.Show("tiếp tục đăng nhập");
                            }
                        };
                    }
                    //autocompletennguoidangnhap();//Nếu mạng oke thì mới thực hiện hành động autocomplete tên người dùng và đọc file xml các kiểu
                    //string duongDanTenFile = Directory.GetCurrentDirectory() + @"\Login.xml";
                    if (File.Exists(duongDanTenFile))
                    {
                        DocFileXML();
                        //Nếu trong file chứa app có lưu tài khoản xml thì checked là true
                        if (!string.IsNullOrEmpty(txtUserName.Text))
                        { chkGhiNhoMatKhau.Checked = true; }
                    }
                    if (chkGhiNhoMatKhau.Checked == true)
                    {
                        lblKetQua.Text = "Tài khoản bạn được lưu lại!";
                    }
                }
                catch
                {
                    //MessageBox.Show("Vui lòng kiểm tra đường truyền", "Không thể kết nối server...");
                }
            }
        }
        #endregion

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btmLogin.PerformClick();
            }
        }
        //region formload
        private void frmUser_Load(object sender, EventArgs e)
        {
            TestVersion();
        }
        private void autocompletennguoidangnhap()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.mConnect))
                {
                    SqlCommand cmd = new SqlCommand("select UserName from AspNetUsers", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MSPCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MSPCollection.Add(reader.GetString(0));
                    }
                    txtUserName.AutoCompleteCustomSource = MSPCollection;
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối mạng lý do:" + ex.Message);
                Application.Exit();
            }
        }
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Thoat();
        }
        private void btmthoat_Click(object sender, EventArgs e)
        {
            Thoat();
        }
        private void Thoat()
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            txtUserName.ResetText();
            txtPassWord.ResetText();
            txtPassWord.Focus();
        }

        private void Taikhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbnum_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            Process.Start("http://erp.baobao.vn");
        }

        private void lbBaoBaows_Click(object sender, EventArgs e)
        {
            Process.Start("http://erp.baobao.vn");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool VerifyUserNamePassword(string userName, string password)
        {
            var usermanager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityDbContext()));
            return usermanager.Find(userName, password) != null;
        }
        private void Login_Click(object sender, EventArgs e)
        {
            var u =txtUserName.Text; var p = txtPassWord.Text;
            try
            {
                var t = VerifyUserNamePassword(u, p);
                MessageBox.Show(""+ t.ToString(),"");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //System.Threading.Tasks.Task<Microsoft.AspNetCore.Identity.SignInResult>
            //PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure);
        }

        private void lbDangKy_Click(object sender, EventArgs e)
        {
            Process.Start("http://erp.baobao.vn/Account/Register");
        }

        private void chkGhiNhoMatKhau_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

