namespace Video.NET
{
    /// <summary>
    /// Представляет вспомогательный класс для FFprobe.
    /// </summary>
    public class FFprobe : FFutil<FFprobe>
    {
        /// <summary>
        /// Имя исполняемого файла FFprobe по умолчанию.
        /// </summary>
        public override string BinaryFile => "ffprobe.exe";
    }
}