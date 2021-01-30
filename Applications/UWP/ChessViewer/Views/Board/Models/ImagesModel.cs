using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Composition;
using Windows.Foundation;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Media;

using Bau.Libraries.LibChessGame.Models.Pieces;

namespace ChessViewer.Views.Board.Models
{
	/// <summary>
	///		Modelo con el contenido de las imágenes
	/// </summary>
	public class ImagesModel
	{
		public ImagesModel(PieceImagesListModel pieces)
		{
			Pieces = pieces;
		}

		/// <summary>
		///		Obtiene el nombre de archivo de un sprite
		/// </summary>
		private string GetFileName(PieceImageModel.GraphicTypes type, PieceBaseModel.PieceColor color)
		{
			string fileName = string.Empty;

				// Obtiene el nombre del directorio y el archivo
				if (type == PieceImageModel.GraphicTypes.Cell)
				{
					fileName = System.IO.Path.Combine(Controllers.HostController.SubPathBoard, Pieces.BoardPath + "/");
					if (color == PieceBaseModel.PieceColor.Black)
						fileName += "BoardDark.gif";
					else
						fileName += "BoardLight.gif";
				}
				else
				{
					fileName = System.IO.Path.Combine(Controllers.HostController.SubPathPiecess, Pieces.PiecesPath + "/");
					fileName += type.ToString() + color.ToString() + ".gif";
				}
				// Crea el sprite
				return fileName;
		}

		/// <summary>
		///		Crea / obtiene una imagen al diccionario
		/// </summary>
		public CompositionSurfaceBrush GetSurfaceBrush(PieceImageModel.GraphicTypes type, PieceBaseModel.PieceColor color)
		{
			// Cambia o añade una imagen del diccionarios
			if (Images.ContainsKey((type, color)))
				Images[(type, color)] = CreateSpriteBrush(GetFileName(type, color));
			else
				Images.Add((type, color), CreateSpriteBrush(GetFileName(type, color)));
			// Devuelve la imagen
			return Images[(type, color)];
		}

		/// <summary>
		///		Crea un sprite
		/// </summary>
		private CompositionSurfaceBrush CreateSpriteBrush(string fileName)
		{
			CompositionSurfaceBrush sprite = Pieces.Compositor.CreateSurfaceBrush();

				// Asigna la imagen
				sprite.Surface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///" + fileName));
				sprite.Stretch = CompositionStretch.Fill;
				// Devuelve el sprite
				return sprite;
		}

		/// <summary>
		///		Obtiene una brocha para un texto
		/// </summary>
		public CompositionSurfaceBrush GetTextBrush(string text)
		{
			CompositionSurfaceBrush _drawingBrush = Pieces.Compositor.CreateSurfaceBrush();
			CompositionDrawingSurface _drawingSurface = CanvasComposition
																.CreateCompositionGraphicsDevice(Pieces.Compositor, 
																								 CanvasDevice.GetSharedDevice())
																.CreateDrawingSurface(new Size(256, 256), 
																					  DirectXPixelFormat.B8G8R8A8UIntNormalized, DirectXAlphaMode.Premultiplied);

				// Dibuja el texto
				using (CanvasDrawingSession canvas = CanvasComposition.CreateDrawingSession(_drawingSurface))
				{
					Rect rect = new Rect(new Point(2, 2), (_drawingSurface.Size.ToVector2() - new Vector2(4, 4)).ToSize());

						// Limpia la superficie
						canvas.Clear(Colors.Transparent);
						// Rectángulo redondeado alrededor del texto
						canvas.FillRoundedRectangle(rect, 15, 15, Colors.LightBlue);
						canvas.DrawRoundedRectangle(rect, 15, 15, Colors.Gray, 2);
						// Pinta el texto
						canvas.DrawText(text, rect, Colors.Black, 
										new CanvasTextFormat()
											{
												FontFamily = "Verdana",
												FontSize = 32,
												WordWrapping = CanvasWordWrapping.WholeWord,
												VerticalAlignment = CanvasVerticalAlignment.Center,
												HorizontalAlignment = CanvasHorizontalAlignment.Center
											}
										);
				}
				// Devuelve la brocha
				return _drawingBrush;
		}

		/// <summary>
		///		Lista de piezas
		/// </summary>
		private PieceImagesListModel Pieces { get; }

		/// <summary>
		///		Imágenes de las piezas
		/// </summary>
		private Dictionary<(PieceImageModel.GraphicTypes, PieceBaseModel.PieceColor), CompositionSurfaceBrush> Images { get; } = new Dictionary<(PieceImageModel.GraphicTypes, PieceBaseModel.PieceColor color), CompositionSurfaceBrush>();
	}
}
