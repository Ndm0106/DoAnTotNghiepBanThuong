using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
using LiveCharts;
using LiveCharts.Wpf;
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

namespace DoAnTotNghiepBanThuong.ViewUC
{
    /// <summary>
    /// Interaction logic for ThongKeDoanhThuUC.xaml
    /// </summary>
    public partial class ThongKeDoanhThuUC : UserControl
    {
        
        public ThongKeDoanhThuUC()
        {
            InitializeComponent();
            LoadData();
            
        }
        private void LoadData()
        {
            // Lấy dữ liệu doanh thu theo tháng từ cơ sở dữ liệu
            var doanhThuTheoThang = TinhDoanhThuTheoThang();

            // Gán dữ liệu vào biểu đồ
            var doanhThuValues = new ChartValues<double>(doanhThuTheoThang.Select(dt => dt.DoanhThu).ToArray());
            var labels = doanhThuTheoThang.Select(dt => dt.Thang).ToArray();

            DoanhThuChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Doanh Thu",
                    Values = doanhThuValues,
                    DataLabels = true
                }
            };

            DoanhThuChart.AxisX[0].Labels = labels;
        }

        private List<DoanhThuThang> TinhDoanhThuTheoThang()
        {
            using (var db = new QLQuayThuocBanThuongContext())
            {
                var rawData = db.DonBanHangs
                    .GroupBy(dh => new { Year = dh.NgayBan.Value.Year, Month = dh.NgayBan.Value.Month })
                    .Select(g => new
                    {
                        g.Key.Year,
                        g.Key.Month,
                        DoanhThu = g.Sum(dh => dh.TongTienDonBanHang)
                    })
                    .OrderBy(g => g.Year)
                    .ThenBy(g => g.Month)
                    .ToList();

                var doanhThuTheoThang = rawData
                    .Select(data => new DoanhThuThang
                    {
                        Thang = data.Month + "/" + data.Year,
                        DoanhThu = (double)data.DoanhThu
                    })
                    .ToList();

                return doanhThuTheoThang;
                
            }
        }
        // Class để lưu trữ dữ liệu doanh thu theo tháng
        public class DoanhThuThang
        {
            public string Thang { get; set; }
            public double DoanhThu { get; set; }
        }
    }

}
