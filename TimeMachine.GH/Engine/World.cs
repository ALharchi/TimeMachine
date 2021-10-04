using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Rhino;

namespace TimeMachine.GH
{
    public class World
    {
        public List<Voxel> Voxels;
        public List<Condition> Conditions;
        public int CurrentStep;

        public double FieldOfView = 100;

        // R-Tree Helpers
        //private Voxel currentVoxel;
        //private List<Voxel> neighborsFoundByRTree;

        public World() { }

        public World(List<Voxel> voxels, List<Condition> conditions, double neighbourhoodRadius)
        {
            this.Voxels = voxels;
            this.Conditions = conditions;
            this.FieldOfView = neighbourhoodRadius;
            this.CurrentStep = 0;
        }

        public void Update()
        {

            // Build the R-Tree
            //RTree rTree = new RTree();
            //for (int i = 0; i < Voxels.Count; i++)
            //{
            //    rTree.Insert(Voxels[i].Position, i);
            //}

            foreach (Voxel voxel in Voxels)
            {
                // Use the R-Tree to find neighbors of the current voxel
                //currentVoxel = voxel;
                //neighborsFoundByRTree = new List<Voxel>();
                //rTree.Search(new Sphere(voxel.Position, FieldOfView), rTreeCallback);


                List<Condition> applicableConditions = new List<Condition>();
                foreach (Condition cond in this.Conditions)
                {
                    // We check if we are within the accepted range of effect
                    if (CurrentStep < cond.StartsAfter)
                        continue;

                    if (CurrentStep > cond.EndsAfter && cond.EndsAfter != -1)
                        continue;

                    // If yes, we add the condition to as effective
                    applicableConditions.Add(cond);
                }

                //voxel.Update(neighborsFoundByRTree, applicableConditions, CurrentStep);
                // Update with only omni conditions
                voxel.Update(this.Voxels, applicableConditions, CurrentStep);
            }

            CurrentStep++;
        }

        /*
        void rTreeCallback(object sender, RTreeEventArgs args)
        {
            if (currentVoxel != Voxels[args.Id])
                neighborsFoundByRTree.Add(Voxels[args.Id]);
        }
        */



        public override string ToString()
        {
            return "=> Simulation Results: <=\n" +
                   "Voxels: " + this.Voxels.Count + "\n" +
                   "Conditions: " + this.Conditions.Count + "\n" +
                   "Steps: " + this.CurrentStep.ToString();
        }

    }
}
