using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Video.NET.IO.Streams;

namespace Video.NET.Filters
{
    public class ComplexFilter : IList<IFilter>
    {
        protected IList<IFilter> filters;

        public IFilter this[int index] { get => filters[index]; set => filters[index] = value; }

        public int Count => filters.Count;

        public bool IsReadOnly => filters.IsReadOnly;

        internal ComplexFilter(IList<IFilter> filters) : base()
        {
            this.filters = new List<IFilter>();

            if (filters != null && filters.Count() > 0) this.filters = filters;
        }

        internal ComplexFilter(params IFilter[] filters) : this(new List<IFilter>(filters)) { }

        internal ComplexFilter() : this(new List<IFilter>()) { }

        public void Add(IFilter item) => filters.Add(item);

        public void Clear() => filters.Clear();

        public bool Contains(IFilter item) => filters.Contains(item);

        public void CopyTo(IFilter[] array, int arrayIndex) => filters.CopyTo(array, arrayIndex);

        public IEnumerator<IFilter> GetEnumerator() => filters.GetEnumerator();

        public int IndexOf(IFilter item) => filters.IndexOf(item);

        public void Insert(int index, IFilter item) => filters.Insert(index, item);

        public bool Remove(IFilter item) => filters.Remove(item);

        public void RemoveAt(int index) => filters.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => filters.GetEnumerator();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("-filter_complex \"");

            var query = from filter in filters
                        group filter by filter.FileIndex into fileFilters
                        select new
                        {
                            FileIndex = fileFilters.Key,
                            StreamFilters = from filter in fileFilters
                                            select filter,
                        };

            string global = null;

            foreach (var fileFilter in query)
            {
                if (fileFilter.FileIndex < 0)
                {
                    foreach (var streamFilter in fileFilter.StreamFilters) global += $"{streamFilter};";
                    continue;
                }

                // TODO: Add support for indexed streams.
                var query2 = from x in fileFilter.StreamFilters
                             where x.StreamIndex < 0
                             let t = x.GetType().GetTypeInfo().GetCustomAttribute<ForStreamAttribute>()
                             where t != null
                             group x by t.Type into s
                             select new
                             {
                                 T = s.Key,
                                 F = s.JoinFilters(),
                             };

                foreach (var q in query2)
                {
                    string i = null;

                    if (q.T == typeof(AudioStream))
                        i = "a";
                    else if (q.T == typeof(VideoStream))
                        i = "v";

                    if (!string.IsNullOrWhiteSpace(i)) sb.Append($"[{fileFilter.FileIndex}:{i}]{q.F}[{i}{fileFilter.FileIndex}];");
                }
            }

            if (!string.IsNullOrWhiteSpace(global)) sb.Append(global.Trim(';'));
            
            sb.Append("\"");

            return sb.ToString();
        }
    }
}