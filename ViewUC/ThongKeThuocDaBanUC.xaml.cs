using DoAnTotNghiepBanThuong.Model;
using Microsoft.Win32;
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

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for ThongKeThuocDaBanUC.xaml
    /// </summary>
    public partial class ThongKeThuocDaBanUC : UserControl
    {
        public ThongKeThuocDaBanUC()
        {
            InitializeComponent();
            LoadDataThongKeSanPhamDaBan();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private void LoadDataThongKeSanPhamDaBan()
        {
            using (var dbContext = new QLQuayThuocBanThuongContext())
            {
                var query = (
                    from ctdbh in dbContext.ChiTietDonBanHangs
                    join sp in dbContext.SanPhams on ctdbh.IdSanPham equals sp.IdSanPham
                    join dv in dbContext.DonVis on sp.IdDonVi equals dv.IdDonVi
                    group new { ctdbh, sp } by new { ctdbh.IdSanPham, sp.TenSanPham, dv.TenDonVi } into g
                    select new
                    {
                        IdSanPham = g.Key.IdSanPham,
                        TenSanPham = g.Key.TenSanPham,
                        TenDonVi = g.Key.TenDonVi,
                        TongSoLuongBan = g.Sum(x => x.ctdbh.SoLuongBan),
                        TongGiaBan = g.Sum(x => x.ctdbh.SoLuongBan * x.sp.GiaBan - x.ctdbh.ChietKhau)
                    }
                ).ToList();

                listViewThongKeThuocDaBan.ItemsSource = query;
            }
        }

        private void btnThuocDaBan_XuatFile_Click(object sender, RoutedEventArgs e)
        {
            var data = listViewThongKeThuocDaBan.Items.Cast<dynamic>().ToList();

            // Mở hộp thoại lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = "BaoCaoThuocDaBan.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Tạo file Excel
                using (var package = new ExcelPackage(new FileInfo("path")))
                {
                    // Tạo một worksheet mới
                    var worksheet = package.Workbook.Worksheets.Add("ThuocDaBan");

                    // Thiết lập tiêu đề cột
                    worksheet.Cells[1, 1].Value = "Mã sản phẩm";
                    worksheet.Cells[1, 2].Value = "Tên sản phẩm";
                    worksheet.Cells[1, 3].Value = "Đơn vị tính";
                    worksheet.Cells[1, 4].Value = "Tổng số lượng bán";
                    worksheet.Cells[1, 5].Value = "Tổng giá bán";
                    // Định dạng tiêu đề cột
                    using (var range = worksheet.Cells[1, 1, 1, 5])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                    }

                    // Thêm dữ liệu vào worksheet
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].IdSanPham;
                        worksheet.Cells[i + 2, 2].Value = data[i].TenSanPham;
                        worksheet.Cells[i + 2, 3].Value = data[i].TenDonVi;
                        worksheet.Cells[i + 2, 4].Value = data[i].TongSoLuongBan;
                        worksheet.Cells[i + 2, 5].Value = data[i].TongGiaBan;
                        
                    }

                    // Tự động điều chỉnh kích thước cột
                    worksheet.Cells.AutoFitColumns();

                    // Lưu file
                    var file = new System.IO.FileInfo(filePath);
                    package.SaveAs(file);

                    MessageBox.Show("Xuất báo cáo thuốc đã bán thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void txtThuocDaBan_TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtThuocDaBan_TimKiem.Text.Trim().ToLower();

            using (var dbContext = new QLQuayThuocBanThuongContext())
            {
                var query = (
                    from ctdbh in dbContext.ChiTietDonBanHangs
                    join sp in dbContext.SanPhams on ctdbh.IdSanPham equals sp.IdSanPham
                    join dv in dbContext.DonVis on sp.IdDonVi equals dv.IdDonVi
                    group new { ctdbh, sp } by new { ctdbh.IdSanPham, sp.TenSanPham, dv.TenDonVi } into g
                    select new
                    {
                        IdSanPham = g.Key.IdSanPham,
                        TenSanPham = g.Key.TenSanPham,
                        TenDonVi = g.Key.TenDonVi,
                        TongSoLuongBan = g.Sum(x => x.ctdbh.SoLuongBan),
                        TongGiaBan = g.Sum(x => x.ctdbh.SoLuongBan * x.sp.GiaBan - x.ctdbh.ChietKhau)
                    }
                );

                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(p => p.TenSanPham.ToLower().Contains(searchText));
                }

                var filteredList = query.ToList();
                listViewThongKeThuocDaBan.ItemsSource = filteredList;
            }
        }
    }
}
