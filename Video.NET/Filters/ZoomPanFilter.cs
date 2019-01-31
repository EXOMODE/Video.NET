using System;
using Video.NET.IO.Streams;

namespace Video.NET.Filters
{
    /// <summary>
    /// Представляет фильтр зумирования.
    /// </summary>
    [ForStream(typeof(VideoStream))]
    [ForFile]
    public class ZoomPanFilter : Filter
    {
        /// <summary>
        /// Название фильтра.
        /// </summary>
        public override string Name => "zoompan";

        public double From { get; protected set; }
        public double To { get; protected set; }
        public TimeSpan Duration { get; protected set; }
        public double Speed { get; protected set; }
        public long X { get; protected set; }
        public long Y { get; protected set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ZoomPanFilter"/>.
        /// </summary>
        /// <param name="fileIndex">Индекс файла.</param>
        /// <param name="streamIndex">Индекс потока.</param>
        /// <param name="from">Исходное значение зума.</param>
        /// <param name="to">Конечное значение зума.</param>
        /// <param name="duration">Длительность эффекта.</param>
        /// <param name="speed"></param>
        /// <param name="x">Смещение по оси X.</param>
        /// <param name="y">Смещение по оси Y.</param>
        public ZoomPanFilter(long fileIndex, long streamIndex, double from, double to, TimeSpan duration, double speed, long x = 0, long y = 0) : base(fileIndex, streamIndex)
        {
            From = from;
            To = to;
            Duration = duration;
            Speed = speed;
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            string s = Math.Round((Speed / (Duration.TotalSeconds * 100)) * Speed, 3).ToString().Replace(',', '.');
            string from = (From > To ? To : From).ToString().Replace(',', '.');
            string to = (From > To ? From : To).ToString().Replace(',', '.');
            string c = From > To ? "-" : "+";

            return $"{Name}=z='if(lte(zoom,{from}),{to},max({from},zoom{c}{s}))':x='{X}':y='{Y}':d={(Duration.TotalSeconds * 30) - 30}";
        }
    }

    public static class ZoomPanFilterExtensions
    {
        public static Video ZoomPan(this Video video, long fileIndex, long streamIndex, double from, double to, TimeSpan duration, double speed = 1, long x = 0, long y = 0)
            => video.AddFilter(new ZoomPanFilter(fileIndex, streamIndex, from, to, duration, speed, x, y));

        public static Video ZoomPan(this Video video, long fileIndex, long streamIndex, double from, double to, double speed = 1, long x = 0, long y = 0) => video.ZoomPan(fileIndex, streamIndex, from, to, TimeSpan.FromSeconds(1), speed, x, y);

        public static Video ZoomPan(this Video video, long fileIndex, double from, double to, TimeSpan duration, double speed = 1, long x = 0, long y = 0) => video.ZoomPan(fileIndex, -1, from, to, duration, speed, x, y);

        public static Video ZoomPan(this Video video, long fileIndex, double from, double to, double speed = 1, long x = 0, long y = 0) => video.ZoomPan(fileIndex, from, to, TimeSpan.FromSeconds(1), speed, x, y);

        public static Video ZoomPan(this Video video, double from, double to, TimeSpan duration, double speed = 1, long x = 0, long y = 0) => video.ZoomPan(video.Inputs.Count - 1, from, to, duration, speed, x, y);

        public static Video ZoomPan(this Video video, double from, double to, double speed = 1, long x = 0, long y = 0) => video.ZoomPan(from, to, TimeSpan.FromSeconds(1), speed, x, y);

        public static Video ZoomPan(this Video video, double to, TimeSpan duration, double speed = 1, long x = 0, long y = 0) => video.ZoomPan(1, to, duration, speed, x, y);

        public static Video ZoomPan(this Video video, double to, double speed = 1, long x = 0, long y = 0) => video.ZoomPan(to, TimeSpan.FromSeconds(1), speed, x, y);
    }
}