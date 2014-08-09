using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;


namespace GifSplitter
{
	public static class ImageCropper
	{
		public static void makeImageTransparent(ref Bitmap inputBitmap, Color backgroundColor, Color currentFill, Color newFill, bool useNewFill) {			
			for (int x = 0; x < inputBitmap.Width; x++) {
				for (int y = 0; y < inputBitmap.Height; y++) {
					if (inputBitmap.GetPixel (x, y) != backgroundColor && inputBitmap.GetPixel (x, y) != currentFill) {
						inputBitmap.SetPixel (x, y, backgroundColor);
					}
				}
			}
			inputBitmap.MakeTransparent (backgroundColor);

			if (useNewFill) {
				for (int x = 0; x < inputBitmap.Width; x++) {
					for (int y = 0; y < inputBitmap.Height; y++) {
						if (inputBitmap.GetPixel (x, y).A != 0) {
							inputBitmap.SetPixel (x, y, newFill);
						}
					}
				}
			}
		}

		public static Bitmap cropImage(Bitmap inputBitmap, Rectangle cropRect) {
			Bitmap output = new Bitmap (cropRect.Width, cropRect.Height);

			using (Graphics grapher = Graphics.FromImage (output)) {
				grapher.DrawImage (inputBitmap, new RectangleF (0, 0, output.Width, output.Height), cropRect, GraphicsUnit.Pixel);
			}

			return output;
		}
	}
}

