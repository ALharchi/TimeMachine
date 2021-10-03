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
        public double SpreadDivider;
        public int StartsAfter;
        public int EndsAfter;

        public Condition(string name, ConditionType type, string targetProperty, double effect, double spreadDivider, int startsAfter, int endsAfter)
        {
            this.Name = name;
            this.Type = type;
            this.TargetProperty = targetProperty;
            this.Effect = effect;
            this.SpreadDivider = spreadDivider;
            this.StartsAfter = startsAfter;
            this.EndsAfter = endsAfter;
        }



        public override string ToString()
        {
            return "Condition Name: " + Name
                + "\nType: " + Type.ToString()
                + "\nTarget Property: " + TargetProperty 
                + "\nEffect: " + Effect.ToString()
                + "\nStarts After: " + StartsAfter.ToString()
                + "\nEnds After: " + EndsAfter.ToString()
                ;
        }

    }

    public enum ConditionType
    {
        Point,
        Omni,
        Planar
    }


}
