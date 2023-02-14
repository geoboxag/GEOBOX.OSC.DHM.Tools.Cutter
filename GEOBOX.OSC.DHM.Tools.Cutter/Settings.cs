using System.ComponentModel;

namespace GEOBOX.OSC.DHM.Tools.Cutter
{
    /// <summary>
    /// The settings for the DHM cutting process.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The source folder to read the xyz-files (*.xyz) from.
        /// </summary>
        public string SourceFolder { get; set; }

        /// <summary>
        /// The destination file for the result.
        /// </summary>
        public string DestinationFile { get; set; }

        /// <summary>
        /// The x-coordinate of the lower boundary.
        /// </summary>
        public decimal XMinCoord { get; set; }

        /// <summary>
        /// The y-coordinate of the lower boundary.
        /// </summary>
        public decimal XMaxCoord { get; set; }

        /// <summary>
        /// The x-coordinate of the upper boundary.
        /// </summary>
        public decimal YMinCoord { get; set; }

        /// <summary>
        /// The y-coordinate of the upper boundary.
        /// </summary>
        public decimal YMaxCoord { get; set; }

        /// <summary>
        /// The halfing factor for the reduction.
        /// </summary>
        [DefaultValue(0)]
        public int HalfingFactor { get; set; }
    }
}
