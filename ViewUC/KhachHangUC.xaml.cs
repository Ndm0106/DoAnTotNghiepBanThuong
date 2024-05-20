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
    /// Interaction logic for KhachHangUC.xaml
    /// </summary>
    public partial class KhachHangUC : UserControl
    {
        private QLQuayThuocBanThuongContext _db;
        public static System.Windows.Controls.ListView? listView;
        public KhachHangUC()
        {
            InitializeComponent();
            _db = new QLQuayThuocBanThuongContext();
            LoadDataNhaCungCap();
        }
        private void LoadDataNhaCungCap()
        {
            listViewKhachHang.ItemsSource = _db.KhachHangs.ToList();
            listView = listViewKhachHang;
        }
        private void ThemKhachHangWindow_Click(object sender, RoutedEventArgs e)
        {
            var themKhachHang = new ThemKhachHang(_db);
            themKhachHang.Show();
            LoadDataNhaCungCap();
        }
        private void SuaKhachHangWindow_Click(object sender, RoutedEventArgs e)
        {
            KhachHang selectedKH = (KhachHang)listViewKhachHang.SelectedItem;
            if (selectedKH == null)
            {
                MessageBox.Show("Chọn một nhà cung cấp để sửa", "Thông báo", MessageBoxButton.OK);
            }
            else
            {
                var button = sender as System.Windows.Controls.Button;
                var khachhang = button.DataContext as KhachHang;
                var suaKhachHang = new SuaKhachHang(_db, khachhang);
                suaKhachHang.Show();
                LoadDataNhaCungCap();
            }
        }
        private void XoaKhachHang_Click(object sender, RoutedEventArgs e)
        {
            KhachHang selectedKH = (KhachHang)listViewKhachHang.SelectedItem;
            if (selectedKH == null)
            {
                MessageBox.Show("Cần chọn một nhà cung cấp để xoá", "Thông báo", MessageBoxButton.OK);
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá khách hàng này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _db.KhachHangs.Remove(selectedKH);
                _db.SaveChanges();
                LoadDataNhaCungCap();
            }
        }
    }
}
