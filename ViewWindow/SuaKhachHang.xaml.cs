using DoAnTotNghiepBanThuong.Model;
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
    /// Interaction logic for SuaKhachHang.xaml
    /// </summary>
    public partial class SuaKhachHang : Window
    {
        private QLQuayThuocBanThuongContext _dbContext;
        private KhachHang _KhachHang;
        public SuaKhachHang(QLQuayThuocBanThuongContext db, KhachHang khachhang)
        {
            InitializeComponent();
            _dbContext = db;
            _KhachHang = khachhang;
            txtSuaIdKhachHang.Text = _KhachHang.IdKhachHang;
            txtSuaTenKhachHang.Text = _KhachHang.TenKhachHang;
            txtSuaDiaChiKhachHang.Text = _KhachHang.DiaChi;
            txtSuaSoDienThoaiKhachHang.Text = _KhachHang.SoDienThoai;
            txtSuaEmailKhachHang.Text = _KhachHang.Email;
        }
        private void btnSuaKhachHang_Sua(object sender, RoutedEventArgs e)
        {
            string suaTenKH = txtSuaTenKhachHang.Text;
            string suaDiaChiKH = txtSuaDiaChiKhachHang.Text;
            string suaSoDienThoaiKH = txtSuaSoDienThoaiKhachHang.Text;

            string suaEmailKH = txtSuaEmailKhachHang.Text;

            if (string.IsNullOrEmpty(suaTenKH))
            {
                MessageBox.Show("Không được để trống tên khách hàng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _KhachHang.TenKhachHang = suaTenKH;
            _KhachHang.DiaChi = suaDiaChiKH;
            _KhachHang.SoDienThoai = suaSoDienThoaiKH;

            _KhachHang.Email = suaEmailKH;
            _dbContext.SaveChanges();
            KhachHangUC.listView.ItemsSource = _dbContext.KhachHangs.ToList();
            MessageBox.Show("Sửa thành công", "Thông báo");
            this.Close();
        }
        private void btnSuaKhachHang_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu sửa", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();

        }
    }
}
