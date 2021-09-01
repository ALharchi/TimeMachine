using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;


namespace TimeMachine.GH
{
    public class HumidityComponent : GH_Component
    {

        public Property property;

        public HumidityComponent() : base("Humidity", "Humidity", "Add Humidity to the simulation", "TimeMachine", "Conditions") { }
        protected override System.Drawing.Bitmap Icon { get { return null; } }
        public override Guid ComponentGuid { get { return new Guid("ff33d28b-85e7-475b-a76a-63166626110a"); } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Humidity", "Humidity", "Humidity Condition", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            this.property = new Property("Humidity", 0, 100);

        }

    }
}
