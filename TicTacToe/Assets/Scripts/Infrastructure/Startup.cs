using App.States;
using Presentation;
using Presentation.Popups;
using VContainer.Unity;

namespace Infrastructure
{
	/// <summary>
	/// Entry point of the application
	/// </summary>
	public class Startup : IInitializable
	{
		private LifetimeScope _scope;
		private IPresenterFactory _presenterFactory;
		private IAppNavigatorService _appNavigator;

		public Startup(LifetimeScope scope, IPresenterFactory presenterFactory, IAppNavigatorService appNavigator)
		{
			_scope = scope;
			_presenterFactory = presenterFactory;
			_appNavigator = appNavigator;
		}

		public void Initialize()
		{
			// create root app presenter
			var navigationPresenter = _presenterFactory.Create<AppNavigationPresenter>();
			navigationPresenter.ShowUnder(_scope.transform);
			
			// create popups layer presenter, that will display popups
			var popupsLayerPresenter = _presenterFactory.Create<PopupsLayerPresenter>();
			popupsLayerPresenter.ShowUnder(_scope.transform);
			
			// go to the first screen
			_appNavigator.GoToState<HomeState>();
		}
	}
}