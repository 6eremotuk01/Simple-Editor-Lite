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
    /// Interaction logic for MessageBoxSave.xaml
    /// </summary>
    public partial class WantSaveDialog : Window
    {
        int result = -1;

        public WantSaveDialog(string message, string title = "SimpleEditorLite")
        {
            InitializeComponent();

            Title = title; // Установка заголовка окна
            Message.Text = message; // Установка сообщения
        }

        /* Кнопка "Отмена" */
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            result = -1;
        }

        /* Кнопка "Не сохранять" */
        private void NotSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            result = 0;
        }

        /* Кнопка "Сохранить" */
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            result = 1;
        }

        public int Result
        {
            get { return result; }
        }

        /* При закрытии окна */
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            result = -1;
        }
    }
}
