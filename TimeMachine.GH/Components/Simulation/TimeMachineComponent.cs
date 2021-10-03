using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace TimeMachine.GH
{
    public class TimeMachineComponent : GH_Component
    {

        public TimeMachineComponent() : base("Simulation Engine", "Simulation Engine", "Simulate the time ", "TimeMachine", "Simulation") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconTimeMachine; } }
        public override Guid ComponentGuid { get { return new Guid("988da09d-65b6-40e8-8d55-7d146477b352"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.primary; } }

        World myWorld;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Voxels", "Voxels", "Specify the Voxels to Simulate", GH_ParamAccess.list);
            pManager.AddGenericParameter("Conditions", "Conditions", "Add Conditions to the Simulation", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Steps", "Steps", "Simulation Steps", GH_ParamAccess.item, 10);
            pManager.AddNumberParameter("Neighbourhood Radius", "Neighbourhood Radius", "Neighbourhood Radius", GH_ParamAccess.item, 100);
            pManager.AddBooleanParameter("Simulate", "Simulate", "Run the Simulation", GH_ParamAccess.item, false);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Simulation", "Simulation", "The simulation results after all the steps", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Voxel> inputVoxels = new List<Voxel>();
            List<Condition> conditions = new List<Condition>();
            int stepsCount = 0;
            double neighbourhoodRadius = 0.0;
            bool simulate = false;

            DA.GetDataList("Voxels", inputVoxels);
            DA.GetDataList("Conditions", conditions);
            DA.GetData("Steps", ref stepsCount);
            DA.GetData<double>("Neighbourhood Radius", ref neighbourhoodRadius);
            DA.GetData("Simulate", ref simulate);

            if (simulate)
            {
                List<Voxel> voxels = new List<Voxel>();

                foreach (Voxel v in inputVoxels)
                {
                    Voxel newV = v.Clone();
                    voxels.Add(newV);
                }

                this.myWorld = new World(voxels, conditions, neighbourhoodRadius);

                for (int i = 0; i < stepsCount; i++)
                {
                    myWorld.Update();
                }   
            }

            DA.SetData(0, myWorld);

            if (myWorld == null)
                this.Message = "Not initiated";
            else
                this.Message = "Voxels: " + myWorld.Voxels.Count.ToString() +
                               "\n Conditions: " + myWorld.Conditions.Count.ToString() +
                               "\n Steps: " + myWorld.CurrentStep.ToString();

        }
    }
}