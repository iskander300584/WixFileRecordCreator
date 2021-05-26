using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


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


        private List<string> _usedExtensions = new List<string>();
        /// <summary>
        /// Используемые расширения файлов
        /// </summary>
        public List<string> UsedExtensions
        {
            get => _usedExtensions;
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

        #endregion



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
    }
}