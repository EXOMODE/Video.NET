using System;
using System.Collections.Generic;
using Video.NET.IO.Streams;
using Video.NET.Utils;

namespace Video.NET.IO.Files
{
    /// <summary>
    /// Представляет файл.
    /// </summary>
    /// <typeparam name="TStream">Тип потока файла.</typeparam>
    public abstract class File : IFile
    {
        /// <summary>
        /// Индекс файла.
        /// </summary>
        public long Index { get; protected set; }

        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Полный путь к файлу (включая имя файла).
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// Определяет, существует ли файл по заданному пути.
        /// </summary>
        public bool IsExists => System.IO.File.Exists(Path);

        /// <summary>
        /// Размер файла (в байтах).
        /// </summary>
        public long Size { get; protected set; }

        /// <summary>
        /// Битрейт.
        /// </summary>
        public long BitRate { get; protected set; }

        /// <summary>
        /// Длительность файла.
        /// </summary>
        public TimeSpan Duration { get; protected set; }

        public bool IsLoop { get; protected set; }

        /// <summary>
        /// Потоки файла.
        /// </summary>
        public IStream[] Streams { get; protected set; }

        /// <summary>
        /// Инициализирует новый экземпяр класса <see cref="File"/>.
        /// </summary>
        /// <param name="index">Индекс файла.</param>
        /// <param name="path">Полный путь к файлу (включая имя файла).</param>
        /// <param name="duration">Длительность файла.</param>
        /// <param name="isLoop"></param>
        internal File(long index, string path, TimeSpan duration, bool isLoop = false)
        {
            Path = path;
            Name = System.IO.Path.GetFileName(path);
            Index = index;
            IsLoop = isLoop;

            if (IsExists)
            {
                string json = null;

                FFprobe.Global.Eval($"-v quiet -print_format json -show_format -show_streams -print_format json \"{path}\"", s => json = s);

                if (string.IsNullOrWhiteSpace(json)) throw new FormatException("Неподдерживаемый формат файла.");

                Metadata metadata = json.FromJson<Metadata>();

                if (metadata.Format.NbStreams < 1) throw new FormatException("Неподдерживаемый формат файла.");

                Size = metadata.Format.Size;
                BitRate = metadata.Format.BitRate;
                Duration = duration;

                if (duration == TimeSpan.FromSeconds(0) && TimeSpan.TryParse(metadata.Format.Duration, out TimeSpan d)) Duration = d;
                
                List<IStream> streams = new List<IStream>();
                
                foreach (InputStream s in metadata.Streams)
                {
                    IStream stream = null;

                    switch (s.CodecType)
                    {
                        case "video":
                            stream = new VideoStream(s.Index, index);
                            break;

                        case "audio":
                            stream = new AudioStream(s.Index, index);
                            break;
                    }

                    if (stream != null) streams.Add(stream);
                }

                Streams = streams.ToArray();
            }
        }

        /// <summary>
        /// Анализирует метаданные файла.
        /// </summary>
        /// <param name="index">Индекс файла.</param>
        /// <param name="path">Полный путь к файлу (включая имя файла).</param>
        /// <param name="duration">Длительность файла.</param>
        /// <param name="isLoop"></param>
        /// <returns></returns>
        internal static File Analyse(long index, string path, TimeSpan duration, bool isLoop = false)
        {
            string ext = System.IO.Path.GetExtension(path)?.ToLower()?.Trim('.');

            switch (ext)
            {
                case "jpg":
                case "jpeg":
                case "png":
                case "gif":
                case "bmp":
                case "mp4":
                case "mpeg":
                case "avi":
                    return new VideoFile(index, path, duration, isLoop);

                case "wav":
                case "mp3":
                case "ogg":
                    return new AudioFile(index, path, duration, isLoop);

                default:
                    throw new FormatException("Неподдерживаемый формат файла.");
            }
        }
    }
}