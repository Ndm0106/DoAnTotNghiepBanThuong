using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewWindow;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for PhieuXuatUC.xaml
    /// </summary>
    public partial class PhieuXuatUC : UserControl
    {
        public static System.Windows.Controls.ListView? listView;
        private string idNhanVien;
        private string tenNhanVien;
        public PhieuXuatUC(string idNhanVien, string tenNhanVien)
        {
            InitializeComponent();
            this.tenNhanVien = tenNhanVien;
            this.idNhanVien = idNhanVien;
            LoadDataPhieuXuat();
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                cbxPhieuXuat_NhanVien.ItemsSource = db.NhanViens.ToList();
                cbxPhieuXuat_NhanVien.DisplayMemberPath = "TenHienThi";
                cbxPhieuXuat_NhanVien.SelectedValuePath = "IdNhanVien";
            }
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
                //listViewPhieuXuat.ItemsSource = queryDATA;
                //listView = listViewPhieuXuat;
                UpdateListView(queryDATA);
            }
        }

        private void ThemPhieuXuatWindow_Click(object sender, RoutedEventArgs e)
        {
            var themDonBanHang = new ThemDonBanHang(tenNhanVien, idNhanVien);
            themDonBanHang.Show();
        }

        private void SuaPhieuXuatWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                DonBanHang_MLV selectedDonBanHang = button.DataContext as DonBanHang_MLV;
                if (selectedDonBanHang != null)
                {
                    // Tạo một cửa sổ mới để hiển thị chi tiết đơn nhập hàng
                    SuaDonBanHang detailWindow = new SuaDonBanHang(selectedDonBanHang);
                    detailWindow.ShowDialog();
                }
            }
        }

        private void XoaPhieuXuat_Click(object sender, RoutedEventArgs e)
        {
            using (var _db = new QLQuayThuocBanThuongContext())
            {
                var button = sender as System.Windows.Controls.Button;
                if (button != null)
                {
                    // Lấy sản phẩm được chọn từ ListView
                    DonBanHang_MLV donbanhang = (DonBanHang_MLV)button.DataContext;
                    MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá đơn này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        var relatedRecords = _db.ChiTietDonBanHangs.Where(x => x.IdDonBanHang == donbanhang.IdDonBanHang);
                        _db.ChiTietDonBanHangs.RemoveRange(relatedRecords);
                        
                        var itemToRemove = _db.DonBanHangs.FirstOrDefault(s => s.IdDonBanHang == donbanhang.IdDonBanHang);
                        if (itemToRemove != null)
                        {
                            _db.DonBanHangs.Remove(itemToRemove);
                            _db.SaveChanges();
                            LoadDataPhieuXuat(); // Load lại dữ liệu sau khi xóa
                            MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }
        private void UpdateListView(List<DonBanHang_MLV> data)
        {
            listViewPhieuXuat.ItemsSource = data;
            listView = listViewPhieuXuat;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewPhieuXuat.ItemsSource);
            view.GroupDescriptions.Clear();
            //PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChiTietDonNhapHang_MLV");
            //view.GroupDescriptions.Add(groupDescription);
        }
        private void btnPhieuXuat_TimKiem_Click(object sender, RoutedEventArgs e)
        {
            using (var _db = new QLQuayThuocBanThuongContext())
            {
                DateTime? startDate = datePickerPhieuXuat_TuNgay.SelectedDate;
                DateTime? endDate = datePickerPhieuXuat_DenNgay.SelectedDate;


                string selectedNhanVienId = null;


                if (cbxPhieuXuat_NhanVien.SelectedItem != null)
                {
                    selectedNhanVienId = (cbxPhieuXuat_NhanVien.SelectedItem as NhanVien).IdNhanVien;
                }

                // Thực hiện truy vấn LINQ
                var query = from dbh in _db.DonBanHangs
                            join kh in _db.KhachHangs on dbh.IdKhachHang equals kh.IdKhachHang
                            join nv in _db.NhanViens on dbh.IdNhanVien equals nv.IdNhanVien
                            where (string.IsNullOrEmpty(selectedNhanVienId) || dbh.IdNhanVien == selectedNhanVienId)
                               && (!startDate.HasValue || dbh.NgayBan >= startDate.Value)
                               && (!endDate.HasValue || dbh.NgayBan <= endDate.Value)
                            select new DonBanHang_MLV
                            {
                                IdDonBanHang = dbh.IdDonBanHang,
                                NgayBan = dbh.NgayBan,
                                TenKhachHang = kh.TenKhachHang,
                                TenHienThi = nv.TenHienThi,
                                TongTienDonBanHang = dbh.TongTienDonBanHang
                            };

                // Thực hiện truy vấn và chuyển kết quả sang danh sách
                var result = query.ToList();

                // Kiểm tra nếu không có kết quả
                if (!result.Any())
                {
                    MessageBox.Show("Không có đơn bán hàng nào cho lựa chọn này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Hiển thị kết quả
                UpdateListView(result);
            }
        }
    }
}
