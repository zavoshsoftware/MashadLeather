using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MashadLeatherEcommerce.KiyanService;
namespace Helper
{
    public class KiyanHelper
    {
        public ValidationSoapHeader ConnectToService()
        {
            ValidationSoapHeader header = new ValidationSoapHeader();

            header.TokenAUT = "Charm@#$568";

            return header;
        }
    }
}