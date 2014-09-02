using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace jsaspnetmvclocalizeWeb.Controllers
{
    public class ResourcesController : ApiController
    {
        [Route("api/Resources/{culture}/{name}")]
        public IEnumerable<KeyValuePair<string, string>> GetAllResources(string culture, string name)
        {
            //string culture = "en-US"; string name = "MyResource";
            const string GlobalResourcesRelPath = "App_GlobalResources\\";
            const string FileExtensionSeparator = ".";
            const string ResxFileExtension = "resx";
            const string ResxKeyAttribute = "name";
            const string ResxValueElement = "value";
            const string ResxElement = "data";
            if (!(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(name)))
            {
                var filePath = HttpContext.Current.Request.PhysicalApplicationPath + GlobalResourcesRelPath + name + FileExtensionSeparator + culture + FileExtensionSeparator + ResxFileExtension;
                if (File.Exists(filePath))//you can improve the behavior here if you want to load a default language in case you don't have that language
                {
                    var rootNode = XElement.Load(filePath);
                    var tempDictionary = rootNode.Descendants(ResxElement)
                       .ToDictionary(x => x.Attribute(ResxKeyAttribute).Value, x => x.Descendants(ResxValueElement).First().Value);
                    return tempDictionary.AsEnumerable();//here you can handle caching mecanisms if you want
                }
                else
                    throw new FileNotFoundException();
            }
            else
                throw new FileNotFoundException();
        }
    }
}
