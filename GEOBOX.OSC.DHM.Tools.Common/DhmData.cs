using System.Collections.Generic;
using System.Linq;

namespace GEOBOX.OSC.DHM.Tools.Common
{
    /// <summary>
    /// Contains the data form the DHM.
    /// </summary>
    public class DhmData
    {
        /// <summary>
        /// The DHM data is by purpose a simple nested dictionary because it outperforms complex objects in speed by a lot here.
        /// </summary>
        private readonly Dictionary<decimal, Dictionary<decimal, decimal>> data;

        /// <summary>
        /// The point coordinates and heights as nested dictionary (Dictionary&lt;X-Coordinate, Dictionary&lt;Y-Coordinate, Z-Coordinate&gt;&gt;)
        /// </summary>
        public IReadOnlyDictionary<decimal, Dictionary<decimal, decimal>> Values => data;

        /// <summary>
        /// Construct empty.
        /// </summary>
        public DhmData()
        {
            data = new Dictionary<decimal, Dictionary<decimal, decimal>>();
        }

        /// <summary>
        /// Construct with DhmDataSorted.
        /// </summary>
        /// <param name="dhmDataSorted"></param>
        public DhmData(DhmDataSorted dhmDataSorted)
        {
            data = dhmDataSorted.Values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToDictionary(innerKvp => innerKvp.Key, innerKvp => innerKvp.Value));
        }

        /// <summary>
        /// Add a coordinate to the end.
        /// </summary>
        /// <param name="xCoordinate">The x-coordinate</param>
        /// <param name="yCoordinate">The y-coordinate</param>
        /// <param name="zCoordinate">The z-coordinate</param>
        public void Add(decimal xCoordinate, decimal yCoordinate, decimal zCoordinate)
        {
            data.TryAdd(xCoordinate, new Dictionary<decimal, decimal>());
            data[xCoordinate].Add(yCoordinate, zCoordinate);
        }
    }
}
