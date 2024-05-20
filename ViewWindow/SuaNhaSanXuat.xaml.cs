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
using XAct.Library.Settings;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for SuaNhaSanXuat.xaml
    /// </summary>
    public partial class SuaNhaSanXuat : Window
    {
        private QLQuayThuocBanThuongContext _dbContext;
        private NhaSanXuat _NhaSanXuat;
        public SuaNhaSanXuat(QLQuayThuocBanThuongContext db, NhaSanXuat NhaSanXuat)
        {
            InitializeComponent();
            _dbContext = db;
            _NhaSanXuat = NhaSanXuat;
            txtSuaIdNhaSanXuat.Text = _NhaSanXuat.IdNhaSanXuat;
            txtSuaTenNhaSanXuat.Text = _NhaSanXuat.TenNhaSanXuat;
            txtSuaDiaChiNhaSanXuat.Text = _NhaSanXuat.DiaChi;
            txtSuaSoDienThoaiNhaSanXuat.Text = _NhaSanXuat.SoDienThoai;
            txtSuaEmailNhaSanXuat.Text = _NhaSanXuat.Email;
            txtSuaSoFaxNhaSanXuat.Text = _NhaSanXuat.Fax;
        }
        private void btnSuaNhaSanXuat_Sua(object sender, RoutedEventArgs e)
        {
            string suaTenNCC = txtSuaTenNhaSanXuat.Text.Trim();
            string suaDiaChiNCC = txtSuaDiaChiNhaSanXuat.Text;
            string suaSoDienThoaiNCC = txtSuaSoDienThoaiNhaSanXuat.Text;
            string suaSoFaxNCC = txtSuaSoFaxNhaSanXuat.Text;
            string suaEmailNCC = txtSuaEmailNhaSanXuat.Text;

            if (string.IsNullOrEmpty(suaTenNCC))
            {
                MessageBox.Show("Không được để trống tên nhà cung cấp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _NhaSanXuat.TenNhaSanXuat = suaTenNCC;
            _NhaSanXuat.DiaChi = suaDiaChiNCC;
            _NhaSanXuat.SoDienThoai = suaSoDienThoaiNCC;
            _NhaSanXuat.Fax = suaSoFaxNCC;
            _NhaSanXuat.Email = suaEmailNCC;
            _dbContext.SaveChanges();
            NhaSanXuatUC.listView.ItemsSource = _dbContext.NhaSanXuats.ToList();
            MessageBox.Show("Sửa thành công", "Thông báo");
            this.Close();
        }
        private void btnSuaNhaSanXuat_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu sửa", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
