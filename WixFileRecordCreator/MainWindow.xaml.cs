using System.Windows;
using System.Windows.Input;
using WixFileRecordCreator.DataContexts;


namespace WixFileRecordCreator
{
    /// <summary>
    /// Главное окно
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Объявление команд

        // Выбор папки
        private static RoutedUICommand selectFolderCommand = new RoutedUICommand("SelectFolderCommand", "SelectFolderCommand", typeof(MainWindow));
        public static RoutedUICommand SelectFolderCommand
        {
            get => selectFolderCommand;
        }

        
        // Добавить расширение
        private static RoutedUICommand addExtensionCommand = new RoutedUICommand("AddExtensionCommand", "AddExtensionCommand", typeof(MainWindow));
        public static RoutedUICommand AddExtensionCommand
        {
            get => addExtensionCommand;
        }


        // Удалить расширение
        private static RoutedUICommand deleteExtensionCommand = new RoutedUICommand("DeleteExtensionCommand", "DeleteExtensionCommand", typeof(MainWindow));
        public static RoutedUICommand DeleteExtensionCommand
        {
            get => deleteExtensionCommand;
        }


        // Загрузить список расширений
        private static RoutedUICommand loadExtensionsCommand = new RoutedUICommand("LoadExtensionsCommand", "LoadExtensionsCommand", typeof(MainWindow));
        public static RoutedUICommand LoadExtensionsCommand
        {
            get => loadExtensionsCommand;
        }


        // Получить результат
        private static RoutedUICommand getResultCommand = new RoutedUICommand("GetResultCommand", "GetResultCommand", typeof(MainWindow));
        public static RoutedUICommand GetResultCommand
        {
            get => getResultCommand;
        }


        // Очистить данные
        private static RoutedUICommand clearDataCommand = new RoutedUICommand("ClearDataCommand", "ClearDataCommand", typeof(MainWindow));
        public static RoutedUICommand ClearDataCommand
        {
            get => clearDataCommand;
        }


        // Очистить результат
        private static RoutedUICommand clearResultCommand = new RoutedUICommand("ClearResultCommand", "ClearResultCommand", typeof(MainWindow));
        public static RoutedUICommand ClearResultCommand
        {
            get => clearResultCommand;
        }

        #endregion


        /// <summary>
        /// Контекст данных
        /// </summary>
        private MainDataContext context;


        /// <summary>
        /// Главное окно
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            context = new MainDataContext(this);

            this.DataContext = context;
        }


        /// <summary>
        /// Проверка возможности выбора папки
        /// </summary>
        private void SelectFolder_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (context != null);
        }


        /// <summary>
        /// Выбор папки
        /// </summary>
        private void SelectFolder_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (context == null)
                return;

            context.SelectFolder();
        }


        /// <summary>
        /// Проверка возможности добавления расширения
        /// </summary>
        private void AddExtension_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (context != null && !context.AreExtensionsLocked && context.AddableExtension.Trim() != "");
        }


        /// <summary>
        /// Добавить расширение
        /// </summary>
        private void AddExtension_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.AddExtension();
        }


        /// <summary>
        /// Проверка возможности удалить расширение
        /// </summary>
        private void DeleteExtension_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (context != null && !context.AreExtensionsLocked && context.SelectedExtension != null && context.SelectedExtension != "");
        }


        /// <summary>
        /// Удалить расширение
        /// </summary>
        private void DeleteExtension_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.DeleteExtension();
        }


        /// <summary>
        /// Проверка возможности загрузки списка расширений
        /// </summary>
        private void LoadExtensions_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (context != null && !context.AreExtensionsLocked && context.FolderName != "");
        }


        /// <summary>
        /// Загрузка списка расширений
        /// </summary>
        private void LoadExtensions_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.LoadExtensionsFromFolder();
        }


        /// <summary>
        /// Проверка возможности получения результата
        /// </summary>
        private void GetResult_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (context != null && context.FolderName != "" && context.UsedExtensions.Count > 0);
        }


        /// <summary>
        /// Получение результата
        /// </summary>
        private void GetResult_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.GetFileRecords();
        }


        /// <summary>
        /// Проверка возможности очистки данных
        /// </summary>
        private void ClearData_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (context != null && (context.FolderName != "" || context.VariableName != "" || context.IdPrefix != ""));
        }


        /// <summary>
        /// Очистить данные
        /// </summary>
        private void ClearData_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.ClearAll();
        }


        /// <summary>
        /// Проверка возможности очистки результа
        /// </summary>
        private void ClearResult_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (context != null && context.ResultText != "");
        }


        /// <summary>
        /// Очистка результата
        /// </summary>
        private void ClearResult_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.ClearResult();
        }
    }
}