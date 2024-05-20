using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiepBanThuong.ModelListView
{
    public class KhachHang_MLV
    {
        public string IdKhachHang { get; set; } = null!;
        public string TenKhachHang { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
    }
}
