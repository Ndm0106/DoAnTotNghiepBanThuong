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
        public static System.Windows.Controls.ListView? listView;
        private string idNhanVien;
        private string tenNhanVien;
        public PhieuXuatUC(string idNhanVien, string tenNhanVien)
        {
            InitializeComponent();
            LoadDataPhieuXuat();
            this.tenNhanVien = tenNhanVien;
            this.idNhanVien = idNhanVien;
        }
        private void LoadDataPhieuXuat()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA = db.DonBanHangs.Select(dbh => new DonBanHang_MLV
                {
                    IdDonBanHang = dbh.IdDonBanHang,
                    TenHienThi = dbh.IdNhanVienNavigation.TenHienThi,
                    NgayBan = dbh.NgayBan,
                    IdKhachHang = dbh.IdKhachHang,
                    SoDienThoai = dbh.IdKhachHangNavigation.SoDienThoai,
                    IdNhanVien = dbh.IdNhanVien,
                    TenKhachHang = dbh.IdKhachHangNavigation.TenKhachHang,
                    TongTienDonBanHang = dbh.TongTienDonBanHang
                }).ToList();
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
            var button = sender as Button;
            if (button != null)
            {
                DonBanHang_MLV selectedDonBanHang = button.DataContext as DonBanHang_MLV;
                if (selectedDonBanHang != null)
                {
                    // Tạo một cửa sổ mới để hiển thị chi tiết đơn nhập hàng
                    SuaDonBanHang detailWindow = new SuaDonBanHang(selectedDonBanHang);
                    detailWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn hàng.");
                }
            }
        }

        private void XoaPhieuXuat_Click(object sender, RoutedEventArgs e)
        {
            using (var _db = new QLQuayThuocBanThuongContext())
            {
                var button = sender as System.Windows.Controls.Button;
                if (button != null)
                {
                    // Lấy sản phẩm được chọn từ ListView
                    DonBanHang_MLV donbanhang = (DonBanHang_MLV)button.DataContext;
                    MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá đơn này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        var relatedRecords = _db.ChiTietDonBanHangs.Where(x => x.IdDonBanHang == donbanhang.IdDonBanHang);
                        _db.ChiTietDonBanHangs.RemoveRange(relatedRecords);
                        
                        var itemToRemove = _db.DonBanHangs.FirstOrDefault(s => s.IdDonBanHang == donbanhang.IdDonBanHang);
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

        private void btnPhieuXuat_Reload_Click(object sender, RoutedEventArgs e)
        {
            LoadDataPhieuXuat();
        }
    }
}
