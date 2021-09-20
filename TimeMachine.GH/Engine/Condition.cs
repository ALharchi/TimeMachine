using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace TimeMachine.GH
{
    public class Condition
    {
        public string Name;
        public ConditionType Type;
        public string TargetProperty;
        public double Effect;
        public int SpreadRadius;
        public double SpreadDivider;
        public int StartsAfter; 
        public int EndsAfter;

        public Condition(string name, ConditionType type, string targetProperty, double effect, int spreadRadius, double spreadDivider, int startsAfter, int endsAfter)
        {
            this.Name = name;
            this.Type = type;
            this.TargetProperty = targetProperty;
            this.Effect = effect;
            this.SpreadRadius = spreadRadius;
            this.SpreadDivider = spreadDivider;
            this.StartsAfter = startsAfter;
            this.EndsAfter = endsAfter;
        }

    }

    public enum ConditionType
    { 
        Omni,
        Planar
    }

}
