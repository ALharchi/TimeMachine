using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;


namespace TimeMachine.GH
{
    public class PropertyComponent : GH_Component
    {

        public Property property;

        public PropertyComponent() : base("Property", "Property", "Add a property to a Voxel", "TimeMachine", "Setup") { }
        protected override System.Drawing.Bitmap Icon { get { return null; } }
        public override Guid ComponentGuid { get { return new Guid("ff33d28b-85e7-475b-a76a-63166626110a"); } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Property Name", GH_ParamAccess.item, "Default");
            pManager.AddNumberParameter("Start", "Start", "Initial Value to Start with", GH_ParamAccess.item, 50);
            pManager.AddNumberParameter("Min", "Min", "Minimum Property Value", GH_ParamAccess.item, 0);
            pManager.AddNumberParameter("Max", "Max", "Maximum Property Value", GH_ParamAccess.item, 100);
            pManager.AddBooleanParameter("Kill Threshold", "Kill Threshold", "Kill the Parent Voxel if Value is outside the specificed Interval", GH_ParamAccess.item, false);
            pManager[5].Optional = true;
            pManager.AddIntervalParameter("Living Interval", "Living Interval", "Living Interval", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Property", "Property", "Humidity Condition", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

        }

    }
}
