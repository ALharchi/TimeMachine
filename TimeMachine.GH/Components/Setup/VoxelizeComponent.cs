using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Rhino;

namespace TimeMachine.GH
{
    public class VoxelizeComponent : GH_Component
    {
        public VoxelizeComponent() : base("Voxelize", "Voxelize", "Approximate a geometry with a voxel representation", "TimeMachine", "Setup") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconVoxelize; } }
        public override Guid ComponentGuid { get { return new Guid("0d76bfb4-cc9c-4cb1-82f8-865d2450d898"); } }
        public override GH_Exposure Exposure { get { return GH_Exposure.primary; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGeometryParameter("Input Geometry", "Input Brep", "Geometry to voxelize", GH_ParamAccess.item);
            pManager.AddIntegerParameter("U Count", "U Count", "Numer of segments in {u} direction", GH_ParamAccess.item, 10);
            pManager.AddIntegerParameter("V Count", "V Count", "Numer of segments in {v} direction", GH_ParamAccess.item, 10);
            pManager.AddIntegerParameter("W Count", "W Count", "Numer of segments in {w} direction", GH_ParamAccess.item, 10);
            pManager.AddBooleanParameter("Show Grid", "Show Grid", "Show the base grid used to compute the voxels positions", GH_ParamAccess.item, false);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Positions", "Positions", "Voxels point representation", GH_ParamAccess.list);
            pManager.AddPointParameter("Grid", "Grid", "Show the overall grid", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            dynamic inputGeometry = 0;

            Mesh inputMesh = new Mesh();
            int uCount = 10;
            int vCount = 10;
            int wCount = 10;
            bool showGrid = false;

            DA.GetData("Input Geometry", ref inputGeometry);

            if (inputGeometry.Value is Brep)
            {
                Mesh[] inputMeshes = Mesh.CreateFromBrep(inputGeometry.Value, MeshingParameters.Default);
                foreach (Mesh m in inputMeshes)
                {
                    inputMesh.Append(m);
                }
                inputMesh.Compact();
            }
            else
            {
                inputMesh = inputGeometry.Value;
            }

            DA.GetData("U Count", ref uCount);
            DA.GetData("V Count", ref vCount);
            DA.GetData("W Count", ref wCount);
            DA.GetData("Show Grid", ref showGrid);

            List<Point3d> gridPoints = new List<Point3d>();
            List<Point3d> outputPoints = new List<Point3d>();


            BoundingBox bbox = inputMesh.GetBoundingBox(true);

            Point3d minCorner = bbox.Min;
            Point3d maxCorner = bbox.Max;

            double maxX = maxCorner.X - minCorner.X;
            double maxY = maxCorner.Y - minCorner.Y;
            double maxZ = maxCorner.Z - minCorner.Z;

            List<Point3d> uPoints = new List<Point3d>();
            for (double i = 0; i <= maxX; i += maxX / uCount)
            {
                Vector3d uVector = Vector3d.XAxis;
                uVector = uVector * i;
                Point3d upt = new Point3d(minCorner);
                Transform uxform = Transform.Translation(uVector);
                upt.Transform(uxform);
                uPoints.Add(upt);
            }

            List<Point3d> vPoints = new List<Point3d>();
            for (double i = maxY / vCount; i <= maxY; i += maxY / vCount)
            {
                Vector3d vVector = Vector3d.YAxis;
                vVector = vVector * i;
                Point3d vpt = new Point3d(minCorner);

                foreach (Point3d pt in uPoints)
                {
                    Point3d newPt = new Point3d(pt);
                    Transform vxform = Transform.Translation(vVector);
                    newPt.Transform(vxform);
                    vPoints.Add(newPt);
                }
            }

            List<Point3d> wPoints = new List<Point3d>();
            for (double i = maxZ / wCount; i <= maxZ; i += maxZ / wCount)
            {
                Vector3d wVector = Vector3d.ZAxis;
                wVector = wVector * i;
                Point3d wpt = new Point3d(minCorner);

                foreach (Point3d pt in vPoints)
                {
                    Point3d newPt = new Point3d(pt);
                    Transform wxform = Transform.Translation(wVector);
                    newPt.Transform(wxform);
                    wPoints.Add(newPt);
                }
            }



            gridPoints.AddRange(uPoints);
            gridPoints.AddRange(vPoints);
            gridPoints.AddRange(wPoints);

            foreach (Point3d pt in gridPoints)
            {
                if (inputMesh.IsPointInside(pt, RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, true))
                {
                    outputPoints.Add(pt);
                }
            }


            DA.SetDataList(0, outputPoints);

            if (showGrid)
            {
                DA.SetDataList(1, gridPoints);
            }
            
        }

        
    }
}