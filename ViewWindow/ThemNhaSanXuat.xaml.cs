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

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for ThemNhaSanXuat.xaml
    /// </summary>
    public partial class ThemNhaSanXuat : Window
    {
        private QLQuayThuocBanThuongContext _dbContext;
        public ThemNhaSanXuat(QLQuayThuocBanThuongContext _db)
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
            string result = $"NCC000{randomDigits}";
            txtThemIdNhaSanXuat.Text = result;
        }
        private void btnThemNhaSanXuat_Them(object sender, RoutedEventArgs e)
        {
            string tenNCC = txtThemTenNhaSanXuat.Text.Trim();
            string diachiNCC = txtThemDiaChiNhaSanXuat.Text.Trim();
            string faxNCC = txtThemFaxNhaSanXuat.Text.Trim();
            string sodienthoaiNCC = txtThemSoDienThoaiNhaSanXuat.Text.Trim();
            string emailNCC = txtThemEmailNhaSanXuat.Text.Trim();
            var queryNCC = _dbContext.NhaSanXuats.Any(x => x.TenNhaSanXuat == tenNCC);
            if (string.IsNullOrEmpty(tenNCC))
            {
                MessageBox.Show("Tên nhà cung cấp không đc để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (queryNCC)
            {
                MessageBox.Show("Đã tồn tại nhà cung cấp này", "Thông báo", MessageBoxButton.OK);
                return;
            }

            NhaSanXuat NhaSanXuat = new NhaSanXuat
            {
                IdNhaSanXuat = txtThemIdNhaSanXuat.Text,
                TenNhaSanXuat = tenNCC,
                DiaChi = diachiNCC,
                Fax = faxNCC,
                SoDienThoai = sodienthoaiNCC,
                Email = emailNCC
            };
            _dbContext.NhaSanXuats.Add(NhaSanXuat);
            _dbContext.SaveChanges();
            NhaSanXuatUC.listView.ItemsSource = _dbContext.NhaSanXuats.ToList();
            //NhaSanXuatUC.listView.ItemsSource = _dbContext.NhaSanXuats.ToList();
            MessageBox.Show("Nhà cung cấp mới đã được thêm thành công", "Thông báo", MessageBoxButton.OK);
            this.Close();
        }
        private void btnThemNhaSanXuat_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu nhập", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
