using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;

namespace TimeMachine.GH
{
    public class World
    {
        public List<Voxel> Voxels;
        public List<Condition> Conditions;
        public int CurrentStep; 

        public double FieldOfView;

        // R-Tree Helpers
        private Voxel currentVoxel;
        private List<Voxel> neighborsFoundByRTree;

        public World()
        { }

        public World(List<Voxel> voxels, List<Condition> conditions)
        {
            this.Voxels = voxels;
            this.Conditions = conditions;
            this.CurrentStep = 0;
        }

        public void Update()
        {
            // Build the R-Tree
            RTree rTree = new RTree();
            for (int i = 0; i < Voxels.Count; i++)
                rTree.Insert(Voxels[i].Position, i);

            foreach (Voxel voxel in Voxels)
            {
                // Use the R-Tree to find neighbors of the current agent
                currentVoxel = voxel;
                neighborsFoundByRTree = new List<Voxel>();
                rTree.Search(new Sphere(voxel.Position, FieldOfView), rTreeCallback);
                voxel.Update(neighborsFoundByRTree, Conditions);
            }
        }

        void rTreeCallback(object sender, RTreeEventArgs args)
        {
            if (currentVoxel != Voxels[args.Id])
                neighborsFoundByRTree.Add(Voxels[args.Id]);
        }

    }
}
