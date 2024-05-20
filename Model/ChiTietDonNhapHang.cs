using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class ChiTietDonNhapHang
    {
        public string IdChiTietDonNhapHang { get; set; } = null!;
        public string IdSanPham { get; set; } = null!;
        public string IdDonNhapHang { get; set; } = null!;
        public int? SoLuongNhap { get; set; }
        public decimal? DonGiaNhap { get; set; }

        public virtual DonNhapHang IdDonNhapHangNavigation { get; set; } = null!;
        public virtual SanPham IdSanPhamNavigation { get; set; } = null!;
    }
}
