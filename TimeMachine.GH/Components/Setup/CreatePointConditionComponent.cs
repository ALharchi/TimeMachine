using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH.Components.Setup
{
    public class CreatePointConditionComponent : GH_Component
    {
        public CreatePointConditionComponent() : base("Create Point Condition", "Create Point Condition", "Create a condition that from a single point in all directions.", "TimeMachine", "Setup") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconCreatePointCondition; } }
        public override Guid ComponentGuid { get { return new Guid("8bf9c3ee-d2cf-4f5a-a1e3-b70b24d0b674"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.secondary; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Condition Name", GH_ParamAccess.item, "Default");
            pManager.AddTextParameter("Target Property", "Target Property", "Specify which property is affected by this condition.", GH_ParamAccess.item);
            pManager.AddPointParameter("Source", "Source", "Source of the condition (Point3D)", GH_ParamAccess.item);
            pManager.AddNumberParameter("Effect", "Effect", "Effect on this condition each step", GH_ParamAccess.item);
            pManager.AddNumberParameter("Divider", "Divider", "Spread divider", GH_ParamAccess.item, 0.5);
            pManager.AddIntegerParameter("Start", "Start", "Start at a specific iteration", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("End", "End", "End after N iterations (0 to keep forever)", GH_ParamAccess.item, 0);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Condition", "Condition", "Condition", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = "";
            string target = "";
            Point3d source = new Point3d();
            double effect = 0.0;
            double divider = 0.0;
            int start = 0;
            int end = 0;

            DA.GetData("Name", ref name);
            DA.GetData("Target Property", ref target);
            DA.GetData("Source", ref source);
            DA.GetData("Effect", ref effect);
            DA.GetData("Divider", ref divider);
            DA.GetData("Start", ref start);
            DA.GetData("End", ref end);

            Condition pointCondition = new Condition(name, ConditionType.Point, target, effect, divider, start, end);

            DA.SetData(0, pointCondition);
        }


    }
}