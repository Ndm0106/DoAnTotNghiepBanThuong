using DoAnTotNghiepBanThuong.Model;
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
            listViewNhomSanPham.ItemsSource = _db.NhomSanPhams.ToList();
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
                var nhomsanpham = (NhomSanPham)button.DataContext;
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
                var nhomsanpham = (NhomSanPham)button.DataContext;
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
                            _db.NhomSanPhams.Remove(nhomsanpham);
                            _db.SaveChanges();
                            LoadDataNhomSanPham();
                        }
                    }
                }

            }
        }
    }
}
