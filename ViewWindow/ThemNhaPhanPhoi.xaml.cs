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
    /// Interaction logic for ThemNhaPhanPhoi.xaml
    /// </summary>
    public partial class ThemNhaPhanPhoi : Window
    {
        private QLQuayThuocBanThuongContext _dbContext;
        public ThemNhaPhanPhoi(QLQuayThuocBanThuongContext _db)
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
            string result = $"NPP000{randomDigits}";
            txtThemIdNhaPhanPhoi.Text = result;
        }
        private void btnThemNhaPhanPhoi_Them(object sender, RoutedEventArgs e)
        {
            string tenNPP = txtThemTenNhaPhanPhoi.Text.Trim();
            string diachiNPP = txtThemDiaChiNhaPhanPhoi.Text.Trim();
            string faxNPP = txtThemFaxNhaPhanPhoi.Text.Trim();
            string sodienthoaiNPP = txtThemSoDienThoaiNhaPhanPhoi.Text.Trim();
            string emailNPP = txtThemEmailNhaPhanPhoi.Text.Trim();
            string masothueNPP = txtThemEmailNhaPhanPhoi.Text.Trim();
            var queryNPP = _dbContext.NhaPhanPhois.Any(x => x.TenNhaPhanPhoi == tenNPP);
            if (string.IsNullOrEmpty(tenNPP))
            {
                MessageBox.Show("Tên nhà cung cấp không đc để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (queryNPP)
            {
                MessageBox.Show("Đã tồn tại nhà cung cấp này", "Thông báo", MessageBoxButton.OK);
                return;
            }

            NhaPhanPhoi NhaPhanPhoi = new NhaPhanPhoi
            {
                IdNhaPhanPhoi = txtThemIdNhaPhanPhoi.Text,
                TenNhaPhanPhoi = tenNPP,
                DiaChi = diachiNPP,
                Fax = faxNPP,
                SoDienThoai = sodienthoaiNPP,
                Email = emailNPP,
                MaSoThue = masothueNPP
            };
            _dbContext.NhaPhanPhois.Add(NhaPhanPhoi);
            _dbContext.SaveChanges();

            NhaPhanPhoiUC.listView.ItemsSource = _dbContext.NhaPhanPhois.ToList();
            MessageBox.Show("Nhà cung cấp mới đã được thêm thành công", "Thông báo", MessageBoxButton.OK);
            this.Close();
        }
        private void btnThemNhaPhanPhoi_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu nhập", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
