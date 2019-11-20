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

namespace MoneyViewerPro
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EntryList entries;
        private CategoryList categories;

        public MainWindow()
        {
            InitializeComponent();
            this.entries = new EntryList();
            this.categories = new CategoryList();
        }

        public MainWindow(EntryList entries, CategoryList categories)
        {
            InitializeComponent();
            this.entries = entries;
            this.categories = categories;
        }

       private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(this, "Möchten Sie Ihre Änderungen speichern?", "Speichern?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                StartWindow startWindow = new StartWindow(entries, categories, StartWindow.StartWindowOptions.SAVE);
                startWindow.ShowDialog();
                if(!startWindow.Successful)
                {
                    e.Cancel = true;
                }
            } else if(result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
