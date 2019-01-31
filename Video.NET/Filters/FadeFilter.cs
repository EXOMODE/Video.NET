using System;
using Video.NET.IO.Streams;

namespace Video.NET.Filters
{
    /// <summary>
    /// Представляет фильтр перехода.
    /// </summary>
    [ForStream(typeof(VideoStream))]
    [ForFile]
    public class FadeFilter : Filter
    {
        /// <summary>
        /// Название фильтра.
        /// </summary>
        public override string Name => "fade";
        
        /// <summary>
        /// Тип перехода.
        /// </summary>
        public FadeType Type { get; set; }

        /// <summary>
        /// Время начала эффекта.
        /// </summary>
        public TimeSpan Start { get; set; }

        /// <summary>
        /// Длительность эффекта.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Цвет.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Определяет прозрачность.
        /// </summary>
        public bool IsAlpha { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FadeFilter"/>.
        /// </summary>
        /// <param name="fileIndex">Индекс файла.</param>
        /// <param name="streamIndex">Индекс потока.</param>
        /// <param name="type">Тип перехода.</param>
        /// <param name="start">Время начала эффекта.</param>
        /// <param name="duration">Длительность эффекта.</param>
        /// <param name="color">Цвет.</param>
        /// <param name="isAlpha">Определяет прозрачность.</param>
        public FadeFilter(long fileIndex, long streamIndex, FadeType type, TimeSpan start, TimeSpan duration, string color = "black", bool isAlpha = false) : base(fileIndex, streamIndex)
        {
            Type = type;
            Start = start;
            Duration = duration;
            Color = color;
            IsAlpha = isAlpha;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FadeFilter"/>.
        /// </summary>
        /// <param name="fileIndex">Индекс файла.</param>
        /// <param name="streamIndex">Индекс потока.</param>
        /// <param name="start">Время начала эффекта.</param>
        /// <param name="duration">Длительность эффекта.</param>
        /// <param name="color">Цвет.</param>
        /// <param name="isAlpha">Определяет прозрачность.</param>
        public FadeFilter(long fileIndex, long streamIndex, TimeSpan start, TimeSpan duration, string color = "black", bool isAlpha = false) : this(fileIndex, streamIndex, FadeType.In, start, duration, color, isAlpha) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FadeFilter"/>.
        /// </summary>
        /// <param name="fileIndex">Индекс файла.</param>
        /// <param name="streamIndex">Индекс потока.</param>
        /// <param name="type">Тип перехода.</param>
        /// <param name="duration">Длительность эффекта.</param>
        /// <param name="color">Цвет.</param>
        /// <param name="isAlpha">Определяет прозрачность.</param>
        public FadeFilter(long fileIndex, long streamIndex, FadeType type, TimeSpan duration, string color = "black", bool isAlpha = false) : this(fileIndex, streamIndex, type, TimeSpan.FromSeconds(0), duration, color, isAlpha) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FadeFilter"/>.
        /// </summary>
        /// <param name="fileIndex">Индекс файла.</param>
        /// <param name="streamIndex">Индекс потока.</param>
        /// <param name="duration">Длительность эффекта.</param>
        /// <param name="color">Цвет.</param>
        /// <param name="isAlpha">Определяет прозрачность.</param>
        public FadeFilter(long fileIndex, long streamIndex, TimeSpan duration, string color = "black", bool isAlpha = false) : this(fileIndex, streamIndex, FadeType.In, duration, color, isAlpha) { }

        /// <summary>
        /// Преобразует фильтр в строковое представление синтаксиса FFmpeg.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Name}=t={Enum.GetName(typeof(FadeType), Type).ToLower()}:st={Start.TotalSeconds.ToString().Replace(',', '.')}:d={Duration.TotalSeconds.ToString().Replace(',', '.')}:color={Color}:alpha={(IsAlpha ? '1' : '0')}";
    }

    public static class FadeFilterExtensions
    {
        public static Video FadeIn(this Video video, int fileIndex, int streamIndex, TimeSpan start, TimeSpan duration, string color = "black", bool isAlpha = false)
            => video.AddFilter(new FadeFilter(fileIndex, streamIndex, FadeType.In, start, duration, color, isAlpha));

        public static Video FadeIn(this Video video, int fileIndex, int streamIndex, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeIn(fileIndex, streamIndex, TimeSpan.FromSeconds(0), duration, color, isAlpha);

        public static Video FadeIn(this Video video, int fileIndex, TimeSpan start, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeIn(fileIndex, -1, start, duration, color, isAlpha);

        public static Video FadeIn(this Video video, int fileIndex, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeIn(fileIndex, TimeSpan.FromSeconds(0), duration, color, isAlpha);

        public static Video FadeIn(this Video video, TimeSpan start, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeIn(video.Inputs.Count - 1, start, duration, color, isAlpha);

        public static Video FadeIn(this Video video, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeIn(video.Inputs.Count - 1, duration, color, isAlpha);

        public static Video FadeOut(this Video video, int fileIndex, int streamIndex, TimeSpan start, TimeSpan duration, string color = "black", bool isAlpha = false)
            => video.AddFilter(new FadeFilter(fileIndex, streamIndex, FadeType.Out, start, duration, color, isAlpha));

        public static Video FadeOut(this Video video, int fileIndex, int streamIndex, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeOut(fileIndex, streamIndex, TimeSpan.FromSeconds(0), duration, color, isAlpha);

        public static Video FadeOut(this Video video, int fileIndex, TimeSpan start, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeOut(fileIndex, -1, start, duration, color, isAlpha);

        public static Video FadeOut(this Video video, int fileIndex, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeOut(fileIndex, TimeSpan.FromSeconds(0), duration, color, isAlpha);

        public static Video FadeOut(this Video video, TimeSpan start, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeOut(video.Inputs.Count - 1, start, duration, color, isAlpha);

        public static Video FadeOut(this Video video, TimeSpan duration, string color = "black", bool isAlpha = false) => video.FadeOut(TimeSpan.FromSeconds(0), duration, color, isAlpha);
    }
}