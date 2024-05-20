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
    /// Interaction logic for PhieuNhapUC.xaml
    /// </summary>
    public partial class PhieuNhapUC : UserControl
    {
        public static System.Windows.Controls.ListView? listView;
        public List<SanPham> sp = new List<SanPham>();
        //public QLQuayThuocBanThuongContext _db = new QLQuayThuocBanThuongContext();
        private string idNhanVien;
        private string nameNhanVien;
        public PhieuNhapUC(string idNhanVien, string nameNhanVien)
        {
            InitializeComponent();
            LoadDataPhieuNhap();
            this.idNhanVien = idNhanVien;
            this.nameNhanVien = nameNhanVien;
        }
        private void LoadDataPhieuNhap()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA = db.DonNhapHangs.Select(dnh => new DonNhapHang_MLV
                {
                    IdDonNhapHang = dnh.IdDonNhapHang,
                    TenHienThi = dnh.IdNhanVienNavigation.TenHienThi,
                    NgayNhap = dnh.NgayNhap,
                    IdNhaPhanPhoi = dnh.IdNhaPhanPhoi,
                    IdNhanVien = dnh.IdNhanVien,
                    TenNhaPhanPhoi = dnh.IdNhaPhanPhoiNavigation.TenNhaPhanPhoi,
                    TongTienDonNhapHang = db.ChiTietDonNhapHangs
                        .Where(ctdnh => ctdnh.IdDonNhapHang == dnh.IdDonNhapHang)
                        .Sum(ctdnh => ctdnh.SoLuongNhap * ctdnh.IdSanPhamNavigation.GiaNhap)
                }).ToList();

                listViewPhieuNhap.ItemsSource = queryDATA;
                //listView = listViewPhieuNhap;
            }
        }
        private void ThemPhieuNhapWindow_Click(object sender, RoutedEventArgs e)
        {

            var themDonNhapHang = new ThemDonNhapHang(sp, idNhanVien, nameNhanVien);
            themDonNhapHang.Show();
            LoadDataPhieuNhap();
        }
        private void SuaPhieuNhapWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                DonNhapHang_MLV selectedDonNhapHang = button.DataContext as DonNhapHang_MLV;
                if (selectedDonNhapHang != null)
                {
                    // Tạo một cửa sổ mới để hiển thị chi tiết đơn nhập hàng
                    SuaDonNhapHang detailWindow = new SuaDonNhapHang(selectedDonNhapHang);
                    detailWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn hàng.");
                }
            }
        }
        private void XoaPhieuNhap_Click(object sender, RoutedEventArgs e)
        {
            using (var _db = new QLQuayThuocBanThuongContext())
            {
                var button = sender as System.Windows.Controls.Button;
                if (button != null)
                {
                    // Lấy sản phẩm được chọn từ ListView
                    var donhaphang = (DonNhapHang_MLV)button.DataContext;
                    MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá đơn này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        // Xóa sản phẩm từ bảng SanPhams
                        var itemToRemove = _db.DonNhapHangs.FirstOrDefault(s => s.IdDonNhapHang == donhaphang.IdDonNhapHang);
                        if (itemToRemove != null)
                        {
                            _db.DonNhapHangs.Remove(itemToRemove);
                            _db.SaveChanges();
                            LoadDataPhieuNhap(); // Load lại dữ liệu sau khi xóa
                            MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }

        private void btnPhieuNhap_Reload_Click(object sender, RoutedEventArgs e)
        {
            LoadDataPhieuNhap();
        }
    }
}
