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
        public double Value;
        public double MinValue;
        public double MaxValue;
        public bool KillThreshold;

        public Property(string name, double value, double minValue, double maxValue, bool killThreshold)
        {
            this.Name = name;
            this.Value = value;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.KillThreshold = killThreshold;
        }

        public Property Clone()
        {
            return new Property(this.Name, this.Value, this.MinValue, this.MaxValue, this.KillThreshold);
        }
    }
}
