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

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for NhaPhanPhoiUC.xaml
    /// </summary>
    public partial class NhaPhanPhoiUC : UserControl
    {
        private QLQuayThuocBanThuongContext db;
        public static System.Windows.Controls.ListView? listView;
        public NhaPhanPhoiUC()
        {
            InitializeComponent();
            db = new QLQuayThuocBanThuongContext();
            LoadDataNhaPhanPhoi();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        private void LoadDataNhaPhanPhoi()
        {
            var query = (from npp in db.NhaPhanPhois
                         select new NhaPhanPhoi_MLV
                         {
                             IdNhaPhanPhoi = npp.IdNhaPhanPhoi,
                             TenNhaPhanPhoi = npp.TenNhaPhanPhoi,
                             DiaChi = npp.DiaChi,
                             Fax = npp.Fax,
                             SoDienThoai = npp.SoDienThoai,
                             Email = npp.Email,
                             MaSoThue = npp.MaSoThue,   

                         }).ToList();
            listViewNhaPhanPhoi.ItemsSource = query;
            listView = listViewNhaPhanPhoi;
        }
        private void ThemNhaPhanPhoiWindow_Click(object sender, RoutedEventArgs e)
        {
            var themNhaPhanPhoi = new ThemNhaPhanPhoi(db);
            themNhaPhanPhoi.Show();
            LoadDataNhaPhanPhoi();
        }
        private void SuaNhaPhanPhoiWindow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                NhaPhanPhoi_MLV NhaPhanPhoi = (NhaPhanPhoi_MLV)button.DataContext;
                if (NhaPhanPhoi != null)
                {
                    var suaNhaPhanPhoi = new SuaNhaPhanPhoi(db, NhaPhanPhoi);
                    suaNhaPhanPhoi.Show();
                    LoadDataNhaPhanPhoi();
                }
            }
        }
        private void XoaNhaPhanPhoi_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var nhaphanphoi = (NhaPhanPhoi_MLV)button.DataContext;
                if (nhaphanphoi != null)
                {
                    bool isUsedInDNH = db.DonNhapHangs.Any(s => s.IdNhaPhanPhoi == nhaphanphoi.IdNhaPhanPhoi);
                    if (isUsedInDNH)
                    {
                        MessageBox.Show("Không thể xoá nhà phân phối này vì nó đã được sử dụng trong ít nhất một đơn nhập hàng.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xoá nhà phân phối này ?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            var itemToRemove = db.NhaPhanPhois.FirstOrDefault(s => s.IdNhaPhanPhoi == nhaphanphoi.IdNhaPhanPhoi);
                            if (itemToRemove != null)
                            {
                                db.NhaPhanPhois.Remove(itemToRemove);
                                db.SaveChanges();
                                LoadDataNhaPhanPhoi();
                            }    
                        }
                    }
                }
            }
        }

        private void txtNhaPhanPhoi_TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtNhaPhanPhoi_TimKiem.Text.Trim().ToLower();

            // Nếu không có ký tự nào trong ô tìm kiếm, hiển thị tất cả các đơn vị
            if (string.IsNullOrEmpty(searchText))
            {
                listViewNhaPhanPhoi.ItemsSource = db.NhaPhanPhois.Select(npp => new NhaPhanPhoi_MLV
                {
                    IdNhaPhanPhoi = npp.IdNhaPhanPhoi,
                    TenNhaPhanPhoi = npp.TenNhaPhanPhoi,
                    DiaChi = npp.DiaChi,
                    Fax = npp.Fax,
                    SoDienThoai = npp.SoDienThoai,
                    Email = npp.Email,
                    MaSoThue = npp.MaSoThue,
                }).ToList();
            }
            else
            {
                // Lọc danh sách các đơn vị dựa trên từ khóa tìm kiếm
                var filteredDonViList = db.NhaPhanPhois.Where(npp => npp.TenNhaPhanPhoi.ToLower().Contains(searchText))
                                                   .Select(npp => new NhaPhanPhoi_MLV
                                                   {
                                                       IdNhaPhanPhoi = npp.IdNhaPhanPhoi,
                                                       TenNhaPhanPhoi = npp.TenNhaPhanPhoi,
                                                       DiaChi = npp.DiaChi,
                                                       Fax = npp.Fax,
                                                       SoDienThoai = npp.SoDienThoai,
                                                       Email = npp.Email,
                                                       MaSoThue = npp.MaSoThue,
                                                   }).ToList();

                // Hiển thị danh sách các đơn vị được lọc
                listViewNhaPhanPhoi.ItemsSource = filteredDonViList;
            }
        }

        private void btnNhaPhanPhoi_XuatFile_Click(object sender, RoutedEventArgs e)
        {
            var data = listViewNhaPhanPhoi.Items.Cast<dynamic>().ToList();

            // Mở hộp thoại lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = "DanhSachNhaPhanPhoi.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Tạo file Excel
                using (var package = new ExcelPackage(new FileInfo("path")))
                {
                    // Tạo một worksheet mới
                    var worksheet = package.Workbook.Worksheets.Add("NhaPhanPhoi");

                    // Thiết lập tiêu đề cột
                    worksheet.Cells[1, 1].Value = "Mã nhà phân phối";
                    worksheet.Cells[1, 2].Value = "Tên nhà phân phối";
                    worksheet.Cells[1, 3].Value = "Địa chỉ";
                    worksheet.Cells[1, 4].Value = "Số Fax";
                    worksheet.Cells[1, 5].Value = "Số điện thoại";
                    worksheet.Cells[1, 6].Value = "Email";
                    worksheet.Cells[1, 7].Value = "Mã số thuế";
                    // Định dạng tiêu đề cột
                    using (var range = worksheet.Cells[1, 1, 1, 7])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                    }

                    // Thêm dữ liệu vào worksheet
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].IdNhaPhanPhoi;
                        worksheet.Cells[i + 2, 2].Value = data[i].TenNhaPhanPhoi;
                        worksheet.Cells[i + 2, 3].Value = data[i].DiaChi;
                        worksheet.Cells[i + 2, 4].Value = data[i].Fax;
                        worksheet.Cells[i + 2, 5].Value = data[i].SoDienThoai;
                        worksheet.Cells[i + 2, 6].Value = data[i].Email;
                        worksheet.Cells[i + 2, 7].Value = data[i].MaSoThue;
                    }

                    // Tự động điều chỉnh kích thước cột
                    worksheet.Cells.AutoFitColumns();

                    // Lưu file
                    var file = new System.IO.FileInfo(filePath);
                    package.SaveAs(file);

                    MessageBox.Show("Xuất danh sách nhà phân phối thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
