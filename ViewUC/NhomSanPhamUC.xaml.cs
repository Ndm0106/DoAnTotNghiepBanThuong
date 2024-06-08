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
    /// Interaction logic for NhomSanPhamUC.xaml
    /// </summary>
    public partial class NhomSanPhamUC : UserControl
    {
        private QLQuayThuocBanThuongContext _db;
        public static System.Windows.Controls.ListView? listView;
        public NhomSanPhamUC()
        {
            InitializeComponent();
            _db = new QLQuayThuocBanThuongContext();
            LoadDataNhomSanPham();
        }
        private void LoadDataNhomSanPham()
        {
            var query = (from nsp in _db.NhomSanPhams
                         select new NhomSanPham_MLV
                         {
                             IdNhomSanPham = nsp.IdNhomSanPham,
                             TenNhomSanPham = nsp.TenNhomSanPham,
                             GhiChu = nsp.GhiChu,

                         }).ToList();
            listViewNhomSanPham.ItemsSource = query;
            listView = listViewNhomSanPham;
        }
        private void ThemNhomSanPhamWindow_Click(object sender, RoutedEventArgs e)
        {
            var themNhomSanPham = new ThemNhomSanPham(_db);
            themNhomSanPham.Show();
            LoadDataNhomSanPham();
        }
        private void SuaNhomSanPhamWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var nhomsanpham = (NhomSanPham_MLV)button.DataContext;
                var suaNhomSanPham = new SuaNhomSanPham(_db, nhomsanpham);
                suaNhomSanPham.Show();
                LoadDataNhomSanPham();
            }

        }
        private void XoaNhomSanPham_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var nhomsanpham = (NhomSanPham_MLV)button.DataContext;
                if (nhomsanpham != null)
                {
                    bool isUsedInProducts = _db.SanPhams.Any(s => s.IdNhomSanPham == nhomsanpham.IdNhomSanPham);
                    if (isUsedInProducts)
                    {
                        MessageBox.Show("Không thể xoá nhóm sản này vì nó đã được sử dụng trong ít nhất một sản phẩm.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá nhóm sản phẩm này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            var itemToRemove = _db.NhomSanPhams.FirstOrDefault(s => s.IdNhomSanPham == nhomsanpham.IdNhomSanPham);
                            if (itemToRemove != null)
                            {
                                _db.NhomSanPhams.Remove(itemToRemove);
                                _db.SaveChanges();
                                LoadDataNhomSanPham();
                            }    
                        }
                    }
                }

            }
        }

        private void txtNhomSanPham_TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtNhomSanPham_TimKiem.Text.Trim().ToLower();

            // Nếu không có ký tự nào trong ô tìm kiếm, hiển thị tất cả các đơn vị
            if (string.IsNullOrEmpty(searchText))
            {
                listViewNhomSanPham.ItemsSource = _db.NhomSanPhams.Select(nhomSanPham => new NhomSanPham_MLV
                {
                    IdNhomSanPham = nhomSanPham.IdNhomSanPham,
                    TenNhomSanPham = nhomSanPham.TenNhomSanPham,
                    GhiChu = nhomSanPham.GhiChu,
                }).ToList();
            }
            else
            {
                // Lọc danh sách các đơn vị dựa trên từ khóa tìm kiếm
                var filteredDonViList = _db.NhomSanPhams.Where(nhomSanPham => nhomSanPham.TenNhomSanPham.ToLower().Contains(searchText))
                                                   .Select(nhomSanPham => new NhomSanPham_MLV
                                                   {
                                                       IdNhomSanPham = nhomSanPham.IdNhomSanPham,
                                                       TenNhomSanPham = nhomSanPham.TenNhomSanPham,
                                                       GhiChu = nhomSanPham.GhiChu,
                                                   }).ToList();

                // Hiển thị danh sách các đơn vị được lọc
                listViewNhomSanPham.ItemsSource = filteredDonViList;
            }
        }
    }
}
