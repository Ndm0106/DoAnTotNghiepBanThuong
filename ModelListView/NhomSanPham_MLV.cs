using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiepBanThuong.ModelListView
{
    public class NhomSanPham_MLV
    {
        public string IdNhomSanPham { get; set; } = null!;
        public string TenNhomSanPham { get; set; } = null!;
        public string? GhiChu { get; set; }
    }
}
