using System;

namespace Memory
{
    class Ram {
        public string[] Addresses { get; private set; }
        public int AddressesSize { get; private set; }
        
        public Ram(int size) {
            Addresses = new string[size];
            AddressesSize = (int)MathF.Log2(Addresses.Length);
            Fill();
        }

        private void Fill() {
            for (int i = 0; i < Addresses.Length; i++) {
                Addresses[i] = Utils.FormatBinaryString(i, AddressesSize);
            }
        }
    }
}