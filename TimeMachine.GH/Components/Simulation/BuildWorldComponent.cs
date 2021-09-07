using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH.Components.Simulation
{
    public class BuildWorldComponent : GH_Component
    {
        public BuildWorldComponent() : base("Build World", "Build World", "Create a World and Populate all the voxels", "TimeMachine", "Simulation") { }
        protected override System.Drawing.Bitmap Icon { get { return null; } }
        public override Guid ComponentGuid { get { return new Guid("a767a294-8261-4c4d-b7f8-21f3d28ade84"); } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
        }

        
    }
}