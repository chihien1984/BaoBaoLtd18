using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlysanxuat
{
    class TabAdd
    {
        public void AddTab(DevExpress.XtraTab.XtraTabControl XtraTabCha, string icon,
        string TabNameAdd, System.Windows.Forms.DevExpress.XtraEditors.XtraForm DevExpress.XtraEditors.XtraForm)
        {
            // Khởi tạo 1 Tab Con (XtraTabPage) 
            DevExpress.XtraTab.XtraTabPage TAbAdd = new DevExpress.XtraTab.XtraTabPage();
            // Đặt đại cái tên cho nó là TestTab (Đây là tên nhé)
            TAbAdd.Name = "TestTab";
            // Tên của nó là đối số như đã nói ở trên
            TAbAdd.Text = TabNameAdd;
            // Add đối số DevExpress.XtraEditors.XtraForm vào Tab con vừa khởi tạo ở trên
            TAbAdd.Controls.Add(DevExpress.XtraEditors.XtraForm);
            // Dock cho nó tràn hết TAb con đó
            DevExpress.XtraEditors.XtraForm.Dock = DockStyle.Fill;
            try
            {
                // Icon của Tab con khi add vào Tab cha sẽ được quy định ở đây(cái này các bác tự chọn đường dẫn đến file Icon đó nhé)
                TAbAdd.Image = System.Drawing.Bitmap.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + @"\star...ico" + icon);
            }
            catch (Exception)
            {
            }
            // Quăng nó lên TAb Cha (XtraTabCha là đối số thứ nhất như đã nói ở trên) 
            XtraTabCha.TabPages.Add(TAbAdd);
        }
    }
}
