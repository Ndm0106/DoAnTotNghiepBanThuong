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
using System.Windows.Shapes;

namespace DoAnTotNghiepBanThuong.ViewWindow
{
    /// <summary>
    /// Interaction logic for CapNhatMatKhau.xaml
    /// </summary>
    public partial class CapNhatMatKhau : Window
    {
        NhanVien_MLV _selectedNV;
        public CapNhatMatKhau(NhanVien_MLV selectedNV)
        {
            InitializeComponent();
            _selectedNV = selectedNV;
        }
        private void btnCapNhatMatKhauNhanVien_DoiMatKhau(object sender, EventArgs e)
        {

        }
        private void btnCapNhatMatKhauNhanVien_Thoat(object sender, EventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (thongbao == MessageBoxResult.OK)
                this.Close();
        }
    }
}
