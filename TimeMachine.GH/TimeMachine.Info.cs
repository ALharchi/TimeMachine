using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace TimeMachine.GH
{
    public class TimeMachineInfo : GH_AssemblyInfo
    {
        public override string Name { get { return "TimeMachine"; } }
        public override Bitmap Icon { get { return null; } }
        public override string Description { get { return "This plugin is used to simulate elemets durability under different environmental conditions."; } }
        public override Guid Id { get { return new Guid("68454440-eebd-4f3e-9e46-0c38a9203054"); } }
        public override string AuthorName { get { return "Ayoub Lharchi"; } }
        public override string AuthorContact { get { return "alha@kglakademi.dk"; } }
    }
}
