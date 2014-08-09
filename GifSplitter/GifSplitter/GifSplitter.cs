using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Reflection;
using System.IO;
using System.Drawing.Imaging;

namespace GifSplitter
{
	public static class GifSplitter
	{

		public static List<byte[]> EnumerateFrames(string imagePath)
		{
			try
			{
				Console.WriteLine("Hello a");
				//Make sure the image exists
				if (!File.Exists(imagePath))
				{
					throw new FileNotFoundException("Unable to locate " + imagePath);
				}

				Dictionary<Guid, ImageFormat> guidToImageFormatMap = new Dictionary<Guid, ImageFormat>()
				{
					{ImageFormat.Bmp.Guid,  ImageFormat.Bmp},
					{ImageFormat.Gif.Guid,  ImageFormat.Png},
					{ImageFormat.Icon.Guid, ImageFormat.Png},
					{ImageFormat.Jpeg.Guid, ImageFormat.Jpeg},
					{ImageFormat.Png.Guid,  ImageFormat.Png}
				};

				List<byte[]> tmpFrames = new List<byte[]>() { };
				Console.WriteLine("Hello b");
				using (Image img = Image.FromFile(imagePath, true))
				{
					//Check the image format to determine what
					//format the image will be saved to the 
					//memory stream in
					ImageFormat imageFormat = null;
					Guid imageGuid = img.RawFormat.Guid;
					Console.WriteLine("Hello c");
					foreach (KeyValuePair<Guid, ImageFormat> pair in guidToImageFormatMap)
					{
						if (imageGuid == pair.Key)
						{
							imageFormat = pair.Value;
							break;
						}
					}

					if (imageFormat == null)
					{
						throw new NoNullAllowedException("Unable to determine image format");
					}

					//Get the frame count
					FrameDimension dimension = new FrameDimension(img.FrameDimensionsList[0]);
					int frameCount = img.GetFrameCount(dimension);

					//Step through each frame
					for (int i = 0; i < frameCount; i++)
					{
						//Set the active frame of the image and then 
						//write the bytes to the tmpFrames array
						img.SelectActiveFrame(dimension, i);
						using (MemoryStream ms = new MemoryStream())
						{

							img.Save(ms, imageFormat);
							tmpFrames.Add(ms.ToArray());
						}
					}

				}

				return tmpFrames;

			}
			catch (Exception ex)
			{
				Console.WriteLine("Hello d");
				System.Console.WriteLine ("ERROR: " + ex.ToString ());
			} catch {
				System.Console.WriteLine ("CAUGHT UNKNOWN");
			} finally {
				Console.WriteLine ("Hello e");
			}

			return null;
		}

		public static  Bitmap ConvertBytesToImage(byte[] imageBytes)
		{
			if (imageBytes == null || imageBytes.Length == 0)
			{
				return null;
			}

			try
			{
				//Read bytes into a MemoryStream
				using (MemoryStream ms = new MemoryStream(imageBytes))
				{
					//Recreate the frame from the MemoryStream
					using (Bitmap bmp = new Bitmap(ms))
					{
						return (Bitmap)bmp.Clone();
					}
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine ("ERROR: " + ex.ToString ());
			}

			return null;
		}
	}
}

