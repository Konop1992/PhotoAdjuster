using System.Drawing;

namespace PhotoAdjuster
{
    public class ImageCombinerProcessor
    {
        private readonly string inputFolder;
        private readonly string outputFolder;

        public ImageCombinerProcessor(string inputFolder, string outputFolder)
        {
            this.inputFolder = inputFolder;
            this.outputFolder = outputFolder;
        }

        public void ProcessImages()
        {
            // Gather all images from the input folder
            var imageFiles = Directory.GetFiles(inputFolder, "*.*")
                                       .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                                      file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                                      file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                                       .ToList();

            int totalImages = imageFiles.Count;

            Console.WriteLine($"Processing {totalImages} images.");

            // Create the image combiner
            ImageCombiner combiner = new ImageCombiner();

            for (int i = 0; i < totalImages; i += 4)
            {
                Console.WriteLine($"Processing {i}/{totalImages}");

                Bitmap img1 = (Bitmap)Image.FromFile(imageFiles[i]);
                Bitmap img2 = (i + 1) < totalImages ? (Bitmap)Image.FromFile(imageFiles[i + 1]) : CreateWhiteImage();
                Bitmap img3 = (i + 2) < totalImages ? (Bitmap)Image.FromFile(imageFiles[i + 2]) : CreateWhiteImage();
                Bitmap img4 = (i + 3) < totalImages ? (Bitmap)Image.FromFile(imageFiles[i + 3]) : CreateWhiteImage();

                Bitmap combinedImage = combiner.CombineImages(img1, img2, img3, img4);

                var outputPath = Path.Combine(outputFolder, $"combined_{i / 4}.jpg");
                int counter = 1;
                while (Path.Exists(outputPath))
                {
                    outputPath = Path.Combine(outputFolder, $"combined_{i / 4}_{counter}.jpg");
                    counter++;
                }
                combinedImage.Save(outputPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Cleanup
                img1.Dispose();
                img2.Dispose();
                img3.Dispose();
                img4.Dispose();
                combinedImage.Dispose();
            }
        }

        private Bitmap CreateWhiteImage()
        {
            // Create a white image of size 4000x3000
            Bitmap whiteImage = new Bitmap(4000, 3000);
            using (Graphics g = Graphics.FromImage(whiteImage))
            {
                g.Clear(Color.White);
            }
            return whiteImage;
        }
    }
}
