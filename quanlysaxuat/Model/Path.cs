using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlysanxuat
{
    public class Path
    {
        public string pathname;
        public string pathbanve;
        public string pathquytrinh;
        public string pathkinhdoanh;
        public string pathDanhMucVatTu;

        public Path()
        {
            pathbanve = @"\\Server\kythuat\DM_SANPHAM";
            pathquytrinh = @"\\Server\kythuat\qtrinh_san_xuat";
            pathkinhdoanh = @"\\Server\KINH-DOANH";
            pathDanhMucVatTu = @"\\Server\kythuat\dm_vattu";
        }
    }
}
