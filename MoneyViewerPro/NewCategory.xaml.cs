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
    /// Interaktionslogik für NewCategory.xaml
    /// </summary>
    public partial class NewCategory : Window
    {
        private CategoryList categories;
        public NewCategory(Window owner, CategoryList categories)
        {
            this.Owner = owner;
            InitializeComponent();
            this.categories = categories;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(categories.categories.Find(category => category.name == txbName.Text) == null)
            {
                if(string.IsNullOrWhiteSpace(txbName.Text))
                {
                    MessageBox.Show(this, "Diese Kategorie existiert bereits", "Hinzufügen nicht möglich", MessageBoxButton.OK, MessageBoxImage.Error);
                } else
                {
                    this.categories.addCategory(new Category(txbName.Text, txbDescription.Text));
                    Close();
                }
            } else
            {
                MessageBox.Show(this, "Diese Kategorie existiert bereits", "Hinzufügen nicht möglich", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
