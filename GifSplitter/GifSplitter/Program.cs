using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
namespace GifSplitter
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			Console.WriteLine ("2");
			List<byte[]> images = GifSplitter.EnumerateFrames ("/Users/indigoorton/Dropbox/Engineer/Mocks/!App/Sandbox App/Assets/!iOS Specific/Spinner/hexagon_loader.gif");

			Console.WriteLine ("FINISHED");

			Console.WriteLine ("Split path into: " + images.Count.ToString() + " images");

			const string outputPath = "/Users/indigoorton/Documents/OutputGifs/hexagon-loading-image";
			for (int index = 0; index < images.Count; index++) {

				Bitmap image = GifSplitter.ConvertBytesToImage (images[index]);

				Bitmap croppedImage = ImageCropper.cropImage (image, new Rectangle (145, 107, 110, 110));

				Color gray = Color.FromArgb (179, 179, 179);
				Color white = Color.FromArgb (255, 255, 255);

				ImageCropper.makeImageTransparent (ref croppedImage, white, gray, white);

				croppedImage.Save (outputPath + index + ".png", ImageFormat.Png);

			}
		}
	}
}
