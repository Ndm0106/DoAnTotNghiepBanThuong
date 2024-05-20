using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class NhomSanPham
    {
        public NhomSanPham()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string IdNhomSanPham { get; set; } = null!;
        public string TenNhomSanPham { get; set; } = null!;
        public string? GhiChu { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
