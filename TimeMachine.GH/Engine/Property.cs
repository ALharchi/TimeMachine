using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine.GH
{
    public struct Property
    {
        public string Name;
        public double Value;
        public double MinValue;
        public double MaxValue;

        public Property(string name, double value, double minValue, double maxValue)
        {
            this.Name = name;
            this.Value = value;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Getting a deep copy of the property
        /// </summary>
        /// <returns></returns>
        public Property Clone()
        {
            return new Property(this.Name, this.Value, this.MinValue, this.MaxValue);
        }
    }
}
