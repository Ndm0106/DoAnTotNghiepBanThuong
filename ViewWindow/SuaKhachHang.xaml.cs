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
    /// Interaction logic for SuaKhachHang.xaml
    /// </summary>
    public partial class SuaKhachHang : Window
    {
        private QLQuayThuocBanThuongContext db;
        private KhachHang_MLV _KhachHang;
        public SuaKhachHang(QLQuayThuocBanThuongContext _db, KhachHang_MLV khachhang)
        {
            InitializeComponent();
            db = _db;
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
            var TENKH = db.KhachHangs.Any(x => x.TenKhachHang == suaTenKH);
            if (string.IsNullOrEmpty(suaTenKH))
            {
                MessageBox.Show("Không được để trống tên khách hàng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TENKH)
            {
                MessageBox.Show("Đã tồn tại khách hàng này", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (!IsNumericString(suaSoDienThoaiKH))
            {
                MessageBox.Show("Số điện thoại phải hợp lệ", "Thông báo", MessageBoxButton.OK);
                return;
            }
            if (!IsValidEmail(suaEmailKH))
            {
                MessageBox.Show("Emai phải đúng định dạng", "Thông báo", MessageBoxButton.OK);
                return;
            }
            var suaKhachHang = db.KhachHangs.FirstOrDefault(x => x.IdKhachHang == _KhachHang.IdKhachHang);
            if (suaKhachHang != null)
            {
                suaKhachHang.TenKhachHang = suaTenKH;
                suaKhachHang.DiaChi = suaDiaChiKH;
                suaKhachHang.SoDienThoai = suaSoDienThoaiKH;
                suaKhachHang.Email = suaEmailKH;
                db.SaveChanges();
                LoadDataKhachHang();
                MessageBox.Show("Sửa thành công", "Thông báo");
                this.Close();
            }
            
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
        private void btnSuaKhachHang_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu sửa", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();

        }
        private bool IsNumericString(string s)
        {
            if (txtSuaSoDienThoaiKhachHang.Text.Length != 10)
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
