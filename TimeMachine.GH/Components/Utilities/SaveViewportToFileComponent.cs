using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Rhino.Display;
using System.Drawing;

namespace TimeMachine.GH.Components.Utilities
{
    public class SaveViewportToFileComponent : GH_Component
    {
        public SaveViewportToFileComponent() : base("Save Viewport To File", "Save Viewport To File", "Capture the viewport to a file", "TimeMachine", "Utilities") { }
        protected override System.Drawing.Bitmap Icon { get { return Properties.Resources.iconViewportToFile; } }
        public override Guid ComponentGuid { get { return new Guid("1278071d-051c-4ce3-8240-e1efff8740be"); } }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Viewport Name", "Viewport Name", "The name of the viewport to save. If empty, the active viewport will be used.", GH_ParamAccess.item, "");
            pManager.AddTextParameter("File Path", "File Path", "Where to save the image", GH_ParamAccess.item);

            pManager.AddBooleanParameter("Draw Axes", "Draw Axes", "Draw the axes in the image capture", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Draw Grid", "Draw Grid", "Draw the grid in the image capture", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Draw Grid Axes", "Draw Grid Axes", "Draw the grid axes in the image capture", GH_ParamAccess.item, false);

            pManager.AddBooleanParameter("Save", "Save", "Save the selected viewport", GH_ParamAccess.item, false);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            string viewportName = "";
            string filePath = "";
            bool drawAxes = false;
            bool drawGrid = false;
            bool drawGridAxes = false;
            bool save = false;

            DA.GetData(0, ref viewportName);
            DA.GetData(1, ref filePath);
            DA.GetData(2, ref drawAxes);
            DA.GetData(3, ref drawGrid);
            DA.GetData(4, ref drawGridAxes);
            DA.GetData(5, ref save);

            if (save)
            {
                RhinoView view = Rhino.RhinoDoc.ActiveDoc.Views.ActiveView;
                if (viewportName != "")
                {
                    view = Rhino.RhinoDoc.ActiveDoc.Views.Find(viewportName, false);
                }
                
                ViewCapture view_capture = new ViewCapture
                {
                    Width = view.ActiveViewport.Size.Width,
                    Height = view.ActiveViewport.Size.Height,
                    ScaleScreenItems = false,
                    DrawAxes = drawAxes,
                    DrawGrid = drawGrid,
                    DrawGridAxes = drawGridAxes,
                    TransparentBackground = false
                };

                Bitmap bitmap = view_capture.CaptureToBitmap(view);

                if (null != bitmap)
                {
                    bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            
        }

        
    }
}