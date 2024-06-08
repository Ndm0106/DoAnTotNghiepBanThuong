using DoAnTotNghiepBanThuong.Model;
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
using System.Windows.Shapes;
using XSystem.Security.Cryptography;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for CapNhatMatKhau.xaml
    /// </summary>
    public partial class CapNhatMatKhau : Window
    {
        NhanVien_MLV _selectedNV;
        QLQuayThuocBanThuongContext db = new QLQuayThuocBanThuongContext();
        public CapNhatMatKhau(NhanVien_MLV selectedNV)
        {
            InitializeComponent();
            _selectedNV = selectedNV;
        }
        private void btnCapNhatMatKhauNhanVien_DoiMatKhau(object sender, EventArgs e)
        {
            string mkHienTai = txtMatKhauHienTai_NhanVien.Password;
            string mkMoi = txtMatKhauMoi_NhanVien.Password;
            string mkMoiXacNhan = txtNhapLaiMatKhauMoi_NhanVien.Password;
            string passOld = MD5Hash(Base64Encode(mkHienTai));
            if (_selectedNV.MatKhau != passOld)
            {
                MessageBox.Show("Vui lòng nhập đúng mật khẩu cũ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (mkMoi == mkHienTai && mkMoiXacNhan == mkHienTai)
            {
                MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (mkMoi != mkMoiXacNhan)
            {
                MessageBox.Show("Vui lòng xác nhận đúng mật khẩu mới", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!IsValidCredentials(mkMoi, mkMoiXacNhan))
            {
                MessageBox.Show("Mật khẩu mới không được phép có khoảng trắng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var nhanVien = db.NhanViens.FirstOrDefault(x => x.IdNhanVien == _selectedNV.IdNhanVien);
            if (nhanVien != null)
            {
                nhanVien.MatKhau = MD5Hash(Base64Encode(mkMoi));
                db.SaveChanges();
                MessageBox.Show("Đổi mật khẩu thành công","Thông báo",MessageBoxButton.OK);
                this.Close();   
            }     
        }
        private void btnCapNhatMatKhauNhanVien_Thoat(object sender, EventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
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
