using System;
using System.Windows.Controls;

using Bau.Libraries.ChessDataBase.ViewModels.Solutions.Details.Files;

namespace Bau.ChessDataBase.Studio.Views.Web
{
	/// <summary>
	///		Ventana para mostrar el contenido de un archivo
	/// </summary>
	public partial class WebExplorerView : UserControl
	{
		public WebExplorerView(FileViewModel viewModel)
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{

		}
	}
}
