#region System Namespaces
#endregion

#region Autodesk Namespaces
#endregion

#region RevitSpeckleExporter
#endregion

namespace RevitSpeckleExporter.Models
{
    public class WallData
    {
        public int ElementId { get; set; }
        public string WallType { get; set; }
        public string LevelName { get; set; }
        public double Length { get; set; }
        public double Area { get; set; }

        public override string ToString()
        {
            return $"Wall [{ElementId}] {WallType} - Level: {LevelName}, Length: {Length:F2}, Area: {Area:F2}";
        }
    }
}
