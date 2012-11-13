using System;

namespace Gifenstein
{
    class Program
    {
        static int Main(string[] args)
        {
            var commands = ManyConsole.ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof (Program));
            try
            {
                return ManyConsole.ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown:");
                Console.WriteLine(e.ToString());

                return -1;
            }
        }
    }
}
