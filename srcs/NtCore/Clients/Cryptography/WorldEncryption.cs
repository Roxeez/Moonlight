using System;
using System.Collections.Generic;
using System.Linq;

namespace NtCore.Clients.Cryptography
{
    public sealed class WorldEncryption : ICryptography
    {
        private static readonly char[] Keys = {' ', '-', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'n'};

        private int _key { get; }
        
        public WorldEncryption(int key)
        {
            _key = key;
        }

        /// <summary>
        ///     Decrypt the raw packet (byte array) to a readable list string
        /// </summary>
        /// <param name="bytes">Bytes to decrypt</param>
        /// <param name="size">Amount of byte to read</param>
        /// <returns>Decrypted packet to string list</returns>
        public IEnumerable<string> Decrypt(byte[] bytes, int size)
        {
            var output = new List<string>();

            string currentPacket = "";
            int index = 0;

            while (index < size)
            {
                byte currentByte = bytes[index];
                index++;

                if (currentByte == 0xFF)
                {
                    output.Add(currentPacket);
                    currentPacket = "";
                    continue;
                }

                byte length = (byte) (currentByte & 0x7F);

                if ((currentByte & 0x80) != 0)
                {
                    while (length != 0)
                    {
                        if (index <= size)
                        {
                            currentByte = bytes[index];
                            index++;

                            byte firstIndex = (byte)(((currentByte & 0xF0u) >> 4) - 1);
                            byte first = (byte)(firstIndex != 255 ? firstIndex != 14 ? Keys[firstIndex] : '\u0000' : '?');
                            if (first != 0x6E)
                                currentPacket += Convert.ToChar(first);

                            if (length <= 1)
                                break;
                        
                            byte secondIndex = (byte)((currentByte & 0xF) - 1);
                            byte second = (byte)(secondIndex != 255 ? secondIndex != 14 ? Keys[secondIndex] : '\u0000' : '?');
                            if (second != 0x6E)
                                currentPacket += Convert.ToChar(second);

                            length -= 2;
                        }
                        else
                        {
                            length--;
                        }
                    }
                }
                else
                {
                    while (length != 0)
                    {
                        if (index < size)
                        {
                            currentPacket += Convert.ToChar(bytes[index] ^ 0xFF);
                            index++;
                        }
                        else if (index == size)
                        {
                            currentPacket += Convert.ToChar(0xFF);
                            index++;
                        }

                        length--;
                    }
                }
            }
            return output;
        }

        /// <summary>
        ///     Encrypt the string packet to byte array
        /// </summary>
        /// <param name="value">String to encrypt</param>
        /// <param name="session">Define if it's a session packet or not</param>
        /// <returns>Encrypted packet as byte array</returns>
        public byte[] Encrypt(string value, bool session = false)
        {
            var output = new List<byte>();

            string mask = new string(value.Select(c =>
            {
                sbyte b = (sbyte) c;
                if (c == '#' || c == '/' || c == '%')
                    return '0';
                if ((b -= 0x20) == 0 || (b += unchecked((sbyte) 0xF1)) < 0 || (b -= 0xB) < 0 ||
                    b - unchecked((sbyte) 0xC5) == 0)
                    return '1';
                return '0';
            }).ToArray());

            int packetLength = value.Length;

            int sequenceCounter = 0;
            int currentPosition = 0;

            while (currentPosition <= packetLength)
            {
                int lastPosition = currentPosition;
                while (currentPosition < packetLength && mask[currentPosition] == '0')
                    currentPosition++;

                int sequences;
                int length;

                if (currentPosition != 0)
                {
                    length = currentPosition - lastPosition;
                    sequences = length / 0x7E;
                    for (int i = 0; i < length; i++, lastPosition++)
                    {
                        if (i == sequenceCounter * 0x7E)
                        {
                            if (sequences == 0)
                            {
                                output.Add((byte) (length - i));
                            }
                            else
                            {
                                output.Add(0x7E);
                                sequences--;
                                sequenceCounter++;
                            }
                        }

                        output.Add((byte) ((byte) value[lastPosition] ^ 0xFF));
                    }
                }

                if (currentPosition >= packetLength)
                    break;

                lastPosition = currentPosition;
                while (currentPosition < packetLength && mask[currentPosition] == '1')
                    currentPosition++;

                if (currentPosition == 0) continue;

                length = currentPosition - lastPosition;
                sequences = length / 0x7E;
                for (int i = 0; i < length; i++, lastPosition++)
                {
                    if (i == sequenceCounter * 0x7E)
                    {
                        if (sequences == 0)
                        {
                            output.Add((byte) ((length - i) | 0x80));
                        }
                        else
                        {
                            output.Add(0x7E | 0x80);
                            sequences--;
                            sequenceCounter++;
                        }
                    }

                    byte currentByte = (byte) value[lastPosition];
                    switch (currentByte)
                    {
                        case 0x20:
                            currentByte = 1;
                            break;
                        case 0x2D:
                            currentByte = 2;
                            break;
                        case 0xFF:
                            currentByte = 0xE;
                            break;
                        default:
                            currentByte -= 0x2C;
                            break;
                    }

                    if (currentByte == 0x00) continue;

                    if (i % 2 == 0)
                        output.Add((byte) (currentByte << 4));
                    else
                        output[output.Count - 1] = (byte) (output.Last() | currentByte);
                }
            }

            output.Add(0xFF);
            
            sbyte sessionNumber = (sbyte) ((_key >> 6) & 0xFF & 0x80000003);

            if (sessionNumber < 0)
                sessionNumber = (sbyte) (((sessionNumber - 1) | 0xFFFFFFFC) + 1);

            byte sessionKey = (byte) (_key & 0xFF);

            if (session)
                sessionNumber = -1;

            switch (sessionNumber)
            {
                case 0:
                    for (int i = 0; i < output.Count; i++)
                        output[i] = (byte) (output[i] + sessionKey + 0x40);
                    break;
                case 1:
                    for (int i = 0; i < output.Count; i++)
                        output[i] = (byte) (output[i] - (sessionKey + 0x40));
                    break;
                case 2:
                    for (int i = 0; i < output.Count; i++)
                        output[i] = (byte) ((output[i] ^ 0xC3) + sessionKey + 0x40);
                    break;
                case 3:
                    for (int i = 0; i < output.Count; i++)
                        output[i] = (byte) ((output[i] ^ 0xC3) - (sessionKey + 0x40));
                    break;
                default:
                    for (int i = 0; i < output.Count; i++)
                        output[i] = (byte) (output[i] + 0x0F);
                    break;
            }
            
            return output.ToArray();
        }
       
    }
}