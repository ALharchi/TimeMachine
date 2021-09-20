using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH
{
    public class CreateOmniConditionComponent : GH_Component
    {
        public CreateOmniConditionComponent() : base("Apply Condition", "Apply Condition", "Create a starting condition to a set of voxels", "TimeMachine", "Setup") { }
        protected override System.Drawing.Bitmap Icon { get { return null; } }
        public override Guid ComponentGuid { get { return new Guid("a206cda5-d484-4847-9bc1-034a414be95d"); } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Condition Name", GH_ParamAccess.item, "Default");
            pManager.AddTextParameter("Target Property", "Target Property", "Specify which property is affected by this condition.", GH_ParamAccess.item);
            pManager.AddPointParameter("Source", "Source", "Source of the condition", GH_ParamAccess.item);
            pManager.AddNumberParameter("Effect", "Effect", "Effect on this condition each step", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Spread", "Spread", "Spread of the condition. This means how many neighbor voxels will be affected", GH_ParamAccess.item);

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Condition", "Condition", "Condition", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {



        }

        
    }
}