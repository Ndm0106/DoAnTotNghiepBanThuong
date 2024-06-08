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

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for ThemSanPham.xaml
    /// </summary>
    public partial class ThemSanPham : Window
    {
        private QLQuayThuocBanThuongContext db;
        public ThemSanPham(QLQuayThuocBanThuongContext _db)
        {
            InitializeComponent();
            db = _db;
            LoadComboBox();


            //tạo mã sản phẩm
            Guid guid = Guid.NewGuid();
            Random random = new Random();
            string randomDigits1 = random.Next(0, 1000).ToString("D3");
            string IdSPNgauNhien = $"SP000{randomDigits1}";

            txtThemIdSanPham.Text = IdSPNgauNhien;

        }
        private void btnThemSanPham_Them(object sender, RoutedEventArgs e)
        {
            string tenSP = txtThemTenSanPham.Text;
            DonVi? selectedTenDV_SP = txtThemDonViSanPham.SelectedItem as Model.DonVi;
            string hamluongSP = txtThemHamLuongSanPham.Text;
            DateTime? hansudungSP = txtThemHanSuDungSanPham.SelectedDate;
            string gianhapSP = txtThemGiaNhapSanPham.Text;
            string giabanSP = txtThemGiaBanLeSanPham.Text;
            string barcodeSP = txtThemBarCodeSanPham.Text;
            NhaSanXuat? selectedtenNSX_SP = txtThemNhaSanXuatSanPham.SelectedItem as NhaSanXuat;
            string soluongtonSP = txtThemSoLuongTonSanPham.Text;
            string thanhphanSP = txtThemThanhPhanSanPham.Text;
            NhomSanPham? selectedtenNSP_SP = txtThemNhomSanPhamSanPham.SelectedItem as NhomSanPham;

            if (string.IsNullOrEmpty(tenSP))
            {
                MessageBox.Show("tên sản phẩm không đc để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool trungtenSP = db.SanPhams.Any(x=>x.TenSanPham == tenSP);
            if(trungtenSP)
            {
                MessageBox.Show("Đã tồn tại sản phẩm này", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }   
            if(int.Parse(soluongtonSP) <= 0 || decimal.Parse(giabanSP) <= 0 || decimal.Parse(gianhapSP) <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsNumericString(soluongtonSP) || !IsNumericString(gianhapSP) || !IsNumericString(giabanSP))
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if ((selectedTenDV_SP != null && selectedTenDV_SP.IdDonVi != null) || (selectedtenNSP_SP != null && selectedtenNSP_SP.IdNhomSanPham != null) || (selectedtenNSX_SP != null && selectedtenNSX_SP.IdNhaSanXuat != null))
            {
                // Tiếp tục xử lý
                var sanpham = new SanPham
                {
                    BarCode = barcodeSP,
                    IdSanPham = txtThemIdSanPham.Text,
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
                    GhiChu = txtThemGhiChuSanPham.Text
                };
                db.Add(sanpham);
                db.SaveChanges();
                LoadListView();
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

        public void LoadListView()
        {
            var queryDATA = from sp in db.SanPhams
                            select new SanPham_MLV
                            {
                                IdDonVi = sp.IdDonVi,
                                IdNhaSanXuat = sp.IdNhaSanXuat,
                                IdNhomSanPham = sp.IdNhomSanPham,
                                IdSanPham = sp.IdSanPham,
                                TenSanPham = sp.TenSanPham,
                                TenDonVi = sp.IdDonViNavigation.TenDonVi,
                                TenNhaSanXuat = sp.IdNhaSanXuatNavigation.TenNhaSanXuat,
                                SoLuongTon = sp.SoLuongTon,
                                GiaBan = sp.GiaBan,
                                ThanhPhan = sp.ThanhPhan,
                                HamLuong = sp.HamLuong,
                                GiaNhap = sp.GiaNhap,
                                TenNhomSanPham = sp.IdNhomSanPhamNavigation.TenNhomSanPham,
                                HanSuDung = sp.HanSuDung,
                                GhiChu = sp.GhiChu
                            };

            SanPhamUC.listView.ItemsSource = queryDATA.ToList();
        }
        private void btnThemSanPham_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu nhập", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
        private void LoadComboBox()
        {
            txtThemDonViSanPham.ItemsSource = db.DonVis.ToList();
            txtThemDonViSanPham.DisplayMemberPath = "TenDonVi";
            txtThemDonViSanPham.SelectedValuePath = "IdDonVi";

            txtThemNhaSanXuatSanPham.ItemsSource = db.NhaSanXuats.ToList();
            txtThemNhaSanXuatSanPham.DisplayMemberPath = "TenNhaSanXuat";
            txtThemNhaSanXuatSanPham.SelectedValuePath = "IdNhaSanXuat";

            txtThemNhomSanPhamSanPham.ItemsSource = db.NhomSanPhams.ToList();
            txtThemNhomSanPhamSanPham.DisplayMemberPath = "TenNhomSanPham";
            txtThemNhomSanPhamSanPham.SelectedValuePath = "IdNhomSanPham";
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
