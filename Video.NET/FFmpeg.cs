namespace Video.NET
{
    /// <summary>
    /// Представляет вспомогательный класс для FFmpeg.
    /// </summary>
    public class FFmpeg : FFutil<FFmpeg>
    {
        /// <summary>
        /// Имя исполняемого файла FFmpeg по умолчанию.
        /// </summary>
        public override string BinaryFile => "ffmpeg.exe";
    }
}