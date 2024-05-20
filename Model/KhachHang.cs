using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DonBanHangs = new HashSet<DonBanHang>();
        }

        public string IdKhachHang { get; set; } = null!;
        public string TenKhachHang { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<DonBanHang> DonBanHangs { get; set; }
    }
}
