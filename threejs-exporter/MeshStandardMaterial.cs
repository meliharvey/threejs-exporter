using System;
using System.Drawing;
using System.Collections.Generic;
using System.Dynamic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Newtonsoft.Json;

namespace glTF_exporter
{
    public class MeshStandardMaterial : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ExportGLTF class.
        /// </summary>
        public MeshStandardMaterial()
          : base("MeshStandardMaterial", "MeshStdrdMat",
              "Create a mesh standard material.",
              "Threejs", "Materials")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "C", "Material's color", GH_ParamAccess.item, Color.CornflowerBlue);
            pManager.AddNumberParameter("Opacity", "O", "Material's opacity (0 = transparent, 1 = opaque)", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("Roughness", "R", "Material's roughness", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("Metalness", "M", "Material's metalness", GH_ParamAccess.item, 0.5);
            pManager.AddNumberParameter("Emissive", "E", "Material's emissiveness", GH_ParamAccess.item, 0);
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
            
            Color color = Color.Black;
            double opacity = 1;
            double roughness = 1;
            double metalness = 0.5;
            double emissive = 0;
            bool wireframe = false;
            string wireframeLineJoin = "round";
            double wireframeLineWidth = 1;

            DA.GetData(0, ref color);
            DA.GetData(1, ref opacity);
            DA.GetData(2, ref roughness);
            DA.GetData(3, ref metalness);
            DA.GetData(4, ref emissive);
            DA.GetData(5, ref wireframe);
            DA.GetData(6, ref wireframeLineJoin);
            DA.GetData(7, ref wireframeLineWidth);

            dynamic material = new ExpandoObject();
            material.uuid = Guid.NewGuid();
            material.type = "MeshStandardMaterial";
            material.color = Convert.ToInt32(ColorTranslator.ToHtml(color).Remove(0, 1), 16);
            material.transparent = true;
            material.opacity = opacity;
            material.roughness = roughness;
            material.metalness = metalness;
            material.emissive = emissive;
            material.wireframe = wireframe;
            material.wireframeLineWidth = wireframeLineJoin;
            material.wireframeLineWidth = wireframeLineWidth;

            /// Wrap the material
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
                return Properties.Resources.MeshStandardMaterial;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("52c8142d-9f07-4ceb-aca5-daef08d10a5c"); }
        }
    }
}