﻿#pragma checksum "..\..\..\..\ViewWindow\SuaNhanVien.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C045DE6362EF6DEBAC3B892837014A40885346B9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DoAnTotNghiepBanThuong.ViewWindow;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DoAnTotNghiepBanThuong.ViewWindow {
    
    
    /// <summary>
    /// SuaNhanVien
    /// </summary>
    public partial class SuaNhanVien : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\..\ViewWindow\SuaNhanVien.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSuaTen_NhanVien;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\ViewWindow\SuaNhanVien.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox txtSuaChucVu_NhanVien;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\ViewWindow\SuaNhanVien.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSuaSoDienThoai_NhanVien;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\ViewWindow\SuaNhanVien.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSuaEmail_NhanVien;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DoAnTotNghiepBanThuong;component/viewwindow/suanhanvien.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ViewWindow\SuaNhanVien.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtSuaTen_NhanVien = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtSuaChucVu_NhanVien = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.txtSuaSoDienThoai_NhanVien = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtSuaEmail_NhanVien = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 97 "..\..\..\..\ViewWindow\SuaNhanVien.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnSuaNhanVien_Sua);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 114 "..\..\..\..\ViewWindow\SuaNhanVien.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnSuaNhanVien_Thoát);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

