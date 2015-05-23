using System;
using System.Collections.Generic;

using System.Text;

namespace t
{
    /// <summary>
    /// Presentation output string in base64 or hexa format
    /// </summary>
    public enum OutputDataType
    {
        Base64 = 1,
        Hex = 2
    }
    /// <summary>
    /// HashAlgorithmName
    /// </summary>
    public enum HashAlgorithmName
    {
        MD5 = 1,
        SHA1 = 2,
        SHA256 = 3,
        SHA384 = 4,
        SHA512 = 5
    }
}
