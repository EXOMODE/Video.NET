using System;
using Video.NET.IO.Streams;

namespace Video.NET.IO.Files
{
    /// <summary>
    /// Представляет базовый интерфейс файла.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Индекс файла.
        /// </summary>
        long Index { get; }

        /// <summary>
        /// Имя файла.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Путь к файлу (включая имя файла с расширением).
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Определяет, существует ли файл по заданному пути.
        /// </summary>
        bool IsExists { get; }

        /// <summary>
        /// Размер файла (в байтах).
        /// </summary>
        long Size { get; }

        /// <summary>
        /// Битрейт.
        /// </summary>
        long BitRate { get; }

        /// <summary>
        /// Длительность файла.
        /// </summary>
        TimeSpan Duration { get; }

        bool IsLoop { get; }

        /// <summary>
        /// Потоки файла.
        /// </summary>
        IStream[] Streams { get; }
    }
}