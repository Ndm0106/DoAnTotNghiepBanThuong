using DoAnTotNghiepBanThuong.Model;
using DoAnTotNghiepBanThuong.ViewUC;
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
using System.Windows.Shapes;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for SuaNhaPhanPhoi.xaml
    /// </summary>
    public partial class SuaNhaPhanPhoi : Window
    {
        private QLQuayThuocBanThuongContext _dbContext;
        private NhaPhanPhoi _NhaPhanPhoi;

        public SuaNhaPhanPhoi(QLQuayThuocBanThuongContext db, NhaPhanPhoi NhaPhanPhoi)
        {
            InitializeComponent();
            _dbContext = db;
            _NhaPhanPhoi = NhaPhanPhoi;
            txtSuaIdNhaPhanPhoi.Text = _NhaPhanPhoi.IdNhaPhanPhoi;
            txtSuaTenNhaPhanPhoi.Text = _NhaPhanPhoi.TenNhaPhanPhoi;
            txtSuaDiaChiNhaPhanPhoi.Text = _NhaPhanPhoi.DiaChi;
            txtSuaSoDienThoaiNhaPhanPhoi.Text = _NhaPhanPhoi.SoDienThoai;
            txtSuaEmailNhaPhanPhoi.Text = _NhaPhanPhoi.Email;
            txtSuaSoFaxNhaPhanPhoi.Text = _NhaPhanPhoi.Fax;
            txtSuaMaSoThueNhaPhanPhoi.Text = _NhaPhanPhoi.MaSoThue;
        }
        private void btnSuaNhaPhanPhoi_Sua(object sender, RoutedEventArgs e)
        {
            string suaTenNPP = txtSuaTenNhaPhanPhoi.Text.Trim();
            string suaDiaChiNPP = txtSuaDiaChiNhaPhanPhoi.Text;
            string suaSoDienThoaiNPP = txtSuaSoDienThoaiNhaPhanPhoi.Text;
            string suaSoFaxNPP = txtSuaSoFaxNhaPhanPhoi.Text;
            string suaEmailNPP = txtSuaEmailNhaPhanPhoi.Text;
            string suaMaSoThueNPP = txtSuaEmailNhaPhanPhoi.Text;
            if (string.IsNullOrEmpty(suaTenNPP))
            {
                MessageBox.Show("Không được để trống tên nhà cung cấp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _NhaPhanPhoi.TenNhaPhanPhoi = suaTenNPP;
            _NhaPhanPhoi.DiaChi = suaDiaChiNPP;
            _NhaPhanPhoi.SoDienThoai = suaSoDienThoaiNPP;
            _NhaPhanPhoi.Fax = suaSoFaxNPP;
            _NhaPhanPhoi.Email = suaEmailNPP;
            _NhaPhanPhoi.MaSoThue = suaMaSoThueNPP;
            _dbContext.SaveChanges();
            NhaPhanPhoiUC.listView.ItemsSource = _dbContext.NhaPhanPhois.ToList();
            //NhaPhanPhoiUC.listView.ItemsSource = _dbContext.NhaPhanPhois.ToList();
            MessageBox.Show("Sửa thành công", "Thông báo");
            this.Close();
        }
        private void btnSuaNhaPhanPhoi_Thoat(object sender, RoutedEventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát phiếu sửa", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
