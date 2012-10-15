using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ManyConsole;

namespace Gifenstein
{
    public class BuildDronePandemic : ConsoleCommand
    {
        public BuildDronePandemic()
        {
            this.IsCommand("build-dronepandemic", "Draws an image meme.");
        }
        public override int Run(string[] remainingArguments)
        {
            throw new NotImplementedException();
        }
    }
}
