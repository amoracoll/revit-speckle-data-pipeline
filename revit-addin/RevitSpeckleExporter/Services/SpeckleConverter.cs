#region System Namespaces
using System.Collections.Generic;
#endregion

#region Speckle Namespaces
using Speckle.Core.Models;
#endregion

#region RevitSpeckleExporter Namespaces
using RevitSpeckleExporter.Models;
#endregion

namespace RevitSpeckleExporter.Services
{
    internal class SpeckleConverter
    {
        internal Base Convert(List<WallData> walls)
        {
            Base root = new Base();
            List<Base> speckleWalls = new List<Base>();

            foreach (WallData wall in walls)
            {
                Base obj = new Base();
                obj["elementId"] = wall.ElementId;
                obj["type"] = wall.WallType;
                obj["level"] = wall.LevelName;
                obj["length"] = wall.Length;
                obj["area"] = wall.Area;
                speckleWalls.Add(obj);
            }

            root["@walls"] = speckleWalls;

            return root;
        }
    }
}
