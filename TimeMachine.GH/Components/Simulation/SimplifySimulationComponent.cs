using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH.Components.Simulation
{
    public class SimplifySimulationComponent : GH_Component
    {
        public SimplifySimulationComponent() : base("Simplify Simulation", "Simplify Simulation", "Simplify the simulation results for visualization purposes", "TimeMachine", "Simulation") {}
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconSimplifySimulation; } }
        public override Guid ComponentGuid { get { return new Guid("e2c17a0c-d737-4b7b-9605-caaeaa4ca606"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.secondary; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Simulation", "Simulation", "Simulation results to simply.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Factor", "Factor", "Simplification Factor. Should be a number between 0 and 1", GH_ParamAccess.item, 0.5);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Simulation", "Simulation", "The simplified simulation results", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            World myWorld = new World();
            double factor = 0.5;

            DA.GetData(0, ref myWorld);
            DA.GetData(1, ref factor);


            if (factor > 1 || factor < 0)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Factor should be between 0 and 1!");
                return;
            }
        }

    }
}