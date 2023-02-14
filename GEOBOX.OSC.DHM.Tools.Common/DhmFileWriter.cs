using System.IO;

namespace GEOBOX.OSC.DHM.Tools.Common
{
    /// <summary>
    /// Writes DHM files.
    /// </summary>
    public static class DhmFileWriter
    {
        /// <summary>
        /// Create a file with DHM data. Existing files are overwritten.
        /// </summary>
        /// <param name="filePath">Path of the file to create(including the filename and extension)</param>
        /// <param name="dhmData">The point coordinates and heights to write as DhmData</param>
        public static void Write(string filePath, DhmData dhmData)
        {
            using var stream = File.CreateText(filePath);

            // loop the X-Coordinates
            foreach (var xCoordinate in dhmData.Values)
            {
                // loop the Y-Coordinates
                foreach (var yZCoordinate in xCoordinate.Value)
                {
                    // write the coordinates line (X-Coordinate Y-Coordinate Z-Coordinate)
                    stream.WriteLine($"{xCoordinate.Key} {yZCoordinate.Key} {yZCoordinate.Value:0.00}");
                }
            }
        }
    }
}
