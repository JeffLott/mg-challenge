using Mg.Challenge.Core.Services;
using System;

namespace Mg.Challenge.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileAccessor = new FileAccessor();
            var parsingService = new FileParsingService();
            var writer = new JsonFileWriter();

            Console.WriteLine("Please enter the full path to the CSV file:");
            var inputPath = Console.ReadLine();
            Console.WriteLine("Please enter the output path for the JSON file:");
            var outputPath = Console.ReadLine();

            var lines = fileAccessor.ReadFile(inputPath);

            if (lines == null)
            {
                Console.WriteLine($"Was not able to read {inputPath}");
                return;
            }

            var dto = parsingService.Parse(lines);

            if(dto == null)
            {
                Console.WriteLine($"Error parsing file: {inputPath}");
                return;
            }

            var wasSuccessful = writer.WriteFileAsJson(outputPath, dto);

            if (wasSuccessful)
                Console.WriteLine($"Successfully wrote the JSON to {outputPath}");
            else
                Console.WriteLine($"Failed to write the JSON to {outputPath}");

            Console.ReadLine();
        }
    }
}
