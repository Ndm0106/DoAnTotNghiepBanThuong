using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewUC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for ThemDonBanHang_ThemKhachHang.xaml
    /// </summary>
    public partial class ThemDonBanHang_ThemKhachHang : Window
    {
        private QLQuayThuocBanThuongContext db;
        
        public ThemDonBanHang_ThemKhachHang(QLQuayThuocBanThuongContext _db)
        {
            InitializeComponent();
            db = _db;
            // Sinh GUID mới
            Guid guid = Guid.NewGuid();

            // Tạo một đối tượng Random
            Random random = new Random();

            // Sinh số ngẫu nhiên từ 0 đến 999 và định dạng nó thành chuỗi có 3 chữ số, sau đó thêm vào cuối chuỗi "SP000"
            string randomDigits = random.Next(0, 1000).ToString("D3");

            // Kết hợp chuỗi "SP000" với chuỗi số ngẫu nhiên
            string result = $"KH000{randomDigits}";

            txtThemDonBanHang_ThemKhachHang_IdKhachHang.Text = result;
        }

        private void btnThemDonBanHang_ThemKhachHang_Thoat_Click(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
            {
                ThemDonBanHang.khachhangMoi = db.KhachHangs.ToList();
                this.Close();
            }
               
        }

        private void btnThemDonBanHang_ThemKhachHang_Luu_Click(object sender, RoutedEventArgs e)
        {
            string tenKH = txtThemDonBanHang_ThemKhachHang_TenKhachHang.Text;
            string diachiKH = txtThemDonBanHang_ThemKhachHang_DiaChiKhachHang.Text;
            string sodienthoaiKH = txtThemDonBanHang_ThemKhachHang_SoDienThoaiKhachHang.Text;
            string emailKH = txtThemDonBanHang_ThemKhachHang_EmailKhachHang.Text;
            var TENKH = db.KhachHangs.Any(x => x.TenKhachHang == tenKH);
            if (string.IsNullOrEmpty(tenKH))
            {
                MessageBox.Show("Tên khách hàng không đc để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TENKH)
            {
                MessageBox.Show("Đã tồn tại khách hàng này", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (!IsNumericString(sodienthoaiKH))
            {
                MessageBox.Show("Số điện thoại phải hợp lệ", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (!IsValidEmail(emailKH))
            {
                MessageBox.Show("Emai phải đúng định dạng", "Thông báo", MessageBoxButton.OK);
                return;
            }
            KhachHang khachhang = new KhachHang
            {
                IdKhachHang = txtThemDonBanHang_ThemKhachHang_IdKhachHang.Text,
                TenKhachHang = tenKH,
                DiaChi = diachiKH,
                SoDienThoai = sodienthoaiKH,
                Email = emailKH
            };
            db.KhachHangs.Add(khachhang);
            db.SaveChanges();
            ThemDonBanHang.khachhangMoi = db.KhachHangs.ToList();
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
            this.Close();
        }
        private bool IsNumericString(string s)
        {
            if (txtThemDonBanHang_ThemKhachHang_SoDienThoaiKhachHang.Text.Length != 10)
            {
                return false;
            }
            foreach (char c in s)
            {
                if (char.IsWhiteSpace(c))
                {
                    return false; // Nếu có ký tự khoảng trắng, trả về false
                }
            }
            char firstText = s[0];
            if (firstText != '0')
            {
                return false;
            }
            if (s == null)
            {
                return false;
            }
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsValidEmail(string email)
        {
            if (!email.Contains("@") || !email.Contains("."))
            {
                return false;
            }

            try
            {
                new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
