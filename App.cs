﻿// See https://aka.ms/new-console-template for more information

using Serilog;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ColorShapes
{
    public class App
    {
        private const string INPUT_CSV = "ColorShapes.csv";
        private const string OUTPUT_CSV = "Output.csv";

        internal async Task RunAsync()
        {
            try
            {
                var path = Directory.GetCurrentDirectory();
                var inputfilePath = Path.Combine(path, INPUT_CSV);
                var stopWatch = Stopwatch.StartNew();

                var colorShapes = await CSVHelper.GetCSV(inputfilePath);
                if (colorShapes != null && colorShapes.Any())
                {
                    Log.Information("Colorshapes CSV loaded in memory");
                    var orderedColorShapes = colorShapes.EquidistantOrderByColor();

                    var outputFilePath = Path.Combine(path, OUTPUT_CSV);
                    CSVHelper.WriteOutPutCsv(outputFilePath, orderedColorShapes);

                    stopWatch.Stop();
                    Log.Information("Console ran for {0}.", stopWatch.Elapsed);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }

}