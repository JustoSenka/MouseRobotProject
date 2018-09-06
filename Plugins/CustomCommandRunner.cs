using RobotRuntime;
using RobotRuntime.Abstractions;
using RobotRuntime.Commands;
using RobotRuntime.Execution;
using RobotRuntime.Tests;
using RobotRuntime.Utils;
using RobotRuntime.Utils.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CustomNamespace
{
    public class CustomCommandRunner : IRunner
    {
        private IFeatureDetectorFactory FeatureDetectorFactory;
        private IScreenStateThread ScreenStateThread;
        public CustomCommandRunner(IFeatureDetectorFactory FeatureDetectorFactory, IScreenStateThread ScreenStateThread)
        {
            // Constructor actually can ask for other managers if needed, like IAssetDatabase etc.
            this.FeatureDetectorFactory = FeatureDetectorFactory;
            this.ScreenStateThread = ScreenStateThread;
        }

        public TestData TestData { set; get; }

        public void Run(IRunnable runnable)
        {
            var command = runnable as CustomCommand;
            var node = TestData.TestFixture.Commands.FirstOrDefault(n => n.value == command);

            //Assembly.LoadFrom(Path.Combine(Paths.PluginPath, "liblept172.dll"));
            //Assembly.LoadFrom(Path.Combine(Paths.PluginPath, "libtesseract304.dll"));
            //Assembly.LoadFrom(Path.Combine(Paths.PluginPath, "Tesseract.dll"));
           /* Assembly.LoadFrom(Path.Combine(Paths.PluginPath, "TextRecogniser.dll"));

            Assembly asm = Assembly.LoadFrom(Path.Combine(Paths.PluginPath, command.PathToDll));
            Type t = asm.GetType(command.Namespace + "." + command.ClassName);
            MethodInfo m = t.GetMethod(command.MethodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            */
            var rand = new Random();

            int i = 1;
            while (true)
            {
                TestData.CommandRunningCallback?.Invoke(i++ % 2 == 0 ? node.value : null);

                // Clone image from screenshot
                var screen = new Bitmap(ScreenStateThread.Width, ScreenStateThread.Height, PixelFormat.Format32bppArgb);
                lock (ScreenStateThread.ScreenBmpLock)
                {
                    BitmapUtility.Clone32BPPBitmap(ScreenStateThread.ScreenBmp, screen);
                }
                //var points = (Point[])m.Invoke(null, new object[] { screen });

                Bitmap resized = new Bitmap(screen, new Size(screen.Width * 2, screen.Height * 2));

                var points = RecognizeText.GetListOfPoints(resized); 

                foreach (var c in GenerateRandomOperation(rand, points))
                {
                    if (TestData.ShouldCancelRun)
                        return;

                    c.Run(TestData);
                }
            }
        }

        private Command[] GenerateRandomOperation(Random rand, Point[] points)
        {
            var countOfOperations = 2;
            var i = rand.Next() % countOfOperations;

            var pointIndex = rand.Next() % points.Length;
            var p = points[pointIndex];

            switch (i)
            {
                case 0:
                    return new[] { new CommandPress(p.X, p.Y, false, MouseButton.Left) };

                case 1:
                    return new[] { new CommandMove(p.X, p.Y) };

                default:
                    return null;
            }
        }

        private static void OverrideCommandPropertiesIfExist(Command command, object value, string prop)
        {
            var destProp = command.GetType().GetProperty(prop);

            if (destProp != null)
                destProp.SetValue(command, value);
        }
    }
}
