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
    /// Interaction logic for ThemDonNhapHang.xaml
    /// </summary>
    public partial class ThemDonNhapHang : Window
    {
        public List<SanPham> sanPhamList;
        public QLQuayThuocBanThuongContext db = new QLQuayThuocBanThuongContext();
        public ObservableCollection<SanPham_MLV> sanPhamObservableList = new ObservableCollection<SanPham_MLV>();
        private ObservableCollection<ChiTietDonNhapHang> chiTietDonNhapHangs = new ObservableCollection<ChiTietDonNhapHang>();
        private string idSanPham;
        public static System.Windows.Controls.ListView? listView;
        private SanPham SanPhamChon;
        private bool isSearching = false;
        string tenNguoiNhap;
        string idNhanVien;
        public ThemDonNhapHang(List<SanPham> sanPhams, string tenNguoiNhap, string idNhanVien)
        {
            InitializeComponent();
            sanPhamList = db.SanPhams.ToList();
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
            var themNhaPhanPhoi = new ThemNhaPhanPhoi(db);
            themNhaPhanPhoi.ShowDialog();
        }
        private void btnThemDonHangHoa_ThemSanPhamMoi_Click(object sender, RoutedEventArgs e)
        {
            var themSanPham = new ThemSanPham(db);
            themSanPham.Show();
        }

        private void btnThemDonNhapHang_Them_Click(object sender, RoutedEventArgs e)
        {
            if (SanPhamChon != null)
            {
                //string idDonNhaHang = txtThemDonNhapHang_IdDonNhapHang.Text;
                int soluongnhapSanPham;
                if (int.TryParse(txtThemDonNhapHang_SoLuong.Text, out soluongnhapSanPham))
                {
                    SanPham_MLV newItem = new SanPham_MLV
                    {
                        IdSanPham = SanPhamChon.IdSanPham,
                        TenSanPham = SanPhamChon.TenSanPham,
                        
                        GiaNhap = SanPhamChon.GiaNhap,
                        HanSuDung = SanPhamChon.HanSuDung,
                        IdDonVi = SanPhamChon.IdDonVi,
                        TenDonVi = SanPhamChon.IdDonViNavigation.TenDonVi,
                        SoLuongNhap = soluongnhapSanPham,
                        TongTienSanPham = soluongnhapSanPham * SanPhamChon.GiaNhap
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
                NgayNhap = DateTime.Now,
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
                    DonGiaNhap = sanPham_MLV.GiaNhap * sanPham_MLV.SoLuongNhap,
                    // Thêm các thông tin khác của chi tiết đơn nhập hàng nếu cần
                };
                db.ChiTietDonNhapHangs.Add(chiTietDonNhapHang);
            }
            db.SaveChanges();

            PhieuNhapUC.listView.ItemsSource = db.DonNhapHangs.ToList();
            MessageBox.Show("Đã tạo mới đơn nhập hàng và nhập chi tiết đơn hàng thành công!");
            this.Close();
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
                txtThemDonNhapHang_GiaNhap.Text = SanPhamChon.GiaNhap.ToString();
                txtThemDonNhapHang_HanSuDung.Text = SanPhamChon.HanSuDung.ToString();
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
            txtThemDonNhapHang_HanSuDung.SelectedDate = null;
            txtThemDonNhapHang_SoLo.Clear();
            txtThemDonNhapHang_DonViTinh.SelectedIndex = -1;
            txtThemDonNhapHang_SoLuong.Clear();
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

    }
}
