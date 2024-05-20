using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class DonVi
    {
        public DonVi()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string IdDonVi { get; set; } = null!;
        public string TenDonVi { get; set; } = null!;

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
