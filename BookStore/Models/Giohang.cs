using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using BookStore.Models;
namespace BookStore.Models
{
    QLBANSACHEntities data = new QLBANSACHEntities();

    public int iMasach { set; get; }
    public string sTensach { set; get; }
    public string sAnhbia { set; get; }
    public double dDongia { set; get; }
    public int iSoluong { set; get; }
    public double dThanhtien
    {
        get { return iSoluong * dDongia; }
    }
    public Giohang(int Masach)
    {
        iMasach = Masach;
        SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
        sTensach = sach.Tensach;
        sAnhbia = sach.Anhbia;
        dDongia = double.Parse(sach.Giaban.ToString());
        iSoluong = 1;
    }
}
}