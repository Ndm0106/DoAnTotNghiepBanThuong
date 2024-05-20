using DoAnTotNghiepBanThuong.Model;
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

namespace DoAnTotNghiepBanThuong
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly QLQuayThuocBanThuongContext db = new QLQuayThuocBanThuongContext();
        private string tenHienThi;
        private string ChucVu;
        private string idNhanVien;
        public LoginWindow()
        {
            InitializeComponent();

        }
        private void btnDangNhap_Click(object sender, RoutedEventArgs e)
        {

            string taikhoan = UsernameBox.Text;
            string passEncode = MD5Hash(Base64Encode(FloatingPasswordBox.Password));
            var nhanvien = (from nv in db.NhanViens
                            join cv in db.ChucVus on nv.IdChucVu equals cv.IdChucVu
                            where nv.TaiKhoan == taikhoan && nv.MatKhau == passEncode
                            select new { NhanVien = nv, ChucVu = cv })
                .FirstOrDefault();
            if (nhanvien != null && passEncode != null && taikhoan != null)
            {
                int idChucVu = nhanvien.ChucVu.IdChucVu;
                tenHienThi = nhanvien.NhanVien.TenHienThi;
                idNhanVien = nhanvien.NhanVien.IdNhanVien;
                ChucVu = LayTenChucVu(idChucVu);
                this.Hide();
                MainWindow mainWindow = new MainWindow(tenHienThi, ChucVu, idNhanVien);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            var diaThoat = MessageBox.Show("Bạn có muốn thoát chương trình không?", "Hệ thống quản lý cửa hàng bán thuốc", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == diaThoat)
                this.Close();
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
        public string GetTenHienThiNguoiDung()
        {
            return tenHienThi;
        }
        public string GetTenChucVuNguoiDung()
        {
            return ChucVu;
        }
        private string LayTenChucVu(int idChucVu)
        {
            string tenChucVu = "";
            try
            {
                using (var dbContext = new QLQuayThuocBanThuongContext())
                {
                    var chucVu = dbContext.ChucVus.FirstOrDefault(cv => cv.IdChucVu == idChucVu);
                    if (chucVu != null)
                    {
                        tenChucVu = chucVu.TenChucVu;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần thiết
                MessageBox.Show("Lỗi khi truy vấn cơ sở dữ liệu: " + ex.Message);
            }
            return tenChucVu;
        }
    }
}
