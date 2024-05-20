using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
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
    /// Interaction logic for SuaNhanVien.xaml
    /// </summary>
    public partial class SuaNhanVien : Window
    {
        private NhanVien_MLV? _nhanvien;
        public int suaIdChucVu = 0;
        public SuaNhanVien(NhanVien_MLV nhanvien)
        {
            InitializeComponent();
            _nhanvien = nhanvien;
            LoadComboBox();
            txtSuaTen_NhanVien.Text = _nhanvien.TenHienThi;

            txtSuaSoDienThoai_NhanVien.Text = _nhanvien.SoDienThoai;

            txtSuaEmail_NhanVien.Text = _nhanvien.Email;

            txtSuaChucVu_NhanVien.Text = _nhanvien.TenChucVu;
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
                                 join cv in db.ChucVus on nv.IdChucVu equals cv.IdChucVu
                                 select new NhanVien_MLV
                                 {
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
                var updateHopLe = db.NhanViens.Any(x => x.TenHienThi == suaTenNV && x.TenHienThi != _nhanvien.TenHienThi);
                if (updateHopLe)
                {
                    MessageBox.Show("Đã tồn tại người này trong danh sách", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var suaNhanVien = db.NhanViens.FirstOrDefault(x => x.IdNhanVien == _nhanvien.IdNhanVien && x.TenHienThi == suaTenNV);
                {
                    if (suaNhanVien != null)
                    {
                        suaNhanVien.TenHienThi = txtSuaTen_NhanVien.Text;
                        suaNhanVien.SoDienThoai = txtSuaSoDienThoai_NhanVien.Text;
                        suaNhanVien.Email = txtSuaEmail_NhanVien.Text;

                        db.SaveChanges();
                        LoadListView();
                        MessageBox.Show("Thông tin tài khoản đã được cập nhật.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu sửa", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
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

    }
}
