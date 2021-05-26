using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using WixFileRecordCreator.DataModels;


namespace WixFileRecordCreator.DataContexts
{
    /// <summary>
    /// Контекст данных главного окна
    /// </summary>
    public class MainDataContext : INotifyPropertyChanged
    {
        #region Поля класса

        private string _folderName = string.Empty;
        /// <summary>
        /// Имя папки с файлами
        /// </summary>
        public string FolderName
        {
            get => _folderName;
            set
            {
                if(_folderName != value)
                {
                    _folderName = value;
                    OnPropertyChanged();

                    GetResultPath();
                }
            }
        }


        private bool _folderIsVariable = false;
        /// <summary>
        /// Путь является переменной
        /// </summary>
        public bool FolderIsVariable
        {
            get => _folderIsVariable;
            set
            {
                if(_folderIsVariable != value)
                {
                    _folderIsVariable = value;
                    OnPropertyChanged();

                    GetResultPath();
                }
            }
        }


        private string _variableName = string.Empty;
        /// <summary>
        /// Имя переменной
        /// </summary>
        public string VariableName
        {
            get => _variableName;
            set
            {
                if(_variableName != value)
                {
                    _variableName = value;
                    OnPropertyChanged();

                    GetResultPath();
                }
            }
        }


        private string _resultPath = string.Empty;
        /// <summary>
        /// Результирующий путь
        /// </summary>
        public string ResultPath
        {
            get => _resultPath;
            private set
            {
                if(_resultPath != value)
                {
                    _resultPath = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool _keyPath = false;
        /// <summary>
        /// KeyPath
        /// </summary>
        public bool KeyPath
        {
            get => _keyPath;
            set
            {
                if (_keyPath != value)
                {
                    _keyPath = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool _checksum = true;
        /// <summary>
        /// Checksum
        /// </summary>
        public bool Checksum
        {
            get => _checksum;
            set
            {
                if (_checksum != value)
                {
                    _checksum = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool _vital = true;
        /// <summary>
        /// Vital
        /// </summary>
        public bool Vital
        {
            get => _vital;
            set
            {
                if (_vital != value)
                {
                    _vital = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool _useIdPrefix = false;
        /// <summary>
        /// Использовать префикс для Id
        /// </summary>
        public bool UseIdPrefix
        {
            get => _useIdPrefix;
            set
            {
                if(_useIdPrefix != value)
                {
                    _useIdPrefix = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _idPrefix = string.Empty;
        /// <summary>
        /// Префикс идентификатора
        /// </summary>
        public string IdPrefix
        {
            get => _idPrefix;
            set
            {
                if (_idPrefix != value)
                {
                    _idPrefix = value;
                    OnPropertyChanged();
                }
            }
        }


        private ObservableCollection<string> _usedExtensions = new ObservableCollection<string>();
        /// <summary>
        /// Используемые расширения файлов
        /// </summary>
        public ObservableCollection<string> UsedExtensions
        {
            get => _usedExtensions;
            private set
            {
                if(_usedExtensions != value)
                {
                    _usedExtensions = value;
                    OnPropertyChanged();
                }
            }
        }


        private String _selectedExtension = null;
        /// <summary>
        /// Выбранное расширение из списка
        /// </summary>
        public String SelectedExtension
        {
            get => _selectedExtension;
            set
            {
                if(_selectedExtension != value)
                {
                    _selectedExtension = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _addableExtension = string.Empty;
        /// <summary>
        /// Добавляемое расширение
        /// </summary>
        public string AddableExtension
        {
            get => _addableExtension;
            set
            {
                if(_addableExtension != value)
                {
                    _addableExtension = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool _areExtensionsLocked = false;
        /// <summary>
        /// Список расширений заблокирован
        /// </summary>
        public bool AreExtensionsLocked
        {
            get => _areExtensionsLocked;
            set
            {
                if(_areExtensionsLocked != value)
                {
                    _areExtensionsLocked = value;
                    OnPropertyChanged();
                }
            }
        }


        private ObservableCollection<FileRecord> _fileRecords = new ObservableCollection<FileRecord>();
        /// <summary>
        /// Список записей о файлах
        /// </summary>
        public ObservableCollection<FileRecord> FileRecords
        {
            get => _fileRecords;
            private set
            {
                if(_fileRecords != value)
                {
                    _fileRecords = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _resultText = string.Empty;
        /// <summary>
        /// Результирующий текст
        /// </summary>
        public string ResultText
        {
            get => _resultText;
            private set
            {
                if(_resultText != value)
                {
                    _resultText = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion


        #region Методы


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        /// <summary>
        /// Получить итоговый путь
        /// </summary>
        private void GetResultPath()
        {
            if (!FolderIsVariable)
                ResultPath = FolderName;
            else if (VariableName != "")
                ResultPath = $"$(var.{VariableName})\\";
            else
                ResultPath = string.Empty;
        }


        /// <summary>
        /// Удалить выбранное расширение
        /// </summary>
        public void DeleteExtension()
        {
            if(SelectedExtension != null && !AreExtensionsLocked)
            {
                UsedExtensions.Remove(SelectedExtension);
                SelectedExtension = null;
            }
        }


        /// <summary>
        /// Добавить расширение в список используемых расширений
        /// </summary>
        public void AddExtension()
        {
            if (AddableExtension != null && AddableExtension.Trim() != "" && !AreExtensionsLocked)
            {
                if (AddableExtension.Trim()[0] != '.')
                    AddableExtension = "." + AddableExtension.Trim().ToLower();
                else
                    AddableExtension = AddableExtension.Trim();


                if (!UsedExtensions.Contains(AddableExtension))
                {
                    UsedExtensions.Add(AddableExtension);
                    AddableExtension = string.Empty;
                }
                else
                    MessageBox.Show("Такое расширение уже добавлено", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        /// <summary>
        /// Выбрать папку
        /// </summary>
        public void SelectFolder()
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog()
            {
                SelectedPath = FolderName
            };

            if (folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            FolderName = folderBrowserDialog.SelectedPath;
        }


        /// <summary>
        /// Загрузить список расширений из папки
        /// </summary>
        public void LoadExtensionsFromFolder()
        {
            if(!AreExtensionsLocked && FolderName != "")
            {
                if(!Directory.Exists(FolderName))
                {
                    MessageBox.Show("Директория не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string[] files = Directory.GetFiles(FolderName);

                if(files == null || files.Length == 0)
                {
                    MessageBox.Show("Ошибка получения списка файлов из директории", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                UsedExtensions = new ObservableCollection<string>();

                foreach(string fileName in files)
                {
                    FileInfo fileInfo = new FileInfo(fileName);

                    if (!UsedExtensions.Contains(fileInfo.Extension.ToLower()))
                        UsedExtensions.Add(fileInfo.Extension.ToLower());
                }
            }
        }


        /// <summary>
        /// Получить информацию о файлах
        /// </summary>
        public void GetFileRecords()
        {
            if (FolderName == "" || UsedExtensions == null || UsedExtensions.Count == 0)
                return;

            if (!Directory.Exists(FolderName))
            {
                MessageBox.Show("Директория не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] files = Directory.GetFiles(FolderName);

            if (files == null || files.Length == 0)
            {
                MessageBox.Show("Ошибка получения списка файлов из директории", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            FileRecords = new ObservableCollection<FileRecord>();
            ResultText = string.Empty;

            string _text = string.Empty;

            foreach(string fileName in files)
            {
                FileInfo fileInfo = new FileInfo(fileName);

                if (!UsedExtensions.Contains(fileInfo.Extension.ToLower()))
                    continue;

                FileRecord fileRecord = new FileRecord()
                {
                    KeyPath = this.KeyPath,
                    Vital = this.Vital,
                    Checksum = this.Checksum,
                    IdPrefix = this.IdPrefix,
                    Path = this.ResultPath,
                    FileName = fileInfo.Name
                };

                FileRecords.Add(fileRecord);

                _text += fileRecord.ResultString + "\n";
            }

            ResultText = _text.Trim();
        }


        /// <summary>
        /// Очистить данные
        /// </summary>
        public void ClearAll()
        {
            FolderName = string.Empty;
            IdPrefix = string.Empty;
            UseIdPrefix = false;

            FolderIsVariable = false;
            VariableName = string.Empty;

            FileRecords = new ObservableCollection<FileRecord>();
            ResultText = string.Empty;

            if(!AreExtensionsLocked)
            {
                UsedExtensions = new ObservableCollection<string>();
                SelectedExtension = null;
                AddableExtension = string.Empty;
            }
        }

        #endregion
    }
}