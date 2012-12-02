using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Frontpage.Controllers
{
    public class RowDTO
    {
    }

    public class AllRightAnnouncement : RowDTO
    {
        public string announcement { get; set; }
    }

    public class AllRightAnimationFrame : RowDTO
    {
        public string customUrl { get; set; }
    }

    public class AllRightMinor : AllRightAnimationFrame
    {
    }

    public class AllRightMajor : AllRightMinor
    {
    }

    public class RowList
    {
        public RowDTO[] elements { get; set; }
    }

    public class BuildController : Controller
    {
        //
        // GET: /Build/

        [HttpPost]
        public ActionResult Index(RowList value)
        {
            string request;

            Request.InputStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(Request.InputStream))
            {
                request = reader.ReadToEnd();
            }

            var serializer = JsonSerializer.Create(MvcApplication.GetJsonSerializationSettings());
            
            serializer.TypeNameHandling = TypeNameHandling.All;

            var xnodeRequest = serializer.Deserialize<RowList>(new JsonTextReader(new StringReader(request)));

            return View();
        }

    }
}
