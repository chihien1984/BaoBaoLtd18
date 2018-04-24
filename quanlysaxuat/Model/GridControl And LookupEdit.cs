using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlysanxuat
{
    class GridControl_And_LookupEdit
        {
        public string Mavlphu { set; get; }
        public string Donvi { set;get; }


        private double _SoLuong;
            
        public double SoLuong
        {
            get { return _SoLuong; }
            set
            {
                if (value < 0) throw new Exception("Số lượng không được nhỏ hơn 0");    
                _SoLuong = value;
            }
        }
        private double _DonGia;

        public double DonGia
        {
            get { return _DonGia; }
            set
            {
                if (value < 0) throw new Exception("Đơn giá không được nhỏ hơn 0");
                _DonGia = value;
            }
        }
        private double _ThanhTien;

        public double ThanhTien
        {
            get { return _ThanhTien; }
            set {if (value < 0) throw new Exception("Thành tiền không được nhỏ hơn 0");
                _ThanhTien = value; }
        }

        public GridControl_And_LookupEdit()
        {
        }
    }
}
