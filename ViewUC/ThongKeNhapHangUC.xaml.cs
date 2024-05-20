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
        from dbh in dbContext.DonNhapHangs
        join npp in dbContext.NhaPhanPhois on dbh.IdNhaPhanPhoi equals npp.IdNhaPhanPhoi
        join nv in dbContext.NhanViens on dbh.IdNhanVien equals nv.IdNhanVien
        select new DonNhapHang_MLV
        {
            IdDonNhapHang = dbh.IdDonNhapHang,
            NgayNhap = dbh.NgayNhap,
            TenNhaPhanPhoi = npp.TenNhaPhanPhoi,
            TenHienThi = nv.TenHienThi,
            chiTietDonNhapHang_MLVs = (
                from ctdnh in dbContext.ChiTietDonNhapHangs
                join sp in dbContext.SanPhams on ctdnh.IdSanPham equals sp.IdSanPham
                join dv in dbContext.DonVis on sp.IdDonVi equals dv.IdDonVi
                where ctdnh.IdDonNhapHang == dbh.IdDonNhapHang
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
    }
}
