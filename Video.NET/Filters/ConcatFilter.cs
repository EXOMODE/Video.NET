using System.Collections.Generic;
using System.Linq;
using System.Text;
using Video.NET.IO.Streams;

namespace Video.NET.Filters
{
    /// <summary>
    /// Представляет фильтр склеивания фрагментов.
    /// </summary>
    [ForVideo]
    public class ConcatFilter : Filter
    {
        protected List<long> videoIndexes = new List<long>();
        protected List<long> audioIndexes = new List<long>();

        public override string Name => "concat";

        public long Count { get; protected set; }
        public long VideoCount { get; protected set; }
        public long AudioCount { get; protected set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConcatFilter"/>.
        /// </summary>
        /// <param name="count">Количество потоков.</param>
        /// <param name="videoCount">Количество видеопотоков.</param>
        /// <param name="audioCount">Количество аудиопотоков.</param>
        public ConcatFilter(long count, long videoCount, long audioCount, IEnumerable<long> videoIndexes = null, IEnumerable<long> audioIndexes = null) : base(-1, -1)
        {
            Count = count;
            VideoCount = videoCount;
            AudioCount = audioCount;

            if (videoIndexes != null) this.videoIndexes = new List<long>(videoIndexes);
            if (audioIndexes != null) this.audioIndexes = new List<long>(audioIndexes);
        }

        /// <summary>
        /// Преобразует фильтр в строковое представление синтаксиса FFmpeg.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Count; i++)
            {
                if (VideoCount > 0 && videoIndexes.Count > 0 && videoIndexes.Contains(i)) sb.Append($"[v{i}]");
                if (AudioCount > 0 && audioIndexes.Count > 0 && audioIndexes.Contains(i)) sb.Append($"[a{i}]");
            }

            sb.Append($"{Name}=n={Count}:v={(videoIndexes.Count > 0 ? VideoCount : 0)}:a={(audioIndexes.Count > 0 ? AudioCount : 0)}");

            if (VideoCount > 0 && videoIndexes.Count > 0) sb.Append($"[v]");
            if (AudioCount > 0 && audioIndexes.Count > 0) sb.Append($"[a]");

            return sb.ToString();
        }
    }

    public static class ConcatFilterExtensions
    {
        public static Video Concat(this Video video)
        {
            IEnumerable<long> videoIndexes = from input in video.Inputs
                                             from stream in input.Streams
                                             where stream is VideoStream
                                             select input.Index;

            IEnumerable<long> audioIndexes = from input in video.Inputs
                                             from stream in input.Streams
                                             where stream is AudioStream
                                             select input.Index;

            return video.AddFilter(new ConcatFilter(video.Inputs.Count, 1, 1, videoIndexes, audioIndexes));
        }
    }
}