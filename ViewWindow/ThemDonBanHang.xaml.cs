using DoAnTotNghiepBanThuong.Model;
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
    /// Interaction logic for ThemDonBanHang.xaml
    /// </summary>
    public partial class ThemDonBanHang : Window
    {
        public List<SanPham> sanPhamList;
        public QLQuayThuocBanThuongContext db = new QLQuayThuocBanThuongContext();
        public ObservableCollection<SanPham_MLV> sanPhamObservableList = new ObservableCollection<SanPham_MLV>();
        private ObservableCollection<ChiTietDonBanHang> chiTietDonBanHangs = new ObservableCollection<ChiTietDonBanHang>();
        private string idSanPham;
        public static System.Windows.Controls.ListView? listView;
        private SanPham SanPhamChon;
        private bool isSearching = false;
        private string tenNguoiBan;
        private string idNhanVien;
        public ThemDonBanHang(string idNhanVien, string tenNguoiBan)
        {
            InitializeComponent();
            sanPhamList = db.SanPhams.ToList();
            // Thêm dữ liệu mẫu
            listViewThemDonBanHang.ItemsSource = sanPhamObservableList;
            // Gán dữ liệu cho các điều khiển
            txtThemDonBanHang_NgayBan.Text = DateTime.Now.ToString();
            GenerateIdDonBanHang();
            LoadComboBox();
            this.tenNguoiBan = tenNguoiBan;
            this.idNhanVien = idNhanVien;
            txtThemDonBanHang_TenNguoiThucHien.Text = tenNguoiBan;
            txtTongTienChietKhau.Text = "0";
            txtThemDonBanHang_ChietKhau.Text = "0";
        }
        private void btnThemDonBanHang_Luu_Click(object sender, RoutedEventArgs e)
        {
            if (sanPhamObservableList.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm vào đơn bán hàng.");
                return;
            }
            if (txtThemDonBanHang_KhachHang.SelectedItem == null)
            {

                MessageBox.Show("Không được bỏ trống tên khách hàng thực hiện", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            DonBanHang newDonBanHang = new DonBanHang
            {
                IdDonBanHang = txtThemDonBanHang_IdDonBanHang.Text,
                IdNhanVien = idNhanVien,
                IdKhachHang = ((KhachHang)txtThemDonBanHang_KhachHang.SelectedItem)?.IdKhachHang,
                NgayBan = DateTime.Now,
                TongTienDonBanHang = decimal.Parse(txtTongTienThanhToanDonBanHang.Text)
                // Gán các thông tin khác của đơn nhập hàng từ các điều khiển khác nếu cần
            };
            db.DonBanHangs.Add(newDonBanHang);
            db.SaveChanges();
            string newDonBanHangid = newDonBanHang.IdDonBanHang;
            foreach (var item in listViewThemDonBanHang.Items)
            {
                SanPham_MLV sanPham_MLV = (SanPham_MLV)item;
                ChiTietDonBanHang chiTietDonBanHang = new ChiTietDonBanHang
                {
                    IdChiTietDonBanHang = Guid.NewGuid().ToString(),
                    IdDonBanHang = newDonBanHangid,
                    IdSanPham = sanPham_MLV.IdSanPham,
                    ChietKhau = int.Parse(txtThemDonBanHang_ChietKhau.Text),
                    SoLuongBan = sanPham_MLV.SoLuongBan,
                    DonGiaBan = sanPham_MLV.GiaBanLe * sanPham_MLV.SoLuongBan,
                    // Thêm các thông tin khác của chi tiết đơn nhập hàng nếu cần
                };
                db.ChiTietDonBanHangs.Add(chiTietDonBanHang);
            }
            db.SaveChanges();
            PhieuXuatUC.listView.ItemsSource = db.DonBanHangs.ToList();
            MessageBox.Show("Đã tạo mới đơn bán hàng và nhập chi tiết đơn hàng thành công!");
            this.Close();
        }

        private void btnThemDonBanHang_Them_Click(object sender, RoutedEventArgs e)
        {
            if (SanPhamChon != null)
            {
                //string idDonNhaHang = txtThemDonNhapHang_IdDonNhapHang.Text;
                int soluongbanSanPham;
                decimal chietkhauSanPham;
                if (int.TryParse(txtThemDonBanHang_SoLuongBan.Text, out soluongbanSanPham)&&(decimal.TryParse(txtThemDonBanHang_ChietKhau.Text, out chietkhauSanPham)))
                {
                    SanPham_MLV newItem = new SanPham_MLV
                    {
                        IdSanPham = SanPhamChon.IdSanPham,
                        TenSanPham = SanPhamChon.TenSanPham,
                        ChietKhau = chietkhauSanPham,
                        GiaBanLe = SanPhamChon.GiaBan,
                        HanSuDung = SanPhamChon.HanSuDung,
                        IdDonVi = SanPhamChon.IdDonVi,
                        TenDonVi = SanPhamChon.IdDonViNavigation.TenDonVi,
                        SoLuongBan = soluongbanSanPham,
                        TongTienSanPham = soluongbanSanPham * SanPhamChon.GiaBan
                    };
                    sanPhamObservableList.Add(newItem);
                    ClearInputFields();
                    decimal tongTienChietKhau = sanPhamObservableList.Sum(item => (decimal)item.ChietKhau);
                    decimal tongTienDonBanHang = sanPhamObservableList.Sum(item => (decimal)item.TongTienSanPham);
                    decimal tongTienThanhToanDonBanhang = tongTienDonBanHang - tongTienChietKhau;
                    txtTongTienDonBanHang.Text = tongTienDonBanHang.ToString("N0");
                    txtThemDonBanHang_ChietKhau.Text = tongTienChietKhau.ToString("N0");
                    txtTongTienThanhToanDonBanHang.Text = tongTienThanhToanDonBanhang.ToString("N0");
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

        private void searchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchListBox.SelectedItem != null)
            {
                // Get the selected product from the ListBox
                SanPhamChon = (SanPham)searchListBox.SelectedItem;

                // Do something with the selected product
                txtThemDonBanHang_TimKiemSanPham.Tag = SanPhamChon.IdSanPham;
                txtThemDonBanHang_TimKiemSanPham.Text = SanPhamChon.TenSanPham;
                //txtThemDonBanHang_SoLo.Text = SanPhamChon.SoLo;
                txtThemDonBanHang_SoLuongTon.Text = SanPhamChon.SoLuongTon.ToString();
                txtThemDonBanHang_DonViTinh.Text = SanPhamChon.IdDonViNavigation.TenDonVi;
                txtThemDonBanHang_GiaBan.Text = SanPhamChon.GiaBan.ToString();
                txtThemDonBanHang_HanSuDung.Text = SanPhamChon.HanSuDung.ToString();
                // Close the popup after selection
                idSanPham = SanPhamChon.IdSanPham;
                searchPopup.IsOpen = false;
                isSearching = false;
            }
        }

        private void txtThemDonBanHang_TimKiemSanPham_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtThemDonBanHang_TimKiemSanPham.Text.Trim();

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

        private void btnThemDonBanHang_Xoa_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnThemKhachHang_Click(object sender, RoutedEventArgs e)
        {
            var DonBanHang_ThemKhachHang = new ThemDonBanHang_ThemKhachHang(db);
            DonBanHang_ThemKhachHang.Show();
        }
        private void GenerateIdDonBanHang()
        {
            // Sinh GUID mới
            Guid guid = Guid.NewGuid();

            // Tạo một đối tượng Random
            Random random = new Random();

            // Sinh số ngẫu nhiên từ 0 đến 999 và định dạng nó thành chuỗi có 3 chữ số, sau đó thêm vào cuối chuỗi "SP000"
            string randomDigits = random.Next(0, 1000).ToString("D3");

            // Kết hợp chuỗi "SP000" với chuỗi số ngẫu nhiên
            string result = $"DBH000{randomDigits}";
            txtThemDonBanHang_IdDonBanHang.Text = result;
        }
        private void LoadComboBox()
        {
            txtThemDonBanHang_DonViTinh.ItemsSource = db.DonVis.ToList();
            txtThemDonBanHang_DonViTinh.DisplayMemberPath = "TenDonVi";
            txtThemDonBanHang_DonViTinh.SelectedValuePath = "IdDonVi";

            txtThemDonBanHang_KhachHang.ItemsSource = db.KhachHangs.ToList();
            txtThemDonBanHang_KhachHang.DisplayMemberPath = "TenKhachHang";
            txtThemDonBanHang_KhachHang.SelectedValuePath = "IdKhachHang";

            //txtThemDonNhapHang_NhanVien.ItemsSource = db.NhanViens.ToList();
            //txtThemDonNhapHang_NhanVien.DisplayMemberPath = "TenHienThi";
            //txtThemDonNhapHang_NhanVien.SelectedValuePath = "IdNhanVien";
        }
        private void ClearInputFields()
        {
            // Xóa dữ liệu trên các TextBox và ComboBox
            txtThemDonBanHang_TimKiemSanPham.Clear();
            txtThemDonBanHang_HanSuDung.SelectedDate = null;
            txtThemDonBanHang_SoLuongTon.Clear();
            txtThemDonBanHang_DonViTinh.SelectedIndex = -1;
            txtThemDonBanHang_SoLuongBan.Clear();
            txtThemDonBanHang_GiaBan.Clear();
            txtThemDonBanHang_SoLuongTon.Clear();
        }

        private void btnThemDonBanHang_Thoat_Click(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
