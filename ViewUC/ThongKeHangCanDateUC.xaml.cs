﻿using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
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
    /// Interaction logic for ThongKeHangCanDateUC.xaml
    /// </summary>
    public partial class ThongKeHangCanDateUC : UserControl
    {
        public ThongKeHangCanDateUC()
        {
            InitializeComponent();
        }

        private void CkbCanDate_Checked(object sender, RoutedEventArgs e)
        {
            CkbHetHan.IsChecked = false; // Bỏ tick CheckBox "Hàng hết hạn"
            UpdateListView();
        }

        private void CkbCanDate_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateListView();
        }

        private void CkbHetHan_Checked(object sender, RoutedEventArgs e)
        {
            CkbCanDate.IsChecked = false; // Bỏ tick CheckBox "Hàng cận date"
            UpdateListView();
        }

        private void CkbHetHan_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateListView();
        }

        private void UpdateListView()
        {
            var allProducts = GetSanPham();
            List<SanPham_MLV> filteredProducts = new List<SanPham_MLV>();

            if (CkbCanDate.IsChecked == true)
            {
                var canDateThreshold = DateTime.Now.AddMonths(1); // Cận date trong vòng 1 tháng
                filteredProducts = allProducts.Where(p => p.HanSuDung <= canDateThreshold && p.HanSuDung > DateTime.Now).ToList();
            }
            else if (CkbHetHan.IsChecked == true)
            {
                filteredProducts = allProducts.Where(p => p.HanSuDung <= DateTime.Now).ToList();
            }

            LvSanPham.ItemsSource = filteredProducts;
        }

        private List<SanPham_MLV> GetSanPham()
        {
            List<SanPham_MLV> sanPhams = new List<SanPham_MLV>();

            using (var db = new QLQuayThuocBanThuongContext())
            {
                sanPhams = (from sp in db.SanPhams
                            select new SanPham_MLV
                            {
                                IdSanPham = sp.IdSanPham,
                                TenSanPham = sp.TenSanPham,
                                TenDonVi = sp.IdDonViNavigation.TenDonVi,
                                GiaBanLe = sp.GiaBan,
                                HanSuDung = sp.HanSuDung
                            }).ToList();
            }

            return sanPhams;
        }

        
    }
}
