using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class NhaPhanPhoi
    {
        public NhaPhanPhoi()
        {
            DonNhapHangs = new HashSet<DonNhapHang>();
        }

        public string IdNhaPhanPhoi { get; set; } = null!;
        public string TenNhaPhanPhoi { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? Fax { get; set; }
        public string? MaSoThue { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<DonNhapHang> DonNhapHangs { get; set; }
    }
}
