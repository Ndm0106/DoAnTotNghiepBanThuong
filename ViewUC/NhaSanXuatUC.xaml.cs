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
    /// Interaction logic for NhaSanXuatUC.xaml
    /// </summary>
    public partial class NhaSanXuatUC : UserControl
    {
        private QLQuayThuocBanThuongContext _db;
        public static System.Windows.Controls.ListView? listView;
        public NhaSanXuatUC()
        {
            InitializeComponent();
            _db = new QLQuayThuocBanThuongContext();
            LoadDataNhaSanXuat();
        }
        private void LoadDataNhaSanXuat()
        {
            listViewNhaSanXuat.ItemsSource = _db.NhaSanXuats.ToList();
            listView = listViewNhaSanXuat;
        }
        private void ThemNhaSanXuatWindow_Click(object sender, RoutedEventArgs e)
        {
            var themNhaSanXuat = new ThemNhaSanXuat(_db);
            themNhaSanXuat.Show();
            LoadDataNhaSanXuat();
        }
        private void SuaNhaSanXuatWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                NhaSanXuat NhaSanXuat = (NhaSanXuat)button.DataContext;
                if (NhaSanXuat != null)
                {
                    var suaNhaSanXuat = new SuaNhaSanXuat(_db, NhaSanXuat);
                    suaNhaSanXuat.Show();
                    LoadDataNhaSanXuat();
                }
            }
        }
        private void XoaNhaSanXuat_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var NhaSanXuat = (NhaSanXuat)button.DataContext;
                if (NhaSanXuat != null)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá nhà cung cấp này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        _db.NhaSanXuats.Remove(NhaSanXuat);
                        _db.SaveChanges();
                        LoadDataNhaSanXuat();
                    }
                }
            }
        }
    }
}
