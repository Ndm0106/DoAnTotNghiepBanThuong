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
    /// Interaction logic for ThemDonVi.xaml
    /// </summary>
    public partial class ThemDonVi : Window
    {
        private QLQuayThuocBanThuongContext _dbContext;
        public ThemDonVi(QLQuayThuocBanThuongContext db)
        {
            InitializeComponent();
            _dbContext = db;
            Guid guid = Guid.NewGuid();

            // Tạo một đối tượng Random
            Random random = new Random();

            // Sinh số ngẫu nhiên từ 0 đến 999 và định dạng nó thành chuỗi có 3 chữ số, sau đó thêm vào cuối chuỗi "SP000"
            string randomDigits = random.Next(0, 1000).ToString("D3");

            // Kết hợp chuỗi "SP000" với chuỗi số ngẫu nhiên
            string result = $"DV000{randomDigits}";

            txtIdDonViThem.Text = result;
        }
        private void btnThemDonVi_Luu(object sender, RoutedEventArgs e)
        {
            string tenDV = txtTenDonViThem.Text.Trim();
            var TENDV = _dbContext.DonVis.Any(x => x.TenDonVi == tenDV);
            if (string.IsNullOrEmpty(tenDV))
            {
                MessageBox.Show("Tên đơn vị không đc để trống", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (TENDV)
            {
                MessageBox.Show("Đã tồn tại đơn vị", "Thông báo", MessageBoxButton.OK);
                return;
            }
            // Sinh GUID mới

            DonVi newdonvi = new DonVi
            {
                IdDonVi = txtIdDonViThem.Text,
                TenDonVi = txtTenDonViThem.Text
            };
            _dbContext.DonVis.Add(newdonvi);
            _dbContext.SaveChanges();
            DonViTinhUC.listView.ItemsSource = _dbContext.DonVis.ToList();
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
            this.Close();
        }
        private void btnThemDonVi_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
