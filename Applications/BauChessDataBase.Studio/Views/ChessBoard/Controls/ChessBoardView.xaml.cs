﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

using Bau.Libraries.LibChessGame.Models.Pieces;
using Bau.Libraries.LibChessGame.ViewModels.Board;
using Bau.Libraries.LibChessGame.ViewModels.Board.Actions;
using Bau.Libraries.LibChessGame.ViewModels.Board.Scapes;

namespace Bau.ChessDataBase.Studio.Views.ChessBoard.Controls
{
	/// <summary>
	///		Control para mostrar un tablero
	/// </summary>
	public partial class ChessBoardView : UserControl
	{
		// Constantes privadas
		private const int LabelWidth = 30;
		private const int LabelHeight = 30;
		private const double AnimationTime = 0.25;

		public ChessBoardView()
		{
			InitializeComponent();
		}

		/// <summary>
		///		Inicializa el tablero
		/// </summary>
		public void Init(GameBoardViewModel gameBoardViewModel)
		{
			ViewModel = gameBoardViewModel;
			ViewModel.ResetGame += (sender, eventArgs) => Reset();
			ViewModel.ShowMovements += (sender, eventArgs) => ShowMovements(eventArgs.Actions, eventArgs.DisableAnimations);
		}

		/// <summary>
		///		Inicializa el tablero
		/// </summary>
		private void Reset()
		{
			// Limpia el canvas
			udtCanvas.Children.Clear();
			// Añade las etiquetas
			foreach (ScapeBaseViewModel scape in ViewModel.Scapes.Scapes)
				if (scape is LabelViewModel label)
					udtCanvas.Children.Add(CreateLabel(label));
			// Añade las celadas
			foreach (ScapeBaseViewModel scape in ViewModel.Scapes.Scapes)
				if (scape is CellViewModel cell)
					udtCanvas.Children.Add(CreateCell(cell));
			// Añade las piezas
			foreach (ScapeBaseViewModel scape in ViewModel.Scapes.Scapes)
				if (scape is FigureViewModel figure)
					udtCanvas.Children.Add(CreateFigure(figure));
			// Muestra las imágenes
			ShowImages();
		}

		/// <summary>
		///		Crea una etiqueta
		/// </summary>
		private Control CreateLabel(LabelViewModel scape)
		{
			return new Label 
						{ 
							Content = scape.Text.ToString(), 
							HorizontalContentAlignment = HorizontalAlignment.Center,
							VerticalContentAlignment = VerticalAlignment.Center,
							FontSize = 16, 
							FontWeight = FontWeights.Bold,
							Tag = scape
						};
		}

		/// <summary>
		///		Crea una celda del tablero
		/// </summary>
		private FrameworkElement CreateCell(CellViewModel cell)
		{
			Image image = CreateImage(GetImageFileName(cell), cell.Row, cell.Column);

				// Asigna el tag
				image.Tag = cell;
				// Devuelve el control
				return image;
		}

		/// <summary>
		///		Crea el control para una figura
		/// </summary>
		private FrameworkElement CreateFigure(FigureViewModel figure)
		{
			Image image = CreateImage(GetImageFileName(figure), figure.Row, figure.Column);

				// Asigna el tag
				image.Tag = figure;
				// Devuelve el control
				return image;
		}

		/// <summary>
		///		Crea una imagen y la añade al canvas
		/// </summary>
		private Image CreateImage(string fileName, int row, int column)
		{
			Image view = new Image();
			int width = GetImageWidth();
			int height = GetImageHeight();

				// Carga la imagen
				view.Source = LoadImage(fileName);
				view.Stretch = Stretch.Fill;
				// Asigna las propiedades de visualización
				Canvas.SetTop(view, GetImageTop(row));
				Canvas.SetLeft(view, GetImageLeft(column));
				// Asigna el tamaños
				if (width > 16 && height > 16)
				{
					view.Width = GetImageWidth();
					view.Height = GetImageHeight();
				}
				// Devuelve el control
				return view;
		}

		/// <summary>
		///		Obtiene el nombre de archivo de imagen a cargar
		/// </summary>
		private string GetImageFileName(ScapeBaseViewModel scape)
		{
			// Dependiendo del tipo de imagen
			switch (scape)
			{
				case CellViewModel cell:
					return System.IO.Path.Combine(ViewModel.ChessGameMainViewModel.PathBoardImages, cell.FileImage);
				case FigureViewModel figure:
					return System.IO.Path.Combine(ViewModel.ChessGameMainViewModel.PathPiecesImages, figure.FileImage);
			}
			// Si ha llegado hasta aquí es porque el escaque no pertenecía a ningún dato válido
			return string.Empty;
		}

		/// <summary>
		///		Carga una imagen
		/// </summary>
		private BitmapImage LoadImage(string fileName)
		{
			BitmapImage image = new BitmapImage();

				// Inicializa la carga de imagen
				image.BeginInit();
				// Carga la imagen
				if (!string.IsNullOrEmpty(fileName) && System.IO.File.Exists(fileName))
					image.UriSource = new Uri(fileName, UriKind.Relative);
				else
					image.UriSource = new Uri($"pack://application:,,,/Resources/ChessBoard/{System.IO.Path.GetFileName(fileName)}", UriKind.Absolute);
				// Finaliza la carga de la imagen
				image.CacheOption = BitmapCacheOption.OnLoad; // ... carga la imagen en este momento, no la deja en caché
				image.EndInit();
				// Devuelve la imagen cargada
				return image;
		}

		/// <summary>
		///		Obtiene el siguiente color
		/// </summary>
		private PieceBaseModel.PieceColor GetNextColor(PieceBaseModel.PieceColor color)
		{
			return color == PieceBaseModel.PieceColor.White ? PieceBaseModel.PieceColor.Black : PieceBaseModel.PieceColor.White;
		}

		/// <summary>
		///		Muestra las imágenes
		/// </summary>
		private void ShowImages()
		{
			int width = GetImageWidth();
			int height = GetImageHeight();

				if (width >= 16 && height >= 16)
					foreach (FrameworkElement cell in udtCanvas.Children)
						if (cell.Tag != null && cell.Tag is ScapeBaseViewModel scapeViewModel && scapeViewModel != null)
							switch (cell)
							{
								case Image image:
										Canvas.SetTop(image, LabelHeight + height * scapeViewModel.Row);
										Canvas.SetLeft(image, LabelWidth + width * scapeViewModel.Column);
										image.Width = width;
										image.Height = height;
										if (scapeViewModel is CellViewModel)
											Canvas.SetZIndex(image, 0);
										else
											Canvas.SetZIndex(image, 1);
									break;
								case Label label:
										if (scapeViewModel.Column == -1) // ... cabeceras de fila
										{
											Canvas.SetTop(label, LabelHeight + (height - LabelHeight) / 2 + height * scapeViewModel.Row);
											Canvas.SetLeft(label, 0);
										}
										else // ... cabeceras de columna
										{
											Canvas.SetTop(label, 0);
											Canvas.SetLeft(label, LabelWidth +  (width - LabelWidth) / 2 + width * scapeViewModel.Column);
										}
										label.Width = LabelWidth;
										label.Height = LabelHeight;
									break;
							}
		}

		/// <summary>
		///		Obtiene el ancho de la imagen
		/// </summary>
		private int GetImageWidth()
		{
			return (int) (ActualWidth - LabelWidth) / 8;
		}

		/// <summary>
		///		Obtiene el alto de la imagen
		/// </summary>
		private int GetImageHeight()
		{
			return (int) (ActualHeight - LabelHeight) / 8;
		}

		/// <summary>
		///		Obtiene la posición Y de una imagen a partir de su fila
		/// </summary>
		private double GetImageTop(int row)
		{
			return LabelHeight + GetImageHeight() * row;
		}

		/// <summary>
		///		Obtiene la posición X de una imagen a partir de su fila
		/// </summary>
		private double GetImageLeft(int column)
		{
			return LabelWidth + GetImageWidth() * column;
		}

		/// <summary>
		///		Muestra una serie de movimientos
		/// </summary>
		private void ShowMovements(List<ActionBaseModel> actions, bool disableAnimations)
		{
			if (actions.Count > 0)
			{
				// El reset debe ser la primera acción
				if (actions[0] is ActionResetBoardModel)
					Reset();
				// Muestra las animaciones o ejecuta las acciones
				if (!disableAnimations && MainWindow.MainController.ConfigurationController.ViewModel.ShowAnimations)
				{ 
					Storyboard storyBoard = CreateAnimations(actions);

						// ... y las muestra
						if (storyBoard.Children.Count > 0)
						{
							// Asigna el evento de fin de animación
							storyBoard.Completed += (sender, evntArgs) => ExecuteActions(actions);
							// Comienza la animación
							storyBoard.Begin();
						}
						else
							ExecuteActions(actions);
				}
				else // Ejecuta directamente las acciones sobre el tablero
					ExecuteActions(actions);
			}
		}

		/// <summary>
		///		Crea las animaciones
		/// </summary>
		private Storyboard CreateAnimations(List<ActionBaseModel> actions)
		{
			Storyboard storyBoard  = new Storyboard();

				// Limpia el storyBoard
				storyBoard.Children.Clear();
				// Asigna las propiedades de duración
				storyBoard.BeginTime = TimeSpan.FromSeconds(0);
				storyBoard.Duration = new Duration(TimeSpan.FromSeconds(AnimationTime));
				// Crea las animaciones
				foreach (ActionBaseModel baseAction in actions)
					switch (baseAction)
					{
						case ActionMoveModel action:
								Image image = SearchPiece(action.Type, action.Color, action.FromRow, action.FromColumn);

									if (image != null && image.Tag != null && image.Tag is FigureViewModel)
									{
										AddAnimationToStoryBoard(storyBoard, image,
																 CreateDoubleAnimation(GetImageTop(action.FromRow), GetImageTop(action.ToRow)),
																 0, AnimationTime, new PropertyPath(Canvas.TopProperty));
										AddAnimationToStoryBoard(storyBoard, image,
																 CreateDoubleAnimation(GetImageLeft(action.FromColumn), GetImageLeft(action.ToColumn)),
																 0, AnimationTime, new PropertyPath(Canvas.LeftProperty));
									}
									else
										System.Diagnostics.Debug.WriteLine("Error when execute movement");
							break;
						case ActionCaptureModel action:
								Image imageCaptured = SearchPiece(action.Type, action.Color, action.TargetRow, action.TargetColumn);

									if (imageCaptured != null && imageCaptured.Tag != null && imageCaptured.Tag is FigureViewModel)
										AddAnimationToStoryBoard(storyBoard, imageCaptured,
																 CreateDoubleAnimation(1, 0),
																 AnimationTime / 2, AnimationTime, new PropertyPath(Image.OpacityProperty));
									else
										System.Diagnostics.Debug.WriteLine("Error when execute movement");
						break;
					}
				// Devuelve el storyBoard
				return storyBoard;
		}

		/// <summary>
		///		Crea una animación Double sobre un control
		/// </summary>
		private DoubleAnimation CreateDoubleAnimation(double from, double to)
		{
			DoubleAnimation animation = new DoubleAnimation();

				// Asigna las propiedades
				animation.From = from;
				animation.To = to;
				// Añade la animación al storyBoard
				return animation;
		}

		/// <summary>
		///		Añade una animación al storyBoard
		/// </summary>
		private void AddAnimationToStoryBoard(Storyboard storyBoard, DependencyObject control, AnimationTimeline animation, 
											  double startSeconds, double endSeconds, PropertyPath propertyPath)
		{
			// Asigna las propiedades de inicio y duración
			animation.BeginTime = TimeSpan.FromSeconds(startSeconds);
			animation.Duration = TimeSpan.FromSeconds(endSeconds);
			// Añade los datos a la animación
			Storyboard.SetTarget(animation, control);
			Storyboard.SetTargetProperty(animation, propertyPath);
			// Añade la animación al storyboard
			storyBoard.Children.Add(animation);
		}

		/// <summary>
		///		Ejecuta una serie de acciones sobre la lista de figuras
		/// </summary>
		private void ExecuteActions(List<ActionBaseModel> actions)
		{
			// Ejecuta las acciones
			foreach (ActionBaseModel baseAction in actions)
				if (!(baseAction is ActionResetBoardModel))
					switch (baseAction)
					{
						case ActionMoveModel action:
								Image image = SearchPiece(action.Type, action.Color, action.FromRow, action.FromColumn);

									if (image != null && image.Tag is FigureViewModel figure)
									{
										figure.Row = action.ToRow;
										figure.Column = action.ToColumn;
									}
									else
										System.Diagnostics.Debug.WriteLine("Error when execute movement");
							break;
						case ActionPromoteModel action:
								udtCanvas.Children.Add(CreateFigure(new FigureViewModel(action.ToRow, action.ToColumn, action.Type, action.Color)));
							break;
						case ActionCaptureModel action:
								Image imageCaptured = SearchPiece(action.Type, action.Color, action.TargetRow, action.TargetColumn);

									if (imageCaptured != null && imageCaptured.Tag is FigureViewModel)
										udtCanvas.Children.Remove(imageCaptured);
									else
										System.Diagnostics.Debug.WriteLine("Error when execute movement");
							break;
					}
			// Muestra las imágenes
			ShowImages();
			// Indica que ya se puede mover
			ViewModel.IsMoving = false;
		}

		/// <summary>
		///		Busca una pieza
		/// </summary>
		private Image SearchPiece(PieceBaseModel.PieceType type, PieceBaseModel.PieceColor color, int row, int column)
		{
			// Busca la pieza
			foreach (FrameworkElement control in udtCanvas.Children)
				if (control is Image image && image.Tag != null && image.Tag is FigureViewModel figure &&
						figure.Type == type && figure.Color == color && figure.Row == row && figure.Column == column)
					return image;
			// Devuelve una pieza vacía
			return null;
		}

		/// <summary>
		///		ViewModel
		/// </summary>
		public GameBoardViewModel ViewModel { get; private set; }

		private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ShowImages();
		}
	}
}
