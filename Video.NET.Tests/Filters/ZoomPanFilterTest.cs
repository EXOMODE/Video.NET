using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Video.NET.Filters;
using Video.NET.Utils;

namespace Video.NET.Tests.Filters
{
    [TestClass]
    public class ZoomPanFilterTest
    {
        protected const string BinaryPath = "Bin";
        protected const string TmpPath = "Temp";
        protected const string ImagesPath = "Images";
        protected const string AudioPath = "Audio";
        protected const string VideoPath = "Video";
        protected const string SubtitlesPath = "Subtitles";
        protected const string OutputPath = "Output";

        static ZoomPanFilterTest()
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
        public void TestZoomPanFilter()
        {
            string input1 = Path.Combine(ImagesPath, "Test01.jpg");
            string input2 = Path.Combine(ImagesPath, "Test02.jpg");
            string input3 = Path.Combine(ImagesPath, "Test03.jpg");
            string input4 = Path.Combine(ImagesPath, "Test04.jpg");

            string output = Path.Combine(OutputPath, "04 - TestZoomPanFilter.mp4");

            TimeSpan videoDuration = TimeSpan.FromSeconds(5);

            using (Video video = new Video())
            {
                video
                    .AddInput(input1, videoDuration)
                        .ZoomPan(1.5, 1, TimeSpan.FromSeconds(5))
                    .AddInput(input2, videoDuration)
                        .ZoomPan(1, 1.5, TimeSpan.FromSeconds(5))
                    .AddInput(input3, videoDuration)
                        .ZoomPan(1.5, 1.25, TimeSpan.FromSeconds(5))
                    .AddInput(input4, videoDuration)
                        .ZoomPan(1.25, 1, TimeSpan.FromSeconds(5))
                    .Concat()
                    .Render(output);

                File.WriteAllText(Path.Combine(TmpPath, "04 - TestZoomPanFilter.json"), video.ToJson());
            }
        }

        [TestMethod]
        public void TestZoomPanFilterWithFadeFilter()
        {
            string input1 = Path.Combine(ImagesPath, "Test01.jpg");
            string input2 = Path.Combine(ImagesPath, "Test02.jpg");
            string input3 = Path.Combine(ImagesPath, "Test03.jpg");
            string input4 = Path.Combine(ImagesPath, "Test04.jpg");

            string output = Path.Combine(OutputPath, "05 - TestZoomPanFilterWithFadeFilter.mp4");

            TimeSpan videoDuration = TimeSpan.FromSeconds(5);
            TimeSpan fadeInStart = TimeSpan.FromSeconds(0);
            TimeSpan fadeInDuration = TimeSpan.FromSeconds(1);
            TimeSpan fadeOutStart = TimeSpan.FromSeconds(5);
            TimeSpan fadeOutDuration = TimeSpan.FromSeconds(1);

            using (Video video = new Video())
            {
                video
                    .AddInput(input1, videoDuration)
                        .ZoomPan(1.5, 1, TimeSpan.FromSeconds(5))
                        .FadeOut(fadeOutStart, fadeOutDuration)
                    .AddInput(input2, videoDuration)
                        .ZoomPan(1, 1.5, TimeSpan.FromSeconds(5))
                        .FadeIn(fadeInStart, fadeInDuration)
                        .FadeOut(fadeOutStart, fadeOutDuration)
                    .AddInput(input3, videoDuration)
                        .ZoomPan(1.5, 1.25, TimeSpan.FromSeconds(5))
                        .FadeIn(fadeInStart, fadeInDuration)
                        .FadeOut(fadeOutStart, fadeOutDuration)
                    .AddInput(input4, videoDuration)
                        .ZoomPan(1.25, 1, TimeSpan.FromSeconds(5))
                        .FadeIn(fadeInStart, fadeInDuration)
                        .FadeOut(fadeOutStart, fadeOutDuration)
                    .Concat()
                    .Render(output);

                File.WriteAllText(Path.Combine(TmpPath, "05 - TestZoomPanFilterWithFadeFilter.json"), video.ToJson());
            }
        }
    }
}