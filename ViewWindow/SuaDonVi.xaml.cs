using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewUC;
using Microsoft.EntityFrameworkCore;
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
using XAct.Library.Settings;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for SuaDonVi.xaml
    /// </summary>
    public partial class SuaDonVi : Window
    {
        private QLQuayThuocBanThuongContext db;
        private DonVi_MLV _DonVi;
        public SuaDonVi(QLQuayThuocBanThuongContext _db, DonVi_MLV donvi)
        {
            InitializeComponent();
            db = _db;
            _DonVi = donvi;
            txtSuaIdDonVi.Text = _DonVi.IdDonVi;
            txtSuaTenDonVi.Text = _DonVi.TenDonVi;
        }
        private void btnSuaDonVi_Luu(object sender, RoutedEventArgs e)
        {

            string suaTenDonVi = txtSuaTenDonVi.Text;

            if (string.IsNullOrEmpty(suaTenDonVi))
            {
                MessageBox.Show("Không được để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var suaDonVi = db.DonVis.FirstOrDefault(x => x.IdDonVi == _DonVi.IdDonVi);
            if (suaDonVi != null)
            {
                suaDonVi.TenDonVi = suaTenDonVi;
                db.SaveChanges();
                LoadDataDonVi();
                //DonViTinhUC.listView.ItemsSource = db.DonVis.ToList();
                MessageBox.Show("Đã sửa thành công", "Thông báo", MessageBoxButton.OK);
                this.Close();
            }
            
        }
        private void LoadDataDonVi()
        {
            var query = (from dv in db.DonVis
                         select new DonVi_MLV
                         {
                             IdDonVi = dv.IdDonVi,
                             TenDonVi = dv.TenDonVi

                         }).ToList();
            DonViTinhUC.listView.ItemsSource = query;

        }
        private void btnSuaDonVi_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
