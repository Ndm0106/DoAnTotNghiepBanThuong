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
    /// Interaction logic for SuaNhaPhanPhoi.xaml
    /// </summary>
    public partial class SuaNhaPhanPhoi : Window
    {
        private QLQuayThuocBanThuongContext db;
        private NhaPhanPhoi_MLV _NhaPhanPhoi;

        public SuaNhaPhanPhoi(QLQuayThuocBanThuongContext _db, NhaPhanPhoi_MLV NhaPhanPhoi)
        {
            InitializeComponent();
            db = _db;
            _NhaPhanPhoi = NhaPhanPhoi;
            txtSuaIdNhaPhanPhoi.Text = _NhaPhanPhoi.IdNhaPhanPhoi;
            txtSuaTenNhaPhanPhoi.Text = _NhaPhanPhoi.TenNhaPhanPhoi;
            txtSuaDiaChiNhaPhanPhoi.Text = _NhaPhanPhoi.DiaChi;
            txtSuaSoDienThoaiNhaPhanPhoi.Text = _NhaPhanPhoi.SoDienThoai;
            txtSuaEmailNhaPhanPhoi.Text = _NhaPhanPhoi.Email;
            txtSuaSoFaxNhaPhanPhoi.Text = _NhaPhanPhoi.Fax;
            txtSuaMaSoThueNhaPhanPhoi.Text = _NhaPhanPhoi.MaSoThue;
        }
        private void btnSuaNhaPhanPhoi_Sua(object sender, RoutedEventArgs e)
        {
            string suaTenNPP = txtSuaTenNhaPhanPhoi.Text.Trim();
            string suaDiaChiNPP = txtSuaDiaChiNhaPhanPhoi.Text;
            string suaSoDienThoaiNPP = txtSuaSoDienThoaiNhaPhanPhoi.Text;
            string suaSoFaxNPP = txtSuaSoFaxNhaPhanPhoi.Text;
            string suaEmailNPP = txtSuaEmailNhaPhanPhoi.Text;
            string suaMaSoThueNPP = txtSuaEmailNhaPhanPhoi.Text;
            if (string.IsNullOrEmpty(suaTenNPP))
            {
                MessageBox.Show("Không được để trống tên nhà phân phối", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsNumericString(suaSoDienThoaiNPP))
            {
                MessageBox.Show("Số điện thoại đúng định dạng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidEmail(suaEmailNPP))
            {
                MessageBox.Show("Email phải đúng định dạng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var suaNhaPhanPhoi = db.NhaPhanPhois.FirstOrDefault(x => x.IdNhaPhanPhoi == _NhaPhanPhoi.IdNhaPhanPhoi);
            if (suaNhaPhanPhoi != null)
            {
                suaNhaPhanPhoi.TenNhaPhanPhoi = suaTenNPP;
                suaNhaPhanPhoi.DiaChi = suaDiaChiNPP;
                suaNhaPhanPhoi.SoDienThoai = suaSoDienThoaiNPP;
                suaNhaPhanPhoi.Fax = suaSoFaxNPP;
                suaNhaPhanPhoi.Email = suaEmailNPP;
                suaNhaPhanPhoi.MaSoThue = suaMaSoThueNPP;
                db.SaveChanges();
                LoadDataNhaPhanPhoi();
                MessageBox.Show("Sửa thành công", "Thông báo");
                this.Close();
            }
        }
        private void LoadDataNhaPhanPhoi()
        {
            var query = (from npp in db.NhaPhanPhois
                         select new NhaPhanPhoi_MLV
                         {
                             IdNhaPhanPhoi = npp.IdNhaPhanPhoi,
                             TenNhaPhanPhoi = npp.TenNhaPhanPhoi,
                             DiaChi = npp.DiaChi,
                             Fax = npp.Fax,
                             SoDienThoai = npp.SoDienThoai,
                             Email = npp.Email,
                             MaSoThue = npp.MaSoThue,

                         }).ToList();
            NhaPhanPhoiUC.listView.ItemsSource = query;
        }
        private void btnSuaNhaPhanPhoi_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
        private bool IsNumericString(string s)
        {
            if (txtSuaSoDienThoaiNhaPhanPhoi.Text.Length != 10)
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
