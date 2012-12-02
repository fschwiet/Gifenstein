using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Frontpage.Controllers;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Frontpage.Test
{
    [TestFixture]
    public class PolymorphicSerialization
    {
        [Test]
        public void CanSerializeRowObjects()
        {
            var sample = new RowList()
                {
                    elements = new RowDTO[]
                        {
                            new AllRightAnnouncement() {announcement = "alright"}
                        }
                };

            var settings = Frontpage.MvcApplication.GetJsonSerializationSettings();

            var serializer = JsonSerializer.Create(settings);

            var sb = new StringBuilder();
            var reader = new StringWriter(sb);

            serializer.Serialize(reader, sample);

            Console.WriteLine("Serialized: " + sb.ToString());

            var result = serializer.Deserialize<RowList>(new JsonTextReader(new StringReader(sb.ToString())));

            Assert.That(result.elements.Length, Is.EqualTo(1));

            var element = result.elements.Cast<AllRightAnnouncement>().Single();

            Assert.That(element.announcement, Is.EqualTo("alright"));
        }
   }
}
