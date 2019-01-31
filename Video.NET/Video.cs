using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video.NET.Filters;
using Video.NET.IO.Files;
using Video.NET.IO.Streams;
using SFile = System.IO.File;

namespace Video.NET
{
    public class Video : IDisposable
    {
        public ComplexFilter ComplexFilter { get; set; }

        public List<IFile> Inputs { get; protected set; }
        
        public Video(params string[] files)
        {
            ComplexFilter = new ComplexFilter();
            Inputs = new List<IFile>();

            foreach (string file in files) AddInput(file);
        }

        public Video(IEnumerable<string> files) : this(files.ToArray()) { }

        public Video() : this(new List<string>()) { }

        public Video AddInputs(TimeSpan duration, bool isLoop, params string[] files)
        {
            foreach (string file in files)
            {
                if (!SFile.Exists(file)) throw new FileNotFoundException("Не найден входной файл.", file);
                Inputs.Add(IO.Files.File.Analyse(Inputs.Count, file, duration, isLoop));
            }

            return this;
        }

        public Video AddInputs(bool isLoop, params string[] files) => AddInputs(TimeSpan.FromSeconds(0), isLoop, files);

        public Video AddInputs(params string[] files) => AddInputs(false, files);
        
        public Video AddInputs(TimeSpan duration, bool isLoop, IEnumerable<string> files) => AddInputs(duration, isLoop, files.ToArray());

        public Video AddInputs(TimeSpan duration, IEnumerable<string> files) => AddInputs(duration, false, files);

        public Video AddInputs(bool isLoop, IEnumerable<string> files) => AddInputs(TimeSpan.FromSeconds(0), isLoop, files);

        public Video AddInputs(IEnumerable<string> files) => AddInputs(false, files);

        public Video AddInput(string file, TimeSpan duration, bool isLoop) => AddInputs(duration, isLoop, file);

        public Video AddInput(string file, TimeSpan duration) => AddInput(file, duration, false);

        public Video AddInput(string file, bool isLoop) => AddInput(file, TimeSpan.FromSeconds(0), isLoop);

        public Video AddInput(string file) => AddInput(file, false);

        public async Task<Video> RenderAsync(string output)
        {
            StringBuilder sb = new StringBuilder("-r 1/5 -r 30 ");

            foreach (IFile input in Inputs) sb.Append($"-loop {(input.IsLoop ? '1' : '0')} -t {input.Duration.Hours}:{input.Duration.Minutes}:{input.Duration.Seconds}.{input.Duration.Milliseconds} -i \"{input.Path}\" ");

            sb.Append(ComplexFilter.ToString());

            if (Inputs.Where(x => x.Streams.Where(s => s is AudioStream).Count() > 0).Count() > 0)
                sb.Append($" -map \"[v]\" -map \"[a]\" -y \"{output}\"");
            else
                sb.Append($" -map \"[v]\" -y \"{output}\"");

            await FFmpeg.Global.EvalAsync(sb.ToString());

            return this;
        }

        public Video Render(string output) => RenderAsync(output).Result;

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~Video() {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}