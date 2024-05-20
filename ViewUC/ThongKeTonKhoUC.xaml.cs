using DoAnTotNghiepBanThuong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for ThongKeTonKhoUC.xaml
    /// </summary>
    public partial class ThongKeTonKhoUC : UserControl
    {
        public static System.Windows.Controls.ListView? listView;
        public ThongKeTonKhoUC()
        {
            InitializeComponent();
            LoadDataTonKho();
        }
        private void LoadDataTonKho()
        {
            using (var dbContext = new QLQuayThuocBanThuongContext())
            {
                var productInventory = (
    from sp in dbContext.SanPhams
    join dv in dbContext.DonVis on sp.IdDonVi equals dv.IdDonVi
    join nsp in dbContext.NhomSanPhams on sp.IdNhomSanPham equals nsp.IdNhomSanPham
    select new
    {
        sp.IdSanPham,
        sp.TenSanPham,
        TenDonVi = dv.TenDonVi,
        TenNhomSanPham = nsp.TenNhomSanPham,
        GiaNhap = sp.GiaNhap,
        GiaBan = sp.GiaBan,
        SoLuongNhap = dbContext.ChiTietDonNhapHangs.Where(ctpn => ctpn.IdSanPham == sp.IdSanPham).Sum(ctpn => ctpn.SoLuongNhap),
        SoLuongBan = dbContext.ChiTietDonBanHangs.Where(ctdb => ctdb.IdSanPham == sp.IdSanPham).Sum(ctdb => ctdb.SoLuongBan)
    })
    .Select(p => new
    {
        p.IdSanPham,
        p.TenSanPham,
        p.TenDonVi,
        p.TenNhomSanPham,
        p.SoLuongNhap,
        p.SoLuongBan,
        p.GiaNhap,
        p.GiaBan,
        SoLuongTonKho = p.SoLuongNhap - p.SoLuongBan,
        DonGiaNhap = p.SoLuongNhap * p.GiaNhap, // Tính DonGiaNhap
        DonGiaBan = p.SoLuongBan * p.GiaBan // Tính DonGiaBan
    })
    .ToList();

                listViewThongKeTonKho.ItemsSource = productInventory;
            }
        }
    }
}
