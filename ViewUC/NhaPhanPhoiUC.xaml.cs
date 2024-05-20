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
    /// Interaction logic for NhaPhanPhoiUC.xaml
    /// </summary>
    public partial class NhaPhanPhoiUC : UserControl
    {
        private QLQuayThuocBanThuongContext _db;
        public static System.Windows.Controls.ListView? listView;
        public NhaPhanPhoiUC()
        {
            InitializeComponent();
            _db = new QLQuayThuocBanThuongContext();
            LoadDataNhaPhanPhoi();
        }
        private void LoadDataNhaPhanPhoi()
        {
            listViewNhaPhanPhoi.ItemsSource = _db.NhaPhanPhois.ToList();
            listView = listViewNhaPhanPhoi;
        }
        private void ThemNhaPhanPhoiWindow_Click(object sender, RoutedEventArgs e)
        {
            var themNhaPhanPhoi = new ThemNhaPhanPhoi(_db);
            themNhaPhanPhoi.Show();
            LoadDataNhaPhanPhoi();
        }
        private void SuaNhaPhanPhoiWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                NhaPhanPhoi NhaPhanPhoi = (NhaPhanPhoi)button.DataContext;
                if (NhaPhanPhoi != null)
                {
                    var suaNhaPhanPhoi = new SuaNhaPhanPhoi(_db, NhaPhanPhoi);
                    suaNhaPhanPhoi.Show();
                    LoadDataNhaPhanPhoi();
                }
            }
        }
        private void XoaNhaPhanPhoi_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var NhaPhanPhoi = (NhaPhanPhoi)button.DataContext;
                if (NhaPhanPhoi != null)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá nhà cung cấp này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        _db.NhaPhanPhois.Remove(NhaPhanPhoi);
                        _db.SaveChanges();
                        LoadDataNhaPhanPhoi();
                    }
                }
            }
        }
    }
}
