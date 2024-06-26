﻿using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewUC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ThemDonNhapHang.xaml
    /// </summary>
    public partial class ThemDonNhapHang : Window
    {
        public List<SanPham> sanPhamList;
        public QLQuayThuocBanThuongContext db = new QLQuayThuocBanThuongContext();
        public ObservableCollection<SanPham_MLV> sanPhamObservableList = new ObservableCollection<SanPham_MLV>();
        private ObservableCollection<ChiTietDonNhapHang_MLV> chiTietDonNhapHangs = new ObservableCollection<ChiTietDonNhapHang_MLV>();
        private string idSanPham;
        public static List<SanPham>? sanphamMoi;
        public static List<NhaPhanPhoi>? nhaphanphoiMoi;
        public static System.Windows.Controls.ListView? listView;
        private SanPham SanPhamChon;
        private bool isSearching = false;
        string tenNguoiNhap;
        string idNhanVien;
        public ThemDonNhapHang(List<SanPham> sanPhams, string tenNguoiNhap, string idNhanVien)
        {
            InitializeComponent();
            sanPhamList = db.SanPhams.ToList();
            sanphamMoi = db.SanPhams.ToList();
            this.tenNguoiNhap = tenNguoiNhap;
            this.idNhanVien = idNhanVien;
            // Thêm dữ liệu mẫu
            listViewThemDonNhapHang.ItemsSource = sanPhamObservableList;
            // Gán dữ liệu cho các điều khiển
            txtThemDonNhapHang_NgayNhap.Text = DateTime.Now.ToString();
            txtThemDonNhapHang_NhanVien.Text = tenNguoiNhap;
            GenerateIdDonNhapHang();
            LoadComboBox();
        }
        private void btnThemDonNhapHang_NhaPhanPhoi(object sender, RoutedEventArgs e)
        {
            var themNhaPhanPhoi = new ThemDonNhapHang_ThemNhaPhanPhoi(db);
            themNhaPhanPhoi.ShowDialog();
            txtThemDonNhapHang_NhaPhanPhoi.ItemsSource = nhaphanphoiMoi;
        }
        private void btnThemDonHangHoa_ThemSanPhamMoi_Click(object sender, RoutedEventArgs e)
        {
            var themSanPham = new ThemDonNhapHang_ThemSanPhamMoi(db);
            themSanPham.ShowDialog();
            sanPhamList = sanphamMoi;
        }

        private void btnThemDonNhapHang_Them_Click(object sender, RoutedEventArgs e)
        {
            if (SanPhamChon != null)
            {
                //string idDonNhaHang = txtThemDonNhapHang_IdDonNhapHang.Text;
                int soluongnhapSanPham;
                decimal gianhapSanPham;
                if (int.TryParse(txtThemDonNhapHang_SoLuongNhap.Text, out soluongnhapSanPham) && (decimal.TryParse(txtThemDonNhapHang_GiaNhap.Text,out gianhapSanPham)))
                {
                    SanPham_MLV newItem = new SanPham_MLV
                    {
                        IdSanPham = SanPhamChon.IdSanPham,
                        TenSanPham = SanPhamChon.TenSanPham, 
                        //GiaNhap = SanPhamChon.GiaNhap,
                        GiaNhap = gianhapSanPham,
                        HanSuDung = SanPhamChon.HanSuDung,
                        IdDonVi = SanPhamChon.IdDonVi,
                        TenDonVi = SanPhamChon.IdDonViNavigation.TenDonVi,
                        SoLuongNhap = soluongnhapSanPham,
                        //TongTienSanPham = soluongnhapSanPham * SanPhamChon.GiaNhap
                        TongTienSanPham = soluongnhapSanPham * gianhapSanPham
                    };
                    sanPhamObservableList.Add(newItem);
                    ClearInputFields();
                    decimal tongTienDonNhapHang = sanPhamObservableList.Sum(item => (decimal)item.TongTienSanPham);
                    txtTongTienDonNhapHang.Text = tongTienDonNhapHang.ToString("N0");
                }
                else
                {
                    // Chuỗi không hợp lệ, xử lý lỗi tại đây hoặc hiển thị thông báo cho người dùng
                    MessageBox.Show("Số lượng nhập không hợp lệ.");
                }

            }
            else
            {
                // Xử lý khi không có sản phẩm được chọn
                MessageBox.Show("Vui lòng chọn sản phẩm trước khi thêm.");
            }
        }



        private void LoadComboBox()
        {
            txtThemDonNhapHang_DonViTinh.ItemsSource = db.DonVis.ToList();
            txtThemDonNhapHang_DonViTinh.DisplayMemberPath = "TenDonVi";
            txtThemDonNhapHang_DonViTinh.SelectedValuePath = "IdDonVi";

            txtThemDonNhapHang_NhaPhanPhoi.ItemsSource = db.NhaPhanPhois.ToList();
            txtThemDonNhapHang_NhaPhanPhoi.DisplayMemberPath = "TenNhaPhanPhoi";
            txtThemDonNhapHang_NhaPhanPhoi.SelectedValuePath = "IdNhaPhanPhoi";

        }
        
        private void btnThemDonNhapHang_Luu_Click(object sender, RoutedEventArgs e)
        {
            if (sanPhamObservableList.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm vào đơn nhập hàng.");
                return;
            }
            if (txtThemDonNhapHang_NhaPhanPhoi.SelectedItem == null)
            {

                MessageBox.Show("Không được bỏ trống nhà phân phối", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            DonNhapHang newDonNhapHang = new DonNhapHang
            {
                IdDonNhapHang = txtThemDonNhapHang_IdDonNhapHang.Text,
                IdNhanVien = idNhanVien,
                IdNhaPhanPhoi = ((NhaPhanPhoi)txtThemDonNhapHang_NhaPhanPhoi.SelectedItem)?.IdNhaPhanPhoi,
                NgayNhap = txtThemDonNhapHang_NgayNhap.SelectedDate,
                TongTienDonNhapHang = decimal.Parse(txtTongTienDonNhapHang.Text)
                // Gán các thông tin khác của đơn nhập hàng từ các điều khiển khác nếu cần
            };
            db.DonNhapHangs.Add(newDonNhapHang);
            db.SaveChanges();
            string newDonNhapHangid = newDonNhapHang.IdDonNhapHang;
            foreach (var item in listViewThemDonNhapHang.Items)
            {
                
                SanPham_MLV sanPham_MLV = (SanPham_MLV)item;
                ChiTietDonNhapHang chiTietDonNhapHang = new ChiTietDonNhapHang
                {
                    IdChiTietDonNhapHang = Guid.NewGuid().ToString(),
                    IdDonNhapHang = newDonNhapHangid,
                    IdSanPham = sanPham_MLV.IdSanPham,
                    SoLuongNhap = sanPham_MLV.SoLuongNhap,
                    DonGiaNhap = sanPham_MLV.TongTienSanPham,
                    // Thêm các thông tin khác của chi tiết đơn nhập hàng nếu cần
                };
                var suaSanPham = db.SanPhams.FirstOrDefault(x => x.IdSanPham == sanPham_MLV.IdSanPham);
                if (suaSanPham != null)
                {
                    suaSanPham.GiaNhap = sanPham_MLV.TongTienSanPham/sanPham_MLV.SoLuongNhap;
                    suaSanPham.SoLuongTon += sanPham_MLV.SoLuongNhap;
                }
                db.ChiTietDonNhapHangs.Add(chiTietDonNhapHang);
            }
            db.SaveChanges();
            LoadDataPhieuNhap();
            MessageBox.Show("Đã tạo mới đơn nhập hàng và nhập chi tiết đơn hàng thành công!");
            this.Close();
        }
        private void LoadDataPhieuNhap()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA = db.DonNhapHangs.Select(dnh => new DonNhapHang_MLV
                {
                    IdDonNhapHang = dnh.IdDonNhapHang,
                    TenHienThi = dnh.IdNhanVienNavigation.TenHienThi,
                    NgayNhap = dnh.NgayNhap,
                    IdNhaPhanPhoi = dnh.IdNhaPhanPhoi,
                    IdNhanVien = dnh.IdNhanVien,
                    TenNhaPhanPhoi = dnh.IdNhaPhanPhoiNavigation.TenNhaPhanPhoi,
                    TongTienDonNhapHang = dnh.TongTienDonNhapHang
                }).ToList();

                PhieuNhapUC.listView.ItemsSource = queryDATA;
                
            }
        }
        private void txtThemDonNhapHang_TimKiemSanPham_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtThemDonNhapHang_TimKiemSanPham.Text.Trim();

            // Kiểm tra độ dài của văn bản nhập vào
            if (searchText.Length >= 1)
            {
                // Lọc danh sách sản phẩm dựa trên văn bản tìm kiếm
                var matchedProducts = sanPhamList.Where(product => product.TenSanPham.ToLower().Contains(searchText.ToLower())).ToList();

                // Hiển thị danh sách sản phẩm phù hợp trong ListBox
                searchListBox.ItemsSource = matchedProducts;

                // Mở popup nếu có sản phẩm phù hợp
                searchPopup.IsOpen = matchedProducts.Any();
            }
            else
            {
                // Đóng popup nếu không có đủ ký tự tìm kiếm
                searchPopup.IsOpen = false;
            }
        }
        private void searchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchListBox.SelectedItem != null)
            {
                // Get the selected product from the ListBox
                SanPhamChon = (SanPham)searchListBox.SelectedItem;
                // Do something with the selected product
                txtThemDonNhapHang_TimKiemSanPham.Tag = SanPhamChon.IdSanPham;
                txtThemDonNhapHang_TimKiemSanPham.Text = SanPhamChon.TenSanPham;
                
                txtThemDonNhapHang_DonViTinh.Text = SanPhamChon.IdDonViNavigation.TenDonVi;
                txtThemDonNhapHang_GiaNhap.Text = SanPhamChon.GiaNhap.HasValue ? $"{SanPhamChon.GiaNhap.Value:F0}" : "N/A"; 
                // Close the popup after selection
                idSanPham = SanPhamChon.IdSanPham;
                searchPopup.IsOpen = false;
                isSearching = false;
            }
        }
        private void ClearInputFields()
        {
            // Xóa dữ liệu trên các TextBox và ComboBox
            txtThemDonNhapHang_TimKiemSanPham.Clear();
            txtThemDonNhapHang_SoLuongNhap.Clear();
            txtThemDonNhapHang_DonViTinh.SelectedIndex = -1;
            //txtThemDonNhapHang_SoLuongNhap.Clear();
            txtThemDonNhapHang_GiaNhap.Clear();
        }

        private void GenerateIdDonNhapHang()
        {
            // Sinh GUID mới
            Guid guid = Guid.NewGuid();

            // Tạo một đối tượng Random
            Random random = new Random();

            // Sinh số ngẫu nhiên từ 0 đến 999 và định dạng nó thành chuỗi có 3 chữ số, sau đó thêm vào cuối chuỗi "SP000"
            string randomDigits = random.Next(0, 1000).ToString("D3");

            // Kết hợp chuỗi "SP000" với chuỗi số ngẫu nhiên
            string result = $"DNH000{randomDigits}";
            txtThemDonNhapHang_IdDonNhapHang.Text = result;
        }

        private void btnThemDonNhapHang_Thoat_Click(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }

        private void btnThemDonNhapHang_Xoa_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var sanPham = button.DataContext as SanPham_MLV;
                if (sanPham != null)
                {
                    MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm '{sanPham.TenSanPham}' không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        sanPhamObservableList.Remove(sanPham);
                        decimal tongTienDonNhapHang = sanPhamObservableList.Sum(item => (decimal)item.TongTienSanPham);
                        txtTongTienDonNhapHang.Text = tongTienDonNhapHang.ToString("N0");
                    }
                }
            }
        }

        private void btnThemDonNhapHang_Sua_Click(object sender, RoutedEventArgs e)
        {
            if (listViewThemDonNhapHang.SelectedItem != null)
            {
                decimal giaNhap;
                int soLuongNhap;

                if (decimal.TryParse(txtThemDonNhapHang_GiaNhap.Text, out giaNhap) &&
                    int.TryParse(txtThemDonNhapHang_SoLuongNhap.Text, out soLuongNhap))
                {
                    SanPham_MLV selectedSanPham = (SanPham_MLV)listViewThemDonNhapHang.SelectedItem;
                    selectedSanPham.GiaNhap = giaNhap;
                    selectedSanPham.SoLuongNhap = soLuongNhap;
                    selectedSanPham.TongTienSanPham = soLuongNhap * giaNhap;

                    // Cập nhật ObservableCollection để ListView tự động cập nhật
                    listViewThemDonNhapHang.Items.Refresh();    

                    // Cập nhật lại tổng tiền, chiết khấu và tổng thanh toán
                    decimal tongTienDonNhapHang = sanPhamObservableList.Sum(item => (decimal)item.TongTienSanPham);
                    txtTongTienDonNhapHang.Text = tongTienDonNhapHang.ToString("N0");

                    ClearInputFields();
                    btnThemDonNhapHang_Them.Visibility = Visibility.Visible;
                    btnThemDonNhapHang_Sua.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Thông tin nhập vào không hợp lệ");
                }

            }
            listViewThemDonNhapHang.UnselectAll();
        }

        private void listViewThemDonNhapHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewThemDonNhapHang.SelectedItem != null)
            {
                btnThemDonNhapHang_Them.Visibility = Visibility.Collapsed;
                btnThemDonNhapHang_Sua.Visibility = Visibility.Visible;
                SanPham_MLV selectedSanPham = (SanPham_MLV)listViewThemDonNhapHang.SelectedItem;
                txtThemDonNhapHang_TimKiemSanPham.Text = selectedSanPham.TenSanPham;
                searchPopup.IsOpen = false;
                txtThemDonNhapHang_DonViTinh.Text = selectedSanPham.TenDonVi;
                txtThemDonNhapHang_SoLuongNhap.Text = selectedSanPham.SoLuongNhap.ToString();
                txtThemDonNhapHang_GiaNhap.Text = selectedSanPham.GiaNhap.HasValue ? $"{selectedSanPham.GiaNhap.Value:F0}" : "N/A";
                
            }
        }
    }
}
