using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SachOnlineTVD.Models;
namespace SachOnlineTVD.Models
{
    public class GioHang
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get
            {
                return iSoLuong * dDonGia;
            }
        }

        public GioHang(int ms)
        {
            this.iMaSach = ms;
            SACH s = db.SACHes.Single(n => n.MaSach == iMaSach);
            this.sTenSach = s.TenSach;
            this.sAnhBia = s.AnhBia;
            this.dDonGia = double.Parse(s.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}