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
using XAct.Library.Settings;

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for NguoiDungUC.xaml
    /// </summary>
    public partial class NguoiDungUC : UserControl
    {
        private QLQuayThuocBanThuongContext db;
        public static System.Windows.Controls.ListView? listView;
        public NguoiDungUC()
        {
            InitializeComponent();
            LoadDataNhanVien();
            db = new QLQuayThuocBanThuongContext();
        }
        private void LoadDataNhanVien()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA = (from nv in db.NhanViens
                                 select new NhanVien_MLV
                                 {
                                     IdNhanVien = nv.IdNhanVien,
                                     TenHienThi = nv.TenHienThi,
                                     TenChucVu = nv.IdChucVuNavigation.TenChucVu,
                                     TaiKhoan = nv.TaiKhoan,
                                     MatKhau = nv.MatKhau,
                                     SoDienThoai = nv.SoDienThoai,
                                     IdChucVu = nv.IdChucVu,
                                     Email = nv.Email,
                                 }).ToList();
                listViewNhanVien.ItemsSource = queryDATA;
                listView = listViewNhanVien;
            }

        }

        private void ThemNhanVienWindow_Click(object sender, RoutedEventArgs e)
        {
            var themNhanVien = new ThemNhanVien(db);
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
        }

        private void XoaNhanVien_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                NhanVien_MLV nhanvien = (NhanVien_MLV)button.DataContext;
                if(nhanvien.TenChucVu == "admin")
                {
                    MessageBox.Show("Không thể xoá tài khoản admin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                bool isUsedInDNH = db.DonNhapHangs.Any(s => s.IdNhanVien == nhanvien.IdNhanVien);
                bool isUsedInDBH = db.DonBanHangs.Any(s => s.IdNhanVien == nhanvien.IdNhanVien);
                if (isUsedInDNH || isUsedInDBH)
                {
                    MessageBox.Show("Không thể xoá người dùng này vì đã thực hiện ít nhất một đơn nhập hàng hoặc đơn bán hàng.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá tài khoản này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        var deleteNV = db.NhanViens.FirstOrDefault(x => x.IdNhanVien == nhanvien.IdNhanVien);
                        if (deleteNV != null) {
                            db.NhanViens.Remove(deleteNV);
                            db.SaveChanges();
                            LoadDataNhanVien();
                            MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButton.OK);
                        }
                    }
                }
            }
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

        private void txtNguoiDung_TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtNguoiDung_TimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                listViewNhanVien.ItemsSource = db.NhanViens.Select(nhanVien => new NhanVien_MLV
                {
                    IdNhanVien = nhanVien.IdNhanVien,
                    TenHienThi = nhanVien.TenHienThi,
                    TaiKhoan = nhanVien.TaiKhoan,
                    TenChucVu = nhanVien.IdChucVuNavigation.TenChucVu,
                    SoDienThoai = nhanVien.SoDienThoai,

                }).ToList();
            }
            else
            {
                var filteredDonViList = db.NhanViens.Where(nhanVien => nhanVien.TenHienThi.ToLower().Contains(searchText))
                                                   .Select(nhanVien => new NhanVien_MLV
                                                   {
                                                       IdNhanVien = nhanVien.IdNhanVien,
                                                       TenHienThi = nhanVien.TenHienThi,
                                                       TaiKhoan = nhanVien.TaiKhoan,
                                                       TenChucVu = nhanVien.IdChucVuNavigation.TenChucVu,
                                                       SoDienThoai = nhanVien.SoDienThoai,
                                                   }).ToList();
                listViewNhanVien.ItemsSource = filteredDonViList;
            }
        }
    }
}
