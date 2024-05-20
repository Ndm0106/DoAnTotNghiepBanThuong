using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class DonNhapHang
    {
        public DonNhapHang()
        {
            ChiTietDonNhapHangs = new HashSet<ChiTietDonNhapHang>();
        }

        public string IdDonNhapHang { get; set; } = null!;
        public string IdNhanVien { get; set; } = null!;
        public string IdNhaPhanPhoi { get; set; } = null!;
        public DateTime? NgayNhap { get; set; }
        public decimal? TongTienDonNhapHang { get; set; }

        public virtual NhaPhanPhoi IdNhaPhanPhoiNavigation { get; set; } = null!;
        public virtual NhanVien IdNhanVienNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietDonNhapHang> ChiTietDonNhapHangs { get; set; }
    }
}
