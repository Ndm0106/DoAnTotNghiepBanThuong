using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewWindow;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for PhieuNhapUC.xaml
    /// </summary>
    public partial class PhieuNhapUC : UserControl
    {
        public static System.Windows.Controls.ListView? listView;
        public List<SanPham> sp = new List<SanPham>();
        //public QLQuayThuocBanThuongContext _db = new QLQuayThuocBanThuongContext();
        private string idNhanVien;
        private string nameNhanVien;
        public PhieuNhapUC(string idNhanVien, string nameNhanVien)
        {
            InitializeComponent();
            LoadDataPhieuNhap();
            this.idNhanVien = idNhanVien;
            this.nameNhanVien = nameNhanVien;
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                cbxPhieuNhap_NhanVien.ItemsSource = db.NhanViens.ToList();
                cbxPhieuNhap_NhanVien.DisplayMemberPath = "TenHienThi";
                cbxPhieuNhap_NhanVien.SelectedValuePath = "IdNhanVien";
            }
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

                //listViewPhieuNhap.ItemsSource = queryDATA;
                //listView = listViewPhieuNhap;
                UpdateListView(queryDATA);
            }
            
        }
        private void ThemPhieuNhapWindow_Click(object sender, RoutedEventArgs e)
        {

            var themDonNhapHang = new ThemDonNhapHang(sp, idNhanVien, nameNhanVien);
            themDonNhapHang.Show();
            LoadDataPhieuNhap();
        }
        private void SuaPhieuNhapWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                DonNhapHang_MLV selectedDonNhapHang = button.DataContext as DonNhapHang_MLV;
                if (selectedDonNhapHang != null)
                {
                    // Tạo một cửa sổ mới để hiển thị chi tiết đơn nhập hàng
                    SuaDonNhapHang detailWindow = new SuaDonNhapHang(selectedDonNhapHang);
                    detailWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn hàng.");
                }
            }
        }
        private void XoaPhieuNhap_Click(object sender, RoutedEventArgs e)
        {
            using (var _db = new QLQuayThuocBanThuongContext())
            {
                var button = sender as System.Windows.Controls.Button;
                if (button != null)
                {
                    
                    var donhaphang = (DonNhapHang_MLV)button.DataContext;
                    MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá đơn này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        var relatedRecords = _db.ChiTietDonNhapHangs.Where(x => x.IdDonNhapHang == donhaphang.IdDonNhapHang);
                        _db.ChiTietDonNhapHangs.RemoveRange(relatedRecords);
                        
                        var itemToRemove = _db.DonNhapHangs.FirstOrDefault(s => s.IdDonNhapHang == donhaphang.IdDonNhapHang);
                        if (itemToRemove != null)
                        {
                            _db.DonNhapHangs.Remove(itemToRemove);
                            _db.SaveChanges();
                            LoadDataPhieuNhap(); // Load lại dữ liệu sau khi xóa
                            MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }
        private void UpdateListView(List<DonNhapHang_MLV> data)
        {
            listViewPhieuNhap.ItemsSource = data;
            listView = listViewPhieuNhap;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewPhieuNhap.ItemsSource);
            view.GroupDescriptions.Clear();
            //PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChiTietDonNhapHang_MLV");
            //view.GroupDescriptions.Add(groupDescription);
        }
        private void btnPhieuNhap_TimKiem_Click(object sender, RoutedEventArgs e)
        {
            using (var _db = new QLQuayThuocBanThuongContext())
            {
                DateTime? startDate = datePickerPhieuNhap_TuNgay.SelectedDate;
                DateTime? endDate = datePickerPhieuNhap_DenNgay.SelectedDate;

                
                string selectedNhanVienId = null;

               
                if (cbxPhieuNhap_NhanVien.SelectedItem != null)
                {
                    selectedNhanVienId = (cbxPhieuNhap_NhanVien.SelectedItem as NhanVien).IdNhanVien;
                }

                // Thực hiện truy vấn LINQ
                var query = from dnh in _db.DonNhapHangs
                            join npp in _db.NhaPhanPhois on dnh.IdNhaPhanPhoi equals npp.IdNhaPhanPhoi
                            join nv in _db.NhanViens on dnh.IdNhanVien equals nv.IdNhanVien
                            where (string.IsNullOrEmpty(selectedNhanVienId) || dnh.IdNhanVien == selectedNhanVienId)
                               && (!startDate.HasValue || dnh.NgayNhap >= startDate.Value)
                               && (!endDate.HasValue || dnh.NgayNhap <= endDate.Value)
                            select new DonNhapHang_MLV
                            {
                                IdDonNhapHang = dnh.IdDonNhapHang,
                                NgayNhap = dnh.NgayNhap,
                                TenNhaPhanPhoi = npp.TenNhaPhanPhoi,
                                TenHienThi = nv.TenHienThi,
                                TongTienDonNhapHang = dnh.TongTienDonNhapHang
                            };

                // Thực hiện truy vấn và chuyển kết quả sang danh sách
                var result = query.ToList();

                // Kiểm tra nếu không có kết quả
                if (!result.Any())
                {
                    MessageBox.Show("Không có đơn nhập hàng nào cho lựa chọn này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Hiển thị kết quả
                UpdateListView(result);
            }
        }
    }
}
