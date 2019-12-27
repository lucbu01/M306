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
        public NewEntry(EntryList entries, CategoryList categories)
        {
            InitializeComponent();
            this.categories = categories;
            this.entries = entries;
            this.cmbCategory.ItemsSource = categories.categories;
            txbPrice.Text = Convert.ToString(1.51);
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
            entries.addEntry(new Entry(cmbCategory.SelectedItem as Category, txbDescription.Text, Convert.ToDouble(txbPrice.Text), datName.SelectedDate.Value));
        }

        private void changed(object sender, EventArgs e)
        {
            bool enabled = true;
            enabled = enabled && (radAusgaben.IsChecked.Value || radEinnahmen.IsChecked.Value);
            enabled = enabled && (ckbIsFremdwaehrung.IsChecked.Value ? !string.IsNullOrWhiteSpace(txbFremdwaehrung.Text) : true);
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
            changed(sender, e);
        }

        private void datName_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            changed(sender, e);
        }

        private void txbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            changed(sender, e);
        }

        private void txbFremdwaehrung_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show(ExchangeRate.getExchangeRate("EUR", "CHF").Factor.ToString());
            changed(sender, e);
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
            changed(sender, e);
        }

        private void ckbIsFremdwaehrung_Unchecked(object sender, RoutedEventArgs e)
        {
            lblFremdwaehrung.IsEnabled = false;
            txbFremdwaehrung.IsEnabled = false;
            changed(sender, e);
        }
    }
}
