﻿#pragma checksum "..\..\..\..\MVVM\View\ProfileView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3BC62A4CE875D400F3868BE4CD375605B0EDA2FC0EE270B6FD0898600C7EB88F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using WpfProjekt.MVVM.View;


namespace WpfProjekt.MVVM.View {
    
    
    /// <summary>
    /// ProfileView
    /// </summary>
    public partial class ProfileView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\MVVM\View\ProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ProfEditButton;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\MVVM\View\ProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlayGameButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\MVVM\View\ProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteGameButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\MVVM\View\ProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView GamesInStoreListView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfProjekt;component/mvvm/view/profileview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MVVM\View\ProfileView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ProfEditButton = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\MVVM\View\ProfileView.xaml"
            this.ProfEditButton.Click += new System.Windows.RoutedEventHandler(this.ProfEditButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PlayGameButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\MVVM\View\ProfileView.xaml"
            this.PlayGameButton.Click += new System.Windows.RoutedEventHandler(this.PlayGameButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DeleteGameButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\MVVM\View\ProfileView.xaml"
            this.DeleteGameButton.Click += new System.Windows.RoutedEventHandler(this.DeleteGameButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.GamesInStoreListView = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

