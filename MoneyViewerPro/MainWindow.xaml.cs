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
using System.Globalization;

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
        private string all = "--Alle--";
        private Category allCategories = new Category("--Alle--", "");
        private Dictionary<int, string> months = new Dictionary<int, string>();
        private bool changes = false;
        //need for calculation
        private string year = null;
        private string month = null;
        private string category = null;

        public MainWindow()
        {
            InitializeComponent();
            this.entries = new EntryList();
            this.categories = new CategoryList();
            this.btnNewEntry.IsEnabled = false;
            fillMonths();
            fillCategoriesBox();
            fillYearBox();
            fillMonthBox();
        }

        public MainWindow(EntryList entries, CategoryList categories)
        {
            InitializeComponent();
            this.entries = entries;
            this.categories = categories;
            fillMonths();
            fillCategoriesBox();
            fillYearBox();
            fillMonthBox();
            this.btnNewEntry.IsEnabled = categories.categories.Count > 0;
            updateData();
        }

        public MainWindow(EntryList entries, CategoryList categories, string filename, string password)
        {
            InitializeComponent();
            this.entries = entries;
            this.categories = categories;
            this.filename = filename;
            this.password = password;
            this.mniSave.IsEnabled = !string.IsNullOrWhiteSpace(filename);
            fillMonths();
            fillCategoriesBox();
            fillYearBox();
            fillMonthBox();
            this.btnNewEntry.IsEnabled = categories.categories.Count > 0;
            updateData();
        }

        private void fillMonths()
        {
            months = new Dictionary<int, string>();
            months.Add(1, "Januar");
            months.Add(2, "Februar");
            months.Add(3, "März");
            months.Add(4, "April");
            months.Add(5, "Mai");
            months.Add(6, "Juni");
            months.Add(7, "Juli");
            months.Add(8, "August");
            months.Add(9, "September");
            months.Add(10, "Oktober");
            months.Add(11, "November");
            months.Add(12, "Dezember");
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
            NewEntry entryWindow = new NewEntry(this, this.entries, this.categories);
            entryWindow.ShowDialog();
            fillYearBox();
            changes = true;
            updateData();
        }

        private void btnNewCategory_Click(object sender, RoutedEventArgs e)
        {
            NewCategory categoryWindow = new NewCategory(this, this.categories);
            categoryWindow.ShowDialog();
            fillCategoriesBox();
            this.btnNewEntry.IsEnabled = categories.categories.Count > 0;
            changes = true;
            updateData();
        }

        private void newWindow(object sender, RoutedEventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "MoneyViewerPro.exe");
        }

        private bool trySave()
        {
            if(changes)
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
            StartWindow startWindow = new StartWindow(this, entries, categories, StartWindow.StartWindowOptions.SAVE);
            startWindow.ShowDialog();
            if(startWindow.Successful)
            {
                this.filename = startWindow.Path;
                this.password = startWindow.Password;
                this.mniSave.IsEnabled = true;
                changes = false;
            }
            return startWindow.Successful;
        }

        private bool save()
        {
            try
            {
                FileWriter.write(new FileData(entries, categories), filename, password);
                changes = false;
                return true;
            } catch
            {
                MessageBox.Show(this, "Es gab einen Fehler beim Speichern. Möglicherweise wird es von einem anderen Programm oder Prozess verwendet. Versuchen Sie es nochmal oder wählen Sie \"Speichern unter\" um das File an einem anderen Ort zu speichern", "Fehler beim Speichern", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void mniSave_Click(object sender, RoutedEventArgs e)
        {
            save();
        }

        private void mniSaveOn_Click(object sender, RoutedEventArgs e)
        {
            saveOn();
        }

        private void fillCategoriesBox()
        {
            List<Category> items = new List<Category>();
            items.Add(allCategories);
            items.AddRange(this.categories.categories);
            cmbCategory.ItemsSource = items;
            cmbCategory.SelectedItem = allCategories;
        }

        private void fillYearBox()
        {
            List<string> items = new List<string>();
            items.Add(all);
            foreach(int year in entries.years())
            {
                items.Add(year.ToString());
            }
            cmbYear.ItemsSource = items;
            cmbYear.SelectedItem = all;
        }

        private void fillMonthBox()
        {
            string year = cmbYear.SelectedItem as string;
            List<string> items = new List<string>();
            items.Add(all);
            if(year != null && year != all)
            {
                foreach(int month in entries.months(Convert.ToInt32(year)))
                {
                    items.Add(this.months[month]);
                }
                cmbMonth.IsEnabled = true;
            } else
            {
                cmbMonth.IsEnabled = false;
            }
            cmbMonth.ItemsSource = items;
            cmbMonth.SelectedItem = all;
        }

        private void cmbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillMonthBox();
            updateData();
        }

        private void CmbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateData();
        }
        private void CmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateData();
        }

        private void updateData() {
            getCmbValues();
            initalizeDatagrid();
        }

        private void initalizeDatagrid() {
            List<Budget> budget = new List<Budget>();
            budget.Add(new Budget() { Datum = "gesamt", Einnahmen = calculate(true), Ausgaben = calculate(false) }) ;
            dgBudget.ItemsSource = budget;
        }

        private double calculate(bool isIncome)
        {
            double sum = 0.0;
            List<Entry> entriesList = entries.entries;
            if (isIncome) {
                entriesList = entriesList.Where(x => x.value > 0).ToList();
            }
            else {
                entriesList = entriesList.Where(x => x.value < 0).ToList();
            }
            if (year != null) {
                entriesList = entriesList.Where(x => x.dateTime.Year == Int32.Parse(year)).ToList();
            } 
            if (month != null) {
                entriesList = entriesList.Where(x => x.dateTime.Month == DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month).ToList();
            }
            if (category != null) {
                entriesList = entriesList.Where(x => x.category.name == category).ToList();
            }
            entriesList.ForEach(x => sum += x.value);
            return Math.Abs(sum);
        }

        private void getCmbValues() {
            year = cmbYear.Text;
            month = cmbMonth.Text;
            category = cmbCategory.Text;

            if (year == all) {
                year = null;
            }
            if (month == all) {
                month = null;
            }
            if (category == all) {
                category = null;
            }
        }
    }

    public class Budget
    {
        public string Datum { get; set; }

        public double Einnahmen { get; set; }

        public double Ausgaben { get; set; }
    }
}
