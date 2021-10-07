using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;


namespace TimeMachine.GH
{
    public class CreatePropertyComponent : GH_Component
    {
        public CreatePropertyComponent() : base("Create Property", "Property", "Add a property to a Voxel", "TimeMachine", "Setup") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconCreateProperty; } }
        public override Guid ComponentGuid { get { return new Guid("ff33d28b-85e7-475b-a76a-63166626110a"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.tertiary; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Property Name", GH_ParamAccess.item);
            pManager.AddNumberParameter("Start", "Start", "Initial Value to Start with", GH_ParamAccess.item, 50);
            pManager.AddNumberParameter("Min", "Min", "Minimum Property Value", GH_ParamAccess.item, 0);
            pManager.AddNumberParameter("Max", "Max", "Maximum Property Value", GH_ParamAccess.item, 100);
            pManager.AddBooleanParameter("Kill Threshold", "Kill Threshold", "Kill the Parent Voxel if Value is outside the specificed Interval", GH_ParamAccess.item, false);
            pManager.AddNumberParameter("Multiplier", "Multiplier", "Multiplier", GH_ParamAccess.item, 1);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Property", "Property", "Property of a Voxel", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = "";
            double startValue = 0;
            double minValue = 0;
            double maxValue = 0;
            bool kill = false;
            double multiplier = 0.0;

            DA.GetData(0, ref name);
            DA.GetData(1, ref startValue);
            DA.GetData(2, ref minValue);
            DA.GetData(3, ref maxValue);
            DA.GetData(4, ref kill);
            DA.GetData(5, ref multiplier);


            List<double> values = new List<double>();
            values.Add(startValue);

            Property property = new Property(name, values, minValue, maxValue, kill, multiplier);

            DA.SetData(0, property);

            this.Message = name;
        }

    }
}
