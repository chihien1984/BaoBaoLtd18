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

namespace quanlysanxuat
{
    public partial class frmLoading : Form
    {
        private string filename;
        private string pathname;
        public frmLoading(string fileName, string pathName) {

            this.filename = fileName;
            this.pathname = pathName;
            InitializeComponent();
        }
        private void Loading()
        {
            BackgroundWorkerRun wrun = new BackgroundWorkerRun();
            wrun.filename = filename;
            wrun.pathname = pathname;
            wrun.formLoad = this;
            wrun.RunMyWorker();
        }
        private void LoadingXls()
        {
            BackgroundWorkerRun wrun = new BackgroundWorkerRun();
            wrun.filename = filename;
            wrun.pathname = pathname;
            wrun.formLoad = this;
            wrun.RunMyWorkerXls();
        }
        private void frmLoading_Load(object sender, EventArgs e)
        {
            Loading();
            LoadingXls();
        }

    }
}