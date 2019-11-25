using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace glTF_exporter
{
    public class ExportJSON : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SaveCSV class.
        /// </summary>
        public ExportJSON()
          : base("Export JSON", "JSON",
              "Export scene as a JSON file.",
              "Threejs", "Scene")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Path", "P", "Path to file directory", GH_ParamAccess.item);
            pManager.AddTextParameter("File Name", "N", "CSV file name", GH_ParamAccess.item);
            pManager.AddTextParameter("Data", "D", "Data as a list of strings", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Full Path", "P", "The full file path of the CSV", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = null;
            string name = null;
            List<string> data = new List<string>();

            DA.GetData(0, ref path);
            DA.GetData(1, ref name);
            if ((!DA.GetDataList(2, data)))
                return;
            if ((data.Count == 0))
                return;

            string filePath = path + "\\" + name + ".json";
            string dataCombined = "";

            for (int i = 0; i < data.Count; i++)
            {
                dataCombined += data[i] + Environment.NewLine;
            }

            File.WriteAllText(filePath, dataCombined);

            DA.SetData(0, filePath);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Properties.Resources.ExportJSON;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8bc3d185-7333-4633-8057-a72500a9d971"); }
        }
    }
}