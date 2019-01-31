using System;

namespace Video.NET.IO.Files
{
    /// <summary>
    /// Представляет аудиофайл.
    /// </summary>
    public class AudioFile : File
    {
        /// <summary>
        /// Инициализирует новый экземпяр класса <see cref="AudioFile"/>.
        /// </summary>
        /// <param name="index">Индекс файла.</param>
        /// <param name="path">Полный путь к файлу (включая имя файла).</param>
        /// <param name="duration">Длительность файла.</param>
        /// <param name="isLoop"></param>
        internal AudioFile(long index, string path, TimeSpan duration, bool isLoop = false) : base(index, path, duration, isLoop)
        {

        }
    }
}