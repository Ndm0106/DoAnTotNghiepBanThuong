
use QLQuayThuocBanThuong
go

create table ChucVu
(
IdChucVu int identity(1,1) primary key,
TenChucVu nvarchar(100) not null
)
go

insert into ChucVu(TenChucVu)values(N'admin')
insert into ChucVu(TenChucVu)values(N'nhân viên')


create table NhanVien
(
IdNhanVien nvarchar(128) primary key,
TenHienThi nvarchar(max) not null,
TaiKhoan nvarchar(100) ,
MatKhau nvarchar(max) ,
SoDienThoai nvarchar(20),
Email nvarchar(200),
IdChucVu int not null 

foreign key(IdChucVu) references ChucVu(IdChucVu)
)

insert into NhanVien(IdNhanVien,TenHienThi,TaiKhoan,MatKhau,SoDienThoai,Email,IdChucVu)values(N'NV001',N'Đình Mạnh',N'admin',N'db69fc039dcbd2962cb4d28f5891aae1',N'0393972501',N'dinhmanh525@gmail.com',1)
insert into NhanVien(IdNhanVien,TenHienThi,TaiKhoan,MatKhau,SoDienThoai,Email,IdChucVu)values(N'NV002',N'Đình Tiến',N'nhanvien',N'32035964ea350cc45ca333216c4a59ff',N'0393972501',N'dinhtien676@gmail.com',2)

go
create table DonVi
(
IdDonVi nvarchar(128) primary key,
TenDonVi nvarchar(100) not null
)

insert into DonVi(IdDonVi,TenDonVi)values(N'DV001',N'Thùng')
insert into DonVi(IdDonVi,TenDonVi)values(N'DV002',N'Viên')
insert into DonVi(IdDonVi,TenDonVi)values(N'DV003',N'Nang')
insert into DonVi(IdDonVi,TenDonVi)values(N'DV004',N'Lọ')
insert into DonVi(IdDonVi,TenDonVi)values(N'DV005',N'Cái')
insert into DonVi(IdDonVi,TenDonVi)values(N'DV006',N'Vòng')
insert into DonVi(IdDonVi,TenDonVi)values(N'DV007',N'Gói nhỏ')
insert into DonVi(IdDonVi,TenDonVi)values(N'DV008',N'Liều')

go
create table NhaSanXuat
(

IdNhaSanXuat  nvarchar(128) primary key,
TenNhaSanXuat nvarchar(max) not null,
DiaChi nvarchar(max),
Fax nvarchar(20),
SoDienThoai nvarchar(20),
Email nvarchar(200)
)

insert into NhaSanXuat(IdNhaSanXuat,TenNhaSanXuat,DiaChi,SoDienThoai,Email,Fax)values(N'NSX0001',N'Công ty CP Dược Hậu Giang',N'288 Bis - Nguyễn Văn Cừ - Phường An Hòa - Quận Ninh Kiều - TP. Cần Thơ',N'0292-3891433',N'dhgpharma@dhgpharma.com.vn',N'')
insert into NhaSanXuat(IdNhaSanXuat,TenNhaSanXuat,DiaChi,SoDienThoai,Email,Fax)values(N'NSX0002',N'Công ty CP Traphaco',N'75 Yên Ninh - Phường Quán Thánh - Quận Ba Đình - TP. Hà Nội',N'18006612',N'info@traphaco.com.vn',N'')
insert into NhaSanXuat(IdNhaSanXuat,TenNhaSanXuat,DiaChi,SoDienThoai,Email,Fax)values(N'NSX0003',N'Công ty TNHH Sanofi – Aventis Việt Nam',N'Số 10 Hàm Nghi, Quận 1, TPHCM',N'0435-371-835',N'sanofi@sanofi.com.vn',N'')
insert into NhaSanXuat(IdNhaSanXuat,TenNhaSanXuat,DiaChi,SoDienThoai,Email,Fax)values(N'NSX0004',N'Công ty CP Dược phẩm Imexpharm',N'Số 4, Đường 30/4 - Phường 1 - TP Cao Lãnh - Tỉnh Đồng Tháp',N'0277-3851941',N'imp@imexpharm.com',N'')
insert into NhaSanXuat(IdNhaSanXuat,TenNhaSanXuat,DiaChi,SoDienThoai,Email,Fax)values(N'NSX0005',N'Công ty CP Dược – Trang thiết bị y tế Bình Định',N'498 Nguyễn Thái Học - Phường Quang Trung - TP. Quy Nhơn - Tỉnh Bình Định',N'0256-3846500',N'info@bidiphar.com',N'')
insert into NhaSanXuat(IdNhaSanXuat,TenNhaSanXuat,DiaChi,SoDienThoai,Email,Fax)values(N'NSX0006',N'Công ty CP Pymepharco',N'166-170 Nguyễn Huệ, TP. Tuy Hoà, Tỉnh Phú Yên',N'0257-3829165',N'hcns@pymepharco.com',N'')
insert into NhaSanXuat(IdNhaSanXuat,TenNhaSanXuat,DiaChi,SoDienThoai,Email,Fax)values(N'NSX0007',N'Công ty CP Dược phẩm TV.Pharm',N'27 Nguyễn Chí Thanh - Phường 9 - TP Trà Vinh - Tỉnh Trà Vinh',N'0294-3855372',N'info@tvpharm.com.vn',N'')
insert into NhaSanXuat(IdNhaSanXuat,TenNhaSanXuat,DiaChi,SoDienThoai,Email,Fax)values(N'NSX0008',N'Công ty CP Dược phẩm OPC',N'Trụ sở chính: 1017 Hồng Bàng - Phường 12 - Quận 6 - TP. Hồ Chí Minh',N'028-37517111',N'info@opcpharma.com',N'')
insert into NhaSanXuat(IdNhaSanXuat,TenNhaSanXuat,DiaChi,SoDienThoai,Email,Fax)values(N'NSX0009',N'Công ty CP Dược phẩm Hà Tây',N'Số 10A Quang Trung - Quận Hà Đông - TP. Hà Nội',N'024-33824685',N'duochatay@gmail.com',N'')

go
create table NhaPhanPhoi
(
IdNhaPhanPhoi  nvarchar(128) primary key,
TenNhaPhanPhoi nvarchar(max) not null,
DiaChi nvarchar(max),
Fax nvarchar(20),
MaSoThue nvarchar(200),
SoDienThoai nvarchar(20),
Email nvarchar(200)
)
insert into NhaPhanPhoi(IdNhaPhanPhoi,TenNhaPhanPhoi,DiaChi,SoDienThoai,Email)values(N'NPP0001',N'Tổng công ty dược Việt Nam - CTCP',N'12 Ngô Tất Tố, Văn Miếu, Đống Đa, Hà Nội',N'02438443151',N'vinapharm@vinapharm.com.vn')
insert into NhaPhanPhoi(IdNhaPhanPhoi,TenNhaPhanPhoi,DiaChi,SoDienThoai,Email)values(N'NPP0002',N'Công ty cổ phần dược phẩm trung ương CPC1',N'87 P.Nguyễn Văn Trỗi, Phương Liệt, Thanh Xuân, Hà Nội',N'0978777191',N'')
insert into NhaPhanPhoi(IdNhaPhanPhoi,TenNhaPhanPhoi,DiaChi,SoDienThoai,Email,Fax)values(N'NPP0003',N'Công ty cổ phần dược phẩm thiết bị y tế Hà Nội',N'Số 2 Hàng Bài - Hoàn Kiếm, Hà Nội',N'02438255998',N'Contact@hapharco.com.vn',N'02438266029')
insert into NhaPhanPhoi(IdNhaPhanPhoi,TenNhaPhanPhoi,DiaChi,SoDienThoai,Email)values(N'NPP0004',N'Công ty cổ phân dược phẩm FPT Long Châu',N'379-381 Hai Bà Trưng, P. Võ Thị Sáu, Q.3, TP. HCM',N'02873023456',N'sale@nhathuoclongchau.com.vn')
insert into NhaPhanPhoi(IdNhaPhanPhoi,TenNhaPhanPhoi,DiaChi,SoDienThoai,Email)values(N'NPP0005',N'Công ty cổ phần dược - thiết bị y tế Đà Nẵng',N'2 Phan Đình Phùng, Hải Châu 1, Hải Châu, Đà Nẵng',N'02363821642',N'infor@dapharco.com.vn')
insert into NhaPhanPhoi(IdNhaPhanPhoi,TenNhaPhanPhoi,DiaChi,SoDienThoai,Email)values(N'NPP0006',N'Công ty cổ phần dược phẩm Thái Minh',N'Số 3, ngõ 2, phố Thọ Tháp, phường Dịch Vọng, Cầu Giấy, Hà Nội',N'02432003300',N'info@tmp.vn')



go
create table KhachHang
(
IdKhachHang  nvarchar(128) primary key,
TenKhachHang nvarchar(200) not null,
DiaChi nvarchar(max),
SoDienThoai nvarchar(20),
Email nvarchar(200)
)

insert into KhachHang(IdKhachHang,TenKhachHang,DiaChi,SoDienThoai,Email)values(N'KH0001',N'Nguyễn Văn Ánh',N'Hà Nội',N'0971636693',N'vananh23@gmail.com')
insert into KhachHang(IdKhachHang,TenKhachHang,DiaChi,SoDienThoai,Email)values(N'KH0002',N'Nguyễn Đăng Hạnh',N'Hải Dương',N'0919541104',N'vananh23@gmail.com')
insert into KhachHang(IdKhachHang,TenKhachHang,DiaChi,SoDienThoai,Email)values(N'KH0003',N'Nguyễn Công Liễn',N'Thái Nguyên',N'0965784037',N'vananh23@gmail.com')
insert into KhachHang(IdKhachHang,TenKhachHang,DiaChi,SoDienThoai,Email)values(N'KH0004',N'Hoàng Vũ Hiếu',N'Bắc Ninh',N'0378404595',N'vananh23@gmail.com')
insert into KhachHang(IdKhachHang,TenKhachHang,DiaChi,SoDienThoai,Email)values(N'KH0005',N'Triệu Hồng Tuấn',N'Bắc Giang',N'0862808883',N'vananh23@gmail.com')
insert into KhachHang(IdKhachHang,TenKhachHang,DiaChi,SoDienThoai,Email)values(N'KH0006',N'Vũ Hồng Diễm',N'Phú Thọ',N'0826745717',N'vananh23@gmail.com')




go
create table NhomSanPham
(
IdNhomSanPham nvarchar(128) primary key,
TenNhomSanPham nvarchar(100) not null,
GhiChu nvarchar(200)
)

insert into NhomSanPham(IdNhomSanPham,TenNhomSanPham)values(N'NSP001',N'Dược phẩm')
insert into NhomSanPham(IdNhomSanPham,TenNhomSanPham)values(N'NSP002',N'Thực phẩm chức năng')
insert into NhomSanPham(IdNhomSanPham,TenNhomSanPham)values(N'NSP003',N'Thuốc dùng ngoài')
insert into NhomSanPham(IdNhomSanPham,TenNhomSanPham)values(N'NSP004',N'Thuốc kê đơn')
insert into NhomSanPham(IdNhomSanPham,TenNhomSanPham)values(N'NSP005',N'Thuốc không kê đơn')
insert into NhomSanPham(IdNhomSanPham,TenNhomSanPham,GhiChu)values(N'NSP006',N'Thuốc lạ',N'Nhóm thuốc rất lạ')

go
create table SanPham
(
IdSanPham nvarchar(128) primary key,
TenSanPham nvarchar(max) not null,
ThanhPhan nvarchar(max),
HamLuong nvarchar(max),
SoLuongTon int,
GiaBan money,
GiaNhap money,
HanSuDung Datetime,
BarCode nvarchar(128),
GhiChu nvarchar(max),
IdNhaSanXuat nvarchar(128) not null,
IdDonVi nvarchar(128) not null,
IdNhomSanPham nvarchar(128) not null
foreign key(IdNhaSanXuat) references NhaSanXuat(IdNhaSanXuat),
foreign key(IdNhomSanPham) references NhomSanPham(IdNhomSanPham),
foreign key(IdDonVi) references DonVi(IdDonVi)
)
insert into SanPham(IdSanPham,TenSanPham,GhiChu,IdDonVi,IdNhomSanPham,IdNhaSanXuat,SoLuongTon,GiaNhap,GiaBan)values(N'SP0001',N'Ích Nhân',N'Thực phẩm bảo vệ sức khoẻ',N'DV002',N'NSP001',N'NSX0001',200,12300,15000)
insert into SanPham(IdSanPham,TenSanPham,GhiChu,IdDonVi,IdNhomSanPham,IdNhaSanXuat,SoLuongTon,GiaNhap,GiaBan)values(N'SP0002',N'Paradon',N'đỡ nhức mỏi',N'DV001',N'NSP003',N'NSX0002',100,16800,2000)
insert into SanPham(IdSanPham,TenSanPham,GhiChu,IdDonVi,IdNhomSanPham,IdNhaSanXuat,SoLuongTon,GiaNhap,GiaBan)values(N'SP0003',N'Ích Thận Vương',N'giúp thận khoẻ hơn',N'DV004',N'NSP002',N'NSX0003',123,5100,6000)
insert into SanPham(IdSanPham,TenSanPham,GhiChu,IdDonVi,IdNhomSanPham,IdNhaSanXuat,SoLuongTon,GiaNhap,GiaBan)values(N'SP0004',N'Cosamin',N'hỗ trợ xương',N'DV006',N'NSP004',N'NSX0004',123,55600,60000)
insert into SanPham(IdSanPham,TenSanPham,GhiChu,IdDonVi,IdNhomSanPham,IdNhaSanXuat,SoLuongTon,GiaNhap,GiaBan)values(N'SP0005',N'Kanka bổ thận',N'bổ thận',N'DV005',N'NSP005',N'NSX0005',34,7100,10000)
insert into SanPham(IdSanPham,TenSanPham,GhiChu,IdDonVi,IdNhomSanPham,IdNhaSanXuat,SoLuongTon,GiaNhap,GiaBan)values(N'SP0006',N'Thuốc bổ não Pep Iq Up',N'Hỗ trợ não bộ',N'DV002',N'NSP002',N'NSX0006',58,6100,10000)
insert into SanPham(IdSanPham,TenSanPham,GhiChu,IdDonVi,IdNhomSanPham,IdNhaSanXuat,SoLuongTon,GiaNhap,GiaBan)values(N'SP0007',N'Thuốc bổ não Hash',N'Hỗ trợ não bộ',N'DV004',N'NSP004',N'NSX0007',76,2100,5000)
go






create table DonNhapHang
(
IdDonNhapHang nvarchar(128) primary key,
IdNhanVien nvarchar(128) not null,
IdNhaPhanPhoi nvarchar(128) not null,
NgayNhap datetime,
TongTienDonNhapHang money
foreign key(IdNhaPhanPhoi) references NhaPhanPhoi(IdNhaPhanPhoi),
foreign key(IdNhanVien) references NhanVien(IdNhanVien)
)
go


create table ChiTietDonNhapHang
(
IdChiTietDonNhapHang nvarchar(128) primary key,
IdSanPham nvarchar(128) not null,
IdDonNhapHang nvarchar(128) not null,
SoLuongNhap int,
DonGiaNhap money
foreign key(IdSanPham) references SanPham(IdSanPham),
foreign key(IdDonNhapHang) references DonNhapHang(IdDonNhapHang)
)

go
create table DonBanHang
(
IdDonBanHang nvarchar(128) Primary Key,
IdKhachHang nvarchar(128) not null,
IdNhanVien nvarchar(128) not null,
NgayBan datetime,
TongTienDonBanHang money
foreign key(IdKhachHang) references KhachHang(IdKhachHang),
foreign key(IdNhanVien) references NhanVien(IdNhanVien)
)







go
create table ChiTietDonBanHang
(
IdChiTietDonBanHang nvarchar(128) Primary Key,
IdDonBanHang  nvarchar(128) not null,
IdSanPham nvarchar(128) not null,
SoLuongBan int,
ChietKhau decimal(18, 2),
DonGiaBan money
foreign key(IdDonBanHang) references DonBanHang(IdDonBanHang),
foreign key(IdSanPham) references SanPham(IdSanPham)
)
go



