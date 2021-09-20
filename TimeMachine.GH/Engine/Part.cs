using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;


namespace TimeMachine.GH.Engine
{
    public class Part
    {

        public Brep PartBrep;
        public Mesh PartMesh;

        public List<Point3d> Pts;

        public Part()
        { 
        
        }

    }
}
