using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
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
using static MaterialDesignThemes.Wpf.Theme;
using XAct.Library.Settings;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for ThemNhomSanPham.xaml
    /// </summary>
    public partial class ThemNhomSanPham : Window
    {
        private QLQuayThuocBanThuongContext db;
        public ThemNhomSanPham(QLQuayThuocBanThuongContext _db)
        {
            InitializeComponent();
            db = _db;
            // Sinh GUID mới
            Guid guid = Guid.NewGuid();

            // Tạo một đối tượng Random
            Random random = new Random();

            // Sinh số ngẫu nhiên từ 0 đến 999 và định dạng nó thành chuỗi có 3 chữ số, sau đó thêm vào cuối chuỗi "SP000"
            string randomDigits = random.Next(0, 1000).ToString("D3");

            // Kết hợp chuỗi "SP000" với chuỗi số ngẫu nhiên
            string result = $"NSP000{randomDigits}";
            txtThemIdNhomSanPham.Text = result;
        }
        private void btnThemNhomSanPham_Them(object sender, RoutedEventArgs e)
        {
            string tenNSP = txtThemTenNhomSanPham.Text.Trim();
            string ghichuNSP = txtThemGhiChuNhomSanPham.Text.Trim();

            var queryNSP = db.NhomSanPhams.Any(x => x.TenNhomSanPham == tenNSP);
            if (string.IsNullOrEmpty(tenNSP))
            {
                MessageBox.Show("Tên nhà cung cấp không đc để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (queryNSP)
            {
                MessageBox.Show("Đã tồn tại nhà cung cấp", "Thông báo", MessageBoxButton.OK);
                return;
            }



            NhomSanPham nhomsanpham = new NhomSanPham
            {
                IdNhomSanPham = txtThemIdNhomSanPham.Text,
                TenNhomSanPham = tenNSP,
                GhiChu = ghichuNSP
            };
            db.NhomSanPhams.Add(nhomsanpham);
            db.SaveChanges();
            LoadDataNhomSanPham();
            MessageBox.Show("Nhóm sản phẩm mới đã được thêm thành công", "Thông báo", MessageBoxButton.OK);
            this.Close();
        }
        private void LoadDataNhomSanPham()
        {
            var query = (from nsp in db.NhomSanPhams
                         select new NhomSanPham_MLV
                         {
                             IdNhomSanPham = nsp.IdNhomSanPham,
                             TenNhomSanPham = nsp.TenNhomSanPham,
                             GhiChu = nsp.GhiChu,

                         }).ToList();
            NhomSanPhamUC.listView.ItemsSource = query;
            
        }
        private void btnThemNhomSanPham_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu nhập", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
