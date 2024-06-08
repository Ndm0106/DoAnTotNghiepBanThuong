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
using XAct;
using XAct.Library.Settings;
using static MaterialDesignThemes.Wpf.Theme;

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
        public static List<KhachHang>? khachhangMoi;
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
            txtTongTienDonBanHang.Text = "0";
            txtTongTienThanhToanDonBanHang.Text = "0";
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
                    ChietKhau = decimal.Parse(txtThemDonBanHang_ChietKhau.Text),
                    SoLuongBan = sanPham_MLV.SoLuongBan,
                    DonGiaBan = sanPham_MLV.GiaBan * sanPham_MLV.SoLuongBan,
                    // Thêm các thông tin khác của chi tiết đơn nhập hàng nếu cần
                };
                var suaSanPham = db.SanPhams.FirstOrDefault(x => x.IdSanPham == sanPham_MLV.IdSanPham);
                if (suaSanPham != null)
                {
                    suaSanPham.GiaBan = sanPham_MLV.TongTienSanPham / sanPham_MLV.SoLuongBan;
                    suaSanPham.SoLuongTon -= sanPham_MLV.SoLuongBan;
                }
                db.ChiTietDonBanHangs.Add(chiTietDonBanHang);
            }
            db.SaveChanges();
            LoadDataPhieuXuat();
            MessageBox.Show("Đã tạo mới đơn bán hàng và nhập chi tiết đơn hàng thành công!");
            this.Close();
        }
        private void LoadDataPhieuXuat()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA = db.DonBanHangs.Select(dbh => new DonBanHang_MLV
                {
                    IdDonBanHang = dbh.IdDonBanHang,
                    TenHienThi = dbh.IdNhanVienNavigation.TenHienThi,
                    NgayBan = dbh.NgayBan,
                    IdKhachHang = dbh.IdKhachHang,
                    SoDienThoai = dbh.IdKhachHangNavigation.SoDienThoai,
                    IdNhanVien = dbh.IdNhanVien,
                    TenKhachHang = dbh.IdKhachHangNavigation.TenKhachHang,
                    TongTienDonBanHang = dbh.TongTienDonBanHang
                }).ToList();
                PhieuXuatUC.listView.ItemsSource = queryDATA;
                
            }
        }
        private void btnThemDonBanHang_Them_Click(object sender, RoutedEventArgs e)
        {
            if (SanPhamChon != null)
            {
                //string idDonNhaHang = txtThemDonNhapHang_IdDonNhapHang.Text;
                int soluongbanSanPham;
                int soluongtonSanPham;
                decimal chietkhauSanPham;
                decimal giabanSanPham;
                if (int.TryParse(txtThemDonBanHang_SoLuongBan.Text, out soluongbanSanPham)&&(decimal.TryParse(txtThemDonBanHang_ChietKhau.Text, out chietkhauSanPham) && int.TryParse(txtThemDonBanHang_SoLuongTonHienTai.Text, out soluongtonSanPham)) && decimal.TryParse(txtThemDonBanHang_GiaBan.Text,out giabanSanPham))
                {
                    if(soluongbanSanPham > soluongtonSanPham)
                    {
                        MessageBox.Show("Số lượng bán phải nhỏ hơn số lượng tồn","Thông báo",MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }    
                    SanPham_MLV newItem = new SanPham_MLV
                    {
                        IdSanPham = SanPhamChon.IdSanPham,
                        TenSanPham = SanPhamChon.TenSanPham,
                        ChietKhau = chietkhauSanPham,
                        GiaBan = giabanSanPham,
                        SoLuongTon = SanPhamChon.SoLuongTon,
                        HanSuDung = SanPhamChon.HanSuDung,
                        IdDonVi = SanPhamChon.IdDonVi,
                        TenDonVi = SanPhamChon.IdDonViNavigation.TenDonVi,
                        SoLuongBan = soluongbanSanPham,
                        TongTienSanPham = soluongbanSanPham * giabanSanPham
                    };
                    sanPhamObservableList.Add(newItem);
                    ClearInputFields();
                    decimal tongTienChietKhau = sanPhamObservableList.Sum(item => (decimal)item.ChietKhau);
                    decimal tongTienDonBanHang = sanPhamObservableList.Sum(item => (decimal)item.TongTienSanPham);
                    decimal tongTienThanhToanDonBanhang = tongTienDonBanHang - tongTienChietKhau;
                    txtTongTienDonBanHang.Text = tongTienDonBanHang.ToString("N0");
                    txtTongTienChietKhau.Text = tongTienChietKhau.ToString("N0");
                    txtTongTienThanhToanDonBanHang.Text = tongTienThanhToanDonBanhang.ToString("N0");
                }
                else
                {
                    // Chuỗi không hợp lệ, xử lý lỗi tại đây hoặc hiển thị thông báo cho người dùng
                    MessageBox.Show("Thông tin nhập vào không hợp lệ");
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
                int soLuongTonHienTai = GetSoLuongTonHienTai(SanPhamChon.IdSanPham);
                // Do something with the selected product

                if (SanPhamChon.HanSuDung <= DateTime.Now)
                {
                    txtThemDonBanHang_ChietKhau.IsEnabled = false;
                    txtThemDonBanHang_SoLuongBan.IsEnabled = false;
                    txtThemDonBanHang_GiaBan.IsEnabled = false;
                    MessageBox.Show("Sản phẩm này đã hết hạn", "Thông báo", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    txtThemDonBanHang_ChietKhau.IsEnabled = true;
                    txtThemDonBanHang_SoLuongBan.IsEnabled = true;
                    txtThemDonBanHang_GiaBan.IsEnabled = true;
                }
                txtThemDonBanHang_SoLuongTonHienTai.Text = soLuongTonHienTai.ToString();
                txtThemDonBanHang_TimKiemSanPham.Tag = SanPhamChon.IdSanPham;
                txtThemDonBanHang_TimKiemSanPham.Text = SanPhamChon.TenSanPham;
                txtThemDonBanHang_DonViTinh.Text = SanPhamChon.IdDonViNavigation.TenDonVi;
                //txtThemDonBanHang_DonViTinh.Text = SanPhamChon.IdDonViNavigation.TenDonVi;
                txtThemDonBanHang_GiaBan.Text = SanPhamChon.GiaBan.HasValue ? $"{SanPhamChon.GiaBan.Value:F0}" : "N/A";
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
                var matchedProducts = sanPhamList.Where(product => product.TenSanPham.ToLower().Contains(searchText.ToLower()) && product.SoLuongTon>0 && product.HanSuDung>DateTime.Now).ToList();

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
        private int GetSoLuongTonHienTai(string idSanPham)
        {
            using (var dbContext = new QLQuayThuocBanThuongContext())
            {
                // Tính toán số lượng tồn kho hiện tại
                var sanPham = dbContext.SanPhams
            .Where(sp => sp.IdSanPham == idSanPham)
            .Select(sp => new
            {
                SoLuongTon = sp.SoLuongTon ?? 0,
                SoLuongNhap = dbContext.ChiTietDonNhapHangs.Where(ctpn => ctpn.IdSanPham == sp.IdSanPham).Sum(ctpn => (int?)ctpn.SoLuongNhap) ?? 0,
                SoLuongBan = dbContext.ChiTietDonBanHangs.Where(ctdb => ctdb.IdSanPham == sp.IdSanPham).Sum(ctdb => (int?)ctdb.SoLuongBan) ?? 0
            })
            .FirstOrDefault();

                if (sanPham != null)
                {
                    return sanPham.SoLuongTon + sanPham.SoLuongNhap - sanPham.SoLuongBan;
                }
                return 0;
            }
        }
        private void btnThemDonBanHang_Xoa_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                // Lấy sản phẩm được chọn từ ListView
                var sanpham = (SanPham_MLV)button.DataContext;
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá sản phẩm này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    sanPhamObservableList.Remove(sanpham);
                    decimal tongTienChietKhau = sanPhamObservableList.Sum(item => (decimal)item.ChietKhau);
                    decimal tongTienDonBanHang = sanPhamObservableList.Sum(item => (decimal)item.TongTienSanPham);
                    decimal tongTienThanhToanDonBanhang = tongTienDonBanHang - tongTienChietKhau;
                    txtTongTienDonBanHang.Text = tongTienDonBanHang.ToString("N0");
                    txtThemDonBanHang_ChietKhau.Text = tongTienChietKhau.ToString("N0");
                    txtTongTienThanhToanDonBanHang.Text = tongTienThanhToanDonBanhang.ToString("N0");
                }
            }

        }
        
        private void btnThemKhachHang_Click(object sender, RoutedEventArgs e)
        {
            var DonBanHang_ThemKhachHang = new ThemDonBanHang_ThemKhachHang(db);
            DonBanHang_ThemKhachHang.ShowDialog();
            txtThemDonBanHang_KhachHang.ItemsSource = khachhangMoi;
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
        }
        
        private void ClearInputFields()
        {
            txtThemDonBanHang_TimKiemSanPham.Clear();
            //txtThemDonBanHang_HanSuDung.SelectedDate = null;
            txtThemDonBanHang_SoLuongTonHienTai.Clear();
            txtThemDonBanHang_DonViTinh.SelectedIndex = -1;
            txtThemDonBanHang_SoLuongBan.Clear();
            txtThemDonBanHang_GiaBan.Clear();
            txtThemDonBanHang_ChietKhau.Text = "0";
        }

        private void btnThemDonBanHang_Thoat_Click(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }

        private void btnThemDonBanHang_Sua_Click(object sender, RoutedEventArgs e)
        {
            if (listViewThemDonBanHang.SelectedItem != null)
            {
                decimal giaBan;
                decimal chietKhau;
                int soLuongBan;

                if (decimal.TryParse(txtThemDonBanHang_GiaBan.Text, out giaBan) &&
                    decimal.TryParse(txtThemDonBanHang_ChietKhau.Text, out chietKhau) &&
                    int.TryParse(txtThemDonBanHang_SoLuongBan.Text, out soLuongBan))
                {
                    SanPham_MLV selectedSanPham = (SanPham_MLV)listViewThemDonBanHang.SelectedItem;
                    selectedSanPham.GiaBan = giaBan;
                    selectedSanPham.ChietKhau = chietKhau;
                    selectedSanPham.SoLuongBan = soLuongBan;
                    selectedSanPham.TongTienSanPham = soLuongBan * giaBan;

                    // Cập nhật ObservableCollection để ListView tự động cập nhật
                    listViewThemDonBanHang.Items.Refresh();

                    // Cập nhật lại tổng tiền, chiết khấu và tổng thanh toán
                    decimal tongTienChietKhau = sanPhamObservableList.Sum(item => (decimal)item.ChietKhau);
                    decimal tongTienDonBanHang = sanPhamObservableList.Sum(item => (decimal)item.TongTienSanPham);
                    decimal tongTienThanhToanDonBanhang = tongTienDonBanHang - tongTienChietKhau;
                    txtTongTienDonBanHang.Text = tongTienDonBanHang.ToString("N0");
                    txtTongTienChietKhau.Text = tongTienChietKhau.ToString("N0");
                    txtTongTienThanhToanDonBanHang.Text = tongTienThanhToanDonBanhang.ToString("N0");
                    
                    ClearInputFields();
                    btnThemDonBanHang_Them.Visibility = Visibility.Visible;
                    btnThemDonBanHang_Sua.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Thông tin nhập vào không hợp lệ");
                }
                
            }
            listViewThemDonBanHang.UnselectAll();   
        }
        private void listViewThemDonBanHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewThemDonBanHang.SelectedItem != null)
            {
                btnThemDonBanHang_Them.Visibility = Visibility.Collapsed;
                btnThemDonBanHang_Sua.Visibility = Visibility.Visible;
                SanPham_MLV selectedSanPham = (SanPham_MLV)listViewThemDonBanHang.SelectedItem;
                txtThemDonBanHang_TimKiemSanPham.Text = selectedSanPham.TenSanPham;
                searchPopup.IsOpen = false;
                txtThemDonBanHang_DonViTinh.Text = selectedSanPham.TenDonVi;
                txtThemDonBanHang_SoLuongTonHienTai.Text = selectedSanPham.SoLuongTon.ToString();
                txtThemDonBanHang_SoLuongBan.Text = selectedSanPham.SoLuongBan.ToString();
                txtThemDonBanHang_GiaBan.Text = selectedSanPham.GiaBan.HasValue ? $"{selectedSanPham.GiaBan.Value:F0}" : "N/A";
                txtThemDonBanHang_ChietKhau.Text = selectedSanPham.ChietKhau.HasValue ? $"{selectedSanPham.ChietKhau.Value:F0}" : "N/A"; 
            }
        }
    }
}
