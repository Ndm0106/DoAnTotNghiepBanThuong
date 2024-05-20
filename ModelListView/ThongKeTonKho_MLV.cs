using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiepBanThuong.ModelListView
{
    public class ThongKeTonKho_MLV
    {
        public string? IdSanPham { get; set; } = null!;
        public string? TenDonVi { get; set; } = null!;
        public string? TenSanPham { get; set; } = null!;
        public string? TenNhomSanPham { get; set; } = null!;
        public int? SoLuongTonKho { get; set; }
        public decimal? DonGiaBan { get; set; }
        public decimal? DonGiaNhap { get; set; }
        public int? SoLuongNhap { get; set; }
        public int? SoLuongBan { get; set; }
        public decimal? ThanhTienNhap { get; set; }
        public decimal? ThanhTienBan { get; set; }
    }
}
