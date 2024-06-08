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
    /// Interaction logic for SuaSanPham.xaml
    /// </summary>
    public partial class SuaSanPham : Window
    {
        private SanPham_MLV? _sanpham;
        public SuaSanPham(SanPham_MLV sanpham)
        {
            InitializeComponent();
            _sanpham = sanpham;
            txtSuaTenSanPham.Text = _sanpham.TenSanPham;
            txtSuaGhiChuSanPham.Text = _sanpham.GhiChu;
            txtSuaIdSanPham.Text = _sanpham.IdSanPham;
            txtSuaSoLuongTonSanPham.Text = _sanpham.SoLuongTon.ToString();
            txtSuaGiaBanLeSanPham.Text = _sanpham.GiaBan.HasValue ? $"{_sanpham.GiaBan.Value:F0}" : "N/A";
            txtSuaGiaNhapSanPham.Text = _sanpham.GiaNhap.HasValue ? $"{_sanpham.GiaNhap.Value:F0}" : "N/A";

            txtSuaHamLuongSanPham.Text = _sanpham.HamLuong;
            txtSuaThanhPhanSanPham.Text = _sanpham.ThanhPhan;
            txtSuaHanSuDungSanPham.SelectedDate = _sanpham.HanSuDung;

            txtSuaDonViSanPham.SelectedValue = _sanpham.IdDonVi;
            txtSuaNhaSanXuatSanPham.SelectedValue = _sanpham.IdNhaSanXuat;
            txtSuaNhomSanPhamSanPham.SelectedValue = _sanpham.IdNhomSanPham;

            txtSuaDonViSanPham.Text = _sanpham.TenDonVi;
            txtSuaNhaSanXuatSanPham.Text = _sanpham.TenNhaSanXuat;
            txtSuaNhomSanPhamSanPham.Text = _sanpham.TenNhomSanPham;
            LoadComboBox();
        }
        private void btnSuaSanPham_Sua(object sender, RoutedEventArgs e)
        {
            string suaTenSP = txtSuaTenSanPham.Text;
            string suaNhaSanXuatSP = txtSuaTenSanPham.Text;
            
            DateTime? suaHanSuDungSP = txtSuaHanSuDungSanPham.SelectedDate;
            string suaGhiChuSP = txtSuaTenSanPham.Text;
            string suathanhphanSP = txtSuaThanhPhanSanPham.Text;
            string suahamluongSp = txtSuaHamLuongSanPham.Text;
            string suaIdDonVi;
            string suaIdNhaSanXuat;
            string suaIdNhomSanPham;

            using (var db = new QLQuayThuocBanThuongContext())
            {
                
                if (txtSuaDonViSanPham.Text == null)
                {
                    MessageBox.Show("Không được bỏ trống đơn vị", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtSuaNhaSanXuatSanPham.Text == null)
                {
                    MessageBox.Show("Không được bỏ trống nhà sản xuất", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtSuaNhomSanPhamSanPham.Text == null)
                {
                    MessageBox.Show("Không được bỏ trống nhóm sản phẩm", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var updateHopLe = db.SanPhams.Any(x => x.TenSanPham == suaTenSP && x.TenSanPham != _sanpham.TenSanPham);
                if (updateHopLe)
                {
                    MessageBox.Show("Đã tồn tại sản phẩm này trong danh sách", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!int.TryParse(txtSuaSoLuongTonSanPham.Text, out int soLuongTon))
                {
                    MessageBox.Show("Số lượng tồn phải là một số hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!decimal.TryParse(txtSuaGiaBanLeSanPham.Text, out decimal giaBanLe))
                {
                    MessageBox.Show("Giá bán lẻ phải là một số hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(giaBanLe<=0)
                {
                    MessageBox.Show("Giá bán lẻ phải là một số không âm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }    
                // Validate and parse GiaNhap
                if (!decimal.TryParse(txtSuaGiaNhapSanPham.Text, out decimal giaNhap))
                {
                    MessageBox.Show("Giá nhập phải là một số hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (giaNhap <= 0)
                {
                    MessageBox.Show("Giá nhập phải là một số không âm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var selectedIdDonVi = (DonVi)txtSuaDonViSanPham.SelectedItem;
                suaIdDonVi = selectedIdDonVi.IdDonVi;

                var selectedIdNhaSanXuat = (NhaSanXuat)txtSuaNhaSanXuatSanPham.SelectedItem;
                suaIdNhaSanXuat = selectedIdNhaSanXuat.IdNhaSanXuat;

                var selectedIdNhomSanPham = (NhomSanPham)txtSuaNhomSanPhamSanPham.SelectedItem;
                suaIdNhomSanPham = selectedIdNhomSanPham.IdNhomSanPham;
                var suaSanPham = db.SanPhams.FirstOrDefault(x => x.IdSanPham == _sanpham.IdSanPham && x.TenSanPham == suaTenSP);

                if (suaSanPham != null)
                {

                    suaSanPham.TenSanPham = txtSuaTenSanPham.Text;
                    suaSanPham.SoLuongTon = soLuongTon;
                    suaSanPham.GiaBan = giaBanLe;
                    suaSanPham.GiaNhap = giaNhap;
                    suaSanPham.GhiChu = txtSuaGhiChuSanPham.Text;
                    suaSanPham.IdDonVi = suaIdDonVi;
                    suaSanPham.ThanhPhan = suathanhphanSP;
                    suaSanPham.HanSuDung = suaHanSuDungSP;
                    suaSanPham.IdNhaSanXuat = suaIdNhaSanXuat;
                    suaSanPham.IdNhomSanPham = suaIdNhomSanPham;
                    
                    db.SaveChanges();
                    LoadListView();
                    MessageBox.Show("Thông tin sản phẩm đã được cập nhật.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm trong cơ sở dữ liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
        public void LoadListView()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var queryDATA =
               (from sp in db.SanPhams
                join dv in db.DonVis on sp.IdDonVi equals dv.IdDonVi
                join nsp in db.NhomSanPhams on sp.IdNhomSanPham equals nsp.IdNhomSanPham
                join ncc in db.NhaSanXuats on sp.IdNhaSanXuat equals ncc.IdNhaSanXuat
                select new SanPham_MLV
                {
                    IdSanPham = sp.IdSanPham,
                    TenSanPham = sp.TenSanPham,
                    TenDonVi = sp.IdDonViNavigation.TenDonVi,
                    SoLuongTon = sp.SoLuongTon,
                    GiaNhap = sp.GiaNhap,
                    HamLuong = sp.HamLuong,
                    GiaBan = sp.GiaBan,
                    ThanhPhan = sp.ThanhPhan,
                    HanSuDung = sp.HanSuDung,
                    TenNhaSanXuat = sp.IdNhaSanXuatNavigation.TenNhaSanXuat,
                    TenNhomSanPham = sp.IdNhomSanPhamNavigation.TenNhomSanPham,
                    GhiChu = sp.GhiChu,
                    IdDonVi = sp.IdDonViNavigation.IdDonVi,
                    IdNhaSanXuat = sp.IdNhaSanXuatNavigation.IdNhaSanXuat,
                    IdNhomSanPham = sp.IdNhomSanPhamNavigation.IdNhomSanPham
                }
               ).ToList();
                SanPhamUC.listView.ItemsSource = queryDATA;
            }
        }
        private void btnSuaSanPham_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu sửa", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
        private void LoadComboBox()
        {
            using (var _db = new QLQuayThuocBanThuongContext())
            {
                txtSuaDonViSanPham.ItemsSource = _db.DonVis.ToList();
                //txtSuaDonViSanPham.DisplayMemberPath = "TenDonVi";
                //txtSuaDonViSanPham.SelectedValuePath = "IdDonVi";

                //txtSuaDonViSanPham.SelectedValue = _sanpham.IdDonVi;

                txtSuaNhaSanXuatSanPham.ItemsSource = _db.NhaSanXuats.ToList();
                //txtSuaNhaSanXuatSanPham.DisplayMemberPath = "TenNhaSanXuat";
                //txtSuaNhaSanXuatSanPham.SelectedValuePath = "IdNhaSanXuat";

                //txtSuaNhaSanXuatSanPham.SelectedValue = _sanpham.IdNhaSanXuat;



                txtSuaNhomSanPhamSanPham.ItemsSource = _db.NhomSanPhams.ToList();
                //txtSuaNhomSanPhamSanPham.DisplayMemberPath = "TenNhomSanPham";
                //txtSuaNhomSanPhamSanPham.SelectedValuePath = "IdNhomSanPham";

                //txtSuaNhomSanPhamSanPham.SelectedValue = _sanpham.IdNhomSanPham;

            }

        }


    }
}
