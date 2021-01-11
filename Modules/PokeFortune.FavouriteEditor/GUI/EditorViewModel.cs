using Microsoft.Win32;
using PokeFortune.Core.Mvvm;
using PokeFortune.FavouriteEditor.Managers;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PokeFortune.FavouriteEditor.GUI
{
	public class EditorViewModel : RegionViewModelBase
	{
		public EditorViewModel(IRegionManager regionManager) : base(regionManager)
		{
			var time = Stopwatch.StartNew();
			TemplateManager = new TemplateManager();
			time.Stop();
			Debug.WriteLine(time.Elapsed);


			NextRowCmd = new DelegateCommand(TemplateManager.NextRow);
			PrevRowCmd = new DelegateCommand(TemplateManager.PreviousRow);
			NextColumnCmd = new DelegateCommand(TemplateManager.NextColumn);
			PrevColumnCmd = new DelegateCommand(TemplateManager.PreviousColumn);

			SaveImageCmd = new DelegateCommand(OnSaveImage);
			ResetImageCmd = new DelegateCommand(TemplateManager.ResetTable);
		}

		public TemplateManager TemplateManager { get; }

		public ICommand NextRowCmd { get; init; }
		public ICommand PrevRowCmd { get; init; }
		public ICommand NextColumnCmd { get; init; }
		public ICommand PrevColumnCmd { get; init; }

		public ICommand SaveImageCmd { get; init; }
		public ICommand ResetImageCmd { get; init; }


		private void OnSaveImage()
		{
			FileStream fs;
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.Filter = "Png (*.png)|*.png|JPeg (*.jpeg)|*.jpeg|Bitmap (*.bmp)|*.bmp";
			saveFileDialog.Title = "Speichern"; //Properties.Resources._str_SaveAnImage;

			if (saveFileDialog.ShowDialog() == true)//DialogResult.OK)
			{
				if ((fs = (FileStream)saveFileDialog.OpenFile()) != null)
				{
					//var bmp = new WriteableBitmap(TemplateManager.CurrentImage);

					//bmp.WriteTga(fs);

					ImageFormat imageFormat = null;
					BitmapEncoder encoder = null;

					switch (saveFileDialog.FilterIndex)
					{
						case 1:
							imageFormat = ImageFormat.Png;
							encoder = new PngBitmapEncoder();
							break;

						case 2:
							imageFormat = ImageFormat.Jpeg;
							encoder = new JpegBitmapEncoder();
							break;

						case 3:
							imageFormat = ImageFormat.Bmp;
							encoder = new BmpBitmapEncoder();
							break;
					}

					if (imageFormat != null && encoder != null)
					{
						encoder.Frames.Add(BitmapFrame.Create(TemplateManager.CurrentImage));
						encoder.Save(fs);
					}

					//fs.Write(bmp.ToByteArray(), 0, bmp.ToByteArray().Length);

					fs.Close();
				}
			}
		}
	}
}