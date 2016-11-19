using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace YjjUtilities
{
    public class StringHelperProxySoapHeader : SoapHeader
    {
        public string SecretKey { get; set; }

        public bool IsValid
        {
            get { return SecretKey.Equals("4965E71976130A3A469123DE8F97C7FD"); }
        }
    }
}