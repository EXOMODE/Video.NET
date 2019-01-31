using Newtonsoft.Json;
using Video.NET.Utils;

namespace Video.NET.IO
{
    internal class FileFormat
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("nb_streams")]
        public long NbStreams { get; set; }

        [JsonProperty("nb_programs")]
        public long NbPrograms { get; set; }

        [JsonProperty("format_name")]
        public string FormatName { get; set; }

        [JsonProperty("format_long_name")]
        public string FormatLongName { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("size")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Size { get; set; }

        [JsonProperty("bit_rate")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long BitRate { get; set; }

        [JsonProperty("probe_score")]
        public long ProbeScore { get; set; }
    }
}