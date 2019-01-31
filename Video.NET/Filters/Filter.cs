namespace Video.NET.Filters
{
    /// <summary>
    /// Представляет базовый класс для реализации фильтра.
    /// </summary>
    public abstract class Filter : IFilter
    {
        /// <summary>
        /// Название фильтра.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Индекс файла.
        /// </summary>
        public long FileIndex { get; protected set; }

        /// <summary>
        /// Индекс потока.
        /// </summary>
        public long StreamIndex { get; protected set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Filter"/>.
        /// </summary>
        /// <param name="fileIndex">Индекс файла.</param>
        /// <param name="streamIndex">Индекс потока.</param>
        public Filter(long fileIndex, long streamIndex)
        {
            FileIndex = fileIndex;
            StreamIndex = streamIndex;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Filter"/>.
        /// </summary>
        /// <param name="fileIndex">Индекс файла.</param>
        public Filter(long fileIndex) : this(fileIndex, 0) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Filter"/>.
        /// </summary>
        public Filter() : this(0) { }
    }
}