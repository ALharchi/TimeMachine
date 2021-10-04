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
            pManager.AddNumberParameter("Minimum", "Minimum", "Minimum Value of the property over all the voxels.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Maximum", "Maximum", "Minimum Value of the property over all the voxels.", GH_ParamAccess.item);
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
            List<double> globalValues = new List<double>();

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

                    for (int i = 0; i < myWorld.CurrentStep; i++)
                    {
                        globalValues.Add(targetProp.Values[i]);
                    }
                }
            }



            globalValues.Sort();


            DA.SetDataList(0, outputValues);
            DA.SetData(1, globalValues.First());
            DA.SetData(2, globalValues.Last());


            this.Message = "Current step: " + step.ToString() + 
                "\nMin: " + globalValues.First().ToString() +
                "\nMax: " + globalValues.Last().ToString("0.000")
                ;
        }
    }
}