using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Grasshopper;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Special;

namespace TimeMachine.GH
{
    public class CreateVoxelComponent : GH_Component
    {
        public CreateVoxelComponent() : base("Create Voxel", "Voxel", "Create a new Voxel", "TimeMachine", "Setup") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconCreateVoxel; } }
        public override Guid ComponentGuid { get { return new Guid("1b113a3a-e500-483f-a46d-011494cb6c0c"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.primary; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Position", "Position", "Voxel Position in space", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Lifespan", "Lifespan", "Lifespan of the voxel. It will die after N iterations. 0 = Unlimited lifespan", GH_ParamAccess.item, 0);
            pManager.AddGenericParameter("Properties", "Properties", "Properties of the Voxel", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Fixed Properties", "Fixed Properties", "Fixed Properties", GH_ParamAccess.item, false);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Voxel", "Voxel", "Voxel with embedded Properties", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Point3d position = new Point3d();
            int lifeSpan = 0;
            List<Property> properties = new List<Property>();
            bool fixedProperties = false;


            DA.GetData(0, ref position);
            DA.GetData(1, ref lifeSpan);
            DA.GetDataList(2, properties);
            DA.GetData(3, ref fixedProperties);


            List<Property> clonedProperties = new List<Property>();

            foreach (Property p in properties)
            {
                clonedProperties.Add(p.Clone());
            }

            Voxel voxel = new Voxel(position, clonedProperties, lifeSpan, fixedProperties);

            DA.SetData(0, voxel);

            this.Message = "Properties: " + voxel.Properties.Count.ToString();
        }
    }
}