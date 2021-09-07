using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace TimeMachine.GH
{
    public struct Voxel
    {
        public Point3d Position;
        public List<Property> Properties;
        public List<double> Values;
        public bool IsAlive { get; set; }
        public int Age { get; set; }

        public Voxel(Point3d position, List<Property> properties)
        {
            this.Position = position;
            this.Properties = properties;
            this.Values = new List<double>();
            this.IsAlive = true;
            this.Age = 0;
        }

        public void Kill()
        {
            this.IsAlive = false;
        }
    }
}