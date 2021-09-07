using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine.GH.Engine
{
    public struct World
    {

        List<Voxel> Voxels { get; set; }
        int step { get; set; }

        public World(List<Voxel> voxels)
        {
            this.Voxels = voxels;
            this.step = 0;
        }

    }
}
