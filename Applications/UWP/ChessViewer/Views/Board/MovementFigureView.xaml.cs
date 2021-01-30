using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Bau.Libraries.LibChessGame.ViewModels.Board.Movements;

namespace ChessViewer.Views.Board
{
	/// <summary>
	///		Control que muestra el icono y el texto de un movimiento
	/// </summary>
	public sealed partial class MovementFigureView : UserControl
	{
		// Propiedades
		public static readonly DependencyProperty MovementProperty = DependencyProperty.Register(nameof(Movement), typeof(MovementFigureViewModel),
																								 typeof(MovementFigureView), new PropertyMetadata(null));

		public MovementFigureView()
		{
			InitializeComponent();
			grdMovement.DataContext = this;
		}

		/// <summary>
		///		ViewModel
		/// </summary>
		public MovementFigureViewModel Movement 
		{ 
			get { return (MovementFigureViewModel) GetValue(MovementProperty); }
			set { SetValue(MovementProperty, value); }
		}
	}
}
