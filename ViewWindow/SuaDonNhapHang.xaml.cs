using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewUC;
using Microsoft.EntityFrameworkCore;
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
using XAct.Library.Settings;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for SuaDonNhapHang.xaml
    /// </summary>
    public partial class SuaDonNhapHang : Window
    {
        public ObservableCollection<SanPham_MLV> sanPhamObservableList = new ObservableCollection<SanPham_MLV>();
        public DonNhapHang_MLV selectedDNH_MLV;
        public List<SanPham> sanPhamList;
        private ObservableCollection<ChiTietDonNhapHang_MLV> chiTietDonNhapHangs; /* = new ObservableCollection<ChiTietDonNhapHang_MLV>();*/
        QLQuayThuocBanThuongContext db = new QLQuayThuocBanThuongContext();
        private string idSanPham;
        private SanPham_MLV selectedSanPham;
        public SuaDonNhapHang(DonNhapHang_MLV selectedDNH_MLV)
        {
            InitializeComponent();
            this.selectedDNH_MLV = selectedDNH_MLV;
            LoadDataSanCoDonNhapHang();
            using (var db = new QLQuayThuocBanThuongContext())
            {
                sanPhamList = db.SanPhams.ToList();
            }

            
            chiTietDonNhapHangs = new ObservableCollection<ChiTietDonNhapHang_MLV>();
            listViewSuaDonNhapHang.ItemsSource = GetChiTietDonNhapHang();
        }
        private void LoadDataSanCoDonNhapHang()
        {
            txtSuaDonNhapHang_IdDonNhapHang.Text = selectedDNH_MLV.IdDonNhapHang;
            txtSuaDonNhapHang_NgayNhap.Text = selectedDNH_MLV.NgayNhap.ToString();
            txtSuaDonNhapHang_TenHienThi.Text = selectedDNH_MLV.TenHienThi;
            txtSuaDonNhapHang_NhaPhanPhoi.Text = selectedDNH_MLV.TenNhaPhanPhoi;
            txtSuaTongTienDonNhapHang.Text = selectedDNH_MLV.TongTienDonNhapHang.HasValue ? $"{selectedDNH_MLV.TongTienDonNhapHang.Value:N0}" : "N/A";

        }

        private List<ChiTietDonNhapHang_MLV> GetChiTietDonNhapHang()
        {
            // Lấy danh sách chi tiết đơn nhập hàng từ cơ sở dữ liệu dựa trên IdDonNhapHang
            // Code để lấy dữ liệu từ cơ sở dữ liệu và trả về danh sách chi tiết đơn nhập hàng


            List<ChiTietDonNhapHang_MLV> chiTietDonNhapHangs = new List<ChiTietDonNhapHang_MLV>();

            // Sử dụng Entity Framework để truy vấn cơ sở dữ liệu
            using (var db = new QLQuayThuocBanThuongContext())
            {
                // Lấy danh sách chi tiết đơn nhập hàng từ cơ sở dữ liệu dựa trên IdDonNhapHang
                chiTietDonNhapHangs = (from ctdnh in db.ChiTietDonNhapHangs
                                       join sp in db.SanPhams on ctdnh.IdSanPham equals sp.IdSanPham
                                       where ctdnh.IdDonNhapHang == selectedDNH_MLV.IdDonNhapHang
                                       select new ChiTietDonNhapHang_MLV
                                       {
                                           IdSanPham = ctdnh.IdSanPham,
                                           TenSanPham = ctdnh.IdSanPhamNavigation.TenSanPham,
                                           TenDonVi = sp.IdDonViNavigation.TenDonVi,
                                           GiaNhap = ctdnh.DonGiaNhap/ ctdnh.SoLuongNhap,
                                           HanSuDung = ctdnh.IdSanPhamNavigation.HanSuDung,
                                           SoLuongNhap = ctdnh.SoLuongNhap,
                                           DonGiaNhap = ctdnh.DonGiaNhap
                                       }).ToList();
            }
            return chiTietDonNhapHangs;

        }
        private bool isSearching = false;
        private SanPham SanPhamChon;
        private void ClearInputFields()
        {
            // Xóa dữ liệu trên các TextBox và ComboBox
            txtSuaDonNhapHang_SuaTenSanPham.Clear();
            txtSuaDonNhapHang_SuaHanSuDung.SelectedDate = null;
            //txtSuaDonNhapHang_SuaSoLo.Clear();

            txtSuaDonNhapHang_SuaSoLuong.Clear();
            txtSuaDonNhapHang_SuaGiaNhap.Clear();
        }

        private void btnSuaDonNhapHang_Luu_Click(object sender, RoutedEventArgs e)
        {

            if (selectedDNH_MLV != null)
            {
                if (chiTietDonNhapHangs.Count == 0)
                {
                    // Xóa đơn nhập hàng nếu không còn chi tiết
                    var query = db.DonNhapHangs.FirstOrDefault(x => x.IdDonNhapHang == selectedDNH_MLV.IdDonNhapHang);
                    if (query != null)
                    {
                        // Cập nhật số lượng sản phẩm trước khi xóa chi tiết đơn nhập hàng
                        var chiTietDonNhapHangList = db.ChiTietDonNhapHangs.Where(ctdnh => ctdnh.IdDonNhapHang == selectedDNH_MLV.IdDonNhapHang).ToList();
                        foreach (var chiTiet in chiTietDonNhapHangList)
                        {
                            var sanPham = db.SanPhams.FirstOrDefault(sp => sp.IdSanPham == chiTiet.IdSanPham);
                            if (sanPham != null)
                            {
                                sanPham.SoLuongTon -= chiTiet.SoLuongNhap;
                            }
                        }

                        db.ChiTietDonNhapHangs.RemoveRange(chiTietDonNhapHangList);
                        db.DonNhapHangs.Remove(query);
                        db.SaveChanges();
                        LoadDataPhieuNhap();
                        MessageBox.Show("Đơn nhập hàng đã bị xóa vì không còn chi tiết nào.", "Thông báo", MessageBoxButton.OK);
                        this.Close();
                        return;
                    }
                }
                else
                {
                    // Cập nhật tổng tiền đơn nhập hàng trong đối tượng selectedDNH_MLV
                    selectedDNH_MLV.TongTienDonNhapHang = decimal.Parse(txtSuaTongTienDonNhapHang.Text.Replace("₫", "").Replace(",", "").Trim());
                    db.SaveChanges();
                    LoadDataPhieuNhap();
                    MessageBox.Show("Đã cập nhật thông tin chi tiết đơn nhập hàng và tổng tiền đơn nhập hàng thành công!", "Thông báo", MessageBoxButton.OK);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Không thể cập nhật");
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

                PhieuNhapUC.listView.ItemsSource = queryDATA; 
            }
        }
        private void btnSuaDonNhapHang_SuaThongTinSanPham_Click(object sender, RoutedEventArgs e)
        {
            if (listViewSuaDonNhapHang.SelectedItem != null)
            {
                decimal giaNhap;
                if (decimal.TryParse(txtSuaDonNhapHang_SuaGiaNhap.Text, out giaNhap))
                {
                    // Lấy thông tin sản phẩm đã sửa từ các TextBox

                    DateTime dateTime;
                    if (DateTime.TryParse(txtSuaDonNhapHang_SuaHanSuDung.Text, out dateTime))
                    {
                        string tenSanPham = txtSuaDonNhapHang_SuaTenSanPham.Text;
                        int soLuong = int.Parse(txtSuaDonNhapHang_SuaSoLuong.Text);
                        ChiTietDonNhapHang_MLV selectedProduct = (ChiTietDonNhapHang_MLV)listViewSuaDonNhapHang.SelectedItem;
                        chiTietDonNhapHangs = new ObservableCollection<ChiTietDonNhapHang_MLV>(GetChiTietDonNhapHang());
                        var productToUpdate = chiTietDonNhapHangs.FirstOrDefault(p => p.IdSanPham == selectedProduct.IdSanPham);
                        foreach (var item in chiTietDonNhapHangs)
                        {
                            if (item.IdSanPham == selectedProduct.IdSanPham)
                            {
                                item.GiaNhap = giaNhap;
                                item.SoLuongNhap = soLuong;
                                item.HanSuDung = dateTime;
                                item.DonGiaNhap = giaNhap * soLuong;
                                break;
                            }
                        }
                        decimal donGiaNhap = giaNhap * soLuong;
                        decimal tongTienDonNhapHang = chiTietDonNhapHangs.Sum(item => (decimal)item.DonGiaNhap);
                        txtSuaTongTienDonNhapHang.Text = tongTienDonNhapHang.ToString("N0");

                        // Cập nhật lại ListView
                        UpdateListView();
                        UpdateDatabase(selectedProduct.IdSanPham, soLuong, giaNhap, dateTime, tongTienDonNhapHang, donGiaNhap);
                        ClearInputFields();
                        listViewSuaDonNhapHang.ItemsSource = GetChiTietDonNhapHang();
                        
                    }
                    else
                    {
                        MessageBox.Show("Ngày không hợp lệ.");
                    }
                }
                else
                {
                    MessageBox.Show("Giá nhập không hợp lệ.");
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để sửa đổi.");
            }
        }

        private void listViewSuaDonNhapHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewSuaDonNhapHang.SelectedItem != null)
            {
                ChiTietDonNhapHang_MLV selectedSanPham = (ChiTietDonNhapHang_MLV)listViewSuaDonNhapHang.SelectedItem;
                txtSuaDonNhapHang_SuaTenSanPham.Text = selectedSanPham.TenSanPham;
                txtSuaDonNhapHang_SuaSoLuong.Text = selectedSanPham.SoLuongNhap.ToString();
                txtSuaDonNhapHang_SuaGiaNhap.Text = selectedSanPham.GiaNhap.HasValue ? $"{selectedSanPham.GiaNhap.Value:F0}" : "N/A";
                txtSuaDonNhapHang_SuaHanSuDung.SelectedDate = selectedSanPham.HanSuDung;
                //txtSuaDonNhapHang_SuaSoLo.Text = selectedSanPham.SoLo;
            }
        }

        private void UpdateListView()
        {

            foreach (ChiTietDonNhapHang_MLV item in listViewSuaDonNhapHang.Items)
            {
                chiTietDonNhapHangs.Add(item);
            }
            // Xóa toàn bộ mục trong chiTietDonNhapHangs
            chiTietDonNhapHangs.Clear();
        }
        private void UpdateDatabase(string idSanPham, int soLuong, decimal giaNhap, DateTime hanSuDung, decimal tongTienDonNhapHang , decimal donGiaNhap)
        {
            // Tìm chi tiết đơn nhập hàng tương ứng trong cơ sở dữ liệu
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var chiTietDonNhapHang = db.ChiTietDonNhapHangs.FirstOrDefault(ctdnh => ctdnh.IdSanPham == idSanPham && ctdnh.IdDonNhapHang == selectedDNH_MLV.IdDonNhapHang);

                // Nếu không tìm thấy, không cần thực hiện gì
                if (chiTietDonNhapHang == null)
                {
                    return;
                }
                var sanPhamToUpdate = db.SanPhams.FirstOrDefault(sp => sp.IdSanPham == idSanPham);
                if (sanPhamToUpdate != null)
                {
                    sanPhamToUpdate.GiaNhap = giaNhap;
                    sanPhamToUpdate.HanSuDung = hanSuDung;

                    // Cập nhật thông tin khác của sản phẩm nếu cần
                }
                // Cập nhật thông tin chi tiết đơn nhập hàng
                chiTietDonNhapHang.SoLuongNhap = soLuong;
                chiTietDonNhapHang.IdSanPhamNavigation.HanSuDung = hanSuDung;
                chiTietDonNhapHang.IdSanPhamNavigation.GiaNhap = giaNhap;
                chiTietDonNhapHang.DonGiaNhap = donGiaNhap;
                var donNhapHang = db.DonNhapHangs.FirstOrDefault(sp => sp.IdDonNhapHang == chiTietDonNhapHang.IdDonNhapHang);
                //donNhapHang.TongTienDonNhapHang = decimal.Parse(txtSuaTongTienDonNhapHang.Text);
                donNhapHang.TongTienDonNhapHang = tongTienDonNhapHang;
                // Đánh dấu chi tiết đơn nhập hàng là đã thay đổi
                db.Entry(chiTietDonNhapHang).State = EntityState.Modified;
                // Lưu các thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }
        }

        private void btnSuaDonNhapHang_Thoat_Click(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }

        private void btnSuaDonNhapHang_Xoa_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                // Lấy sản phẩm được chọn từ ListView
                var chiTietSanPham = (ChiTietDonNhapHang_MLV)button.DataContext;
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá sản phẩm này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    chiTietDonNhapHangs.Remove(chiTietSanPham);
                    listViewSuaDonNhapHang.ItemsSource = chiTietDonNhapHangs;
                    decimal tongTienDonNhapHang = (decimal)chiTietDonNhapHangs.Sum(item => item.DonGiaNhap);
                    txtSuaTongTienDonNhapHang.Text = $"{tongTienDonNhapHang:N0}"; 
                }
            }
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
