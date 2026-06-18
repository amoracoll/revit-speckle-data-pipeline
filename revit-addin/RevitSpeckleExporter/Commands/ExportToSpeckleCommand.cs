#region System Namespaces
using System;
using System.Collections.Generic;
using System.Windows.Interop;
#endregion

#region Autodesk Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

#region Speckle Namespaces
using Speckle.Core.Models;
#endregion

#region RevitSpeckleExporter Namespaces
using RevitSpeckleExporter.Models;
using RevitSpeckleExporter.Services;
using RevitSpeckleExporter.Views;
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
            Document document = uiApp.ActiveUIDocument.Document;

            try
            {
                SpeckleExportDialog dialog = new SpeckleExportDialog();
                WindowInteropHelper owner = new WindowInteropHelper(System.Windows.Application.Current.MainWindow)
                {
                    Owner = uiApp.MainWindowHandle
                };

                bool confirmed = dialog.ShowDialog(owner);

                if (!confirmed)
                {
                    return Result.Cancelled;
                }

                List<WallData> walls = new ElementDataExtractor().ExtractWalls(document);

                if (walls.Count == 0)
                {
                    return Result.Cancelled;
                }

                Base root = new SpeckleConverter().Convert(walls);

                SpeckleService service = new SpeckleService(dialog.ServerUrl, dialog.StreamId, dialog.Token);

                string commitId = service.SendAsync(root, $"Walls export — {document.Title}").GetAwaiter().GetResult();

                TaskDialog.Show(
                    "Export complete",
                    $"Exported {walls.Count} walls.\nCommit: {commitId}\nStream: {dialog.StreamId}");

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
