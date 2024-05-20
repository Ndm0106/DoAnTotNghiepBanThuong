using System;
using System.Collections.Generic;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietDonBanHangs = new HashSet<ChiTietDonBanHang>();
            ChiTietDonNhapHangs = new HashSet<ChiTietDonNhapHang>();
        }

        public string IdSanPham { get; set; } = null!;
        public string TenSanPham { get; set; } = null!;
        public string? ThanhPhan { get; set; }
        public string? HamLuong { get; set; }
        public int? SoLuongTon { get; set; }
        public decimal? GiaBan { get; set; }
        public decimal? GiaNhap { get; set; }
        public DateTime? HanSuDung { get; set; }
        public string? BarCode { get; set; }
        public string? GhiChu { get; set; }
        public string IdNhaSanXuat { get; set; } = null!;
        public string IdDonVi { get; set; } = null!;
        public string IdNhomSanPham { get; set; } = null!;

        public virtual DonVi IdDonViNavigation { get; set; } = null!;
        public virtual NhaSanXuat IdNhaSanXuatNavigation { get; set; } = null!;
        public virtual NhomSanPham IdNhomSanPhamNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietDonBanHang> ChiTietDonBanHangs { get; set; }
        public virtual ICollection<ChiTietDonNhapHang> ChiTietDonNhapHangs { get; set; }
    }
}
