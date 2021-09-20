using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH.Components.Simulation
{
    public class TimeMachineComponent : GH_Component
    {

        public TimeMachineComponent() : base("Time Machine", "TimeMachine", "Description", "TimeMachine", "Simulation") { }
        protected override System.Drawing.Bitmap Icon { get { return null; } }
        public override Guid ComponentGuid { get { return new Guid("988da09d-65b6-40e8-8d55-7d146477b352"); } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Voxels", "Voxels", "Specify the Voxels to Simulate", GH_ParamAccess.list);
            pManager.AddGenericParameter("Conditions", "Conditions", "Add Conditions to the Simulation", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Steps", "Steps", "Simulation Steps", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Run", "Run", "Run the Simulation", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Future World", "Future World", "The TimeMachine World after all the steps", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

        }

        
    }
}