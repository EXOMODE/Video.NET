namespace Video.NET.Filters
{
    /// <summary>
    /// Представляет базовый интерфейс фильтра.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Название фильтра.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Индекс файла.
        /// </summary>
        long FileIndex { get; }

        /// <summary>
        /// Индекс потока.
        /// </summary>
        long StreamIndex { get; }
    }
}