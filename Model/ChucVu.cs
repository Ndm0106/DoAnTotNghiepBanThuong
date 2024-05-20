using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class ChucVu
    {
        public ChucVu()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public int IdChucVu { get; set; }
        public string TenChucVu { get; set; } = null!;

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
