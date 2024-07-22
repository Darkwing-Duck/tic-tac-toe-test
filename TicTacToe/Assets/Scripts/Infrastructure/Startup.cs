using App.States;
using Presentation;
using Presentation.Popups;
using VContainer.Unity;

namespace Infrastructure
{
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
			var navigationPresenter = _presenterFactory.Create<AppNavigationPresenter>();
			navigationPresenter.ShowUnder(_scope.transform);
			
			var popupsLayerPresenter = _presenterFactory.Create<PopupsLayerPresenter>();
			popupsLayerPresenter.ShowUnder(_scope.transform);
			
			_appNavigator.GoToState<HomeState>();
		}
	}
}