using System;
using Windows.UI.Xaml.Data;

using Bau.Libraries.LibChessGame.Models.Board.Movements;
using Bau.Libraries.LibChessGame.Models.Pieces;

namespace ChessViewer.Converters
{
	/// <summary>
	///		Conversor para los iconos asociados a los archivos del árbol de secciones
	/// </summary>
	public class FileIconConverter : IValueConverter
	{
		/// <summary>
		///		Convierte un tipo en un icono
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, string language)
		{ 
			if (value is MovementFigureModel movement)
				return GetIcon(movement);
			else
				return null;
		}

		/// <summary>
		///		Obtiene el icono asociado a un <see cref="MovementFigureModel"/>
		/// </summary>
		private string GetIcon(MovementFigureModel movement)
		{	
			string nameColor = "";
			string namePiece = "";

				// Obtiene el nombre del color
				if (movement.Color == PieceBaseModel.PieceColor.White)
					nameColor = "White";
				else
					nameColor = "Black";
				// Obtiene el nombre de la pieza
				switch (movement.OriginPiece)
				{ 
					case PieceBaseModel.PieceType.Pawn:
							namePiece = "Pawn";
						break;
					case PieceBaseModel.PieceType.Rook:
							namePiece = "Rook";
						break;
					case PieceBaseModel.PieceType.Knight:
							namePiece = "Knight";
						break;
					case PieceBaseModel.PieceType.Bishop:
							namePiece = "Bishop";
						break;
					case PieceBaseModel.PieceType.Queen:
							namePiece = "Queen";
						break;
					case PieceBaseModel.PieceType.King:
							namePiece = "King";
						break;
				}
				// Obtiene el nombre de la imagen
				if (!string.IsNullOrEmpty(nameColor) && !string.IsNullOrEmpty(namePiece))
					return $"ms-appx:///Assets/Images/{namePiece}{nameColor}.gif";
				else
					return null;
		}

		/// <summary>
		///		Convierte un valor de vuelta
		/// </summary>
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{ 
			throw new NotImplementedException();
		}
	}
}