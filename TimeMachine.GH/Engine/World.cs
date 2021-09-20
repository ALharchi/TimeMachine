using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;

namespace TimeMachine.GH.Engine
{
    public class World
    {

        public List<Voxel> Voxels;// { get; set; }
        public int CurrentStep;// { get; set; }


        // R-Tree Helpers
        private Voxel currentVoxel;
        private List<Voxel> neighboursFoundByRTree;

        public World(List<Voxel> voxels)
        {
            this.Voxels = voxels;
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
                // Use the R-Tree to find neighbours of the current agent
                currentVoxel = voxel;
                neighboursFoundByRTree = new List<Voxel>();
                rTree.Search(new Sphere(voxel.Position, 1515), rTreeCallback);
                //voxel.ComputeDesiredVelocity(neighboursFoundByRTree);
            }

        }

        // This function will be used by the R-Tree search function
        // It simply stores the neighbour found by the R-Tree in the results list
        void rTreeCallback(object sender, RTreeEventArgs args)
        {
   //         if (currentVoxel != Voxels[args.Id])
 //               neighboursFoundByRTree.Add(Voxels[args.Id]);
        }

        private List<int> FindAffectedVoxels()
        {
            List<int> indices = new List<int>();

            return indices;
        }


    }
}
