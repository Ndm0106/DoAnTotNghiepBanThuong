using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ModelListView;
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
    /// Interaction logic for ThongKeXuatHangUC.xaml
    /// </summary>
    public partial class ThongKeXuatHangUC : UserControl
    {
        public ThongKeXuatHangUC()
        {
            InitializeComponent();
            LoadDataThongKeXuatHang();
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
                listViewThongKeXuatHang.ItemsSource = query.ToList();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewThongKeXuatHang.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChiTietDonBanHang_MLV");
                view.GroupDescriptions.Add(groupDescription);

            }
        }
    }
}
