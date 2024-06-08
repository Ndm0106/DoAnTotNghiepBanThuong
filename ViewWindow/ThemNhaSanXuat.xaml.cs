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
using static MaterialDesignThemes.Wpf.Theme;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for ThemNhaSanXuat.xaml
    /// </summary>
    public partial class ThemNhaSanXuat : Window
    {
        private QLQuayThuocBanThuongContext db;
        public ThemNhaSanXuat(QLQuayThuocBanThuongContext _db)
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
            string result = $"NCC000{randomDigits}";
            txtThemIdNhaSanXuat.Text = result;
        }
        private void btnThemNhaSanXuat_Them(object sender, RoutedEventArgs e)
        {
            string tenNCC = txtThemTenNhaSanXuat.Text.Trim();
            string diachiNCC = txtThemDiaChiNhaSanXuat.Text.Trim();
            string faxNCC = txtThemFaxNhaSanXuat.Text.Trim();
            string sodienthoaiNCC = txtThemSoDienThoaiNhaSanXuat.Text.Trim();
            string emailNCC = txtThemEmailNhaSanXuat.Text.Trim();
            var queryNCC = db.NhaSanXuats.Any(x => x.TenNhaSanXuat == tenNCC);
            if (string.IsNullOrEmpty(tenNCC))
            {
                MessageBox.Show("Tên nhà sản xuất không đc để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (queryNCC)
            {
                MessageBox.Show("Đã tồn tại nhà sản xuất này", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (!IsNumericString(sodienthoaiNCC))
            {
                MessageBox.Show("Số điện thoại phải hợp lệ", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (!IsValidEmail(emailNCC))
            {
                MessageBox.Show("Emai phải đúng định dạng", "Thông báo", MessageBoxButton.OK);
                return;
            }
            NhaSanXuat NhaSanXuat = new NhaSanXuat
            {
                IdNhaSanXuat = txtThemIdNhaSanXuat.Text,
                TenNhaSanXuat = tenNCC,
                DiaChi = diachiNCC,
                Fax = faxNCC,
                SoDienThoai = sodienthoaiNCC,
                Email = emailNCC
            };
            db.NhaSanXuats.Add(NhaSanXuat);
            db.SaveChanges();
            LoadDataNhaSanXuat();
            MessageBox.Show("Nhà sản xuất mới đã được thêm thành công", "Thông báo", MessageBoxButton.OK);
            this.Close();
        }
        private void LoadDataNhaSanXuat()
        {
            var query = (from nsx in db.NhaSanXuats
                         select new NhaSanXuat_MLV
                         {
                             IdNhaSanXuat = nsx.IdNhaSanXuat,
                             TenNhaSanXuat = nsx.TenNhaSanXuat,
                             DiaChi = nsx.DiaChi,
                             Fax = nsx.Fax,
                             SoDienThoai = nsx.SoDienThoai,
                             Email = nsx.Email,

                         }).ToList();
            NhaSanXuatUC.listView.ItemsSource = query;
            
        }
        private void btnThemNhaSanXuat_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu nhập", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
        private bool IsNumericString(string s)
        {
            if (txtThemSoDienThoaiNhaSanXuat.Text.Length != 10)
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
