using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine.GH
{
    public struct Voxel
    {
        public List<Property> Properties;
        public bool IsAlive { get; set; }

        public Voxel(List<Property> properties)
        {
            this.Properties = properties;
            this.IsAlive = true;
        }

        public void Kill()
        {
            this.IsAlive = false;
        }
    }
}
