using System;

using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace ChessViewer.Controllers.Extensors
{
	/// <summary>
	///		Extensiones para composición
	/// </summary>
	public static class CompositionExtensors
	{
		/// <summary>
		///		Obtiene el contenedor visual de un alemento
		/// </summary>
		public static ContainerVisual GetVisual(this UIElement element)
		{
			Visual hostVisual = ElementCompositionPreview.GetElementVisual(element);
			ContainerVisual root = hostVisual.Compositor.CreateContainerVisual();

				// Asigna la raíz al elemento
				ElementCompositionPreview.SetElementChildVisual(element, root);
				// Devuelve el elemento raíz
				return root;
		}
	}
}
