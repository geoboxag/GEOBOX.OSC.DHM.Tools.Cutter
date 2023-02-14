namespace GEOBOX.OSC.DHM.Tools.Common
{
    /// <summary>
    /// Crops a DHM to the desired dimensions.
    /// </summary>
    public class DhmCropingHelper
    {
        /// <summary>
        /// Crop the DHM to the desired dimensions.
        /// </summary>
        /// <param name="dhmData">The input point coordinates and heights as DhmData</param>
        /// <param name="xMinCoord">The X-Coordinate (E) of the left lower boundary</param>
        /// <param name="xMaxCoord">The X-Coordinate (E) of the right upper boundary</param>
        /// <param name="yMinCoord">The Y-Coordinate (N) of the left lower boundary</param>
        /// <param name="yMaxCoord">The Y-Coordinate (N) of the right upper boundary</param>
        /// <returns>The cropped point coordinates and heights as DhmData</returns>
        public static DhmData CropDhm(DhmData dhmData,
            decimal xMinCoord, decimal xMaxCoord, decimal yMinCoord, decimal yMaxCoord)
        {
            var cropedDhm = new DhmData();

            // loop the X-Coordinates
            foreach (var xCoordionate in dhmData.Values)
            {
                var xValue = xCoordionate.Key;

                // check if inside boundaries (X-Coordinate)
                if (xValue >= xMinCoord && xValue <= xMaxCoord)
                {
                    // loop the Y-Coordinates
                    foreach (var yZCoordinate in xCoordionate.Value)
                    {
                        var yValue = yZCoordinate.Key;


                        // check if inside boundaries (Y-Coordinate)
                        if (yValue >= yMinCoord && yValue <= yMaxCoord)
                        {
                            // add the the coordinate pair to the result
                            cropedDhm.Add(xCoordionate.Key, yZCoordinate.Key, yZCoordinate.Value);
                        }
                    }
                }
            }

            return cropedDhm;
        }
    }
}