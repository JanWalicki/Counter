using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Counter.Models
{
    public class CounterModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ColorString { get; set; }

        [XmlIgnore]
        public Color Color
        {
            get => Color.FromArgb(ColorString);
            set => ColorString = value.ToHex();
        }


        public int IniValue { get; set; }


        public CounterModel()
        {
            Value = 0;
            IniValue = 0;
        }

        public CounterModel(int iniValue)
        {
            Value = iniValue;
            IniValue = iniValue;
        }

        public CounterModel(string name, Color color, int iniValue)
        {
            Name = name;
            Color = color;
            IniValue = iniValue;
            Value = iniValue;
        }


        public void Reset()
        {
            Value = IniValue;
        }
    }
}
