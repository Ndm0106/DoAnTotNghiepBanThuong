using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
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

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for SuaDonBanHang.xaml
    /// </summary>
    public partial class SuaDonBanHang : Window
    {
        public ObservableCollection<SanPham_MLV> sanPhamObservableList = new ObservableCollection<SanPham_MLV>();
        public DonBanHang_MLV selectedDBH_MLV;
        public List<SanPham> sanPhamList;
        private ObservableCollection<ChiTietDonBanHang_MLV> chiTietDonBanHangs;
        private string idSanPham;
        private SanPham_MLV selectedSanPham;
        decimal tongtienChietKhau;
        decimal tongtienDonBanHang;
        public SuaDonBanHang(DonBanHang_MLV selectedDBH_MLV)
        {
            InitializeComponent();
            this.selectedDBH_MLV = selectedDBH_MLV;
            LoadDataSanCoDonBanHang();
            using (var db = new QLQuayThuocBanThuongContext())
            {
                sanPhamList = db.SanPhams.ToList();
            }
            
            
            chiTietDonBanHangs = new ObservableCollection<ChiTietDonBanHang_MLV>();
            listViewSuaDonBanHang.ItemsSource = GetChiTietDonBanHang();
        }
        private void LoadDataSanCoDonBanHang()
        {
            txtSuaDonBanHang_IdDonBanHang.Text = selectedDBH_MLV.IdDonBanHang;
            txtSuaDonBanHang_NgayBan.Text = selectedDBH_MLV.NgayBan.ToString();
            txtSuaDonBanHang_TenNguoiThucHien.Text = selectedDBH_MLV.TenHienThi;
            txtSuaDonBanHang_KhachHang.Text = selectedDBH_MLV.TenKhachHang;
            txtTongTienDonBanHang.Text = selectedDBH_MLV.TongTienDonBanHang.HasValue ? $"{selectedDBH_MLV.TongTienDonBanHang.Value:N0}" : "N/A";
        }


        private List<ChiTietDonBanHang_MLV> GetChiTietDonBanHang()
        {
            // Lấy danh sách chi tiết đơn nhập hàng từ cơ sở dữ liệu dựa trên IdDonNhapHang
            // Code để lấy dữ liệu từ cơ sở dữ liệu và trả về danh sách chi tiết đơn nhập hàng
            
            List<ChiTietDonBanHang_MLV> chiTietDonBanHangs = new List<ChiTietDonBanHang_MLV>();

            // Sử dụng Entity Framework để truy vấn cơ sở dữ liệu
            using (var db = new QLQuayThuocBanThuongContext())
            {
                // Lấy danh sách chi tiết đơn nhập hàng từ cơ sở dữ liệu dựa trên IdDonNhapHang
                chiTietDonBanHangs = (from ctdbh in db.ChiTietDonBanHangs
                                       join sp in db.SanPhams on ctdbh.IdSanPham equals sp.IdSanPham
                                       where ctdbh.IdDonBanHang == selectedDBH_MLV.IdDonBanHang
                                      select new ChiTietDonBanHang_MLV
                                       {
                                           IdSanPham = ctdbh.IdSanPham,
                                           TenSanPham = ctdbh.IdSanPhamNavigation.TenSanPham,
                                           TenDonVi = sp.IdDonViNavigation.TenDonVi,
                                           GiaBan = ctdbh.IdSanPhamNavigation.GiaBan,
                                           HanSuDung = ctdbh.IdSanPhamNavigation.HanSuDung,
                                           ChietKhau = ctdbh.ChietKhau,
                                           SoLuongBan = ctdbh.SoLuongBan,
                                           DonGiaBan = ctdbh.SoLuongBan * sp.GiaBan
                                       }).ToList();
            }
            return chiTietDonBanHangs;
        }


        private void btnSuaDonBanHang_Luu_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                if (selectedDBH_MLV != null)
                {
                    // Cập nhật tổng tiền đơn nhập hàng trong đối tượng selectedDNH_MLV
                    selectedDBH_MLV.TongTienDonBanHang = decimal.Parse(txtTongTienDonBanHang.Text.Replace("₫", "").Replace(",", "").Trim());

                    // Cập nhật tổng tiền đơn nhập hàng trong cơ sở dữ liệu
                    // Cập nhật tổng tiền đơn nhập hàng trong đối tượng selectedDNH_MLV


                    // Lưu các thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();


                    MessageBox.Show("Đã cập nhật thông tin chi tiết đơn bán hàng và tổng tiền đơn bán hàng thành công!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật");
                }
            }
        }

        private void btnSuaDonBanHang_Thoat_Click(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }

        private void btnSuaDonBanHang_Sua_Click(object sender, RoutedEventArgs e)
        {
            if (listViewSuaDonBanHang.SelectedItem != null)
            {
                decimal giaBan;
                decimal chietKhau;
                if (decimal.TryParse(txtSuaDonBanHang_GiaBan.Text, out giaBan) && decimal.TryParse(txtSuaDonBanHang_ChietKhau.Text, out chietKhau))
                {
                    // Lấy thông tin sản phẩm đã sửa từ các TextBox

                    DateTime dateTime;
                    if (DateTime.TryParse(txtSuaDonBanHang_HanSuDung.Text, out dateTime))
                    {
                        string tenSanPham = txtSuaDonBanHang_TenSanPham.Text;
                        int soLuong = int.Parse(txtSuaDonBanHang_SoLuongBan.Text);
                        //string soLo = txtSuaDonNhapHang_SuaSoLo.Text;
                        // Lấy sản phẩm được chọn
                        ChiTietDonBanHang_MLV selectedProduct = (ChiTietDonBanHang_MLV)listViewSuaDonBanHang.SelectedItem;
                        chiTietDonBanHangs = new ObservableCollection<ChiTietDonBanHang_MLV>(GetChiTietDonBanHang());
                        // Tìm sản phẩm tương ứng trong danh sách
                        var productToUpdate = chiTietDonBanHangs.FirstOrDefault(p => p.IdSanPham == selectedProduct.IdSanPham);
                        foreach (var item in chiTietDonBanHangs)
                        {
                            if (item.IdSanPham == selectedProduct.IdSanPham)
                            {
                                item.GiaBan = giaBan;
                                item.SoLuongBan = soLuong;
                                item.HanSuDung = dateTime;
                                item.ChietKhau = chietKhau;
                                //item.SoLo = soLo;
                                item.DonGiaBan = giaBan * soLuong - chietKhau;
                                break;
                            }
                        }
                        decimal tongTienDonBanHang = chiTietDonBanHangs.Sum(item => (decimal)item.DonGiaBan);
                        txtTongTienDonBanHang.Text = tongTienDonBanHang.ToString("N0");

                        // Cập nhật lại ListView
                        UpdateListView();
                        UpdateDatabase(selectedProduct.IdSanPham, soLuong, giaBan, dateTime, chietKhau);
                        ClearInputFields();
                        listViewSuaDonBanHang.ItemsSource = GetChiTietDonBanHang();
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
        }
        private void ClearInputFields()
        {
            // Xóa dữ liệu trên các TextBox và ComboBox
            txtSuaDonBanHang_TenSanPham.Clear();
            txtSuaDonBanHang_HanSuDung.SelectedDate = null;
            //txtSuaDonNhapHang_SuaSoLo.Clear();

            txtSuaDonBanHang_SoLuongBan.Clear();
            txtSuaDonBanHang_GiaBan.Clear();
        }
        private void UpdateListView()
        {

            foreach (ChiTietDonBanHang_MLV item in listViewSuaDonBanHang.Items)
            {
                chiTietDonBanHangs.Add(item);
            }
            // Xóa toàn bộ mục trong chiTietDonNhapHangs
            chiTietDonBanHangs.Clear();
        }
        private void UpdateDatabase(string idSanPham, int soLuong, decimal giaBan, DateTime hanSuDung,decimal chietKhau)
        {
            // Tìm chi tiết đơn nhập hàng tương ứng trong cơ sở dữ liệu
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var chiTietDonBanHang = db.ChiTietDonBanHangs.FirstOrDefault(ctdnh => ctdnh.IdSanPham == idSanPham && ctdnh.IdDonBanHang == selectedDBH_MLV.IdDonBanHang);

                // Nếu không tìm thấy, không cần thực hiện gì
                if (chiTietDonBanHang == null)
                {
                    return;
                }
                var sanPhamToUpdate = db.SanPhams.FirstOrDefault(sp => sp.IdSanPham == idSanPham);
                if (sanPhamToUpdate != null)
                {
                    sanPhamToUpdate.GiaBan = giaBan;

                    sanPhamToUpdate.HanSuDung = hanSuDung;

                    // Cập nhật thông tin khác của sản phẩm nếu cần
                }
                // Cập nhật thông tin chi tiết đơn nhập hàng
                chiTietDonBanHang.SoLuongBan = soLuong;
                chiTietDonBanHang.ChietKhau = chietKhau;
                chiTietDonBanHang.IdSanPhamNavigation.HanSuDung = hanSuDung;
                chiTietDonBanHang.IdSanPhamNavigation.GiaBan = giaBan;
                // Đánh dấu chi tiết đơn nhập hàng là đã thay đổi
                db.Entry(chiTietDonBanHang).State = EntityState.Modified;

                // Lưu các thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }
        }
        private void btnSuaDonBanHang_Xoa_Click(object sender, RoutedEventArgs e)    
        {
            
        }
        private void listViewSuaDonBanHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            if (listViewSuaDonBanHang.SelectedItem != null)
            {
                    ChiTietDonBanHang_MLV selectedSanPham = (ChiTietDonBanHang_MLV)listViewSuaDonBanHang.SelectedItem;
                    txtSuaDonBanHang_TenSanPham.Text = selectedSanPham.TenSanPham;
                    txtSuaDonBanHang_SoLuongBan.Text = selectedSanPham.SoLuongBan.ToString();
                    txtSuaDonBanHang_GiaBan.Text = selectedSanPham.GiaBan.HasValue ? $"{selectedSanPham.GiaBan.Value:F0}" : "N/A";
                    txtSuaDonBanHang_ChietKhau.Text = selectedSanPham.ChietKhau.HasValue ? $"{selectedSanPham.ChietKhau.Value:F0}" : "N/A";
                //txtSuaDonNhapHang_SuaSoLo.Text = selectedSanPham.SoLo;
                    txtSuaDonBanHang_HanSuDung.SelectedDate = selectedSanPham.HanSuDung;

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
