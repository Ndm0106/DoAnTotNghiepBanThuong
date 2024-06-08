using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewUC;
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
using static MaterialDesignThemes.Wpf.Theme;
using XAct.Library.Settings;
using Microsoft.EntityFrameworkCore;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for SuaNhomSanPham.xaml
    /// </summary>
    public partial class SuaNhomSanPham : Window
    {
        private QLQuayThuocBanThuongContext db;
        private NhomSanPham_MLV _NhomSanPham;
        public SuaNhomSanPham(QLQuayThuocBanThuongContext _db, NhomSanPham_MLV nhomsanpham)
        {
            InitializeComponent();
            db = _db;
            _NhomSanPham = nhomsanpham;
            txtSuaIdNhomSanPham.Text = _NhomSanPham.IdNhomSanPham;
            txtSuaTenNhomSanPham.Text = _NhomSanPham.TenNhomSanPham;
            txtSuaGhiChuNhomSanPham.Text = _NhomSanPham.GhiChu;
        }
        private void btnSuaNhomSanPham_Sua(object sender, RoutedEventArgs e)
        {
            string suaTenNSP = txtSuaTenNhomSanPham.Text;
            string suaDiaChiNSP = txtSuaGhiChuNhomSanPham.Text;


            if (string.IsNullOrEmpty(suaTenNSP))
            {
                MessageBox.Show("Không được để trống tên nhóm sản phẩm", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var suaNhomSanPham = db.NhomSanPhams.FirstOrDefault(x => x.IdNhomSanPham == _NhomSanPham.IdNhomSanPham);
            if (suaNhomSanPham != null)
            {
                suaNhomSanPham.TenNhomSanPham = suaTenNSP;
                suaNhomSanPham.GhiChu = suaDiaChiNSP;
                db.SaveChanges();
                LoadDataNhomSanPham();
                MessageBox.Show("Sửa thành công", "Thông báo");
                this.Close();
            }      
        }
        private void LoadDataNhomSanPham()
        {
            var query = (from nsp in db.NhomSanPhams
                         select new NhomSanPham_MLV
                         {
                             IdNhomSanPham = nsp.IdNhomSanPham,
                             TenNhomSanPham = nsp.TenNhomSanPham,
                             GhiChu = nsp.GhiChu,

                         }).ToList();
            NhomSanPhamUC.listView.ItemsSource = query;
            
        }
        private void btnSuaNhomSanPham_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu sửa", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
