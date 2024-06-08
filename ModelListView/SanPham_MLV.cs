using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiepBanThuong.ModelListView
{
    public class SanPham_MLV
    {
        public string IdSanPham { get; set; } = null!;
        public string TenSanPham { get; set; } = null!;
        public int? SoLuongTon { get; set; }
        public int? SoLuongNhap { get; set; }
        public int? SoLuongBan { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBan { get; set; }
        public string? BarCode { get; set; }
        public string? ThanhPhan { get; set; }
        public string? HamLuong { get; set; }
        public DateTime? HanSuDung { get; set; }
        public string? GhiChu { get; set; }
        public string IdDonVi { get; set; } = null!;
        public string? IdNhaSanXuat { get; set; }
        public string? IdNhomSanPham { get; set; }
        public string TenDonVi { get; set; } = null!;
        public string TenNhaSanXuat { get; set; } = null!;
        public string TenNhomSanPham { get; set; } = null!;
        public decimal? TongTienSanPham { get; set; }
        public decimal? ChietKhau { get; set; }
        public bool isEditing { get; set; }
    }
}
