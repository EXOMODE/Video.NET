using Video.NET.IO.Codecs;

namespace Video.NET.IO.Streams
{
    /// <summary>
    /// Представляет поток файла.
    /// </summary>
    /// <typeparam name="TCodec">Тип кодека.</typeparam>
    public abstract class Stream<TCodec> : IStream
        where TCodec : Codec
    {
        /// <summary>
        /// Индекс потока в файле.
        /// </summary>
        public long Index { get; protected set; }

        /// <summary>
        /// Индекс файла-держателя потока.
        /// </summary>
        public long OwnerIndex { get; protected set; }

        /// <summary>
        /// Кодек.
        /// </summary>
        public TCodec Codec { get; protected set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Stream"/>.
        /// </summary>
        /// <param name="index">Индекс потока в файле.</param>
        /// <param name="ownerIndex">Индекс файла-держателя потока.</param>
        /// <param name="codec">Кодек.</param>
        internal Stream(long index, long ownerIndex, TCodec codec)
        {
            Index = index;
            OwnerIndex = ownerIndex;
            Codec = codec;
        }
    }
}