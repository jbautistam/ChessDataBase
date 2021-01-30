using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ChessViewer
{
    /// <summary>
    /// Proporciona un comportamiento específico de la aplicación para complementar la clase Application predeterminada.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Inicializa el objeto de aplicación Singleton. Esta es la primera línea de código creado
        /// ejecutado y, como tal, es el equivalente lógico de main() o WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Se invoca cuando el usuario final inicia la aplicación normalmente. Se usarán otros puntos
        /// de entrada cuando la aplicación se inicie para abrir un archivo específico, por ejemplo.
        /// </summary>
        /// <param name="args">Información detallada acerca de la solicitud y el proceso de inicio.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

				// No repetir la inicialización de la aplicación si la ventana tiene contenido todavía,
				// solo asegurarse de que la ventana está activa.
				if (rootFrame == null)
				{
					// Crear un marco para que actúe como contexto de navegación y navegar a la primera página.
					rootFrame = new Frame();

					rootFrame.NavigationFailed += OnNavigationFailed;

					if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
					{
						//TODO: Cargar el estado de la aplicación suspendida previamente
					}

					// Poner el marco en la ventana actual.
					Window.Current.Content = rootFrame;
				}

				if (args.PrelaunchActivated == false)
				{
					if (rootFrame.Content == null)
					{
						// Cuando no se restaura la pila de navegación, navegar a la primera página,
						// configurando la nueva página pasándole la información requerida como
						//parámetro de navegación
						rootFrame.Navigate(typeof(MainPage), args.Arguments);
					}
					// Asegurarse de que la ventana actual está activa.
					Window.Current.Activate();
				}
        }

        /// <summary>
        /// Se invoca cuando la aplicación la inicia normalmente el usuario final. Se usarán otros puntos
        /// </summary>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Se invoca al suspender la ejecución de la aplicación. El estado de la aplicación se guarda
        /// sin saber si la aplicación se terminará o se reanudará con el contenido
        /// de la memoria aún intacto.
        /// </summary>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Guardar el estado de la aplicación y detener toda actividad en segundo plano
            deferral.Complete();
        }
    }
}
