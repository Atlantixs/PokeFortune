using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace PokeFortune.Core.Helpers
{
	public enum ImageFormatType
	{
		Png,
		Jpeg,
		Bmp
	}

	public static class MediaHelper
	{
		public static string ConvertToBase64String(byte[] img)
		{
			if (img == null)
				return string.Empty;

			var dataType = "image";

			return $"data:{dataType};base64, {Convert.ToBase64String(img)}";
		}

		#region ToImage

		public static Image ConvertToImage(Uri uri)
		{
			var img = new BitmapImage(uri);
			return ConvertToImage(img);
		}

		public static Image ConvertToImage(BitmapImage image)
		{
			var bytes = ConvertToBytes(image);
			return ConvertToImage(bytes);
		}

		public static Image ConvertToImage(byte[] img)
		{
			return Image.FromStream(new MemoryStream(img));
		}

		#endregion ToImage

		#region ToBitmapImage

		public static BitmapImage ConvertToBitmapImage(Image img)
		{
			var bytes = ConvertToBytes(img);
			return ConvertToBitmapImage(bytes);
		}

		public static BitmapImage ConvertToBitmapImage(byte[] img)
		{
			BitmapImage bi = new BitmapImage();
			bi.BeginInit();
			bi.StreamSource = new MemoryStream(img);
			bi.EndInit();
			bi.Freeze();

			return bi;
		}

		#endregion ToBitmapImage

		#region ToBytes

		public static byte[] ConvertToBytes(Uri uri)
		{
			var img = new BitmapImage(uri);
			return ConvertToBytes(img);
		}

		public static byte[] ConvertToBytes(Image image)
		{
			ImageConverter conv = new ImageConverter();
			return (byte[])conv.ConvertTo(image, typeof(byte[]));
		}

		public static byte[] ConvertToBytes(BitmapImage image)
		{
			byte[] data;
			var encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(image));

			using (MemoryStream ms = new MemoryStream())
			{
				encoder.Save(ms);
				data = ms.ToArray();
			}
			return data;
		}

		#endregion ToBytes

		/// <summary>
		/// Skaliert ein Bild zu der angeben Größen, Format und Qualität
		/// </summary>
		/// <param name="path"> Path to which the image would be saved. </param>
		/// <param name="quality"> An integer from 0 to 100, with 100 being the highest quality. </param>
		public static byte[] ScaleImage(byte[] img, int maxWidth, int maxHeight, ImageFormatType format = ImageFormatType.Jpeg, int quality = 100)
		{
			var picture = new Picture(maxWidth, maxHeight, img);

			using (var ms = new MemoryStream(picture.Data))
			{
				var image = Image.FromStream(ms, true) as Bitmap;

				var newImg = SetFormatAndQuality(GetImageFormat(format), image, quality);

				return newImg;
			}
		}

		/// <summary>
		/// Saves an image as a jpeg image, with the given quality
		/// </summary>
		/// <param name="path"> Path to which the image would be saved. </param>
		/// <param name="quality"> An integer from 0 to 100, with 100 being the highest quality. </param>
		private static byte[] SetFormatAndQuality(ImageFormat format, Bitmap img, int quality)
		{
			if (quality < 0 || quality > 100)
				throw new ArgumentOutOfRangeException($"{nameof(quality)} must be between 0 and 100.");

			if (format == null)
				throw new ArgumentNullException($"{nameof(format)} bei der Konvertierung ist null");

			var formatString = string.Empty;

			if (format == ImageFormat.Jpeg)
				formatString = "jpeg";
			else if (format == ImageFormat.Png)
				formatString = "png";
			else if (format == ImageFormat.Bmp)
				formatString = "bmp";
			else
				return Array.Empty<byte>();

			// Encoder parameter for image quality
			EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
			// image codec
			ImageCodecInfo codec = GetEncoderInfo($"image/{formatString}");
			EncoderParameters encoderParams = new EncoderParameters(1);
			encoderParams.Param[0] = qualityParam;

			byte[] byteArray = null;

			using (var ms = new MemoryStream())
			{
				img.Save(ms, codec, encoderParams);
				byteArray = ms.ToArray();
			}

			return byteArray;
		}

		/// <summary>
		/// Returns the image codec with the given mime type
		/// </summary>
		private static ImageCodecInfo GetEncoderInfo(string mimeType)
		{
			// Get image codecs for all image formats
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

			// Find the correct image codec
			for (int i = 0; i < codecs.Length; i++)
				if (codecs[i].MimeType == mimeType)
					return codecs[i];

			return null;
		}

		private static ImageFormat GetImageFormat(ImageFormatType type)
		{
			switch (type)
			{
				case ImageFormatType.Png:
					return ImageFormat.Png;

				case ImageFormatType.Jpeg:
					return ImageFormat.Jpeg;

				case ImageFormatType.Bmp:
					return ImageFormat.Bmp;

				default:
					return null;
			}
		}

		public static Bitmap MergeTwoImages(Image firstImage, Image secondImage, int width, int height)
		{
			if (firstImage == null)
			{
				throw new ArgumentNullException("firstImage");
			}

			if (secondImage == null)
			{
				throw new ArgumentNullException("secondImage");
			}

			int outputImageWidth = firstImage.Width;
			int outputImageHeight = firstImage.Height;

			var startWidth = width + ((94 - secondImage.Width) / 2);
			var startHeight = height + ((94 - secondImage.Height) / 2);

			Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, PixelFormat.Format32bppArgb);

			using (Graphics graphics = Graphics.FromImage(outputImage))
			{
				graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size),
						new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
				graphics.DrawImage(secondImage, new Rectangle(new Point(startWidth, startHeight), secondImage.Size),
						new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
			}

			return outputImage;
		}
	}

	internal class Picture
	{
		private readonly int _maxWidth;
		private readonly int _maxHeight;

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="maxWidth">Maximale erlaubte Breite, bevor skalliert wird</param>
		/// <param name="maxHeight">Maximal erlaubte Höhe, bevor skalliert wird</param>
		/// <param name="origData">Bild als byte-Array</param>
		public Picture(int maxWidth, int maxHeight, byte[] origData)
		{
			_maxWidth = maxWidth;
			_maxHeight = maxHeight;
			TryScale(origData);
		}

		#region properties

		/// <summary>
		/// Gib an, ob der Bildbearbeitungsprozess gültig war und mit den Daten weitergearbeitet werden kann
		/// </summary>
		public bool IsValid { get; private set; } = true;

		/// <summary>
		/// Gibt an, ob es zu einer Skallierung gekommen ist
		/// </summary>
		public bool IsScaled { get; private set; } = false;

		/// <summary>
		/// Maximal erlaubte Breite
		/// </summary>
		public int MaxWidth => _maxWidth;

		/// <summary>
		/// Maximal erlaubte Höhe
		/// </summary>
		public int MaxHeight => _maxHeight;

		private byte[] _data;
		public byte[] Data => _data;
		/// <summary>
		/// Breite des Bildes
		/// </summary>
		public int Width { get; private set; } = 0;

		/// <summary>
		/// Höhe des Bildes
		/// </summary>
		public int Height { get; private set; } = 0;

		#endregion properties

		#region methods

		/// <summary>
		/// Sollte die Breite/Höhe des Bilder größer als die Zielvorgabe sein, wird das Bild autom. auf die passende Größe skaliert
		/// </summary>
		private void TryScale(byte[] input)
		{
			try
			{
				if (input == null || input.Length <= 0)
				{
					IsValid = false;
					return;
				}

				//mStream.Write(input, 0, Convert.ToInt32(input.Length));
				var imgConverter = new ImageConverter();

				var ms = new MemoryStream(input);
				using (var bm = Image.FromStream(ms, true) as Bitmap)
				{
					ms.Dispose();

					if (bm.Width <= _maxWidth && bm.Height <= _maxHeight)
					{
						Width = bm.Width;
						Height = bm.Height;
						_data = input;
						return;
					}

					float scale = System.Math.Min(_maxWidth / (float)bm.Width, _maxHeight / (float)bm.Height);
					var scaleWidth = bm.Width * scale;
					var scaleHeight = bm.Height * scale;
					using (var newBm = new System.Drawing.Bitmap((int)scaleWidth, (int)scaleHeight))
					{
						using (var graph = System.Drawing.Graphics.FromImage(newBm))
						{
							graph.InterpolationMode = InterpolationMode.HighQualityBilinear;
							graph.FillRectangle(System.Drawing.Brushes.Transparent, new System.Drawing.RectangleF(0, 0, (int)scaleWidth, (int)scaleHeight));
							graph.DrawImage(bm, 0, 0, scaleWidth, scaleHeight);

							var converter = new System.Drawing.ImageConverter();
							Width = newBm.Width;
							Height = newBm.Height;
							_data = (byte[])converter.ConvertTo(newBm, typeof(byte[]));
						}
					}
				}

				IsScaled = true;
				IsValid = true;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				IsValid = false;
			}
		}

		#endregion methods
	}
}
