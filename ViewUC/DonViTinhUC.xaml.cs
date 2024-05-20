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
    /// Interaction logic for DonViTinhUC.xaml
    /// </summary>
    public partial class DonViTinhUC : UserControl
    {
        private QLQuayThuocBanThuongContext _db;
        public static System.Windows.Controls.ListView? listView;
        public DonViTinhUC()
        {
            InitializeComponent();
            _db = new QLQuayThuocBanThuongContext();
            LoadDataDonVi();
        }
        private void LoadDataDonVi()
        {

            listViewDonVi.ItemsSource = _db.DonVis.ToList(); ;
            listView = listViewDonVi;
        }

        private void ThemDonViWindow_Click(object sender, RoutedEventArgs e)
        {

            var themDonVi = new ThemDonVi(_db);
            themDonVi.Show();
            LoadDataDonVi();
        }

        private void SuaDonViWindow_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var donvi = (DonVi)button.DataContext;
                var suaDonVi = new SuaDonVi(_db, donvi);
                suaDonVi.Show();
                LoadDataDonVi();
            }
        }

        private void XoaDonVi_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var donvi = (DonVi)button.DataContext;
                if (donvi != null)
                {
                    // Kiểm tra xem đơn vị được chọn có xuất hiện trong bất kỳ sản phẩm nào không
                    bool isUsedInProducts = _db.SanPhams.Any(s => s.IdDonVi == donvi.IdDonVi);
                    if (isUsedInProducts)
                    {
                        MessageBox.Show("Không thể xoá đơn vị này vì nó đã được sử dụng trong ít nhất một sản phẩm.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá đơn vị này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            _db.DonVis.Remove(donvi);
                            _db.SaveChanges();
                            LoadDataDonVi();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy đơn vị để xoá", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
