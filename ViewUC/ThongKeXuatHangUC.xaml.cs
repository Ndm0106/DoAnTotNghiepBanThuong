using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Net;

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for ThongKeXuatHangUC.xaml
    /// </summary>
    public partial class ThongKeXuatHangUC : UserControl
    {
        public ThongKeXuatHangUC()
        {
            InitializeComponent();
            LoadDataThongKeXuatHang();
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                cbxThongKeXuatHang_KhachHang.ItemsSource = db.KhachHangs.ToList();
                cbxThongKeXuatHang_KhachHang.DisplayMemberPath = "TenKhachHang";
                cbxThongKeXuatHang_KhachHang.SelectedValuePath = "IdKhachHang";

                cbxThongKeXuatHang_NhanVien.ItemsSource = db.NhanViens.ToList();
                cbxThongKeXuatHang_NhanVien.DisplayMemberPath = "TenHienThi";
                cbxThongKeXuatHang_NhanVien.SelectedValuePath = "IdNhanVien";
            }
        }
        private void LoadDataThongKeXuatHang()
        {
            using (var dbContext = new QLQuayThuocBanThuongContext())
            {
                var query = (
        from dbh in dbContext.DonBanHangs
        join kh in dbContext.KhachHangs on dbh.IdKhachHang equals kh.IdKhachHang
        join nv in dbContext.NhanViens on dbh.IdNhanVien equals nv.IdNhanVien
        select new DonBanHang_MLV
        {
            IdDonBanHang = dbh.IdDonBanHang,
            NgayBan = dbh.NgayBan,
            TenKhachHang = kh.TenKhachHang,
            TenHienThi = nv.TenHienThi,
            chiTietDonBanHang_MLVs = (
                from ctdbh in dbContext.ChiTietDonBanHangs
                join sp in dbContext.SanPhams on ctdbh.IdSanPham equals sp.IdSanPham
                join dv in dbContext.DonVis on sp.IdDonVi equals dv.IdDonVi
                where ctdbh.IdDonBanHang == dbh.IdDonBanHang
                select new ChiTietDonBanHang_MLV
                {
                    TenSanPham = sp.TenSanPham,
                    SoLuongBan = ctdbh.SoLuongBan,
                    ChietKhau = ctdbh.ChietKhau,
                    DonGiaBan = ctdbh.SoLuongBan * sp.GiaBan - ctdbh.ChietKhau,
                    GiaBan = sp.GiaBan,
                    TenDonVi = dv.TenDonVi,
                }
            ).ToList()
        }
    ).ToList();

                foreach (var item in query)
                {
                    var totalAmount = 0M;
                    foreach (var detailItem in item.chiTietDonBanHang_MLVs)
                    {

                        totalAmount += (decimal)detailItem.DonGiaBan;

                    }
                    item.TongTienDonBanHang = totalAmount;
                    // Thêm tổng số lượng và tổng tiền vào mỗi mục chi tiết phiếu nhập

                }
                UpdateListView(query);

            }
        }
        private void UpdateListView(List<DonBanHang_MLV> data)
        {
            listViewThongKeXuatHang.ItemsSource = data;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewThongKeXuatHang.ItemsSource);
            view.GroupDescriptions.Clear();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChiTietDonBanHang_MLV");
            view.GroupDescriptions.Add(groupDescription);
        }
        private void btnThongKeXuatHang_TimKiem_Click(object sender, RoutedEventArgs e)
        {
            using (var _db = new QLQuayThuocBanThuongContext())
            {
                DateTime? startDate = datePickerThongKeXuatHang_TuNgay.SelectedDate;
                DateTime? endDate = datePickerThongKeXuatHang_DenNgay.SelectedDate;

                string selectedKhachHangId = null;
                string selectedNhanVienId = null;

                if (cbxThongKeXuatHang_KhachHang.SelectedItem != null)
                {
                    selectedKhachHangId = (cbxThongKeXuatHang_KhachHang.SelectedItem as KhachHang).IdKhachHang;
                }

                if (cbxThongKeXuatHang_NhanVien.SelectedItem != null)
                {
                    selectedNhanVienId = (cbxThongKeXuatHang_NhanVien.SelectedItem as NhanVien).IdNhanVien;
                }

                // Thực hiện truy vấn LINQ
                var query = from dbh in _db.DonBanHangs
                            join kh in _db.KhachHangs on dbh.IdKhachHang equals kh.IdKhachHang
                            join nv in _db.NhanViens on dbh.IdNhanVien equals nv.IdNhanVien
                            where (string.IsNullOrEmpty(selectedKhachHangId) || dbh.IdKhachHang == selectedKhachHangId)
                               && (string.IsNullOrEmpty(selectedNhanVienId) || dbh.IdNhanVien == selectedNhanVienId)
                               && (!startDate.HasValue || dbh.NgayBan >= startDate.Value)
                               && (!endDate.HasValue || dbh.NgayBan <= endDate.Value)
                            select new DonBanHang_MLV
                            {
                                IdDonBanHang = dbh.IdDonBanHang,
                                NgayBan = dbh.NgayBan,
                                TenKhachHang = kh.TenKhachHang,
                                TenHienThi = nv.TenHienThi,
                                TongTienDonBanHang = _db.ChiTietDonBanHangs
                            .Where(ctdbh => ctdbh.IdDonBanHang == dbh.IdDonBanHang)
                            .Sum(ctdbh => ctdbh.SoLuongBan * ctdbh.IdSanPhamNavigation.GiaBan - ctdbh.ChietKhau),
                                chiTietDonBanHang_MLVs = (from ctdbh in _db.ChiTietDonBanHangs
                                                          join sp in _db.SanPhams on ctdbh.IdSanPham equals sp.IdSanPham
                                                          join dv in _db.DonVis on sp.IdDonVi equals dv.IdDonVi
                                                          where ctdbh.IdDonBanHang == dbh.IdDonBanHang
                                                          select new ChiTietDonBanHang_MLV
                                                          {
                                                              TenSanPham = sp.TenSanPham,
                                                              SoLuongBan = ctdbh.SoLuongBan,
                                                              ChietKhau = ctdbh.ChietKhau,
                                                              DonGiaBan = ctdbh.DonGiaBan,
                                                              GiaBan = ctdbh.DonGiaBan/ ctdbh.SoLuongBan,
                                                              TenDonVi = dv.TenDonVi,
                                                          }).ToList()
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
        private void btnThongKeXuatHang_XuatFile_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Lưu tệp Excel",
                FileName = "BaoCaoChiTietThongXuatNhapHang.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                ExportListViewToExcel(listViewThongKeXuatHang, filePath);
            }
        }
        private void ExportListViewToExcel(ListView listView, string filePath)
        {
            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("ThongKeDonBanHang");

                    // Đặt tiêu đề cho các cột
                    worksheet.Cells[1, 1].Value = "Mã phiếu";
                    worksheet.Cells[1, 2].Value = "Ngày bán";
                    worksheet.Cells[1, 3].Value = "Tên khách hàng";
                    worksheet.Cells[1, 4].Value = "Người thực hiện";
                    worksheet.Cells[1, 5].Value = "Tổng tiền";

                    // Thêm màu nền cho các tiêu đề
                    using (var range = worksheet.Cells[1, 1, 1, 5])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                        range.Style.Font.Bold = true;
                    }

                    int row = 2;
                    foreach (DonBanHang_MLV item in listView.Items)
                    {
                        worksheet.Cells[row, 1].Value = item.IdDonBanHang;
                        worksheet.Cells[row, 2].Value = item.NgayBan.HasValue ? item.NgayBan.Value.ToString("dd/MM/yyyy") : "N/A";
                        worksheet.Cells[row, 3].Value = item.TenKhachHang;
                        worksheet.Cells[row, 4].Value = item.TenHienThi;
                        worksheet.Cells[row, 5].Value = item.TongTienDonBanHang;

                        row++;

                        // Thêm chi tiết đơn nhập hàng
                        worksheet.Cells[row, 2].Value = "Tên Sản Phẩm";
                        worksheet.Cells[row, 3].Value = "Đơn Vị";
                        worksheet.Cells[row, 4].Value = "Số Lượng Bán";
                        worksheet.Cells[row, 5].Value = "Giá Bán";
                        worksheet.Cells[row, 6].Value = "Thành Tiền Bán";

                        using (var detailRange = worksheet.Cells[row, 2, row, 6])
                        {
                            detailRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            detailRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                            detailRange.Style.Font.Bold = true;
                        }

                        row++;

                        foreach (var detail in item.chiTietDonBanHang_MLVs)
                        {
                            worksheet.Cells[row, 2].Value = detail.TenSanPham;
                            worksheet.Cells[row, 3].Value = detail.TenDonVi;
                            worksheet.Cells[row, 4].Value = detail.SoLuongBan;
                            worksheet.Cells[row, 5].Value = detail.GiaBan;
                            worksheet.Cells[row, 6].Value = detail.DonGiaBan;
                            row++;
                        }
                    }
                    worksheet.Cells.AutoFitColumns();
                    FileInfo excelFile = new FileInfo(filePath);
                    excelPackage.SaveAs(excelFile);
                }

                MessageBox.Show("Dữ liệu đã được xuất thành công ra tệp Excel.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi xuất dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
