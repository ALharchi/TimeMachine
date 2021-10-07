using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine.GH
{
    public class Property
    {
        public string Name;
        public List<double> Values;
        public double MinValue;
        public double MaxValue;
        public bool KillThreshold;
        public double Multiplier;

        public Property()
        { }

        public Property(string name, List<double> values, double minValue, double maxValue, bool killThreshold, double multiplier)
        {
            this.Name = name;
            this.Values = new List<double>(values);
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.KillThreshold = killThreshold;
            this.Multiplier = multiplier;
        }

        public Property Clone()
        {
            return new Property(this.Name, new List<double>(this.Values), this.MinValue, this.MaxValue, this.KillThreshold, this.Multiplier);
        }

        public override string ToString()
        {
            return "Property Name: " + Name
                 + "\nMinimum Value: " + MinValue.ToString()
                 + "\nMaximum Value: " + MaxValue.ToString()
                 + "\nKillThreshold: " + KillThreshold.ToString();
        }
    }
}
