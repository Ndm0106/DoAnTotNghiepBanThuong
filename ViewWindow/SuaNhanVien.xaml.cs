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
using XSystem.Security.Cryptography;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for SuaNhanVien.xaml
    /// </summary>
    public partial class SuaNhanVien : Window
    {
        public NhanVien_MLV? nhanvien;
        
        public SuaNhanVien(NhanVien_MLV _nhanvien)
        {
            InitializeComponent();
            nhanvien = _nhanvien;
            LoadComboBox();
            txtSuaTen_NhanVien.Text = nhanvien.TenHienThi;

            txtSuaSoDienThoai_NhanVien.Text = nhanvien.SoDienThoai;

            txtSuaEmail_NhanVien.Text = nhanvien.Email;
            //txtSuaChucVu_NhanVien.Text = nhanvien.TenChucVu;
            txtSuaChucVu_NhanVien.SelectedValue = nhanvien.IdChucVu;
        }
        private void LoadComboBox()
        {
            using (var _dbContext = new QLQuayThuocBanThuongContext())
            {
                txtSuaChucVu_NhanVien.ItemsSource = _dbContext.ChucVus.ToList();
                txtSuaChucVu_NhanVien.DisplayMemberPath = "TenChucVu";
                txtSuaChucVu_NhanVien.SelectedValuePath = "IdChucVu";
            }
        }
        private void LoadListView()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA = (from nv in db.NhanViens
                                 select new NhanVien_MLV
                                 {
                                     IdNhanVien = nv.IdNhanVien,
                                     IdChucVu = nv.IdChucVu,
                                     TenHienThi = nv.TenHienThi,
                                     TenChucVu = nv.IdChucVuNavigation.TenChucVu,
                                     TaiKhoan = nv.TaiKhoan,
                                     SoDienThoai = nv.SoDienThoai,
                                     Email = nv.Email,
                                 }).ToList();
                NguoiDungUC.listView.ItemsSource = queryDATA;
            }
        }
        private void btnSuaNhanVien_Sua(object sender, EventArgs e)
        {
            string suaTenNV = txtSuaTen_NhanVien.Text;
            string suaEmailNV = txtSuaEmail_NhanVien.Text;
            string suaSoDienThoaiNV = txtSuaSoDienThoai_NhanVien.Text;
            int suaIdChucVu = 0;
            using (var db = new QLQuayThuocBanThuongContext())
            {

                if (txtSuaChucVu_NhanVien.SelectedItem != null)
                {
                    var selectedIdChucVu = (ChucVu)txtSuaChucVu_NhanVien.SelectedItem;
                    suaIdChucVu = selectedIdChucVu.IdChucVu;
                }
                else
                {
                    MessageBox.Show("Không được bỏ trống thông tin chức vụ", "Thông tin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var updateHopLe = db.NhanViens.Any(x => x.TenHienThi == suaTenNV && x.TenHienThi != nhanvien.TenHienThi);
                if (updateHopLe)
                {
                    MessageBox.Show("Đã tồn tại người này trong danh sách", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(!IsNumericString(suaSoDienThoaiNV))
                {
                    MessageBox.Show("Số điện thoại đúng định dạng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!IsValidEmail(suaEmailNV))
                {
                    MessageBox.Show("Email phải đúng định dạng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var suaNhanVien = db.NhanViens.FirstOrDefault(x => x.IdNhanVien == nhanvien.IdNhanVien && x.TenHienThi == suaTenNV);
                {
                    if (suaNhanVien != null)
                    {
                        suaNhanVien.TenHienThi = suaTenNV;
                        suaNhanVien.SoDienThoai = suaSoDienThoaiNV;
                        suaNhanVien.Email = suaEmailNV;
                        suaNhanVien.IdChucVu = suaIdChucVu;
                        db.SaveChanges();
                        LoadListView();
                        MessageBox.Show("Thông tin tài khoản đã được cập nhật.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản trong cơ sở dữ liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void btnSuaNhanVien_Thoát(object sender, EventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }

        private bool IsNumericString(string s)
        {
            if (txtSuaSoDienThoai_NhanVien.Text.Length != 10)
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
