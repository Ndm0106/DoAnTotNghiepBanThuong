using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using DoAnTotNghiepBanThuong.ViewWindow;
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
using XAct.Library.Settings;

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for NhaSanXuatUC.xaml
    /// </summary>
    public partial class NhaSanXuatUC : UserControl
    {
        private QLQuayThuocBanThuongContext db;
        public static System.Windows.Controls.ListView? listView;
        public NhaSanXuatUC()
        {
            InitializeComponent();
            db = new QLQuayThuocBanThuongContext();
            LoadDataNhaSanXuat();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        private void LoadDataNhaSanXuat()
        {
            var query = (from nsx in db.NhaSanXuats
                         select new NhaSanXuat_MLV
                         {
                             IdNhaSanXuat = nsx.IdNhaSanXuat,
                             TenNhaSanXuat = nsx.TenNhaSanXuat,
                             DiaChi = nsx.DiaChi,
                             Fax = nsx.Fax,
                             SoDienThoai = nsx.SoDienThoai,
                             Email = nsx.Email,

                         }).ToList();
            listViewNhaSanXuat.ItemsSource = query;
            listView = listViewNhaSanXuat;
        }
        private void ThemNhaSanXuatWindow_Click(object sender, RoutedEventArgs e)
        {
            var themNhaSanXuat = new ThemNhaSanXuat(db);
            themNhaSanXuat.Show();
            LoadDataNhaSanXuat();
        }
        private void SuaNhaSanXuatWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                NhaSanXuat_MLV NhaSanXuat = (NhaSanXuat_MLV)button.DataContext;
                if (NhaSanXuat != null)
                {
                    var suaNhaSanXuat = new SuaNhaSanXuat(db, NhaSanXuat);
                    suaNhaSanXuat.Show();
                    LoadDataNhaSanXuat();
                }
            }
        }
        private void XoaNhaSanXuat_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var nhasanxuat = (NhaSanXuat_MLV)button.DataContext;
                if (nhasanxuat != null)
                {
                    bool isUsedInProducts = db.SanPhams.Any(s => s.IdNhaSanXuat == nhasanxuat.IdNhaSanXuat);
                    if (isUsedInProducts)
                    {
                        MessageBox.Show("Không thể xoá nhà sản xuất này vì nó đã được sử dụng trong ít nhất một sản phẩm.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá nhà sản xuất này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            var itemToRemove = db.NhaSanXuats.FirstOrDefault(s => s.IdNhaSanXuat == nhasanxuat.IdNhaSanXuat);
                            if (itemToRemove != null)
                            {
                                db.NhaSanXuats.Remove(itemToRemove);
                                db.SaveChanges();
                                LoadDataNhaSanXuat();
                            }
                        }
                    } 
                }
            }
        }

        private void txtNhaSanXuat_TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtNhaSanXuat_TimKiem.Text.Trim().ToLower();

            // Nếu không có ký tự nào trong ô tìm kiếm, hiển thị tất cả các đơn vị
            if (string.IsNullOrEmpty(searchText))
            {
                listViewNhaSanXuat.ItemsSource = db.NhaSanXuats.Select(nsx => new NhaSanXuat_MLV
                {
                    IdNhaSanXuat = nsx.IdNhaSanXuat,
                    TenNhaSanXuat = nsx.TenNhaSanXuat,
                    DiaChi = nsx.DiaChi,
                    Fax = nsx.Fax,
                    SoDienThoai = nsx.SoDienThoai,
                    Email = nsx.Email,
                }).ToList();
            }
            else
            {
                // Lọc danh sách các đơn vị dựa trên từ khóa tìm kiếm
                var filteredDonViList = db.NhaSanXuats.Where(nsx => nsx.TenNhaSanXuat.ToLower().Contains(searchText))
                                                   .Select(nsx => new NhaSanXuat_MLV
                                                   {
                                                       IdNhaSanXuat = nsx.IdNhaSanXuat,
                                                       TenNhaSanXuat = nsx.TenNhaSanXuat,
                                                       DiaChi = nsx.DiaChi,
                                                       Fax = nsx.Fax,
                                                       SoDienThoai = nsx.SoDienThoai,
                                                       Email = nsx.Email,
                                                   }).ToList();

                // Hiển thị danh sách các đơn vị được lọc
                listViewNhaSanXuat.ItemsSource = filteredDonViList;
            }
        }

        private void btnNhaSanXuat_XuatFile_Click(object sender, RoutedEventArgs e)
        {
            var data = listViewNhaSanXuat.Items.Cast<dynamic>().ToList();

            // Mở hộp thoại lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = "DanhSachNhaSanXuat.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Tạo file Excel
                using (var package = new ExcelPackage(new FileInfo("path")))
                {
                    // Tạo một worksheet mới
                    var worksheet = package.Workbook.Worksheets.Add("NhaSanXuat");

                    // Thiết lập tiêu đề cột
                    worksheet.Cells[1, 1].Value = "Mã nhà sản xuất";
                    worksheet.Cells[1, 2].Value = "Tên nhà sản xuất";
                    worksheet.Cells[1, 3].Value = "Địa chỉ";
                    worksheet.Cells[1, 4].Value = "Số Fax";
                    worksheet.Cells[1, 5].Value = "Số điện thoại";
                    worksheet.Cells[1, 6].Value = "Email";

                    // Định dạng tiêu đề cột
                    using (var range = worksheet.Cells[1, 1, 1, 6])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                    }

                    // Thêm dữ liệu vào worksheet
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].IdNhaSanXuat;
                        worksheet.Cells[i + 2, 2].Value = data[i].TenNhaSanXuat;
                        worksheet.Cells[i + 2, 3].Value = data[i].DiaChi;
                        worksheet.Cells[i + 2, 4].Value = data[i].Fax;
                        worksheet.Cells[i + 2, 5].Value = data[i].SoDienThoai;
                        worksheet.Cells[i + 2, 6].Value = data[i].Email;
                    }

                    // Tự động điều chỉnh kích thước cột
                    worksheet.Cells.AutoFitColumns();

                    // Lưu file
                    var file = new System.IO.FileInfo(filePath);
                    package.SaveAs(file);

                    MessageBox.Show("Xuất danh sách nhà sản xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
