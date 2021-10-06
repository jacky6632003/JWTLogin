using System;
using System.Collections.Generic;
using System.Text;

namespace JWTLoginCommon.Config
{
    public class AesCryptoConfig
    {
        public const string AesCrypto = "AesCrypto";

        public string AesKey { get; set; }
        public string AesIv { get; set; }
    }
}
