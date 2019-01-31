using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Video.NET.Filters;
using Video.NET.Utils;

namespace Video.NET.Tests.Filters
{
    [TestClass]
    public class FadeFilterTest
    {
        protected const string BinaryPath = "Bin";
        protected const string TmpPath = "Temp";
        protected const string ImagesPath = "Images";
        protected const string AudioPath = "Audio";
        protected const string VideoPath = "Video";
        protected const string SubtitlesPath = "Subtitles";
        protected const string OutputPath = "Output";

        static FadeFilterTest()
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
        public void TestFadeInFilter()
        {
            string input1 = Path.Combine(ImagesPath, "Test01.jpg");
            string input2 = Path.Combine(ImagesPath, "Test02.jpg");
            string input3 = Path.Combine(ImagesPath, "Test03.jpg");
            string input4 = Path.Combine(ImagesPath, "Test04.jpg");

            string output = Path.Combine(OutputPath, "01 - TestFadeInFilter.mp4");

            TimeSpan videoDuration = TimeSpan.FromSeconds(5);
            TimeSpan fadeDuration = TimeSpan.FromSeconds(.25);

            using (Video video = new Video())
            {
                video
                    .AddInput(input1, videoDuration, true)
                        .FadeIn(fadeDuration)
                    .AddInput(input2, videoDuration, true)
                        .FadeIn(fadeDuration)
                    .AddInput(input3, videoDuration, true)
                        .FadeIn(fadeDuration)
                    .AddInput(input4, videoDuration, true)
                        .FadeIn(fadeDuration)
                    .Concat()
                    .Render(output);
                
                File.WriteAllText(Path.Combine(TmpPath, "01 - TestFadeInFilter.json"), video.ToJson());
            }
        }

        [TestMethod]
        public void TestFadeOutFilter()
        {
            string input1 = Path.Combine(ImagesPath, "Test01.jpg");
            string input2 = Path.Combine(ImagesPath, "Test02.jpg");
            string input3 = Path.Combine(ImagesPath, "Test03.jpg");
            string input4 = Path.Combine(ImagesPath, "Test04.jpg");

            string output = Path.Combine(OutputPath, "02 - TestFadeOutFilter.mp4");

            TimeSpan videoDuration = TimeSpan.FromSeconds(5);
            TimeSpan fadeStart = TimeSpan.FromSeconds(4.75);
            TimeSpan fadeDuration = TimeSpan.FromSeconds(.25);
            
            using (Video video = new Video())
            {
                video
                    .AddInput(input1, videoDuration, true)
                        .FadeOut(fadeStart, fadeDuration)
                    .AddInput(input2, videoDuration, true)
                        .FadeOut(fadeStart, fadeDuration)
                    .AddInput(input3, videoDuration, true)
                        .FadeOut(fadeStart, fadeDuration)
                    .AddInput(input4, videoDuration, true)
                        .FadeOut(fadeStart, fadeDuration)
                    .Concat()
                    .Render(output);

                File.WriteAllText(Path.Combine(TmpPath, "02 - TestFadeOutFilter.json"), video.ToJson());
            }
        }

        [TestMethod]
        public void TestFadeFilter()
        {
            string input1 = Path.Combine(ImagesPath, "Test01.jpg");
            string input2 = Path.Combine(ImagesPath, "Test02.jpg");
            string input3 = Path.Combine(ImagesPath, "Test03.jpg");
            string input4 = Path.Combine(ImagesPath, "Test04.jpg");

            string output = Path.Combine(OutputPath, "03 - TestFadeFilter.mp4");

            TimeSpan videoDuration = TimeSpan.FromSeconds(5);
            TimeSpan fadeInStart = TimeSpan.FromSeconds(0);
            TimeSpan fadeInDuration = TimeSpan.FromSeconds(.25);
            TimeSpan fadeOutStart = TimeSpan.FromSeconds(4.75);
            TimeSpan fadeOutDuration = TimeSpan.FromSeconds(.25);

            using (Video video = new Video())
            {
                video
                    .AddInput(input1, videoDuration, true)
                        .FadeOut(fadeOutStart, fadeOutDuration)
                    .AddInput(input2, videoDuration, true)
                        .FadeIn(fadeInStart, fadeInDuration)
                        .FadeOut(fadeOutStart, fadeOutDuration)
                    .AddInput(input3, videoDuration, true)
                        .FadeIn(fadeInStart, fadeInDuration)
                        .FadeOut(fadeOutStart, fadeOutDuration)
                    .AddInput(input4, videoDuration, true)
                        .FadeIn(fadeInStart, fadeInDuration)
                        .FadeOut(fadeOutStart, fadeOutDuration)
                    .Concat()
                    .Render(output);

                File.WriteAllText(Path.Combine(TmpPath, "03 - TestFadeFilter.json"), video.ToJson());
            }
        }
    }
}