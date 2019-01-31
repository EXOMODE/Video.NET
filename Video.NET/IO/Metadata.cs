using Newtonsoft.Json;

namespace Video.NET.IO
{
    internal class Metadata
    {
        [JsonProperty("streams")]
        public InputStream[] Streams { get; set; }

        [JsonProperty("format")]
        public FileFormat Format { get; set; }
    }
}