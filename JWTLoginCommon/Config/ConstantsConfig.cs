using System;
using System.Collections.Generic;
using System.Text;

namespace JWTLoginCommon.Config
{
    public class ConstantsConfig
    {
        public const string Constant = "Constant";
        public string Issuer { get; set; }
        public string Audiance { get; set; }
        public string Secret { get; set; }
    }
}
