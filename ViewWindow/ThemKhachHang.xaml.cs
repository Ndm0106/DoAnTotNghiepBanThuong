using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewUC;
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
using XAct.Users;
using static MaterialDesignThemes.Wpf.Theme;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for ThemKhachHang.xaml
    /// </summary>
    public partial class ThemKhachHang : Window
    {
        private QLQuayThuocBanThuongContext db;
        public ThemKhachHang(QLQuayThuocBanThuongContext _db)
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

            txtThemIdKhachHang.Text = result;
        }
        private void btnThemKhachHang_Them(object sender, RoutedEventArgs e)
        {
            string tenKH = txtThemTenKhachHang.Text.Trim();
            string diachiKH = txtThemDiaChiKhachHang.Text.Trim();
            string sodienthoaiKH = txtThemSoDienThoaiKhachHang.Text.Trim();
            string emailKH = txtThemEmailKhachHang.Text.Trim();
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
            if(!IsNumericString(sodienthoaiKH))
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
                IdKhachHang = txtThemIdKhachHang.Text,
                TenKhachHang = tenKH,
                DiaChi = diachiKH,
                SoDienThoai = sodienthoaiKH,
                Email = emailKH
            };
            db.KhachHangs.Add(khachhang);
            db.SaveChanges();
            LoadDataKhachHang();
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
            this.Close();
        }
        private void LoadDataKhachHang()
        {
            var query = (from kh in db.KhachHangs
                         select new KhachHang_MLV
                         {
                             IdKhachHang = kh.IdKhachHang,
                             TenKhachHang = kh.TenKhachHang,
                             DiaChi = kh.DiaChi,
                             SoDienThoai = kh.SoDienThoai,
                             Email = kh.Email,
                         }).ToList();
            KhachHangUC.listView.ItemsSource = query;
            
        }
        private void btnThemKhachHang_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu nhập", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
        private bool IsNumericString(string s)
        {
            if (txtThemSoDienThoaiKhachHang.Text.Length != 10)
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
            if(firstText != '0')
            {
                return false;
            }    
            if(s == null)
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
