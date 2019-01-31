namespace Video.NET.IO.Streams
{
    /// <summary>
    /// Представляет базовый интерфейс потока.
    /// </summary>
    public interface IStream
    {
        /// <summary>
        /// Индекс потока в файле.
        /// </summary>
        long Index { get; }

        /// <summary>
        /// Индекс файла-держателя потока.
        /// </summary>
        long OwnerIndex { get; }
    }
}