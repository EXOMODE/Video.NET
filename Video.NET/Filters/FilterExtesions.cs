using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Video.NET.IO.Streams;

namespace Video.NET.Filters
{
    /// <summary>
    /// Расширения фильтров.
    /// </summary>
    public static class FilterExtesions
    {
        public static Video AddFilter<T>(this Video video, T filter) where T : IFilter
        {
            long fileIndex = filter.FileIndex;
            long streamIndex = filter.StreamIndex;

            TypeInfo typeInfo = typeof(T).GetTypeInfo();
            
            ForStreamAttribute forStreamAttribute = typeInfo.GetCustomAttribute<ForStreamAttribute>();

            if (streamIndex >= 0 && forStreamAttribute != null)
            {
                IEnumerable<IStream> streams = from i in video.Inputs
                                               from s in i.Streams
                                               where i.Index == fileIndex && s.OwnerIndex == fileIndex && s.Index == streamIndex
                                               select s;

                foreach (IStream stream in streams)
                {
                    switch (stream)
                    {
                        case AudioStream _:
                            if (forStreamAttribute.Type == typeof(AudioStream)) video.ComplexFilter.Add(filter);
                            break;

                        case VideoStream _:
                            if (forStreamAttribute.Type == typeof(VideoStream)) video.ComplexFilter.Add(filter);
                            break;
                    }
                }

                return video;
            }

            ForFileAttribute forFileAttribute = typeInfo.GetCustomAttribute<ForFileAttribute>();
            ForVideoAttribute forVideoAttribute = typeInfo.GetCustomAttribute<ForVideoAttribute>();

            if ((fileIndex >= 0 && forFileAttribute != null) || forVideoAttribute != null)
            {
                video.ComplexFilter.Add(filter);
                return video;
            }
            
            return video;
        }

        public static string JoinFilters(this IEnumerable<IFilter> filters)
        {
            if (filters == null || filters.Count() == 0) return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (IFilter filter in filters) sb.Append(filter.ToString() + ',');

            return sb.ToString(0, sb.Length - 1);
        }
    }
}