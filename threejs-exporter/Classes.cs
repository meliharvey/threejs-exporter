using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace glTF_exporter
{
    public class MeshWrapper
    {
        public dynamic Geometry;
        public dynamic Children;
        public dynamic Material;

        public MeshWrapper(dynamic geometry, dynamic children, dynamic material)
        {
            Geometry = geometry;
            Children = children;
            Material = material;
        }
    }

    public class MaterialWrapper
    {
        public dynamic Material;

        public MaterialWrapper(dynamic material)
        {
            Material = material;
        }
    }

}