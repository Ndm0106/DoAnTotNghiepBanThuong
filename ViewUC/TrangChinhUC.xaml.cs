using DoAnTotNghiepBanThuong.Model;
using LiveCharts.Wpf;
using LiveCharts;
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
using System.Globalization;

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for TrangChinhUC.xaml
    /// </summary>
    public partial class TrangChinhUC : UserControl
    {
        public TrangChinhUC()
        {
            InitializeComponent();
            LoadData();
            LoadStatistics();
        }
        private void LoadStatistics()
        {
            using (var dbContext = new QLQuayThuocBanThuongContext())
            {
                DateTime now = DateTime.Now;
                DateTime startOfThisMonth = new DateTime(now.Year, now.Month, 1);
                DateTime startOfLastMonth = startOfThisMonth.AddMonths(-1);

                // Doanh số
                var doanhSoThangTruoc = dbContext.DonBanHangs
                    .Where(db => db.NgayBan >= startOfLastMonth && db.NgayBan < startOfThisMonth)
                    .Sum(db => (decimal?)db.TongTienDonBanHang) ?? 0;

                var doanhSoThangNay = dbContext.DonBanHangs
                    .Where(db => db.NgayBan >= startOfThisMonth && db.NgayBan < now)
                    .Sum(db => (decimal?)db.TongTienDonBanHang) ?? 0;

                // Tổng số lượng nhập hàng
                var soLuongNhapThangTruoc = dbContext.DonNhapHangs
                    .Where(ctdn => ctdn.NgayNhap >= startOfLastMonth && ctdn.NgayNhap < startOfThisMonth)
                    .AsEnumerable()
                    .Count();

                var soLuongNhapThangNay = dbContext.DonNhapHangs
                    .Where(ctdn => ctdn.NgayNhap >= startOfThisMonth && ctdn.NgayNhap < now)
                    .AsEnumerable()
                    .Count();

                // Tổng số lượng bán hàng
                var soLuongBanThangTruoc = dbContext.DonBanHangs
                    .Where(ctdb => ctdb.NgayBan >= startOfLastMonth && ctdb.NgayBan < startOfThisMonth)
                    .AsEnumerable()
                    .Count();


                var soLuongBanThangNay = dbContext.DonBanHangs
                    .Where(ctdb => ctdb.NgayBan >= startOfThisMonth && ctdb.NgayBan < now)
                    .AsEnumerable()
                    .Count();

                // Cảnh báo sản phẩm
                var sapHetHan = dbContext.SanPhams
                    .Count(sp => sp.HanSuDung >= now && sp.HanSuDung < now.AddMonths(1));

                var hetHan = dbContext.SanPhams
                    .Count(sp => sp.HanSuDung < now);
                var vietnamCulture = new CultureInfo("vi-VN");
                // Hiển thị lên giao diện
                lblDoanhSoThangTruoc.Content = doanhSoThangTruoc.ToString("N0", vietnamCulture);
                lblDoanhSoThangNay.Content = doanhSoThangNay.ToString("N0", vietnamCulture);
                lblNhapHangThangTruoc.Content = soLuongNhapThangTruoc.ToString();
                lblNhapHangThangNay.Content = soLuongNhapThangNay.ToString();
                lblBanHangThangTruoc.Content = soLuongBanThangTruoc.ToString();
                lblBanHangThangNay.Content = soLuongBanThangNay.ToString();
                lblSapHetHan.Content = sapHetHan.ToString();
                lblHetHan.Content = hetHan.ToString();
            }
        }
        private void LoadData()
        {
            // Lấy dữ liệu doanh thu theo tháng từ cơ sở dữ liệu
            var soluongBanTheoThang = TinhSoLuongBansTheoThang();

            // Gán dữ liệu vào biểu đồ
            var soluongbanValues = new ChartValues<int>(soluongBanTheoThang.Select(dt => dt.SoLuongBan).ToArray());
            var labels = soluongBanTheoThang.Select(dt => dt.Thang).ToArray();

            SoLuongChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Số lượng",
                    Values = soluongbanValues,
                    DataLabels = true
                }
            };

            SoLuongChart.AxisX[0].Labels = labels;
        }

        private List<SoLuongTieuThuTheoThang> TinhSoLuongBansTheoThang()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var rawData = db.ChiTietDonBanHangs
                    .Join(db.DonBanHangs,
                          chiTiet => chiTiet.IdDonBanHang,
                          donBanHang => donBanHang.IdDonBanHang,
                          (chiTiet, donBanHang) => new { chiTiet, donBanHang })
                    .GroupBy(x => new { Year = x.donBanHang.NgayBan.Value.Year, Month = x.donBanHang.NgayBan.Value.Month })
                    .Select(g => new
                    {
                        g.Key.Year,
                        g.Key.Month,
                        SoLuongBan = g.Sum(x => x.chiTiet.SoLuongBan)
                    })
                    .OrderBy(g => g.Year)
                    .ThenBy(g => g.Month)
                    .ToList();

                var soluongBanTheoThang = rawData
                    .Select(data => new SoLuongTieuThuTheoThang
                    {
                        Thang = data.Month + "/" + data.Year,
                        SoLuongBan = (int)data.SoLuongBan,
                    })
                    .ToList();

                return soluongBanTheoThang;
            }
        }
        // Class để lưu trữ dữ liệu doanh thu theo tháng
        public class SoLuongTieuThuTheoThang
        {
            public string Thang { get; set; }
            public int SoLuongBan { get; set; }
        }
    }
}
