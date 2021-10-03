using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Rhino;
using System.Linq;

namespace TimeMachine.GH
{
    public class GetPropertyComponent : GH_Component
    {
        public GetPropertyComponent() : base("Get Property", "Get Property", "Visualize a property evolution through time.", "TimeMachine", "Simulation") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconGetProperty; } }
        public override Guid ComponentGuid { get { return new Guid("41654c22-03b8-4809-bac0-7aebbaebd799"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.primary; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Simulation", "Simulation", "Simulation Results", GH_ParamAccess.item);
            pManager.AddTextParameter("Property", "Property", "Name of the property to get", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Step", "Step", "Which step do you want to get", GH_ParamAccess.item, 0);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Values", "Values", "Values at the given timestep", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            World myWorld = new World();
            string propertyName = "";
            int step = 0;


            DA.GetData(0, ref myWorld);
            DA.GetData(1, ref propertyName);
            DA.GetData(2, ref step);


            List<dynamic> outputValues = new List<dynamic>();

            if (step > myWorld.CurrentStep - 1)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "This step doesn't exist in this simulation!");
                return;
            }

            foreach (Voxel v in myWorld.Voxels)
            {
                Property targetProp = v.Properties.Where(property => property.Name == propertyName).FirstOrDefault();

                if (targetProp == null)
                {
                    outputValues.Add(null);
                }
                else
                {
                    outputValues.Add(targetProp.Values[step]);
                }


            }


            /*
            for (int i = 0; i <= step; i++)
            {
                // we need to get the property index
                Property property = new Property();

                int ind = 0;
                foreach (Property propIter in myWorld.Voxels[i].Properties)
                {
                    if (propIter.Name == propertyName)
                    {
                        property = propIter;
                        break;
                    }
                    ind++;
                }

                if (property.Name == null)
                {
                    this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "The property doesn't exist in this simulation!");
                    return;
                }

                // Finally we populate our values from each voxel
                foreach (Voxel v in myWorld.Voxels)
                {
                    // we get the property value in that particular timestep
                    double voxelPropertyValue = v.Properties[ind].Values[i];
                    outputValues.Add(voxelPropertyValue);
                }
                
            }
            */
            DA.SetDataList(0, outputValues);
            this.Message = "Current step: " + step.ToString();
        }
    }
}