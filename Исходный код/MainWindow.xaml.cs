using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Threading;

namespace TextEditor
{
    public partial class MainWindow : Window
    {
        string CurrentDirrectory = String.Empty; // Текущая дирректория
        string CurrentFileName = "Безымянный"; // Начальное название файла
        bool IsSaved = true; // Сохранен файл или нет

        public MainWindow()
        {
            InitializeComponent();
        }


        /* Функция получения файла через стандартное диалогоове окно.
         * Взвращает:
         *  Дирректорию на файл, если файл был выбран
         *  Иначе возвращает пустую строку */
        public string OpenFileName()
        {
            // Создаем окно открытия файла
            OpenFileDialog open_dialog = new OpenFileDialog(); // Объект
            open_dialog.InitialDirectory = Directory.GetCurrentDirectory(); // Устанвливаем начальной дирректорией положение программы на диске
            open_dialog.Filter = "Text documents|*.txt|Any files|*.*"; // Установка ограничений на типы файлов

            // Выводим на экран окно и если файл был выбран возвращаем дирректорию на него 
            if (open_dialog.ShowDialog() != false) return open_dialog.FileName;

            // Иначе возращаем пустую строку
            return String.Empty;
        }

        /* Функция сохранения файла через стандартное диалогоове окно.
         * Взвращает:
         *  Дирректорию на файл, если файл был выбран
         *  Иначе возвращает пустую строку */
        public string SaveFileName()
        {
            // Создаем окно сохранения файла
            SaveFileDialog open_dialog = new SaveFileDialog();
            open_dialog.InitialDirectory = Directory.GetCurrentDirectory(); // Устанвливаем начальной дирректорией положение программы на диске
            open_dialog.Filter = "Text documents|*.txt|Any files|*.*"; // Установка ограничений на типы файлов
            open_dialog.FileName = CurrentFileName; // Ставим текущее наименование файла

            // Выводим на экран окно и если файл был выбран возвращаем дирректорию на него 
            if (open_dialog.ShowDialog() != false) return open_dialog.FileName;

            // Иначе возращаем пустую строку
            return String.Empty;
        }

        
        /* Диалогове окно с вопросом о сохранении файла. 
         *Возвращает:
         *  1 - нажата клавиша "Сохранить", 
         *  0 - если "Не сохранять", 
         *  иначе -1. */
        public int WantSave()
        {
            if (!IsSaved)
            {
                WantSaveDialog msg = new WantSaveDialog("Вы хотите сохранить изменения в файле " + CurrentFileName + "?");
                if (msg.ShowDialog() != false) return msg.Result;
            }
            if (IsSaved) return 0;

            return -1;
        }


        /* Функция сохранения файла. 
         *Возвращает:
         *  ture - файл был сохранен,
         *  иначе false */
        public bool SaveFile()
        {
            // Если текущая дирректория не пустая (файл не был сохранен под конкретным имененем)
            if (CurrentDirrectory != String.Empty)
            {
                File.WriteAllText(CurrentDirrectory, Editor.Text, Encoding.Default); // Запись в файл содержимого редактора

                IsSaved = true; // Файл сохранён

                CurrentFileName = System.IO.Path.GetFileName(CurrentDirrectory); // Установка имени файла
                SetWindowName(CurrentFileName, IsSaved); // Установка заголовка окна

                return true;
            }
            else return SaveFileAs(); // Иначе сохраняем файл как...
        }
        
        /* Функция сохранения файла с указанием имени для него. 
         *Возвращает:
         *  ture - файл был сохранен,
         *  иначе false */
        public bool SaveFileAs()
        {
            CurrentDirrectory = SaveFileName(); // Записывам дирректорию через окно сохранения файла

            // Если текущая дирректория не пустая (файл не был сохранен под конкретным имененем)
            if (CurrentDirrectory != String.Empty)
            {
                File.WriteAllText(CurrentDirrectory, Editor.Text, Encoding.Default); // Запись в файл содержимого редактора

                IsSaved = true; // Файл сохранён

                CurrentFileName = System.IO.Path.GetFileName(CurrentDirrectory); // Установка имени файла
                SetWindowName(CurrentFileName, IsSaved); // Установка заголовка окна

                return true;
            }
            return false;
        }


        /* Установка заголовка окна. */
        public void SetWindowName(string new_window_name, bool is_saved = true)
        {
            StringBuilder builder = new StringBuilder();

            if (!is_saved) builder.Append("*"); // Добавляет в начало наименования звездочку, если файл не сохранен
            builder.Append(new_window_name); //Ъ Добавляем имя файла
            builder.Append(" - SimpleEditorLite"); // Название программы

            this.Title = builder.ToString(); // Устанавливаем заголовок окна
        }

        /* Процедура для кнопки меню "Открыть". */
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (WantSave() == -1) return; // Если пользователь отменил действие

            CurrentDirrectory = OpenFileName(); // Открываем файл

            // Если файл выбран
            if (CurrentDirrectory != String.Empty)
            {
                Editor.Text = File.ReadAllText(CurrentDirrectory, Encoding.Default); // Записываем содержимое файла в редактор

                IsSaved = true; // Файл сохранен

                CurrentFileName = System.IO.Path.GetFileName(CurrentDirrectory); // Текущее имя файла
                SetWindowName(CurrentFileName, IsSaved); // Утановка заголовка окна
            }
        }

        /* Процедура для кнопки меню "Сохранить". */
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        /* Процедура для кнопки меню "Сохранить как". */
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileAs();
        }

        /* Процедура для кнопки меню "Создать". */
        private void New_Click(object sender, RoutedEventArgs e)
        {
            int result = WantSave(); // Рзультат диалогового окна
            if (result == -1) return; // Если пользователя окна
            if (result == 1) if (!SaveFile()) return; // Если пользователь нажал на сохранение файла

            Editor.Clear(); // Осистка текстового редактора

            // Очистка буфера отмены и повтора
            Editor.IsUndoEnabled = false;
            Editor.IsUndoEnabled = true;

            // Установка переменных в начальное состояние
            CurrentDirrectory = String.Empty;
            CurrentFileName = "Безымянный";
            SetWindowName(CurrentFileName);
            IsSaved = true;
        }

        /* Процедура для кнопки меню "Выход". */
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            int result = WantSave(); // Результат диалогового окна
            if (result == -1) return; // Если пользователь отменил сохранение
            if (result == 1) SaveFile();

            Close();
        }

        /* Отображение "О программе". */
        private void Reference_Click(object sender, RoutedEventArgs e)
        {
            ReferenceDialog msg = new ReferenceDialog(); // Создание окна справки
            msg.ShowDialog(); // Вывод диалогового окна
        }


        /* Процедура для кнопки меню "Проверка орфографии". */
        private void SpellCheck_Checked(object sender, RoutedEventArgs e)
        {
            Editor.Language = System.Windows.Markup.XmlLanguage.GetLanguage("ru-ru"); // Установка русского языка для проверки орфографии

            // Переключение состояния проверки орфографии
            if (SpellCheck.IsChecked) Editor.SpellCheck.IsEnabled = true; 
            else Editor.SpellCheck.IsEnabled = false; 
        }

        /* Процедура для кнопки меню "Перенос по словам". */
        private void TextWrapping_Checked(object sender, RoutedEventArgs e)
        {
            // Переключение состояния переноса по словам
            if (TextWrapping.IsChecked) Editor.TextWrapping = System.Windows.TextWrapping.Wrap;
            else Editor.TextWrapping = System.Windows.TextWrapping.NoWrap;
        }


        /* Процедура обработки изменения поля редактора. */
        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Если файл был сохранён
            if (IsSaved)
            {
                IsSaved = false; // Изменяем состояние сохранения

                // Установка наименования окна в зависимости от сохраненности файла
                if (CurrentDirrectory != String.Empty) SetWindowName(CurrentFileName, IsSaved);
                else SetWindowName(CurrentFileName, IsSaved);
            }
        }


        /* Переключение переноса по словам. */
        private void WordWrap_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Переключение состояния переноса по словам
            TextWrapping.IsChecked = !TextWrapping.IsChecked;
            TextWrapping_Checked(sender, e); // Влючение/выключение переноса по словам
        }

        /* Переключение проверки орфографии. */
        private void SpellCheck_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Переключение проверки орфографии
            SpellCheck.IsChecked = !SpellCheck.IsChecked;
            SpellCheck_Checked(sender, e); // Включение/выключение проверки орфографии
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int result = WantSave(); // Результат диалогового окна

            if (result == -1) e.Cancel = true; // Если пользователь окна
            if (result == 1) SaveFile(); // Если пользователь выбрал сохранить
        }
    }

    /* Комманды приложения. */
    public class Commands
    {
        static Commands()
        {
            WordWrap = new RoutedCommand("WordWrap", typeof(MainWindow));
            SpellCheck = new RoutedCommand("InsertDateTime", typeof(MainWindow));
        }

        public static RoutedCommand WordWrap { get; set; }
        public static RoutedCommand SpellCheck { get; set; }
    }
}


