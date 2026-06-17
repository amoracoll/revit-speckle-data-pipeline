#region System Namespaces
using System.Collections.Generic;
#endregion

#region Autodesk Namespaces
using Autodesk.Revit.DB;
#endregion

#region RevitSpeckleExporter
using RevitSpeckleExporter.Models;
#endregion

namespace RevitSpeckleExporter.Services
{
    public class ElementDataExtractor
    {
        public List<WallData> ExtractWalls(Document document)
        {
            List<WallData> walls = new List<WallData>();

            FilteredElementCollector collector = new FilteredElementCollector(document)
                .OfClass(typeof(Wall));

            foreach (Element element in collector)
            {
                Wall wall = element as Wall;

                if (wall == null)
                {
                    continue;
                }

                WallData wallData = MapWallToData(wall, document);

                if (wallData == null)
                {
                    continue;
                }

                walls.Add(wallData);
            }

            return walls;
        }

        private WallData MapWallToData(Wall wall, Document doc)
        {
            Parameter levelParam = wall.get_Parameter(BuiltInParameter.LEVEL_PARAM);
            string levelName = "Unknown";

            if (levelParam != null && levelParam.HasValue)
            {
                Level level = doc.GetElement(levelParam.AsElementId()) as Level;

                if (level != null)
                {
                    levelName = level.Name;
                }
            }

            Parameter lengthParam = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);

            Parameter areaParam = wall.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED);

            return new WallData
            {
                ElementId = wall.Id.IntegerValue,
                WallType = wall.Name,
                LevelName = levelName,
                Length = lengthParam?.AsDouble() ?? 0,
                Area = areaParam?.AsDouble() ?? 0
            };
        }
    }
}
