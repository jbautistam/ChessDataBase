using System;
using Windows.UI.Xaml.Data;

using Bau.Libraries.LibChessGame.Mvvm.Media;
using Windows.UI.Xaml.Media;

namespace ChessViewer.Converters
{
	/// <summary>
	///		Conversor para <see cref="MvvmColor"/>
	/// </summary>
	public class MvvmColorConverter : IValueConverter
	{
		/// <summary>
		///		Convierte un <see cref="MvvmColor"/> en un <see cref="System.Windows.Media.Color"/>
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, string language)
		{ 
			MvvmColor source = value as MvvmColor;

				// Convierte el color
				if (source == null)
					return new SolidColorBrush(Windows.UI.Colors.Black);
				else
					return new SolidColorBrush(Windows.UI.Color.FromArgb(source.A, source.R, source.G, source.B));
		}

		/// <summary>
		///		Convierte un <see cref="System.Windows.Media.Color"/> en <see cref="MvvmColor"/>
		/// </summary>
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{	
			throw new NotImplementedException();
		}
	}
}