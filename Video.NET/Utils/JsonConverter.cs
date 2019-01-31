using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Video.NET.Utils
{
    public static class JsonConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        public static string ToJson<T>(this T self) => JsonConvert.SerializeObject(self, Settings);

        public static T FromJson<T>(this string json) => JsonConvert.DeserializeObject<T>(json, Settings);
    }
}