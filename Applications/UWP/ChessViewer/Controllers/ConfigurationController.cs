using System;
using Windows.Storage;

namespace ChessViewer.Controllers
{
	/// <summary>
	///		Controlador de configuración
	/// </summary>
    public class ConfigurationController
    {
		/// <summary>
		///		Carga la configuración
		/// </summary>
		public void Load()
		{
			ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

				// Asigna los datos de la configuración
				Game = localSettings.Values[nameof(Game)]?.ToString();
				PathBoardImages = localSettings.Values[nameof(PathBoardImages)]?.ToString();
				PathPieceImages = localSettings.Values[nameof(PathPieceImages)]?.ToString();
				MustShowAnimations = ((bool?) localSettings.Values[nameof(MustShowAnimations)]) ?? true;
		}

		/// <summary>
		///		Graba la configuración
		/// </summary>
		public void Save()
		{
			ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

				// Asigna los datos de la configuración
				localSettings.Values[nameof(Game)] = Game;
				localSettings.Values[nameof(PathBoardImages)] = PathBoardImages;
				localSettings.Values[nameof(PathPieceImages)] = PathPieceImages;
				localSettings.Values[nameof(MustShowAnimations)] = MustShowAnimations;
		}

		/// <summary>
		///		Nombre del archivo con el juego actual
		/// </summary>
		public string Game { get; set; }

		/// <summary>
		///		Directorio con las imágenes de tablero
		/// </summary>
		public string PathBoardImages { get; set; }

		/// <summary>
		///		Directorio con las imágenes de las piezas
		/// </summary>
		public string PathPieceImages { get; set; }
		
		/// <summary>
		///		Indica si se deben mostrar animaciones
		/// </summary>
		public bool MustShowAnimations { get; set; }
    }
}
