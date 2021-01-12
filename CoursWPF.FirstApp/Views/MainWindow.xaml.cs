using CoursWPF.FirstApp.Models;
using CoursWPF.FirstApp.ViewModels;
using CoursWPF.FirstApp.ViewModels.Abstracts;
using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using CoursWPF.FirstApp.Views.Abstracts;
using ControlzEx.Theming;

namespace CoursWPF.FirstApp
{
    /// <summary>
    /// Interaction logic for StaticMainWindow.xaml
    /// </summary>
    public partial class StaticMainWindow : MetroWindow, IMainWindow
    {
        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="StaticMainWindow"/>
        /// </summary>
        public StaticMainWindow()
        {
            this.InitializeComponent();
            ThemeManager.Current.ChangeTheme(this, "Dark.Cyan");
        }

        #endregion
    }
}
