using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WixFileRecordCreator.DataModels
{
    /// <summary>
    /// Модель записи о файле
    /// </summary>
    public class FileRecord : INotifyPropertyChanged
    {
        #region Поля класса


        private bool _keyPath = false;
        /// <summary>
        /// KeyPath
        /// </summary>
        public bool KeyPath
        {
            get => _keyPath;
            set
            {
                if(_keyPath != value)
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
                if(_checksum != value)
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
                if(_vital != value)
                {
                    _vital = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _fileName = string.Empty;
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName
        {
            get => _fileName;
            set
            {
                if(_fileName != value)
                {
                    _fileName = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _path = string.Empty;
        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string Path
        {
            get => _path;
            set
            {
                if(_path != value)
                {
                    _path = value;
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
                if(_idPrefix != value)
                {
                    _idPrefix = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _resultString = string.Empty;
        /// <summary>
        /// Результирующая строка
        /// </summary>
        public string ResultString
        {
            get => _resultString;
            private set
            {
                if (_resultString != value)
                {
                    _resultString = value;
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

            GetResultString();
        }


        /// <summary>
        /// Получение результирующей строки  TODO
        /// </summary>
        private void GetResultString()
        {
            if (FileName == "" || Path == "")
                return;

            ResultString = $"<File Id=\"{IdPrefix + Helpers.StringConverter.GetStringWithoutSymbols(FileName)}\" KeyPath=\"{Helpers.StringConverter.BoolToString(KeyPath)}\" Name=\"{FileName}\" Checksum=\"{Helpers.StringConverter.BoolToString(Checksum)}\" Vital=\"{Helpers.StringConverter.BoolToString(Vital)}\" Source=\"{Helpers.StringConverter.CombinePath(Path, FileName)}\" />";
        }
    }
}
