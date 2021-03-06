using RobotRuntime.Abstractions;
using RobotRuntime;
using RobotRuntime.Execution;
using RobotRuntime.Tests;

namespace RobotEditor.Resources.ScriptTemplates
{
    public class CustomCommandRunner : IRunner
    {
        private CommandRunningCallback m_Callback;

        public CustomCommandRunner()
        {
            // Constructor actually can ask for other managers if needed, like IHierarchyManager etc.
        }

        public TestData TestData { set; get; }

        public void Run(IRunnable runnable)
        {
            Logger.Log(LogType.Debug, "This is custom command runner which only logs this message");
			
            var command = runnable as Command;

            // Callbacks are necessary so hierarchy could highlight currently running command
            TestData.InvokeCallback(command.Guid);

			// Optional, depends on the commands it can run
            command.Run(TestData);
        }
    }
}
