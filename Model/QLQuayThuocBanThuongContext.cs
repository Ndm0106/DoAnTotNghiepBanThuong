using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoAnTotNghiepBanThuong.Model
{
    public partial class QLQuayThuocBanThuongContext : DbContext
    {
        public QLQuayThuocBanThuongContext()
        {
        }

        public QLQuayThuocBanThuongContext(DbContextOptions<QLQuayThuocBanThuongContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietDonBanHang> ChiTietDonBanHangs { get; set; } = null!;
        public virtual DbSet<ChiTietDonNhapHang> ChiTietDonNhapHangs { get; set; } = null!;
        public virtual DbSet<ChucVu> ChucVus { get; set; } = null!;
        public virtual DbSet<DonBanHang> DonBanHangs { get; set; } = null!;
        public virtual DbSet<DonNhapHang> DonNhapHangs { get; set; } = null!;
        public virtual DbSet<DonVi> DonVis { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<NhaPhanPhoi> NhaPhanPhois { get; set; } = null!;
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<NhomSanPham> NhomSanPhams { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-BO68SJN\\DINHMANH;Initial Catalog=QLQuayThuocBanThuong;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonBanHang>(entity =>
            {
                entity.HasKey(e => e.IdChiTietDonBanHang)
                    .HasName("PK__ChiTietD__651A32FB753F82AA");

                entity.ToTable("ChiTietDonBanHang");

                entity.Property(e => e.IdChiTietDonBanHang).HasMaxLength(128);

                entity.Property(e => e.ChietKhau).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DonGiaBan).HasColumnType("money");

                entity.Property(e => e.IdDonBanHang).HasMaxLength(128);

                entity.Property(e => e.IdSanPham).HasMaxLength(128);

                entity.HasOne(d => d.IdDonBanHangNavigation)
                    .WithMany(p => p.ChiTietDonBanHangs)
                    .HasForeignKey(d => d.IdDonBanHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDo__DonGi__5629CD9C");

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.ChiTietDonBanHangs)
                    .HasForeignKey(d => d.IdSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDo__IdSan__571DF1D5");
            });

            modelBuilder.Entity<ChiTietDonNhapHang>(entity =>
            {
                entity.HasKey(e => e.IdChiTietDonNhapHang)
                    .HasName("PK__ChiTietD__BAB6ADBB9E9F8CC0");

                entity.ToTable("ChiTietDonNhapHang");

                entity.Property(e => e.IdChiTietDonNhapHang).HasMaxLength(128);

                entity.Property(e => e.DonGiaNhap).HasColumnType("money");

                entity.Property(e => e.IdDonNhapHang).HasMaxLength(128);

                entity.Property(e => e.IdSanPham).HasMaxLength(128);

                entity.HasOne(d => d.IdDonNhapHangNavigation)
                    .WithMany(p => p.ChiTietDonNhapHangs)
                    .HasForeignKey(d => d.IdDonNhapHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDo__IdDon__4F7CD00D");

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.ChiTietDonNhapHangs)
                    .HasForeignKey(d => d.IdSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDo__DonGi__4E88ABD4");
            });

            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.HasKey(e => e.IdChucVu)
                    .HasName("PK__ChucVu__81D916DFB8A4BE3A");

                entity.ToTable("ChucVu");

                entity.Property(e => e.TenChucVu).HasMaxLength(100);
            });

            modelBuilder.Entity<DonBanHang>(entity =>
            {
                entity.HasKey(e => e.IdDonBanHang)
                    .HasName("PK__DonBanHa__5EE36707E8B3AF42");

                entity.ToTable("DonBanHang");

                entity.Property(e => e.IdDonBanHang).HasMaxLength(128);

                entity.Property(e => e.IdKhachHang).HasMaxLength(128);

                entity.Property(e => e.IdNhanVien).HasMaxLength(128);

                entity.Property(e => e.NgayBan).HasColumnType("datetime");

                entity.Property(e => e.TongTienDonBanHang).HasColumnType("money");

                entity.HasOne(d => d.IdKhachHangNavigation)
                    .WithMany(p => p.DonBanHangs)
                    .HasForeignKey(d => d.IdKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonBanHan__TongT__52593CB8");

                entity.HasOne(d => d.IdNhanVienNavigation)
                    .WithMany(p => p.DonBanHangs)
                    .HasForeignKey(d => d.IdNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonBanHan__IdNha__534D60F1");
            });

            modelBuilder.Entity<DonNhapHang>(entity =>
            {
                entity.HasKey(e => e.IdDonNhapHang)
                    .HasName("PK__DonNhapH__FEE9EA41B7CDBF71");

                entity.ToTable("DonNhapHang");

                entity.Property(e => e.IdDonNhapHang).HasMaxLength(128);

                entity.Property(e => e.IdNhaPhanPhoi).HasMaxLength(128);

                entity.Property(e => e.IdNhanVien).HasMaxLength(128);

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.Property(e => e.TongTienDonNhapHang).HasColumnType("money");

                entity.HasOne(d => d.IdNhaPhanPhoiNavigation)
                    .WithMany(p => p.DonNhapHangs)
                    .HasForeignKey(d => d.IdNhaPhanPhoi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonNhapHa__TongT__4AB81AF0");

                entity.HasOne(d => d.IdNhanVienNavigation)
                    .WithMany(p => p.DonNhapHangs)
                    .HasForeignKey(d => d.IdNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonNhapHa__IdNha__4BAC3F29");
            });

            modelBuilder.Entity<DonVi>(entity =>
            {
                entity.HasKey(e => e.IdDonVi)
                    .HasName("PK__DonVi__F27207FDE2548A59");

                entity.ToTable("DonVi");

                entity.Property(e => e.IdDonVi).HasMaxLength(128);

                entity.Property(e => e.TenDonVi).HasMaxLength(100);
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.IdKhachHang)
                    .HasName("PK__KhachHan__7CF5D8361E32F9BA");

                entity.ToTable("KhachHang");

                entity.Property(e => e.IdKhachHang).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);

                entity.Property(e => e.TenKhachHang).HasMaxLength(200);
            });

            modelBuilder.Entity<NhaPhanPhoi>(entity =>
            {
                entity.HasKey(e => e.IdNhaPhanPhoi)
                    .HasName("PK__NhaPhanP__C3ACA4D5C7784B36");

                entity.ToTable("NhaPhanPhoi");

                entity.Property(e => e.IdNhaPhanPhoi).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.MaSoThue).HasMaxLength(200);

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            });

            modelBuilder.Entity<NhaSanXuat>(entity =>
            {
                entity.HasKey(e => e.IdNhaSanXuat)
                    .HasName("PK__NhaSanXu__0FEFD35C5BEBBB72");

                entity.ToTable("NhaSanXuat");

                entity.Property(e => e.IdNhaSanXuat).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.IdNhanVien)
                    .HasName("PK__NhanVien__B8294845999D7078");

                entity.ToTable("NhanVien");

                entity.Property(e => e.IdNhanVien).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);

                entity.Property(e => e.TaiKhoan).HasMaxLength(100);

                entity.HasOne(d => d.IdChucVuNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.IdChucVu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NhanVien__IdChuc__398D8EEE");
            });

            modelBuilder.Entity<NhomSanPham>(entity =>
            {
                entity.HasKey(e => e.IdNhomSanPham)
                    .HasName("PK__NhomSanP__4593817ADA7EF3BE");

                entity.ToTable("NhomSanPham");

                entity.Property(e => e.IdNhomSanPham).HasMaxLength(128);

                entity.Property(e => e.GhiChu).HasMaxLength(200);

                entity.Property(e => e.TenNhomSanPham).HasMaxLength(100);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.IdSanPham)
                    .HasName("PK__SanPham__5FFA2D42D2050988");

                entity.ToTable("SanPham");

                entity.Property(e => e.IdSanPham).HasMaxLength(128);

                entity.Property(e => e.BarCode).HasMaxLength(128);

                entity.Property(e => e.GiaBan).HasColumnType("money");

                entity.Property(e => e.GiaNhap).HasColumnType("money");

                entity.Property(e => e.HanSuDung).HasColumnType("datetime");

                entity.Property(e => e.IdDonVi).HasMaxLength(128);

                entity.Property(e => e.IdNhaSanXuat).HasMaxLength(128);

                entity.Property(e => e.IdNhomSanPham).HasMaxLength(128);

                entity.HasOne(d => d.IdDonViNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdDonVi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__IdDonVi__47DBAE45");

                entity.HasOne(d => d.IdNhaSanXuatNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdNhaSanXuat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__IdNhomS__45F365D3");

                entity.HasOne(d => d.IdNhomSanPhamNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.IdNhomSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__IdNhomS__46E78A0C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
