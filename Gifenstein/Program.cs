using System;

namespace Gifenstein
{
    class Program
    {
        static int Main(string[] args)
        {
            var commands = ManyConsole.ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof (Program));
            return ManyConsole.ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
    }
}
