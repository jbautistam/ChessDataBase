using System;
using Windows.UI.Composition;

using Bau.Libraries.LibChessGame.Models.Pieces;

namespace ChessViewer.Views.Board.Models
{
	/// <summary>
	///		Clase con los datos de una imagen del tablero
	/// </summary>
	public class PieceImageModel
	{
		/// <summary>Tipos para los gráficos</summary>
		public enum GraphicTypes
		{
			/// <summary>Desconocido. No se debería utilizar</summary>
			Unknown,
			/// <summary>Celda</summary>
			Cell,
			/// <summary>Alfil</summary>
			Bishop,
			/// <summary>Rey</summary>
			King,
			/// <summary>Caballo</summary>
			Knight,
			/// <summary>Peón</summary>
			Pawn,
			/// <summary>Reina</summary>
			Queen,
			/// <summary>Torre</summary>
			Rook
		}
		/// <summary>
		///		Tipo
		/// </summary>
		public GraphicTypes Type { get; set; }

		/// <summary>
		///		Color
		/// </summary>
		public PieceBaseModel.PieceColor Color { get; set; }

		/// <summary>
		///		Fila
		/// </summary>
		public int Row { get; set; }

		/// <summary>
		///		Columna
		/// </summary>
		public int Column { get; set; }

		/// <summary>
		///		Sprite
		/// </summary>
		public SpriteVisual Sprite { get; set; }
	}
}
