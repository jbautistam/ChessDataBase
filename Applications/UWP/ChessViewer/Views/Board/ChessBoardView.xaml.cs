using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using ChessViewer.Controllers.Extensors;
using ChessViewer.Views.Board.Models;
using Bau.Libraries.LibChessGame.ViewModels.Board;
using Bau.Libraries.LibChessGame.ViewModels;
using Bau.Libraries.LibChessGame.ViewModels.Board.Scapes;
using Bau.Libraries.LibChessGame.ViewModels.Board.Actions;
using System.Collections.Generic;
using Bau.Libraries.LibChessGame.Models.Pieces;

namespace ChessViewer.Views.Board
{
	/// <summary>
	///		Control para mostrar las figuras del tablero
	/// </summary>
	public sealed partial class ChessBoardView : UserControl
	{
		// Constantes privadas
		private const float BoardTop = 20;
		private const float BoardLeft = 20;
		private const int LabelWidth = (int) BoardLeft;
		private const int LabelHeight = 30;
		private const double AnimationTime = 1;
		// Variables privadas
		private Compositor _compositor;
		private ContainerVisual _root;
		private PieceImagesListModel _pieces;

		public ChessBoardView()
		{
			InitializeComponent();
		}

		/// <summary>
		///		Inicializa el viewModel
		/// </summary>
		public void Init(GameBoardViewModel gameBoardViewModel)
		{
			// Inicializa el viewModel (antes de inicializar el control)
			ViewModel = gameBoardViewModel;
			// Inicializa el control
			InitControl();
			// Inicializa los eventos del ViewModel (después de inicializar el control)
			ViewModel.MainViewModel.ComboPathBoard.PropertyChanged += ComboPathImages_PropertyChanged;
			ViewModel.MainViewModel.ComboPathPieces.PropertyChanged += ComboPathImages_PropertyChanged;
			ViewModel.ResetGame += (sender, eventArgs) => Reset();
			ViewModel.ShowMovements += (sender, eventArgs) => ShowMovements(eventArgs.Actions, eventArgs.ShowAnimation);
		}

		/// <summary>
		///		Inicializa el control
		/// </summary>
		private void InitControl()
		{
			// Inicializa los datos necesarios para composición
			_root = udtCanvas.GetVisual();
			_compositor = _root.Compositor;
			// y lo pinta
			Reset();
		}

		/// <summary>
		///		Inicializa el tablero
		/// </summary>
		private void Reset()
		{
			if (_root != null && _compositor != null)
			{
				// Inicializa las piezas
				_pieces = new PieceImagesListModel(_compositor, ViewModel.MainViewModel.ComboPathBoard.SelectedPath, ViewModel.MainViewModel.ComboPathPieces.SelectedPath);
				// Limpia el canvas y la memoria
				udtCanvas.Children.Clear();
				_root.Children.RemoveAll();
				_pieces.Clear();
				// Ajusta el tamaño
				UpdateChoords();
				// Añade las etiquetas
				foreach (ScapeBaseViewModel scape in ViewModel.Scapes.Scapes)
					if (scape is LabelViewModel label)
						CreateLabel(label);
				// Añade las celadas
				foreach (ScapeBaseViewModel scape in ViewModel.Scapes.Scapes)
					if (scape is CellViewModel cell)
						CreateCell(cell);
				// Añade las piezas
				foreach (ScapeBaseViewModel scape in ViewModel.Scapes.Scapes)
					if (scape is FigureViewModel figure)
						CreatePiece(figure);
				// Pinta el tablero
				PaintBoard();
			}
		}

		/// <summary>
		///		Pinta el tablero
		/// </summary>
		private void PaintBoard()
		{
			if (_pieces != null) // ... en la carga se hace un resize antes de crear las piezas
			{
				// Modifica las coordenads
				UpdateChoords();
				// Pinta las etiquetas
				foreach (UIElement element in udtCanvas.Children)
					if (element is TextBlock textBlock)
						ShowLabel(textBlock);
				// Pinta las piezas
				foreach (PieceImageModel piece in _pieces)
					ShowPiece(piece);
			}
		}

		/// <summary>
		///		Muestra una serie de movimientos
		/// </summary>
		private void ShowMovements(List<ActionBaseModel> actions, bool showAnimation)
		{
			// Actualiza las coordenadas
			UpdateChoords();
			// Si hay algún movimiento que mostrar
			if (actions.Count > 0)
			{
				// El reset debe ser la primera acción
				if (actions[0] is ActionResetBoardModel)
					Reset();
				// Muestra las animaciones o ejecuta las acciones
				ExecuteActions(actions, showAnimation);
			}
		}

		/// <summary>
		///		Ejecuta una serie de acciones sobre la lista de figuras
		/// </summary>
		private void ExecuteActions(List<ActionBaseModel> actions, bool showAnimation)
		{
			// Ajusta la variable de "mostrar animación" incluyendo el checkbox de la ventana
			showAnimation &= ViewModel.MainViewModel.MustShowAnimation;
			// Ejecuta las acciones
			foreach (ActionBaseModel baseAction in actions)
				if (!(baseAction is ActionResetBoardModel))
					switch (baseAction)
					{
						case ActionMoveModel action:
								MovePiece(GetPieceType(action.Type), action.Color, action.FromRow, action.FromColumn, action.ToRow, action.ToColumn, showAnimation);
							break;
						case ActionPromoteModel action:
								CreatePiece(new FigureViewModel(action.ToRow, action.ToColumn, action.Type, action.Color));
							break;
						case ActionCaptureModel action:
								KillPiece(GetPieceType(action.Type), action.Color, action.TargetRow, action.TargetColumn, showAnimation);
							break;
					}
			// Indica que ya se puede mover
			ViewModel.IsMoving = false;
		}

		/// <summary>
		///		Modifica los límites
		/// </summary>
		private void UpdateChoords()
		{
			CellWidth = (float) (ActualWidth - BoardLeft * 2) / 8;
			CellHeight = (float) (ActualHeight - BoardTop * 2) / 8;
		}

		/// <summary>
		///		Crea una etiqueta
		/// </summary>
		private void CreateLabel(LabelViewModel label)
		{
			udtCanvas.Children.Add(new TextBlock 
										{ 
											Text = label.Text.ToString(), 
											HorizontalTextAlignment = TextAlignment.Center,
											FontSize = 16, 
											FontWeight = Windows.UI.Text.FontWeights.Bold,
											Tag = new Vector2(label.Row, label.Column)
										}
									);
		}

		/// <summary>
		///		Crea una figura
		/// </summary>
		private void CreatePiece(FigureViewModel figure)
		{
			PaintPiece(GetPieceType(figure.Type), figure.Color, figure.Row, figure.Column);
		}

		/// <summary>
		///		Obtiene el tipo de pieza
		/// </summary>
		private PieceImageModel.GraphicTypes GetPieceType(PieceBaseModel.PieceType type)
		{
			switch (type)
			{
				case PieceBaseModel.PieceType.Pawn:
					return PieceImageModel.GraphicTypes.Pawn;
				case PieceBaseModel.PieceType.Rook:
					return PieceImageModel.GraphicTypes.Rook;
				case PieceBaseModel.PieceType.Knight:
					return PieceImageModel.GraphicTypes.Knight;
				case PieceBaseModel.PieceType.Bishop:
					return PieceImageModel.GraphicTypes.Bishop;
				case PieceBaseModel.PieceType.King:
					return PieceImageModel.GraphicTypes.King;
				case PieceBaseModel.PieceType.Queen:
					return PieceImageModel.GraphicTypes.Queen;
				default:
					return PieceImageModel.GraphicTypes.Unknown;
			}
		}

		/// <summary>
		///		Crea las celdas
		/// </summary>
		private void CreateCell(CellViewModel cell)
		{
			PaintPiece(PieceImageModel.GraphicTypes.Cell, cell.Color, cell.Row, cell.Column);
		}

		/// <summary>
		///		Pinta una pieza
		/// </summary>
		private void PaintPiece(PieceImageModel.GraphicTypes type, PieceBaseModel.PieceColor color, int row, int column)
		{
			PieceImageModel piece = _pieces.Search(type, color, row, column);

				// Si no se ha encontrado la pieza se añade a los elementos
				if (piece == null)
				{
					// Crea el sprite
					piece = _pieces.Add(type, color, row, column);
					// y lo inserta en el contenedor
					_root.Children.InsertAtTop(piece.Sprite);
				}
				// Ajusta las coordenadas de la celda
				ShowPiece(piece);
		}

		/// <summary>
		///		Mueve una pieza
		/// </summary>
		private void MovePiece(PieceImageModel.GraphicTypes type, PieceBaseModel.PieceColor color, 
							   int rowSource, int columnSource, int rowTarget, int columnTarget,
							   bool showAnimation)
		{
			PieceImageModel piece = _pieces.Search(type, color, rowSource, columnSource);

				// Si se ha encontrado la pieza, la mueve
				if (piece != null)
				{
					// Cambia la posición en la colección
					piece.Row = rowTarget;
					piece.Column = columnTarget;
					// Añade la animación del movimiento
					if (showAnimation)
						AnimateMovement(piece, rowTarget, columnTarget);
					else
						ShowPiece(piece);
				}
		}

		/// <summary>
		///		Elimina una pieza
		/// </summary>
		private void KillPiece(PieceImageModel.GraphicTypes type, PieceBaseModel.PieceColor color, 
							   int rowSource, int columnSource, bool showAnimation)
		{
			PieceImageModel piece = _pieces.Search(type, color, rowSource, columnSource);

				// Si se ha encontrado la pieza, la mueve
				if (piece != null)
				{
					// Cambia la posición en la colección
					piece.Row = -1;
					piece.Column = -1;
					// Añade la animación del movimiento
					if (showAnimation)
						AnimateDestroy(piece);
					else
						_root.Children.Remove(piece.Sprite);
				}
		}

		/// <summary>
		///		Crea la animación de un movimiento
		/// </summary>
		private void AnimateMovement(PieceImageModel piece, int row, int column)
		{ 
			Vector3KeyFrameAnimation animation = _compositor.CreateVector3KeyFrameAnimation();

				// Crea los parámetros de animación
				animation.InsertKeyFrame(1f, GetOffset(row, column));
				animation.Duration = TimeSpan.FromSeconds(AnimationTime);
				animation.Direction = AnimationDirection.Normal;
				// Ejecuta la animación
				piece.Sprite.StartAnimation(nameof(piece.Sprite.Offset), animation);
		} 

		/// <summary>
		///		Crea la animación de destruir una pieza
		/// </summary>
		private void AnimateDestroy(PieceImageModel piece)
		{ 
			ScalarKeyFrameAnimation animation = _compositor.CreateScalarKeyFrameAnimation();

				// Crea los parámetros de animación
				animation.InsertKeyFrame(1f, 0);
				animation.Duration = TimeSpan.FromSeconds(AnimationTime);
				animation.Direction = AnimationDirection.Normal;
				// Ejecuta la animación
				piece.Sprite.StartAnimation(nameof(piece.Sprite.Opacity), animation);
		}

		/// <summary>
		///		Obtiene el offset de una pieza
		/// </summary>
		private void ShowPiece(PieceImageModel piece)
		{
			piece.Sprite.Offset = GetOffset(piece.Row, piece.Column);
			piece.Sprite.Size = new Vector2(CellWidth, CellHeight);
		}

		/// <summary>
		///		Obtiene el offset de una fila / columna
		/// </summary>
		private Vector3 GetOffset(int row, int column)
		{
			return new Vector3(BoardLeft + column * CellWidth, BoardTop + row * CellHeight, 0);
		}

		/// <summary>
		///		Cambia el offset de un control
		/// </summary>
		private void ShowLabel(TextBlock label)
		{
			Vector2 position = (Vector2) label.Tag;

				// Coloca las etiquetas
				if (position.X >= 0) // ... cabeceras de fila
				{
					Canvas.SetTop(label, BoardTop + (CellHeight - LabelHeight) / 2 + CellHeight * position.X);
					Canvas.SetLeft(label, 0);
				}
				else // ... cabeceras de columna
				{
					Canvas.SetTop(label, 0);
					Canvas.SetLeft(label, BoardLeft + (CellWidth - LabelWidth) / 2 + CellWidth * position.Y);
				}
				// Asigna el ancho
				label.Width = LabelWidth;
				label.Height = LabelHeight;
		}

		/// <summary>
		///		Ancho de las celdas
		/// </summary>
		private float CellWidth { get; set; }

		/// <summary>
		///		Ancho de las celdas
		/// </summary>
		private float CellHeight { get; set; }

		/// <summary>
		///		ViewModel
		/// </summary>
		public GameBoardViewModel ViewModel { get; private set; }

		private void UdtCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			PaintBoard();
		}

		private void ComboPathImages_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals(nameof(PathComboImagesViewModel.SelectedPath), StringComparison.CurrentCultureIgnoreCase))
				ViewModel.Reset();
		}
	}
}
