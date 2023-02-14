using System.Collections.Generic;
using System.Linq;

namespace GEOBOX.OSC.DHM.Tools.Common
{
    /// <summary>
    /// Contains the sorted data form the DHM.
    /// </summary>
    public class DhmDataSorted
    {
        /// <summary>
        /// The sorted DHM data is by purpose a simple nested list because it outperforms complex objects in speed by a lot here.
        /// </summary>
        private readonly List<KeyValuePair<decimal, IReadOnlyList<KeyValuePair<decimal, decimal>>>> data;

        /// <summary>
        /// The point coordinates and heights as nested list (Dictionary&lt;X-Coordinate, Dictionary&lt;Y-Coordinate, Z-Coordinate&gt;&gt;)
        /// </summary>
        public IReadOnlyList<KeyValuePair<decimal, IReadOnlyList<KeyValuePair<decimal, decimal>>>> Values => data;

        /// <summary>
        /// Construct empty.
        /// </summary>
        public DhmDataSorted()
        {
            data = new List<KeyValuePair<decimal, IReadOnlyList<KeyValuePair<decimal, decimal>>>>();
        }

        /// <summary>
        /// Construct with DhmData.
        /// </summary>
        /// <param name="dhmData">The unsorted point coordinates and heights as DhmData</param>
        public DhmDataSorted(DhmData dhmData)
        {
            data = new List<KeyValuePair<decimal, IReadOnlyList<KeyValuePair<decimal, decimal>>>>();

            // add values
            foreach (var xCoordinate in dhmData.Values)
            {
                data.Add(new KeyValuePair<decimal, IReadOnlyList<KeyValuePair<decimal, decimal>>>(xCoordinate.Key, xCoordinate.Value.OrderBy(data => data.Key).ToList()));
            }

            // sort
            data = data.OrderBy(data => data.Key).ToList();
        }

        /// <summary>
        /// Construct with a nested list.
        /// </summary>
        /// <param name="dhmDataSorted">The unsorted point coordinates and heights as nested list</param>
        public DhmDataSorted(List<KeyValuePair<decimal, IReadOnlyList<KeyValuePair<decimal, decimal>>>> dhmDataSorted)
        {
            data = dhmDataSorted;
        }

        /// <summary>
        /// Add a coordinate to the end.
        /// </summary>
        /// <param name="xCoordinate">The x-coordinate</param>
        /// <param name="yZCoordinatesSorted">The y- and z-coordinates as a list</param>
        public void Add(decimal xCoordinate, List<KeyValuePair<decimal, decimal>> yZCoordinatesSorted)
        {
            data.Add(new KeyValuePair<decimal, IReadOnlyList<KeyValuePair<decimal, decimal>>>(xCoordinate, yZCoordinatesSorted));
        }
    }
}
