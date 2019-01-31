using Newtonsoft.Json;
using System.Collections.Generic;
using Video.NET.Utils;

namespace Video.NET.IO
{
    internal class InputStream
    {
        [JsonProperty("index")]
        public long Index { get; set; }

        [JsonProperty("codec_name")]
        public string CodecName { get; set; }

        [JsonProperty("codec_long_name")]
        public string CodecLongName { get; set; }

        [JsonProperty("profile")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Profile { get; set; }

        [JsonProperty("codec_type")]
        public string CodecType { get; set; }

        [JsonProperty("codec_time_base")]
        public string CodecTimeBase { get; set; }

        [JsonProperty("codec_tag_string")]
        public string CodecTagString { get; set; }

        [JsonProperty("codec_tag")]
        public string CodecTag { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("coded_width")]
        public long CodedWidth { get; set; }

        [JsonProperty("coded_height")]
        public long CodedHeight { get; set; }

        [JsonProperty("has_b_frames")]
        public long HasBFrames { get; set; }

        [JsonProperty("sample_aspect_ratio")]
        public string SampleAspectRatio { get; set; }

        [JsonProperty("display_aspect_ratio")]
        public string DisplayAspectRatio { get; set; }

        [JsonProperty("pix_fmt")]
        public string PixFmt { get; set; }

        [JsonProperty("level")]
        public long Level { get; set; }

        [JsonProperty("color_range")]
        public string ColorRange { get; set; }

        [JsonProperty("color_space")]
        public string ColorSpace { get; set; }

        [JsonProperty("chroma_location")]
        public string ChromaLocation { get; set; }

        [JsonProperty("refs")]
        public long Refs { get; set; }

        [JsonProperty("r_frame_rate")]
        public string RFrameRate { get; set; }

        [JsonProperty("avg_frame_rate")]
        public string AvgFrameRate { get; set; }

        [JsonProperty("time_base")]
        public string TimeBase { get; set; }

        [JsonProperty("start_pts")]
        public long StartPts { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("duration_ts")]
        public long DurationTs { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("bits_per_raw_sample")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long BitsPerRawSample { get; set; }

        [JsonProperty("disposition")]
        public Dictionary<string, long> Disposition { get; set; }
    }
}