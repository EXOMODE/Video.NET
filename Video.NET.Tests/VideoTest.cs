using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Video.NET.Utils;

namespace Video.NET.Tests
{
    [TestClass]
    public class VideoTest
    {
        protected const string BinaryPath = "Bin";
        protected const string TmpPath = "Temp";
        protected const string ImagesPath = "Images";
        protected const string AudioPath = "Audio";
        protected const string VideoPath = "Video";
        protected const string SubtitlesPath = "Subtitles";
        protected const string OutputPath = "Output";

        static VideoTest()
        {
            if (!Directory.Exists(BinaryPath)) Directory.CreateDirectory(BinaryPath);
            if (!Directory.Exists(TmpPath)) Directory.CreateDirectory(TmpPath);
            if (!Directory.Exists(ImagesPath)) Directory.CreateDirectory(ImagesPath);
            if (!Directory.Exists(AudioPath)) Directory.CreateDirectory(AudioPath);
            if (!Directory.Exists(VideoPath)) Directory.CreateDirectory(VideoPath);
            if (!Directory.Exists(SubtitlesPath)) Directory.CreateDirectory(SubtitlesPath);
            if (!Directory.Exists(OutputPath)) Directory.CreateDirectory(OutputPath);

            FFmpeg.Global.BinaryPath = BinaryPath;
            FFprobe.Global.BinaryPath = BinaryPath;
        }
        
        [TestMethod]
        public void TestAddImageInputs()
        {
            string input1 = Path.Combine(ImagesPath, "Test01.jpg");
            string input2 = Path.Combine(ImagesPath, "Test02.png");
            string input3 = Path.Combine(ImagesPath, "Test03.bmp");
            string input4 = Path.Combine(ImagesPath, "Test04.gif");

            using (Video video = new Video(input1, input2, input3))
            {
                video.AddInput(input4);

                Assert.IsTrue(video.Inputs.Count == 4);

                for (int i = 0; i < video.Inputs.Count; i++) File.WriteAllText(Path.Combine(TmpPath, Path.GetFileName(video.Inputs[i].Name) + ".json"), video.Inputs[i].ToJson());
            }
        }
    }
}