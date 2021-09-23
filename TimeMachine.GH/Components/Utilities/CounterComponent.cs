using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH
{
    public class CounterComponent : GH_Component
    {
        public CounterComponent() : base("Counter", "Counter", "A counter that goes up and down", "TimeMachine", "Utilities") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconCounter; } }
        public override Guid ComponentGuid { get { return new Guid("1c669d49-86fa-43b9-95eb-3cc52eab05fb"); } }

        int count = 0;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Next", "N", "Next", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Previous", "P", "Previous", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Reset", "R", "Reset Count", GH_ParamAccess.item, false);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Count", "C", "Current Count", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool n = false;
            bool p = false;
            bool r = false;

            DA.GetData(0, ref n);
            DA.GetData(1, ref p);
            DA.GetData(2, ref r);

            if (r)
                this.count = 0;

            if (n)
                this.count += 1;

            if (p == true && this.count != 0)
                this.count -= 1;

            this.Message = count.ToString();
            DA.SetData(0, count);
        }

        
    }
}