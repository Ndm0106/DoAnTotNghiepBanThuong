using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            DonBanHangs = new HashSet<DonBanHang>();
            DonNhapHangs = new HashSet<DonNhapHang>();
        }

        public string IdNhanVien { get; set; } = null!;
        public string TenHienThi { get; set; } = null!;
        public string? TaiKhoan { get; set; }
        public string? MatKhau { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public int IdChucVu { get; set; }

        public virtual ChucVu IdChucVuNavigation { get; set; } = null!;
        public virtual ICollection<DonBanHang> DonBanHangs { get; set; }
        public virtual ICollection<DonNhapHang> DonNhapHangs { get; set; }
    }
}
