using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH.Components.Simulation
{
    public class GetStatusComponent : GH_Component
    {
        public GetStatusComponent() : base("Get Status", "Get Status", "Get the voxels status through time.", "TimeMachine", "Simulation") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconGetStatus; } }
        public override Guid ComponentGuid { get { return new Guid("0f576c5a-0efb-470c-abe2-03cfaa5eaae6"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.primary; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Simulation", "Simulation", "Simulation Results", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Step", "Step", "Which step do you want to check out.", GH_ParamAccess.item, 0);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Status", "Status", "Voxels status.", GH_ParamAccess.list);

        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            World myWorld = new World();
            int step = 0;

            DA.GetData(0, ref myWorld);
            DA.GetData(1, ref step);

            List<bool> outputValues = new List<bool>();

            if (step > myWorld.CurrentStep - 1)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "This step doesn't exist in this simulation!");
                return;
            }

            foreach (Voxel v in myWorld.Voxels)
            {
                outputValues.Add(v.IsAlive[step]);
            }

            DA.SetDataList(0, outputValues);
            
            this.Message = "Current step: " + step.ToString();
        }

        
    }
}