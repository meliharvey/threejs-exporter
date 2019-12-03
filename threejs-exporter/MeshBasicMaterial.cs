using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Newtonsoft.Json;

namespace glTF_exporter
{
    public class MeshBasicMaterial : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MeshBasicMaterial class.
        /// </summary>
        public MeshBasicMaterial()
          : base("MeshBasicMaterial", "BasicMat",
              "Create a MeshBasicMaterial.",
              "Threejs", "Materials")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "C", "Add a color", GH_ParamAccess.item, Color.CornflowerBlue);
            pManager.AddBooleanParameter("Wireframe", "W", "Display as wireframe", GH_ParamAccess.item, false);
            pManager.AddTextParameter("WireframeLineJoin", "J", "Style of wireframe joins ('round', 'miter', or 'bevel'", GH_ParamAccess.item, "round");
            pManager.AddNumberParameter("WireframeLineWidth", "L", "The width of the wireframe line", GH_ParamAccess.item, 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("JSON", "J", "JSON string", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "M", "Threejs material", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Color color = Color.White;
            bool wireframe = false;
            string wireframeLineJoin = "round";
            double wireframeLineWidth = 1;

            DA.GetData(0, ref color);
            DA.GetData(1, ref wireframe);
            DA.GetData(2, ref wireframeLineJoin);
            DA.GetData(3, ref wireframeLineWidth);

            

            dynamic material = new ExpandoObject();
            material.uuid = Guid.NewGuid();
            material.type = "MeshBasicMaterial";

            string hexColor = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
            material.color = Convert.ToInt32(hexColor, 16);

            material.wireframe = wireframe;
            material.wireframeLineJoin = wireframeLineJoin;
            material.wireframeLineWidth = wireframeLineWidth;

            //Wrap the material
            MaterialWrapper wrapper = new MaterialWrapper(material);

            string JSON = JsonConvert.SerializeObject(material);

            DA.SetData(0, JSON);
            DA.SetData(1, wrapper);
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
                return Properties.Resources.MeshBasicMaterial;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("87793bf9-80e8-44e7-aa29-59c7c138511b"); }
        }
    }
}