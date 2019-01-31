using Video.NET.IO.Codecs;

namespace Video.NET.IO.Streams
{
    /// <summary>
    /// Представляет поток аудио.
    /// </summary>
    public class AudioStream : Stream<AudioCodec>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AudioStream"/>.
        /// </summary>
        /// <param name="index">Индекс потока в файле.</param>
        /// <param name="ownerIndex">Индекс файла-держателя потока.</param>
        internal AudioStream(long index, long ownerIndex) : base(index, ownerIndex, new AudioCodec()) { }
    }
}