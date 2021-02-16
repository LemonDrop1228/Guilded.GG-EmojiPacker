﻿using EmojiPacker.Utility;
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
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace EmojiPacker.Views
{
    /// <summary>
    /// Interaction logic for CreateView.xaml
    /// </summary>
    public partial class CreateView : BaseView
    {

        public CreateView()
        {
            InitializeComponent();
            this.DataContext = this;
            CurrentPack = new EmojiPackWrapper();
        }

        public CreateView(EmojiPackWrapper emojiPack)
        {
            CurrentPack = emojiPack;
            InitializeComponent();
            this.DataContext = this;
        }

        public override void CloseView()
        {
            
        }

        private void AddEmoteButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPack.Emojis.Add(new EmojiDefinition());
        }

        private void ClearPackButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPack.Emojis.Clear();
        }

        private void SavePackButton_Click(object sender, RoutedEventArgs e)
        {
            var tempPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{CurrentPack.Name}.emojipack.json".Replace(" ", "_");

            var jsonToSave = CurrentPack.GetPackJson().Replace("/", @"\/");

            File.WriteAllText(tempPath, jsonToSave);

            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", tempPath));
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            (e.OriginalSource as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {
            (e.OriginalSource as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void TextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            TextBox textBox = null;
            string text = string.Empty;
            try
            {
                text = (string)e.Data.GetData(typeof(String));
                textBox = (sender as TextBox);
            }
            catch (Exception) { }

            if (textBox != null && !string.IsNullOrEmpty(text))
                textBox.Text = text;
        }
    }
}