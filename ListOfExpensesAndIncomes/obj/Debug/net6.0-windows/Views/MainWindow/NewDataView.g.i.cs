﻿#pragma checksum "..\..\..\..\..\Views\MainWindow\NewDataView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6A188D35A9D4216885A101DF0C978289BEE17939"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ListOfExpensesAndIncomes.Views.MainWindow;
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


namespace ListOfExpensesAndIncomes.Views {
    
    
    /// <summary>
    /// NewData
    /// </summary>
    public partial class NewData : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\..\Views\MainWindow\NewDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox typeField;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\Views\MainWindow\NewDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem IncomeComboBoxItem;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\Views\MainWindow\NewDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem PurchaseComboBoxItem;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\Views\MainWindow\NewDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dateField;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\..\Views\MainWindow\NewDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sumField;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\..\Views\MainWindow\NewDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descrField;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\..\Views\MainWindow\NewDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EnterButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ListOfExpensesAndIncomes;component/views/mainwindow/newdataview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\MainWindow\NewDataView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.typeField = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.IncomeComboBoxItem = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 3:
            this.PurchaseComboBoxItem = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 4:
            this.dateField = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.sumField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.descrField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.EnterButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

