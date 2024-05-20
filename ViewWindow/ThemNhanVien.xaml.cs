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
using XSystem.Security.Cryptography;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for ThemNhanVien.xaml
    /// </summary>
    public partial class ThemNhanVien : Window
    {
        private QLQuayThuocBanThuongContext _dbContext;
        public ThemNhanVien(QLQuayThuocBanThuongContext _db)
        {
            InitializeComponent();
            _dbContext = _db;
            LoadComboBox();
        }
        private void LoadListView()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA = (from nv in db.NhanViens
                                 join cv in db.ChucVus on nv.IdChucVu equals cv.IdChucVu
                                 select new
                                 {
                                     TenHienThi = nv.TenHienThi,
                                     TenChucVu = cv.TenChucVu,
                                     TaiKhoan = nv.TaiKhoan,
                                     SoDienThoai = nv.SoDienThoai,
                                     Email = nv.Email,
                                 }).ToList();
                NguoiDungUC.listView.ItemsSource = queryDATA;
            }

        }
        private void btnThemNhanVien_Them(object sender, RoutedEventArgs e)
        {
            string themTenNV = txtThemTen_NhanVien.Text;
            string themTaiKhoanNV = txtThemTaiKhoan_NhanVien.Text;
            string themMatKhauNV = txtThemMatKhau_NhanVien.Password;
            string themSoDienThoaiNV = txtThemSoDienThoai_NhanVien.Text;
            string themEmailNV = txtThemEmail_NhanVien.Text;
            ChucVu? selectedChucVuNV = txtThemChucVu_NhanVien.SelectedItem as ChucVu;
            if (string.IsNullOrEmpty(themTenNV) || string.IsNullOrEmpty(themTaiKhoanNV) || string.IsNullOrEmpty(themMatKhauNV))
            {
                MessageBox.Show("Không được để trống tên nhân viên, tài khoản, mật khẩu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsNumericString(themSoDienThoaiNV))
            {
                MessageBox.Show("Phải nhập đúng định dạng số điện thoại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidCredentials(themTaiKhoanNV, themMatKhauNV))
            {
                MessageBox.Show("Tài khoản và mật khẩu không được phép có khoảng trắng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (selectedChucVuNV != null && selectedChucVuNV.IdChucVu != null)
            {

                string passEncode = MD5Hash(Base64Encode(themMatKhauNV));
                var cv = Convert.ToInt32(selectedChucVuNV.IdChucVu);
                var nhanvien = new Model.NhanVien
                {
                    TenHienThi = themTenNV,
                    TaiKhoan = themTaiKhoanNV,
                    MatKhau = passEncode,
                    SoDienThoai = themSoDienThoaiNV,
                    Email = themEmailNV,
                    IdChucVu = cv
                };
                _dbContext.Add(nhanvien);
                _dbContext.SaveChanges();
                LoadListView();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
                this.Close();
            }
            else
            {
                // Xử lý trường hợp khi một trong các đối tượng hoặc thuộc tính là null
                MessageBox.Show("Không thể tạo khi các trường chưa chọn", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
        private void btnThemNhanVien_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu thêm", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
        private void LoadComboBox()
        {
            txtThemChucVu_NhanVien.ItemsSource = _dbContext.ChucVus.ToList();
            txtThemChucVu_NhanVien.DisplayMemberPath = "TenChucVu";
            txtThemChucVu_NhanVien.SelectedValuePath = "IdChucVu";
        }
        private bool IsNumericString(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        static bool IsValidCredentials(string username, string password)
        {
            // Kiểm tra xem tên tài khoản không chứa khoảng trắng
            foreach (char c in username)
            {
                if (char.IsWhiteSpace(c))
                {
                    return false; // Nếu có ký tự khoảng trắng, trả về false
                }
            }

            // Kiểm tra xem mật khẩu không chứa khoảng trắng
            foreach (char c in password)
            {
                if (char.IsWhiteSpace(c))
                {
                    return false; // Nếu có ký tự khoảng trắng, trả về false
                }
            }

            return true; // Nếu không có ký tự khoảng trắng, trả về true
        }
    }
}
