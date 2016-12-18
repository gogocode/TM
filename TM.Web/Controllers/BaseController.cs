using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Domain;
using TM.Domain.Manager;
using TM.Domain.Models;
using TM.Domain.Services;

namespace TM.Web.Controllers
{
    public class BaseController : Controller
    {

        public Int16 _PageSize;


        public BaseController()
        {

            _PageSize = WebConfigManager.PageSize;
        }


    }
}
