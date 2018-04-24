using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlysanxuat.Model
{
    using System;
    using System.Collections.Generic;
    public partial class DeliveryResource
    {
        public long ResourceID { get; set; }
        public string Ma_Nguonluc { get; set; }
        public string Ten_Nguonluc { get; set; }
        public string ToThucHien { get; set; }
        public Nullable<System.DateTime> Ngay { get; set; }
        public string Nguoi { get; set; }
    }
}
