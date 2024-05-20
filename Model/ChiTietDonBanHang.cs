using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class ChiTietDonBanHang
    {
        public string IdChiTietDonBanHang { get; set; } = null!;
        public string IdDonBanHang { get; set; } = null!;
        public string IdSanPham { get; set; } = null!;
        public int? SoLuongBan { get; set; }
        public decimal? ChietKhau { get; set; }
        public decimal? DonGiaBan { get; set; }

        public virtual DonBanHang IdDonBanHangNavigation { get; set; } = null!;
        public virtual SanPham IdSanPhamNavigation { get; set; } = null!;
    }
}
