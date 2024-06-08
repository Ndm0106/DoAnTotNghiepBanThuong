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

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for ThemDonNhapHang_ThemSanPhamMoi.xaml
    /// </summary>
    public partial class ThemDonNhapHang_ThemSanPhamMoi : Window
    {
        private QLQuayThuocBanThuongContext db;
        public ThemDonNhapHang_ThemSanPhamMoi( QLQuayThuocBanThuongContext db)
        {
            InitializeComponent();
            this.db = db;
            Guid guid = Guid.NewGuid();
            Random random = new Random();
            string randomDigits1 = random.Next(0, 1000).ToString("D3");
            string IdSPNgauNhien = $"SP000{randomDigits1}";

            //tạo barcode
            string guidDigits = guid.ToString().Substring(guid.ToString().Length - 11).Replace("-", "");
            int randomNumber = random.Next(0, 100);
            string randomDigits2 = randomNumber.ToString("D2");
            string barcode = guidDigits + randomDigits2;
            LoadComboBox();
            txtThemDonNhapHang_ThemSanPhamMoi_IdSanPham.Text = IdSPNgauNhien;
            txtThemDonNhapHang_ThemSanPhamMoi_BarCodeSanPham.Text = barcode;
        }
        private void LoadComboBox()
        {
            txtThemDonNhapHang_ThemSanPhamMoi_DonViSanPham.ItemsSource = db.DonVis.ToList();
            txtThemDonNhapHang_ThemSanPhamMoi_DonViSanPham.DisplayMemberPath = "TenDonVi";
            txtThemDonNhapHang_ThemSanPhamMoi_DonViSanPham.SelectedValuePath = "IdDonVi";

            txtThemDonNhapHang_ThemSanPhamMoi_NhaSanXuatSanPham.ItemsSource = db.NhaSanXuats.ToList();
            txtThemDonNhapHang_ThemSanPhamMoi_NhaSanXuatSanPham.DisplayMemberPath = "TenNhaSanXuat";
            txtThemDonNhapHang_ThemSanPhamMoi_NhaSanXuatSanPham.SelectedValuePath = "IdNhaSanXuat";

            txtThemDonNhapHang_ThemSanPhamMoi_NhomSanPhamSanPham.ItemsSource = db.NhomSanPhams.ToList();
            txtThemDonNhapHang_ThemSanPhamMoi_NhomSanPhamSanPham.DisplayMemberPath = "TenNhomSanPham";
            txtThemDonNhapHang_ThemSanPhamMoi_NhomSanPhamSanPham.SelectedValuePath = "IdNhomSanPham";
        }
        private void btnThemDonNhapHang_ThemSanPhamMoi_Thoat_Click(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
            {
                ThemDonNhapHang.sanphamMoi = db.SanPhams.ToList();
                this.Close();
            }    
                
        }

        private void btnThemDonNhapHang_ThemSanPhamMoi_Luu_Click(object sender, RoutedEventArgs e)
        {
            string tenSP = txtThemDonNhapHang_ThemSanPhamMoi_TenSanPham.Text;
            DonVi? selectedTenDV_SP = txtThemDonNhapHang_ThemSanPhamMoi_DonViSanPham.SelectedItem as Model.DonVi;
            string hamluongSP = txtThemDonNhapHang_ThemSanPhamMoi_HamLuongSanPham.Text;
            DateTime? hansudungSP = txtThemDonNhapHang_ThemSanPhamMoi_HanSuDungSanPham.SelectedDate;
            string gianhapSP = txtThemDonNhapHang_ThemSanPhamMoi_GiaNhapSanPham.Text;
            string giabanSP = txtThemDonNhapHang_ThemSanPhamMoi_GiaBanLeSanPham.Text;
            NhaSanXuat? selectedtenNSX_SP = txtThemDonNhapHang_ThemSanPhamMoi_NhaSanXuatSanPham.SelectedItem as NhaSanXuat;
            string soluongtonSP = txtThemDonNhapHang_ThemSanPhamMoi_SoLuongTonSanPham.Text;
            string thanhphanSP = txtThemDonNhapHang_ThemSanPhamMoi_ThanhPhanSanPham.Text;
            NhomSanPham? selectedtenNSP_SP = txtThemDonNhapHang_ThemSanPhamMoi_NhomSanPhamSanPham.SelectedItem as NhomSanPham;
            if (string.IsNullOrEmpty(tenSP))
            {
                MessageBox.Show("tên sản phẩm không đc để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool trungtenSP = db.SanPhams.Any(x => x.TenSanPham == tenSP);
            if (trungtenSP)
            {
                MessageBox.Show("Đã tồn tại sản phẩm này", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (int.Parse(soluongtonSP) <= 0 || decimal.Parse(giabanSP) <= 0 || decimal.Parse(gianhapSP) <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsNumericString(soluongtonSP) || !IsNumericString(gianhapSP) || !IsNumericString(giabanSP))
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (selectedTenDV_SP != null && selectedTenDV_SP.IdDonVi != null)
            {
                // Tiếp tục xử lý
                var sanpham = new SanPham
                {
                    BarCode = txtThemDonNhapHang_ThemSanPhamMoi_BarCodeSanPham.Text,
                    IdSanPham = txtThemDonNhapHang_ThemSanPhamMoi_IdSanPham.Text,
                    TenSanPham = tenSP,
                    IdDonVi = selectedTenDV_SP.IdDonVi,
                    ThanhPhan = thanhphanSP,
                    SoLuongTon = int.Parse(soluongtonSP),
                    HamLuong = hamluongSP,
                    GiaNhap = decimal.Parse(gianhapSP),
                    GiaBan = decimal.Parse(giabanSP),
                    HanSuDung = hansudungSP,
                    IdNhaSanXuat = selectedtenNSX_SP.IdNhaSanXuat,
                    IdNhomSanPham = selectedtenNSP_SP.IdNhomSanPham,
                    GhiChu = txtThemDonNhapHang_ThemSanPhamMoi_GhiChuSanPham.Text
                };
                db.Add(sanpham);
                db.SaveChanges();
                ThemDonNhapHang.sanphamMoi = db.SanPhams.ToList();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
                this.Close();
            }
            else
            {
                // Xử lý trường hợp khi một trong các đối tượng hoặc thuộc tính là null
                MessageBox.Show("Không thể tạo khi các trường chưa chọn", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private bool IsNumericString(string s)
        {
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
