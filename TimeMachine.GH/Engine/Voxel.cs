using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace TimeMachine.GH
{
    public class Voxel
    {
        public Point3d Position;
        public List<Property> Properties;

        public int LifeSpan { get; set; }
        public bool IsAlive { get; set; }
        public int Age { get; set; }

        public Voxel(Point3d position, List<Property> properties, int LifeSpan)
        {
            this.Position = position;
            this.Properties = properties;
            this.LifeSpan = LifeSpan;
            this.IsAlive = true;
            this.Age = 0;
        }


        public void Update(List<Voxel> neighbors, List<Condition> conditions)
        {
            // If voxel is dead, we don't do anything
            if (!IsAlive)
            {
                return;
            }

            // We kill the voxel if any property value is outside the threshold and killing is activated
            foreach (Property p in Properties)
            {
                if (p.KillThreshold)
                {
                    if (p.Values.Last() < p.MinValue || p.Values.Last() > p.MaxValue)
                    {
                        this.IsAlive = false;
                        return;
                    }
                }
            }


        }

        /*
        public void Update()
        {


            foreach (Property pr in this.Properties)
            { 
            
            }

        }
        */
        
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
    }
}