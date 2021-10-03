using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH
{
    public class CreateOmniConditionComponent : GH_Component
    {
        public CreateOmniConditionComponent() : base("Create Omni Condition", "Create Omni Condition", "Create an condition that affect all the voxels in the simulation.", "TimeMachine", "Setup") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconCreateOmniCondition ; } }
        public override Guid ComponentGuid { get { return new Guid("a206cda5-d484-4847-9bc1-034a414be95d"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.secondary; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Condition Name", GH_ParamAccess.item, "Default");
            pManager.AddTextParameter("Target Property", "Target Property", "Specify which property is affected by this condition.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Effect", "Effect", "Effect on this condition each step", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Start", "Start", "Start at a specific iteration", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("End", "End", "End after N iterations (-1 to keep running forever)", GH_ParamAccess.item, -1);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Condition", "Condition", "Condition", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = "";
            string target = "";
            double effect = 0.0;
            double divider = 0.0;
            int start = 0;
            int end = -1;

            DA.GetData("Name", ref name);
            DA.GetData("Target Property", ref target);
            DA.GetData("Effect", ref effect);
            DA.GetData("Start", ref start);
            DA.GetData("End", ref end);

            Condition omniCondition = new Condition(name, ConditionType.Omni, target, effect, divider, start, end);

            DA.SetData(0, omniCondition);
            this.Message = name;
        }

    }
}