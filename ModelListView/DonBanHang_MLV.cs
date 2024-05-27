using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiepBanThuong.ModelListView
{
    public class DonBanHang_MLV
    {
        public string IdDonBanHang { get; set; } = null!;
        public string IdKhachHang { get; set; } = null!;
        public string IdNhanVien { get; set; } = null!;
        public DateTime? NgayBan { get; set; }
        public decimal? TongTienDonBanHang { get; set; }
        public string TenKhachHang { get; set; } = null!;
        public string? SoDienThoai { get; set; }
        public string TenHienThi { get; set; } = null!;
        public List<ChiTietDonBanHang_MLV>? chiTietDonBanHang_MLVs { get; set; }
    }
}
