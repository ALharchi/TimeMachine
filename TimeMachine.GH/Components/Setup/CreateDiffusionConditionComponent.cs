using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Rhino;

namespace TimeMachine.GH
{


    public class CreateDiffusionConditionComponent : GH_Component
    {
        public CreateDiffusionConditionComponent() : base("Create Diffusion Condition", "Create Diffusion Condition", "Create a diffusion condition.", "TimeMachine", "Setup") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconCreatePlanarCondition; } }
        public override Guid ComponentGuid { get { return new Guid("41b98423-35ba-4462-a71d-9710a69a1d40"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.secondary; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Condition Name", GH_ParamAccess.item, "Default");
            pManager.AddTextParameter("Target Property", "Target Property", "Specify which property is affected by this condition.", GH_ParamAccess.item, "Default");
            pManager.AddNumberParameter("Divider", "Divider", "Spread divider", GH_ParamAccess.item, 0.9);
            pManager.AddIntegerParameter("Start", "Start", "Start at a specific iteration", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("End", "End", "End after N iterations (0 to keep forever)", GH_ParamAccess.item, -1);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Condition", "Condition", "Condition", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = "";
            string target = "";
            double divider = 0.0;
            int start = 0;
            int end = 0;

            DA.GetData("Name", ref name);
            DA.GetData("Target Property", ref target);
            DA.GetData("Divider", ref divider);
            DA.GetData("Start", ref start);
            DA.GetData("End", ref end);


            Condition diffusionCondition = new Condition(name, target, divider, start, end);

            DA.SetData(0, diffusionCondition);
        }
    }
}