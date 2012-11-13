using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ManyConsole;

namespace Gifenstein.Cheezburger
{
    public class OhaiCommand : ConsoleCommand
    {
        public OhaiCommand()
        {
            this.IsCommand("chz-ohai", "Checks API access to the cheezburger API");
        }
        public override int Run(string[] remainingArguments)
        {
            using(var client = new WebClient())
            {
                var result = client.DownloadString("https://api.cheezburger.com/v1/ohai");
                Console.WriteLine(result);
            }

            return 0;
        }
    }
}
