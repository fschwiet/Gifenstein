using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ManyConsole;

namespace Gifenstein.Cheezburger
{
    public class OhaiCommand : ConsoleCommand
    {
        public string client_id;
        public string client_secret;

        public OhaiCommand()
        {
            this.IsCommand("chz-ohai", "Checks API access to the cheezburger API");
            this.HasRequiredOption("chzid=", "client_id used to authenticate with Cheezburger service.", v => client_id = v);
            this.HasRequiredOption("chzsecret=", "client_secret used to authenticate with Cheezburger service.", v => client_secret = v);
        }

        public override int Run(string[] remainingArguments)
        {
            using(var client = new WebClient())
            {
                var postParameters = new NameValueCollection();
                postParameters.Add("client_id", client_id);
                postParameters.Add("client_secret", client_secret);
                postParameters.Add("grant_type", "client_credentials");
                var authResult = client.UploadValues("https://api.cheezburger.com/oauth/access_token", "POST",
                                                     postParameters);

                var authResultText = new StreamReader(new MemoryStream(authResult), Encoding.UTF8).ReadToEnd();

                var deserializedAuthResult = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(authResultText, new
                    {
                        access_token = "string",
                        expires_in = 123,
                        refresh_token = "string"
                    });

                var result = client.DownloadString("https://api.cheezburger.com/v1/ohai?access_token=" + deserializedAuthResult.access_token);
                Console.WriteLine(result);
            }

            return 0;
        }
    }
}
