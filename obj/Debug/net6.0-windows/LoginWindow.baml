   M S B A M L   `   `   `  ����Q  MDoAnTotNghiepBanThuong, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:DoAnTotNghiepBanThuongDoAnTotNghiepBanThuong  5   	   (    "DoAnTotNghiepBanThuong.LoginWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:DoAnTotNghiepBanThuong     5   	   _ [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � WindowStyle$
  None=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � AllowsTransparency$
 True��5
   	   
������  � Title$ LoginWindow��5   	   $	��450��6   $	��450��6*   .��5      ��  �� CornerRadius$ 20��6   ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,1&�63   .��5      �  � Color$ 	LightCyan��6    � Offset$ 0Q�61   � 5      $ 	LightBlue��6   $	 0.5Q�61   � 5      $ DarkTurquoise��6   $ 1Q�65   5      ._�5   
   �� 
�������$��50 30 50 30q�6$   $ 20��69   $��5q�6K   ._�5      � 	 � RowDefinitions	 5      �� 5      $
��auto �6$   64   �� 5      $
��auto �6$   64   �� 5      $
��auto �6$   64   �� 5      $
��auto �6$   64   .+�5      � $��0��6   $��
20 30 20 0q�6$   .+�5       �� $��50(�6!   $��Center=�6/   $��Center=�6L   
����� �$��Đăng nhập��6�   5!      � 5#      $��1��6   $��0 50 0 0q�6$   .+�5$      �� 
 �� Orientation$
 
Horizontal=�6!   .+�5%      '  !MaterialDesignThemes.Wpf.PackIcon  $��	20 0 10 0q�5&      ���#80deea$��49.96��5(      $��49.96��5)         Kind$ Account=�5*      �� 5+      -   5         ��Name$ UsernameBox��5/      $	��220��5-   !   ���#80deea$��Center=�50      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tài khoản��51       !  MaterialDesignOutlinedTextBox#��[  52      53      54      � 56      $��2��6   $��0 10 0 0q�6$   .+�57      �� $
 
Horizontal=�6!   .+�58        ���#80deea$��49.96��6H   $��	20 0 10 0q�59   3   $��49.96��5:      $
 lock=�6$   G� 5;      -   5         G�Name$ FloatingPasswordBox��5>      ���#80deea$	��220��5=      $ Mật khẩu��5?      .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton$
 True��5@       + 'MaterialDesignOutlinedRevealPasswordBox#��[ 5A      5B      5C      5D      � 5F      $��3��6   $��
20 30 20 0q�6$   .+�5G      �� $
 
Horizontal=�6!   .+�5H      �� -   5         ��Name$ btnDangNhap��5I       �� 	IsDefault$
 true��5K      ���#80deea���#80deea
�������$	��105��5O      $��30 0 0 0q�5P      $��Đăng Nhập��5Q       # MaterialDesignRaisedLightButton#��[ 5S       � ToolTip$4 .Resource name: MaterialDesignRaisedLightButton��5T      {�5R      �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window5T      �� 5U      -   5        $ btnThoat��5V      $	��105��5X      $��40 0 0 0q�5Y      
��� E��
��� E��
�������$��Thoát��5]      #��[ 5_      $4 .Resource name: MaterialDesignRaisedLightButton��5`      {�5^      �� *DataContext.ControlsEnabled+ �� *FindAncestor+
 Window5`      5a      5b      5c      5d      5e      5f      