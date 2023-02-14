using System;
using System.Globalization;
using System.IO;

namespace GEOBOX.OSC.DHM.Tools.Common
{
    /// <summary>
    /// Reads DHM files from a folder.
    /// </summary>
    public class DhmFileReader
    {
        private const int COORDINATELENGTH = 10;
        private const int YCOORDINATESTART = 11;
        private const int HEIGHTLENGTH = 6;
        private const int HEIGHTSTART = 22;
        private const int LINELENGTH = 30;
        private const int LINECONTENTLENGTH = 28;

        /// <summary>
        /// Read all DHM files from a folder.
        /// </summary>
        /// <param name="folderPath">Path of the folder conatining the DHM files</param>
        /// <param name="fileExtension">Extension of the DHM files (default: *.xyz)</param>
        /// <returns>The point coordinates and heights from the files as DhmData</returns>
        public static DhmData ReadFromFolder(string folderPath, string fileExtension = "*.xyz")
        {
            var dhmData = new DhmData();
            
            // check if the folder exists
            if (!Directory.Exists(folderPath)) return dhmData;

            // declare possible file header 
            string header = $"X Y Z{Environment.NewLine}";
            int headerLength = header.Length;

            // get and sort all files with the specified extension from the folder
            var files = Directory.GetFiles(folderPath, fileExtension);
            Array.Sort(files);

            // loop the files
            foreach (var file in files)
            {
                using (StreamReader stream = File.OpenText(file))
                {
                    // read file into span
                    ReadOnlySpan<char> fileContentAsSpan = stream.ReadToEnd().AsSpan();
                    int contentLenght = fileContentAsSpan.Length;
                    if (contentLenght < headerLength) continue;

                    // declare index where every line start position is hold while looping the lines
                    int lineStartPosition = 0;

                    decimal xCoordinate;
                    decimal yCoordinate;
                    decimal zCoordinate;

                    // check if file contains a header 
                    if (fileContentAsSpan.Slice(0, headerLength).ToString().Equals(header, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // skip the header (set lineStartPosition to next line)
                        lineStartPosition = headerLength;
                    }

                    // loop the lines
                    while (true)
                    {
                        // get and convert the coordinates from the line
                        xCoordinate = decimal.Parse(fileContentAsSpan.Slice(lineStartPosition, COORDINATELENGTH), provider: NumberFormatInfo.InvariantInfo);
                        yCoordinate = decimal.Parse(fileContentAsSpan.Slice(lineStartPosition + YCOORDINATESTART, COORDINATELENGTH), provider: NumberFormatInfo.InvariantInfo);
                        zCoordinate = decimal.Parse(fileContentAsSpan.Slice(lineStartPosition + HEIGHTSTART, HEIGHTLENGTH), provider: NumberFormatInfo.InvariantInfo);

                        // add the coordinates to the result
                        dhmData.Add(xCoordinate, yCoordinate, zCoordinate);

                        // set lineStartPosition to next line
                        lineStartPosition += LINELENGTH;

                        // check if there is a next line
                        if (lineStartPosition + LINECONTENTLENGTH > contentLenght)
                        {
                            break;
                        }
                    }
                }
            }

            return dhmData;
        }
    }
}
