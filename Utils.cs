using System;

namespace Memory
{
    class Utils
    {
        static public string FormatBinaryString(int toBinary, int size)
        {
            string binary = Convert.ToString(toBinary, 2);
            if (binary.Length < size)
                for (int j = binary.Length; j < size; j++)
                {
                    binary = "0" + binary;
                }
            return binary;
        }
    }
}
