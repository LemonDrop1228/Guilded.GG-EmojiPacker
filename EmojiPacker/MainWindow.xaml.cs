using EmojiPacker.TestHelpers;
using EmojiPacker.Utility;
using EmojiPacker.Views;
using Microsoft.Win32;
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
            {
                CurrentView.CloseView();
                CurrentView = null;
            }

            // CurrentView = new CreateView(CreateTestPack.GetNewPackWrapper());   // For testing only
            ContentViewer.Child = CurrentView = new CreateView();
        }

        private void LoadPackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var oFileDiag = new OpenFileDialog() { 
                    CheckFileExists = true,
                    CheckPathExists = true,
                    Filter = "json | *.json"
                };
                if (oFileDiag.ShowDialog() == true)
                {
                    var packPath = oFileDiag.FileName;
                    var jsonData = System.IO.File.ReadAllText(oFileDiag.FileName);

                    if (CurrentView != null)
                    {
                        CurrentView.CloseView();
                        CurrentView = null;
                    }

                    var emojiPack = PackImporter.Import(jsonData);
                    if (emojiPack != null)
                        ContentViewer.Child = CurrentView = new CreateView(emojiPack);
                }
            }
            catch (Exception) { }
        }

        private void TitleColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void MainPackWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentView != null)
            {
                CurrentView.CloseView();
                CurrentView = null;
            }

            this.Close();
        }

        private void MinimizeAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
