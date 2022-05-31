using RobotRuntime;
using RobotRuntime.Tests;
using RobotRuntime.Utils;
using System;
using System.Threading.Tasks;

namespace RobotEditor.Resources.ScriptTemplates
{
    [Serializable]
    // [RunnerType(typeof(CustomCommandRunner))] // Can also use already implemented types: SimpleCommandRunner etc.
    // [PropertyDesignerType("CustomCommandDesigner")] // Optional, will specify how to draw command in inspector
    public class AndroidClick : Command
    {
        // This is what will appear in dropdown in inspector under Command Type. Must be unique
        public override string Name { get { return "AndroidClick"; } }
        public override bool CanBeNested { get { return true; } }

        public int Count { get; set; } = 5;

        // having an empty constructor is a must, will not work otherwise
        public AndroidClick() { }
        public AndroidClick(int count)
        {
            this.Count = count;
        }

        public override void Run(TestData TestData)
        {
            for (int i = 0; i < Count; i++)
            {
                var t1 = Task.Run(() =>
                {
                    ProcessUtility.StartFromCommandLine(@"C:\Users\Justas\Downloads\platform-tools_r29.0.2-windows\platform-tools\adb.exe", "shell input tap 500 500");
                    ProcessUtility.StartFromCommandLine(@"C:\Users\Justas\Downloads\platform-tools_r29.0.2-windows\platform-tools\adb.exe", "shell input tap 500 500");
                    ProcessUtility.StartFromCommandLine(@"C:\Users\Justas\Downloads\platform-tools_r29.0.2-windows\platform-tools\adb.exe", "shell input tap 500 500");
                });
                var t2 = Task.Run(async () =>
                {
                    await Task.Delay(5);
                    ProcessUtility.StartFromCommandLine(@"C:\Users\Justas\Downloads\platform-tools_r29.0.2-windows\platform-tools\adb.exe", "shell input tap 500 500");
                    ProcessUtility.StartFromCommandLine(@"C:\Users\Justas\Downloads\platform-tools_r29.0.2-windows\platform-tools\adb.exe", "shell input tap 500 500");
                    ProcessUtility.StartFromCommandLine(@"C:\Users\Justas\Downloads\platform-tools_r29.0.2-windows\platform-tools\adb.exe", "shell input tap 500 500");
                });
                var t3 = Task.Run(async () =>
                {
                    await Task.Delay(10);
                    ProcessUtility.StartFromCommandLine(@"C:\Users\Justas\Downloads\platform-tools_r29.0.2-windows\platform-tools\adb.exe", "shell input tap 500 500");
                    ProcessUtility.StartFromCommandLine(@"C:\Users\Justas\Downloads\platform-tools_r29.0.2-windows\platform-tools\adb.exe", "shell input tap 500 500");
                    ProcessUtility.StartFromCommandLine(@"C:\Users\Justas\Downloads\platform-tools_r29.0.2-windows\platform-tools\adb.exe", "shell input tap 500 500");
                });

                Task.WaitAll(t1, t2, t3);
                //gdsgd
            }
        }

        public override string ToString()
        {
            // This is what hierarchy will show
            return "Click on android phone: " + Count;
        }
    }
}
