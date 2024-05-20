using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
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
    /// Interaction logic for NguoiDungUC.xaml
    /// </summary>
    public partial class NguoiDungUC : UserControl
    {
        private QLQuayThuocBanThuongContext _db;
        public static System.Windows.Controls.ListView? listView;
        public NguoiDungUC()
        {
            InitializeComponent();
            LoadDataNhanVien();
            _db = new QLQuayThuocBanThuongContext();
        }
        private void LoadDataNhanVien()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA = db.NhanViens.Select(nv => new NhanVien_MLV
                {
                    TenHienThi = nv.TenHienThi,
                    TenChucVu = nv.IdChucVuNavigation.TenChucVu,
                    TaiKhoan = nv.TaiKhoan,
                    MatKhau = nv.MatKhau,
                    SoDienThoai = nv.SoDienThoai,
                    Email = nv.Email,
                }).ToList();
                listViewNhanVien.ItemsSource = queryDATA;
                listView = listViewNhanVien;
            }

        }

        private void ThemNhanVienWindow_Click(object sender, RoutedEventArgs e)
        {
            var themNhanVien = new ThemNhanVien(_db);
            themNhanVien.Show();
            LoadDataNhanVien();
        }

        private void SuaNhanVienWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var nhanvien = (NhanVien_MLV)button.DataContext;
                var suaNhanVien = new SuaNhanVien(nhanvien);
                suaNhanVien.Show();
                LoadDataNhanVien();
            }
            else
            {
                MessageBox.Show("Cần chọn một sản phẩm để sửa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void XoaNhanVien_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var nhanvien = (NhanVien_MLV)button.DataContext;
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá tài khoản này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var deleteNV = _db.NhanViens.FirstOrDefault(x => x.TenHienThi == nhanvien.TenHienThi);
                    _db.NhanViens.Remove(deleteNV);
                    _db.SaveChanges();
                    LoadDataNhanVien();
                    MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButton.OK);
                }
            }
        }

        private void CapQuyenNhanVien_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DoiMatKhauNhanVienWindow_Click(object sender, RoutedEventArgs e)
        {
            NhanVien_MLV selectedNV = (NhanVien_MLV)listViewNhanVien.SelectedItem;
            if (selectedNV != null)
            {
                var capnhatMatKhau = new CapNhatMatKhau(selectedNV);
                capnhatMatKhau.Show();
                LoadDataNhanVien();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên để đổi mật khẩu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}
