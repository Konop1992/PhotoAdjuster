using System;
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
                g.DrawImage(ScaleImage(img1), 0, 0);        // Top-left
                g.DrawImage(ScaleImage(img2), 4000, 0);     // Top-right
                g.DrawImage(ScaleImage(img3), 0, 3000);     // Bottom-left
                g.DrawImage(ScaleImage(img4), 4000, 3000);  // Bottom-right
            }

            return finalImage;
        }

        private Bitmap ScaleImage(Bitmap originalImage)
        {
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

                // Calculate the position to center the image
                int offsetX = (4000 - newWidth) / 2;
                int offsetY = (3000 - newHeight) / 2;

                // Clear the background to white
                g.Clear(Color.White);
                g.DrawImage(originalImage, offsetX, offsetY, newWidth, newHeight);
            }
            return scaledImage;
        }
    }

}
