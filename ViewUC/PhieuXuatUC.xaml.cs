using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewWindow;
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
    /// Interaction logic for PhieuXuatUC.xaml
    /// </summary>
    public partial class PhieuXuatUC : UserControl
    {
        public QLQuayThuocBanThuongContext db;
        public static System.Windows.Controls.ListView? listView;
        private string idNhanVien;
        private string tenNhanVien;
        public PhieuXuatUC(string idNhanVien, string tenNhanVien)
        {
            InitializeComponent();
            db = new QLQuayThuocBanThuongContext();
            LoadDataPhieuXuat();
            this.tenNhanVien = tenNhanVien;
            this.idNhanVien = idNhanVien;
        }
        private void LoadDataPhieuXuat()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA = db.DonBanHangs.Select(
                    dbh => new DonBanHang_MLV
                    {
                        IdDonBanHang = dbh.IdDonBanHang,
                        NgayBan = dbh.NgayBan,
                        TenKhachHang = dbh.IdKhachHangNavigation.TenKhachHang,
                        SoDienThoai = dbh.IdKhachHangNavigation.SoDienThoai,
                        TenHienThi = dbh.IdNhanVienNavigation.TenHienThi,
                        TongTienDonBanHang = db.ChiTietDonBanHangs.
                        Where(ctdbh => ctdbh.IdDonBanHang == dbh.IdDonBanHang).
                        Sum(ctdbh => ctdbh.SoLuongBan * ctdbh.IdSanPhamNavigation.GiaBan)
                    }
                    ).ToList();
                listViewPhieuXuat.ItemsSource = queryDATA;
                listView = listViewPhieuXuat;
            }
        }

        private void ThemPhieuXuatWindow_Click(object sender, RoutedEventArgs e)
        {
            var themDonBanHang = new ThemDonBanHang(tenNhanVien, idNhanVien);
            themDonBanHang.Show();
        }

        private void SuaPhieuXuatWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void XoaPhieuXuat_Click(object sender, RoutedEventArgs e)
        {
            using (var _db = new QLQuayThuocBanThuongContext())
            {
                var button = sender as System.Windows.Controls.Button;
                if (button != null)
                {
                    // Lấy sản phẩm được chọn từ ListView
                    var dobanhang = (DonBanHang_MLV)button.DataContext;
                    MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá đơn này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        // Xóa sản phẩm từ bảng SanPhams
                        var itemToRemove = _db.DonBanHangs.FirstOrDefault(s => s.IdDonBanHang == dobanhang.IdDonBanHang);
                        if (itemToRemove != null)
                        {
                            _db.DonBanHangs.Remove(itemToRemove);
                            _db.SaveChanges();
                            LoadDataPhieuXuat(); // Load lại dữ liệu sau khi xóa
                            MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }
    }
}
