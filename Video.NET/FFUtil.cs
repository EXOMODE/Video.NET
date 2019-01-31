using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Video.NET
{
    public abstract class FFutil<T> where T : new()
    {
        public abstract string BinaryFile { get; }

        public string BinaryPath { get; set; }

        public string WorkingPath { get; set; }

        public event Action<string> ErrorDataReceived;
        public event Action<string> OutputDataReceived;
        public event Action Exited;

        public static T Global { get; protected set; }

        static FFutil()
        {
            if (Global == null) Global = new T();
        }

        protected void OnErrorDataReceived(object sender, DataReceivedEventArgs e) => ErrorDataReceived?.Invoke(e.Data);

        protected void OnOutputDataReceived(object sender, DataReceivedEventArgs e) => OutputDataReceived?.Invoke(e.Data);

        protected void OnExited(object sender, EventArgs e) => Exited?.Invoke();

        public async Task EvalAsync(string arguments, Action<string> outputDataReceivedHandler, Action<string> errorDataReceivedHandler)
        {
            if (!File.Exists(Path.Combine(BinaryPath, BinaryFile))) throw new FileNotFoundException("Исполняемый файл не найден.", BinaryFile);
            if (string.IsNullOrWhiteSpace(arguments)) throw new ArgumentNullException("arguments");

            using (Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    Arguments = arguments,
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    FileName = Path.Combine(BinaryPath ?? string.Empty, BinaryFile),
                    LoadUserProfile = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    WorkingDirectory = WorkingPath ?? string.Empty,
                },
                EnableRaisingEvents = true,
            })
            {
                process.Exited += OnExited;
                process.OutputDataReceived += OnOutputDataReceived;
                process.ErrorDataReceived += OnErrorDataReceived;

                if (process.Start())
                {
                    int i = 0;

                    while (!process.HasExited && i < 20000)
                    {
                        if (!process.WaitForExit(100)) i += 100;

                        if (outputDataReceivedHandler != null)
                        {
                            using (StreamReader sr = process.StandardOutput)
                            {
                                string data = await sr.ReadToEndAsync();

                                if (!string.IsNullOrWhiteSpace(data))
                                {
                                    outputDataReceivedHandler.Invoke(data);

                                    if (!process.HasExited) process.Kill();
                                }
                            }
                        }

                        using (StreamReader sr = process.StandardError)
                        {
                            string error = await sr.ReadToEndAsync();

                            if (!string.IsNullOrWhiteSpace(error))
                            {
                                if (errorDataReceivedHandler != null) errorDataReceivedHandler.Invoke(error);
                                if (!process.HasExited) process.Kill();
                            }
                        }
                    }
                    
                    if (i > 20000) process.Kill();
                }
            }
        }

        public Task EvalAsync(string arguments, Action<string> outputDataReceivedHandler) => EvalAsync(arguments, outputDataReceivedHandler, null);

        public Task EvalAsync(string arguments) => EvalAsync(arguments, null);

        public void Eval(string arguments, Action<string> outputDataReceivedHandler, Action<string> errorDataReceivedHandler) => EvalAsync(arguments, outputDataReceivedHandler, errorDataReceivedHandler).Wait();

        public void Eval(string arguments, Action<string> outputDataReceivedHandler) => Eval(arguments, outputDataReceivedHandler, null);

        public void Eval(string arguments) => Eval(arguments, null);
    }
}