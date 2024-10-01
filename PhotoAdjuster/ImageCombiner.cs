using System.Drawing;

namespace PhotoAdjuster
{
    public class ImageCombiner
    {
        public Bitmap CombineImages(Bitmap img1, Bitmap img2, Bitmap img3, Bitmap img4)
        {
            // Create a new bitmap with the size of 8000x6000
            Bitmap finalImage = new Bitmap(8000, 6000);
            using (Graphics g = Graphics.FromImage(finalImage))
            {
                // Set the interpolation mode for better quality
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Scale and draw each image
                g.DrawImage(ScaleImage(img1, 4000, 3000), 0, 0);        // Top-left (0, 0) to (4000, 3000), aligned to (4000, 3000)
                g.DrawImage(ScaleImage(img2, 0, 3000), 4000, 0);     // Top-right (4000, 0) to (8000, 3000), aligned to (0, 3000)
                g.DrawImage(ScaleImage(img3, 4000, 0), 0, 3000);     // Bottom-left (0, 3000) to (4000, 6000), aligned to (4000, 0)
                g.DrawImage(ScaleImage(img4, 0, 0), 4000, 3000);  // Bottom-right (4000, 3000) to (8000, 6000), aligned to (0, 0)
            }

            return finalImage;
        }

        private Bitmap ScaleImage(Bitmap originalImage, int alignHorizontal, int alignVertical)
        {
            // Check if the image is vertical
            if (originalImage.Height > originalImage.Width)
            {
                // Rotate the image to make it horizontal
                originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            // Create a new bitmap with the desired size
            Bitmap scaledImage = new Bitmap(4000, 3000);
            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                // Set the interpolation mode for better quality
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Calculate the scaling factor
                float scale = Math.Min(4000f / originalImage.Width, 3000f / originalImage.Height);
                int newWidth = (int)(originalImage.Width * scale);
                int newHeight = (int)(originalImage.Height * scale);

                // Calculate the offsets based on alignment parameters
                int offsetX = alignHorizontal - (newWidth / 2); // Centering based on width
                int offsetY = alignVertical - (newHeight / 2);   // Centering based on height

                // Ensure offsets do not go out of bounds
                offsetX = Math.Max(0, Math.Min(4000 - newWidth, offsetX));
                offsetY = Math.Max(0, Math.Min(3000 - newHeight, offsetY));

                // Clear the background to white
                g.Clear(Color.White);
                // Draw the image without offsets that might cause gaps
                g.DrawImage(originalImage, offsetX, offsetY, newWidth, newHeight);
            }
            return scaledImage;
        }
    }
}
