#region System Namespaces
using System.Reflection;
using System.Windows.Media.Imaging;
#endregion

#region Autodesk Namespaces
using Autodesk.Revit.UI;
using Autodesk.Revit.Exceptions;
#endregion

#region RevitSpeckleExporter Namespaces
using RevitSpeckleExporter.Resources;
#endregion

namespace RevitSpeckleExporter
{
    internal class App : IExternalApplication
    {
        private const string TabName = "RevitSpeckleExporter";

        public Result OnStartup(UIControlledApplication uiControlledApplication)
        {
            try
            {
                uiControlledApplication.CreateRibbonTab(TabName);
            }
            catch (ArgumentException)
            {

            }

            RibbonPanel panel = uiControlledApplication.CreateRibbonPanel(TabName, "Export");

            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            BitmapSource exportIcon = IconHelper.ToBitmapSource(RevitSpeckleExporterResources.ExportToSpeckle);

            PushButtonData btnExport = new PushButtonData(
                "ExportToSpeckle",
                "Export to\nSpeckle",
                assemblyPath,
                "RevitSpeckleExporter.Commands.ExportToSpeckleCommand")
            {
                ToolTip = "Exports model elements to a Speckle stream",
                LargeImage = exportIcon,
                Image = exportIcon
            };

            panel.AddItem(btnExport);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
