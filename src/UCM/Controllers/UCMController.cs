using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Editors;

namespace UCM.Controllers
{
    public class UCMController : UmbracoAuthorizedJsonController
    {
        public object Index()
        {
            return new { mensagem = "qualquer merda" };
        }
    }
}