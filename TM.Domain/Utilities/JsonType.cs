using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TM.Domain.Models.Json;
using Newtonsoft.Json;

namespace TM.Domain.Utilities
{
    public static class JsonType
    {
        public static List<CommonType> GetSlotAuthTypes()
        {
            string path = HttpContext.Current.Server.MapPath("~").Replace("TM.Web","TM.Domain");
            string filePath = path + "JsonTypes\\SlotAuthType.json";
            List<CommonType> commonTypes = new List<CommonType>();

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                commonTypes = JsonConvert.DeserializeObject<List<CommonType>>(json);
            }

            return commonTypes;
        }
    }
}
