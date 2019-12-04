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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace MoneyViewerPro
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public enum StartWindowOptions
        {
            OPEN,
            SAVE
        }
        private EntryList entries;
        private CategoryList categories;
        private StartWindowOptions option = StartWindowOptions.OPEN;
        public bool Successful { get; set; } = false;

        public StartWindow()
        {
            InitializeComponent();
        }

        public StartWindow(EntryList entries, CategoryList categories, StartWindowOptions options)
        {
            InitializeComponent();
            this.entries = entries;
            this.categories = categories;
            this.option = options;
            if(option == StartWindowOptions.SAVE)
            {
                this.btnNew.Content = "Abbrechen";
                this.btnOpen.Content = "Speichern";
                this.lblPassword.Content += " (optional)";
                this.pwbPassword.IsEnabled = true;
                this.Title = "Speichern - Money Viewer Pro";
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if(option == StartWindowOptions.OPEN)
            {
                new MainWindow().Show();
                Close();
            }
        }

        private void txbFile_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            FileDialog dialog;
            if (option == StartWindowOptions.OPEN)
            {
                dialog = new OpenFileDialog();
                dialog.Title = "File öffnen";
            } else
            {
                dialog = new SaveFileDialog();
                dialog.Title = "File speichern";
            }
            dialog.Filter = "Money Viever File (*.mvf)|*.mvf";
            dialog.AddExtension = true;
            if (dialog.ShowDialog() == true)
            {
                txbFile.Text = dialog.FileName;
            }

        }
    }
}
