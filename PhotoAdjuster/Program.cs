// See https://aka.ms/new-console-template for more information
using PhotoAdjuster;
using System.Drawing;

if (args.Length < 2)
{
    ShowHelp();
    Console.ReadKey(); // Wait for any key
    return;
}

string inputFolder = args[0];
string outputFolder = args[1];

if(!Path.Exists(inputFolder) || !Path.Exists(outputFolder))
{
    Console.WriteLine($"Input Folder({inputFolder}) or Output Folder doesn't exist.");
    Console.ReadKey(); // Wait for any key
    return;
}

// Your application logic here
Console.WriteLine($"Input Folder: {inputFolder}");
Console.WriteLine($"Output Folder: {outputFolder}");

// Ensure the output folder exists
Directory.CreateDirectory(outputFolder);

ImageCombinerProcessor processor = new ImageCombinerProcessor(inputFolder, outputFolder);
processor.ProcessImages();

Console.WriteLine($"Processing finished. Press any key to finish.");
Console.ReadKey(); // Wait for any key
return;

static void ShowHelp()
{
    Console.WriteLine("Usage: dotnet run <inputFolder> <outputFolder>");
    Console.WriteLine("Provide the paths to the input and output folders.");
    Console.WriteLine("Example: dotnet run C:\\path\\to\\input C:\\path\\to\\output");
}