using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace TM.Domain.Manager
{
    public static class WebConfigManager
    {
        private static Int16 _PageSize = Int16.Parse(WebConfigurationManager.AppSettings["DefaultPageSize"]);

        public static Int16 PageSize
        {
            get {
                return _PageSize;
            }
        }
    }
}
