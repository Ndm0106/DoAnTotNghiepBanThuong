using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ViewUC;
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
using System.Windows.Shapes;
using XAct.Library.Settings;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for ThemKhachHang.xaml
    /// </summary>
    public partial class ThemKhachHang : Window
    {
        private QLQuayThuocBanThuongContext _dbContext;
        public ThemKhachHang(QLQuayThuocBanThuongContext _db)
        {
            InitializeComponent();
            _dbContext = _db;
            // Sinh GUID mới
            Guid guid = Guid.NewGuid();

            // Tạo một đối tượng Random
            Random random = new Random();

            // Sinh số ngẫu nhiên từ 0 đến 999 và định dạng nó thành chuỗi có 3 chữ số, sau đó thêm vào cuối chuỗi "SP000"
            string randomDigits = random.Next(0, 1000).ToString("D3");

            // Kết hợp chuỗi "SP000" với chuỗi số ngẫu nhiên
            string result = $"SP000{randomDigits}";

            txtThemIdKhachHang.Text = result;
        }
        private void btnThemKhachHang_Them(object sender, RoutedEventArgs e)
        {
            string tenKH = txtThemTenKhachHang.Text.Trim();
            string diachiKH = txtThemDiaChiKhachHang.Text.Trim();

            string sodienthoaiKH = txtThemSoDienThoaiKhachHang.Text.Trim();
            string emailKH = txtThemEmailKhachHang.Text.Trim();
            var TENKH = _dbContext.KhachHangs.Any(x => x.TenKhachHang == tenKH);
            if (string.IsNullOrEmpty(tenKH))
            {
                MessageBox.Show("Tên khách hàng không đc để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TENKH)
            {
                MessageBox.Show("Đã tồn tại khách hàng này", "Thông báo", MessageBoxButton.OK);
                return;
            }
            KhachHang khachhang = new KhachHang
            {
                TenKhachHang = tenKH,
                DiaChi = diachiKH,

                SoDienThoai = sodienthoaiKH,
                Email = emailKH
            };
            _dbContext.KhachHangs.Add(khachhang);
            _dbContext.SaveChanges();
            KhachHangUC.listView.ItemsSource = _dbContext.KhachHangs.ToList();
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
            this.Close();
        }
        private void btnThemKhachHang_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu nhập", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
