using DoAnTotNghiepBanThuong.Model;
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

            //tạo barcode
            string guidDigits = guid.ToString().Substring(guid.ToString().Length - 11).Replace("-", "");
            int randomNumber = random.Next(0, 100);
            string randomDigits2 = randomNumber.ToString("D2");
            string barcode = guidDigits + randomDigits2;

            txtThemIdSanPham.Text = IdSPNgauNhien;


            txtThemBarCodeSanPham.Text = barcode;

        }
        private void btnThemSanPham_Them(object sender, RoutedEventArgs e)
        {
            string tenSP = txtThemTenSanPham.Text;
            DonVi? selectedTenDV_SP = txtThemDonViSanPham.SelectedItem as Model.DonVi;
            string hamluongSP = txtThemHamLuongSanPham.Text;
            DateTime? hansudungSP = txtThemHanSuDungSanPham.SelectedDate;
            string gianhapSP = txtThemGiaNhapSanPham.Text;
            string giabanleSP = txtThemGiaBanLeSanPham.Text;
            NhaSanXuat? selectedtenNSX_SP = txtThemNhaSanXuatSanPham.SelectedItem as NhaSanXuat;
            string soluongtonSP = txtThemSoLuongTonSanPham.Text;
            string thanhphanSP = txtThemThanhPhanSanPham.Text;
            NhomSanPham? selectedtenNSP_SP = txtThemNhomSanPhamSanPham.SelectedItem as NhomSanPham;

            if (string.IsNullOrEmpty(tenSP))
            {
                MessageBox.Show("tên sản phẩm không đc để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //if(checkSoLo) {
            //    MessageBox.Show("Sản phẩm này đã tồn tại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}     
            if (selectedTenDV_SP != null && selectedTenDV_SP.IdDonVi != null)
            {
                // Tiếp tục xử lý
                var sanpham = new SanPham
                {
                    BarCode = txtThemBarCodeSanPham.Text,
                    IdSanPham = txtThemIdSanPham.Text,
                    TenSanPham = tenSP,
                    IdDonVi = selectedTenDV_SP.IdDonVi,
                    ThanhPhan = thanhphanSP,
                    SoLuongTon = int.Parse(soluongtonSP),
                    HamLuong = hamluongSP,
                    GiaNhap = decimal.Parse(gianhapSP),
                    GiaBan = decimal.Parse(giabanleSP),
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

                            select new
                            {
                                IdDonVi = sp.IdDonVi,
                                IdNhaSanXuat = sp.IdNhaSanXuat,
                                IdNhomSanPham = sp.IdNhomSanPham,
                                IdSanPham = sp.IdSanPham,
                                TenSanPham = sp.TenSanPham,
                                TenDonVi = sp.IdDonViNavigation.TenDonVi,
                                TenNhaSanXuat = sp.IdNhaSanXuatNavigation.TenNhaSanXuat,
                                SoLuongTon = sp.SoLuongTon,
                                GiaBanLe = sp.GiaBan,
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
    }
}
