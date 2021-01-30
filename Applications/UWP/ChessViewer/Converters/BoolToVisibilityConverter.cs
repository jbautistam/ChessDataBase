using System;
using Windows.UI.Xaml.Data;

namespace ChessViewer.Converters
{
	/// <summary>
	///		Conversor para transformar un valor booleano en un enumerado "Visibility"
	/// </summary>
	public class BoolToVisibilityConverter : IValueConverter
	{
		/// <summary>
		///		Convierte un valor boolean en un valor de visibilidad
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, string language)
		{ 
			if (value != null && (value is bool) && (bool) value)
				return Windows.UI.Xaml.Visibility.Visible;
			else
				return Windows.UI.Xaml.Visibility.Collapsed;
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
