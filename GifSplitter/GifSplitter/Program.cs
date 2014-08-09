using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

using System.Windows.Media.Imaging;

namespace GifSplitter
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			Console.WriteLine ("2");
            List<byte[]> images = GifHandler.EnumerateFrames("C:\\Users\\Indigo\\Desktop\\hexagon_loader.gif");

			Console.WriteLine ("FINISHED");

			Console.WriteLine ("Split path into: " + images.Count.ToString() + " images");

            const string outputPath = "C:\\Users\\Indigo\\Desktop\\hexagon-loading-image-gray";

            bool saveToPNG = true;

            List<Bitmap> bitmaps = new List<Bitmap>();


			for (int index = 0; index < images.Count; index++) {

				Bitmap image = GifHandler.ConvertBytesToImage (images[index]);

				Bitmap croppedImage = ImageCropper.cropImage (image, new Rectangle (145, 107, 110, 110));

				Color gray = Color.FromArgb (179, 179, 179);
				Color white = Color.FromArgb (255, 255, 255);
                if (saveToPNG)
                {
				    ImageCropper.makeImageTransparent (ref croppedImage, white, gray, Color.Gray, true);

                //if (saveToPNG) {
				    croppedImage.Save (outputPath + index + ".png", ImageFormat.Png);
                } else {
                    bitmaps.Add(croppedImage);
                }
			}

            if (!saveToPNG)
            {
                GifBitmapEncoder gifEncoded = GifHandler.CombineFrames(bitmaps);

                using (FileStream targetFile = new FileStream(@"C:\Users\Indigo\Desktop\hexagon_loader_output.gif", FileMode.Create))
                {
                    gifEncoded.Save(targetFile);
                }
            }
		}
	}
}
