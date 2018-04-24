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

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for Reference.xaml
    /// </summary>
    public partial class ReferenceDialog : Window
    {
        public ReferenceDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /* Открытие ссылки на GitHub */
        private void GitHub_URL_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(GitHub_URL.NavigateUri.ToString());
        }

        /* Открытие ссылки на Telegram */
        private void Telegram_URL_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Telegram_URL.NavigateUri.ToString());
        }
    }
}
