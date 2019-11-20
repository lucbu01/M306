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
        private StartWindowOptions options;
        public bool Successful { get; set; } = false;

        public StartWindow()
        {
            InitializeComponent();
        }

        public StartWindow(EntryList entries, CategoryList categories, StartWindowOptions options)
        {
            this.entries = entries;
            this.categories = categories;
            this.options = options;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
