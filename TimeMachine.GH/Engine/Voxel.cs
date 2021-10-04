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

        public void Update(List<Voxel> allVoxels, List<Condition> conditions, double currentStep)
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

                    double distanceToCondition = this.Position.DistanceTo(condition.Source);

                    if (distanceToCondition < condition.Extent)
                    {
                        double actualEffect = condition.Effect * condition.SpreadDivider;
                        targetProp.Values[targetProp.Values.Count - 1] = targetProp.Values.Last() + actualEffect;
                    }

                    /*
                    
                    List<double> effectReduced = new List<double>();
                    */

                    //List<Voxel> affectedVoxels = new List<Voxel>();
                    //List<double> allDistances = new List<double>();

                    //List<Tuple<Voxel, double>> affectedVoxels = new List<Tuple<Voxel, double>>();

                    /*
                    foreach (Voxel v in allVoxels)
                    {
                        double dist = condition.Source.DistanceTo(v.Position);
                        if (dist < condition.Extent)
                        {
                            affectedVoxels.Add(Tuple.Create(v, dist));
                            //allDistances.Add(condition.Source.DistanceTo(v.Position));
                            //affectedVoxels.Add(v);
                        }
                    }
                    //allDistances.Sort();
                    //allVoxels.Sort()

                    //List<Tuple<Voxel, double>> affectedVoxelsSorted =  affectedVoxels.Sort

                    affectedVoxels.Sort((a, b) => a.Item2.CompareTo(b.Item2));


                    List<Voxel> cleanAffectedVoxels = new List<Voxel>();
                    foreach (var i in affectedVoxels)
                    {
                        cleanAffectedVoxels.Add(i.Item1);
                    }



                    int counter = 0;
                    foreach (Voxel v in cleanAffectedVoxels)
                    {

                        double actualEffect = ((counter * 100) / cleanAffectedVoxels.Count) * condition.SpreadDivider * condition.Effect ;
                        targetProp.Values[targetProp.Values.Count - 1] = targetProp.Values.Last() + actualEffect;

                        /*
                        double distanceFromCondition = condition.Source.DistanceTo(v.Position);

                        if (distanceFromCondition > condition.Extent)
                        {
                            //double val = distanceFromCondition  * 

                            //double actualEffect =  * condition.SpreadDivider;
                            //targetProp.Values[targetProp.Values.Count - 1] = targetProp.Values.Last() + actualEffect;
                        }
                        //RhinoApp.WriteLine(distanceFromMe.ToString());

                        counter++;
                    }
                        */

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