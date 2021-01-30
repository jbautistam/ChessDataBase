using System;
using System.Collections.Generic;
using Windows.UI.Composition;

using Bau.Libraries.LibChessGame.Models.Pieces;

namespace ChessViewer.Views.Board.Models
{
	/// <summary>
	///		Lista de imágenes
	/// </summary>
	public class PieceImagesListModel : List<PieceImageModel>
	{
		public PieceImagesListModel(Compositor compositor, string boardPath, string piecesPath)
		{
			Compositor = compositor;
			BoardPath = boardPath; 
			PiecesPath = piecesPath; 
			Images = new ImagesModel(this);
		}

		/// <summary>
		///		Añade una pieza
		/// </summary>
		public PieceImageModel Add(PieceImageModel.GraphicTypes type, PieceBaseModel.PieceColor color, int row, int column)
		{
			PieceImageModel piece = new PieceImageModel
											{
												Type = type,
												Color = color,
												Row = row,
												Column = column,
												Sprite = CreateSprite(type, color)
											};

				// Añade la pieza a la colección
				Add(piece);
				// y la devuelve
				return piece;
		}

		/// <summary>
		///		Modifica los directorios de imágenes
		/// </summary>
		public void UpdatePath(string boardPath, string piecesPath)
		{
			// Cambia los directorios
			BoardPath = boardPath;
			PiecesPath = piecesPath;
			// Cambia las imágenes
			foreach (PieceImageModel piece in this)
				piece.Sprite = CreateSprite(piece.Type, piece.Color);
		}

		/// <summary>
		///		Busca una pieza en la lista
		/// </summary>
		public PieceImageModel Search(PieceImageModel.GraphicTypes cell, PieceBaseModel.PieceColor color, int row, int column)
		{
			// Busca la pieza
			foreach (PieceImageModel piece in this)
				if (piece.Type == cell && piece.Color == color && piece.Row == row && piece.Column == column)
					return piece;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return null;
		}

		/// <summary>
		///		Crea un sprite
		/// </summary>
		private SpriteVisual CreateSprite(PieceImageModel.GraphicTypes type, PieceBaseModel.PieceColor color)
		{
			SpriteVisual sprite = Compositor.CreateSpriteVisual();

				// Asigna la imagen
				sprite.Brush = Images.GetSurfaceBrush(type, color);
				// Devuelve el sprite
				return sprite;
		}

		/// <summary>
		///		Objeto central del manejo de imágenes
		/// </summary>
		internal Compositor Compositor { get; }

		/// <summary>
		///		Imágenes
		/// </summary>
		private ImagesModel Images { get; }

		/// <summary>
		///		Directorio de las imágenes de tablero
		/// </summary>
		internal string BoardPath { get; private set; }

		/// <summary>
		///		Directorio de las imágenes de piezas
		/// </summary>
		internal string PiecesPath { get; private set; }
	}
}
