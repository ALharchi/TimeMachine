using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH.Components.Simulation
{
    public class ViewerComponent : GH_Component
    {
        public ViewerComponent() : base("Viewer", "Viewer", "Description","TimeMachine", "Simulation") { }
        protected override System.Drawing.Bitmap Icon { get { return null; } }
        public override Guid ComponentGuid { get { return new Guid("41654c22-03b8-4809-bac0-7aebbaebd799"); } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("World", "World", "A TimeMachine World", GH_ParamAccess.item);
            pManager.AddColourParameter("Legend", "Legend", "Colors as a Tree", GH_ParamAccess.tree);
            pManager.AddNumberParameter("Time", "Time", "Timestep of the simulation", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
        }
        
    }
}