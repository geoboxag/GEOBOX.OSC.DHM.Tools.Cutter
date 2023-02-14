using GEOBOX.OSC.DHM.Tools.Common;
using GEOBOX.OSC.DHM.Tools.Cutter;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

Settings settings;

var asseblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var jsonPath = Path.Combine(asseblyPath, "Settings.json");

// read the settings
try
{
    var jsonString = File.ReadAllText(jsonPath);
    settings = JsonSerializer.Deserialize<Settings>(jsonString);
}
catch(Exception ex)
{
    Console.WriteLine($"Fehler beim Einlesen der Einstellungs-Datei ({jsonPath}):");
    Console.WriteLine(ex.Message);

    return;
}

Console.WriteLine($"DHM wird mit folgenden Einstellungen zugeschnitten:");
Console.WriteLine($"  Quell-Ordner:\t\t{settings.SourceFolder}");
Console.WriteLine($"  Ziel-Datei:\t\t{settings.DestinationFile}");
Console.WriteLine($"  Untere Begrenzung:\t{settings.XMinCoord} / {settings.YMinCoord}");
Console.WriteLine($"  Obere Begrenzung:\t{settings.XMaxCoord} / {settings.YMaxCoord}");
Console.WriteLine($"  Halbierungs-Faktor:\t{settings.HalfingFactor} ({Math.Pow(2, settings.HalfingFactor)}x kleiner)");
Console.WriteLine();

// read the dhm from all files in folder
Console.WriteLine($"DHM-Dateien werden eingelesen ({settings.SourceFolder}\\*.xyz)...");
var dhmData = DhmFileReader.ReadFromFolder(settings.SourceFolder);

// crop the dhm to selection
Console.WriteLine("DHM wird zugeschnitten...");
var fittedDhm = DhmCropingHelper.CropDhm(dhmData, settings.XMinCoord, settings.XMaxCoord, settings.YMinCoord, settings.YMaxCoord);

// reduce dhm resolution
var reducedFittedDhm = fittedDhm;
if (settings.HalfingFactor > 0)
{
    Console.WriteLine("DHM Auflösung wird reduziert...");
    reducedFittedDhm = DhmReducer.HalfeByFactor(fittedDhm, settings.HalfingFactor);
}

// write the dhm to file
Console.WriteLine($"DHM-Datei wird geschrieben...");
DhmFileWriter.Write(settings.DestinationFile, reducedFittedDhm);

Console.WriteLine($"DHM-Datei erfolgreich erstellt.");
Console.WriteLine();
Console.WriteLine("Beenden mit beliebige Taste...");

Console.ReadLine();