using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Rhino;

namespace TimeMachine.GH
{
    public class CreatePlanarConditionComponent : GH_Component
    {
        public CreatePlanarConditionComponent() : base("Create Planar Condition", "Create Planar Condition", "Create a planar condition", "TimeMachine", "Setup") { }
        protected override System.Drawing.Bitmap Icon { get { return null; } }
        public override Guid ComponentGuid { get { return new Guid("41b98423-35ba-4462-a71d-9710a69a1d40"); } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Condition Name", GH_ParamAccess.item, "Default");
            pManager.AddTextParameter("Target Property", "Target Property", "Specify which property is affected by this condition.", GH_ParamAccess.item , "Default");
            pManager.AddBrepParameter("Target Geometry", "Target Geometry", "Target Geometry", GH_ParamAccess.item);
            pManager.AddRectangleParameter("Source", "Source", "Source of the condition", GH_ParamAccess.item);
            pManager.AddVectorParameter("Direction", "Direction", "Condition Direction", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Condition", "Condition", "Condition", GH_ParamAccess.item);
            pManager.AddBrepParameter("Target", "Target", "Target Approximation", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            ConditionType conditionType = ConditionType.Planar;

            Brep Target = null; Rectangle3d Source = new Rectangle3d(); Vector3d Direction = new Vector3d();

            Line extrDir = new Line(Source.Center, Direction);
            Vector3d dirUni = new Vector3d(Direction);
            
            dirUni.Unitize();
            
            Brep[] containingBrep = Brep.CreateFromSweep(extrDir.ToNurbsCurve(), Source.ToNurbsCurve(), false, RhinoDoc.ActiveDoc.ModelAbsoluteTolerance);
            Brep differenceBrep = Brep.CreateBooleanDifference(Target, containingBrep[0], RhinoDoc.ActiveDoc.ModelAbsoluteTolerance)[0];

            List<Brep> faces = new List<Brep>();

            foreach (BrepFace f in differenceBrep.Faces)
            {
                faces.Add(f.DuplicateFace(false));
            }

            List<Point3d> facesCenters = new List<Point3d>();


            foreach (Brep f in faces)
            {
                Surface brepFace = f.Faces[0].ToNurbsSurface();

                Curve[] crvs = Curve.JoinCurves(f.GetWireframe(0));
                Curve faceEdges = crvs[0];

                AreaMassProperties areaProp = AreaMassProperties.Compute(brepFace);

                facesCenters.Add(areaProp.Centroid);

            }



            //A = faces;
            //B = facesCenters;



        }


    }
}