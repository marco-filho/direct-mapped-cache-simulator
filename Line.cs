using System;

namespace Memory
{
    class Line
    {
        public string Index {get; private set; }
        public int IndexSize { get; private set; }
        public bool Valid { get; private set; }
        public string Tag { get; private set; }
        public int TagSize { get; private set; }
        public string Data { get; private set; }
        
        public Line(int index, int cacheSize, int ramSize) {
            IndexSize = (int)Math.Log2(cacheSize);
            Index = Utils.FormatBinaryString(index, IndexSize);
            Valid = false;
            Data = "random data " + new Random().Next().ToString();
            Tag = "";
            TagSize = (int)Math.Log2(ramSize) - IndexSize;
        }
        
        public void Update(string adress) {
            Valid = true;
            Tag = adress.Substring(0, TagSize);
        }

        public int MatchAdress(string adress) {
            string index = adress.Substring(TagSize);
            string adressTag = adress.Substring(0, TagSize);

            if (Index != index) return -1;
            else if (Index == index && Tag == adressTag) return 1;
            else return 0;
        }

        public override string ToString() {
            return ("Tag: " + Tag + " | " +
                "Index: " + Index + " " +
                    "[ Valid: " + (Valid ? 1 : 0) + " | Data: " + Data + " ]");
        }
    }
}
