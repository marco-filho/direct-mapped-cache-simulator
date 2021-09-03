using System;

namespace Memory
{
    class Cache
    {
        public Line[] Lines { get; private set; }
        private int Size { get; set; }
        private int IndexSize { get; set; }
        public Ram Ram { get; private set; }

        public Cache(Ram ram, int size) {
            Ram = ram;
            Size = size;
            IndexSize = (int)Math.Log2(Size);
            int ramSize = ram.Addresses.Length;
            Lines = new Line[size];
            
            for (int i = 0; i < size; i++)
                Lines[i] = new Line(i, size, ramSize);

            Fill(ram);
        }

        private void Fill(Ram ram) {
            var random = new Random();

            for (int i = 0; i < Lines.Length; i++) {
                bool found = false;
                int n = random.Next(ram.Addresses.Length);
                string address = ram.Addresses[n];

                foreach (var l in Lines) {
                    if (address.Substring((int)Math.Log2(IndexSize)) == l.Index && l.Valid) {
                        found = true;
                        break;
                    }
                }
                if (found) {
                    i--;
                    continue;
                }
                
                Lines[i].Update(address);
            }
        }
    }
}
