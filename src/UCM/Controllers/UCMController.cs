using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.WebApi;

namespace UCM.Controllers
{
    public class UCMController : UmbracoAuthorizedApiController
    {
        public object Index()
        {
            return new { };
        }
    }
}