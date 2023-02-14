using System.Linq;

namespace GEOBOX.OSC.DHM.Tools.Common
{
    /// <summary>
    /// Reduces a DHM uniformly by 2^halfingFactor.
    /// </summary>
    public static class DhmReducer
    {
        /// <summary>
        /// Reduce the DHM uniformly by 2^halfingFactor (e.g. halfingFactor: 4, points(input): 32'000 => divisor: 2^4 = 16, points(result): 32'000 / 16 = 2'000)
        /// </summary>
        /// <param name="dhmData">The point coordinates and heights to reduce as DhmData</param>
        /// <param name="halfingFactor">The halfing factor for the reduction</param>
        /// <returns></returns>
        public static DhmData HalfeByFactor(DhmData dhmData, int halfingFactor)
        {
            if (halfingFactor <= 1)
            {
                return dhmData;
            }

            var reducedDhmDataSorted = ReduceByFactor(new DhmDataSorted(dhmData), halfingFactor);

            return new DhmData(reducedDhmDataSorted);
        }

        private static DhmDataSorted ReduceByFactor(DhmDataSorted dhmDataSorted, int halfingFactor)
        {
            bool halfingFactorIsEven = (halfingFactor % 2) == 0;
            DhmDataSorted reducedDhmDataSorted;

            if (halfingFactorIsEven) // even: remove odd entries from outer list
            {
                bool isEven = false; // do not remove first entry
                reducedDhmDataSorted = new DhmDataSorted(dhmDataSorted.Values.Where(x => isEven = !isEven).ToList()); // remove odd entries from dhmDataList (isEven alternates and removes every second entry)
            }
            else // odd: remove odd/even entries from inner lists
            {
                reducedDhmDataSorted = new DhmDataSorted();
                bool isEvenStartValue = false; // do not remove the first entry

                foreach (var xCoordinate in dhmDataSorted.Values)
                {
                    bool isEven = isEvenStartValue;
                    isEvenStartValue = !isEvenStartValue; // alternate: first remove odd then even and odd again...

                    // remove odd/even entries from inner List (isEven alternates and removes every second entry)
                    reducedDhmDataSorted.Add(xCoordinate.Key, xCoordinate.Value.Where(x => isEven = !isEven).ToList());
                }
            }

            if (halfingFactor > 1)
            {
                halfingFactor--;
                return ReduceByFactor(reducedDhmDataSorted, halfingFactor); // call recursively
            }

            return reducedDhmDataSorted;
        }
    }
}
