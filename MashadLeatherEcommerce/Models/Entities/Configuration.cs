using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Configuration : BaseEntity
    {
        public string Key { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage ="فقط اعداد مجاز می باشند")]
        public string Value { get; set; }
    }
}