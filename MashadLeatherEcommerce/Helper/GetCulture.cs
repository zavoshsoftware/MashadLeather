using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  Helper
{
    public class GetCulture
    {
        public string CurrentLang()
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant();
            string lang = culture.ToLower();
            return lang;
        }
    }
}