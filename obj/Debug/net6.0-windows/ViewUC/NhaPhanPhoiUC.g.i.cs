﻿#pragma checksum "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BD9EF631A920155391C07692452ED6B825695B57"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DoAnTotNghiepBanThuong.ViewUC;
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


namespace DoAnTotNghiepBanThuong.ViewUC {
    
    
    /// <summary>
    /// NhaPhanPhoiUC
    /// </summary>
    public partial class NhaPhanPhoiUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 31 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNhaPhanPhoi_TimKiem;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ThemNhaPhanPhoiWindow;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNhaPhanPhoi_XuatFile;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewNhaPhanPhoi;
        
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
            System.Uri resourceLocater = new System.Uri("/DoAnTotNghiepBanThuong;component/viewuc/nhaphanphoiuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
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
            this.txtNhaPhanPhoi_TimKiem = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
            this.txtNhaPhanPhoi_TimKiem.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtNhaPhanPhoi_TimKiem_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ThemNhaPhanPhoiWindow = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
            this.ThemNhaPhanPhoiWindow.Click += new System.Windows.RoutedEventHandler(this.ThemNhaPhanPhoiWindow_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnNhaPhanPhoi_XuatFile = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
            this.btnNhaPhanPhoi_XuatFile.Click += new System.Windows.RoutedEventHandler(this.btnNhaPhanPhoi_XuatFile_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listViewNhaPhanPhoi = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 74 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SuaNhaPhanPhoiWindow_Click);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 77 "..\..\..\..\ViewUC\NhaPhanPhoiUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.XoaNhaPhanPhoi_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

