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

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for SuaNhaSanXuat.xaml
    /// </summary>
    public partial class SuaNhaSanXuat : Window
    {
        private QLQuayThuocBanThuongContext db;
        private NhaSanXuat_MLV _NhaSanXuat;
        public SuaNhaSanXuat(QLQuayThuocBanThuongContext _db, NhaSanXuat_MLV NhaSanXuat)
        {
            InitializeComponent();
            db = _db;
            _NhaSanXuat = NhaSanXuat;
            txtSuaIdNhaSanXuat.Text = _NhaSanXuat.IdNhaSanXuat;
            txtSuaTenNhaSanXuat.Text = _NhaSanXuat.TenNhaSanXuat;
            txtSuaDiaChiNhaSanXuat.Text = _NhaSanXuat.DiaChi;
            txtSuaSoDienThoaiNhaSanXuat.Text = _NhaSanXuat.SoDienThoai;
            txtSuaEmailNhaSanXuat.Text = _NhaSanXuat.Email;
            txtSuaSoFaxNhaSanXuat.Text = _NhaSanXuat.Fax;
        }
        private void btnSuaNhaSanXuat_Sua(object sender, RoutedEventArgs e)
        {
            string suaTenNCC = txtSuaTenNhaSanXuat.Text.Trim();
            string suaDiaChiNCC = txtSuaDiaChiNhaSanXuat.Text;
            string suaSoDienThoaiNCC = txtSuaSoDienThoaiNhaSanXuat.Text;
            string suaSoFaxNCC = txtSuaSoFaxNhaSanXuat.Text;
            string suaEmailNCC = txtSuaEmailNhaSanXuat.Text;

            if (string.IsNullOrEmpty(suaTenNCC))
            {
                MessageBox.Show("Không được để trống tên nhà cung cấp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsNumericString(suaSoDienThoaiNCC))
            {
                MessageBox.Show("Số điện thoại đúng định dạng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidEmail(suaEmailNCC))
            {
                MessageBox.Show("Email phải đúng định dạng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var suaNhaSanXuat = db.NhaSanXuats.FirstOrDefault(x => x.IdNhaSanXuat == _NhaSanXuat.IdNhaSanXuat);
            if (suaNhaSanXuat != null) {
                suaNhaSanXuat.TenNhaSanXuat = suaTenNCC;
                suaNhaSanXuat.DiaChi = suaDiaChiNCC;
                suaNhaSanXuat.SoDienThoai = suaSoDienThoaiNCC;
                suaNhaSanXuat.Fax = suaSoFaxNCC;
                suaNhaSanXuat.Email = suaEmailNCC;
                db.SaveChanges();
                LoadDataNhaSanXuat();
                MessageBox.Show("Sửa thành công", "Thông báo");
                this.Close();
            }
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
        private void btnSuaNhaSanXuat_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu sửa", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
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
        private bool IsNumericString(string s)
        {
            if (txtSuaSoDienThoaiNhaSanXuat.Text.Length != 10)
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
    }
}
