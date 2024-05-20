using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiepBanThuong.ModelListView
{
    public class ChiTietDonNhapHang_MLV
    {
        public string? IdChiTietDonNhapHang { get; set; }
        public string? IdSanPham { get; set; }
        public string? IdDonNhapHang { get; set; }
        public int? SoLuongNhap { get; set; }
        public decimal? DonGiaNhap { get; set; }
        public string? TenSanPham { get; set; }
        public decimal? ThanhTienNhap { get; set; }
        public string? TenDonVi { get; set; }
        public decimal? GiaNhap { get; set; }
        public DateTime? HanSuDung { get; set; }
        public string? SoLo { get; set; }
        public string? IdDonVi { get; set; }
        public virtual SanPham_MLV IdSanPhamNavigation { get; set; } = null!;
        public virtual DonNhapHang_MLV IdDonNhapHangNavigation { get; set; } = null!;
    }
}
