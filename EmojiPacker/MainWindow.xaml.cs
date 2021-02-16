using EmojiPacker.TestHelpers;
using EmojiPacker.Views;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Reflection;
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

namespace EmojiPacker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BaseView currentView;

        public string TitleText => $"Emoji Packer {getRunningVersion()}";
        public BaseView CurrentView { get => currentView; set => currentView = value; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private Version getRunningVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (Exception)
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        private void NewPackButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentView != null)
                CurrentView.CloseView();

            CurrentView = new CreateView();
            ContentViewer.Child = CurrentView;
        }
    }
}
