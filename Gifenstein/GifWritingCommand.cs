using ManyConsole;

namespace Gifenstein
{
    public abstract class GifWritingCommand : ConsoleCommand
    {
        protected string Output;

        public GifWritingCommand()
        {
            this.HasRequiredOption("o=", "Output file (gif will be appended if not present)", v => Output = v);
        }

        public override int? OverrideAfterHandlingArgumentsBeforeRun(string[] remainingArguments)
        {
            if (!Output.EndsWith(".gif"))
                Output = Output.TrimEnd('.') + ".gif";

            return base.OverrideAfterHandlingArgumentsBeforeRun(remainingArguments);
        }
    }
}