using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewWindow;
using Microsoft.Win32;
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
    /// Interaction logic for KhachHangUC.xaml
    /// </summary>
    public partial class KhachHangUC : UserControl
    {
        private QLQuayThuocBanThuongContext db;
        public static System.Windows.Controls.ListView? listView;
        public KhachHangUC()
        {
            InitializeComponent();
            db = new QLQuayThuocBanThuongContext();
            LoadDataKhachHang();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        private void LoadDataKhachHang()
        {
            var query = (from kh in db.KhachHangs
                         select new KhachHang_MLV
                         {
                             IdKhachHang = kh.IdKhachHang,
                             TenKhachHang = kh.TenKhachHang,
                             DiaChi = kh.DiaChi,
                             SoDienThoai = kh.SoDienThoai,
                             Email = kh.Email,
                         }).ToList();
            listViewKhachHang.ItemsSource = query;
            listView = listViewKhachHang;
        }
        private void ThemKhachHangWindow_Click(object sender, RoutedEventArgs e)
        {
            var themKhachHang = new ThemKhachHang(db);
            themKhachHang.Show();
            LoadDataKhachHang();
        }
        private void SuaKhachHangWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null )
            {
                var khachhang = button.DataContext as KhachHang_MLV;
                if( khachhang != null )
                {
                    var suaKhachHang = new SuaKhachHang(db, khachhang);
                    suaKhachHang.Show();
                    LoadDataKhachHang();
                }    
            }    
        }
        private void XoaKhachHang_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var khachhang = (KhachHang_MLV)button.DataContext;
                if (khachhang != null)
                {
                    bool isUsedInDBH = db.DonBanHangs.Any(s => s.IdKhachHang == khachhang.IdKhachHang);
                    if (isUsedInDBH)
                    {
                        MessageBox.Show("Không thể xoá khách hàng này vì nó đã có đơn bán hàng.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá khách hàng này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            var itemToRemove = db.KhachHangs.FirstOrDefault(s => s.IdKhachHang == khachhang.IdKhachHang);
                            if (itemToRemove != null)
                            {
                                db.KhachHangs.Remove(itemToRemove);
                                db.SaveChanges();
                                LoadDataKhachHang();
                            }
                        }
                    }
                }
            }
        }

        private void txtKhachHang_TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtKhachHang_TimKiem.Text.Trim().ToLower();

            // Nếu không có ký tự nào trong ô tìm kiếm, hiển thị tất cả các đơn vị
            if (string.IsNullOrEmpty(searchText))
            {
                listViewKhachHang.ItemsSource = db.KhachHangs.Select(kh => new KhachHang_MLV
                {
                    IdKhachHang = kh.IdKhachHang,
                    TenKhachHang = kh.TenKhachHang,
                    DiaChi = kh.DiaChi,
                    SoDienThoai = kh.SoDienThoai,
                    Email = kh.Email,
                }).ToList();
            }
            else
            {
                // Lọc danh sách các đơn vị dựa trên từ khóa tìm kiếm
                var filteredDonViList = db.KhachHangs.Where(kh => kh.TenKhachHang.ToLower().Contains(searchText))
                                                   .Select(kh => new KhachHang_MLV
                                                   {
                                                       IdKhachHang = kh.IdKhachHang,
                                                       TenKhachHang = kh.TenKhachHang,
                                                       DiaChi = kh.DiaChi,
                                                       SoDienThoai = kh.SoDienThoai,
                                                       Email = kh.Email,
                                                   }).ToList();

                // Hiển thị danh sách các đơn vị được lọc
                listViewKhachHang.ItemsSource = filteredDonViList;
            }
        }

        private void btnKhachHang_XuatFile_Click(object sender, RoutedEventArgs e)
        {
            var data = listViewKhachHang.Items.Cast<dynamic>().ToList();

            // Mở hộp thoại lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = "DanhSachKhachHang.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Tạo file Excel
                using (var package = new ExcelPackage(new FileInfo("path")))
                {
                    // Tạo một worksheet mới
                    var worksheet = package.Workbook.Worksheets.Add("KhachHang");

                    // Thiết lập tiêu đề cột
                    worksheet.Cells[1, 1].Value = "Mã khách hàng";
                    worksheet.Cells[1, 2].Value = "Tên khách hàng";
                    worksheet.Cells[1, 3].Value = "Địa chỉ";
                    worksheet.Cells[1, 4].Value = "Số điện thoại";
                    worksheet.Cells[1, 5].Value = "Email";

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
                        worksheet.Cells[i + 2, 1].Value = data[i].IdKhachHang;
                        worksheet.Cells[i + 2, 2].Value = data[i].TenKhachHang;
                        worksheet.Cells[i + 2, 3].Value = data[i].DiaChi;
                        worksheet.Cells[i + 2, 4].Value = data[i].SoDienThoai;
                        worksheet.Cells[i + 2, 5].Value = data[i].Email;
                    }

                    // Tự động điều chỉnh kích thước cột
                    worksheet.Cells.AutoFitColumns();

                    // Lưu file
                    var file = new System.IO.FileInfo(filePath);
                    package.SaveAs(file);

                    MessageBox.Show("Xuất danh sách khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
