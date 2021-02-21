using PokeFortune.Core;
using PokeFortune.Core.Collections;
using PokeFortune.Core.Extensions;
using PokeFortune.Core.Helpers;
using PokeFortune.Core.Properties;
using PokeFortune.Data;
using PokeFortune.Data.Enums;
using PokeFortune.FavouriteEditor.Enums;
using PokeFortune.FavouriteEditor.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace PokeFortune.FavouriteEditor.Managers
{
	public interface ITemplateManager
	{
		List<RowType> GenerationRows { get; }
		RowType SelectedRow { get; set; }

		List<ColumnType> PokemonColumns { get; }
		ColumnType SelectedColumn { get; set; }

		FortuneCollection<PokeCell> PokemonCollection { get; }
		PokeCell SelectedPokemon { get; set; }
		BitmapSource CurrentImage { get; }

		bool ShinyMode { get; set; }

		public void NextColumn();
		public void PreviousColumn();
		public void NextRow();
		public void PreviousRow();

		public void SaveTable();
		public void ResetTable();
	}

	public class TemplateManager : BindableBase, ITemplateManager
	{
		private readonly List<PokeCell> _allPokes = new List<PokeCell>();

		private readonly List<(RowType row, ColumnType col, PokeCell poke)> _selectedList = new List<(RowType row, ColumnType col, PokeCell poke)>();

		public TemplateManager()
		{
			GenerationRows.AddRange(Enum.GetValues(typeof(RowType)).Cast<RowType>());
			PokemonColumns.AddRange(Enum.GetValues(typeof(ColumnType)).Cast<ColumnType>());

			_selectedRow = GenerationRows.FirstOrDefault();
			_selectedColumn = PokemonColumns.FirstOrDefault();


			foreach (var poke in PokemonList.Pokemons)
			{
				//var colTypes = new List<ColumnType>();

				//if (poke.LegendaryType != LegendaryType.None)
				//	colTypes.Add(ColumnType.Legendary);
				//else
				//{
				//	colTypes.Add(GetColumnTypeByPokemonType(poke.FirstPokemonType));

				//	if (poke.SecondPokemonType != PokemonType.None)
				//		colTypes.Add(GetColumnTypeByPokemonType(poke.SecondPokemonType));
				//}

				//var rowType = GetRowTypeByGenerationType(poke.GenerationType);

				//foreach (var col in colTypes)
				_allPokes.Add(new PokeCell(poke));

				if (poke.HasFemale)
					_allPokes.Add(new PokeCell(poke.Female));

				foreach (var form in poke.Forms)
				{
					//if (poke.LegendaryType == LegendaryType.None)
					//{
					//	colTypes.Clear();
					//	colTypes.Add(GetColumnTypeByPokemonType(form.FirstPokemonType));

					//	if (poke.SecondPokemonType != PokemonType.None)
					//		colTypes.Add(GetColumnTypeByPokemonType(form.SecondPokemonType));
					//}

					//switch (form.FormType)
					//{
					//	case FormType.Other:
					//	case FormType.Regional:
					//		rowType = GetRowTypeByGenerationType(form.GenerationType);
					//		break;
					//	case FormType.Mega:
					//	case FormType.Primal:
					//	case FormType.Gigantamax:
					//		rowType = RowType.Transformation;
					//		break;
					//}

					//foreach (var col in colTypes)
					_allPokes.Add(new PokeCell(form));
				}
			}

			UpdatePokemonCollection();
			UpdateTable();
		}

		private ColumnType GetColumnTypeByPokemonType(PokemonType type) => type switch
		{
			PokemonType.Normal => ColumnType.Normal,
			PokemonType.Grass => ColumnType.Grass,
			PokemonType.Fire => ColumnType.Fire,
			PokemonType.Water => ColumnType.Water,
			PokemonType.Flying => ColumnType.Flying,
			PokemonType.Electric => ColumnType.Electric,
			PokemonType.Bug => ColumnType.Bug,
			PokemonType.Poison => ColumnType.Poison,
			PokemonType.Rock => ColumnType.Rock,
			PokemonType.Ground => ColumnType.Ground,
			PokemonType.Fighting => ColumnType.Fighting,
			PokemonType.Psychic => ColumnType.Psychic,
			PokemonType.Ghost => ColumnType.Ghost,
			PokemonType.Ice => ColumnType.Ice,
			PokemonType.Dragon => ColumnType.Dragon,
			PokemonType.Dark => ColumnType.Dark,
			PokemonType.Steel => ColumnType.Steel,
			PokemonType.Fairy => ColumnType.Fairy,
			_ => 0
		};

		private RowType GetRowTypeByGenerationType(GenerationType type) => type switch
		{
			GenerationType.FirstGen => RowType.FirstGen,
			GenerationType.SecondGen => RowType.SecondGen,
			GenerationType.ThirdGen => RowType.ThirdGen,
			GenerationType.FourthGen => RowType.FourthGen,
			GenerationType.FifthGen => RowType.FifthGen,
			GenerationType.SixthGen => RowType.SixthGen,
			GenerationType.SeventhGen => RowType.SeventhGen,
			GenerationType.EighthGen => RowType.EighthGen,
			_ => 0
		};

		#region Selection

		public List<RowType> GenerationRows { get; } = new List<RowType>();

		private RowType _selectedRow;
		public RowType SelectedRow
		{
			get => _selectedRow;
			set
			{
				if (SetProperty(ref _selectedRow, value))
					UpdatePokemonCollection();
			}
		}

		public List<ColumnType> PokemonColumns { get; } = new List<ColumnType>();

		private ColumnType _selectedColumn;
		public ColumnType SelectedColumn
		{
			get => _selectedColumn;
			set
			{
				if (SetProperty(ref _selectedColumn, value))
					UpdatePokemonCollection();
			}
		}

		public FortuneCollection<PokeCell> PokemonCollection { get; } = new FortuneCollection<PokeCell>();

		public PokeCell SelectedPokemon
		{
			get => _selectedList.FirstOrDefault(x => x.row == SelectedRow && x.col == SelectedColumn).poke;
			set
			{
				if (value != null)
				{
					UpdateSelectedPokemon(value);
					RaisePropertyChanged();
				}
			}
		}

		private bool _shinyMode = false;
		public bool ShinyMode
		{
			get => _shinyMode;
			set
			{
				if (SetProperty(ref _shinyMode, value))
					UpdateTable();
			}
		}

		private void UpdatePokemonCollection()
		{
			_ = UpdatePokemonCollectionAsync();
		}

		private async Task UpdatePokemonCollectionAsync()
		{
			await new TaskFactory().StartNew(() =>
			{
				PokemonCollection.Clear();

				PokemonCollection.Add(new PokeCell(PokemonList.MissingNo));

				if (SelectedRow != RowType.Favourite && SelectedColumn != ColumnType.Favourite)
				{
					var toAdd = new List<PokeCell>(_allPokes);

					if (SelectedColumn == ColumnType.Legendary)
					{
						toAdd.RemoveAll(x => !x.IsLegendary);
					}
					else
					{
						PokemonType pokeType = SelectedColumn switch
						{
							ColumnType.Normal => PokemonType.Normal,
							ColumnType.Grass => PokemonType.Grass,
							ColumnType.Fire => PokemonType.Fire,
							ColumnType.Water => PokemonType.Water,
							ColumnType.Flying => PokemonType.Flying,
							ColumnType.Electric => PokemonType.Electric,
							ColumnType.Bug => PokemonType.Bug,
							ColumnType.Poison => PokemonType.Poison,
							ColumnType.Rock => PokemonType.Rock,
							ColumnType.Ground => PokemonType.Ground,
							ColumnType.Fighting => PokemonType.Fighting,
							ColumnType.Psychic => PokemonType.Psychic,
							ColumnType.Ghost => PokemonType.Ghost,
							ColumnType.Ice => PokemonType.Ice,
							ColumnType.Dragon => PokemonType.Dragon,
							ColumnType.Dark => PokemonType.Dark,
							ColumnType.Steel => PokemonType.Steel,
							ColumnType.Fairy => PokemonType.Fairy
						};

						toAdd.RemoveAll(x => x.IsLegendary || !x.Pokemon.HasPokemonType(pokeType));
					}

					if (SelectedRow == RowType.Transformation)
					{
						toAdd.RemoveAll(x => !x.IsTransformation);
					}
					else
					{
						GenerationType genType = SelectedRow switch
						{
							RowType.FirstGen => GenerationType.FirstGen,
							RowType.SecondGen => GenerationType.SecondGen,
							RowType.ThirdGen => GenerationType.ThirdGen,
							RowType.FourthGen => GenerationType.FourthGen,
							RowType.FifthGen => GenerationType.FifthGen,
							RowType.SixthGen => GenerationType.SixthGen,
							RowType.SeventhGen => GenerationType.SeventhGen,
							RowType.EighthGen => GenerationType.EighthGen
						};

						toAdd.RemoveAll(x => x.IsTransformation || x.Pokemon.GenerationType != genType);
					}

					PokemonCollection.AddRange(toAdd);
				}
				else if (SelectedRow != RowType.Favourite && SelectedColumn == ColumnType.Favourite)
					PokemonCollection.AddRange(_selectedList.Where(x => x.row == SelectedRow && x.col != ColumnType.Favourite).Select(x => x.poke));
				else if (SelectedRow == RowType.Favourite && SelectedColumn != ColumnType.Favourite)
					PokemonCollection.AddRange(_selectedList.Where(x => x.row != RowType.Favourite && x.col == SelectedColumn).Select(x => x.poke));
				else
					PokemonCollection.AddRange(_selectedList.Where(x => x.row == RowType.Favourite || x.col == ColumnType.Favourite).Select(x => x.poke));
			});
		}

		private void UpdateSelectedPokemon(PokeCell selectedPoke)
		{
			if (_selectedList.RemoveAll(x => x.row == SelectedRow && x.col == SelectedColumn) > 0)
				ResetField(SelectedRow, SelectedColumn);

			if (_selectedList.RemoveAll(x => x.row == SelectedRow && x.col == ColumnType.Favourite) > 0)
				ResetField(SelectedRow, ColumnType.Favourite);

			if (_selectedList.RemoveAll(x => x.row == RowType.Favourite && x.col == SelectedColumn) > 0)
				ResetField(RowType.Favourite, SelectedColumn);

			if (_selectedList.RemoveAll(x => x.row == RowType.Favourite && x.col == ColumnType.Favourite) > 0)
				ResetField(RowType.Favourite, ColumnType.Favourite);

			_selectedList.Add((SelectedRow, SelectedColumn, selectedPoke));

			AddCurrentPokemonToTable(selectedPoke);
			NextColumn();
		}

		public void NextColumn()
		{
			var nextCol = PokemonColumns.Next(SelectedColumn);
			_selectedColumn = nextCol;

			if (nextCol == PokemonColumns.FirstOrDefault())
				NextRow();
			else
				UpdatePokemonCollection();

			RaisePropertyChanged(nameof(SelectedColumn));
		}

		public void PreviousColumn()
		{
			var prevCol = PokemonColumns.Previous(SelectedColumn);
			_selectedColumn = prevCol;

			if (prevCol == PokemonColumns.LastOrDefault())
				PreviousRow();
			else
				UpdatePokemonCollection();

			RaisePropertyChanged(nameof(SelectedColumn));
		}

		public void NextRow()
		{
			var nextRow = GenerationRows.Next(SelectedRow);
			SelectedRow = nextRow;
		}

		public void PreviousRow()
		{
			var prevRow = GenerationRows.Previous(SelectedRow);
			SelectedRow = prevRow;
		}

		#endregion

		#region Image

		public int StartWidth => 99;
		public int StartHeight => 50;

		public int BoxWidth => 96;
		public int BoxHeight => 96;

		public int VerticalBorderThickness => 2;
		public int HorizontalBorderThickness => 2;


		private Bitmap _currentTemplate;
		private BitmapSource _currentImage = new BitmapImage(CoreIconPool.FAVOURITE_TABLE);
		public BitmapSource CurrentImage
		{
			get => _currentImage;
			set => SetProperty(ref _currentImage, value);
		}

		private void UpdateTable()
		{
			_currentTemplate = (Bitmap)MediaHelper.ConvertToImage(CoreIconPool.FAVOURITE_TABLE);

			var height = StartHeight;
			foreach (var row in GenerationRows)
			{
				var width = StartWidth;
				foreach (var col in PokemonColumns)
				{
					var poke = _selectedList.FirstOrDefault(x => x.row == row && x.col == col).poke;

					if (poke != null)
					{
						var pictureBytes = MediaHelper.ConvertToBytes(ShinyMode ? poke.Pokemon.ShinySprite : poke.Pokemon.Sprite);
						var scaledPicture = MediaHelper.ScaleImage(pictureBytes, BoxWidth, BoxHeight, ImageFormatType.Png);
						var picture = MediaHelper.ConvertToImage(scaledPicture);

						var drawWidth = width + ((BoxWidth - picture.Width) / 2);
						var drawHeight = height + ((BoxHeight - picture.Height) / 2);

						using (Graphics graphics = Graphics.FromImage(_currentTemplate))
						{
							graphics.DrawImage(picture, new Rectangle(new Point(drawWidth, drawHeight), picture.Size),
									new Rectangle(new Point(), picture.Size), GraphicsUnit.Pixel);
						}
					}

					width += BoxWidth + VerticalBorderThickness;
				}

				height += BoxHeight + HorizontalBorderThickness;
			}

			CurrentImage = MediaHelper.ConvertToBitmapImage(_currentTemplate);
		}

		private void AddCurrentPokemonToTable(PokeCell poke)
		{
			if (poke == null)
				return;

			var t = Path.GetFileName(poke.Pokemon.Sprite.AbsoluteUri);

			var width = StartWidth + (((int)SelectedColumn) * (BoxWidth + VerticalBorderThickness));
			var height = StartHeight + (((int)SelectedRow) * (BoxHeight + HorizontalBorderThickness));

			var pictureBytes = MediaHelper.ConvertToBytes(ShinyMode ? poke.Pokemon.ShinySprite : poke.Pokemon.Sprite);
			var scaledPicture = MediaHelper.ScaleImage(pictureBytes, BoxWidth, BoxHeight, ImageFormatType.Png);
			var pokePicture = MediaHelper.ConvertToImage(scaledPicture);

			var drawWidth = width + ((BoxWidth - pokePicture.Width) / 2);
			var drawHeight = height + ((BoxHeight - pokePicture.Height) / 2);

			using (Graphics graphics = Graphics.FromImage(_currentTemplate))
			{
				graphics.DrawImage(pokePicture, new Rectangle(new System.Drawing.Point(drawWidth, drawHeight), pokePicture.Size),
						new Rectangle(new System.Drawing.Point(), pokePicture.Size), GraphicsUnit.Pixel);
			}

			CurrentImage = MediaHelper.ConvertToBitmapImage(_currentTemplate);
		}

		private void ResetField(RowType rowType, ColumnType colType, bool updateTable = false)
		{
			var width = StartWidth + (((int)colType) * (BoxWidth + VerticalBorderThickness));
			var height = StartHeight + (((int)rowType) * (BoxHeight + HorizontalBorderThickness));

			var fieldPicture = MediaHelper.ConvertToImage(colType switch
			{
				ColumnType.Normal => CoreIconPool.NORMAL_96,
				ColumnType.Grass => CoreIconPool.GRASS_96,
				ColumnType.Fire => CoreIconPool.FIRE_96,
				ColumnType.Water => CoreIconPool.WATER_96,
				ColumnType.Flying => CoreIconPool.FLYING_96,
				ColumnType.Electric => CoreIconPool.ELECTRIC_96,
				ColumnType.Bug => CoreIconPool.BUG_96,
				ColumnType.Poison => CoreIconPool.POISON_96,
				ColumnType.Rock => CoreIconPool.ROCK_96,
				ColumnType.Ground => CoreIconPool.GROUND_96,
				ColumnType.Fighting => CoreIconPool.FIGHTING_96,
				ColumnType.Psychic => CoreIconPool.PSYCHIC_96,
				ColumnType.Ghost => CoreIconPool.GHOST_96,
				ColumnType.Ice => CoreIconPool.ICE_96,
				ColumnType.Dragon => CoreIconPool.DRAGON_96,
				ColumnType.Dark => CoreIconPool.DARK_96,
				ColumnType.Steel => CoreIconPool.STEEL_96,
				ColumnType.Fairy => CoreIconPool.FAIRY_96,
				ColumnType.Legendary => CoreIconPool.LEGENDARY_96,
				ColumnType.Favourite => CoreIconPool.FAVOURITE_96,
				_ => throw new NotImplementedException(),
			});

			using (Graphics graphics = Graphics.FromImage(_currentTemplate))
			{
				graphics.DrawImage(fieldPicture, new Rectangle(new System.Drawing.Point(width, height), fieldPicture.Size),
						new Rectangle(new System.Drawing.Point(), fieldPicture.Size), GraphicsUnit.Pixel);
			}

			if (updateTable)
				CurrentImage = MediaHelper.ConvertToBitmapImage(_currentTemplate);
		}

		public void SaveTable()
		{
			FileStream fs;
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.Filter = "Png (*.png)|*.png|JPeg (*.jpeg)|*.jpeg|Bitmap (*.bmp)|*.bmp";
			saveFileDialog.Title = Resources.Save;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				if ((fs = (FileStream)saveFileDialog.OpenFile()) != null)
				{
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
						encoder.Frames.Add(BitmapFrame.Create(CurrentImage));
						encoder.Save(fs);
					}

					fs.Close();
				}
			}
		}

		public void ResetTable()
		{
			_selectedList.Clear();

			SelectedRow = GenerationRows.FirstOrDefault();
			SelectedColumn = PokemonColumns.FirstOrDefault();

			_currentTemplate = (Bitmap)MediaHelper.ConvertToImage(CoreIconPool.FAVOURITE_TABLE);
			CurrentImage = new BitmapImage(CoreIconPool.FAVOURITE_TABLE);
		}

		#endregion
	}
}