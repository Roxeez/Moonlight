using System;
using System.Collections.Generic;

namespace NtCore.Cryptography
{
    public interface ICryptography
    {
        byte[] Encrypt(string packet, bool session = false);
        IEnumerable<string> Decrypt(byte[] packet, int size);
    }
}