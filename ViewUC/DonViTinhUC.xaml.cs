using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using XAct;

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for DonViTinhUC.xaml
    /// </summary>
    public partial class DonViTinhUC : UserControl
    {
        private QLQuayThuocBanThuongContext db;
        
        public static System.Windows.Controls.ListView? listView;
        public DonViTinhUC()
        {
            InitializeComponent();
            db = new QLQuayThuocBanThuongContext();
            LoadDataDonVi();
        }
        private void LoadDataDonVi()
        {
            var query = (from dv in db.DonVis
                         select new DonVi_MLV
                         {
                             IdDonVi = dv.IdDonVi,
                             TenDonVi = dv.TenDonVi

                         }).ToList();
            listViewDonVi.ItemsSource = query;
            listView = listViewDonVi;
            
        }

        private void ThemDonViWindow_Click(object sender, RoutedEventArgs e)
        {

            var themDonVi = new ThemDonVi(db);
            themDonVi.ShowDialog();
            LoadDataDonVi();
            
            
        }

        private void SuaDonViWindow_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var donvi = (DonVi_MLV)button.DataContext;
                var suaDonVi = new SuaDonVi(db, donvi);
                suaDonVi.ShowDialog();
                LoadDataDonVi(); 
            }
        }

        private void XoaDonVi_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var donvi = (DonVi_MLV)button.DataContext;
                if (donvi != null)
                {
                    // Kiểm tra xem đơn vị được chọn có xuất hiện trong bất kỳ sản phẩm nào không
                    bool isUsedInProducts = db.SanPhams.Any(s => s.IdDonVi == donvi.IdDonVi);
                    if (isUsedInProducts)
                    {
                        MessageBox.Show("Không thể xoá đơn vị này vì nó đã được sử dụng trong ít nhất một sản phẩm.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        
                        MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá đơn vị này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            var itemToRemove = db.DonVis.FirstOrDefault(s => s.IdDonVi == donvi.IdDonVi);
                            if (itemToRemove != null)
                            {
                                db.DonVis.Remove(itemToRemove);
                                db.SaveChanges();
                                LoadDataDonVi();
                            }    
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy đơn vị để xoá", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void txtDonVi_TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtDonVi_TimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                listViewDonVi.ItemsSource = db.DonVis.Select(donVi => new DonVi_MLV
                {
                    IdDonVi = donVi.IdDonVi,
                    TenDonVi = donVi.TenDonVi
                }).ToList();
            }
            else
            {
                var filteredDonViList = db.DonVis.Where(donVi => donVi.TenDonVi.ToLower().Contains(searchText))
                                                   .Select(donVi => new DonVi_MLV
                                                   {
                                                       IdDonVi = donVi.IdDonVi,
                                                       TenDonVi = donVi.TenDonVi
                                                   }).ToList();
                listViewDonVi.ItemsSource = filteredDonViList;
            }   
        }
    }
}
