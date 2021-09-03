using System;

namespace Memory
{
    static class Accesser {
        static public int Hits { get; private set; }
        static public int Misses { get; private set; }
        static public float HitRate { get; private set; }
        static public float MissRate { get; private set; }

        static public string[] Instructions { get; private set; }
        
        static public void Access(Cache cache, int howMany = 10) {
            int ramSize = cache.Ram.Addresses.Length;
            int addressesSize = cache.Ram.AddressesSize;
            GenerateInstructions(ramSize, addressesSize, howMany);

            foreach (var i in Instructions) {
                foreach (Line l in cache.Lines) {
                    if (l.MatchAdress(i) == 1)
                        Hits++;
                    else if (l.MatchAdress(i) == 0) {
                        l.Update(i);
                        Misses++;
                    }
                }
            }

            CalculateRates();
        }

        static private void GenerateInstructions(int ramSize, int addressesSize, int howMany) {
            Instructions = new string[howMany];

            for (int i = 0; i < howMany; i++) {
                var random = new Random();
                Instructions[i] = Utils.FormatBinaryString(random.Next(ramSize), addressesSize);
            }
        }
        
        static private void CalculateRates() {
            HitRate = (float)Hits / (float)(Hits + Misses);
            MissRate = 1 - HitRate;
        }
    }
}