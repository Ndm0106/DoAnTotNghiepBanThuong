using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiepBanThuong.ModelListView
{
    public class NhaSanXuat_MLV
    {
        public string IdNhaSanXuat { get; set; } = null!;
        public string TenNhaSanXuat { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? Fax { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
    }
}
