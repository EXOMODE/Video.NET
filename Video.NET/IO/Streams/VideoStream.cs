using Video.NET.IO.Codecs;

namespace Video.NET.IO.Streams
{
    /// <summary>
    /// Представляет поток видео.
    /// </summary>
    public class VideoStream : Stream<VideoCodec>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VideoStream"/>.
        /// </summary>
        /// <param name="index">Индекс потока в файле.</param>
        /// <param name="ownerIndex">Индекс файла-держателя потока.</param>
        internal VideoStream(long index, long ownerIndex) : base(index, ownerIndex, new VideoCodec()) { }
    }
}