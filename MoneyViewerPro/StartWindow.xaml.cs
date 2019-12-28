using System;
using System.Collections.Generic;
using System.IO;
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
        public string Path { get; set; } = null;
        public string Password { get; set; } = null;

        public StartWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public StartWindow(Window owner, EntryList entries, CategoryList categories, StartWindowOptions options)
        {
            this.Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();
            this.entries = entries;
            this.categories = categories;
            this.option = options;
            if(option == StartWindowOptions.SAVE)
            {
                this.btnNew.Content = "Abbrechen";
                this.btnOpen.Content = "Speichern";
                this.lblPassword.Content = "Passwort (optional)";
                this.pwbPassword.IsEnabled = true;
                this.Title = "Speichern - Money Viewer Pro";
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            if(option == StartWindowOptions.SAVE)
            {
                try
                {
                    if(string.IsNullOrWhiteSpace(pwbPassword.Password))
                    {
                        FileWriter.write(new FileData(entries, categories), txbFile.Text);
                    } else
                    {
                        FileWriter.write(new FileData(entries, categories), txbFile.Text, pwbPassword.Password);
                    }
                    this.Successful = true;
                    Close();
                } catch
                {
                    MessageBox.Show(this, "Es gab einen Fehler beim Speichern. Möglicherweise wird es von einem anderen Programm oder Prozess verwendet oder der Pfad ist ungültig. Versuchen Sie es nochmal oder wählen Sie einen anderen Pfad", "Fehler beim Speichern", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } else
            {
                try
                {
                    FileData data = new FileData();
                    Password = null;
                    if (string.IsNullOrWhiteSpace(pwbPassword.Password))
                    {
                        data = FileLoader.load(txbFile.Text);
                    }
                    else
                    {
                        data = FileLoader.load(txbFile.Text, pwbPassword.Password);
                        Password = pwbPassword.Password;
                    }
                    new MainWindow(data.EntryList, data.CategoryList, txbFile.Text, Password).Show();
                    this.Successful = true;
                    Close();
                } catch (Exception ex)
                {
                    if(ex.Message == "decrypt error")
                    {
                        MessageBox.Show(this, "Das eingegebene Passwort ist falsch oder es wurde beim Speichern keines verwendet.", "Fehler beim Lesen", MessageBoxButton.OK, MessageBoxImage.Error);
                    } else if(ex.Message == "file format error")
                    {
                        MessageBox.Show(this, "Das File hat nicht den erwarteten Inhalt. Möglicherweise ist es mit einem Passwort verschlüsselt oder der Inhalt wurde verändert.", "Fehler beim Lesen", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(this, "Es gab einen Fehler beim Lesen des angegebenen Files. Möglicherweise wird es von einem anderen Programm oder Prozess verwendet oder der Pfad ist ungültig. Überprüfen Sie den Pfad und schliessen Sie alle Programme, die das File verwenden.", "Fehler beim Lesen", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
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
            if(option == StartWindowOptions.OPEN)
            {
                btnOpen.IsEnabled = !string.IsNullOrWhiteSpace(txbFile.Text) && File.Exists(txbFile.Text);
            } else
            {
                btnOpen.IsEnabled = !string.IsNullOrWhiteSpace(txbFile.Text);
            }
        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            /*
            string encrypted = Encrypter.encrypt("teststring", "testpasswort");
            MessageBox.Show(encrypted);
            MessageBox.Show(Encrypter.decrypt(encrypted, "testpasswort"));
            try
            {
                MessageBox.Show(Encrypter.decrypt(encrypted, "falschespasswort"));
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
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
                btnOpen.IsEnabled = true;
            }

        }
    }
}
