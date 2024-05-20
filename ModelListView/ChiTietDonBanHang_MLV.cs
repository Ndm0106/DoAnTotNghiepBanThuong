using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiepBanThuong.ModelListView
{
    public class ChiTietDonBanHang_MLV
    {
        public string? IdChiTietHoaDonBanHang { get; set; }
        public string? IdDonBanHang { get; set; }
        public string? IdSanPham { get; set; }
        public int? SoLuongBan { get; set; }
        public decimal? DonGiaBan { get; set; }
        public decimal? ChietKhau { get; set; }
    }
}
