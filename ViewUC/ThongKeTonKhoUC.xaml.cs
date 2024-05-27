using DoAnTotNghiepBanThuong.Model;
using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml;
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
using System.IO;

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for ThongKeTonKhoUC.xaml
    /// </summary>
    public partial class ThongKeTonKhoUC : UserControl
    {
        public static System.Windows.Controls.ListView? listView;
        public ThongKeTonKhoUC()
        {
            InitializeComponent();
            LoadDataTonKho();
            

            // Trong hàm Main hoặc hàm khởi tạo
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        private void LoadDataTonKho()
        {
            using (var dbContext = new QLQuayThuocBanThuongContext())
            {
                var productInventory = (
    from sp in dbContext.SanPhams
    join dv in dbContext.DonVis on sp.IdDonVi equals dv.IdDonVi
    join nsp in dbContext.NhomSanPhams on sp.IdNhomSanPham equals nsp.IdNhomSanPham
    select new
    {
        sp.IdSanPham,
        sp.TenSanPham,
        sp.SoLuongTon,
        TenDonVi = dv.TenDonVi,
        TenNhomSanPham = nsp.TenNhomSanPham,
        GiaNhap = sp.GiaNhap,
        GiaBan = sp.GiaBan,
        SoLuongNhap = dbContext.ChiTietDonNhapHangs.Where(ctpn => ctpn.IdSanPham == sp.IdSanPham).Sum(ctpn => ctpn.SoLuongNhap),
        SoLuongBan = dbContext.ChiTietDonBanHangs.Where(ctdb => ctdb.IdSanPham == sp.IdSanPham).Sum(ctdb => ctdb.SoLuongBan)
    })
    .Select(p => new
    {
        p.IdSanPham,
        p.TenSanPham,
        p.TenDonVi,
        p.TenNhomSanPham,
        SoLuongTonKho = p.SoLuongNhap + p.SoLuongTon  - p.SoLuongBan,
       
    })
    .ToList();

                listViewThongKeTonKho.ItemsSource = productInventory;
            }
        }

        private void btnTonKho_XuatFile_Click(object sender, RoutedEventArgs e)
        {
            var data = listViewThongKeTonKho.Items.Cast<dynamic>().ToList();

            // Mở hộp thoại lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = "BaoCaoTonKho.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Tạo file Excel
                using (var package = new ExcelPackage(new FileInfo("path")))
                {
                    // Tạo một worksheet mới
                    var worksheet = package.Workbook.Worksheets.Add("TonKho");

                    // Thiết lập tiêu đề cột
                    worksheet.Cells[1, 1].Value = "Mã sản phẩm";
                    worksheet.Cells[1, 2].Value = "Tên sản phẩm";
                    worksheet.Cells[1, 3].Value = "ĐVT";
                    worksheet.Cells[1, 4].Value = "Tồn kho";
                    worksheet.Cells[1, 5].Value = "Nhóm sản phẩm";

                    // Định dạng tiêu đề cột
                    using (var range = worksheet.Cells[1, 1, 1, 5])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    // Thêm dữ liệu vào worksheet
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].IdSanPham;
                        worksheet.Cells[i + 2, 2].Value = data[i].TenSanPham;
                        worksheet.Cells[i + 2, 3].Value = data[i].TenDonVi;
                        worksheet.Cells[i + 2, 4].Value = data[i].SoLuongTonKho;
                        worksheet.Cells[i + 2, 5].Value = data[i].TenNhomSanPham;
                    }

                    // Tự động điều chỉnh kích thước cột
                    worksheet.Cells.AutoFitColumns();

                    // Lưu file
                    var file = new System.IO.FileInfo(filePath);
                    package.SaveAs(file);

                    MessageBox.Show("Xuất báo cáo tồn kho thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
