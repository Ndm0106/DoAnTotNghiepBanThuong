using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class NhaSanXuat
    {
        public NhaSanXuat()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string IdNhaSanXuat { get; set; } = null!;
        public string TenNhaSanXuat { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? Fax { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
