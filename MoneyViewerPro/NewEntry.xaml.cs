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
    /// Interaktionslogik für NewEntry.xaml
    /// </summary>
    public partial class NewEntry : Window
    {
        CategoryList categories;
        EntryList entries;
        private double value;
        public NewEntry(Window owner, EntryList entries, CategoryList categories)
        {
            this.Owner = owner;
            InitializeComponent();
            this.categories = categories;
            this.entries = entries;
            this.cmbCategory.ItemsSource = categories.categories;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            double price;
            if(string.IsNullOrWhiteSpace(txbDescription.Text))
            {
                MessageBox.Show(this, "Bitte fügen Sie eine Beschreibung hinzu", "Hinzufügen nicht möglich", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if(cmbCategory.SelectedItem == null)
            {
                MessageBox.Show(this, "Bitte wählen Sie eine Kategorie", "Hinzufügen nicht möglich", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if(!datName.SelectedDate.HasValue)
            {
                MessageBox.Show(this, "Bitte wählen Sie ein Datum", "Hinzufügen nicht möglich", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if(ckbIsFremdwaehrung.IsChecked.Value && string.IsNullOrWhiteSpace(txbFremdwaehrung.Text))
            {
                MessageBox.Show(this, "Bitte geben Sie eine Fremdwährung an oder entfernen Sie den Haken bei Fremdwährung", "Hinzufügen nicht möglich", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            entries.addEntry(new Entry(cmbCategory.SelectedItem as Category, txbDescription.Text, radEinnahmen.IsChecked.Value ? value : -1 * value, datName.SelectedDate.Value));
            Close();
        }

        private void changed(object sender, EventArgs e)
        {
            bool enabled = true;
            enabled = enabled && (radAusgaben.IsChecked.Value || radEinnahmen.IsChecked.Value);
            enabled = enabled && value > -1;
            enabled = enabled && !string.IsNullOrWhiteSpace(txbDescription.Text);
            enabled = enabled && datName.SelectedDate.HasValue;
            enabled = enabled && !string.IsNullOrWhiteSpace(txbPrice.Text);
            btnSave.IsEnabled = enabled;
        }

        private void txbPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            string output = "";
            bool noDotUsed = true;
            foreach(char character in txbPrice.Text.ToCharArray())
            {
                int number;
                if(int.TryParse(character.ToString(), out number))
                {
                    output += character;
                } else if (noDotUsed && (character == ',' || character == '.'))
                {
                    output += '.';
                    noDotUsed = false;
                }
            }
            txbPrice.Text = output;
            txbPrice.Select(txbPrice.Text.Length, 0);
            calcValue();
            changed(sender, e);
        }

        private void datName_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.datName.SelectedDate.HasValue && this.datName.SelectedDate.Value.Date > DateTime.Now.Date)
            {
                this.datName.SelectedDate = null;
            }
            calcValue();
            changed(sender, e);
        }

        private void txbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            changed(sender, e);
        }

        private void txbFremdwaehrung_TextChanged(object sender, TextChangedEventArgs e)
        {
            calcValue();
            changed(sender, e);
        }

        private void calcValue()
        {
            value = string.IsNullOrWhiteSpace(txbPrice.Text) ? -1 : Convert.ToDouble(txbPrice.Text);
            if (value > -1 && ckbIsFremdwaehrung.IsChecked.HasValue && ckbIsFremdwaehrung.IsChecked.Value)
            {
                try
                {
                    if(!string.IsNullOrWhiteSpace(txbFremdwaehrung.Text) && txbFremdwaehrung.Text.Trim().Length == 3 && datName.SelectedDate.HasValue)
                    {
                        ExchangeRate rate = ExchangeRate.getExchangeRate(txbFremdwaehrung.Text.Trim().ToUpper(), "CHF", datName.SelectedDate.Value);
                        value = rate.calculate(value); 
                        lblFremdwaehrungValue.Content = "CHF " + Math.Round(value, 2) + " (Wechselkurs " + txbFremdwaehrung.Text.Trim().ToUpper() + " zu CHF am " + datName.SelectedDate.Value.ToString("dd.MM.yyyy") + ": " + rate.Factor + ")";
                    } else
                    {
                        lblFremdwaehrungValue.Content = "";
                        value = -1;
                    }
                }
                catch
                {
                    lblFremdwaehrungValue.Content = "Wechselkurs konnte nicht abgerufen werden";
                    value = -1;
                }
            } else
            {
                lblFremdwaehrungValue.Content = "";
            }
            value = Math.Round(value, 2);
        }

        private void radEinnahmen_Checked(object sender, RoutedEventArgs e)
        {
            changed(sender, e);
        }

        private void radAusgaben_Checked(object sender, RoutedEventArgs e)
        {
            changed(sender, e);
        }

        private void cmbCategory_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            changed(sender, null);
        }

        private void ckbIsFremdwaehrung_Checked(object sender, RoutedEventArgs e)
        {
            lblFremdwaehrung.IsEnabled = true;
            txbFremdwaehrung.IsEnabled = true;
            calcValue();
            changed(sender, e);
        }

        private void ckbIsFremdwaehrung_Unchecked(object sender, RoutedEventArgs e)
        {
            lblFremdwaehrung.IsEnabled = false;
            txbFremdwaehrung.IsEnabled = false;
            calcValue();
            changed(sender, e);
        }
    }
}
