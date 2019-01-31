namespace Video.NET.IO.Codecs
{
    /// <summary>
    /// Представляет информацию о кодеке.
    /// </summary>
    public abstract class Codec
    {
        /// <summary>
        /// Имя кодека.
        /// </summary>
        public string Name { get; set; }
    }
}