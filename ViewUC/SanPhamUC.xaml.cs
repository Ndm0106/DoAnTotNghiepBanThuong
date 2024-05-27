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
    /// Interaction logic for SanPhamUC.xaml
    /// </summary>
    public partial class SanPhamUC : UserControl
    {
        private QLQuayThuocBanThuongContext _db;
        public static System.Windows.Controls.ListView? listView;
        public SanPhamUC()
        {
            InitializeComponent();
            _db = new QLQuayThuocBanThuongContext();

            LoadDataSanPham();
        }
        private void LoadDataSanPham()
        {
            var queryDATA = (from sp in _db.SanPhams
                             select new SanPham_MLV
                             {
                                 IdDonVi = sp.IdDonVi,
                                 IdNhaSanXuat = sp.IdNhaSanXuat,
                                 IdNhomSanPham = sp.IdNhomSanPham,
                                 IdSanPham = sp.IdSanPham,
                                 TenSanPham = sp.TenSanPham,
                                 TenDonVi = sp.IdDonViNavigation.TenDonVi,
                                 TenNhaSanXuat = sp.IdNhaSanXuatNavigation.TenNhaSanXuat,
                                 SoLuongTon = sp.SoLuongTon,
                                 GiaBanLe = sp.GiaBan,
                                 GiaNhap = sp.GiaNhap,
                                 ThanhPhan = sp.ThanhPhan,
                                 HamLuong = sp.HamLuong,
                                 TenNhomSanPham = sp.IdNhomSanPhamNavigation.TenNhomSanPham,
                                 HanSuDung = sp.HanSuDung,
                                 
                                 GhiChu = sp.GhiChu
                             }).ToList();
            listViewSanPham.ItemsSource = queryDATA;
            listView = listViewSanPham;

        }
        private void ThemSanPhanMoiWindow_Click(object sender, RoutedEventArgs e)
        {
            var themSanPham = new ThemSanPham(_db);
            themSanPham.Show();
            LoadDataSanPham();
        }
        private void SuaSanPhamWindow_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var sanpham = (SanPham_MLV)button.DataContext;
                var suaSanPham = new SuaSanPham(sanpham);
                suaSanPham.Show();
                LoadDataSanPham();
            }


        }
        private void XoaSanPham_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                // Lấy sản phẩm được chọn từ ListView
                var sanpham = (SanPham_MLV)button.DataContext;
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá sản phẩm này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    // Xóa sản phẩm từ bảng SanPhams
                    var itemToRemove = _db.SanPhams.FirstOrDefault(s => s.IdSanPham == sanpham.IdSanPham);
                    if (itemToRemove != null)
                    {
                        _db.SanPhams.Remove(itemToRemove);
                        _db.SaveChanges();
                        LoadDataSanPham(); // Load lại dữ liệu sau khi xóa
                        MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButton.OK);
                    }
                }
            }
        }

    }
}
