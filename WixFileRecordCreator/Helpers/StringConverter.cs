using System;
using System.Linq;
using System.Text;


namespace WixFileRecordCreator.Helpers
{
    /// <summary>
    /// Класс преобразования строк
    /// </summary>
    public class StringConverter
    {
        /// <summary>
        /// Получить строку без служебных символов
        /// </summary>
        /// <param name="source">исходная строка</param>
        /// <returns>возвращает строку без служебных символов</returns>
        public static string GetStringWithoutSymbols(string source)
        {
            if (source.Length == 0)
                return string.Empty;

            StringBuilder str = new StringBuilder();
            foreach (char ch in source)
                if (Char.IsLetter(ch) || Char.IsDigit(ch))
                    str.Append(ch);

            bool deleted = false;

            // удаление первого символа, если он является цифрой
            do
            {
                deleted = false;

                if (str.Length > 0 && Char.IsDigit(str[0]))
                {
                    str.Remove(0, 1);
                    deleted = true;
                }
            }
            while (deleted);

            return str.ToString();
        }


        /// <summary>
        /// Преобразование BOOL в строку
        /// </summary>
        /// <param name="value">значение</param>
        /// <returns>возвращает соответствующее значение для WIX SETUP</returns>
        public static string BoolToString(bool value)
        {
            return (value) ? "yes" : "no";
        }


        /// <summary>
        /// Комбинирование пути и имени файла
        /// </summary>
        /// <param name="path">путь</param>
        /// <param name="fileName">имя файла</param>
        /// <returns>возвращает скомбинированную строку</returns>
        public static string CombinePath(string path, string fileName)
        {
            if (path.Last() != '\\')
                path = path + "\\";

            return path + fileName;
        }
    }
}