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
using XAct.Library.Settings;
using XAct.Users;

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for SanPhamUC.xaml
    /// </summary>
    public partial class SanPhamUC : UserControl
    {
        private QLQuayThuocBanThuongContext db;
        public static System.Windows.Controls.ListView? listView;
        public SanPhamUC()
        {
            InitializeComponent();
            db = new QLQuayThuocBanThuongContext();
            
            LoadDataSanPham();
           
        }
        private void LoadDataSanPham()
        {
            var queryDATA = (from sp in db.SanPhams
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
                                 GiaBan = sp.GiaBan,
                                 GiaNhap = sp.GiaNhap,
                                 ThanhPhan = sp.ThanhPhan,
                                 HamLuong = sp.HamLuong,
                                 TenNhomSanPham = sp.IdNhomSanPhamNavigation.TenNhomSanPham,
                                 HanSuDung = sp.HanSuDung,
                                 GhiChu = sp.GhiChu
                             }).Where(x=>x.SoLuongTon>0).ToList();
            listViewSanPham.ItemsSource = queryDATA;
            listView = listViewSanPham;
        }
        private void ThemSanPhanMoiWindow_Click(object sender, RoutedEventArgs e)
        {
            var themSanPham = new ThemSanPham(db);
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
                bool isUsedInCTDNH = db.ChiTietDonNhapHangs.Any(s => s.IdSanPham == sanpham.IdSanPham);
                bool isUsedInCTDBH = db.ChiTietDonBanHangs.Any(s => s.IdSanPham == sanpham.IdSanPham);
                if (isUsedInCTDNH || isUsedInCTDBH)
                {
                    MessageBox.Show("Không thể xoá sản phẩm này vì nó đã được sử dụng trong ít nhất một đơn nhập hàng hoặc đơn bán hàng.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá sản phẩm này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        var itemToRemove = db.SanPhams.FirstOrDefault(s => s.IdSanPham == sanpham.IdSanPham);
                        if (itemToRemove != null)
                        {
                            db.SanPhams.Remove(itemToRemove);
                            db.SaveChanges();
                            LoadDataSanPham(); // Load lại dữ liệu sau khi xóa
                            MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }
        
        private void txtTimKiemSanPham_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtTimKiemSanPham.Text.Trim().ToLower();

            // Nếu không có ký tự nào trong ô tìm kiếm, hiển thị tất cả các đơn vị
            if (string.IsNullOrEmpty(searchText))
            {
                listViewSanPham.ItemsSource = db.SanPhams.Select(sanPham => new SanPham_MLV
                {
                    IdDonVi = sanPham.IdDonVi,
                    IdNhaSanXuat = sanPham.IdNhaSanXuat,
                    IdNhomSanPham = sanPham.IdNhomSanPham,
                    IdSanPham = sanPham.IdSanPham,
                    TenSanPham = sanPham.TenSanPham,
                    TenDonVi = sanPham.IdDonViNavigation.TenDonVi,
                    TenNhaSanXuat = sanPham.IdNhaSanXuatNavigation.TenNhaSanXuat,
                    SoLuongTon = sanPham.SoLuongTon,
                    GiaBan = sanPham.GiaBan,
                    GiaNhap = sanPham.GiaNhap,
                    ThanhPhan = sanPham.ThanhPhan,
                    HamLuong = sanPham.HamLuong,
                    TenNhomSanPham = sanPham.IdNhomSanPhamNavigation.TenNhomSanPham,
                    HanSuDung = sanPham.HanSuDung,
                }).ToList();
            }
            else
            {
                // Lọc danh sách các đơn vị dựa trên từ khóa tìm kiếm
                var filteredSanPhamList = db.SanPhams.Where(sanPham => sanPham.TenSanPham.ToLower().Contains(searchText))
                                                   .Select(sanPham => new SanPham_MLV
                                                   {
                                                       IdDonVi = sanPham.IdDonVi,
                                                       IdNhaSanXuat = sanPham.IdNhaSanXuat,
                                                       IdNhomSanPham = sanPham.IdNhomSanPham,
                                                       IdSanPham = sanPham.IdSanPham,
                                                       TenSanPham = sanPham.TenSanPham,
                                                       TenDonVi = sanPham.IdDonViNavigation.TenDonVi,
                                                       TenNhaSanXuat = sanPham.IdNhaSanXuatNavigation.TenNhaSanXuat,
                                                       SoLuongTon = sanPham.SoLuongTon,
                                                       GiaBan = sanPham.GiaBan,
                                                       GiaNhap = sanPham.GiaNhap,
                                                       ThanhPhan = sanPham.ThanhPhan,
                                                       HamLuong = sanPham.HamLuong,
                                                       TenNhomSanPham = sanPham.IdNhomSanPhamNavigation.TenNhomSanPham,
                                                       HanSuDung = sanPham.HanSuDung,
                                                   }).ToList();

                // Hiển thị danh sách các đơn vị được lọc
                listViewSanPham.ItemsSource = filteredSanPhamList;
            }
        }
    }
}
