using System;

namespace Video.NET.IO.Files
{
    /// <summary>
    /// Представляет файл видео.
    /// </summary>
    public class VideoFile : File
    {
        /// <summary>
        /// Инициализирует новый экземпяр класса <see cref="VideoFile"/>.
        /// </summary>
        /// <param name="index">Индекс файла.</param>
        /// <param name="path">Полный путь к файлу (включая имя файла).</param>
        /// <param name="duration">Длительность файла.</param>
        /// <param name="isLoop"></param>
        internal VideoFile(long index, string path, TimeSpan duration, bool isLoop = false) : base(index, path, duration, isLoop)
        {

        }
    }
}