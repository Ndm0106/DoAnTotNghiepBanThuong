   M S B A M L   `   `   `  ����Q  MDoAnTotNghiepBanThuong, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:DoAnTotNghiepBanThuong.ViewWindow!DoAnTotNghiepBanThuong.ViewWindow  5   	   F    @DoAnTotNghiepBanThuong.ViewWindow.ThemDonNhapHang_ThemSanPhamMoi   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:DoAnTotNghiepBanThuong.ViewWindow     5   	   _ [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	    � AllowsTransparency$
 True��5   	   
������  � Title$$ ThemDonNhapHang_ThemSanPhamMoi��5   	   $	��600��60   $	��720��6=   .��5      �� 
�������$��1q�6$   
������� �� CornerRadius$ 20��6O   ._�5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   .+�5      � $��0��6   $��	20 10 0 0q�6    .+�5      �� $��30(�6   �� 6,   .�61   Thêm mới sản phẩm6D   6K   5      � 5      $��1��6    5      �� 5      6&   �� 5      6&   .+�5       � $��0��6   $��20 0 0 0q�6$    5!      �� 5"      $
��auto �6(   68   �� 5#      $
��auto �6(   68    � ColumnDefinitions 5%      �� 5&      $
��auto �6+   6:   �� 5'      $
��auto �6+   6:   �� 5(      $
��auto �6+   6:   �� 5)      $
��auto �6+   6:   .+�5+      � $��10q�6   $��0��6'   $��0��64   .+�5,      �� .+�5-      �� $��14(�6(   �� 67   .�6<   Mã sản phẩm6I   6P   �� 5.      -   5         ��Name$1 +txtThemDonNhapHang_ThemSanPhamMoi_IdSanPham��5/      $	��150��50      
�������$��49.96��52      $	��Top=�53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Mã sản phẩm��54      	 �� AcceptsReturn$
	 True��55      ${�False��56   !    !  MaterialDesignOutlinedTextBox#��[  57      
 �� TextWrapping$

 Wrap=�58      " �� VerticalScrollBarVisibility$
 Auto=�59      5:      5;      � 5=      $��10q�6   $��0��6(   $��1��65   .+�5>      �� .+�5?      �� $��14(�6(   �� 67   .�6<   Tên sản phẩm6J   6Q   �� 5@      -   5        $2 ,txtThemDonNhapHang_ThemSanPhamMoi_TenSanPham��5A      $	��150��5B      
�������$��49.96��5D      $	��Top=�5E      $ Tên sản phẩm��5F      $
	 True��5G      #��[  5H      $

 Wrap=�5I      $
 Auto=�5J      5K      5L      � 5M      $��10q�6   $��1��6(   $��0��65   .+�5N      �� .+�5O      �� $��14(�6(   �� 67   .�6<   
Mã vạch6E   6L   �� 5P      -   5        $6 0txtThemDonNhapHang_ThemSanPhamMoi_BarCodeSanPham��5Q      
�������$	��150��5S      $��49.96��5T      $	��Top=�5U      $ 
Mã vạch��5V      $
	 True��5W      #��[  5X      $

 Wrap=�5Y      $
 Auto=�5Z      5[      5\      � 5]      $��10q�6   $��1��6'   $��1��64   .+�5^      �� .+�5_      �� $��14(�6(   �� 67   .�6<   Thành phần6H   6O   �� 5`      -   5        $8 2txtThemDonNhapHang_ThemSanPhamMoi_ThanhPhanSanPham��5a      $	��150��5b      
�������$��49.96��5d      $	��Top=�5e      $ Tên sản phẩm��5f      $
	 True��5g      #��[  5h      $

 Wrap=�5i      $
 Auto=�5j      5k      5l      � 5m      $��10q�6   $��1��6'   $��2��64   .+�5n      �� .+�5o      �� $��14(�6(   �� 67   .�6<   Hàm lượng6G   6N   �� 5p      -   5        $7 1txtThemDonNhapHang_ThemSanPhamMoi_HamLuongSanPham��5q      $	��150��5r      
�������$��49.96��5t      $	��Top=�5u      $ Tên sản phẩm��5v      $
	 True��5w      #��[  5x      $

 Wrap=�5y      $
 Auto=�5z      5{      5|      � 5}      $��10q�6   $��1��6'   $��3��64   .+�5~      �� .+�5      �� $��14(�6(   �� 67   .�6<   Hạn sử dụng6I   6P   (  "System.Windows.Controls.DatePicker  5�      -   5          Name$8 2txtThemDonNhapHang_ThemSanPhamMoi_HanSuDungSanPham��5�      
�������$	��150��5�      $ Hạn sử dụng��5�       $  MaterialDesignOutlinedDatePicker#��[ 5�      5�      5�      � 5�      $��10q�6   $��0��6'   $��2��64   .+�5�      �� .+�5�      �� $��14(�6(   �� 67   .�6<   Đơn vị tính6I   6P   �� 5�      -   5         ��Name$4 .txtThemDonNhapHang_ThemSanPhamMoi_DonViSanPham��5�      
�������$	��150��5�      $ Đơn vị tính��5�       " MaterialDesignOutlinedComboBox#��[ 5�      5�      5�      5�      � 5�      $��10q�6   $��0��6'   $��3��64   .+�5�      �� .+�5�      �� $��14(�6(   �� 67   .�6<   Nhóm sản phẩm6K   6R   �� 5�      -   5        $: 4txtThemDonNhapHang_ThemSanPhamMoi_NhomSanPhamSanPham��5�      
�������$	��150��5�      $ Nhóm sản phẩm��5�      #��[ 5�      5�      5�      5�      5�      5�      � 5�      $��2��6   $��20 0 0 0q�6     5�      �� 5�      $
��auto �6'   66   �� 5�      $
��auto �6'   66   �� 5�      $
��auto �6'   66   .+�5�      � $��10q�6   $��0��6#   .+�5�      �� .+�5�      �� $��14(�6$   �� 63   .�68   Số lượng6B   6I   �� 5�      -	   5        $9 3txtThemDonNhapHang_ThemSanPhamMoi_SoLuongTonSanPham��5�      $	��200��5�      
�������$��49.96��5�      $	��Top=�5�      $ Số lượng��5�      $
	 True��5�      #��[  5�      $

 Wrap=�5�      $
 Auto=�5�      5�      5�      � 5�      $��10q�6   $��1��6#   .+�5�      �� .+�5�      �� $��14(�6$   �� 63   .�68   Giá nhập6B   6I   �� 5�      -
   5        $6 0txtThemDonNhapHang_ThemSanPhamMoi_GiaNhapSanPham��5�   !   $	��210��5�      
�������$��49.96��5�      $	��Top=�5�      $ Giá nhập��5�      #��[  5�      5�      5�      � 5�      $��10q�6   $��2��6$   .+�5�      �� .+�5�      �� $��14(�6$   �� 63   .�68   Giá bán lẻ6D   6K   �� 5�      -   5        $7 1txtThemDonNhapHang_ThemSanPhamMoi_GiaBanLeSanPham��5�      $	��210��5�      
�������$��49.96��5�      $	��Top=�5�      $ Giá bán lẻ��5�      #��[  5�      5�      5�      5�      � 5�      $��3��6   $��30 0 0 0q�6    .+�5�      �� .+�5�      �� $��14(�6    �� 6/   .�64   Nhà sản xuất6B   6I   �� 5�      -   5        $9 3txtThemDonNhapHang_ThemSanPhamMoi_NhaSanXuatSanPham��5�      
�������$	��660��5�      $
��Left=�5�      $ Nhà sản xuất��5�       �� 
IsEditable$
 True��5�       �� IsTextSearchEnabled$
 True��6    w� TextPath$ TenNhaSanXuat��5�      #��[ 5�      5�      5�      5�      � 5�      $��	30 10 0 0q�6   $��4��6&   .+�5�      �� .+�5�      �� $��14(�6    �� 6/   .�64   
Ghi chú6=   6D   �� 5�      -   5        $5 /txtThemDonNhapHang_ThemSanPhamMoi_GhiChuSanPham��5�      $	��100��5�      
�������$
��Left=�5�      $	��660��5�      $	��Top=�5�      $ Chi chú��5�      $
	 True��5�      #��[  5�      $

 Wrap=�5�      $
 Auto=�5�      5�      5      � 5     $��5��6   $��	0 30 10 0q�6    .+�5     �� -   5         ��Name$+ %btnThemDonNhapHang_ThemSanPhamMoi_Luu��5     $��Right=�5     $	��100��5     $��	0 0 130 0q�5     $��Center=�5	     $��35��5
     +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��5       MaterialDesignRaisedButton#��[ 5      � ToolTip$3 -MaterialDesignRaisedButton with Round Corners��5     {�5     �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window.��5     ��  �� Orientation$ 
Horizontal=�6!   .+�5     '  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�62      Kind$ Check=�6M   �� 5     $��Center=�6$   $
��Lưu��6?   5     5     �� 5     -   5        $- 'btnThemDonNhapHang_ThemSanPhamMoi_Thoat��5     $	��100��5     $��0 0 20 0q�5     $��Right=�5     
��� E��
��� E��$��35��5     $ 10��5     #��[ 5     $3 -MaterialDesignRaisedButton with Round Corners��5      {�5     �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window.��5!     �� $ 
Horizontal=�6!   .+�5"       $��Center=�62   $ Close=�6M   �� 5#     $��Center=�6$   $��Thoát��6?   5$     5%     5&     5'     5(     5)     