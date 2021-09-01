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
        public double MinValue;
        public double MaxValue;
        
        public Property(string name, double minValue, double maxValue)
        {
            this.Name = name;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
    }
}
