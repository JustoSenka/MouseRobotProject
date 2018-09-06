using RobotRuntime.Execution;
using RobotRuntime;
using System;
using RobotRuntime.Tests;
using System.Reflection;
using RobotRuntime.Utils;
using System.IO;

namespace CustomNamespace
{
    [Serializable]
    [RunnerType(typeof(CustomCommandRunner))]
	//[PropertyDesignerType("CustomCommandDesigner")] // Optional
    public class CustomCommand : Command
    {
		// This is what will appear in dropdown in inspector under Command Type
        public override string Name { get { return "Custom Command"; } }
        public override bool CanBeNested { get { return true; } }

        public string PathToDll { get; set; } = "";
        public string Namespace { get; set; } = "Namespace1";
        public string ClassName { get; set; } = "Class1";
        public string MethodName { get; set; } = "Method1";

		// having an empty constructor is a must, will not work otherwise
        public CustomCommand() { } 
        public CustomCommand(int SomeInt)
        {
        }

        public override void Run(TestData TestData)
        {
        }

        public override string ToString()
        {
            return "Execute: " + Namespace + "." + ClassName + "." + MethodName;
        }
    }
}
