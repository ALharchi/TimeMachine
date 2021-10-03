using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Rhino;

namespace TimeMachine.GH
{
    public class Voxel
    {
        public Point3d Position;
        public List<Property> Properties;
        public List<bool> IsAlive { get; set; }
        public int LifeSpan { get; set; }
        public int Age { get; set; }

        public Voxel(Point3d position, List<Property> properties, int LifeSpan)
        {
            this.Position = position;
            this.Properties = properties;
            this.LifeSpan = LifeSpan;
            this.IsAlive = new List<bool>();
            this.IsAlive.Add(true);
            this.Age = 0;
        }

        public void Update(List<Voxel> neighbors, List<Condition> conditions, double currentStep)
        {
            // Adding Initial Value
            // and checking life status (updating life)
            foreach (Property p in this.Properties)
            {
                p.Values.Add(p.Values.Last());
                

                // FIX THIS PLZZ
                if (p.KillThreshold && IsAlive.Last())
                {
                    if (p.Values.Last() < p.MinValue || p.Values.Last() > p.MaxValue)
                    {
                        this.IsAlive.Add(false);
                        //return;
                    }
                    else
                    {
                        this.IsAlive.Add(true);
                    }
                }
                else
                {
                    this.IsAlive.Add(true);
                }
                
            }

            // We go over the conditions to update the properties
            foreach (Condition condition in conditions)
            {
                // Get the affected property
                Property targetProp = FindTargetPropertyByName(condition.TargetProperty);

                // If there is no affected property we exit to the next condition
                if (targetProp == null)
                    continue;

                if (condition.Type == ConditionType.Omni)
                {
                    targetProp.Values[targetProp.Values.Count - 1] = targetProp.Values.Last() + condition.Effect;
                    //targetProp.Values.Add(targetProp.Values.Last() + condition.Effect);
                }


                // Not sure if I'll implement Planar?
                if (condition.Type == ConditionType.Planar)
                {

                }

                if (condition.Type == ConditionType.Point)
                {
                    /*
                    List<double> distanceToNeighbors = new List<double>();
                    List<double> effectReduced = new List<double>();
                    */


                    foreach (Voxel v in neighbors)
                    {
                        double distanceFromMe = this.Position.DistanceTo(v.Position);
                        RhinoApp.WriteLine(distanceFromMe.ToString());
                    }

                    RhinoApp.WriteLine("==");



                }
            }
        }


        public Voxel Clone()
        {
            List<Property> clonedProperties = new List<Property>();

            foreach (Property pr in this.Properties)
            {
                Property clonedProperty = pr.Clone();
                clonedProperties.Add(clonedProperty);
            }

            return new Voxel(new Point3d(this.Position), clonedProperties, this.LifeSpan);
        }


        public override string ToString()
        {
            return "Voxel Position: " + Position.ToString()
                + "\nProperties: " + this.Properties.Count.ToString();
        }


        public Property FindTargetPropertyByName(string name)
        {
            foreach (Property p in this.Properties)
            {
                if (p.Name == name)
                    return p;
            }
            return null;
        }

    }
}