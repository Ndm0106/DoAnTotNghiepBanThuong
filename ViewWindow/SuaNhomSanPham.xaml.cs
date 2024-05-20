using DoAnTotNghiepBanThuong.Model;
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

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for SuaNhomSanPham.xaml
    /// </summary>
    public partial class SuaNhomSanPham : Window
    {
        private QLQuayThuocBanThuongContext _dbContext;
        private NhomSanPham _NhomSanPham;
        public SuaNhomSanPham(QLQuayThuocBanThuongContext db, NhomSanPham nhomsanpham)
        {
            InitializeComponent();
            _dbContext = db;
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
            _NhomSanPham.TenNhomSanPham = suaTenNSP;
            _NhomSanPham.GhiChu = suaDiaChiNSP;
            _dbContext.SaveChanges();
            NhomSanPhamUC.listView.ItemsSource = _dbContext.NhomSanPhams.ToList();
            MessageBox.Show("Sửa thành công", "Thông báo");
            this.Close();
        }
        private void btnSuaNhomSanPham_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu sửa", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
