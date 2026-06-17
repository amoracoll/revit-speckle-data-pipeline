#region System Namespaces
using System;
using System.Collections.Generic;
#endregion

#region Autodesk Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

#region RevitSpeckleExporter
using RevitSpeckleExporter.Services;
#endregion

namespace RevitSpeckleExporter.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class ExportToSpeckleCommand : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;

            try
            {
                ElementDataExtractor extractor = new ElementDataExtractor();
                List<Models.WallData> walls = extractor.ExtractWalls(doc);

                // TODO: revit speckle exportes is not implemented yet
                TaskDialog.Show(
                    "Revit Speckle Exporter",
                    $"Se han extraído {walls.Count} muros del modelo.\n\n" +
                    $"Ejemplo: {(walls.Count > 0 ? walls[0].ToString() : "N/A")}"
                );

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
