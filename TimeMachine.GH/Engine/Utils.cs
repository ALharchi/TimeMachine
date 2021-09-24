using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Geometry;


namespace TimeMachine.GH
{
    public class Utils
    {


        public static List<Point3d> Voxelize(Mesh inputMesh, int uCount, int vCount, int wCount)
        {
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

            return outputPoints;
        }

    }
}
