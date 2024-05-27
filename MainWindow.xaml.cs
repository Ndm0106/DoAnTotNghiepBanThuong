using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ViewUC;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoAnTotNghiepBanThuong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _nhanVien;
        private string _chucVu;
        private string _idnhanVien;

        public MainWindow(string nhanVien, string chucVu, string idNhanVien)
        {
            InitializeComponent();
            mainContentControl.Content = new TrangChinhUC();
            _nhanVien = nhanVien;
            _chucVu = chucVu;
            _idnhanVien = idNhanVien;
            HienThiThongTinNguoiDung(_nhanVien, _chucVu);
        }
        private void BtnClick_TrangChinh(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new TrangChinhUC();
        }
        private void BtnClick_KhachHang(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new KhachHangUC();
        }
        private void BtnClick_SanPham(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new SanPhamUC();
        }
        private void BtnClick_NhanVien(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new NguoiDungUC();
        }
        private void BtnClick_NhapHang(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new PhieuNhapUC(_nhanVien, _idnhanVien);
            //_nhanVien, _idnhanVien
        }
        private void BtnClick_BanHang(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new PhieuXuatUC(_nhanVien, _idnhanVien);
            //_nhanVien, _idnhanVien
        }
        private void BtnClick_ThongKe(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new ThongKeUC();
        }
        private void BtnClick_QuanLy(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new QuanLyUC();
        }
        private void BtnClick_DangXuat(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Đăng xuất người dùng
                Logout();
            }
        }
        private void HienThiThongTinNguoiDung(string tenNguoiDung, string chucVu)
        {
            // Hiển thị thông tin người dùng đã đăng nhập lên các Label
            lblTenHienThi.Content = tenNguoiDung;
            lblTenChucVu.Content = chucVu;
        }




        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Logout()
        {
            // Thực hiện các thao tác cần thiết khi đăng xuất, ví dụ như xóa thông tin người dùng khỏi bộ nhớ
            // Tùy thuộc vào cách bạn quản lý trạng thái đăng nhập, bạn có thể cần phải xóa thông tin người dùng ở đây

            // Mở cửa sổ đăng nhập
            var loginWindow = new LoginWindow();
            loginWindow.Show();

            // Đóng cửa sổ hiện tại (MainWindow)
            this.Close();
        }
    }
}