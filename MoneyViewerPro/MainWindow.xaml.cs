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
using System.Diagnostics;

namespace MoneyViewerPro
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum CloseAction
        {
            STANDARD, OPEN_OTHER_FILE
        }
        private EntryList entries;
        private CategoryList categories;
        private CloseAction closeAction = CloseAction.STANDARD;
        private string filename = "";
        private string password = "";

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

        public MainWindow(EntryList entries, CategoryList categories, string filename, string password)
        {
            InitializeComponent();
            this.entries = entries;
            this.categories = categories;
            this.filename = filename;
            this.password = password;
            this.mniSave.IsEnabled = true;
        }

       private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(trySave())
            {
                if(this.closeAction == CloseAction.OPEN_OTHER_FILE)
                {
                    new StartWindow().Show();
                }
            } 
            else 
            {
                e.Cancel = true;
            }
        }

        private void btnNewEntry_Click(object sender, RoutedEventArgs e)
        {
            NewEntry entryWindow = new NewEntry();
            entryWindow.ShowDialog();
        }

        private void btnNewCategory_Click(object sender, RoutedEventArgs e)
        {
            NewCategory categoryWindow = new NewCategory();
            categoryWindow.ShowDialog();
        }

        private void newWindow(object sender, RoutedEventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "MoneyViewerPro.exe");
        }

        private bool trySave()
        {
            MessageBoxResult result = MessageBox.Show(this, "Möchten Sie Ihre Änderungen speichern?", "Speichern?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if(string.IsNullOrEmpty(this.filename))
                {
                    return saveOn();
                } else
                {
                    return save();
                }
            }
            else if (result == MessageBoxResult.Cancel)
            {
                return false;
            }
            return true;
        }

        private void openFile(object sender, RoutedEventArgs e)
        {
            this.closeAction = CloseAction.OPEN_OTHER_FILE;
            Close();
        }

        private bool saveOn()
        {
            StartWindow startWindow = new StartWindow(entries, categories, StartWindow.StartWindowOptions.SAVE);
            startWindow.ShowDialog();
            return startWindow.Successful;
        }

        private bool save()
        {
            return false;
        }

        private void mniSave_Click(object sender, RoutedEventArgs e)
        {
            save();
        }

        private void mniSaveOn_Click(object sender, RoutedEventArgs e)
        {
            saveOn();
        }
    }
}
