using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for ThongKeNhapHangUC.xaml
    /// </summary>
    public partial class ThongKeNhapHangUC : UserControl
    {
        public ThongKeNhapHangUC()
        {
            InitializeComponent();
            LoadDataThongKeNhapHang();
        }
        private void LoadDataThongKeNhapHang()
        {
            using (var dbContext = new QLQuayThuocBanThuongContext())
            {
                var query = (
        from dnh in dbContext.DonNhapHangs
        join npp in dbContext.NhaPhanPhois on dnh.IdNhaPhanPhoi equals npp.IdNhaPhanPhoi
        join nv in dbContext.NhanViens on dnh.IdNhanVien equals nv.IdNhanVien
        select new DonNhapHang_MLV
        {
            IdDonNhapHang = dnh.IdDonNhapHang,
            NgayNhap = dnh.NgayNhap,
            TenNhaPhanPhoi = npp.TenNhaPhanPhoi,
            TenHienThi = nv.TenHienThi,
            chiTietDonNhapHang_MLVs = (
                from ctdnh in dbContext.ChiTietDonNhapHangs
                join sp in dbContext.SanPhams on ctdnh.IdSanPham equals sp.IdSanPham
                join dv in dbContext.DonVis on sp.IdDonVi equals dv.IdDonVi
                where ctdnh.IdDonNhapHang == dnh.IdDonNhapHang
                select new ChiTietDonNhapHang_MLV
                {
                    TenSanPham = sp.TenSanPham,
                    SoLuongNhap = ctdnh.SoLuongNhap,
                    DonGiaNhap = ctdnh.SoLuongNhap * sp.GiaNhap,
                    GiaNhap = sp.GiaNhap,
                    TenDonVi = dv.TenDonVi,
                }
            ).ToList()
        }
    ).ToList();

                foreach (var item in query)
                {
                    var totalAmount = 0M;
                    foreach (var detailItem in item.chiTietDonNhapHang_MLVs)
                    {

                        totalAmount += (decimal)detailItem.DonGiaNhap;

                    }
                    item.TongTienDonNhapHang = totalAmount;
                    // Thêm tổng số lượng và tổng tiền vào mỗi mục chi tiết phiếu nhập

                }
                listViewThongKeNhapHang.ItemsSource = query.ToList();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewThongKeNhapHang.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChiTietDonNhapHang_MLV");
                view.GroupDescriptions.Add(groupDescription);

            }
        }

        private void btnThongKeNhapHang_XuatFile_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Lưu tệp Excel"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                ExportListViewToExcel(listViewThongKeNhapHang, filePath);
            }
        }
        private void ExportListViewToExcel(ListView listView, string filePath)
        {
            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Data");

                    // Đặt tiêu đề cho các cột
                    worksheet.Cells[1, 1].Value = "Mã phiếu";
                    worksheet.Cells[1, 2].Value = "Ngày nhập";
                    worksheet.Cells[1, 3].Value = "TênNCC";
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
                    foreach (DonNhapHang_MLV item in listView.Items)
                    {
                        worksheet.Cells[row, 1].Value = item.IdDonNhapHang;
                        worksheet.Cells[row, 2].Value = item.NgayNhap.HasValue ? item.NgayNhap.Value.ToString("dd/MM/yyyy") : "N/A";
                        worksheet.Cells[row, 3].Value = item.TenNhaPhanPhoi;
                        worksheet.Cells[row, 4].Value = item.TenHienThi;
                        worksheet.Cells[row, 5].Value = item.TongTienDonNhapHang;

                        row++;

                        // Thêm chi tiết đơn nhập hàng
                        worksheet.Cells[row, 2].Value = "Tên Sản Phẩm";
                        worksheet.Cells[row, 3].Value = "Đơn Vị";
                        worksheet.Cells[row, 4].Value = "Số Lượng Nhập";
                        worksheet.Cells[row, 5].Value = "Giá Nhập";
                        worksheet.Cells[row, 6].Value = "Thành Tiền Nhập";

                        using (var detailRange = worksheet.Cells[row, 2, row, 6])
                        {
                            detailRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            detailRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                            detailRange.Style.Font.Bold = true;
                        }

                        row++;

                        foreach (var detail in item.chiTietDonNhapHang_MLVs)
                        {
                            worksheet.Cells[row, 2].Value = detail.TenSanPham;
                            worksheet.Cells[row, 3].Value = detail.TenDonVi;
                            worksheet.Cells[row, 4].Value = detail.SoLuongNhap;
                            worksheet.Cells[row, 5].Value = detail.GiaNhap;
                            worksheet.Cells[row, 6].Value = detail.DonGiaNhap;
                            row++;
                        }
                    }

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
