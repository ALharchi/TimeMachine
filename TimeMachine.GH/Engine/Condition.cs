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
        public Point3d Source;
        public double Effect;
        public double Extent;
        public int Divisions;
        public double SpreadDivider;
        public int StartsAfter;
        public int EndsAfter;


        /// <summary>
        /// Create a Point type condition
        /// </summary>
        public Condition(string name, string targetProperty, Point3d source, double effect, double extent, int divisions, double spreadDivider, int startsAfter, int endsAfter)
        {
            this.Name = name;
            this.TargetProperty = targetProperty;
            this.Source = source;
            this.Effect = effect;
            this.Extent = extent;
            this.Divisions = divisions;
            this.SpreadDivider = spreadDivider;
            this.StartsAfter = startsAfter;
            this.EndsAfter = endsAfter;

            this.Type = ConditionType.Point;
        }

        /// <summary>
        /// Create an OMNI type condition
        /// </summary>
        public Condition(string name, string targetProperty, int startsAfter, int endsAfter)
        {
            this.Name = name;
            this.TargetProperty = targetProperty;
            this.StartsAfter = startsAfter;
            this.EndsAfter = endsAfter;

            this.Type = ConditionType.Omni;
        }

        public double GetReducedEffect(Voxel v)
        {
            double distanceToVoxel = this.Source.DistanceTo(v.Position);

            if (distanceToVoxel > this.Extent)
            {
                return 0.0;
            }
            else
            {
                double reducedEffect = Math.Pow(SpreadDivider, (distanceToVoxel / Math.Floor(Extent / Divisions))) * this.Effect;
                return reducedEffect;
            }
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
        Planar,
        Environment
    }


}
