using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class DonBanHang
    {
        public DonBanHang()
        {
            ChiTietDonBanHangs = new HashSet<ChiTietDonBanHang>();
        }

        public string IdDonBanHang { get; set; } = null!;
        public string IdKhachHang { get; set; } = null!;
        public string IdNhanVien { get; set; } = null!;
        public DateTime? NgayBan { get; set; }
        public decimal? TongTienDonBanHang { get; set; }

        public virtual KhachHang IdKhachHangNavigation { get; set; } = null!;
        public virtual NhanVien IdNhanVienNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietDonBanHang> ChiTietDonBanHangs { get; set; }
    }
}
