using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiepBanThuong.ModelListView
{
    public class DonNhapHang_MLV
    {
        public string? IdDonNhapHang { get; set; } = null!;
        public string? IdNhaPhanPhoi { get; set; }
        public string? IdNhanVien { get; set; } = null!;
        public DateTime? NgayNhap { get; set; }
        public decimal? TongTienDonNhapHang { get; set; }
        public string? TenHienThi { get; set; }
        public string? TenNhaPhanPhoi { get; set; }
        public List<ChiTietDonNhapHang_MLV>? chiTietDonNhapHang_MLVs { get; set; }
    }
}
