﻿#pragma checksum "..\..\..\VisitorWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7CC3F32B464B9BF2459EFCFE42FA6B5EBC29734C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MicroElectronsManager;
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


namespace MicroElectronsManager {
    
    
    /// <summary>
    /// VisitorWindow
    /// </summary>
    public partial class VisitorWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\VisitorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TbWelcome;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\VisitorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnExit;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\VisitorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridVisitors;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\VisitorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CmAdd;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\VisitorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CmExit;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\VisitorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CmWhoEntry;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\VisitorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CmWhoExit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MicroElectronsManager;component/visitorwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\VisitorWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\VisitorWindow.xaml"
            ((MicroElectronsManager.VisitorWindow)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\VisitorWindow.xaml"
            ((MicroElectronsManager.VisitorWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TbWelcome = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.BtnExit = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\VisitorWindow.xaml"
            this.BtnExit.Click += new System.Windows.RoutedEventHandler(this.BtnExit_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DataGridVisitors = ((System.Windows.Controls.DataGrid)(target));
            
            #line 25 "..\..\..\VisitorWindow.xaml"
            this.DataGridVisitors.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGridVisitors_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CmAdd = ((System.Windows.Controls.MenuItem)(target));
            
            #line 28 "..\..\..\VisitorWindow.xaml"
            this.CmAdd.Click += new System.Windows.RoutedEventHandler(this.CmAdd_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CmExit = ((System.Windows.Controls.MenuItem)(target));
            
            #line 29 "..\..\..\VisitorWindow.xaml"
            this.CmExit.Click += new System.Windows.RoutedEventHandler(this.CmExit_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CmWhoEntry = ((System.Windows.Controls.MenuItem)(target));
            
            #line 30 "..\..\..\VisitorWindow.xaml"
            this.CmWhoEntry.Click += new System.Windows.RoutedEventHandler(this.CmWhoEntry_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.CmWhoExit = ((System.Windows.Controls.MenuItem)(target));
            
            #line 31 "..\..\..\VisitorWindow.xaml"
            this.CmWhoExit.Click += new System.Windows.RoutedEventHandler(this.CmWhoExit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

