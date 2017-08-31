using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.WebApi;

namespace UCM.Controllers
{
    /*
     function makeRequest(action, data, callback) {
        var url = '/Umbraco/backoffice/Api/UCM/' + action;
        $.post({
            url: url,
            dataType: 'json',
            data: data
        })
		.fail(function (e) {
		    alert('request failed for url: ' + url);
		    console.log(e);
		})
		.done(callback);
     }
    */

    public class UCMController : UmbracoAuthorizedApiController
    {
        [Serializable]
        public class ContentType
        {
            public string Name;
            public string[] Fields;

            public ContentType(IContentType type)
            {
                Name = type.Name;
                Fields = (from t in type.PropertyTypes
                          select t.Alias).ToArray();
            }
        }

        [Serializable]
        public class ContentModel
        {
            public string Name;
            public int Id;
            public Guid Guid;
            public ContentType Type;
            public ContentModel[] Children;
            public ContentModel(IContent content)
            {
                Name = content.Name;
                Id = content.Id;
                Guid = content.Key;
                Type = new ContentType(content.ContentType);
                
                //EU SEI QUE ESSA RECURSÃO AKI VAI ESTOURAR A MEMORIA!
                //MAS EH LINQ, E FICOU LINDO, E FODA-C
                Children = (from c in content.Children()
                            select (new ContentModel(c))).ToArray();
            }
        }

        private IContentService _cs { get { return ApplicationContext.Services.ContentService; } }

        [HttpPost]
        public object Index()
        {
            return new { };
        }


        [System.Web.Http.HttpPost]
        public object GetContentTree()
        {
            var rootElements = _cs.GetRootContent();
            return (from e in _cs.GetRootContent()
                    select (new ContentModel(e))).ToArray();
        }

        [System.Web.Http.HttpPost]
        public object ExportAll()
        {
            return new { StatusCode = "Completed"};
        }
    }
}